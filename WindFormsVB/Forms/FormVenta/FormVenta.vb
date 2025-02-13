Imports BLL
Imports Entidades

Public Class FormVenta
    Private Sub FormVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarVentas()
    End Sub

    Private Sub CargarVentas()

        Dim ventas As List(Of Venta) = VentaBLL.ObtenerVentas()
        VentaDataView.DataSource = ventas
    End Sub

    Private Sub dgvVentas_SelectionChanged(sender As Object, e As EventArgs) Handles VentaDataView.SelectionChanged
        If VentaDataView.SelectedRows.Count > 0 Then
            Dim ventaID As Integer = Convert.ToInt32(VentaDataView.SelectedRows(0).Cells("ID").Value)
            CargarDetallesVenta(ventaID)
        End If
    End Sub

    Private Sub CargarDetallesVenta(ventaID As Integer)

        Dim detalles As List(Of VentaItem) = VentaBLL.ObtenerDetallesDeVenta(ventaID)
        VentaItemDataView.DataSource = detalles
    End Sub

    Private Sub btnCrearVenta_Click(sender As Object, e As EventArgs) Handles btnCrearVenta.Click
        Dim formNuevaVenta As New FormNuevaVenta()
        formNuevaVenta.ShowDialog()
        CargarVentas()
    End Sub

    Private Sub btnVerReporte_Click(sender As Object, e As EventArgs) Handles btnVerReporte.Click
        Dim formReporte As New FormReporte()
        formReporte.ShowDialog()
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub
End Class
