Public Class Form5
    Dim iIdEntrada As Integer
    Dim cReferenciaMaquinas As Collection
    Dim cReferenciEntradas As Collection
    Dim cResponsables As Collection
    Dim cAdjuntos As Collection


    Private Sub Form5_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose(True)
    End Sub

    Private Sub Form5_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GuardarPosicion(Me)
        MiSaveSetting("BitacoraMantenimiento", "Ver", "Zoom", RichTextBox1.ZoomFactor.ToString)

    End Sub
    Private Sub ActualizarMenuStrip()
        Try
            Select Case Me.Opacity.ToString("0.00")
                Case "1.00"
                    ToolStripMenuItem8.Checked = True
                    ToolStripMenuItem9.Checked = False
                    ToolStripMenuItem10.Checked = False
                    ToolStripMenuItem11.Checked = False
                Case "0.90"
                    ToolStripMenuItem8.Checked = False
                    ToolStripMenuItem9.Checked = True
                    ToolStripMenuItem10.Checked = False
                    ToolStripMenuItem11.Checked = False
                Case "0.80"
                    ToolStripMenuItem8.Checked = False
                    ToolStripMenuItem9.Checked = False
                    ToolStripMenuItem10.Checked = True
                    ToolStripMenuItem11.Checked = False
                Case "0.70"
                    ToolStripMenuItem8.Checked = False
                    ToolStripMenuItem9.Checked = False
                    ToolStripMenuItem10.Checked = False
                    ToolStripMenuItem11.Checked = True
                Case Else
                    ToolStripMenuItem8.Checked = False
                    ToolStripMenuItem9.Checked = False
                    ToolStripMenuItem10.Checked = False
                    ToolStripMenuItem11.Checked = False

            End Select
        Catch ex As Exception

        End Try
        ToolStripMenuItem2.Checked = False
        ToolStripMenuItem3.Checked = False
        ToolStripMenuItem4.Checked = False
        ToolStripMenuItem5.Checked = False
        ToolStripMenuItem6.Checked = False
        Select Case RichTextBox1.ZoomFactor
            Case 0.5
                ToolStripMenuItem2.Checked = True
            Case 0.75
                ToolStripMenuItem6.Checked = True
            Case 1
                ToolStripMenuItem3.Checked = True
            Case 1.5
                ToolStripMenuItem5.Checked = True
            Case 2
                ToolStripMenuItem4.Checked = True
        End Select

    End Sub
    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sSQL As String
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim iResponsable As Integer
        Dim iReferencia As Integer
        Dim iAdjunto As Integer
        Dim mComando As MySql.Data.MySqlClient.MySqlCommand

        mComando = New MySql.Data.MySqlClient.MySqlCommand
        Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection


        mConexion = NuevaConexion()



        ColocarForm(Me)
        iIdEntrada = Me.Tag
        cReferenciaMaquinas = New Collection
        cResponsables = New Collection
        cAdjuntos = New Collection
        RichTextBox1.ZoomFactor = Val(Replace(MiGetSetting("BitacoraMantenimiento", "Ver", "Zoom", "2"), ",", "."))
        ActualizarMenuStrip()

        mLector = Consulta("select idresponsable from responsables where identrada=" & iIdEntrada, mConexion)
        Do While mLector.Read
            cResponsables.Add(mLector.Item("idresponsable"))
        Loop
        mLector.Close()
        For Each iResponsable In cResponsables
            ToolStripSplitButton1.DropDownItems.Add(Obtener("select nombre from usuarios where id=" & iResponsable, "nombre", mConexion))
        Next iResponsable

        mLector = Consulta("select idmaquina from referenciamaquinas where identrada=" & iIdEntrada, mConexion)
        Do While mLector.Read
            cReferenciaMaquinas.Add(mLector.Item("idmaquina"))
        Loop
        mLector.Close()
        For Each iReferencia In cReferenciaMaquinas
            ToolStripSplitButton2.DropDownItems.Add(Obtener("select descripcion from maquinas where id=" & iReferencia, "descripcion", mConexion))
        Next iReferencia

        mLector = Consulta("select id from adjuntos where identrada=" & iIdEntrada, mConexion)
        Do While mLector.Read
            cAdjuntos.Add(mLector.Item("id"))
        Loop
        mLector.Close()
        For Each iAdjunto In cAdjuntos
            ToolStripSplitButton3.DropDownItems.Add(Obtener("select nombre from adjuntos where id=" & iAdjunto, "nombre", mConexion))
            ToolStripSplitButton3.DropDownItems.Item(ToolStripSplitButton3.DropDownItems.Count - 1).Tag = iAdjunto
        Next iAdjunto

        mLector = Consulta("select identrada from referenciaentradas where idreferencia=" & iIdEntrada, mConexion)
        Do While mLector.Read
            ToolStripSplitButton4.DropDownItems.Add(mLector.Item("identrada"))
        Loop
        mLector.Close()


        ToolStripSplitButton1.Text = cResponsables.Count & " Responsables"
        ToolStripSplitButton2.Text = cReferenciaMaquinas.Count & " Referencias"
        ToolStripSplitButton3.Text = cAdjuntos.Count & " Adjuntos"
        ToolStripSplitButton4.Text = ToolStripSplitButton4.DropDownItems.Count & " Relacionadas"

        Try
            mComando.Connection = mConexion
            sSQL = "insert into leidos values (0," & iIdEntrada & "," & iIdUsuario & ",now())"
            mComando.CommandText = sSQL
            mComando.ExecuteNonQuery()
        Catch mierror As MySql.Data.MySqlClient.MySqlException
            ReportarError(mierror.ToString)
        End Try
        mConexion.Close()
    End Sub


    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        RichTextBox1.ZoomFactor = 2
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        RichTextBox1.ZoomFactor = 1
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        RichTextBox1.ZoomFactor = 0.5
        ActualizarMenuStrip()
    End Sub


    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        Dim fForma As New Form2
        'cVentanasEditor.Add(fForma, fForma.Handle.ToString)
        Form1.Timer1.Enabled = True
        fForma.Tag = iIdEntrada
        fForma.Show()
    End Sub


    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        RichTextBox1.ZoomFactor = 1.5
        ActualizarMenuStrip()

    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        RichTextBox1.ZoomFactor = 0.75
        ActualizarMenuStrip()

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If RichTextBox1.ZoomFactor < 5 Then RichTextBox1.ZoomFactor = RichTextBox1.ZoomFactor + 0.1
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        If RichTextBox1.ZoomFactor > 0.1 Then RichTextBox1.ZoomFactor = RichTextBox1.ZoomFactor - 0.1
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        NuevoToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub AcercarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcercarToolStripMenuItem.Click
        ToolStripButton1.PerformClick()
    End Sub

    Private Sub AlejarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlejarToolStripMenuItem.Click
        ToolStripButton2.PerformClick()
    End Sub

    Private Sub ToolStripStatusLabel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel2.Click
        EspacioUtilizado(RichTextBox1)
    End Sub

    Private Sub ToolStripProgressBar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripProgressBar1.Click
        EspacioUtilizado(RichTextBox1)
    End Sub


    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged
        If Len(RichTextBox1.Rtf) >= 65535 Then
            MsgBox("El mensaje es demasiado largo.")
            ToolStripProgressBar1.Value = 65535
        Else
            ToolStripProgressBar1.Value = Len(RichTextBox1.Rtf)
        End If
    End Sub

    Private Sub ToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem11.Click
        Me.Opacity = 0.7
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        Me.Opacity = 0.8
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click
        Me.Opacity = 0.9
        ActualizarMenuStrip()
    End Sub

    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        Me.Opacity = 1
        ActualizarMenuStrip()
    End Sub


    Private Sub ToolStripSplitButton4_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStripSplitButton4.DropDownItemClicked
        AbrirEntrada(Val(e.ClickedItem.Text))
    End Sub

    Private Sub ToolStripSplitButton3_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStripSplitButton3.DropDownItemClicked
        Dim iIdAdjunto As Integer
        iIdAdjunto = Val(e.ClickedItem.Tag)
        AbrirAdjunto(iIdEntrada, iIdAdjunto)
    End Sub

    Private Sub CopiarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopiarToolStripMenuItem.Click
        'CopiarToolStripMenuItem.PerformClick()
        RichTextBox1.Copy()
    End Sub

    Private Sub CopiarToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopiarToolStripMenuItem1.Click
        RichTextBox1.Copy()
    End Sub

    Private Sub SeleccionarTodoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionarTodoToolStripMenuItem1.Click
        RichTextBox1.SelectAll()
    End Sub

    Private Sub SeleccionarTodoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionarTodoToolStripMenuItem.Click
        SeleccionarTodoToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub ToolStripSplitButton3_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSplitButton3.ButtonClick

    End Sub

    Private Sub ExportarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportarToolStripMenuItem.Click
        Dim sArchivo As String
        SaveFileDialog1.Filter = "Rich text file|*.rtf"
        SaveFileDialog1.FileName = Replace(Me.Text, ":", "_")
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.Cancel Then
        Else
            Try
                sArchivo = SaveFileDialog1.FileName
                RichTextBox1.SaveFile(sArchivo)
            Catch
            End Try
        End If
    End Sub
End Class