﻿Public Class Cliente
    Public Property ClienteID As Integer
    Public Property Nombre As String
    Public Property Apellido As String
    Public Property Email As String
    Public Property Telefono As String

    Public Sub New()
    End Sub


    Public Sub New(nombre As String, apellido As String, email As String, telefono As String)
        Me.Nombre = nombre
        Me.Apellido = apellido
        Me.Email = email
        Me.Telefono = telefono
    End Sub
End Class
