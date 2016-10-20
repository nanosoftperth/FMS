Imports System.Xml.Serialization
Imports System.IO

Public Class MileageTester
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSWearch_Click(sender As Object, e As EventArgs) Handles btnSWearch.Click

        Dim answerText As String = String.Empty

        Try
            Dim vinNumber As String = txtVINNumber.Text
            Dim startDate As Date = CDate(dateEditStartDate.Value)
            Dim endDate As Date = CDate(dateEditEndDate.Value)

            Dim vnr As New FMS.ServiceAccess.WebServices.VINNumberRequest With {.VINNumber = vinNumber,
                                                                                .StartDate = startDate,
                                                                                .EndDate = endDate}

            Dim resp = FMS.ServiceAccess.Helper.GetVehicleData(vnr)

            Dim xmls As New XmlSerializer(resp.GetType)
            Dim sr As New StringWriter()

            xmls.Serialize(sr, resp)

            Dim serializedString As String = sr.ToString

            answerText = serializedString

        Catch ex As Exception
            answerText = ex.Message
        End Try


        memoAnswer.Enabled = True
        memoAnswer.Text = answerText
        memoAnswer.Enabled = False

    End Sub
End Class