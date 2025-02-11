Imports Entidades

Public Class FormCliente
    ' Lista de clientes
    Private listaClientes As New List(Of Cliente)

    ' BindingSource
    Private clientesBindingSource As New BindingSource()

    ' Evento Load para inicializar datos
    Private Sub FormClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Simulando algunos datos de clientes
        listaClientes.Add(New Cliente(1, "Juan", "Pérez", "juan@example.com", "123456789"))
        listaClientes.Add(New Cliente(2, "Ana", "Gómez", "ana@example.com", "987654321"))
        listaClientes.Add(New Cliente(3, "Carlos", "Martínez", "carlos@example.com", "112233445"))

        ' Establecer la lista de clientes como fuente de datos del BindingSource
        clientesBindingSource.DataSource = listaClientes

        ' Enlazar el BindingSource al DataGridView
        DataGridView1.DataSource = clientesBindingSource

        ' Opcional: Personalizar las columnas del DataGridView
        DataGridView1.Columns("ClienteID").HeaderText = "ID Cliente"
        DataGridView1.Columns("Nombre").HeaderText = "Nombre"
        DataGridView1.Columns("Apellido").HeaderText = "Apellido"
        DataGridView1.Columns("Email").HeaderText = "Correo Electrónico"
        DataGridView1.Columns("Telefono").HeaderText = "Teléfono"

        ' Si quieres ocultar alguna columna
        DataGridView1.Columns("ClienteID").Visible = False

        Dim btnModificar As New DataGridViewButtonColumn()
        btnModificar.Name = "Modificar"
        btnModificar.HeaderText = "Acciones"
        btnModificar.Text = "Modificar"
        btnModificar.UseColumnTextForButtonValue = True

        Dim btnEliminar As New DataGridViewButtonColumn()
        btnEliminar.Name = "Eliminar"
        btnEliminar.HeaderText = "Acciones"
        btnEliminar.Text = "Eliminar"
        btnEliminar.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Add(btnModificar)
        DataGridView1.Columns.Add(btnEliminar)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
