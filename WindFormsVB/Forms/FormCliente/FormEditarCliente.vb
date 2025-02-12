Imports BLL

Public Class FormEditarCliente
    Private clienteId As Integer

    Public Sub New(id As Integer, cliente As String, correo As String, telefono As String)
        InitializeComponent()
        clienteId = id
        txtCliente.Text = cliente
        txtCorreo.Text = correo
        txtTelefono.Text = telefono
    End Sub


    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            ClienteBLL.AgregarOEditarCliente(clienteId, txtCliente.Text, txtCorreo.Text, txtTelefono.Text)
            MessageBox.Show("Cliente actualizado correctamente.")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error al actualizar cliente: " & ex.Message)
        End Try
    End Sub

End Class
