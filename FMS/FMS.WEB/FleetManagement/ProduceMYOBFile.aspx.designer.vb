﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated. 
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class ProduceMYOBFile

    '''<summary>
    '''form1 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents form1 As Global.System.Web.UI.HtmlControls.HtmlForm

    '''<summary>
    '''ASPxCheckBox1 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxCheckBox1 As Global.DevExpress.Web.ASPxCheckBox

    '''<summary>
    '''ASPxLabel1 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxLabel1 As Global.DevExpress.Web.ASPxLabel

    '''<summary>
    '''txtInvoiceFilename control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents txtInvoiceFilename As Global.DevExpress.Web.ASPxTextBox

    '''<summary>
    '''ASPxLabel2 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxLabel2 As Global.DevExpress.Web.ASPxLabel

    '''<summary>
    '''txtMYOBFilename control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents txtMYOBFilename As Global.DevExpress.Web.ASPxTextBox

    '''<summary>
    '''ASPxLabel3 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxLabel3 As Global.DevExpress.Web.ASPxLabel

    '''<summary>
    '''txtInvStartNo control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents txtInvStartNo As Global.DevExpress.Web.ASPxTextBox

    '''<summary>
    '''ASPxLabel4 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxLabel4 As Global.DevExpress.Web.ASPxLabel

    '''<summary>
    '''cboMonth control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents cboMonth As Global.DevExpress.Web.ASPxComboBox

    '''<summary>
    '''ASPxLabel5 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxLabel5 As Global.DevExpress.Web.ASPxLabel

    '''<summary>
    '''dteStart control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents dteStart As Global.DevExpress.Web.ASPxDateEdit

    '''<summary>
    '''ASPxLabel6 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxLabel6 As Global.DevExpress.Web.ASPxLabel

    '''<summary>
    '''dteEnd control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents dteEnd As Global.DevExpress.Web.ASPxDateEdit

    '''<summary>
    '''btnChkMYOBCustNum control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnChkMYOBCustNum As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''pupChkMYOB control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents pupChkMYOB As Global.DevExpress.Web.ASPxPopupControl

    '''<summary>
    '''ASPxDocumentViewer1 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxDocumentViewer1 As Global.DevExpress.XtraReports.Web.ASPxDocumentViewer

    '''<summary>
    '''btnCloseChkMYOB control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnCloseChkMYOB As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''btnMatchMYOBNames control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnMatchMYOBNames As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''pupMatchMYOBNames control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents pupMatchMYOBNames As Global.DevExpress.Web.ASPxPopupControl

    '''<summary>
    '''cbxUpdCustNames control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents cbxUpdCustNames As Global.DevExpress.Web.ASPxCheckBox

    '''<summary>
    '''lblRecAdded control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents lblRecAdded As Global.DevExpress.Web.ASPxLabel

    '''<summary>
    '''btnProcMatch control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnProcMatch As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''btnCloseMatchMYOBNames control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnCloseMatchMYOBNames As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''popup control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents popup As Global.DevExpress.Web.ASPxPopupControl

    '''<summary>
    '''btnProcess control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnProcess As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''puDialogBox control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents puDialogBox As Global.DevExpress.Web.ASPxPopupControl

    '''<summary>
    '''lblBoxText control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents lblBoxText As Global.DevExpress.Web.ASPxLabel

    '''<summary>
    '''btnYes control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnYes As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''btnNo control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnNo As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''btnPrevInvSumRep control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnPrevInvSumRep As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''pupPrevInvSumRep control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents pupPrevInvSumRep As Global.DevExpress.Web.ASPxPopupControl

    '''<summary>
    '''ASPxDocumentViewer2 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxDocumentViewer2 As Global.DevExpress.XtraReports.Web.ASPxDocumentViewer

    '''<summary>
    '''btnClosePrevInvSumRep control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnClosePrevInvSumRep As Global.DevExpress.Web.ASPxButton

    '''<summary>
    '''idLoadingPanel control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents idLoadingPanel As Global.DevExpress.Web.ASPxLoadingPanel

    '''<summary>
    '''ASPxGlobalEvents1 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ASPxGlobalEvents1 As Global.DevExpress.Web.ASPxGlobalEvents

    '''<summary>
    '''LoadingPanel control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents LoadingPanel As Global.DevExpress.Web.ASPxLoadingPanel
End Class
