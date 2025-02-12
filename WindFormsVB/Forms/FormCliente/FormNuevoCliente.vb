Imports BLL

Public Class FormNuevoCliente

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try

            If String.IsNullOrWhiteSpace(txtCliente.Text) OrElse
               String.IsNullOrWhiteSpace(txtCorreo.Text) OrElse
               String.IsNullOrWhiteSpace(txtTelefono.Text) Then
                MessageBox.Show("Todos los campos son obligatorios.")
                Exit Sub
            End If

            ClienteBLL.AgregarOEditarCliente(0, txtCliente.Text, txtCorreo.Text, txtTelefono.Text)
            MessageBox.Show("Cliente agregado correctamente.")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error al guardar cliente: " & ex.Message)
        End Try
    End Sub

    Private Sub FormNuevoCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
