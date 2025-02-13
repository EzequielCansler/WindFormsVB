Public Class VentaItem
    Public Property ID As Integer
    Public Property IDVenta As Integer
    Public Property IDProducto As Integer
    Public Property PrecioUnitario As Decimal
    Public Property Cantidad As Integer
    Public Property PrecioTotal As Decimal


    Public Sub New()
    End Sub
    'borrar
    Public Sub New(id As Integer, idVenta As Integer, idProducto As Integer, precioUnitario As Decimal, cantidad As Integer, precioTotal As Decimal)
        Me.ID = id
        Me.IDVenta = idVenta
        Me.IDProducto = idProducto
        Me.PrecioUnitario = precioUnitario
        Me.Cantidad = cantidad
        Me.PrecioTotal = precioTotal
    End Sub
End Class
