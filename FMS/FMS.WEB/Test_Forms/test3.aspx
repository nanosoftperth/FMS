<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="test3.aspx.vb" Inherits="FMS.WEB.test3" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <table>
        <tr>
            <td style="padding: 5px;">
                <dx:ASPxLabel ID="ASPxLabel1" Width="80px" runat="server" Text="Start Time: "></dx:ASPxLabel>
            </td>
            <td style="padding: 5px;">
                <dx:ASPxDateEdit ID="dateEditStartTime" runat="server"></dx:ASPxDateEdit>
            </td>
            <td style="padding: 5px;">
                <dx:ASPxLabel ID="ASPxLabel2" Width="80px" runat="server" Text="End time: "></dx:ASPxLabel>
            </td>
            <td style="padding: 5px;">
                <dx:ASPxDateEdit ID="dateEditEndtime" runat="server"></dx:ASPxDateEdit>
            </td>
            <td style="padding: 5px;">
                <dx:ASPxButton ID="ASPxButton1"
                    runat="server"
                    Text="Refresh Report"
                    Theme="SoftOrange">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>

    <dx:ASPxGridView ID="dgvGeoFences" KeyFieldName="PK" runat="server" AutoGenerateColumns="False" DataSourceID="odsGeoFenceReport" Theme="SoftOrange" EnableTheming="True">
        <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterBar="Visible" ShowFilterRowMenu="True" ShowFooter="True"></Settings>
        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

        <SettingsPager PageSize="500"></SettingsPager>
        <SettingsDataSecurity AllowDelete="False" AllowInsert="False" AllowEdit="False"></SettingsDataSecurity>
        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>

        <Columns>
            <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="DeviceID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn FieldName="StartTime" VisibleIndex="6">

                <PropertiesDateEdit DisplayFormatString="dd/MMM/yyyy HH:mm:ss">
                    <TimeSectionProperties>
                        <TimeEditProperties>
                            <ClearButton Visibility="Auto"></ClearButton>
                        </TimeEditProperties>
                    </TimeSectionProperties>

                    <ClearButton Visibility="Auto"></ClearButton>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn FieldName="EndTime" VisibleIndex="7">
                <PropertiesDateEdit DisplayFormatString="dd/MMM/yyyy HH:mm:ss">
                    <TimeSectionProperties>
                        <TimeEditProperties>
                            <ClearButton Visibility="Auto"></ClearButton>
                        </TimeEditProperties>
                    </TimeSectionProperties>
                    <ClearButton Visibility="Auto"></ClearButton>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn FieldName="GeoFence_Description" VisibleIndex="3"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Vehicle_Name" VisibleIndex="4"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="GeoFence_Name" VisibleIndex="2"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Driver_Name" VisibleIndex="5"></dx:GridViewDataTextColumn>

            <%--the primary key (cheated, row_number() over (order by x)--%>
            <dx:GridViewDataTextColumn FieldName="PK" VisibleIndex="5" Visible="false"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="TimeTakes" VisibleIndex="8"></dx:GridViewDataTextColumn>

            <%----%>
        </Columns>
    </dx:ASPxGridView>

    <asp:ObjectDataSource runat="server" ID="odsGeoFenceReport" SelectMethod="GetReport" TypeName="FMS.Business.ReportGeneration.GeoFenceReport_Simple">
        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
            <asp:ControlParameter ControlID="dateEditStartTime" PropertyName="Value" Name="startdate" Type="DateTime"></asp:ControlParameter>
            <asp:ControlParameter ControlID="dateEditEndtime" PropertyName="Value" Name="enddate" Type="DateTime"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>








</asp:Content>
