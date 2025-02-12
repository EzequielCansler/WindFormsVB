Imports BLL

Public Class FormProducto
    Private Sub FormCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarProductos()
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
    End Sub


    Private Sub CargarProductos()
        ProductoDataView.DataSource = ProductoBLL.ObtenerProductos()
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
    End Sub


    Private Sub ProductoDataView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles ProductoDataView.CellClick
        If e.RowIndex >= 0 Then
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
        End If
    End Sub


    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Dim nuevoProductoForm As New FormNuevoProducto()
        nuevoProductoForm.ShowDialog()
        CargarProductos()
    End Sub


    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If ProductoDataView.SelectedRows.Count > 0 Then
            Try
                Dim id As Integer = Convert.ToInt32(ProductoDataView.SelectedRows(0).Cells("ID").Value)
                ProductoBLL.EliminarProducto(id)
                CargarProductos()
            Catch ex As Exception
                MessageBox.Show("Error al eliminar el producto: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Seleccione un producto para eliminar.")
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If ProductoDataView.SelectedRows.Count > 0 Then
            Try
                Dim id As Integer = Convert.ToInt32(ProductoDataView.SelectedRows(0).Cells("ID").Value)
                Dim nombre As String = ProductoDataView.SelectedRows(0).Cells("Nombre").Value.ToString()
                Dim precio As Decimal = Convert.ToDecimal(ProductoDataView.SelectedRows(0).Cells("Precio").Value)
                Dim categoria As String = ProductoDataView.SelectedRows(0).Cells("Categoria").Value.ToString()

                Dim editarForm As New FormEditarProducto(id, nombre, precio, categoria)
                editarForm.ShowDialog()
                CargarProductos()
            Catch ex As Exception
                MessageBox.Show("Error al modificar el producto: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Seleccione un producto para modificar.")
        End If
    End Sub
    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub
End Class