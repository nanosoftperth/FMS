<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NanoReportParam.ascx.vb" Inherits="FMS.WEB.NanoReportParam" %>



<script type="text/javascript">

    var uniqueClientID = '<%=UniqueClientID%>';

    $(document).ready(function () {
        // alert(uniqueClientID);
    })

</script>

<table>
    <tr>
        <td style="padding: 2px;">
            <dx:ASPxLabel ID="lblParameterName" runat="server" Text="this is a test"></dx:ASPxLabel>
        </td>
        <td>

            <%--change below to div, not panel? -dg--%>
            <dx:ASPxPanel ID="panelContent" runat="server" Width="200px">
                <PanelCollection>
                    <dx:PanelContent runat="server"></dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>

            <div id="dateTimeDIV" runat="server">


                <dx:ASPxComboBox ID="comboDateSelected"
                    runat="server" DataSourceID="odsDateTypes">
                </dx:ASPxComboBox>


                <asp:ObjectDataSource runat="server" ID="odsDateTypes"></asp:ObjectDataSource>
                <dx:ASPxDateEdit    TimeSectionProperties-Visible="true" 
                                    DisplayFormatString="G" 
                                    ID="dateSpecificDate" 
                                    runat="server" 
                                    EditFormat="DateTime"></dx:ASPxDateEdit>



            </div>

        </td>
    </tr>

</table>
