Public Class Venta
    Public Property VentaID As Integer
    Public Property ClienteID As Integer
    Public Property Fecha As DateTime
    Public Property MontoTotal As Decimal
    Public Property ProductoID As Integer

    Public Sub New(ventaID As Integer, clienteID As Integer, fecha As DateTime, montoTotal As Decimal, productoID As Integer)
        Me.VentaID = ventaID
        Me.ClienteID = clienteID
        Me.Fecha = fecha
        Me.MontoTotal = montoTotal
        Me.ProductoID = productoID
    End Sub

End Class
