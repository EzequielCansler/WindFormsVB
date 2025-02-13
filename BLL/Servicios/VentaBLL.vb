Imports DAL
Imports Entidades

Public Class VentaBLL

    Public Shared Function ObtenerVentas() As List(Of Venta)
        Return VentaDAL.ObtenerVentas()
    End Function


    Public Shared Function ObtenerDetallesDeVenta(ventaID As Integer) As List(Of VentaItem)
        Return VentaDAL.ObtenerDetallesDeVenta(ventaID)
    End Function


    Public Shared Sub CrearVenta(venta As Venta, detalles As List(Of VentaItem))
        VentaDAL.CrearVenta(venta, detalles)
    End Sub
End Class
