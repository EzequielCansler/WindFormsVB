Imports System.Data.SqlClient
Imports Entidades

Public Class VentaDAL
    Public Shared Function ObtenerVentas() As (List(Of Venta), List(Of String))
        Dim ventas As New List(Of Venta)
        Dim nombreCliente As New List(Of String)
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()
            Dim query As String = " SELECT V.ID, V.IDCliente, V.Fecha, V.Total, C.Cliente
                                    FROM ventas AS V
                                    JOIN Clientes AS C ON V.IDCliente = C.ID
                                  "
            Dim cmd As New SqlCommand(query, conn)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                ventas.Add(New Venta(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetDouble(3)))
                nombreCliente.Add(reader.GetString(4))
            End While
        End Using
        Return (ventas, nombreCliente)
    End Function
    Public Shared Function ObtenerVentasPorOrden(orden As String) As List(Of Venta)
        Dim ventas As New List(Of Venta)()

        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()

            ' Consulta SQL para ordenar según la fecha
            Dim query As String
            If orden = "ascendente" Then
                query = "SELECT * FROM Ventas ORDER BY Fecha ASC"
            Else
                query = "SELECT * FROM Ventas ORDER BY Fecha DESC"
            End If

            Using cmd As New SqlCommand(query, conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim venta As New Venta()
                        venta.ID = reader.GetInt32(reader.GetOrdinal("ID"))
                        venta.IDCliente = reader.GetInt32(reader.GetOrdinal("IDCliente"))
                        venta.Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha"))
                        venta.Total = reader.GetDouble(reader.GetOrdinal("Total"))
                        ventas.Add(venta)
                    End While
                End Using
            End Using
        End Using

        Return ventas
    End Function

    Public Shared Function ObtenerVentaPorID(ID As Integer) As (Venta, String)
        Dim venta As Venta = Nothing
        Dim cliente As String = String.Empty

        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()
            Dim query As String = " SELECT V.ID, V.IDCliente, V.Fecha, V.Total, C.Cliente
                                    FROM ventas AS V
                                    JOIN Clientes AS C ON V.IDCliente = C.ID
                                    WHERE V.ID = @ID        
                                  "

            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ID", ID)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                venta = New Venta(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetDouble(3))
                cliente = reader.GetString(4)
            End If

        End Using

        Return (venta, cliente)
    End Function

    Public Shared Function ObtenerDetallesDeVenta(ventaID As Integer) As (List(Of VentaItem), List(Of String))
        Dim detalles As New List(Of VentaItem)
        Dim nombresProductos As New List(Of String)

        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()

            Dim query As String = "SELECT V.ID, V.IDVenta, V.IDProducto, P.Nombre, V.PrecioUnitario, V.Cantidad, V.PrecioTotal
                               FROM ventasitems AS V
                               JOIN Productos AS P ON V.IDProducto = P.ID
                               WHERE V.IDVenta = @IDVenta"

            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@IDVenta", ventaID)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                detalles.Add(New VentaItem(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetInt32(2),
                reader.GetDouble(4),
                reader.GetDouble(5),
                reader.GetDouble(6)
            ))

                nombresProductos.Add(reader.GetString(3))
            End While
        End Using

        Return (detalles, nombresProductos)
    End Function

    Public Shared Sub CrearVenta(venta As Venta, detalles As List(Of VentaItem))
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()
            Dim transaction As SqlTransaction = conn.BeginTransaction()
            Try

                Dim queryVenta As String = "INSERT INTO ventas (IDCliente, Fecha, Total) OUTPUT INSERTED.ID VALUES (@IDCliente, @Fecha, @Total)"
                Dim cmdVenta As New SqlCommand(queryVenta, conn, transaction)
                cmdVenta.Parameters.AddWithValue("@IDCliente", venta.IDCliente)
                cmdVenta.Parameters.AddWithValue("@Fecha", venta.Fecha)
                cmdVenta.Parameters.AddWithValue("@Total", venta.Total)
                Dim idVenta As Integer = cmdVenta.ExecuteScalar()

                Dim queryItem As String = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal)"
                For Each item In detalles
                    Dim cmdItem As New SqlCommand(queryItem, conn, transaction)
                    cmdItem.Parameters.AddWithValue("@IDVenta", idVenta)
                    cmdItem.Parameters.AddWithValue("@IDProducto", item.IDProducto)
                    cmdItem.Parameters.AddWithValue("@PrecioUnitario", item.PrecioUnitario)
                    cmdItem.Parameters.AddWithValue("@Cantidad", item.Cantidad)
                    cmdItem.Parameters.AddWithValue("@PrecioTotal", item.PrecioTotal)
                    cmdItem.ExecuteNonQuery()
                Next

                transaction.Commit()
            Catch ex As Exception
                transaction.Rollback()
                Throw New Exception("Error al registrar la venta: " & ex.Message)
            End Try
        End Using
    End Sub
    Private Shared Function ObtenerPrecioUnitario(productoID As Integer, conexion As SqlConnection) As Double
        Dim query As String = "SELECT Precio FROM productos WHERE ID = @IDProducto"
        Using cmd As New SqlCommand(query, conexion)
            cmd.Parameters.AddWithValue("@IDProducto", productoID)
            Return Convert.ToDouble(cmd.ExecuteScalar())
        End Using
    End Function

    Public Shared Function ProductoEnVenta(IDVenta As Integer, IDProducto As Integer) As Boolean
        Try
            Using conexion As SqlConnection = ConexionBD.ObtenerConexion()
                conexion.Open()
                Dim cmd As New SqlCommand("SELECT COUNT(*) FROM ventasitems WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto", conexion)
                cmd.Parameters.AddWithValue("@IDVenta", IDVenta)
                cmd.Parameters.AddWithValue("@IDProducto", IDProducto)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                Return count > 0
            End Using
        Catch ex As Exception
            Throw New ApplicationException("Error al verificar si el producto está en la venta: " + ex.Message, ex)
        End Try
    End Function
    Public Shared Sub ActualizarCantidadProductoEnVenta(idVenta As Integer, idProducto As Integer, cantidad As Integer)
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            Dim query As String = "UPDATE ventasitems SET Cantidad = Cantidad + @Cantidad, PrecioTotal = PrecioUnitario * (Cantidad + @Cantidad) WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IDVenta", idVenta)
                cmd.Parameters.AddWithValue("@IDProducto", idProducto)
                cmd.Parameters.AddWithValue("@Cantidad", cantidad)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Shared Function ObtenerDetallesVenta(idVenta As Integer) As List(Of VentaItem)
        Dim detalles As New List(Of VentaItem)
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()
            Dim query As String = "SELECT * FROM VentaDetalle WHERE IDVenta = @IDVenta"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IDVenta", idVenta)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        detalles.Add(New VentaItem(reader("ID"), reader("IDVenta"), reader("IDProducto"), reader("PrecioUnitario"), reader("Cantidad"), reader("PrecioTotal")))
                    End While
                End Using
            End Using
        End Using
        Return detalles
    End Function

    Public Shared Sub ModificarVenta(venta As Venta, detalles As List(Of VentaItem), accion As String, transaction As SqlTransaction)
        Dim conn As SqlConnection = transaction.Connection

        If accion = "modificar" Then
            ' Modificar la venta
            Dim queryVenta As String = "UPDATE ventas SET IDCliente = @IDCliente WHERE ID = @ID"
            Using cmdVenta As New SqlCommand(queryVenta, conn, transaction)
                cmdVenta.Parameters.AddWithValue("@ID", venta.ID)
                cmdVenta.Parameters.AddWithValue("@IDCliente", venta.IDCliente)
                cmdVenta.ExecuteNonQuery()
            End Using

            ' Eliminar productos previos
            Dim queryEliminar As String = "DELETE FROM ventasitems WHERE IDVenta = @IDVenta"
            Using cmdEliminar As New SqlCommand(queryEliminar, conn, transaction)
                cmdEliminar.Parameters.AddWithValue("@IDVenta", venta.ID)
                cmdEliminar.ExecuteNonQuery()
            End Using

            ' Insertar nuevos productos
            Dim queryInsert As String = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal)"
            For Each detalle In detalles
                Using cmdDetalle As New SqlCommand(queryInsert, conn, transaction)
                    cmdDetalle.Parameters.AddWithValue("@IDVenta", detalle.IDVenta)
                    cmdDetalle.Parameters.AddWithValue("@IDProducto", detalle.IDProducto)
                    cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario)
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad)
                    cmdDetalle.Parameters.AddWithValue("@PrecioTotal", detalle.PrecioTotal)
                    cmdDetalle.ExecuteNonQuery()
                End Using
            Next

        ElseIf accion = "agregarProducto" Then
            ' Agregar o actualizar cantidad del producto en la venta
            Dim queryActualizar As String = "UPDATE ventasitems SET Cantidad = Cantidad + @Cantidad, PrecioTotal = PrecioUnitario * (Cantidad + @Cantidad) WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto"
            Dim filasAfectadas As Integer
            Using cmdActualizar As New SqlCommand(queryActualizar, conn, transaction)
                cmdActualizar.Parameters.AddWithValue("@IDVenta", venta.ID)
                cmdActualizar.Parameters.AddWithValue("@IDProducto", detalles(0).IDProducto)
                cmdActualizar.Parameters.AddWithValue("@Cantidad", detalles(0).Cantidad)
                filasAfectadas = cmdActualizar.ExecuteNonQuery()
            End Using

            ' Si el producto no existe en la venta, insertarlo
            If filasAfectadas = 0 Then
                Dim queryInsert As String = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal)"
                Using cmdInsert As New SqlCommand(queryInsert, conn, transaction)
                    cmdInsert.Parameters.AddWithValue("@IDVenta", detalles(0).IDVenta)
                    cmdInsert.Parameters.AddWithValue("@IDProducto", detalles(0).IDProducto)
                    cmdInsert.Parameters.AddWithValue("@PrecioUnitario", detalles(0).PrecioUnitario)
                    cmdInsert.Parameters.AddWithValue("@Cantidad", detalles(0).Cantidad)
                    cmdInsert.Parameters.AddWithValue("@PrecioTotal", detalles(0).PrecioTotal)
                    cmdInsert.ExecuteNonQuery()
                End Using
            End If

        ElseIf accion = "eliminarProducto" Then
            ' Restar cantidad del producto en la venta
            Dim queryRestar As String = "UPDATE ventasitems SET Cantidad = Cantidad - @Cantidad, PrecioTotal = PrecioUnitario * (Cantidad - @Cantidad) WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto AND Cantidad >= @Cantidad"
            Using cmdRestar As New SqlCommand(queryRestar, conn, transaction)
                cmdRestar.Parameters.AddWithValue("@IDVenta", venta.ID)
                cmdRestar.Parameters.AddWithValue("@IDProducto", detalles(0).IDProducto)
                cmdRestar.Parameters.AddWithValue("@Cantidad", detalles(0).Cantidad)
                cmdRestar.ExecuteNonQuery()
            End Using

            ' Eliminar el producto si la cantidad es 0 o menor
            Dim queryEliminarProducto As String = "DELETE FROM ventasitems WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto AND Cantidad <= 0"
            Using cmdEliminar As New SqlCommand(queryEliminarProducto, conn, transaction)
                cmdEliminar.Parameters.AddWithValue("@IDVenta", venta.ID)
                cmdEliminar.Parameters.AddWithValue("@IDProducto", detalles(0).IDProducto)
                cmdEliminar.ExecuteNonQuery()
            End Using
        End If
    End Sub

    Public Shared Function EliminarVenta(ventaID As Integer) As Boolean
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Dim query As String = "DELETE FROM Ventas WHERE ID = @VentaID"

                Using command As New SqlCommand(query, conn)
                    command.Parameters.AddWithValue("@VentaID", ventaID)

                    Dim rowsAffected As Integer = command.ExecuteNonQuery()
                    Return rowsAffected > 0
                End Using
            End Using
        Catch ex As Exception

            Return False
        End Try
    End Function
    Public Shared Function RollbackTransaccion() As String
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Using transaction As SqlTransaction = conn.BeginTransaction()
                    Try
                        transaction.Rollback()
                        Return "Los cambios han sido descartados exitosamente."
                    Catch ex As Exception
                        Throw New Exception("Error al intentar descartar cambios: " & ex.Message)
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al conectarse a la base de datos: " & ex.Message)
        End Try
    End Function
End Class


