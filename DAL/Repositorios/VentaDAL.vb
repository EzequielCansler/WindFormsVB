Imports System.Data.SqlClient
Imports Entidades

Public Class VentaDAL
    Public Shared Function ObtenerVentas() As List(Of Venta)
        Dim ventas As New List(Of Venta)
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()
            Dim query As String = "SELECT * FROM ventas"
            Dim cmd As New SqlCommand(query, conn)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                ventas.Add(New Venta(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetDouble(3)))
            End While
        End Using
        Return ventas
    End Function

    Public Shared Function ObtenerDetallesDeVenta(ventaID As Integer) As List(Of VentaItem)
        Dim detalles As New List(Of VentaItem)
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()
            Dim query As String = "SELECT * FROM ventasitems WHERE IDVenta = @IDVenta"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@IDVenta", ventaID)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                detalles.Add(New VentaItem(reader("ID"), reader("IDVenta"), reader("IDProducto"), reader("PrecioUnitario"), reader("Cantidad"), reader("PrecioTotal")))
            End While
        End Using
        Return detalles
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
