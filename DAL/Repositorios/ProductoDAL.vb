﻿Imports Entidades
Imports System.Data.SqlClient

Public Class ProductoDAL

    Public Shared Function ObtenerProductos() As List(Of Producto)
        Dim productos As New List(Of Producto)
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Dim query As String = "SELECT ID, Nombre, Precio, Categoria FROM Productos"
                Using cmd As New SqlCommand(query, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            productos.Add(New Producto(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetString(3)))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener los productos: " & ex.Message)
        End Try
        Return productos
    End Function
    Public Shared Function BuscarProductosPorNombre(nombre As String) As List(Of Producto)
        Dim productos As New List(Of Producto)
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()
            Dim query As String = "SELECT * FROM productos WHERE Nombre LIKE @nombre"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@nombre", "%" & nombre & "%")

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim producto As New Producto()
                        producto.ID = reader.GetInt32(reader.GetOrdinal("ID"))
                        producto.Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                        producto.Precio = reader.GetDouble(reader.GetOrdinal("Precio"))
                        producto.Categoria = reader.GetString(reader.GetOrdinal("Categoria"))
                        productos.Add(producto)
                    End While
                End Using
            End Using
        End Using
        Return productos
    End Function
    Public Shared Function ObtenerProductoPorID(ID As Integer) As Producto
        Dim producto As Producto = Nothing
        Using conn As SqlConnection = ConexionBD.ObtenerConexion()
            conn.Open()
            Dim query As String = "SELECT ID, Nombre, Precio, Categoria FROM productos WHERE ID = @ID"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ID", ID)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                producto = New Producto(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetString(3))
            End If
        End Using
        Return producto
    End Function

    Public Shared Sub AgregarProducto(nombre As String, precio As Decimal, categoria As String)
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Dim query As String = "INSERT INTO Productos (Nombre, Precio, Categoria) VALUES (@Nombre, @Precio, @Categoria)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Nombre", nombre)
                    cmd.Parameters.AddWithValue("@Precio", precio)
                    cmd.Parameters.AddWithValue("@Categoria", categoria)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al agregar el producto: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub EditarProducto(ID As Integer, nombre As String, precio As Decimal, categoria As String)
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Dim query As String = "UPDATE Productos SET Nombre = @Nombre, Precio = @Precio, Categoria = @Categoria WHERE ID = @ID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.Parameters.AddWithValue("@Nombre", nombre)
                    cmd.Parameters.AddWithValue("@Precio", precio)
                    cmd.Parameters.AddWithValue("@Categoria", categoria)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al editar el producto: " & ex.Message)
        End Try
    End Sub


    Public Shared Sub EliminarProducto(ID As Integer)
        Try
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                conn.Open()
                Dim query As String = "DELETE FROM Productos WHERE ID = @ID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar el Producto: " & ex.Message)
        End Try
    End Sub
End Class
