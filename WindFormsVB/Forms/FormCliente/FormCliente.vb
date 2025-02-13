Imports BLL

Public Class FormCliente

    Private Sub FormCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarClientes()
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
    End Sub


    Private Sub CargarClientes()
        ClienteDataView.DataSource = ClienteBLL.ObtenerClientes()
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
    End Sub


    Private Sub ClienteDataView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles ClienteDataView.CellClick
        If e.RowIndex >= 0 Then
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
        End If
    End Sub


    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Dim nuevoClienteForm As New FormNuevoCliente()
        nuevoClienteForm.ShowDialog()
        CargarClientes()
    End Sub


    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If ClienteDataView.SelectedRows.Count > 0 Then
            Try
                Dim id As Integer = Convert.ToInt32(ClienteDataView.SelectedRows(0).Cells("ID").Value)
                ClienteBLL.EliminarCliente(id)
                CargarClientes()
            Catch ex As Exception
                MessageBox.Show("Error al eliminar el cliente: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Seleccione un cliente para eliminar, debe clickear en el cuadrado a la izquierda del Id (en la flecha).")
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If ClienteDataView.SelectedRows.Count > 0 Then
            Try
                Dim id As Integer = Convert.ToInt32(ClienteDataView.SelectedRows(0).Cells("ID").Value)
                Dim nombre As String = ClienteDataView.SelectedRows(0).Cells("Cliente").Value.ToString()
                Dim correo As String = ClienteDataView.SelectedRows(0).Cells("Correo").Value.ToString()
                Dim telefono As String = ClienteDataView.SelectedRows(0).Cells("Telefono").Value.ToString()

                Dim editarForm As New FormEditarCliente(id, nombre, correo, telefono)
                editarForm.ShowDialog()
                CargarClientes()
            Catch ex As Exception
                MessageBox.Show("Error al modificar el cliente: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Seleccione un cliente para modificar, debe clickear en el cuadrado a la izquierda del Id (en la flecha).")
        End If
    End Sub
    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub
End Class
