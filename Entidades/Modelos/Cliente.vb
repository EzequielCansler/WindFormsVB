Public Class Cliente
    Public Property ID As Integer
    Public Property Cliente As String
    Public Property Correo As String
    Public Property Telefono As String

    Public Sub New()
    End Sub


    Public Sub New(clienteID As Integer, cliente As String, correo As String, telefono As String)

        Me.ID = clienteID
        Me.Cliente = cliente
        Me.Correo = correo
        Me.Telefono = telefono
    End Sub
End Class
