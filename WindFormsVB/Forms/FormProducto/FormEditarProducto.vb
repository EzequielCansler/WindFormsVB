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
            Dim precioDecimal As Decimal
            If Not Decimal.TryParse(txtPrecio.Text, precioDecimal) Then
                MessageBox.Show("Ingrese un precio válido.")
                Exit Sub
            End If

            ProductoBLL.AgregarOEditarProducto(productoId, txtNombre.Text, precioDecimal, txtCategoria.Text)
            MessageBox.Show("Producto actualizado correctamente.")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error al actualizar producto: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

End Class


