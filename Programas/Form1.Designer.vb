<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AbrirToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.SeleccionarTodoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.MarcarComoLeidoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MarcarComoNoLeidoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NuevoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EdicionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpcionesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FuenteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ColoresToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NoLeidosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SeleccionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AgruparToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.MostrarUltimasEntradasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.AutoAbrirAdjuntosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoAbrirRelacionadosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.AutoGuardarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AgregarMaquinasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AgregarMaquinaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UsuariosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AgregarUsuarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CambiarContraseñaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
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
        Me.LeidosPorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ActualizarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpacidadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.BloquearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FontDialog1 = New System.Windows.Forms.FontDialog
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.Label1 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Maquina = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Adjuntos = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirToolStripMenuItem1, Me.ToolStripSeparator1, Me.SeleccionarTodoToolStripMenuItem, Me.ToolStripSeparator2, Me.MarcarComoLeidoToolStripMenuItem, Me.MarcarComoNoLeidoToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(203, 104)
        '
        'AbrirToolStripMenuItem1
        '
        Me.AbrirToolStripMenuItem1.Name = "AbrirToolStripMenuItem1"
        Me.AbrirToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+A"
        Me.AbrirToolStripMenuItem1.Size = New System.Drawing.Size(202, 22)
        Me.AbrirToolStripMenuItem1.Text = "Abrir"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(199, 6)
        '
        'SeleccionarTodoToolStripMenuItem
        '
        Me.SeleccionarTodoToolStripMenuItem.Name = "SeleccionarTodoToolStripMenuItem"
        Me.SeleccionarTodoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.SeleccionarTodoToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.SeleccionarTodoToolStripMenuItem.Text = "Seleccionar todo"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(199, 6)
        '
        'MarcarComoLeidoToolStripMenuItem
        '
        Me.MarcarComoLeidoToolStripMenuItem.Name = "MarcarComoLeidoToolStripMenuItem"
        Me.MarcarComoLeidoToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.MarcarComoLeidoToolStripMenuItem.Text = "Marcar como leido"
        '
        'MarcarComoNoLeidoToolStripMenuItem
        '
        Me.MarcarComoNoLeidoToolStripMenuItem.Name = "MarcarComoNoLeidoToolStripMenuItem"
        Me.MarcarComoNoLeidoToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.MarcarComoNoLeidoToolStripMenuItem.Text = "Marcar como no leido"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.EdicionToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.VerToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(989, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirToolStripMenuItem, Me.NuevoToolStripMenuItem, Me.SalirToolStripMenuItem})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.ArchivoToolStripMenuItem.Text = "&Archivo"
        '
        'AbrirToolStripMenuItem
        '
        Me.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem"
        Me.AbrirToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AbrirToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.AbrirToolStripMenuItem.Text = "&Abrir"
        '
        'NuevoToolStripMenuItem
        '
        Me.NuevoToolStripMenuItem.Name = "NuevoToolStripMenuItem"
        Me.NuevoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NuevoToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.NuevoToolStripMenuItem.Text = "&Nuevo"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SalirToolStripMenuItem.Text = "Sa&lir"
        '
        'EdicionToolStripMenuItem
        '
        Me.EdicionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpcionesToolStripMenuItem, Me.AgregarMaquinasToolStripMenuItem, Me.UsuariosToolStripMenuItem})
        Me.EdicionToolStripMenuItem.Name = "EdicionToolStripMenuItem"
        Me.EdicionToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.EdicionToolStripMenuItem.Text = "&Edicion"
        '
        'OpcionesToolStripMenuItem
        '
        Me.OpcionesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FuenteToolStripMenuItem, Me.ColoresToolStripMenuItem, Me.ToolStripSeparator3, Me.MostrarUltimasEntradasToolStripMenuItem, Me.ToolStripSeparator5, Me.AutoAbrirAdjuntosToolStripMenuItem, Me.AutoAbrirRelacionadosToolStripMenuItem, Me.ToolStripSeparator4, Me.AutoGuardarToolStripMenuItem})
        Me.OpcionesToolStripMenuItem.Name = "OpcionesToolStripMenuItem"
        Me.OpcionesToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.OpcionesToolStripMenuItem.Text = "&Opciones"
        '
        'FuenteToolStripMenuItem
        '
        Me.FuenteToolStripMenuItem.Name = "FuenteToolStripMenuItem"
        Me.FuenteToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.FuenteToolStripMenuItem.Text = "&Fuente"
        '
        'ColoresToolStripMenuItem
        '
        Me.ColoresToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NoLeidosToolStripMenuItem, Me.SeleccionToolStripMenuItem, Me.AgruparToolStripMenuItem})
        Me.ColoresToolStripMenuItem.Name = "ColoresToolStripMenuItem"
        Me.ColoresToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.ColoresToolStripMenuItem.Text = "&Colores"
        '
        'NoLeidosToolStripMenuItem
        '
        Me.NoLeidosToolStripMenuItem.Name = "NoLeidosToolStripMenuItem"
        Me.NoLeidosToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.NoLeidosToolStripMenuItem.Text = "No leidos"
        '
        'SeleccionToolStripMenuItem
        '
        Me.SeleccionToolStripMenuItem.Name = "SeleccionToolStripMenuItem"
        Me.SeleccionToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.SeleccionToolStripMenuItem.Text = "Leidos"
        '
        'AgruparToolStripMenuItem
        '
        Me.AgruparToolStripMenuItem.Name = "AgruparToolStripMenuItem"
        Me.AgruparToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.AgruparToolStripMenuItem.Text = "Agrupar"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(224, 6)
        '
        'MostrarUltimasEntradasToolStripMenuItem
        '
        Me.MostrarUltimasEntradasToolStripMenuItem.Name = "MostrarUltimasEntradasToolStripMenuItem"
        Me.MostrarUltimasEntradasToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.MostrarUltimasEntradasToolStripMenuItem.Text = "AutoMostrar ultimas entradas"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(224, 6)
        '
        'AutoAbrirAdjuntosToolStripMenuItem
        '
        Me.AutoAbrirAdjuntosToolStripMenuItem.Name = "AutoAbrirAdjuntosToolStripMenuItem"
        Me.AutoAbrirAdjuntosToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.AutoAbrirAdjuntosToolStripMenuItem.Text = "AutoAbrir adjuntos"
        '
        'AutoAbrirRelacionadosToolStripMenuItem
        '
        Me.AutoAbrirRelacionadosToolStripMenuItem.Name = "AutoAbrirRelacionadosToolStripMenuItem"
        Me.AutoAbrirRelacionadosToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.AutoAbrirRelacionadosToolStripMenuItem.Text = "AutoAbrir relacionados"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(224, 6)
        '
        'AutoGuardarToolStripMenuItem
        '
        Me.AutoGuardarToolStripMenuItem.Name = "AutoGuardarToolStripMenuItem"
        Me.AutoGuardarToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.AutoGuardarToolStripMenuItem.Text = "AutoGuardar"
        '
        'AgregarMaquinasToolStripMenuItem
        '
        Me.AgregarMaquinasToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AgregarMaquinaToolStripMenuItem})
        Me.AgregarMaquinasToolStripMenuItem.Name = "AgregarMaquinasToolStripMenuItem"
        Me.AgregarMaquinasToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.AgregarMaquinasToolStripMenuItem.Text = "&Maquinas"
        '
        'AgregarMaquinaToolStripMenuItem
        '
        Me.AgregarMaquinaToolStripMenuItem.Name = "AgregarMaquinaToolStripMenuItem"
        Me.AgregarMaquinaToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.AgregarMaquinaToolStripMenuItem.Text = "&Agregar maquinas"
        '
        'UsuariosToolStripMenuItem
        '
        Me.UsuariosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AgregarUsuarioToolStripMenuItem, Me.CambiarContraseñaToolStripMenuItem})
        Me.UsuariosToolStripMenuItem.Name = "UsuariosToolStripMenuItem"
        Me.UsuariosToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.UsuariosToolStripMenuItem.Text = "&Usuarios"
        '
        'AgregarUsuarioToolStripMenuItem
        '
        Me.AgregarUsuarioToolStripMenuItem.Name = "AgregarUsuarioToolStripMenuItem"
        Me.AgregarUsuarioToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.AgregarUsuarioToolStripMenuItem.Text = "&Agregar usuario"
        '
        'CambiarContraseñaToolStripMenuItem
        '
        Me.CambiarContraseñaToolStripMenuItem.Name = "CambiarContraseñaToolStripMenuItem"
        Me.CambiarContraseñaToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.CambiarContraseñaToolStripMenuItem.Text = "&Cambiar contraseña"
        '
        'BuscarToolStripMenuItem
        '
        Me.BuscarToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PorMaquinaToolStripMenuItem, Me.PorReferenciaToolStripMenuItem, Me.PorResponsableToolStripMenuItem, Me.PorAutorToolStripMenuItem, Me.PorContenidoToolStripMenuItem, Me.PorFechaToolStripMenuItem, Me.PorAdjuntosToolStripMenuItem, Me.LeidosPorToolStripMenuItem})
        Me.BuscarToolStripMenuItem.Name = "BuscarToolStripMenuItem"
        Me.BuscarToolStripMenuItem.Size = New System.Drawing.Size(51, 20)
        Me.BuscarToolStripMenuItem.Text = "&Buscar"
        '
        'PorMaquinaToolStripMenuItem
        '
        Me.PorMaquinaToolStripMenuItem.Name = "PorMaquinaToolStripMenuItem"
        Me.PorMaquinaToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.PorMaquinaToolStripMenuItem.Text = "Por &maquina"
        '
        'PorReferenciaToolStripMenuItem
        '
        Me.PorReferenciaToolStripMenuItem.Name = "PorReferenciaToolStripMenuItem"
        Me.PorReferenciaToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.PorReferenciaToolStripMenuItem.Text = "Por r&eferencia"
        '
        'PorResponsableToolStripMenuItem
        '
        Me.PorResponsableToolStripMenuItem.Name = "PorResponsableToolStripMenuItem"
        Me.PorResponsableToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.PorResponsableToolStripMenuItem.Text = "Por re&sponsable"
        '
        'PorAutorToolStripMenuItem
        '
        Me.PorAutorToolStripMenuItem.Name = "PorAutorToolStripMenuItem"
        Me.PorAutorToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.PorAutorToolStripMenuItem.Text = "Por &autor"
        '
        'PorContenidoToolStripMenuItem
        '
        Me.PorContenidoToolStripMenuItem.Name = "PorContenidoToolStripMenuItem"
        Me.PorContenidoToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.PorContenidoToolStripMenuItem.Text = "Por contenido"
        '
        'PorFechaToolStripMenuItem
        '
        Me.PorFechaToolStripMenuItem.Name = "PorFechaToolStripMenuItem"
        Me.PorFechaToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.PorFechaToolStripMenuItem.Text = "Por fecha"
        '
        'PorAdjuntosToolStripMenuItem
        '
        Me.PorAdjuntosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConAdjuntosToolStripMenuItem, Me.SinAdjuntosToolStripMenuItem})
        Me.PorAdjuntosToolStripMenuItem.Name = "PorAdjuntosToolStripMenuItem"
        Me.PorAdjuntosToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.PorAdjuntosToolStripMenuItem.Text = "Por adjuntos"
        '
        'ConAdjuntosToolStripMenuItem
        '
        Me.ConAdjuntosToolStripMenuItem.Name = "ConAdjuntosToolStripMenuItem"
        Me.ConAdjuntosToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.ConAdjuntosToolStripMenuItem.Text = "Con adjuntos"
        '
        'SinAdjuntosToolStripMenuItem
        '
        Me.SinAdjuntosToolStripMenuItem.Name = "SinAdjuntosToolStripMenuItem"
        Me.SinAdjuntosToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.SinAdjuntosToolStripMenuItem.Text = "Sin adjuntos"
        '
        'LeidosPorToolStripMenuItem
        '
        Me.LeidosPorToolStripMenuItem.Name = "LeidosPorToolStripMenuItem"
        Me.LeidosPorToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.LeidosPorToolStripMenuItem.Text = "No leidos"
        '
        'VerToolStripMenuItem
        '
        Me.VerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActualizarToolStripMenuItem, Me.OpacidadToolStripMenuItem, Me.BloquearToolStripMenuItem, Me.VersionToolStripMenuItem})
        Me.VerToolStripMenuItem.Name = "VerToolStripMenuItem"
        Me.VerToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.VerToolStripMenuItem.Text = "&Ver"
        '
        'ActualizarToolStripMenuItem
        '
        Me.ActualizarToolStripMenuItem.Name = "ActualizarToolStripMenuItem"
        Me.ActualizarToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.ActualizarToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.ActualizarToolStripMenuItem.Text = "&Actualizar"
        '
        'OpacidadToolStripMenuItem
        '
        Me.OpacidadToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5})
        Me.OpacidadToolStripMenuItem.Name = "OpacidadToolStripMenuItem"
        Me.OpacidadToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.OpacidadToolStripMenuItem.Text = "Opacidad"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(114, 22)
        Me.ToolStripMenuItem2.Text = "100%"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(114, 22)
        Me.ToolStripMenuItem3.Text = "90%"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(114, 22)
        Me.ToolStripMenuItem4.Text = "80%"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(114, 22)
        Me.ToolStripMenuItem5.Text = "70%"
        '
        'BloquearToolStripMenuItem
        '
        Me.BloquearToolStripMenuItem.Name = "BloquearToolStripMenuItem"
        Me.BloquearToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.BloquearToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.BloquearToolStripMenuItem.Text = "Bloquear"
        '
        'VersionToolStripMenuItem
        '
        Me.VersionToolStripMenuItem.Name = "VersionToolStripMenuItem"
        Me.VersionToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.VersionToolStripMenuItem.Text = "Version"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel3, Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2, Me.ToolStripProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 486)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(989, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Image = CType(resources.GetObject("ToolStripStatusLabel3.Image"), System.Drawing.Image)
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(66, 17)
        Me.ToolStripStatusLabel3.Text = "Entradas"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Image = CType(resources.GetObject("ToolStripStatusLabel1.Image"), System.Drawing.Image)
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(58, 17)
        Me.ToolStripStatusLabel1.Text = "Sin leer"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(748, 17)
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
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(989, 508)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Espere..."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
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
        Me.DataGridView1.Size = New System.Drawing.Size(989, 462)
        Me.DataGridView1.TabIndex = 4
        '
        'Id
        '
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
        '
        'Timer1
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(989, 508)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Bitacora de Mantenimiento"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NuevoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EdicionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpcionesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AgregarMaquinasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsuariosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ActualizarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FuenteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents ColoresToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NoLeidosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents AgregarUsuarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CambiarContraseñaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AgregarMaquinaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AbrirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SeleccionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorMaquinaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorResponsableToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorReferenciaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorAutorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LeidosPorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BloquearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AgruparToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpacidadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorContenidoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoAbrirAdjuntosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoAbrirRelacionadosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AbrirToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SeleccionarTodoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MarcarComoLeidoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MarcarComoNoLeidoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PorFechaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents PorAdjuntosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConAdjuntosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SinAdjuntosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Maquina As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Adjuntos As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents AutoGuardarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MostrarUltimasEntradasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel

End Class
