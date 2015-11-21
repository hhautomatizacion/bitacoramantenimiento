Public Class Form1

    Dim bEnterPresionado As Boolean
    Dim sCronometro As New System.Diagnostics.Stopwatch
    Dim iPorcentajeAnterior As Integer
    Dim iRestanteAnterior As Integer

    Dim lUltimoRegistroCargado As Long

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim sArchivo As String
        Dim fForma As Form2
        Dim p As System.Diagnostics.Process
        e.Cancel = False
        For Each fForma In cVentanasEditor
            fForma.Close()
            If fForma.Visible Then e.Cancel = True
        Next
        If e.Cancel = False Then Me.Visible = False

        For Each p In cProcesos
            Try
                p.CloseMainWindow()
            Catch
            End Try
        Next

        GuardarPosicion(Me)
        GuardarAnchoColumnas(Me, DataGridView1)
        GuardarOpciones()

        Try
            For Each sArchivo In My.Computer.FileSystem.GetFiles(sTempDir)
                If My.Computer.FileSystem.GetFileInfo(sArchivo).LastAccessTime.AddDays(lDiasConservarTemporales) < Now Then
                    My.Computer.FileSystem.DeleteFile(sArchivo, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                End If
            Next
            Debug.Print("end!")
        Catch mierror As Exception

        End Try
    End Sub
    Private Sub GuardarOpciones()
        SaveSetting("BitacoraMantenimiento", "General", "Server", sServer)
        SaveSetting("BitacoraMantenimiento", "General", "TempDir", sTempDir)
        SaveSetting("BitacoraMantenimiento", "General", "LogDir", sLogDir)
        MiSaveSetting("BitacoraMantenimiento", "General", "ColorLeido", iColorLeido.ToString)
        MiSaveSetting("BitacoraMantenimiento", "General", "ColorNoLeido", iColorNoLeido.ToString)
        MiSaveSetting("BitacoraMantenimiento", "General", "ColorAgrupar", iColorAgrupar.ToString)
        MiSaveSetting("BitacoraMantenimiento", "General", "AutoGuardarSegundos", iAutoGuardarSegundos.ToString)
        MiSaveSetting("BitacoraMantenimiento", "General", "AutoAbrirAdjuntos", -Val(bAutoAbrirAdjuntos))
        MiSaveSetting("BitacoraMantenimiento", "General", "AutoAbrirRelacionados", -Val(bAutoAbrirRelacionados))
        MiSaveSetting("BitacoraMantenimiento", "General", "MesesACargar", lMesesACargar.ToString)
        MiSaveSetting("BitacoraMantenimiento", "General", "DiasConservarTemporales", lDiasConservarTemporales.ToString)
    End Sub

    Private Sub ActualizarMenuStrip()
        Try
            Select Case Me.Opacity.ToString("0.00")
                Case "1.00"
                    ToolStripMenuItem2.Checked = True
                    ToolStripMenuItem3.Checked = False
                    ToolStripMenuItem4.Checked = False
                    ToolStripMenuItem5.Checked = False
                Case "0.90"
                    ToolStripMenuItem2.Checked = False
                    ToolStripMenuItem3.Checked = True
                    ToolStripMenuItem4.Checked = False
                    ToolStripMenuItem5.Checked = False
                Case "0.80"
                    ToolStripMenuItem2.Checked = False
                    ToolStripMenuItem3.Checked = False
                    ToolStripMenuItem4.Checked = True
                    ToolStripMenuItem5.Checked = False
                Case "0.70"
                    ToolStripMenuItem2.Checked = False
                    ToolStripMenuItem3.Checked = False
                    ToolStripMenuItem4.Checked = False
                    ToolStripMenuItem5.Checked = True
                Case Else
                    ToolStripMenuItem2.Checked = False
                    ToolStripMenuItem3.Checked = False
                    ToolStripMenuItem4.Checked = False
                    ToolStripMenuItem5.Checked = False
            End Select
            AutoAbrirAdjuntosToolStripMenuItem.Checked = bAutoAbrirAdjuntos
            AutoAbrirRelacionadosToolStripMenuItem.Checked = bAutoAbrirRelacionados
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lInicio As Long
        cVentanasEditor = New Collection
        cProcesos = New Collection
        sVersion = My.Application.Info.Version.ToString
        SplashScreen1.Show()
        sServer = GetSetting("BitacoraMantenimiento", "General", "Server", "mttoserver")
        sUser = GetSetting("BitacoraMantenimiento", "General", "User", "root")
        sPassword = GetSetting("BitacoraMantenimiento", "General", "Password", "manttocl")
        sTempDir = GetSetting("BitacoraMantenimiento", "General", "TempDir", "c:\temp\")
        sLogDir = GetSetting("BitacoraMantenimiento", "General", "LogDir", "c:\log\")
        SaveSetting("BitacoraMantenimiento", "General", "Server", sServer)
        SaveSetting("BitacoraMantenimiento", "General", "TempDir", sTempDir)
        SaveSetting("BitacoraMantenimiento", "General", "LogDir", sLogDir)
        If Not ConexionEstablecida() Then
            Do
            Loop Until ConexionEstablecida()
        End If
        sFechaAnterior = ""
        mConexionInicial = NuevaConexion()
        mConexionSecundaria = NuevaConexion()
        mConexionModuloPrincipal = NuevaConexion()




        CrearDirectorio(sTempDir)
        CrearDirectorio(sLogDir)

        SplashScreen1.Close()
        LoginForm1.ShowDialog()
        If iIdUsuario = 0 Then End
        ColocarForm(Me)
        CargarOpciones()
        Label1.Font = New Font(sNombreFuente, iTamanioFuente)
        iTamanioMaximoArchivo = Val(Obtener("show variables where variable_name='max_allowed_packet'", "value", mConexionSecundaria))

        '        If iTamanioMaximoArchivo = 0 Then iTamanioMaximoArchivo = 100 * 1024

        lInicio = Val(Obtener("SELECT id FROM entradas where fecha < date_sub(now(),interval " & lMesesACargar & " month) order by fecha desc limit 1", "id", mConexionSecundaria))
        'If lInicio > 1 Then lInicio = lInicio  Else lInicio = 0
        Actualizar(lInicio)
        ActualizarMenuStrip()
        CargarAnchoColumnas(Me, DataGridView1)

        Timer1.Interval = 1000 * iAutoGuardarSegundos
        Timer1.Enabled = False
        If System.IO.File.Exists(sTempDir & iIdUsuario & "-autosave.bak") Then
            LoadAutoSaved()
        End If
    End Sub
    Sub LoadAutoSaved()
        Dim cEntradas As New Collection
        Dim eNuevaEntrada As Entrada
        Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Dim fs As New System.IO.FileStream(sTempDir & iIdUsuario & "-autosave.bak", IO.FileMode.Open)
        Try
            cEntradas = bf.Deserialize(fs)
        Catch
            Debug.Print("No se pudo restaurar las entradas autoguardadas")
        End Try
        fs.Close()
        Debug.Print("Restaurando " & cEntradas.Count)
        For Each eNuevaEntrada In cEntradas
            Dim fNuevaEntrada As New Form2

            fNuevaEntrada.iIdMaquina = eNuevaEntrada.IdMaquina
            fNuevaEntrada.RichTextBox1.Rtf = eNuevaEntrada.RTF
            fNuevaEntrada.cAdjuntos = eNuevaEntrada.Adjuntos
            fNuevaEntrada.cReferencias = eNuevaEntrada.Referencias
            fNuevaEntrada.cResponsables = eNuevaEntrada.Responsables
            fNuevaEntrada.bModificado = True
            Timer1.Enabled = True
            fNuevaEntrada.Show()
        Next

        BorrarAutosave()
    End Sub

    Private Sub CrearDirectorio(ByVal Directorio As String)
        If My.Computer.FileSystem.DirectoryExists(Directorio) Then
        Else
            Try
                My.Computer.FileSystem.CreateDirectory(Directorio)
            Catch
            End Try
        End If

    End Sub
    Private Sub CargarOpciones()

        sNombreFuente = MiGetSetting("BitacoraMantenimiento", "General", "NombreFuente", "Arial")
        iTamanioFuente = Val(Replace(MiGetSetting("BitacoraMantenimiento", "General", "TamanioFuente", "10"), ",", "."))
        iColorLeido = Val(MiGetSetting("BitacoraMantenimiento", "General", "ColorLeido", Color.LightGray.ToArgb))
        iColorNoLeido = Val(MiGetSetting("BitacoraMantenimiento", "General", "ColorNoLeido", Color.Yellow.ToArgb))
        iColorAgrupar = Val(MiGetSetting("BitacoraMantenimiento", "General", "ColorAgrupar", "-1513240"))
        bAutoAbrirAdjuntos = -Val(MiGetSetting("BitacoraMantenimiento", "General", "AutoAbrirAdjuntos", "1"))
        bAutoAbrirRelacionados = -Val(MiGetSetting("BitacoraMantenimiento", "General", "AutoAbrirRelacionados", "1"))
        iAutoGuardarSegundos = MiGetSetting("BitacoraMantenimiento", "General", "AutoGuardarSegundos", "3")
        lMesesACargar = MiGetSetting("BitacoraMantenimiento", "General", "MesesACargar", "3")
        lDiasConservarTemporales = MiGetSetting("BitacoraMantenimiento", "General", "DiasConservarTemporales", "6")

    End Sub
    Sub ActualizarUltimos()
        Actualizar(lUltimoRegistroCargado)
    End Sub
    Private Sub Actualizar(ByVal InicioRegistros)


        ActualizarNoLeidos()

        Me.Text = "Bitacora de Mantenimiento (" & Obtener("select nombre from usuarios where id=" & iIdUsuario, "nombre", mConexionSecundaria) & ")"

        If BackgroundWorker1.IsBusy Then
            ToolStripStatusLabel1.Text = "Espere por favor..."
        Else

            DataGridView1.Visible = False
            Label1.Visible = True
            sCronometro.Start()
            BackgroundWorker1.RunWorkerAsync(InicioRegistros)

        End If


    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader

        Dim iContador As Integer
        Dim iRenglonesNuevos As Integer
        Dim iRenglonesPrevios As Integer
        Dim dFecha As Date
        Dim iDiaAnterior As Integer
        Dim bColorear As Boolean

        Dim iPorcentaje As Integer




        iRenglonesPrevios = CInt(e.Argument)
        iRenglonesNuevos = Val(Obtener("SELECT COUNT(id) AS renglonesnuevos FROM entradas WHERE id >" & iRenglonesPrevios, "renglonesnuevos", mConexionSecundaria))
        iContador = 1

        If iRenglonesNuevos > 0 Then


            mLector = Consulta("SELECT id,fecha,maquina,descripcion FROM entradas WHERE id >" & iRenglonesPrevios, mConexionInicial)
            'Consulta("select entradas.id,entradas.fecha,maquinas.descripcion as maquina,entradas.descripcion from entradas left join maquinas on entradas.maquina=maquinas.id where entradas.id >" & iRenglonesPrevios, mConexionInicial)

            Do While mLector.Read
                Dim sColumnas(3) As String
                Dim rRenglonNuevo As Renglon

                rRenglonNuevo = Nothing


                dFecha = CDate(mLector.Item("fecha"))
                sColumnas(0) = mLector.Item("id").ToString
                sColumnas(1) = dFecha.ToString("yyyy-MM-dd HH:mm:ss")
                sColumnas(2) = Obtener("SELECT descripcion FROM maquinas WHERE id=" & mLector.Item("maquina").ToString, "descripcion", mConexionSecundaria)
                sColumnas(3) = mLector.Item("descripcion")

                rRenglonNuevo.Id = CInt(mLector.Item("id"))
                rRenglonNuevo.Celdas = sColumnas
                If dFecha.Day = iDiaAnterior Then
                Else
                    bColorear = Not bColorear
                    iDiaAnterior = dFecha.Day
                End If
                rRenglonNuevo.Colorear = bColorear
                Dim iTemp As Integer
                iTemp = Val(Obtener("select count(identrada) as archivos_adjuntos from adjuntos where identrada =" & mLector.Item("id") & " group by identrada", "archivos_adjuntos", mConexionSecundaria))
                If iTemp > 0 Then
                    rRenglonNuevo.TieneAdjuntos = True
                    rRenglonNuevo.Adjuntos = iTemp
                End If
                iTemp = Val(Obtener("select identrada from leidos where identrada =" & mLector.Item("id") & " and idusuario=" & iIdUsuario & " ", "identrada", mConexionSecundaria))
                If iTemp > 0 Then
                    rRenglonNuevo.Leido = True
                End If


                iPorcentaje = ((iContador / iRenglonesNuevos) * 100)
                BackgroundWorker1.ReportProgress(iPorcentaje, rRenglonNuevo)
                iContador = iContador + 1
            Loop
            mLector.Close()

        End If
        e.Result = CBool(iContador > 1)

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

        Dim iPorcentajeProgreso As Integer
        Dim iElapsedSeconds As Integer
        Dim iRestante As Integer
        Dim rRenglonNuevo As Renglon
        iPorcentajeProgreso = e.ProgressPercentage
        If iPorcentajeProgreso > 0 Then
            ToolStripProgressBar1.Value = iPorcentajeProgreso
            If iPorcentajeProgreso Mod 5 = 0 And iPorcentajeProgreso <> iPorcentajeAnterior Then
                iElapsedSeconds = sCronometro.Elapsed.TotalSeconds
                iRestante = (100 * iElapsedSeconds / iPorcentajeProgreso) - iElapsedSeconds
                If iRestante > 0 Then
                    If iRestanteAnterior = 0 Then
                        iRestanteAnterior = iRestante + 1
                    End If
                    If iRestante < iRestanteAnterior Then
                        Label1.Text = "Espere...." & vbCrLf & iRestante.ToString("0") & " segundos"
                        iRestanteAnterior = iRestante
                    End If
                End If
                iPorcentajeAnterior = iPorcentajeProgreso
            End If
        End If
        rRenglonnuevo = e.UserState
        DataGridView1.Rows.Add(rRenglonNuevo.Celdas)
        If rRenglonNuevo.Leido Then
            If rRenglonNuevo.Colorear Then
                ColorearRenglon(DataGridView1.Rows.Count - 1, iColorAgrupar)
            End If
        Else
            ColorearRenglon(DataGridView1.Rows.Count - 1, iColorNoLeido)
        End If
        If rRenglonNuevo.TieneAdjuntos Then
            DataGridView1.Item(4, DataGridView1.Rows.Count - 1).Value = 1
            DataGridView1.Item(4, DataGridView1.Rows.Count - 1).ToolTipText = rRenglonNuevo.Adjuntos
        End If
        lUltimoRegistroCargado = rRenglonNuevo.Id

    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        ActualizarNoLeidos()


        If DataGridView1.RowCount > 0 And e.Result = True Then
            MoverVisualizacionDataGrid(DataGridView1.RowCount - 1, True)
        End If
        
        sCronometro.Stop()
        Debug.Print("Form1_BackgroundWorker1:".PadRight(25, " ") & sCronometro.Elapsed.ToString)
        sCronometro.Reset()
        Label1.Visible = False
        DataGridView1.Visible = True
    End Sub
    Sub MoverVisualizacionDataGrid(ByVal iRenglon As Integer, Optional ByVal Seleccionar As Boolean = False)
        Try
            DataGridView1.CurrentCell = DataGridView1.Item(0, iRenglon)
            DataGridView1.FirstDisplayedCell = DataGridView1.Item(0, iRenglon)
            If Seleccionar Then DataGridView1.Item(0, iRenglon).Selected = True
        Catch
        End Try
    End Sub
    Private Sub ActualizarNoLeidos()
        Dim iNoLeidos As Integer
        iNoLeidos = Val(Obtener("select count(id) as total from entradas", "total", mConexionSecundaria)) - Val(Obtener("select count(distinct(identrada)) as leidos from leidos where idusuario=" & iIdUsuario & "", "leidos", mConexionSecundaria))
        ToolStripStatusLabel1.Text = iNoLeidos.ToString & " Sin leer"
        ToolStripStatusLabel3.Text = DataGridView1.Rows.Count.ToString & " Entradas"
        ToolStripProgressBar1.Value = 0
    End Sub


    Private Sub ColorearRenglon(ByVal Renglon As Integer, ByVal Color As Integer)
        DataGridView1.Item(0, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
        DataGridView1.Item(1, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
        DataGridView1.Item(2, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
        DataGridView1.Item(3, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
        DataGridView1.Item(4, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
    End Sub


    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        Dim fNuevaEntrada As New Form2

        Timer1.Enabled = True
        fNuevaEntrada.Show()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim iId As Integer
        If e.RowIndex >= 0 Then
            iId = Val(DataGridView1.Item(0, e.RowIndex).Value)
            If iId >= 0 Then
                ColorearRenglon(e.RowIndex, iColorLeido)
                AbrirEntrada(iId)
                ActualizarNoLeidos()
            End If
        End If

    End Sub



    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ActualizarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActualizarToolStripMenuItem.Click
        Actualizar(lUltimoRegistroCargado)
    End Sub

    Private Sub FuenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FuenteToolStripMenuItem.Click
        FontDialog1.Font = New Font(sNombreFuente, iTamanioFuente)
        If FontDialog1.ShowDialog = Windows.Forms.DialogResult.Cancel Then
        Else
            sNombreFuente = FontDialog1.Font.Name
            iTamanioFuente = FontDialog1.Font.Size
            MiSaveSetting("BitacoraMantenimiento", "General", "NombreFuente", sNombreFuente)
            MiSaveSetting("BitacoraMantenimiento", "General", "TamanioFuente", iTamanioFuente.ToString)
            CargarAnchoColumnas(Me, DataGridView1)
            Actualizar(lUltimoRegistroCargado)
        End If


    End Sub

    Private Sub NoLeidosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoLeidosToolStripMenuItem.Click
        ColorDialog1.Color = Color.FromArgb(iColorNoLeido)
        If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            iColorNoLeido = ColorDialog1.Color.ToArgb
            Actualizar(lUltimoRegistroCargado)
        End If
    End Sub

    Private Sub AgregarUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgregarUsuarioToolStripMenuItem.Click
        Dim iEsSuper As Integer
        Dim sEsSuper As String
        Dim sNumeroEmpleado1 As String
        Dim sNumeroEmpleado2 As String
        Dim sNombre As String
        Dim sPassword1 As String
        Dim sPassword2 As String
        Dim sExistente As String
        Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection

        Dim mComando As MySql.Data.MySqlClient.MySqlCommand
        mComando = New MySql.Data.MySqlClient.MySqlCommand


        mConexion = NuevaConexion()

        If Not bSuperUsuario Then
            MsgBox("No cuenta con suficientes permisos para realizar esta accion")
            Exit Sub
        End If
        Do
            sEsSuper = InputBox("El nuevo usuario es SuperUsuario", "Nuevo usuario").ToUpper
        Loop Until sEsSuper = "SI" Or sEsSuper = "NO"
        If sEsSuper = "SI" Then
            iEsSuper = 1
        End If
        Do
            sNumeroEmpleado1 = InputBox("Teclee numero de empleado", "Nuevo usuario")
            sNumeroEmpleado2 = InputBox("Vuelva a teclear numero de empleado", "Nuevo usuario")
            If Val(sNumeroEmpleado1) = 0 Then sNumeroEmpleado1 = ""
            If Val(sNumeroEmpleado2) = 0 Then sNumeroEmpleado2 = ""
            If Len(sNumeroEmpleado1) = 0 Then Exit Sub
            If Len(sNumeroEmpleado2) = 0 Then Exit Sub
        Loop Until sNumeroEmpleado1 = sNumeroEmpleado2
        sNombre = InputBox("Teclee nombre completo", "Nuevo usuario")
        Do
            sPassword1 = InputBox("Teclee nuevo password", "Nuevo usuario")
            sPassword2 = InputBox("Vuelva a teclear nuevo password", "Nuevo usuario")
            If Len(sPassword1) > 0 Then sPassword1 = sPassword1.Replace("'", "")
            If Len(sPassword2) > 0 Then sPassword2 = sPassword2.Replace("'", "")
            If Len(sPassword1) = 0 Then Exit Sub
            If Len(sPassword2) = 0 Then Exit Sub
        Loop Until sPassword1 = sPassword2
        If Len(sNombre) > 0 Then
            sNombre = sNombre.ToUpper
            sNombre = sNombre.Replace("'", "")
            sNombre = Strings.Left(sNombre, 40)
        Else
            Exit Sub
        End If
        If sPassword1 = sPassword2 And sNumeroEmpleado1 = sNumeroEmpleado2 Then
            sExistente = Obtener("select nombre from usuarios where no_empleado=" & sNumeroEmpleado1, "nombre", mConexionSecundaria)
            If Len(sExistente) > 0 Then
                MsgBox("Ya existe un usuario con ese numero de empleado." & vbCrLf & sExistente)
            Else
                Try
                    mComando.Connection = mConexion
                    mComando.CommandText = "insert into usuarios values (0," & iIdUsuario & ",'" & sNombre & "'," & sNumeroEmpleado1 & ",sha1('" & sPassword1 & "')," & IESSUPER.TOSTRING & ",now(),now(),'" & sVersion & "')"
                    mComando.ExecuteNonQuery()
                    MsgBox("El usuario se ha agregado con exito")
                    Actualizar(lUltimoRegistroCargado)
                Catch mierror As MySql.Data.MySqlClient.MySqlException
                    ReportarError(mierror.ToString)
                End Try
            End If
        Else
            MsgBox("Ambos intentos no coinciden.")
        End If


    End Sub

    Private Sub CambiarContraseñaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CambiarContraseñaToolStripMenuItem.Click
        Dim sPassword1 As String
        Dim sPassword2 As String
        Dim mComando As MySql.Data.MySqlClient.MySqlCommand
        Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection
        mComando = New MySql.Data.MySqlClient.MySqlCommand

        mConexion = NuevaConexion()

        iIdUsuario = 0
        LoginForm1.ShowDialog()
        If iIdUsuario = 0 Then End
        sPassword1 = InputBox("Teclee nuevo password", "Cambiar contraseña")
        sPassword2 = InputBox("Vuelva a teclear nuevo password", "Cambiar contraseña")
        If Len(sPassword1) > 0 Then sPassword1 = sPassword1.Replace("'", "")
        If Len(sPassword2) > 0 Then sPassword2 = sPassword2.Replace("'", "")
        If Len(sPassword1) = 0 Or Len(sPassword2) = 0 Then
            MsgBox("No se ha realizado el cambio de contraseña")
        Else
            If sPassword1 = sPassword2 Then
                Try
                    mComando.Connection = mConexion
                    mComando.CommandText = "update usuarios set palabra_ingreso=sha1('" & sPassword1 & "') where id=" & iIdUsuario
                    mComando.ExecuteNonQuery()
                    MsgBox("La contraseña se ha cambiado con exito")
                Catch mierror As MySql.Data.MySqlClient.MySqlException
                    ReportarError(mierror.ToString)
                End Try
            Else
                MsgBox("Ambos intentos no coinciden. No se ha realizado el cambio de contraseña")
            End If
        End If
    End Sub

    Private Sub AgregarMaquinaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgregarMaquinaToolStripMenuItem.Click
        If bSuperUsuario Then
            Form4.Show()
        Else
            MsgBox("No cuenta con los suficientes permisos para realizar esta accion.")
        End If
    End Sub

    Private Sub AbrirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirToolStripMenuItem.Click
        Dim iID As Integer
        Dim iItem As Integer
        For iItem = 0 To DataGridView1.SelectedRows.Count - 1
            iID = (DataGridView1.Item(0, DataGridView1.SelectedRows.Item(iItem).Index).Value)
            ColorearRenglon(DataGridView1.SelectedRows.Item(iItem).Index, iColorLeido)
            AbrirEntrada(iID)
        Next
        ActualizarNoLeidos()
    End Sub

    Private Sub SeleccionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionToolStripMenuItem.Click
        ColorDialog1.Color = Color.FromArgb(iColorLeido)
        If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            iColorLeido = ColorDialog1.Color.ToArgb
            Actualizar(lUltimoRegistroCargado)
        End If
    End Sub

    Private Sub PorMaquinaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorMaquinaToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim iMaquina As Integer
        Dim cIdEntradas As Collection
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()

        cIdEntradas = New Collection
        iMaquina = SeleccionarMaquina()
        If iMaquina > 0 Then
            mLector = Consulta("select id from entradas where maquina=" & iMaquina, Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes de la maquina " & Obtener("select descripcion from maquinas where id=" & iMaquina, "descripcion", mConexionSecundaria)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If
    End Sub

    Private Sub PorResponsableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorResponsableToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim iResponsable As Integer
        Dim cIdEntradas As Collection
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()

        cIdEntradas = New Collection
        iResponsable = SeleccionarResponsable()
        If iResponsable > 0 Then
            mLector = Consulta("select identrada as id from responsables where idresponsable=" & iResponsable, Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes de trabajos realizados por " & Obtener("select nombre from usuarios where id=" & iResponsable, "nombre", mConexionSecundaria)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If

    End Sub

    Private Sub PorReferenciaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorReferenciaToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim iReferencia As Integer
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()

        Dim cIdEntradas As Collection
        cIdEntradas = New Collection
        iReferencia = SeleccionarMaquina()
        If iReferencia > 0 Then
            mLector = Consulta("SELECT identrada as id FROM referenciamaquinas where idmaquina=" & iReferencia, Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes con referencia a la maquina " & Obtener("select descripcion from maquinas where id=" & iReferencia, "descripcion", mConexionSecundaria)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If
    End Sub

    Private Sub PorAutorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorAutorToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim iUsuario As Integer
        Dim cIdEntradas As Collection
        cIdEntradas = New Collection
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()

        iUsuario = SeleccionarResponsable()
        If iUsuario > 0 Then
            mLector = Consulta("SELECT id FROM entradas where usuario=" & iUsuario, Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes realizados por " & Obtener("select nombre from usuarios where id=" & iUsuario, "nombre", mConexionSecundaria)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If
    End Sub

    Private Sub LeidosPorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeidosPorToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim iUsuario As Integer
        Dim cIdEntradas As Collection
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()


        cIdEntradas = New Collection
        If bSuperUsuario Then
            iUsuario = SeleccionarResponsable()
        Else
            iUsuario = iIdUsuario
        End If
        If iUsuario > 0 Then
            mLector = Consulta("select entradas.id from entradas  left join leidos on leidos.idusuario=" & iUsuario & " and entradas.id=leidos.identrada where leidos.identrada is null", Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes no leidos por " & Obtener("select nombre from usuarios where id=" & iUsuario, "nombre", Conexion)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If
    End Sub

    Private Sub BloquearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BloquearToolStripMenuItem.Click
        Dim iConteo As Integer
        GuardarOpciones()
        GuardarAnchoColumnas(Me, DataGridView1)
        iIdUsuario = 0
        iConteo = 0
        Do
            LoginForm1.ShowDialog()
            iConteo = iConteo + 1
            If iConteo > 3 Then End
        Loop Until iIdUsuario > 0
        CargarOpciones()
        CargarAnchoColumnas(Me, DataGridView1)
        Actualizar(lUltimoRegistroCargado)
    End Sub

    Private Sub VersionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersionToolStripMenuItem.Click
        AboutBox1.Show()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            bEnterPresionado = True
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        Dim iId As Integer
        If DataGridView1.RowCount > 0 Then
            If e.KeyCode = Keys.Enter Then
                If bEnterPresionado Then
                    bEnterPresionado = False
                    iId = Val(DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value)
                    If iId >= 0 Then
                        ColorearRenglon(DataGridView1.CurrentRow.Index, iColorLeido)
                        AbrirEntrada(iId)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub AgruparToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgruparToolStripMenuItem.Click
        ColorDialog1.Color = Color.FromArgb(iColorAgrupar)
        If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            iColorAgrupar = ColorDialog1.Color.ToArgb
            Actualizar(lUltimoRegistroCargado)
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Me.Opacity = 1
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Me.Opacity = 0.9
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Me.Opacity = 0.8
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        Me.Opacity = 0.7
        ActualizarMenuStrip()
    End Sub

    Private Sub PorContenidoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorContenidoToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim sCadenaBusqueda As String
        Dim cIdEntradas As Collection
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()

        cIdEntradas = New Collection
        sCadenaBusqueda = InputBox("Palabra a buscar dentro del cuerpo del mensaje:")
        If Len(sCadenaBusqueda) > 0 Then
            sCadenaBusqueda = sCadenaBusqueda.Replace("'", "")
            mLector = Consulta("SELECT id FROM entradas where UPPER(CONVERT(CONTENIDO USING UTF8)) like '%" & sCadenaBusqueda.ToUpper & "%'", Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes que contienen '" & sCadenaBusqueda & "' en el cuerpo del mensaje"
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If
        Conexion.Close()
    End Sub



    Private Sub AutoAbrirAdjuntosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoAbrirAdjuntosToolStripMenuItem.Click
        bAutoAbrirAdjuntos = Not bAutoAbrirAdjuntos
        ActualizarMenuStrip()
    End Sub

    Private Sub AutoAbrirRalacionadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoAbrirRelacionadosToolStripMenuItem.Click
        bAutoAbrirRelacionados = Not bAutoAbrirRelacionados
        ActualizarMenuStrip()

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub AbrirToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirToolStripMenuItem1.Click
        AbrirToolStripMenuItem.PerformClick()
    End Sub

    Private Sub SeleccionarTodoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionarTodoToolStripMenuItem.Click
        DataGridView1.SelectAll()
    End Sub

    Private Sub MarcarComoLeidoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarcarComoLeidoToolStripMenuItem.Click
        Dim iID As Integer
        Dim iItem As Integer
        Dim mComando As MySql.Data.MySqlClient.MySqlCommand
        mComando = New MySql.Data.MySqlClient.MySqlCommand
        Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection

        mConexion = NuevaConexion()

        For iItem = 0 To DataGridView1.SelectedRows.Count - 1
            iID = (DataGridView1.Item(0, DataGridView1.SelectedRows.Item(iItem).Index).Value)
            ColorearRenglon(DataGridView1.SelectedRows.Item(iItem).Index, iColorLeido)
            Try
                mComando.Connection = mConexion
                mComando.CommandText = "insert into leidos values (0," & iID.ToString & "," & iIdUsuario.ToString & ",now())"
                mComando.ExecuteNonQuery()
            Catch mierror As MySql.Data.MySqlClient.MySqlException
                ReportarError(mierror.ToString)
            End Try
        Next
    End Sub

    Private Sub MarcarComoNoLeidoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarcarComoNoLeidoToolStripMenuItem.Click
        Dim iID As Integer
        Dim iItem As Integer
        Dim mComando As MySql.Data.MySqlClient.MySqlCommand
        mComando = New MySql.Data.MySqlClient.MySqlCommand
        Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection


        mConexion = NuevaConexion()

        For iItem = 0 To DataGridView1.SelectedRows.Count - 1
            iID = (DataGridView1.Item(0, DataGridView1.SelectedRows.Item(iItem).Index).Value)
            ColorearRenglon(DataGridView1.SelectedRows.Item(iItem).Index, iColorNoLeido)
            Try
                mComando.Connection = mConexion
                mComando.CommandText = "update leidos set idusuario=" & (iIdUsuario + 1000).ToString & " where identrada=" & iID.ToString & "  and idusuario=" & iIdUsuario.ToString
                mComando.ExecuteNonQuery()
            Catch mierror As MySql.Data.MySqlClient.MySqlException
                ReportarError(mierror.ToString)
            End Try
        Next

    End Sub

    Private Sub PorFechaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorFechaToolStripMenuItem.Click
        Dim fDialogoFecha As New Form11
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim cIdEntradas As Collection
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()

        fDialogoFecha.ShowDialog()
        If fDialogoFecha.Tag.GetType.IsArray Then
            cIdEntradas = New Collection
            mLector = Consulta("SELECT id FROM entradas where fecha between '" & fDialogoFecha.Tag(0) & "' and '" & fDialogoFecha.Tag(1) & "'", Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes generados entre " & fDialogoFecha.Tag(0) & " y " & fDialogoFecha.Tag(1)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If
    End Sub





    Private Sub ConAdjuntosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConAdjuntosToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader

        Dim cIdEntradas As Collection
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()

        cIdEntradas = New Collection
        mLector = Consulta("SELECT distinct(identrada) FROM adjuntos", Conexion)
        Do While mLector.Read
            cIdEntradas.Add(mLector.Item("identrada"))
        Loop
        mLector.Close()
        fForma.Text = "Reportes que tienen archivos adjuntos"
        fForma.Tag = cIdEntradas
        fForma.Show()

        Conexion.Close()

    End Sub

    Private Sub SinAdjuntosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SinAdjuntosToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader

        Dim cIdEntradas As Collection
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()

        cIdEntradas = New Collection
        mLector = Consulta("SELECT id FROM entradas where id not in (select identrada from adjuntos)", Conexion)
        Do While mLector.Read
            cIdEntradas.Add(mLector.Item("id"))
        Loop
        mLector.Close()
        fForma.Text = "Reportes que no tienen archivos adjuntos"
        fForma.Tag = cIdEntradas
        fForma.Show()

        Conexion.Close()


    End Sub
    
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim fNuevaEntrada As Form2

       
        Dim bNecesarioGuardar As Boolean
        bNecesarioGuardar = False
        For Each fNuevaEntrada In cVentanasEditor
            If fNuevaEntrada.bAutoGuardar Then
                bNecesarioGuardar = True
            Else
                fNuevaEntrada.ToolStripStatusLabel6.Visible = False
            End If
        Next
        'If lVentanasEditorAnterior <> cVentanasEditor.Count Then
        'bNecesarioGuardar = True
        'lVentanasEditorAnterior = cVentanasEditor.Count
        'End If
        If bNecesarioGuardar Then
            AutoSave()
        End If
        If cVentanasEditor.Count = 0 Then
            'Timer1.Enabled = False
            BorrarAutosave()

        End If
    End Sub
    Public Sub AutoSave()
        Dim eNuevaEntrada As Entrada
        Dim fNuevaEntrada As Form2
        Dim cEntradas As New Collection
        Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Debug.Print("Autoguardando " & cVentanasEditor.Count)
        'bBorrarAutoSave = False
        Try
            For Each fNuevaEntrada In cVentanasEditor
                fNuevaEntrada.ToolStripStatusLabel6.Visible = True
                eNuevaEntrada.IdMaquina = fNuevaEntrada.iIdMaquina
                eNuevaEntrada.RTF = fNuevaEntrada.RichTextBox1.Rtf
                eNuevaEntrada.Adjuntos = fNuevaEntrada.cAdjuntos
                eNuevaEntrada.Referencias = fNuevaEntrada.cReferencias
                eNuevaEntrada.Responsables = fNuevaEntrada.cResponsables
                cEntradas.Add(eNuevaEntrada)
                fNuevaEntrada.bAutoGuardar = False
                Debug.Print("Autoguardar " & fNuevaEntrada.Handle.ToString)
            Next
            Dim fs As New System.IO.FileStream(sTempDir & iIdUsuario & "-autosave.bak", IO.FileMode.Create)
            bf.Serialize(fs, cEntradas)
            fs.Close()
        Catch

            Debug.Print("Error en autoguardar")
        End Try

    End Sub
    Private Sub AutoGuardarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoGuardarToolStripMenuItem.Click
        Dim iTemp As Integer
        iTemp = iAutoGuardarSegundos
        Do
            iTemp = Val(InputBox("Guardar automaticamente cada x segundos", "AutoGuardar", iTemp.ToString))
        Loop Until iTemp > 0
        iAutoGuardarSegundos = iTemp
    End Sub

    Private Sub MostrarUltimasEntradasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostrarUltimasEntradasToolStripMenuItem.Click
        Dim iTemp As Integer
        iTemp = lMesesACargar
        Do
            iTemp = Val(InputBox("Cargar automaticamente los ultimos x meses", "Mostrar entradas", lMesesACargar.ToString))
        Loop Until iTemp > 0
        lMesesACargar = iTemp
    End Sub

End Class
