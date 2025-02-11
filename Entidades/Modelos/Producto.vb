Public Class Producto
    Public Property ProductoID As Integer
    Public Property Nombre As String
    Public Property Precio As Decimal
    Public Property Stock As Integer


    Public Sub New(productoID As Integer, nombre As String, precio As Decimal, stock As Integer)
        Me.ProductoID = productoID
        Me.Nombre = nombre
        Me.Precio = precio
        Me.Stock = stock
    End Sub

End Class
