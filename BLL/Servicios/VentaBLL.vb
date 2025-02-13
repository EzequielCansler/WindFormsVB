Imports DAL
Imports Entidades

Public Class VentaBLL

    Public Shared Function ObtenerVentas() As (List(Of Venta), List(Of String))
        Return VentaDAL.ObtenerVentas()
    End Function
    Public Shared Function ObtenerVentaPorID(ID As Integer) As (Venta, String)
        Return VentaDAL.ObtenerVentaPorID(ID)
    End Function


    Public Shared Function ObtenerDetallesDeVenta(ID As Integer) As (List(Of VentaItem), List(Of String))
        Return VentaDAL.ObtenerDetallesDeVenta(ID)
    End Function


    Public Shared Sub CrearVenta(venta As Venta, detalles As List(Of VentaItem))
        VentaDAL.CrearVenta(venta, detalles)
    End Sub
End Class
