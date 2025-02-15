Public Class Venta
    Public Property ID As Integer
    Public Property IDCliente As Integer
    Public Property Fecha As DateTime
    Public Property Total As Double

    Public Property Items As List(Of VentaItem)

    Public Sub New()
        Items = New List(Of VentaItem)()
    End Sub

    Public Sub New(id As Integer, idCliente As Integer, fecha As DateTime, total As Double)
        Me.ID = id
        Me.IDCliente = idCliente
        Me.Fecha = fecha
        Me.Total = total

    End Sub
End Class
