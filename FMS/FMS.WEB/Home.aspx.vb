Imports FMS.Business.DataObjects.FeatureListConstants

Public Class Home
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'figure out if this query string states that the company is uniqco
        Dim companyName As Object = Request.QueryString("company")
        Dim isUniqco As Boolean = companyName IsNot Nothing AndAlso companyName.ToString.ToLower.Trim = "uniqco"


        'sort out the logo for the user (dependant on if the user is uniqco)
        Dim img_src As String = "<img height=""70px;"" src=""Content/Images/{0}.png"" /> "
        Me.home_logo.Text = String.Format(img_src, If(isUniqco, "nanosoft_uniqco", "nanosoft2"))

        'sort out the message to the user
        Dim normal_msg As String = " Fleet Management Systems provides a very-tailored solution to the specific needs of the customer. " & vbNewLine & _
                                    "This means the system changes to suit your needs, as opposed to you having to change your business, to suit the system."
        Dim uniqco_msg As String = _
                    "Nanosoft GPS is the preferred supplier for Uniqco's cloud reporting tool Unifleet.</BR>" & vbNewLine & _
                    "We provide full integration with the Unifleet system meaning we can show your assets from within the Unifleet app.</BR>" & vbNewLine & _
                    "Please contact us and ask about the Unifleet partner discount <a href=""https://www.nanosoft.com.au/"" type=""text/html"">here</a></BR>" & vbNewLine & _
                    ""
        '"Should you wish to enable your own choice of GPS provider please contact Uniqco direct who will arrange a direct link with your supplier"


        Me.home_message.Text = If(isUniqco, uniqco_msg, normal_msg)

        'UserAuthorisationCheck(FeatureListAccess.Home_Page) ' old code for when this page required authentication 
    End Sub

End Class