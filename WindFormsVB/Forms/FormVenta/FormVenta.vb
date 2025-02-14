Imports System.IO
Imports BLL
Imports Entidades
Imports OfficeOpenXml

Public Class FormVenta
    Private Sub ActualizarDatos(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarVentas()
    End Sub

    Private Sub CargarVentas()
        Dim resultado = VentaBLL.ObtenerVentas()
        Dim ventas As List(Of Venta) = resultado.Item1
        Dim nombresClientes As List(Of String) = resultado.Item2

        If Not VentaDataView.Columns.Contains("NombreCliente") Then
            VentaDataView.Columns.Add("NombreCliente", "Cliente")
        End If

        VentaDataView.DataSource = ventas

        If VentaDataView.Columns("IDCliente") IsNot Nothing Then
            VentaDataView.Columns.Remove("IDCliente")
        End If

        For i As Integer = 0 To ventas.Count - 1
            VentaDataView.Rows(i).Cells("NombreCliente").Value = nombresClientes(i)
        Next
    End Sub

    Private Sub dgvVentas_SelectionChanged(sender As Object, e As EventArgs) Handles VentaDataView.SelectionChanged
        If VentaDataView.SelectedRows.Count > 0 Then
            Dim ventaID As Integer = Convert.ToInt32(VentaDataView.SelectedRows(0).Cells("ID").Value)
            CargarDetallesVenta(ventaID)
        End If
    End Sub

    Private Sub CargarDetallesVenta(ventaID As Integer)

        Dim resultado = VentaBLL.ObtenerDetallesDeVenta(ventaID)
        Dim detalles As List(Of VentaItem) = resultado.Item1
        Dim nombresProductos As List(Of String) = resultado.Item2

        If VentaItemDataView.Columns("Nombre") Is Nothing Then
            VentaItemDataView.Columns.Add("Nombre", "Producto")
        End If

        VentaItemDataView.DataSource = detalles

        If VentaItemDataView.Columns("IDProducto") IsNot Nothing Then
            VentaItemDataView.Columns.Remove("IDProducto")
        End If

        For i As Integer = 0 To detalles.Count - 1
            VentaItemDataView.Rows(i).Cells("Nombre").Value = nombresProductos(i)
        Next
    End Sub



    Private Sub btnCrearVenta_Click(sender As Object, e As EventArgs) Handles btnCrearVenta.Click
        Dim formNuevaVenta As New FormNuevaVenta()
        AddHandler formNuevaVenta.DatosActualizados, AddressOf ActualizarDatos
        formNuevaVenta.ShowDialog()
    End Sub

    Private Sub GenerarReporteVenta(venta As Venta, nombresCliente As String)
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial

        Using package As New ExcelPackage()

            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets.Add("Reporte Venta")

            worksheet.Cells(1, 1).Value = "ID"
            worksheet.Cells(1, 2).Value = "Cliente"
            worksheet.Cells(1, 3).Value = "Fecha"
            worksheet.Cells(1, 4).Value = "Total"

            worksheet.Cells(2, 1).Value = venta.ID
            worksheet.Cells(2, 2).Value = nombresCliente
            worksheet.Cells(2, 3).Value = venta.Fecha.ToString("dd/MM/yyyy")
            worksheet.Cells(2, 4).Value = venta.Total

            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "Excel Files|*.xlsx"
            saveFileDialog.FileName = "Venta_" & venta.ID & ".xlsx"
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Dim fileInfo As New FileInfo(saveFileDialog.FileName)
                package.SaveAs(fileInfo)
            End If
        End Using
    End Sub

    Private Sub GenerarReporteProductos(ventaID As Integer)

        Dim resultado = VentaBLL.ObtenerDetallesDeVenta(ventaID)
        Dim detalles As List(Of VentaItem) = resultado.Item1
        Dim nombresProductos As List(Of String) = resultado.Item2

        Using package As New ExcelPackage()
            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets.Add("Reporte Productos")

            worksheet.Cells(1, 1).Value = "ID"
            worksheet.Cells(1, 2).Value = "Nombre Producto"
            worksheet.Cells(1, 3).Value = "Precio Unitario"
            worksheet.Cells(1, 4).Value = "Cantidad"
            worksheet.Cells(1, 5).Value = "Precio Total"

            For i As Integer = 0 To detalles.Count - 1
                worksheet.Cells(i + 2, 1).Value = detalles(i).IDProducto
                worksheet.Cells(i + 2, 2).Value = nombresProductos(i)
                worksheet.Cells(i + 2, 3).Value = detalles(i).PrecioUnitario
                worksheet.Cells(i + 2, 4).Value = detalles(i).Cantidad
                worksheet.Cells(i + 2, 5).Value = detalles(i).PrecioTotal
            Next

            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "Excel Files|*.xlsx"
            saveFileDialog.FileName = "Productos_Venta_" & ventaID & ".xlsx"
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Dim fileInfo As New FileInfo(saveFileDialog.FileName)
                package.SaveAs(fileInfo)
            End If
        End Using
    End Sub

    Private Sub btnGenerarReporteVenta_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        If VentaDataView.SelectedRows.Count > 0 Then
            Dim ventaID As Integer = Convert.ToInt32(VentaDataView.SelectedRows(0).Cells("ID").Value)
            Dim Resultado = VentaBLL.ObtenerVentaPorID(ventaID)
            Dim Venta As Venta = Resultado.Item1
            Dim Cliente As String = Resultado.Item2
            GenerarReporteVenta(Venta, Cliente)

            GenerarReporteProductos(ventaID)
        Else
            MessageBox.Show("Por favor, seleccione una venta para generar el reporte.")
        End If
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub
    Private Sub btnModificarVenta_Click(sender As Object, e As EventArgs) Handles btnModificarVenta.Click
        If VentaDataView.SelectedRows.Count > 0 Then
            Dim ventaID As Integer = Convert.ToInt32(VentaDataView.SelectedRows(0).Cells("ID").Value)
            Dim resultado = VentaBLL.ObtenerVentaPorID(ventaID)
            Dim venta As Venta = resultado.Item1
            Dim cliente As String = resultado.Item2

            Dim formModificarVenta As New FormEditarVenta(venta, cliente)
            AddHandler formModificarVenta.DatosActualizados, AddressOf ActualizarDatos 'Subcribirse al evento DatosActualizados
            If formModificarVenta.ShowDialog() = DialogResult.OK Then
                CargarVentas()
            End If
        Else
            MessageBox.Show("Por favor, seleccione una venta para modificar.")
        End If
    End Sub
    Private Sub btnEliminarVenta_Click(sender As Object, e As EventArgs) Handles btnEliminarVenta.Click
        If VentaDataView.SelectedRows.Count > 0 Then
            Dim ventaID As Integer = Convert.ToInt32(VentaDataView.SelectedRows(0).Cells("ID").Value)

            Dim result As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar esta venta?", "Confirmar eliminación", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Dim exito As Boolean = VentaBLL.EliminarVenta(ventaID)

                If exito Then
                    MessageBox.Show("Venta eliminada exitosamente.")
                    CargarVentas()
                    VentaItemDataView.DataSource = Nothing

                Else
                    MessageBox.Show("Hubo un problema al eliminar la venta.")
                End If
            End If
        Else
            MessageBox.Show("Por favor, seleccione una venta para eliminar.")
        End If
    End Sub

End Class
