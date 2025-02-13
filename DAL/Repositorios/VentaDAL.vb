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
End Class
