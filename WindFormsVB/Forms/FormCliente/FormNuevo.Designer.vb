<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNuevo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.txtApellido = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnCrear = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(230, 37)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Nuevo Cliente"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(79, 124)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(100, 20)
        Me.txtNombre.TabIndex = 8
        '
        'txtApellido
        '
        Me.txtApellido.Location = New System.Drawing.Point(79, 183)
        Me.txtApellido.Name = "txtApellido"
        Me.txtApellido.Size = New System.Drawing.Size(100, 20)
        Me.txtApellido.TabIndex = 9
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(79, 245)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(100, 20)
        Me.txtEmail.TabIndex = 10
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(79, 303)
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(100, 20)
        Me.txtTelefono.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(76, 287)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Telefono"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(76, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Nombre"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(76, 167)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Apellido"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(76, 229)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Email"
        '
        'btnCrear
        '
        Me.btnCrear.Location = New System.Drawing.Point(90, 353)
        Me.btnCrear.Name = "btnCrear"
        Me.btnCrear.Size = New System.Drawing.Size(75, 23)
        Me.btnCrear.TabIndex = 17
        Me.btnCrear.Text = "Crear"
        Me.btnCrear.UseVisualStyleBackColor = True
        '
        'FormNuevo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(254, 399)
        Me.Controls.Add(Me.btnCrear)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTelefono)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtApellido)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormNuevo"
        Me.Text = "Nuevo Cliente"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents txtApellido As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btnCrear As Button
End Class
