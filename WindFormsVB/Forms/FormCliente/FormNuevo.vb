Imports Entidades

Public Class FormNuevo


    Private Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click

        If String.IsNullOrEmpty(txtNombre.Text) OrElse
           String.IsNullOrEmpty(txtApellido.Text) OrElse
           String.IsNullOrEmpty(txtEmail.Text) OrElse
           String.IsNullOrEmpty(txtTelefono.Text) Then
            MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else

            Dim cliente As New Cliente With {
                .Nombre = txtNombre.Text,
                .Apellido = txtApellido.Text,
                .Email = txtEmail.Text,
                .Telefono = txtTelefono.Text
            }

            MessageBox.Show("Cliente creado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)


            txtNombre.Clear()
            txtApellido.Clear()
            txtEmail.Clear()
            txtTelefono.Clear()
        End If
    End Sub
End Class
