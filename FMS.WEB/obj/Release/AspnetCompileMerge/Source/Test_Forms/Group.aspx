<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Group.aspx.vb" MasterPageFile="~/MainLight.master" Inherits="FMS.WEB.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">


        function dgvGroups_FocusedRowChanged(s, e) {

            var focusedRowIndex = dgvGroups.GetFocusedRowIndex();
            var focusedKey = dgvGroups.GetRowKey(focusedRowIndex);
            dgvMembers.PerformCallback('get|' + focusedKey);
        }

        function btnRemove_Click(s, e) {


            var selectedVals = dgvMembers.GetSelectedKeysOnPage().toString();
            dgvMembers.PerformCallback('remove|' + selectedVals);
            e.cancel = true;
        }

        function btnAdd_Click(s, e) {

            var selectedVals = dgvPotentialGroupMembers.GetSelectedKeysOnPage().toString();
            dgvMembers.PerformCallback('add|' + selectedVals);
            e.cancel = true;
        }

        function btnSaveChanges_Click(s, e) {

            alert('save button pressed');
        }

    </script>

    <style type="text/css">
        .btnSaveChanges {
            float: right;
        }
    </style>

    <table>

        <tr>

            <td style="padding-left: 20px;">
                <dx:ASPxLabel ID="labelGroups" runat="server" Text="Groups:" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>
            </td>

            <td style="padding-left: 20px;">
                <dx:ASPxLabel ID="labelMembers" runat="server" Text="Potential Members:" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>
            </td>

            <td style="padding: 7px;" valign="center"></td>
            <td style="padding-left: 20px;">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Group Members:" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>
            </td>

        </tr>

        <tr>

            <td style="padding-left: 20px;" valign="top">
                <dx:ASPxGridView ID="dgvGroups" ClientInstanceName="dgvGroups" KeyFieldName="GroupID" SettingsBehavior-AllowFocusedRow="true" runat="server" AutoGenerateColumns="False" DataSourceID="odsGroups">
                    <ClientSideEvents FocusedRowChanged="function(s,e){dgvGroups_FocusedRowChanged(s,e);}" />
                    <Columns>
                        <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="GroupID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="GroupName" VisibleIndex="3"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="2" Visible="False"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>

                <asp:ObjectDataSource runat="server" ID="odsGroups" DataObjectTypeName="FMS.Business.DataObjects.Group" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetForApplication" TypeName="FMS.Business.DataObjects.Group" UpdateMethod="Update">
                    <SelectParameters>
                        <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>

            <td style="padding-left: 20px;" valign="top">

                <dx:ASPxGridView ID="dgvPotentialGroupMembers"
                    KeyFieldName="NativeID"
                    ClientInstanceName="dgvPotentialGroupMembers"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataSourceID="odsSubscriber">

                    <Settings ShowGroupPanel="True"></Settings>

                    <ClientSideEvents />
                    <SettingsBehavior AllowSelectByRowClick="true" />
                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0"></dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="SubscriberType_Str" VisibleIndex="2" Caption="Subscriber Type"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="NativeID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="3"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Mobile" VisibleIndex="4"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>



                <asp:ObjectDataSource runat="server" ID="odsSubscriber" SelectMethod="GetAllforApplication" TypeName="FMS.Business.DataObjects.Subscriber" DataObjectTypeName="FMS.Business.DataObjects.Subscriber" UpdateMethod="update">
                    <SelectParameters>
                        <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>

            <td style="padding: 7px;" valign="center">

                <dx:ASPxButton ID="btnAdd" runat="server" AutoPostBack="false" Text="add &gt;&gt;" ClientInstanceName="btnAdd" EnableClientSideAPI="True" Width="80px">
                    <ClientSideEvents Click="function(s,e){btnAdd_Click(s,e);}" />
                </dx:ASPxButton>
                <br />
                <br />
                <br />
                <dx:ASPxButton ID="btnRemove" AutoPostBack="false" runat="server" Text="&lt;&lt; remove" ClientInstanceName="btnRemove" EnableClientSideAPI="True" Width="80px">
                    <ClientSideEvents Click="function(s,e){btnRemove_Click(s,e);}" />

                </dx:ASPxButton>
            </td>

            <td style="padding-left: 20px;" valign="top">

                <dx:ASPxGridView ID="dgvGroupMembers"
                    KeyFieldName="NativeID"
                    ClientInstanceName="dgvMembers"
                    runat="server"
                    SettingsEditing-BatchEditSettings-EditMode="Row"
                    AutoGenerateColumns="False"
                    DataSourceID="odsSubscriber">

                    <Settings ShowGroupPanel="True"></Settings>

                    <ClientSideEvents />

                    <SettingsEditing Mode="Batch">
                        <BatchEditSettings EditMode="Cell"></BatchEditSettings>
                    </SettingsEditing>

                    <SettingsBehavior AllowSelectByRowClick="true" />
                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False"></SettingsDataSecurity>

                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0"></dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="SubscriberType_Str" VisibleIndex="2" Caption="Subscriber Type" ReadOnly="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="NativeID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="3" ReadOnly="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Mobile" VisibleIndex="4" ReadOnly="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn FieldName="SendEmail" VisibleIndex="5"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="SendText" VisibleIndex="6"></dx:GridViewDataCheckColumn>
                    </Columns>
                </dx:ASPxGridView>



            </td>

        </tr>

    </table>

</asp:Content>
