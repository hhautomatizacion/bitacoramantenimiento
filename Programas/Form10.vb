Public Class Form10
    Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
    Dim ConexionAuxiliar As New MySql.Data.MySqlClient.MySqlConnection
    Dim cEntradas As Collection
    Dim bEnterPresionado As Boolean
    Dim sCronometro As New System.Diagnostics.Stopwatch
    Dim iPorcentajeAnterior As Integer
    Dim iRestanteAnterior As Integer

    Private Sub Form10_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Conexion.Close()
        GuardarPosicion(Me)
        GuardarAnchoColumnas(Me, DataGridView1)

    End Sub
    Private Sub ColorearRenglon(ByVal Renglon As Integer, ByVal Color As Integer)
        DataGridView1.Item(0, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
        DataGridView1.Item(1, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
        DataGridView1.Item(2, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
        DataGridView1.Item(3, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
        DataGridView1.Item(4, Renglon).Style.BackColor = Drawing.Color.FromArgb(Color)
    End Sub

    Private Sub Form10_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Conexion = NuevaConexion()
        ConexionAuxiliar = NuevaConexion()

        ColocarForm(Me)
        CargarAnchoColumnas(Me, DataGridView1)
        Label1.Font = New Font(sNombreFuente, itamaniofuente)
        cEntradas = Me.Tag

        BuscarToolStripMenuItem.Enabled = False
        If cEntradas.Count > 0 Then
            ToolStripStatusLabel1.Text = "Entradas encontradas: " & cEntradas.Count.ToString
            DataGridView1.Visible = False
            Label1.Visible = True
            sCronometro.Start()
            BackgroundWorker1.RunWorkerAsync()
        Else
            Label1.Text = "No se encontraron resultados"
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim sIdEntrada As String
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim iContador As Integer
        Dim sSQL As String
        Dim dFecha As Date
        Dim iDiaAnterior As Integer
        Dim bColorear As Boolean
        Dim iPorcentaje As Integer

        For Each sIdEntrada In cEntradas
            Dim sColumnas(3) As String
            Dim rRenglonNuevo As New Renglon
            sSQL = "select id,fecha,maquina,descripcion from entradas where id=" & sIdEntrada
            mLector = Consulta(sSQL, Conexion)
            If mLector.Read Then
                rRenglonNuevo.Id = iContador + 1
                dFecha = CDate(mLector.Item("fecha"))
                sColumnas(0) = mLector.Item("id").ToString
                sColumnas(1) = dFecha.ToString("yyyy-MM-dd HH:mm:ss")
                sColumnas(2) = Obtener("select descripcion from maquinas where id=" & mLector.Item("maquina"), "descripcion", ConexionAuxiliar)
                sColumnas(3) = mLector.Item("descripcion")

                rRenglonNuevo.Celdas = sColumnas
                If dFecha.DayOfYear = iDiaAnterior Then
                Else
                    bColorear = Not bColorear
                    iDiaAnterior = dFecha.DayOfYear
                End If
                rRenglonNuevo.Colorear = bColorear
                Dim iTemp As Integer
                iTemp = Val(Obtener("select count(identrada) as archivos_adjuntos from adjuntos where identrada =" & mLector.Item("id") & " group by identrada", "archivos_adjuntos", ConexionAuxiliar))
                If iTemp > 0 Then
                    rRenglonNuevo.TieneAdjuntos = True
                    rRenglonNuevo.Adjuntos = iTemp
                End If
                iTemp = Val(Obtener("select identrada from leidos where identrada =" & mLector.Item("id") & " and idusuario=" & iIdUsuario & " ", "identrada", ConexionAuxiliar))
                If iTemp > 0 Then
                    rRenglonNuevo.Leido = True
                End If

                iContador = iContador + 1
                iPorcentaje = ((iContador / cEntradas.Count) * 100)
                BackgroundWorker1.ReportProgress(iPorcentaje, rRenglonNuevo)


            End If
            mLector.Close()

        Next sIdEntrada

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        Dim rRenglonNuevo As New Renglon

        Dim iPorcentajeProgreso As Integer
        Dim iElapsedSeconds As Integer
        Dim iRestante As Integer

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

        'ToolStripProgressBar1.Value = e.ProgressPercentage
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

    End Sub


    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ConexionAuxiliar.Close()
        If DataGridView1.RowCount > 0 And cEntradas.Count > 0 Then
            ToolStripProgressBar1.Value = 0
            BuscarToolStripMenuItem.Enabled = True
            DataGridView1.Item(0, DataGridView1.RowCount - 1).Selected = True
            DataGridView1.CurrentCell = DataGridView1.Item(0, DataGridView1.RowCount - 1)
            DataGridView1.FirstDisplayedCell = DataGridView1.Item(0, DataGridView1.RowCount - 1)
        Else
            Label1.Text = "No se encontraron resultados"
        End If
        sCronometro.Stop()
        Debug.Print("Form10_BackgroundWorker1:".PadRight(25, " ") & sCronometro.Elapsed.ToString)
        sCronometro.Reset()
        Label1.Visible = False
        DataGridView1.Visible = True


    End Sub
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim iId As Integer
        If e.RowIndex >= 0 Then
            iId = Val(DataGridView1.Item(0, e.RowIndex).Value)
            If iId >= 0 Then
                AbrirEntrada(iId)
            End If
        End If
    End Sub

    Private Sub PorMaquinaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorMaquinaToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim iMaquina As Integer
        Dim cIdEntradas As Collection
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        cIdEntradas = New Collection
        iMaquina = SeleccionarMaquina()
        If iMaquina > 0 Then
            mLector = Consulta("select id from entradas where maquina=" & iMaquina & " and id in (" & CollectionJoin(cEntradas) & ")", Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes de la maquina " & Obtener("select descripcion from maquinas where id=" & iMaquina, "descripcion", Conexion)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If
    End Sub

    Private Sub PorContenidoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorContenidoToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim sCadenaBusqueda As String
        Dim cIdEntradas As Collection
        cIdEntradas = New Collection
        sCadenaBusqueda = InputBox("Palabra a buscar dentro del cuerpo del mensaje:")
        If Len(sCadenaBusqueda) > 0 Then
            sCadenaBusqueda = sCadenaBusqueda.Replace("'", "")
            mLector = Consulta("SELECT id FROM entradas where UPPER(CONVERT(CONTENIDO USING UTF8)) like '%" & sCadenaBusqueda.ToUpper & "%' and id in (" & CollectionJoin(cEntradas) & ")", Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes que contienen '" & sCadenaBusqueda & "' en el cuerpo del mensaje"
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If
    End Sub

    Private Sub PorFechaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorFechaToolStripMenuItem.Click
        Dim fDialogoFecha As New Form11
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim cIdEntradas As Collection
        fDialogoFecha.ShowDialog()
        If fDialogoFecha.Tag.GetType.IsArray Then
            cIdEntradas = New Collection
            mLector = Consulta("SELECT id FROM entradas where fecha between '" & fDialogoFecha.Tag(0) & "' and '" & fDialogoFecha.Tag(1) & "' and id in (" & CollectionJoin(cEntradas) & ")", Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes generados entre " & fDialogoFecha.Tag(0) & " y " & fDialogoFecha.Tag(1)
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

        iUsuario = SeleccionarResponsable()
        If iUsuario > 0 Then
            mLector = Consulta("SELECT id FROM entradas where usuario=" & iUsuario & " and id in (" & CollectionJoin(cEntradas) & ")", Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes realizados por " & Obtener("select nombre from usuarios where id=" & iUsuario, "nombre", Conexion)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If
    End Sub

    Private Sub PorResponsableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorResponsableToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim iResponsable As Integer
        Dim cIdEntradas As Collection
        cIdEntradas = New Collection
        iResponsable = SeleccionarResponsable()
        If iResponsable > 0 Then
            mLector = Consulta("select identrada as id from responsables where idresponsable=" & iResponsable & " and id in (" & CollectionJoin(cEntradas) & ")", Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes de trabajos realizados por " & Obtener("select nombre from usuarios where id=" & iResponsable, "nombre", Conexion)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If

    End Sub

    Private Sub PorReferenciaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorReferenciaToolStripMenuItem.Click
        Dim fForma As New Form10
        Dim iReferencia As Integer
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim cIdEntradas As Collection
        cIdEntradas = New Collection
        iReferencia = SeleccionarMaquina()
        If iReferencia > 0 Then
            mLector = Consulta("SELECT identrada as id FROM referenciamaquinas where idmaquina=" & iReferencia & " and id in (" & CollectionJoin(cEntradas) & ")", Conexion)
            Do While mLector.Read
                cIdEntradas.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            fForma.Text = "Reportes con referencia a la maquina " & Obtener("select descripcion from maquinas where id=" & iReferencia, "descripcion", Conexion)
            fForma.Tag = cIdEntradas
            fForma.Show()
        End If

    End Sub

   

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub AbrirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirToolStripMenuItem.Click
        Dim iID As Integer
        Dim iItem As Integer
        For iItem = 0 To DataGridView1.SelectedRows.Count - 1
            iID = (DataGridView1.Item(0, DataGridView1.SelectedRows.Item(iItem).Index).Value)
            ColorearRenglon(DataGridView1.SelectedRows.Item(iItem).Index, iColorLeido)
            AbrirEntrada(iID)
        Next
    End Sub

    Private Sub ConAdjuntosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConAdjuntosToolStripMenuItem.Click


        Dim fForma As New Form10
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader

        Dim cIdEntradas As Collection
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()

        cIdEntradas = New Collection
        mLector = Consulta("SELECT distinct(identrada) FROM adjuntos where identrada in (" & CollectionJoin(cEntradas) & ")", Conexion)
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
        mLector = Consulta("SELECT id FROM entradas where id not in (select identrada from adjuntos) and id in (" & CollectionJoin(cEntradas) & ")", Conexion)
        Do While mLector.Read
            cIdEntradas.Add(mLector.Item("id"))
        Loop
        mLector.Close()
        fForma.Text = "Reportes que no tienen archivos adjuntos"
        fForma.Tag = cIdEntradas
        fForma.Show()

        Conexion.Close()



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
        If e.KeyCode = Keys.Escape Then
            e.Handled = True
            Me.Close()
        End If
    End Sub
  
   
End Class