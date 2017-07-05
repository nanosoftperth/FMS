<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusPropertyDisplay.aspx.vb" Inherits="FMS.WEB.CanBusPropertyDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="refresh" content="5" />
    <title></title>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script src="Content/javascript/page.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 10px;
        }    
        span.info{
            margin-left: 10px;
            display:block;
            color: #b1b1b1;
            font-size: 11px;
            font-style: italic;
            font-weight:bold;
        }  
    </style>
    
    <script type="text/javascript" language="javascript">
        function OnFaultCodesClick(contentUrl) {
            alert(contentUrl);          
            //ShowLoginWindow();
            
            /*clientPopupControl.SetContentUrl(contentUrl);
            clientPopupControl.Show();*/
        }
        function ShowLoginWindow() {
            pcLogin.Show();
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <dx:ASPxGridView ID="grid" Settings-ShowColumnHeaders="false" ClientInstanceName="grid" runat="server" Width="400px"
        AutoGenerateColumns="False" OnDataBinding="grid_DataBinding" Settings-UseFixedTableLayout="true" >
            <SettingsPager Visible="False" PageSize="18" >
            </SettingsPager>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="spn" Visible="false" VisibleIndex="0" SortOrder="Ascending" Width="20">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="label" VisibleIndex="1" Width="97">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn  FieldName="description" VisibleIndex="1" Width="140">
                    <DataItemTemplate>
                        <dx:ASPxHyperLink ForeColor="Black" ID="hyperLink" runat="server" OnInit="hyperLink_Init">
                        </dx:ASPxHyperLink>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="dtTime" VisibleIndex="1" Width="105">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        
             
    </div>
        <dx:ASPxPopupControl ID="pcLogin" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcLogin"
        HeaderText="Information" AllowDragging="True" PopupAnimationType="None" EnableViewState="False">        
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <span class="info"></span>                            
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>                
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    </form>
</body>
</html>
