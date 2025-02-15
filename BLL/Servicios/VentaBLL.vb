Imports System.Data.SqlClient
Imports DAL
Imports Entidades

Public Class VentaBLL
    Private Shared transaccionActual As SqlTransaction = Nothing
    Private Shared conexionActual As SqlConnection = Nothing
    Public Shared Sub IniciarTransaccion()
        conexionActual = ConexionBD.ObtenerConexion()
        conexionActual.Open()
        transaccionActual = conexionActual.BeginTransaction()
    End Sub

    Public Shared Sub ConfirmarTransaccion()
        If transaccionActual IsNot Nothing Then
            transaccionActual.Commit()
            transaccionActual.Dispose()
            conexionActual.Close()
        End If
    End Sub
    Public Shared Sub CancelarTransaccion()
        If transaccionActual IsNot Nothing Then
            transaccionActual.Rollback()
            transaccionActual.Dispose()
            conexionActual.Close()
        End If
    End Sub
    Public Shared Function ObtenerVentas() As (List(Of Venta), List(Of String))
        Return VentaDAL.ObtenerVentas()
    End Function
    Public Shared Function ObtenerVentasPorOrden(orden As String) As List(Of Venta)
        Return VentaDAL.ObtenerVentasPorOrden(orden)
    End Function
    Public Shared Function ObtenerVentaPorID(ID As Integer) As (Venta, String)
        Return VentaDAL.ObtenerVentaPorID(ID)
    End Function

    Public Shared Function ObtenerDetallesDeVenta(ID As Integer) As (List(Of VentaItem), List(Of String))
        Return VentaDAL.ObtenerDetallesDeVenta(ID)
    End Function

    Public Shared Sub CrearVenta(venta As Venta, detalles As List(Of VentaItem))
        VentaDAL.CrearVenta(venta, detalles)
    End Sub

    Public Shared Function ActualizarCantidadProductoEnVenta(ventaID As Integer, productoID As Integer, nuevaCantidad As Integer) As Boolean
        ' Validamos que la cantidad no sea negativa
        If nuevaCantidad < 0 Then
            Return False
        End If

        ' Actualizamos la cantidad en la base de datos
        VentaDAL.ActualizarCantidadProductoEnVenta(ventaID, productoID, nuevaCantidad)
        Return True
    End Function

    Public Shared Function ObtenerDetallesConNombresDeProductos(ventaID As Integer) As (List(Of VentaItem), List(Of String))
        Dim resultado = VentaDAL.ObtenerDetallesDeVenta(ventaID)
        Dim detalles As List(Of VentaItem) = resultado.Item1
        Dim nombresProductos As List(Of String) = ObtenerNombresDeProductos(detalles)
        Return (detalles, nombresProductos)
    End Function
    Private Shared Function ObtenerNombresDeProductos(detalles As List(Of VentaItem)) As List(Of String)
        Dim nombres As New List(Of String)
        For Each item In detalles
            Dim producto As Producto = ProductoBLL.ObtenerProductoPorID(item.IDProducto)
            nombres.Add(producto.Nombre)
        Next
        Return nombres
    End Function

    Public Shared Sub ActualizarCantidadProducto(idVenta As Integer, idProducto As Integer, cantidad As Integer)
        VentaDAL.ActualizarCantidadProductoEnVenta(idVenta, idProducto, cantidad)
    End Sub

    Public Shared Function ProductoEnVenta(idVenta As Integer, idProducto As Integer) As Boolean
        Return VentaDAL.ProductoEnVenta(idVenta, idProducto)
    End Function
    Public Shared Function ObtenerDetallesVenta(idVenta As Integer) As List(Of VentaItem)
        Return VentaDAL.ObtenerDetallesVenta(idVenta)
    End Function
    Public Shared Sub ModificarVenta(venta As Venta, detalles As List(Of VentaItem), accion As String)
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()
            Using transaction As SqlTransaction = conn.BeginTransaction()
                Try
                    VentaDAL.ModificarVenta(venta, detalles, accion, transaction)
                    transaction.Commit()
                Catch ex As Exception
                    transaction.Rollback()
                    Throw New Exception("Error al modificar la venta: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub


    Public Shared Sub SumarProducto(venta As Venta, idProducto As Integer, cantidad As Integer)
        Dim detalles As List(Of VentaItem) = VentaDAL.ObtenerDetallesVenta(venta.ID)
        Dim detalleExistente As VentaItem = detalles.FirstOrDefault(Function(d) d.IDProducto = idProducto)

        If detalleExistente IsNot Nothing Then
            detalleExistente.Cantidad += cantidad
            detalleExistente.PrecioTotal = detalleExistente.Cantidad * detalleExistente.PrecioUnitario
            ModificarVenta(venta, New List(Of VentaItem) From {detalleExistente}, "modificar")
        Else
            Dim producto As Producto = ProductoBLL.ObtenerProductoPorID(idProducto)
            Dim nuevoDetalle As New VentaItem(0, venta.ID, idProducto, producto.Precio, cantidad, producto.Precio * cantidad)
            ModificarVenta(venta, New List(Of VentaItem) From {nuevoDetalle}, "agregarProducto")
        End If
    End Sub

    Public Shared Sub RestarProducto(venta As Venta, idProducto As Integer, cantidad As Integer)
        Dim detalles As List(Of VentaItem) = VentaDAL.ObtenerDetallesVenta(venta.ID)
        Dim detalleExistente As VentaItem = detalles.FirstOrDefault(Function(d) d.IDProducto = idProducto)

        If detalleExistente IsNot Nothing Then
            detalleExistente.Cantidad -= cantidad

            If detalleExistente.Cantidad <= 0 Then
                ModificarVenta(venta, New List(Of VentaItem) From {detalleExistente}, "eliminarProducto")
            Else
                detalleExistente.PrecioTotal = detalleExistente.Cantidad * detalleExistente.PrecioUnitario
                ModificarVenta(venta, New List(Of VentaItem) From {detalleExistente}, "modificar")
            End If
        End If
    End Sub
    Public Shared Function EliminarVenta(ventaID As Integer) As Boolean
        Return VentaDAL.EliminarVenta(ventaID)
    End Function
    Public Shared Function RollbackTransaccion() As String
        Try
            Return VentaDAL.RollbackTransaccion()
        Catch ex As Exception
            Throw New Exception("Error al realizar el rollback: " & ex.Message)
        End Try
    End Function
    Public Shared Function CalcularTotalProductos(ventaID As Integer) As Double
        Dim items = VentaDAL.ObtenerDetallesDeVenta(ventaID)
        Dim total As Decimal = 0

        For Each item In items.Item1
            total += item.PrecioTotal
        Next

        Return total
    End Function
End Class
