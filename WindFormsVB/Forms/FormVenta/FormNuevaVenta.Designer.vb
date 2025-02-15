<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNuevaVenta
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
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.ProductoDataView = New System.Windows.Forms.DataGridView()
        Me.numCantidad = New System.Windows.Forms.NumericUpDown()
        Me.btnCrear = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnVolver = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.ProductoDataView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbCliente
        '
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.Location = New System.Drawing.Point(58, 82)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(121, 21)
        Me.cmbCliente.TabIndex = 0
        Me.cmbCliente.Text = "Selecionar Cliente"
        '
        'ProductoDataView
        '
        Me.ProductoDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ProductoDataView.Location = New System.Drawing.Point(58, 114)
        Me.ProductoDataView.Name = "ProductoDataView"
        Me.ProductoDataView.Size = New System.Drawing.Size(673, 228)
        Me.ProductoDataView.TabIndex = 1
        '
        'numCantidad
        '
        Me.numCantidad.Location = New System.Drawing.Point(154, 348)
        Me.numCantidad.Name = "numCantidad"
        Me.numCantidad.Size = New System.Drawing.Size(120, 20)
        Me.numCantidad.TabIndex = 2
        '
        'btnCrear
        '
        Me.btnCrear.AccessibleRole = System.Windows.Forms.AccessibleRole.SpinButton
        Me.btnCrear.Location = New System.Drawing.Point(58, 488)
        Me.btnCrear.Name = "btnCrear"
        Me.btnCrear.Size = New System.Drawing.Size(75, 23)
        Me.btnCrear.TabIndex = 3
        Me.btnCrear.Text = "Crear"
        Me.btnCrear.UseVisualStyleBackColor = True
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(58, 348)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(75, 23)
        Me.btnAgregar.TabIndex = 4
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'btnVolver
        '
        Me.btnVolver.Location = New System.Drawing.Point(656, 474)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(75, 23)
        Me.btnVolver.TabIndex = 5
        Me.btnVolver.Text = "Volver"
        Me.btnVolver.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(55, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Clientes"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(51, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(196, 37)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Crear Venta"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FormNuevaVenta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(767, 563)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnCrear)
        Me.Controls.Add(Me.numCantidad)
        Me.Controls.Add(Me.ProductoDataView)
        Me.Controls.Add(Me.cmbCliente)
        Me.Name = "FormNuevaVenta"
        Me.Text = "FormNuevaVenta"
        CType(Me.ProductoDataView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbCliente As ComboBox
    Friend WithEvents ProductoDataView As DataGridView
    Friend WithEvents numCantidad As NumericUpDown
    Friend WithEvents btnCrear As Button
    Friend WithEvents btnAgregar As Button
    Friend WithEvents btnVolver As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
End Class
