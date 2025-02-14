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

    Public Shared Sub ModificarVenta(venta As Venta, detalles As List(Of VentaItem))
        Using connection As SqlConnection = ConexionBD.ObtenerConexion()
            connection.Open()

            ' Actualizar la venta (fecha, total, cliente)
            Dim queryVenta As String = "UPDATE ventas SET Fecha = @Fecha, Total = @Total, IDCliente = @IDCliente WHERE ID = @ID"
            Using command As New SqlCommand(queryVenta, connection)
                command.Parameters.AddWithValue("@Fecha", venta.Fecha)
                command.Parameters.AddWithValue("@Total", venta.Total)
                command.Parameters.AddWithValue("@IDCliente", venta.IDCliente)
                command.Parameters.AddWithValue("@ID", venta.ID)
                command.ExecuteNonQuery()
            End Using

            ' Obtener las cantidades actuales de los productos en la venta
            Dim queryStock As String = "SELECT IDProducto, Cantidad FROM ventasitems WHERE IDVenta = @IDVenta"
            Dim productosEnVenta As New Dictionary(Of Integer, Integer) ' IDProducto, Cantidad
            Using cmdStock As New SqlCommand(queryStock, connection)
                cmdStock.Parameters.AddWithValue("@IDVenta", venta.ID)
                Using reader As SqlDataReader = cmdStock.ExecuteReader()
                    While reader.Read()
                        productosEnVenta.Add(reader.GetInt32(0), reader.GetDouble(1)) ' ProductoID, Cantidad actual
                    End While
                End Using
            End Using

            ' Comprobar si la cantidad a restar o agregar es válida y actualizar los productos
            For Each item As VentaItem In detalles
                ' Verificar si el producto ya está en la venta
                If productosEnVenta.ContainsKey(item.IDProducto) Then
                    Dim cantidadActual As Integer = productosEnVenta(item.IDProducto)

                    ' Si se quiere agregar productos, asegurarse de que no exceda el stock máximo (ej. 5 unidades disponibles)
                    If item.Cantidad > cantidadActual Then
                        ' Agregar productos si es posible
                        Dim queryAgregarProducto As String = "UPDATE ventasitems SET Cantidad = @Cantidad, PrecioTotal = @PrecioTotal WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto"
                        Using cmdAgregar As New SqlCommand(queryAgregarProducto, connection)
                            cmdAgregar.Parameters.AddWithValue("@Cantidad", item.Cantidad)
                            cmdAgregar.Parameters.AddWithValue("@PrecioTotal", item.Cantidad * item.PrecioUnitario)
                            cmdAgregar.Parameters.AddWithValue("@IDVenta", venta.ID)
                            cmdAgregar.Parameters.AddWithValue("@IDProducto", item.IDProducto)
                            cmdAgregar.ExecuteNonQuery()
                        End Using
                    Else
                        ' Si se quiere restar productos, asegurarse de que no se reste más de lo que hay en stock
                        If item.Cantidad <= cantidadActual Then
                            Dim queryRestarProducto As String = "UPDATE ventasitems SET Cantidad = @Cantidad, PrecioTotal = @PrecioTotal WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto"
                            Using cmdRestar As New SqlCommand(queryRestarProducto, connection)
                                cmdRestar.Parameters.AddWithValue("@Cantidad", item.Cantidad)
                                cmdRestar.Parameters.AddWithValue("@PrecioTotal", item.Cantidad * item.PrecioUnitario)
                                cmdRestar.Parameters.AddWithValue("@IDVenta", venta.ID)
                                cmdRestar.Parameters.AddWithValue("@IDProducto", item.IDProducto)
                                cmdRestar.ExecuteNonQuery()
                            End Using
                        Else
                            Throw New Exception("No puedes restar más productos de los que existen en la venta.")
                        End If
                    End If
                Else
                    ' Si el producto no está en la venta, agregarlo
                    Dim queryInsertar As String = "INSERT INTO ventasitems (IDVenta, IDProducto, Cantidad, PrecioUnitario, PrecioTotal) VALUES (@IDVenta, @IDProducto, @Cantidad, @PrecioUnitario, @PrecioTotal)"
                    Using cmdInsertar As New SqlCommand(queryInsertar, connection)
                        cmdInsertar.Parameters.AddWithValue("@IDVenta", venta.ID)
                        cmdInsertar.Parameters.AddWithValue("@IDProducto", item.IDProducto)
                        cmdInsertar.Parameters.AddWithValue("@Cantidad", item.Cantidad)
                        cmdInsertar.Parameters.AddWithValue("@PrecioUnitario", item.PrecioUnitario)
                        cmdInsertar.Parameters.AddWithValue("@PrecioTotal", item.Cantidad * item.PrecioUnitario)
                        cmdInsertar.ExecuteNonQuery()
                    End Using
                End If
            Next
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
    Public Shared Sub AgregarProductoAVenta(idVenta As Integer, idProducto As Integer, precioUnitario As Double, cantidad As Integer, precioTotal As Double)
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            Dim query As String = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal)"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IDVenta", idVenta)
                cmd.Parameters.AddWithValue("@IDProducto", idProducto)
                cmd.Parameters.AddWithValue("@PrecioUnitario", precioUnitario)
                cmd.Parameters.AddWithValue("@Cantidad", cantidad)
                cmd.Parameters.AddWithValue("@PrecioTotal", precioTotal)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Shared Sub RestarCantidadProductoEnVenta(idVenta As Integer, idProducto As Integer, cantidad As Integer)
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            Dim query As String = "UPDATE ventasitems SET Cantidad = Cantidad - @Cantidad, PrecioTotal = PrecioUnitario * (Cantidad - @Cantidad) WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IDVenta", idVenta)
                cmd.Parameters.AddWithValue("@IDProducto", idProducto)
                cmd.Parameters.AddWithValue("@Cantidad", cantidad)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
            query = "SELECT Cantidad FROM ventasitems WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IDVenta", idVenta)
                cmd.Parameters.AddWithValue("@IDProducto", idProducto)
                Dim nuevaCantidad As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Si la cantidad es 0 o menor, eliminamos el producto de la venta
                If nuevaCantidad <= 0 Then
                    query = "DELETE FROM ventasitems WHERE IDVenta = @IDVenta AND IDProducto = @IDProducto"
                    Using deleteCmd As New SqlCommand(query, conn)
                        deleteCmd.Parameters.AddWithValue("@IDVenta", idVenta)
                        deleteCmd.Parameters.AddWithValue("@IDProducto", idProducto)
                        deleteCmd.ExecuteNonQuery()
                    End Using
                End If
            End Using
        End Using
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
End Class
