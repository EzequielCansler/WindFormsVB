Public Class Producto
    Public Property ID As Integer
    Public Property Nombre As String
    Public Property Precio As Decimal
    Public Property Categoria As String


    Public Sub New(ID As Integer, nombre As String, precio As Decimal, categoria As String)
        Me.ID = ID
        Me.Nombre = nombre
        Me.Precio = precio
        Me.Categoria = categoria
    End Sub

End Class
