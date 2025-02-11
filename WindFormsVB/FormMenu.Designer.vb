<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMenu
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
        Me.Clientesbtn = New System.Windows.Forms.Button()
        Me.Productosbtn = New System.Windows.Forms.Button()
        Me.Ventasbtn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Clientesbtn
        '
        Me.Clientesbtn.Location = New System.Drawing.Point(115, 306)
        Me.Clientesbtn.Name = "Clientesbtn"
        Me.Clientesbtn.Size = New System.Drawing.Size(75, 23)
        Me.Clientesbtn.TabIndex = 0
        Me.Clientesbtn.Text = "Clientes"
        Me.Clientesbtn.UseVisualStyleBackColor = True
        '
        'Productosbtn
        '
        Me.Productosbtn.Location = New System.Drawing.Point(330, 306)
        Me.Productosbtn.Name = "Productosbtn"
        Me.Productosbtn.Size = New System.Drawing.Size(75, 23)
        Me.Productosbtn.TabIndex = 1
        Me.Productosbtn.Text = "Productos"
        Me.Productosbtn.UseVisualStyleBackColor = True
        '
        'Ventasbtn
        '
        Me.Ventasbtn.Location = New System.Drawing.Point(531, 306)
        Me.Ventasbtn.Name = "Ventasbtn"
        Me.Ventasbtn.Size = New System.Drawing.Size(75, 23)
        Me.Ventasbtn.TabIndex = 2
        Me.Ventasbtn.Text = "Ventas"
        Me.Ventasbtn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(218, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(295, 37)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Gestion de Ventas"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FormMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(720, 490)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Ventasbtn)
        Me.Controls.Add(Me.Productosbtn)
        Me.Controls.Add(Me.Clientesbtn)
        Me.Name = "FormMenu"
        Me.Text = "Gestion de Empresa"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Clientesbtn As Button
    Friend WithEvents Productosbtn As Button
    Friend WithEvents Ventasbtn As Button
    Friend WithEvents Label1 As Label
End Class
