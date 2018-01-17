Public Class ServiceRunManagement
    Inherits System.Web.UI.Page

#Region "Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub dteStart_ValueChanged(sender As Object, e As EventArgs)
        'Dim ListDates = GetRunDates(Me.dteStart.Value, Me.dteEnd.Value)
        'Me.gvServiceRun.DataBind()

    End Sub

    Protected Sub dteEnd_ValueChanged(sender As Object, e As EventArgs)
        'Dim ListDates = GetRunDates(Me.dteStart.Value, Me.dteEnd.Value)
        ' Me.gvServiceRun.DataBind()

    End Sub

#End Region

#Region "Methods"
    Public Shared Function GetRunDates(StartDate As Date, EndDate As Date) As List(Of RunDates)
        Dim dateCtr = DateDiff(DateInterval.Day, StartDate, EndDate.AddDays(1))

        Dim listDates As New List(Of RunDates)

        If (dateCtr > 0) Then

            For nRow = 0 To dateCtr - 1
                Dim row As New RunDates

                If (nRow = 0) Then
                    row.RunDate = StartDate
                Else
                    row.RunDate = StartDate.AddDays(nRow)
                End If

                listDates.Add(row)

            Next

        End If

        Return listDates
    End Function

#End Region

#Region "List"
    Public Class RunDates
        Public Property RunDate As Date

    End Class

#End Region

End Class