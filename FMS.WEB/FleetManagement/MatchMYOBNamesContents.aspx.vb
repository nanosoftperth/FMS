Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Threading
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Public Class MatchMYOBNamesContents
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Thread.Sleep(3000)

        If Not (IsPostBack) Then
            '--- Get parameters
            Dim blnUpdateNamesChecked = Request.QueryString("UpdateCustNameChecked")
            Dim strMYOBFilename = Request.QueryString("MYOBFilename")

            'Dim rootPath As String = Server.MapPath("~/")
            'Dim outputPath As String = strMYOBFilename.Replace(rootPath, "/").Replace("\", "/")

            '--- Delete Cust and tblMYOBMatch records (commented from now)
            FMS.Business.DataObjects.CUST.DeleteAll()
            FMS.Business.DataObjects.tblMYOBMatch.DeleteAll()

            ''--- Get List from Cust table (temp table to hold list of customer from Text File)
            '' Note: Text file import operation will still need to discuss for better approach or implementation
            ''<place the textfile import code here>

            Dim folder = Server.MapPath("~/App_Data/MYOB")

            If (Not Directory.Exists(folder)) Then
                Directory.CreateDirectory(folder)
            End If


            Dim strMYOBFile As String = ""
            If (strMYOBFilename.Length > 0) Then
                Dim Val() = strMYOBFilename.Split("=")

                strMYOBFile = Val(1)

            End If

            Dim SavePath As String = System.IO.Path.Combine(folder, strMYOBFile) 'combines the saveDirectory and the filename to get a fully qualified path.

            If System.IO.File.Exists(SavePath) Then
                Dim sr As StreamReader = New StreamReader(SavePath)

                Dim ctr = 1

                Do While sr.Peek() > -1
                    'Console.WriteLine(sr.ReadLine())
                    Dim str = sr.ReadLine()
                    Dim arrCust() As String = str.Split(",")

                    Dim objCust = New FMS.Business.DataObjects.CUST()

                    objCust.ID = ctr
                    objCust.CardID = arrCust(0)
                    objCust.CustomerName = arrCust(1)

                    FMS.Business.DataObjects.CUST.Create(objCust)

                    ctr = ctr + 1
                Loop
                sr.Close()

            Else
                'the file doesn't exist
            End If

            '--- Update customer table based on card from CUST table if cbxUpdCustNames = true
            ' Note: For now it was set to change only one record for testing. Need to remove that in SQL stored procedure
            If (blnUpdateNamesChecked = True) Then

                Dim CustList = FMS.Business.DataObjects.CUST.GetAll()

                For Each c In CustList
                    Dim cardid = c.CardID

                    Dim blnCustUpdated = FMS.Business.DataObjects.CUST.UpdateCustomerBasedOnCardID(cardid)

                Next

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
        End If



    End Sub

End Class