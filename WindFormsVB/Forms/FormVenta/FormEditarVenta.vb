Imports System.Data.SqlClient
Imports BLL
Imports Entidades

Public Class FormEditarVenta
    Dim venta As Venta
    Dim cliente As String
    Dim detalles As List(Of VentaItem)
    Public Event DatosActualizados As EventHandler

    Public Sub New(ventaSeleccionada As Venta, cliente As String)
        InitializeComponent()
        Me.venta = ventaSeleccionada
        Me.cliente = cliente
    End Sub

    Private Sub FormEditarVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarClientes()
        CargarDatosVenta()
        CargarProductosVenta()
        CargarProductosDisponibles()
    End Sub

    Private Sub CargarProductosDisponibles()
        Dim productos As List(Of Producto) = ProductoBLL.ObtenerProductos()
        cbProductos.DataSource = productos
        cbProductos.DisplayMember = "Nombre"
        cbProductos.ValueMember = "ID"
    End Sub

    Private Sub CargarClientes()
        Dim clientes As List(Of Cliente) = ClienteBLL.ObtenerClientes()
        ComboBoxClientes.DataSource = clientes
        ComboBoxClientes.DisplayMember = "Nombre"
        ComboBoxClientes.ValueMember = "ID"
        ComboBoxClientes.SelectedValue = venta.IDCliente
    End Sub

    Private Sub CargarDatosVenta()
        Dim datosVenta As New List(Of Venta) From {venta}
        DataGridViewVenta.DataSource = datosVenta

        If Not DataGridViewVenta.Columns.Contains("Cliente") Then
            DataGridViewVenta.Columns.Add("Cliente", "Cliente")
        End If

        If DataGridViewVenta.Columns.Contains("IDCliente") Then
            DataGridViewVenta.Columns("IDCliente").Visible = False
        End If

        If cliente IsNot Nothing AndAlso DataGridViewVenta.Rows.Count > 0 Then
            DataGridViewVenta.Rows(0).Cells("Cliente").Value = cliente
        End If
    End Sub

    Private Sub CargarProductosVenta()
        Dim resultado = VentaBLL.ObtenerDetallesConNombresDeProductos(venta.ID)
        detalles = resultado.Item1
        Dim nombresProductos As List(Of String) = resultado.Item2

        DataGridViewProductos.Rows.Clear()

        If Not DataGridViewProductos.Columns.Contains("IDProducto") Then
            DataGridViewProductos.Columns.Add("IDProducto", "IDProducto")
        End If
        If Not DataGridViewProductos.Columns.Contains("Cantidad") Then
            DataGridViewProductos.Columns.Add("Cantidad", "Cantidad")
        End If
        If Not DataGridViewProductos.Columns.Contains("PrecioUnitario") Then
            DataGridViewProductos.Columns.Add("PrecioUnitario", "Precio Unitario")
        End If
        If Not DataGridViewProductos.Columns.Contains("PrecioTotal") Then
            DataGridViewProductos.Columns.Add("PrecioTotal", "Precio Total")
        End If
        If Not DataGridViewProductos.Columns.Contains("Nombre") Then
            DataGridViewProductos.Columns.Add("Nombre", "Nombre")
        End If

        For i As Integer = 0 To detalles.Count - 1
            DataGridViewProductos.Rows.Add(detalles(i).IDProducto, detalles(i).Cantidad, detalles(i).PrecioUnitario, detalles(i).PrecioTotal)
            DataGridViewProductos.Rows(i).Cells("Nombre").Value = nombresProductos(i)
        Next

        DataGridViewProductos.Columns("IDProducto").Visible = False
        DataGridViewProductos.Columns("PrecioUnitario").Visible = True
        DataGridViewProductos.Columns("PrecioTotal").Visible = True
    End Sub
    Private Sub btnAgregarProducto_Click(sender As Object, e As EventArgs) Handles btnSumar.Click
        Dim productoId As Integer = Convert.ToInt32(cbProductos.SelectedValue)
        Dim cantidad As Integer = Convert.ToInt32(numCantidad.Value)

        If cantidad > 0 Then
            Dim producto = ProductoBLL.ObtenerProductoPorID(productoId)
            Dim productoExistente As Boolean = False

            For Each row As DataGridViewRow In DataGridViewProductos.Rows
                If row.Cells("IDProducto").Value IsNot Nothing AndAlso row.Cells("IDProducto").Value = productoId Then

                    Dim cantidadExistente As Integer = Convert.ToInt32(row.Cells("Cantidad").Value)
                    row.Cells("Cantidad").Value = cantidadExistente + cantidad
                    row.Cells("PrecioTotal").Value = row.Cells("PrecioUnitario").Value * (cantidadExistente + cantidad)
                    productoExistente = True
                    Exit For
                End If
            Next
            If Not productoExistente Then
                DataGridViewProductos.Rows.Add(productoId, cantidad, producto.Precio, producto.Precio * cantidad, producto.Nombre)
            End If
        Else
            MessageBox.Show("La cantidad debe ser mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub btnEliminarProducto_Click(sender As Object, e As EventArgs) Handles btnRestar.Click
        If cbProductos.SelectedValue IsNot Nothing Then
            Dim productoId As Integer = Convert.ToInt32(cbProductos.SelectedValue)
            Dim cantidad As Integer = Convert.ToInt32(numCantidad.Value)

            If cantidad > 0 Then
                Dim productoExistente As Boolean = False
                For Each row As DataGridViewRow In DataGridViewProductos.Rows
                    If row.Cells("IDProducto").Value IsNot Nothing AndAlso row.Cells("IDProducto").Value = productoId Then
                        Dim cantidadExistente As Integer = Convert.ToInt32(row.Cells("Cantidad").Value)

                        If cantidadExistente >= cantidad Then
                            Dim nuevaCantidad As Integer = cantidadExistente - cantidad
                            row.Cells("Cantidad").Value = nuevaCantidad
                            row.Cells("PrecioTotal").Value = row.Cells("PrecioUnitario").Value * nuevaCantidad
                        End If

                        If Convert.ToInt32(row.Cells("Cantidad").Value) <= 0 Then
                            DataGridViewProductos.Rows.Remove(row)
                        End If

                        productoExistente = True
                        Exit For
                    End If
                Next
                If Not productoExistente Then
                    MessageBox.Show("El producto seleccionado no está en la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("La cantidad debe ser mayor que cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            venta.IDCliente = ComboBoxClientes.SelectedValue

            detalles.Clear()
            For Each row As DataGridViewRow In DataGridViewProductos.Rows
                If row.Cells("IDProducto").Value IsNot Nothing Then
                    detalles.Add(New VentaItem(0, venta.ID, row.Cells("IDProducto").Value, row.Cells("PrecioUnitario").Value, row.Cells("Cantidad").Value, row.Cells("PrecioTotal").Value))
                End If
            Next

            VentaBLL.ModificarVenta(venta, detalles, "modificar")

            MessageBox.Show("Venta modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            RaiseEvent DatosActualizados(Me, EventArgs.Empty)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error al modificar la venta: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            Dim mensaje As String = VentaBLL.RollbackTransaccion()
            MessageBox.Show(mensaje, "Resultado de la operación", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error al intentar realizar el rollback: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
