<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NanoReportParam.ascx.vb" Inherits="FMS.WEB.NanoReportParam" %>



<script type="text/javascript">

    var uniqueClientID = '<%=UniqueClientID%>';
 
    $(document).ready(function () {
        // alert(uniqueClientID);
         
    })
   

</script>

<%--THE TRs below are there as the NanoReportPArams collection will add a <table> wrapper--%>
<tr style="padding-left:10px">

    <td style="text-align: left; padding:0px; vertical-align: middle;" >
        <dx:ASPxLabel ID="lblParameterName" runat="server" Width="70px" Text="this is a test"></dx:ASPxLabel>
    </td>
    <td style="vertical-align: top; text-align: left;">

        <%--change below to div, not panel? -dg--%>
        <div id="panelContent" runat="server">
             
        </div>
       
        <div id="dateTimeDIV" runat="server" >


            <%-- <div style="padding: 2px;">--%>
            <dx:ASPxComboBox ID="comboDateSelected" runat="server"></dx:ASPxComboBox>
            <%--</div>--%>
             
          <div class=" <%=UniqueClientID%> specificDateEdit" 
                    style="display: <%=ISDisplay%>;padding-top: 4px; padding-bottom: 0px;">  
                <dx:ASPxDateEdit TimeSectionProperties-Visible="true"
                    DisplayFormatString="G"
                    ID="dateSpecificDate"
                    runat="server"
                    EditFormat="DateTime" CssClass ="<%=UniqueClientID%> specificDateEdit">
                </dx:ASPxDateEdit>

            </div>


        </div>

    </td>


</tr>


