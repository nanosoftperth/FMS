<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="TimeZoneTester.aspx.vb" Inherits="FMS.WEB.TimeZoneTester" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .btnSaveTimeZone {
            float: right;
        }

        .auto-style1 {
            width: 400px;
        }

        .cboPossibleTimeZones {
            float: left;
        }
    </style>

    <script type="text/javascript">

        function cboPossibleTimeZones_SelectedIndexChanged(s, e) {

            var selectedValue = cboPossibleTimeZones.GetValue();
            dgvTimezoneSettings.PerformCallback(selectedValue);
        }

    </script>

    <table>
        <tr>
            <td class="auto-style1">

                <dx:ASPxComboBox    ID="cboPossibleTimeZones"
                                    ClientInstanceName="cboPossibleTimeZones"
                                    runat="server"
                                    DataSourceID="odsAllTimeZoneOptions"
                                    ValueField="ID"
                                    TextField="Description"
                                    CssClass="cboPossibleTimeZones"
                    Width="260px" >

                    <ClientSideEvents SelectedIndexChanged="function(s,e){cboPossibleTimeZones_SelectedIndexChanged(s,e); }"  />

                </dx:ASPxComboBox>
                            

            </td>
        </tr>
        <tr>
            <td style="padding-top:10px;" class="auto-style1">

                <dx:ASPxGridView    ID="dgvTimezoneSettings" 
                                    ClientInstanceName="dgvTimezoneSettings"
                                    runat="server" 
                                    DataSourceID="odsTimeZoneData" 
                                    AutoGenerateColumns="False">

                   
                     <SettingsPager Visible="False"></SettingsPager>

                    <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="0" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1" Visible="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Value" VisibleIndex="3"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ApplicatiopnSettingValueID" VisibleIndex="4" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ApplicationName" VisibleIndex="5" Visible="False"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>

<%--###############################     ALL DATA SOURCES    ###############################--%>

    <asp:ObjectDataSource runat="server"
        ID="odsAllTimeZoneOptions"
        SelectMethod="GetMSftTimeZonesAndCurrentIfGoogle"
        TypeName="FMS.Business.DataObjects.TimeZone"></asp:ObjectDataSource>

    <asp:ObjectDataSource runat="server"
        ID="odsTimeZoneData"
        SelectMethod="GetTimeZoneValues"
        TypeName="FMS.WEB.ThisSession"></asp:ObjectDataSource>

</asp:Content>
