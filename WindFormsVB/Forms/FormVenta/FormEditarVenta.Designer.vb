<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormEditarVenta
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.DataGridViewVenta = New System.Windows.Forms.DataGridView()
        Me.DataGridViewProductos = New System.Windows.Forms.DataGridView()
        Me.ComboBoxClientes = New System.Windows.Forms.ComboBox()
        Me.Clientes = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnRestar = New System.Windows.Forms.Button()
        Me.btnSumar = New System.Windows.Forms.Button()
        Me.numCantidad = New System.Windows.Forms.NumericUpDown()
        Me.cbProductos = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.DataGridViewVenta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(941, 559)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 0
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'DataGridViewVenta
        '
        Me.DataGridViewVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewVenta.Location = New System.Drawing.Point(26, 222)
        Me.DataGridViewVenta.Name = "DataGridViewVenta"
        Me.DataGridViewVenta.Size = New System.Drawing.Size(468, 240)
        Me.DataGridViewVenta.TabIndex = 1
        '
        'DataGridViewProductos
        '
        Me.DataGridViewProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewProductos.Location = New System.Drawing.Point(519, 222)
        Me.DataGridViewProductos.Name = "DataGridViewProductos"
        Me.DataGridViewProductos.Size = New System.Drawing.Size(517, 240)
        Me.DataGridViewProductos.TabIndex = 2
        '
        'ComboBoxClientes
        '
        Me.ComboBoxClientes.FormattingEnabled = True
        Me.ComboBoxClientes.Location = New System.Drawing.Point(26, 180)
        Me.ComboBoxClientes.Name = "ComboBoxClientes"
        Me.ComboBoxClientes.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxClientes.TabIndex = 3
        '
        'Clientes
        '
        Me.Clientes.AutoSize = True
        Me.Clientes.Location = New System.Drawing.Point(23, 164)
        Me.Clientes.Name = "Clientes"
        Me.Clientes.Size = New System.Drawing.Size(39, 13)
        Me.Clientes.TabIndex = 4
        Me.Clientes.Text = "Cliente"
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(37, 559)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.btnGuardar.TabIndex = 5
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnRestar
        '
        Me.btnRestar.Location = New System.Drawing.Point(883, 178)
        Me.btnRestar.Name = "btnRestar"
        Me.btnRestar.Size = New System.Drawing.Size(75, 23)
        Me.btnRestar.TabIndex = 6
        Me.btnRestar.Text = "Restar"
        Me.btnRestar.UseVisualStyleBackColor = True
        '
        'btnSumar
        '
        Me.btnSumar.Location = New System.Drawing.Point(802, 178)
        Me.btnSumar.Name = "btnSumar"
        Me.btnSumar.Size = New System.Drawing.Size(75, 23)
        Me.btnSumar.TabIndex = 7
        Me.btnSumar.Text = "Sumar"
        Me.btnSumar.UseVisualStyleBackColor = True
        '
        'numCantidad
        '
        Me.numCantidad.Location = New System.Drawing.Point(676, 181)
        Me.numCantidad.Name = "numCantidad"
        Me.numCantidad.Size = New System.Drawing.Size(120, 20)
        Me.numCantidad.TabIndex = 8
        '
        'cbProductos
        '
        Me.cbProductos.FormattingEnabled = True
        Me.cbProductos.Location = New System.Drawing.Point(519, 181)
        Me.cbProductos.Name = "cbProductos"
        Me.cbProductos.Size = New System.Drawing.Size(121, 21)
        Me.cbProductos.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(516, 165)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Agregar otro Producto"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(30, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(256, 37)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Modificar Venta"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FormEditarVenta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1066, 613)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbProductos)
        Me.Controls.Add(Me.numCantidad)
        Me.Controls.Add(Me.btnSumar)
        Me.Controls.Add(Me.btnRestar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.Clientes)
        Me.Controls.Add(Me.ComboBoxClientes)
        Me.Controls.Add(Me.DataGridViewProductos)
        Me.Controls.Add(Me.DataGridViewVenta)
        Me.Controls.Add(Me.btnCancelar)
        Me.Name = "FormEditarVenta"
        Me.Text = "FormEditarVenta"
        CType(Me.DataGridViewVenta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewProductos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCancelar As Button
    Friend WithEvents DataGridViewVenta As DataGridView
    Friend WithEvents DataGridViewProductos As DataGridView
    Friend WithEvents ComboBoxClientes As ComboBox
    Friend WithEvents Clientes As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnRestar As Button
    Friend WithEvents btnSumar As Button
    Friend WithEvents numCantidad As NumericUpDown
    Friend WithEvents cbProductos As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
