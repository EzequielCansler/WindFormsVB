

Imports System.Reflection.Emit

Public Class FormMenu


    Private Sub Clientesbtn_Click(sender As Object, e As EventArgs) Handles Clientesbtn.Click
        Dim formCliente As New FormCliente()
        formCliente.Show()
    End Sub
    Private Sub Productosbtn_Click(sender As Object, e As EventArgs) Handles Productosbtn.Click
        Dim formProducto As New FormProducto()
        formProducto.Show()
    End Sub
    Private Sub Ventasbtn_Click(sender As Object, e As EventArgs) Handles Ventasbtn.Click

    End Sub



End Class
