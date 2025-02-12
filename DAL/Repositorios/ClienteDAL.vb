Imports System.Data.SqlClient
Imports Entidades

Public Class ClienteDAL

    Public Shared Function ObtenerClientes() As List(Of Cliente)
        Dim clientes As New List(Of Cliente)
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Dim query As String = "SELECT ID, Cliente, Correo, Telefono FROM Clientes"
                Using cmd As New SqlCommand(query, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            clientes.Add(New Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener los clientes: " & ex.Message)
        End Try
        Return clientes
    End Function


    Public Shared Sub AgregarCliente(cliente As String, correo As String, telefono As String)
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Dim query As String = "INSERT INTO Clientes (Cliente, Correo, Telefono) VALUES (@Cliente, @Correo, @Telefono)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Cliente", cliente)
                    cmd.Parameters.AddWithValue("@Correo", correo)
                    cmd.Parameters.AddWithValue("@Telefono", telefono)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al agregar el cliente: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub EditarCliente(ID As Integer, cliente As String, correo As String, telefono As String)
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Dim query As String = "UPDATE Clientes SET Cliente = @Cliente, Correo = @Correo, Telefono = @Telefono WHERE ID = @id"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.Parameters.AddWithValue("@Cliente", cliente)
                    cmd.Parameters.AddWithValue("@Correo", correo)
                    cmd.Parameters.AddWithValue("@Telefono", telefono)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al editar el cliente: " & ex.Message)
        End Try
    End Sub


    Public Shared Sub EliminarCliente(ID As Integer)
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Dim query As String = "DELETE FROM Clientes WHERE ID = @ID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar el cliente: " & ex.Message)
        End Try
    End Sub
End Class
