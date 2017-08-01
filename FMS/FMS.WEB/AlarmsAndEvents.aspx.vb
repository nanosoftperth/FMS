Imports DevExpress.Web
Imports System.Web.UI
Public Class AlarmsAndEvents
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dgvEventConfiguration.StartEdit(2)
    End Sub

    Protected Sub ASPxGridView1_RowValidating(sender As Object, e As Data.ASPxDataValidationEventArgs)
        For Each column As GridViewColumn In ASPxGridView1.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If
            If (dataColumn.FieldName.Equals("VehicleID") And e.NewValues(dataColumn.FieldName) Is Nothing) Or _
                          (dataColumn.FieldName.Equals("Metric") And e.NewValues(dataColumn.FieldName) Is Nothing) Or _
                          (dataColumn.FieldName.Equals("QueryType") And e.NewValues(dataColumn.FieldName) Is Nothing) Or _
                          (dataColumn.FieldName.Equals("TextValue") And e.NewValues(dataColumn.FieldName) Is Nothing) Then
                e.RowError = dataColumn.FieldName & " field Value can't be null."
                Exit For
            End If
        Next column
        If e.NewValues("QueryType") IsNot Nothing AndAlso (Not e.NewValues("QueryType").ToString().Equals("=") And Not e.NewValues("QueryType").ToString().Equals("Like")) Then
            Dim intVal As Integer = 0
            Dim dblVal As Double = 0
            If Not e.NewValues("TextValue") Is Nothing Then
                If Not Integer.TryParse(e.NewValues("TextValue").ToString(), intVal) Then
                    AddError(e.Errors, ASPxGridView1.Columns("TextValue"), "Text value should not be string.")
                ElseIf Not Double.TryParse(e.NewValues("TextValue").ToString(), dblVal) Then
                    AddError(e.Errors, ASPxGridView1.Columns("TextValue"), "Text value should not be string.")
                End If
            End If

        End If

    End Sub
    Private Sub AddError(ByVal errors As Dictionary(Of GridViewColumn, String), ByVal column As GridViewColumn, ByVal errorText As String)
        If errors.ContainsKey(column) Then
            Return
        End If
        errors(column) = errorText
    End Sub

    'Protected Sub ASPxGridView1_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs)
    '    If (Not ASPxGridView1.IsNewRowEditing) Then
    '        ASPxGridView1.DoRowValidation()
    '    End If
    'End Sub
End Class