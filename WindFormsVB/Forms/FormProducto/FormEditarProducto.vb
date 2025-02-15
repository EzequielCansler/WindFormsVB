Imports BLL

Public Class FormEditarProducto

    Private productoId As Integer

    Public Sub New(id As Integer, nombre As String, precio As Decimal, categoria As String)
        InitializeComponent()
        productoId = id
        txtNombre.Text = nombre
        txtPrecio.Text = precio.ToString("F2")
        txtCategoria.Text = categoria
    End Sub


    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            ' Validar que los campos no estén vacíos (sin espacios en blanco)
            If String.IsNullOrWhiteSpace(txtNombre.Text.Trim()) OrElse
               String.IsNullOrWhiteSpace(txtPrecio.Text.Trim()) OrElse
               String.IsNullOrWhiteSpace(txtCategoria.Text.Trim()) Then
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Validar que el precio sea un número válido
            Dim precioDecimal As Decimal
            If Not Decimal.TryParse(txtPrecio.Text.Trim(), precioDecimal) Then
                MessageBox.Show("Ingrese un precio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Confirmar antes de guardar
            Dim confirmacion As DialogResult = MessageBox.Show("¿Desea guardar los cambios?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirmacion = DialogResult.No Then
                Return
            End If

            ' Llamar a la capa de negocio para guardar
            ProductoBLL.AgregarOEditarProducto(productoId, txtNombre.Text.Trim(), precioDecimal, txtCategoria.Text.Trim())
            MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error al actualizar producto: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub


End Class


