Imports BLL
Imports Entidades

Public Class FormEditarVenta
    Dim venta As Venta
    Dim cliente As String
    Dim detalles As List(Of VentaItem)

    Public Sub New(venta As Venta, cliente As String)
        InitializeComponent()
        Me.venta = venta
        Me.cliente = cliente
    End Sub

    Private Sub FormModificarVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarClientes()
        CargarDatosVenta()
        CargarProductosVenta()
        CargarProductosDisponibles()
    End Sub

    Private Sub CargarClientes()
        Dim clientes As List(Of Cliente) = ClienteBLL.ObtenerClientes()
        ComboBoxClientes.DataSource = clientes
        ComboBoxClientes.DisplayMember = "Cliente"
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

        If cliente IsNot Nothing Then
            If DataGridViewVenta.Rows.Count > 0 Then
                DataGridViewVenta.Rows(0).Cells("Cliente").Value = cliente
            End If
        End If
    End Sub

    Private Sub CargarProductosDisponibles()
        Dim productos As List(Of Producto) = ProductoBLL.ObtenerProductos()
        cbProductos.DataSource = productos
        cbProductos.DisplayMember = "Nombre"
        cbProductos.ValueMember = "ID"
    End Sub


    Private Sub CargarProductosVenta()
        Dim resultado = VentaBLL.ObtenerDetallesConNombresDeProductos(venta.ID)
        detalles = resultado.Item1
        Dim nombresProductos As List(Of String) = resultado.Item2

        ' Limpiar las filas anteriores
        DataGridViewProductos.Rows.Clear()

        ' Asegúrate de que las columnas estén presentes
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

        ' Cargar los productos y los nombres
        For i As Integer = 0 To detalles.Count - 1
            ' Añadir la fila con los detalles de la venta
            DataGridViewProductos.Rows.Add(detalles(i).IDProducto, detalles(i).Cantidad, detalles(i).PrecioUnitario, detalles(i).PrecioTotal)
            ' Establecer el valor del nombre del producto en la nueva columna "Nombre"
            DataGridViewProductos.Rows(i).Cells("Nombre").Value = nombresProductos(i)
        Next

        ' Ocultar columnas innecesarias
        DataGridViewProductos.Columns("IDProducto").Visible = False
        DataGridViewProductos.Columns("PrecioUnitario").Visible = True
        DataGridViewProductos.Columns("PrecioTotal").Visible = True

    End Sub

    Private Sub cbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProductos.SelectedIndexChanged
        ' Si se selecciona un producto, actualizamos la cantidad inicial
        If cbProductos.SelectedIndex >= 0 Then
            Dim producto As Producto = CType(cbProductos.SelectedItem, Producto)
            ' Configura el NumericUpDown a la cantidad correspondiente
            numCantidad.Value = 1 ' O la cantidad actual si ya tiene uno
        End If
    End Sub

    Private Sub NumericUpDownCantidad_ValueChanged(sender As Object, e As EventArgs) Handles numCantidad.ValueChanged
        ' Aquí se puede validar y actualizar la cantidad según lo que se haya seleccionado
    End Sub
    ' Formulario de la UI
    Private Sub btnSumar_Click(sender As Object, e As EventArgs) Handles btnSumar.Click
        If cbProductos.SelectedIndex >= 0 Then
            Dim idProducto As Integer = Convert.ToInt32(cbProductos.SelectedValue)
            Dim cantidad As Integer = Convert.ToInt32(numCantidad.Value)
            Dim idVenta As Integer = venta.ID ' Asumiendo que tienes el ID de la venta

            ' Verificamos si el producto ya está en la venta usando la capa BLL
            If VentaBLL.ProductoEnVenta(idVenta, idProducto) Then
                ' Si el producto ya está en la venta, agregamos la cantidad al item existente
                VentaBLL.ActualizarCantidadProducto(idVenta, idProducto, cantidad)
                MessageBox.Show("Cantidad actualizada en la venta.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' Si el producto no está en la venta, lo agregamos con la cantidad seleccionada
                Dim producto As Producto = CType(cbProductos.SelectedItem, Producto)
                Dim precioUnitario As Double = producto.Precio ' Suponiendo que tienes el precio del producto
                Dim precioTotal As Double = precioUnitario * cantidad

                ' Agregar el nuevo producto a la venta
                VentaBLL.AgregarProductoAVenta(idVenta, idProducto, precioUnitario, cantidad, precioTotal)
                MessageBox.Show("Producto agregado a la venta.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ' Actualizamos la vista de productos
            CargarProductosVenta()
        Else
            MessageBox.Show("Por favor, seleccione un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub btnRestar_Click(sender As Object, e As EventArgs) Handles btnRestar.Click
        If cbProductos.SelectedIndex >= 0 Then
            Dim idProducto As Integer = Convert.ToInt32(cbProductos.SelectedValue)
            Dim cantidad As Integer = Convert.ToInt32(numCantidad.Value)
            Dim idVenta As Integer = venta.ID ' Asumiendo que tienes el ID de la venta

            ' Verificamos si el producto está en la venta
            If VentaBLL.ProductoEnVenta(idVenta, idProducto) Then
                ' Si el producto está en la venta, verificamos si la cantidad a restar es mayor a 0
                If cantidad > 0 Then
                    ' Llamamos a la función BLL para restar la cantidad del producto
                    VentaBLL.RestarCantidadProductoEnVenta(idVenta, idProducto, cantidad)

                    ' Verificamos si el producto sigue en la venta después de restar
                    If VentaBLL.ProductoEnVenta(idVenta, idProducto) Then
                        ' Si el producto sigue en la venta, simplemente mostramos un mensaje
                        MessageBox.Show("Cantidad reducida en la venta.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        ' Si el producto se eliminó completamente, mostramos otro mensaje
                        MessageBox.Show("Producto eliminado de la venta.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show("La cantidad a restar debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Este producto no está en la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            ' Actualizamos la vista de productos
            CargarProductosVenta()
        Else
            MessageBox.Show("Por favor, seleccione un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub



    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        venta.IDCliente = ComboBoxClientes.SelectedValue

        Dim detalles As New List(Of VentaItem)
        For Each row As DataGridViewRow In DataGridViewProductos.Rows
            If Not row.IsNewRow Then
                Dim precioUnitario As Double = row.Cells("PrecioUnitario").Value
                Dim cantidad As Double = row.Cells("Cantidad").Value
                Dim precioTotal As Double = precioUnitario * cantidad

                Dim ventaItem As New VentaItem(0, venta.ID, row.Cells("IDProducto").Value, precioUnitario, cantidad, precioTotal)
                detalles.Add(ventaItem)
            End If
        Next
        VentaBLL.ModificarVenta(venta, detalles)

        Me.DialogResult = DialogResult.OK
        RaiseEvent DatosActualizados(Me, EventArgs.Empty)
        Me.Close()
    End Sub

    Public Event DatosActualizados As EventHandler

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        RaiseEvent DatosActualizados(Me, EventArgs.Empty)
        Me.Close()
    End Sub

End Class
