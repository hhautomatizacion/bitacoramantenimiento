Public Class Form2
    Dim sReferencia As String
    Public cAdjuntos As Collection
    Public cReferencias As Collection
    Public cResponsables As Collection
    Dim iIdEntradaReferencia As Integer
    Dim iIdEncargado As Integer
    Public iIdMaquina As Integer
    Dim iIdJefeArea As Integer
    Dim sFechaInicio As String
    Dim sFechaFin As String
    Public bModificado As Boolean
    Public bAutoGuardar As Boolean
    Dim sNombreFuente As String
    Dim sTamanioFuente As Single
    Dim bNegritaFuente As Boolean
    Dim bSubrayadoFuente As Boolean
    Dim bTachadoFuente As Boolean
    Dim iColorFuente As Integer
    Dim iColorFondo As Integer
    Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection




    Private Sub Form2_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = False
        If bModificado Then

            If MsgBox("Desea salir?  Se perderan los cambios!!!", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                e.Cancel = True
                Me.Visible = True
                bAutoGuardar = True
            Else
                bAutoGuardar = False
                Me.Visible = False
            End If
        End If
        GuardarPosicion(Me)
        MiSaveSetting("BitacoraMantenimiento", "Editar", "Zoom", RichTextBox1.ZoomFactor.ToString)
        MiSaveSetting("BitacoraMantenimiento", "Editar", "ColorFuente", iColorFuente)
        MiSaveSetting("BitacoraMantenimiento", "Editar", "ColorFondo", iColorFondo)
        If Not Me.Visible Then
            cVentanasEditor.Remove(Me.Handle.ToString)
            If cVentanasEditor.Count = 0 Then
                BorrarAutosave()
            End If

        End If

    End Sub

    Private Sub Form2_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleCreated
        cAdjuntos = New Collection
        cReferencias = New Collection
        cResponsables = New Collection
        cVentanasEditor.Add(Me, Me.Handle.ToString)
    End Sub
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Conexion = NuevaConexion()
        ColocarForm(Me)
        iIdEntradaReferencia = Val(Me.Tag)
        If iIdEntradaReferencia > 0 Then
            sReferencia = " (con referencia a entrada no. " & iIdEntradaReferencia & ")"
            iIdMaquina = Val(Obtener("select maquina from entradas where id=" & iIdEntradaReferencia, "maquina", Conexion))
        End If
        RichTextBox1.ZoomFactor = Val(Replace(MiGetSetting("BitacoraMantenimiento", "Editar", "Zoom", "1"), ",", "."))
        sNombreFuente = MiGetSetting("BitacoraMantenimiento", "Editar", "NombreFuente", "Arial")
        sTamanioFuente = Val(Replace(MiGetSetting("BitacoraMantenimiento", "Editar", "TamanioFuente", "10"), ",", "."))
        bNegritaFuente = -Val(MiGetSetting("BitacoraMantenimiento", "Editar", "NegritaFuente", "0"))
        bSubrayadoFuente = -Val(MiGetSetting("BitacoraMantenimiento", "Editar", "SubrayadoFuente", "0"))
        bTachadoFuente = -Val(MiGetSetting("BitacoraMantenimiento", "Editar", "TachadoFuente", "0"))
        iColorFuente = MiGetSetting("BitacoraMantenimiento", "Editar", "ColorFuente", Color.Black.ToArgb)
        iColorFondo = MiGetSetting("BitacoraMantenimiento", "Editar", "ColorFondo", Color.White.ToArgb)

        RichTextBox1.SelectionFont = New Font(sNombreFuente, sTamanioFuente)
        RichTextBox1.SelectionColor = Color.FromArgb(iColorFuente)
        RichTextBox1.SelectionBackColor = Color.FromArgb(iColorFondo)

        ActualizarMenuStrip()


        ActualizarStripStatus()
        'bModificado = False
        'Timer1.Interval = 10000
        'Timer1.Enabled = True

    End Sub
    Private Sub ActualizarStripStatus()
        Me.Text = Obtener("select descripcion from maquinas where id=" & iIdMaquina, "descripcion", Conexion) & sReferencia
        ToolStripStatusLabel1.Text = cResponsables.Count & " responsables"
        ToolStripStatusLabel2.Text = cReferencias.Count & " referencias"
        ToolStripStatusLabel3.Text = cAdjuntos.Count & " archivos adjuntos"
    End Sub

    Private Sub ArchivoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArchivoToolStripMenuItem1.Click
        Dim sFileName As String
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            For Each sFileName In OpenFileDialog1.FileNames
                If FileInUse(sFileName) Then
                    MsgBox("No se pudo adjuntar el archivo " & sFileName.ToUpper & " porque esta siendo utilizado por otro proceso.")
                Else
                    If FileSize(sFileName) <= iTamanioMaximoArchivo Then
                        cAdjuntos.Add(sFileName)
                    Else
                        MsgBox("El archivo '" & sFileName.ToUpper & "' es demasiado grande (" & FormatoBytes(FileSize(sFileName)) & "). (Tamaño maximo " & FormatoBytes(iTamanioMaximoArchivo) & ")")
                    End If
                End If
            Next sFileName
            ActualizarStripStatus()
            bModificado = True
            bAutoGuardar = True
        End If
    End Sub



    Private Sub GuardarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuardarToolStripMenuItem.Click
        Dim iTotalEntradas As Integer
        If Len(sFechaInicio) = 0 Then sFechaInicio = "0000-00-00 00:00:00"
        If Len(sFechaFin) = 0 Then sFechaFin = "0000-00-00 00:00:00"
        If cResponsables.Count = 0 Then cResponsables.Add(iIdUsuario)
        If Len(RichTextBox1.Text) > 0 Then
            If Len(RichTextBox1.Rtf) > 65535 Then
                MsgBox("El mensaje es demasiado grande. Si necesita incluir imagenes u otra informacion agregarla como archivo adjunto." & vbCrLf & "No se ha guardado el documento.")
            Else
                Me.Visible = False

                iTotalEntradas = Val(Obtener("select count(id) as entradas from entradas", "entradas", Conexion))
                Do
                    GuardarEntrada(iIdEntradaReferencia, iIdMaquina, RichTextBox1.Text, RichTextBox1.Rtf, iIdJefeArea, cResponsables, sFechaInicio, sFechaFin, cReferencias, cAdjuntos, Conexion)
                Loop Until iTotalEntradas <> Val(Obtener("select count(id) as entradas from entradas", "entradas", Conexion))
                'cVentanasEditor.Remove(Me.Handle.ToString)
                bModificado = False
                Form1.ActualizarUltimos()
                Me.Close()
            End If
        Else
            MsgBox("La descripcion esta vacia. No se ha guardado el documento")
        End If
    End Sub





    Private Sub ReferenciaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReferenciaToolStripMenuItem.Click
        Dim sReferencia As String
        Dim iReferencia As Integer
        Dim cColorAnterior As Color
        iReferencia = SeleccionarMaquina()
        If iReferencia > 0 Then
            bModificado = True
            bAutoGuardar = True
            cReferencias.Add(iReferencia)
            sReferencia = Obtener("select descripcion from maquinas where id=" & iReferencia, "descripcion", Conexion)
            cColorAnterior = RichTextBox1.SelectionColor
            RichTextBox1.SelectionColor = Color.Red
            RichTextBox1.AppendText("[" & sReferencia & "]")
            RichTextBox1.SelectionColor = cColorAnterior
            ActualizarStripStatus()
        End If
    End Sub

    Private Sub ReferenciaPrincipalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReferenciaPrincipalToolStripMenuItem.Click
        Dim iTemp As Integer
        iTemp = SeleccionarMaquina()
        If iTemp > 0 Then
            iIdMaquina = iTemp
            bModificado = True
            bAutoGuardar = True
            ActualizarStripStatus()
        End If
    End Sub





    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        If bModificado Then
            If MsgBox("Quieres salir sin guardar los cambios?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            Else
                bModificado = False
            End If
        End If
        Me.Close()
    End Sub
    Private Sub ColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorToolStripMenuItem.Click
        ColorDialog1.Color = RichTextBox1.SelectionColor
        ColorDialog1.ShowDialog()
        RichTextBox1.SelectionColor = ColorDialog1.Color
        iColorFuente = ColorDialog1.Color.ToArgb
    End Sub

    Private Sub FuenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FuenteToolStripMenuItem.Click
        FontDialog1.Font = RichTextBox1.SelectionFont
        If FontDialog1.ShowDialog() <> Windows.Forms.DialogResult.Cancel Then
            Try
                RichTextBox1.SelectionFont = FontDialog1.Font
                sNombreFuente = FontDialog1.Font.Name
                sTamanioFuente = FontDialog1.Font.Size.ToString
                bNegritaFuente = FontDialog1.Font.Bold
                bSubrayadoFuente = FontDialog1.Font.Underline
                bTachadoFuente = FontDialog1.Font.Strikeout

                MiSaveSetting("BitacoraMantenimiento", "Editar", "NombreFuente", sNombreFuente)
                MiSaveSetting("BitacoraMantenimiento", "Editar", "TamanioFuente", sTamanioFuente)
                MiSaveSetting("BitacoraMantenimiento", "Editar", "NegritaFuente", -Val(bNegritaFuente))
                MiSaveSetting("BitacoraMantenimiento", "Editar", "SubrayadoFuente", -Val(bSubrayadoFuente))
                MiSaveSetting("BitacoraMantenimiento", "Editar", "TachadoFuente", -Val(bTachadoFuente))


            Catch
            End Try
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        RichTextBox1.ZoomFactor = 1
        ActualizarMenuStrip()
    End Sub
    Private Sub ActualizarMenuStrip()
        Try
            If RichTextBox1.CanUndo Then
                DeshacerToolStripMenuItem.Enabled = True
                ToolStripButton11.Enabled = True
            Else
                DeshacerToolStripMenuItem.Enabled = False
                ToolStripButton11.Enabled = False
            End If
            If RichTextBox1.CanRedo Then
                RehacerToolStripMenuItem.Enabled = True
                ToolStripButton12.Enabled = True
            Else
                RehacerToolStripMenuItem.Enabled = False
                ToolStripButton12.Enabled = False
            End If
            If RichTextBox1.SelectionBullet Then
                ToolStripButton16.Checked = True
                ViñetasToolStripMenuItem.Checked = True
            Else
                ToolStripButton16.Checked = False
                PegarToolStripMenuItem.Checked = False
            End If
        Catch
        End Try
        Try
            Select Case Me.Opacity.ToString("0.00")
                Case "1.00"
                    ToolStripMenuItem12.Checked = True
                    ToolStripMenuItem13.Checked = False
                    ToolStripMenuItem14.Checked = False
                    ToolStripMenuItem15.Checked = False
                Case "0.90"
                    ToolStripMenuItem12.Checked = False
                    ToolStripMenuItem13.Checked = True
                    ToolStripMenuItem14.Checked = False
                    ToolStripMenuItem15.Checked = False
                Case "0.80"
                    ToolStripMenuItem12.Checked = False
                    ToolStripMenuItem13.Checked = False
                    ToolStripMenuItem14.Checked = True
                    ToolStripMenuItem15.Checked = False
                Case "0.70"
                    ToolStripMenuItem12.Checked = False
                    ToolStripMenuItem13.Checked = False
                    ToolStripMenuItem14.Checked = False
                    ToolStripMenuItem15.Checked = True
                Case Else
                    ToolStripMenuItem12.Checked = False
                    ToolStripMenuItem13.Checked = False
                    ToolStripMenuItem14.Checked = False
                    ToolStripMenuItem15.Checked = False

            End Select
        Catch ex As Exception

        End Try
        Try
            If RichTextBox1.CanUndo Then DeshacerToolStripMenuItem.Text = "Deshacer " & RichTextBox1.UndoActionName Else RehacerToolStripMenuItem.Text = "Deshacer"
            If RichTextBox1.CanRedo Then RehacerToolStripMenuItem.Text = "Rehacer " & RichTextBox1.RedoActionName Else RehacerToolStripMenuItem.Text = "Rehacer"
        Catch
        End Try
        Try
            If RichTextBox1.SelectionAlignment = HorizontalAlignment.Left Then
                ToolStripButton13.Checked = True
                IzquierdaToolStripMenuItem.Checked = True
            Else
                ToolStripButton13.Checked = False
                IzquierdaToolStripMenuItem.Checked = False
            End If

            If RichTextBox1.SelectionAlignment = HorizontalAlignment.Center Then
                ToolStripButton14.Checked = True
                CentroToolStripMenuItem.Checked = True
            Else
                ToolStripButton14.Checked = False
                CentroToolStripMenuItem.Checked = False
            End If
            If RichTextBox1.SelectionAlignment = HorizontalAlignment.Right Then
                ToolStripButton15.Checked = True
                DerechaToolStripMenuItem.Checked = True
            Else
                ToolStripButton15.Checked = False
                DerechaToolStripMenuItem.Checked = False
            End If
        Catch
        End Try
        Try
            ToolStripMenuItem1.Checked = False
            ToolStripMenuItem2.Checked = False
            ToolStripMenuItem3.Checked = False
            ToolStripMenuItem4.Checked = False
            ToolStripMenuItem6.Checked = False

            Select Case RichTextBox1.ZoomFactor
                Case 0.5
                    ToolStripMenuItem4.Checked = True
                Case 0.75
                    ToolStripMenuItem6.Checked = True
                Case 1
                    ToolStripMenuItem2.Checked = True
                Case 1.5
                    ToolStripMenuItem1.Checked = True
                Case 2
                    ToolStripMenuItem3.Checked = True
            End Select
        Catch
        End Try
        Try
            ToolStripButton1.Checked = RichTextBox1.SelectionFont.Bold
            ToolStripButton2.Checked = RichTextBox1.SelectionFont.Italic
            ToolStripButton3.Checked = RichTextBox1.SelectionFont.Underline
            ToolStripButton4.Checked = RichTextBox1.SelectionFont.Strikeout
        Catch
        End Try

    End Sub
    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        RichTextBox1.ZoomFactor = 2
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        RichTextBox1.ZoomFactor = 0.5
        ActualizarMenuStrip()
    End Sub

    Private Sub RTFToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        RichTextBox1.ZoomFactor = 1.5
        ActualizarMenuStrip()
    End Sub

    Private Sub IzquierdaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IzquierdaToolStripMenuItem.Click
        RichTextBox1.SelectionAlignment = HorizontalAlignment.Left
        ActualizarMenuStrip()
    End Sub

    Private Sub CentroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CentroToolStripMenuItem.Click
        RichTextBox1.SelectionAlignment = HorizontalAlignment.Center
        ActualizarMenuStrip()
    End Sub

    Private Sub DerechaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DerechaToolStripMenuItem.Click
        RichTextBox1.SelectionAlignment = HorizontalAlignment.Right
        ActualizarMenuStrip()
    End Sub

    Private Sub ResponsableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResponsableToolStripMenuItem.Click
        Dim iResponsable As Integer
        iResponsable = SeleccionarResponsable()
        If iResponsable > 0 Then
            bModificado = True
            bAutoGuardar = True
            cResponsables.Add(iResponsable)
            ActualizarStripStatus()
        End If
    End Sub


    Private Sub ToolStripStatusLabel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel3.Click
        Dim fForma As New Form9
        fForma.Tag = cAdjuntos
        fForma.ShowDialog()
        cAdjuntos = fForma.Tag
        ActualizarStripStatus()
    End Sub

    Private Sub ToolStripStatusLabel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel2.Click
        Dim fForma As New Form8
        fForma.Tag = cReferencias
        fForma.ShowDialog()
        cReferencias = fForma.Tag
        ActualizarStripStatus()
    End Sub

    Private Sub ToolStripStatusLabel1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel1.Click
        Dim fForma As New Form7
        fForma.Tag = cResponsables
        fForma.ShowDialog()
        cResponsables = fForma.Tag
        ActualizarStripStatus()

    End Sub

    Private Sub RichTextBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles RichTextBox1.DragDrop
        Dim sFileName As String
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.None
            For Each sFileName In e.Data.GetData(DataFormats.FileDrop)
                If FileInUse(sFileName) Then
                    MsgBox("No se pudo adjuntar el archivo " & sFileName.ToUpper & " porque esta siendo utilizado por otro proceso.")
                Else
                    If FileSize(sFileName) <= iTamanioMaximoArchivo Then
                        cAdjuntos.Add(sFileName)
                    Else
                        MsgBox("El archivo '" & sFileName.ToUpper & "' es demasiado grande (" & FormatoBytes(FileSize(sFileName)) & "). (Tamaño maximo " & FormatoBytes(iTamanioMaximoArchivo) & ")")
                    End If
                End If
            Next sFileName
            ActualizarStripStatus()
            bModificado = True
            bAutoGuardar = True

        End If
    End Sub

    Private Sub RichTextBox1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBox1.SelectionChanged
        ActualizarMenuStrip()
    End Sub


    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged
        bModificado = True
        bAutoGuardar = True
        If Len(RichTextBox1.Rtf) >= 65535 Then
            MsgBox("El mensaje es demasiado largo.")
            ToolStripProgressBar1.Value = 65535
        Else
            ToolStripProgressBar1.Value = Len(RichTextBox1.Rtf)
        End If
    End Sub

    Private Sub FechaInicioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        RichTextBox1.ZoomFactor = 0.75
        ActualizarMenuStrip()
    End Sub

    Private Sub DeshacerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeshacerToolStripMenuItem.Click
        RichTextBox1.Undo()
        ActualizarMenuStrip()
    End Sub

    Private Sub CotarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CotarToolStripMenuItem.Click, CortarToolStripMenuItem.Click
        RichTextBox1.Cut()
        ActualizarMenuStrip()
    End Sub

    Private Sub CopiarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopiarToolStripMenuItem.Click, CopiarToolStripMenuItem1.Click
        RichTextBox1.Copy()
        ActualizarMenuStrip()
    End Sub

    Private Sub PegarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PegarToolStripMenuItem.Click, PegarToolStripMenuItem1.Click
        RichTextBox1.Paste()
        ActualizarMenuStrip()
    End Sub

    Private Sub SleccionarTodoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SleccionarTodoToolStripMenuItem.Click
        RichTextBox1.SelectAll()
    End Sub

    Private Sub ViñetasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViñetasToolStripMenuItem.Click
        RichTextBox1.SelectionBullet = Not RichTextBox1.SelectionBullet
        ActualizarMenuStrip()
    End Sub

    Private Sub RehacerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RehacerToolStripMenuItem.Click
        RichTextBox1.Redo()
        ActualizarMenuStrip()
    End Sub




    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        NegritaToolStripMenuItem.PerformClick()

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        CursivaToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        SubrayadoToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        TachadoToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        If RichTextBox1.ZoomFactor < 5 Then RichTextBox1.ZoomFactor = RichTextBox1.ZoomFactor + 0.1
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        If RichTextBox1.ZoomFactor > 0.1 Then RichTextBox1.ZoomFactor = RichTextBox1.ZoomFactor - 0.1
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton10.Click
        GuardarToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        CotarToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton8.Click
        CopiarToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click
        PegarToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ToolStripButton16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton16.Click
        ViñetasToolStripMenuItem.PerformClick()

    End Sub

    Private Sub ToolStripButton13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton13.Click
        IzquierdaToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ToolStripButton14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton14.Click
        CentroToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ToolStripButton15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton15.Click
        DerechaToolStripMenuItem_Click(sender, e)
    End Sub


    Private Sub AcercarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcercarToolStripMenuItem.Click
        ToolStripButton5.PerformClick()

    End Sub

    Private Sub AlejarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlejarToolStripMenuItem.Click
        ToolStripButton6.PerformClick()
    End Sub

    Private Sub ToolStripButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton11.Click
        DeshacerToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton12.Click
        RehacerToolStripMenuItem.PerformClick()
    End Sub

    Private Sub NegritaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NegritaToolStripMenuItem.Click

        Dim ExistingFont As Font = RichTextBox1.SelectionFont
        Dim NewFontStyle As New FontStyle
        With ExistingFont
            NewFontStyle = -((Not .Bold) * FontStyle.Bold)
            NewFontStyle += -(.Italic * FontStyle.Italic)
            NewFontStyle += -(.Underline * FontStyle.Underline)
            NewFontStyle += -(.Strikeout * FontStyle.Strikeout)
            RichTextBox1.SelectionFont = New Font(ExistingFont, NewFontStyle)
        End With

        ActualizarMenuStrip()

    End Sub

    Private Sub CursivaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CursivaToolStripMenuItem.Click
        Dim ExistingFont As Font = RichTextBox1.SelectionFont
        Dim NewFontStyle As New FontStyle
        With ExistingFont
            NewFontStyle = -(.Bold * FontStyle.Bold)
            NewFontStyle += -((Not .Italic) * FontStyle.Italic)
            NewFontStyle += -(.Underline * FontStyle.Underline)
            NewFontStyle += -(.Strikeout * FontStyle.Strikeout)
            RichTextBox1.SelectionFont = New Font(ExistingFont, NewFontStyle)
        End With

        ActualizarMenuStrip()
    End Sub

    Private Sub SubrayadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubrayadoToolStripMenuItem.Click
        Dim ExistingFont As Font = RichTextBox1.SelectionFont
        Dim NewFontStyle As New FontStyle
        With ExistingFont
            NewFontStyle = -(.Bold * FontStyle.Bold)
            NewFontStyle += -(.Italic * FontStyle.Italic)
            NewFontStyle += -((Not .Underline) * FontStyle.Underline)
            NewFontStyle += -(.Strikeout * FontStyle.Strikeout)
            RichTextBox1.SelectionFont = New Font(ExistingFont, NewFontStyle)
        End With
        ActualizarMenuStrip()
    End Sub

    Private Sub TachadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TachadoToolStripMenuItem.Click
        Dim ExistingFont As Font = RichTextBox1.SelectionFont
        Dim NewFontStyle As New FontStyle
        With ExistingFont
            NewFontStyle = -(.Bold * FontStyle.Bold)
            NewFontStyle += -(.Italic * FontStyle.Italic)
            NewFontStyle += -(.Underline * FontStyle.Underline)
            NewFontStyle += -((Not .Strikeout) * FontStyle.Strikeout)
            RichTextBox1.SelectionFont = New Font(ExistingFont, NewFontStyle)
        End With

        ActualizarMenuStrip()

    End Sub

    Private Sub ToolStripButton20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton20.Click
        ArchivoToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub ToolStripButton19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton19.Click
        ResponsableToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton18.Click
        ReferenciaToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton17.Click
        ReferenciaPrincipalToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ColorRojoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorRojoToolStripMenuItem.Click
        RichTextBox1.SelectionColor = Color.Red
        iColorFuente = RichTextBox1.SelectionColor.ToArgb
    End Sub

    Private Sub ColorNegroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorNegroToolStripMenuItem.Click
        RichTextBox1.SelectionColor = Color.Black
        iColorFuente = RichTextBox1.SelectionColor.ToArgb
    End Sub

    Private Sub ToolStripProgressBar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripProgressBar1.Click
        EspacioUtilizado(RichTextBox1)
    End Sub

    Private Sub ToolStripStatusLabel4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel4.Click
        EspacioUtilizado(RichTextBox1)
    End Sub

    Private Sub ColorDeFondoAmarilloToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorDeFondoAmarilloToolStripMenuItem.Click
        RichTextBox1.SelectionBackColor = Color.Yellow
        iColorFondo = RichTextBox1.SelectionBackColor.ToArgb
    End Sub

    Private Sub ColorDeFondoBlancoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorDeFondoBlancoToolStripMenuItem.Click
        RichTextBox1.SelectionBackColor = Color.White
        iColorFondo = RichTextBox1.SelectionBackColor.ToArgb
    End Sub

    Private Sub ColorDeFondoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorDeFondoToolStripMenuItem.Click
        ColorDialog1.Color = RichTextBox1.SelectionBackColor
        ColorDialog1.ShowDialog()
        RichTextBox1.SelectionBackColor = ColorDialog1.Color
        iColorFondo = ColorDialog1.Color.ToArgb
    End Sub






    Private Sub ToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem12.Click
        Me.Opacity = 1
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        Me.Opacity = 0.9
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        Me.Opacity = 0.8
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        Me.Opacity = 0.7
        ActualizarMenuStrip()
    End Sub


    Private Sub FormatoPorDefectoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormatoPorDefectoToolStripMenuItem.Click
        RichTextBox1.SelectionBackColor = Color.White
        RichTextBox1.SelectionColor = Color.Black
        RichTextBox1.SelectionAlignment = HorizontalAlignment.Left
        RichTextBox1.SelectionBullet = False
        RichTextBox1.SelectionFont = New Font("Arial", 12, FontStyle.Regular)
        ActualizarMenuStrip()
    End Sub

    Private Sub SeleccionarTodoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionarTodoToolStripMenuItem.Click
        SleccionarTodoToolStripMenuItem.PerformClick()
    End Sub
End Class