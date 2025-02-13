Public Class VentaItem
    Public Property ID As Integer
    Public Property IDVenta As Integer
    Public Property IDProducto As Integer
    Public Property PrecioUnitario As Double
    Public Property Cantidad As Double
    Public Property PrecioTotal As Double

    Public Sub New(id As Integer, idVenta As Integer, idProducto As Integer, precioUnitario As Double, cantidad As Double, precioTotal As Double)
        Me.ID = id
        Me.IDVenta = idVenta
        Me.IDProducto = idProducto
        Me.PrecioUnitario = precioUnitario
        Me.Cantidad = cantidad
        Me.PrecioTotal = precioTotal
    End Sub
End Class
