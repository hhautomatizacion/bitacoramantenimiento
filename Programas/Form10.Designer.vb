<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form10
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form10))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Maquina = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Adjuntos = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PorMaquinaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PorReferenciaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PorResponsableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PorAutorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PorContenidoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PorFechaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PorAdjuntosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConAdjuntosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SinAdjuntosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Fecha, Me.Maquina, Me.Descripcion, Me.Adjuntos})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 24)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(648, 216)
        Me.DataGridView1.TabIndex = 0
        Me.DataGridView1.Visible = False
        '
        'Id
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Id.DefaultCellStyle = DataGridViewCellStyle1
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        '
        'Fecha
        '
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.Name = "Fecha"
        Me.Fecha.ReadOnly = True
        '
        'Maquina
        '
        Me.Maquina.HeaderText = "Maquina"
        Me.Maquina.Name = "Maquina"
        Me.Maquina.ReadOnly = True
        '
        'Descripcion
        '
        Me.Descripcion.HeaderText = "Descripcion"
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.ReadOnly = True
        '
        'Adjuntos
        '
        Me.Adjuntos.HeaderText = "Adjuntos"
        Me.Adjuntos.Name = "Adjuntos"
        Me.Adjuntos.ReadOnly = True
        Me.Adjuntos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Adjuntos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.BuscarToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(648, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirToolStripMenuItem, Me.SalirToolStripMenuItem})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.ArchivoToolStripMenuItem.Text = "&Archivo"
        '
        'AbrirToolStripMenuItem
        '
        Me.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem"
        Me.AbrirToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AbrirToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.AbrirToolStripMenuItem.Text = "&Abrir"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.SalirToolStripMenuItem.Text = "Sa&lir"
        '
        'BuscarToolStripMenuItem
        '
        Me.BuscarToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PorMaquinaToolStripMenuItem, Me.PorReferenciaToolStripMenuItem, Me.PorResponsableToolStripMenuItem, Me.PorAutorToolStripMenuItem, Me.PorContenidoToolStripMenuItem, Me.PorFechaToolStripMenuItem, Me.PorAdjuntosToolStripMenuItem})
        Me.BuscarToolStripMenuItem.Name = "BuscarToolStripMenuItem"
        Me.BuscarToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.BuscarToolStripMenuItem.Text = "&Buscar"
        '
        'PorMaquinaToolStripMenuItem
        '
        Me.PorMaquinaToolStripMenuItem.Name = "PorMaquinaToolStripMenuItem"
        Me.PorMaquinaToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PorMaquinaToolStripMenuItem.Text = "Por &maquina"
        '
        'PorReferenciaToolStripMenuItem
        '
        Me.PorReferenciaToolStripMenuItem.Name = "PorReferenciaToolStripMenuItem"
        Me.PorReferenciaToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PorReferenciaToolStripMenuItem.Text = "Por r&eferencia"
        '
        'PorResponsableToolStripMenuItem
        '
        Me.PorResponsableToolStripMenuItem.Name = "PorResponsableToolStripMenuItem"
        Me.PorResponsableToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PorResponsableToolStripMenuItem.Text = "Por re&sponsable"
        '
        'PorAutorToolStripMenuItem
        '
        Me.PorAutorToolStripMenuItem.Name = "PorAutorToolStripMenuItem"
        Me.PorAutorToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PorAutorToolStripMenuItem.Text = "Por &autor"
        '
        'PorContenidoToolStripMenuItem
        '
        Me.PorContenidoToolStripMenuItem.Name = "PorContenidoToolStripMenuItem"
        Me.PorContenidoToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PorContenidoToolStripMenuItem.Text = "Por contenido"
        '
        'PorFechaToolStripMenuItem
        '
        Me.PorFechaToolStripMenuItem.Name = "PorFechaToolStripMenuItem"
        Me.PorFechaToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PorFechaToolStripMenuItem.Text = "Por fecha"
        '
        'PorAdjuntosToolStripMenuItem
        '
        Me.PorAdjuntosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConAdjuntosToolStripMenuItem, Me.SinAdjuntosToolStripMenuItem})
        Me.PorAdjuntosToolStripMenuItem.Name = "PorAdjuntosToolStripMenuItem"
        Me.PorAdjuntosToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PorAdjuntosToolStripMenuItem.Text = "Por adjuntos"
        '
        'ConAdjuntosToolStripMenuItem
        '
        Me.ConAdjuntosToolStripMenuItem.Name = "ConAdjuntosToolStripMenuItem"
        Me.ConAdjuntosToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.ConAdjuntosToolStripMenuItem.Text = "Con adjuntos"
        '
        'SinAdjuntosToolStripMenuItem
        '
        Me.SinAdjuntosToolStripMenuItem.Name = "SinAdjuntosToolStripMenuItem"
        Me.SinAdjuntosToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.SinAdjuntosToolStripMenuItem.Text = "Sin adjuntos"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2, Me.ToolStripProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 240)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(648, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Image = CType(resources.GetObject("ToolStripStatusLabel1.Image"), System.Drawing.Image)
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(148, 17)
        Me.ToolStripStatusLabel1.Text = "Entradas encontradas: 0"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(383, 17)
        Me.ToolStripStatusLabel2.Spring = True
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(0, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(648, 216)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Espere..."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form10
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 262)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form10"
        Me.Text = "Busqueda"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorMaquinaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorContenidoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorFechaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorAutorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorResponsableToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorReferenciaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AbrirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Maquina As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Adjuntos As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PorAdjuntosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConAdjuntosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SinAdjuntosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
