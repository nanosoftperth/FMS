<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="Main_Test.aspx.vb" Inherits="FMS.WEB.Main_Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Content/javascript/jquery-1.10.2.min.js"></script>

    <script type="text/javascript">

        function comboTest_SelectedIndexChanged(s, e) {

            var selectedGUID = comboTest.GetValue();

            var newurl = 'GeoFencePropertyDisplay.aspx?ApplicationGeoFenceID=' + selectedGUID;

            $('#iframe1').attr('src', newurl);

        }

    </script>

    <table>
        <tr>
            <td>
                <dx:ASPxComboBox ValueField="ApplicationGeoFenceID"
                    TextField="Name"
                    ID="comboTest"
                    ClientInstanceName="comboTest"
                    runat="server"
                    DataSourceID="odsGeoFences">
                    <ClientSideEvents SelectedIndexChanged="function(s,e){comboTest_SelectedIndexChanged(s,e);}" />
                </dx:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td>

                <div>
                    <iframe id="iframe1" marginwidth='0' marginheight='0' frameborder='0' overflow-y='scroll' overflow-x='hidden' style="height: 227px; width: 285px"></iframe>

                </div>

            </td>
        </tr>

    </table>





    <asp:ObjectDataSource runat="server" ID="odsGeoFences" SelectMethod="GetAllApplicationGeoFences" TypeName="FMS.Business.DataObjects.ApplicationGeoFence">
        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
