Imports System.Configuration
Imports System.Data.SqlClient

Public Class ConexionBD
    Public Shared Function ObtenerConexion() As SqlConnection
        Dim cadenaConexion As String = ConfigurationManager.ConnectionStrings("MiConexionDB").ConnectionString
        Return New SqlConnection(cadenaConexion)
    End Function

    Public Function ProbarConexion() As Boolean
        Try
            Using conn As SqlConnection = ObtenerConexion()
                conn.Open()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
