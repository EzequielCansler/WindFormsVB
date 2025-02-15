Imports DAL
Imports Entidades

Public Class ProductoBLL
    Public Shared Sub AgregarOEditarProducto(ID As Integer, nombre As String, precio As Decimal, categoria As String)
        Try
            If precio <= 0 Then
                Throw New Exception("El precio debe ser mayor a 0")
            End If
            If ID = 0 Then
                ProductoDAL.AgregarProducto(nombre, precio, categoria)
            Else
                ProductoDAL.EditarProducto(ID, nombre, precio, categoria)
            End If
        Catch ex As Exception
            Throw New Exception("Error al agregar o editar el producto: " & ex.Message)
        End Try
    End Sub

    Public Shared Function ObtenerProductos() As List(Of Producto)
        Return ProductoDAL.ObtenerProductos()
    End Function
    Public Shared Function ObtenerProductosPorNombre(nombre As String) As List(Of Producto)
        Return ProductoDAL.BuscarProductosPorNombre(nombre)
    End Function
    Public Shared Function ObtenerProductoPorID(ID As Integer) As Producto
        Return ProductoDAL.ObtenerProductoPorID(ID)
    End Function

    Public Shared Sub EliminarProducto(ID As Integer)
        ProductoDAL.EliminarProducto(ID)
    End Sub
End Class
