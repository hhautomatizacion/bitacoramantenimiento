Public Class Form11

 



    Private Sub Form11_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MonthCalendar1.SelectionEnd = Now
        MonthCalendar1.SelectionStart = Now

        Try
            MonthCalendar1.Font = New Font(sNombreFuente, iTamanioFuente)

        Catch ex As Exception

            MonthCalendar1.Font = New Font("ARIAL", 12)
        End Try
        Me.Tag = False
    End Sub

    Private Sub MonthCalendar1_DateSelected(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateSelected

        Me.Tag = Split(e.Start.ToString("yyyy-MM-dd") & " 00:00:00" & "|" & e.End.ToString("yyyy-MM-dd") & " 23:59:59", "|")
        Me.Close()
    End Sub

    Private Sub MonthCalendar1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MonthCalendar1.Resize
        Me.Width = Me.Width - (Me.ClientSize.Width - MonthCalendar1.Width)
        Me.Height = Me.Height - (Me.ClientSize.Height - MonthCalendar1.Height)
    End Sub



End Class