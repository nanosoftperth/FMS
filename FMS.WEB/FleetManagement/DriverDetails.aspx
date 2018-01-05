<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DriverDetails.aspx.vb" Inherits="FMS.WEB.DriverDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver Details</title>
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Driver Details" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>      
        </div>
        <dx:ASPxGridView ID="gvDriver" runat="server" AutoGenerateColumns="false" 
            KeyFieldName="DriverID" DataSourceID="odsDriver" Width="100%"
            OnHtmlRowPrepared="gvDriver_HtmlRowPrepared">
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" 
                    ShowNewButtonInHeader="True" ShowDeleteButton="True"
                    VisibleIndex="0" ></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="DriverID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Did" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DriverName" VisibleIndex="4" Visible="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DriversLicenseNo" VisibleIndex="5" Visible="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="DriversLicenseExpiryDate" VisibleIndex="6" Visible="true"></dx:GridViewDataDateColumn>
                <dx:GridViewDataCheckColumn FieldName="Inactive" VisibleIndex="7" Visible="true"></dx:GridViewDataCheckColumn>
                <%--<dx:GridViewDataTextColumn FieldName="Comments" ReadOnly="True" VisibleIndex="8">
                    <DataItemTemplate>
                        <dx:ASPxButton ID="btnComment" runat="server" Text="Add" Width="100%" OnClick="btnComment_Click">
                        </dx:ASPxButton>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Allocate Runs To Different Driver" ReadOnly="True" VisibleIndex="9">
                    <DataItemTemplate>
                        <dx:ASPxButton ID="btnAllocate" runat="server" Text="Allocate" Width="100%">
                        </dx:ASPxButton>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>--%>
                
                
                <%--<dx:GridViewCommandColumn VisibleIndex="8">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnComment" Text="Add Comment">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn VisibleIndex="9">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnAllocate" Text="Allocate">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>--%>
                
                
                
            </Columns>
            <Templates>
                <DetailRow>
                    <dx:ASPxLabel runat="server" text="Comments" Font-Bold="true" />
                    <br />
                </DetailRow>
            </Templates>
            <SettingsDetail ShowDetailRow="true" />
            <SettingsPager EnableAdaptivity="true" />
        </dx:ASPxGridView>
        <br />
        <table style="width: 200px">
            <tr>
                <td style="width: 50px">
                    <dx:ASPxButton ID="btnComment" runat="server" Text="Driver Comment Reason" Width="100%">
                    </dx:ASPxButton>
                </td>
                <td style="width: 50px">
                    <dx:ASPxButton ID="btnAllocate" runat="server" Text="Allocate Runs To Different Driver" Width="100%">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <dx:ASPxPopupControl ID="pupComment" runat="server" ClientInstanceName="pupComment" 
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="700px" AllowDragging="True" 
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="lblRecAdded" runat="server" Text="this is a test"></dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <asp:ObjectDataSource ID="odsDriver" runat="server" SelectMethod="GetAllPerApplication" 
            TypeName="FMS.Business.DataObjects.tblDrivers" 
            DataObjectTypeName="FMS.Business.DataObjects.tblDrivers" 
            DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>
