<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="test_Settings.aspx.vb" Inherits="FMS.WEB.test_Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">

        .thebutton {
            margin-left:10px;
        }


    </style>

    <table>

        <tr>
            <td>
                <table>
                    <tr>
                        <td style="padding-left: 10px; padding-right: 10px; padding-bottom: 10px; padding-top: 0px; vertical-align: top;">
                            <table style="width:100px;">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Company Logo:"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right bottom"></td>
                                </tr>
                            </table>

                        </td>
                        <td rowspan="2">
                            <dx:ASPxBinaryImage ID="imgCompanylogo" runat="server" BinaryStorageMode="Session">
                                <EditingSettings Enabled="True"></EditingSettings>
                            </dx:ASPxBinaryImage>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;padding:5px;">
                            <dx:ASPxButton Width="80px" CssClass="thebutton"  ID="ASPxButton1"
                                runat="server"
                                Text="save logo">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; padding-left: 50px;">
                <table>
                    <tr>
                        <td style="padding-left: 10px; padding-right: 10px; padding-bottom: 10px; padding-top: 0px; vertical-align: top;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="General Settings:"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <dx:ASPxGridView ID="ASPxGridView1" KeyFieldName="ApplicatiopnSettingValueID" runat="server" AutoGenerateColumns="False" DataSourceID="odsSettings">
                                <SettingsPager Visible="False"></SettingsPager>
                                <SettingsDataSecurity AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>
                                <Columns>
                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0"></dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="3" Visible="False"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="SettingID" VisibleIndex="2" Visible="False"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Value" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="ApplicatiopnSettingValueID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="ApplicationName" VisibleIndex="6" Visible="False"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1" Visible="True"></dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <asp:ObjectDataSource runat="server"
        ID="odsSettings"
        SelectMethod="GetSettingsForApplication_withoutImages"
        TypeName="FMS.Business.DataObjects.Setting"
        DataObjectTypeName="FMS.Business.DataObjects.Setting"
        UpdateMethod="Update">

        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" Name="applicationid" DbType="Guid"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    
</asp:Content>
