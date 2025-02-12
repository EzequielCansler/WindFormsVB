Imports DAL
Imports Entidades

Public Class ClienteBLL

    Public Shared Sub AgregarOEditarCliente(ID As Integer, cliente As String, correo As String, telefono As String)
        Try
            If ID = 0 Then
                ClienteDAL.AgregarCliente(cliente, correo, telefono)
            Else
                ClienteDAL.EditarCliente(ID, cliente, correo, telefono)
            End If
        Catch ex As Exception
            Throw New Exception("Error al agregar o editar el cliente: " & ex.Message)
        End Try
    End Sub

    Public Shared Function ObtenerClientes() As List(Of Cliente)
        Return ClienteDAL.ObtenerClientes()
    End Function

    Public Shared Sub EliminarCliente(ID As Integer)
        ClienteDAL.EliminarCliente(ID)
    End Sub
End Class
