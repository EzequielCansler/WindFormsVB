Imports BLL

Public Class FormNuevoProducto
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try

            If String.IsNullOrWhiteSpace(txtNombre.Text) OrElse
               String.IsNullOrWhiteSpace(txtPrecio.Text) OrElse
               String.IsNullOrWhiteSpace(txtCategoria.Text) Then
                MessageBox.Show("Todos los campos son obligatorios.")
                Exit Sub
            End If

            Dim precioDecimal As Decimal
            If Not Decimal.TryParse(txtPrecio.Text, precioDecimal) Then
                MessageBox.Show("Ingrese un precio válido.")
                Exit Sub
            End If

            ProductoBLL.AgregarOEditarProducto(0, txtNombre.Text, precioDecimal, txtCategoria.Text)
            MessageBox.Show("Producto agregado correctamente.")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error al guardar Producto: " & ex.Message)
        End Try
    End Sub

End Class



