Imports System.Data.SqlClient
Imports DAL
Imports Entidades

Public Class VentaBLL

    Public Shared Function ObtenerVentas() As (List(Of Venta), List(Of String))
        Return VentaDAL.ObtenerVentas()
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
    Public Shared Sub ModificarVenta(venta As Venta, detalles As List(Of VentaItem))

        VentaDAL.ModificarVenta(venta, detalles)
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
    Public Shared Sub AgregarProductoAVenta(idVenta As Integer, idProducto As Integer, precioUnitario As Double, cantidad As Integer, precioTotal As Double)
        VentaDAL.AgregarProductoAVenta(idVenta, idProducto, precioUnitario, cantidad, precioTotal)
    End Sub
    Public Shared Sub RestarCantidadProductoEnVenta(idVenta As Integer, idProducto As Integer, cantidad As Integer)
        VentaDAL.RestarCantidadProductoEnVenta(idVenta, idProducto, cantidad)
    End Sub
    Public Shared Function EliminarVenta(ventaID As Integer) As Boolean
        Return VentaDAL.EliminarVenta(ventaID)
    End Function

End Class
