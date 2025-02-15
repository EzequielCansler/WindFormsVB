Imports BLL
Imports Entidades

Public Class FormNuevaVenta
    Private venta As New Venta()

    Private Sub FormNuevaVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cargar los clientes
        Dim clientes As List(Of Cliente) = ClienteBLL.ObtenerClientes()
        cmbCliente.DataSource = clientes
        cmbCliente.DisplayMember = "Cliente"
        cmbCliente.ValueMember = "ID"

        ' Cargar los productos
        Dim productos As List(Of Producto) = ProductoBLL.ObtenerProductos()
        ProductoDataView.DataSource = productos
    End Sub

    Private Sub btnAgregarProducto_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If ProductoDataView.SelectedRows.Count > 0 Then
            Dim idProducto As Integer = Convert.ToInt32(ProductoDataView.SelectedRows(0).Cells("ID").Value)
            Dim producto As Producto = ProductoBLL.ObtenerProductoPorID(idProducto)
            Dim cantidad As Integer = Convert.ToInt32(numCantidad.Value)

            If cantidad > 0 Then
                Dim precioTotal As Decimal = producto.Precio * cantidad

                Dim ventaItem As New VentaItem(0, 0, idProducto, producto.Precio, cantidad, precioTotal)
                venta.Items.Add(ventaItem)

                MessageBox.Show($"Producto agregado: {producto.Nombre} - Cantidad: {cantidad}", "Producto agregado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Por favor, ingrese una cantidad mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Por favor, seleccione un producto para agregar. Debe clickear a la izquierda del ID", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


    Private Sub btnGuardarVenta_Click(sender As Object, e As EventArgs) Handles btnCrear.Click

        venta.IDCliente = Convert.ToInt32(cmbCliente.SelectedValue)
        venta.Fecha = DateTime.Now
        venta.Total = venta.Items.Sum(Function(item) item.PrecioTotal)

        VentaBLL.CrearVenta(venta, venta.Items)
        MessageBox.Show("Venta guardada exitosamente")
        RaiseEvent DatosActualizados(Me, EventArgs.Empty)

        Me.Close()
    End Sub

    Public Event DatosActualizados As EventHandler

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        RaiseEvent DatosActualizados(Me, EventArgs.Empty)
        Me.Close()
    End Sub
End Class
