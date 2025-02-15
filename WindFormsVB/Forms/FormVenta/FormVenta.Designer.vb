<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVenta
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
        Me.btnEliminarVenta = New System.Windows.Forms.Button()
        Me.btnModificarVenta = New System.Windows.Forms.Button()
        Me.btnVolver = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.VentaDataView = New System.Windows.Forms.DataGridView()
        Me.btnCrearVenta = New System.Windows.Forms.Button()
        Me.btnReporte = New System.Windows.Forms.Button()
        Me.VentaItemDataView = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkOrdenMasCercano = New System.Windows.Forms.CheckBox()
        Me.chkOrdenMasViejo = New System.Windows.Forms.CheckBox()
        CType(Me.VentaDataView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VentaItemDataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnEliminarVenta
        '
        Me.btnEliminarVenta.Location = New System.Drawing.Point(241, 402)
        Me.btnEliminarVenta.Name = "btnEliminarVenta"
        Me.btnEliminarVenta.Size = New System.Drawing.Size(87, 23)
        Me.btnEliminarVenta.TabIndex = 16
        Me.btnEliminarVenta.Text = "Eliminar Venta"
        Me.btnEliminarVenta.UseVisualStyleBackColor = True
        '
        'btnModificarVenta
        '
        Me.btnModificarVenta.Location = New System.Drawing.Point(129, 402)
        Me.btnModificarVenta.Name = "btnModificarVenta"
        Me.btnModificarVenta.Size = New System.Drawing.Size(92, 23)
        Me.btnModificarVenta.TabIndex = 15
        Me.btnModificarVenta.Text = "Modificar Venta"
        Me.btnModificarVenta.UseVisualStyleBackColor = True
        '
        'btnVolver
        '
        Me.btnVolver.Location = New System.Drawing.Point(798, 402)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(75, 23)
        Me.btnVolver.TabIndex = 14
        Me.btnVolver.Text = "Volver"
        Me.btnVolver.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(295, 37)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Gestion de Ventas"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'VentaDataView
        '
        Me.VentaDataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.VentaDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.VentaDataView.Location = New System.Drawing.Point(30, 97)
        Me.VentaDataView.Name = "VentaDataView"
        Me.VentaDataView.Size = New System.Drawing.Size(326, 268)
        Me.VentaDataView.TabIndex = 11
        '
        'btnCrearVenta
        '
        Me.btnCrearVenta.Location = New System.Drawing.Point(30, 402)
        Me.btnCrearVenta.Name = "btnCrearVenta"
        Me.btnCrearVenta.Size = New System.Drawing.Size(75, 23)
        Me.btnCrearVenta.TabIndex = 10
        Me.btnCrearVenta.Text = "Crear Venta"
        Me.btnCrearVenta.UseVisualStyleBackColor = True
        '
        'btnReporte
        '
        Me.btnReporte.Location = New System.Drawing.Point(651, 402)
        Me.btnReporte.Name = "btnReporte"
        Me.btnReporte.Size = New System.Drawing.Size(115, 23)
        Me.btnReporte.TabIndex = 17
        Me.btnReporte.Text = "Generar Reporte"
        Me.btnReporte.UseVisualStyleBackColor = True
        '
        'VentaItemDataView
        '
        Me.VentaItemDataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.VentaItemDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.VentaItemDataView.Location = New System.Drawing.Point(386, 97)
        Me.VentaItemDataView.Name = "VentaItemDataView"
        Me.VentaItemDataView.Size = New System.Drawing.Size(487, 268)
        Me.VentaItemDataView.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 368)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Ordenar"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(383, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(210, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Lista de productos de la venta selecionada"
        '
        'chkOrdenMasCercano
        '
        Me.chkOrdenMasCercano.AutoSize = True
        Me.chkOrdenMasCercano.Location = New System.Drawing.Point(78, 367)
        Me.chkOrdenMasCercano.Name = "chkOrdenMasCercano"
        Me.chkOrdenMasCercano.Size = New System.Drawing.Size(63, 17)
        Me.chkOrdenMasCercano.TabIndex = 21
        Me.chkOrdenMasCercano.Text = "Nuevos"
        Me.chkOrdenMasCercano.UseVisualStyleBackColor = True
        '
        'chkOrdenMasViejo
        '
        Me.chkOrdenMasViejo.AutoSize = True
        Me.chkOrdenMasViejo.Location = New System.Drawing.Point(147, 367)
        Me.chkOrdenMasViejo.Name = "chkOrdenMasViejo"
        Me.chkOrdenMasViejo.Size = New System.Drawing.Size(67, 17)
        Me.chkOrdenMasViejo.TabIndex = 22
        Me.chkOrdenMasViejo.Text = "Antiguos"
        Me.chkOrdenMasViejo.UseVisualStyleBackColor = True
        '
        'FormVenta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(907, 520)
        Me.Controls.Add(Me.chkOrdenMasViejo)
        Me.Controls.Add(Me.chkOrdenMasCercano)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.VentaItemDataView)
        Me.Controls.Add(Me.btnReporte)
        Me.Controls.Add(Me.btnEliminarVenta)
        Me.Controls.Add(Me.btnModificarVenta)
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.VentaDataView)
        Me.Controls.Add(Me.btnCrearVenta)
        Me.Name = "FormVenta"
        Me.Text = "FormVenta"
        CType(Me.VentaDataView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VentaItemDataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnEliminarVenta As Button
    Friend WithEvents btnModificarVenta As Button
    Friend WithEvents btnVolver As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents VentaDataView As DataGridView
    Friend WithEvents btnCrearVenta As Button
    Friend WithEvents btnReporte As Button
    Friend WithEvents VentaItemDataView As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents chkOrdenMasCercano As CheckBox
    Friend WithEvents chkOrdenMasViejo As CheckBox
End Class
