<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.master" CodeBehind="UserManagement.aspx.vb" Inherits="FMS.WEB.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentLeft" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="Content/javascript/UserManagement.js"></script>


    <link href="/Content/bootstrap.css" rel="stylesheet" />

     <div style="position:absolute;bottom:10px;top:150px;left:15%;right:15%">

    <dx:ASPxRoundPanel ContentHeight="100%" ID="ASPxRoundPanel1" runat="server" Width="95%" EnableTheming="True" Theme="Moderno" HeaderText="User Management Page" Height="282px">
        <PanelCollection>                        

            <dx:PanelContent  runat="server">
                
                    <table style="width:100%;">
                        <tr>
                            <td style="vertical-align:top;">
                                <img style="top:2px;left:2px; height: 146px; width: 134px;" src="Content/Images/user-management.png" />

                            </td>

                             <%--<td style="padding-left: 14px;">
                                <p class="lead">
                                    <iframe src="UserManagementContent.aspx" style="width: 600px; height: 500px; border: none;" class="row"></iframe>
                                </p>
                            </td>--%>

                            <td style="padding-left:20px;">
                                
                                <p class="lead">This page can be used to configure users / roles and what features each role has access to:</p>
                                <p class="lead">
                            
                                    <iframe id="frmContent" src="UserManagementContent.aspx" style="width:100%;bottom:10px; border: none;overflow-y:visible;" class="row"></iframe>
                                </p>
                                <p></p>
                                <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-large">Learn more &raquo;</a></p>--%>

                         </td>
                        </tr>
                    </table>
                
            </dx:PanelContent>
        </PanelCollection>

    </dx:ASPxRoundPanel>

      
    </div>


    

</asp:Content>
