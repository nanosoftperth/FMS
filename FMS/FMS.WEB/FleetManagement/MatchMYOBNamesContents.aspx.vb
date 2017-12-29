Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Public Class MatchMYOBNamesContents
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Thread.Sleep(3000)

        '--- Get parameters
        Dim blnUpdateNamesChecked = Request.QueryString("UpdateCustNameChecked")
        Dim strMYOBFilename = Request.QueryString("MYOBFilename")

        '--- Delete Cust and tblMYOBMatch records (commented from now)
        'FMS.Business.DataObjects.CUST.DeleteAll()
        FMS.Business.DataObjects.tblMYOBMatch.DeleteAll()

        '--- Get List from Cust table (temp table to hold list of customer from Text File)
        ' Note: Text file import operation will still need to discuss for better approach or implementation
        '<place the textfile import code here>

        '--- Update customer table based on card from CUST table if cbxUpdCustNames = true
        ' Note: For now it was set to change only one record for testing. Need to remove that in SQL stored procedure
        If (blnUpdateNamesChecked = True) Then
            Dim blnCustUpdated = FMS.Business.DataObjects.CUST.UpdateCustomerBasedOnCardID()

            'If (blnCustUpdated = True) Then
            '    lblRecAdded.Text = "Customer List Updated!"
            '    lblRecAdded.Visible = True
            'End If

        End If

        '--- Get records from Cust that matches tblCustomers
        Dim qryMatch = FMS.Business.DataObjects.CUST.GetAllExistInTableCustomer()

        '--- Save MYOBCustomerFileName (control: txtMYOBFilename) value to tblParameters if not exist and update if it is
        ' Note: Need this to discuss for for better approach or implementation
        Dim oParam = FMS.Business.DataObjects.tblParameters.GetAll().Where(Function(p) p.ParId = "MYOBCustomerFileName").ToList()
        Dim rParam As New FMS.Business.DataObjects.tblParameters

        If (oParam.Count > 0) Then
            rParam.ParId = oParam.FirstOrDefault.ParId
            rParam.Field1 = strMYOBFilename
            FMS.Business.DataObjects.tblParameters.Update(rParam)
        Else
            rParam.ParId = "MYOBCustomerFileName"
            rParam.Field1 = strMYOBFilename
            FMS.Business.DataObjects.tblParameters.Create(rParam)
        End If

        '--- Save Matched MYOB
        Dim listMYOB As New List(Of FMS.Business.DataObjects.tblMYOBMatch)

        For Each rMYOB In qryMatch
            Dim rowMYOB As New FMS.Business.DataObjects.tblMYOBMatch
            rowMYOB.MYOBId = rMYOB.MYOBCustomerNumber
            rowMYOB.CustomerName = rMYOB.CustomerName
            rowMYOB.ImportedCustomerName = rMYOB.CustomerName

            listMYOB.Add(rowMYOB)
        Next

        FMS.Business.DataObjects.tblMYOBMatch.CreateAll(listMYOB)

        '--- Bind MYOB Match List
        Dim rpt As New FMS.ReportLogic.MYOBMatchList()
        Me.docvwer.Report = rpt


    End Sub

End Class