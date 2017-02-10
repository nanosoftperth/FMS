<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NanoReportParamList.ascx.vb" Inherits="FMS.WEB.NanoReportParamList" %>
<%@ Register Src="~/Controls/NanoReportParam.ascx" TagPrefix="uc1" TagName="NanoReportParam" %>

<%--  --%>


<script type="text/javascript">

    function comboDateSelected_ValueChanged(s, id) {

        var selectedVal = s.GetValue();

        $('.' + id).hide(id);

        if (selectedVal == 'Specific') {
            $('.' + id + '.specificDateEdit').show('slow');
        } else {
            //$('.specificTimeEdit').show();
        }

    }


</script>

<table>

    <asp:PlaceHolder ID="mainDIV" runat="server"></asp:PlaceHolder>

</table>
<%--<div  id="" runat="server"></div>--%>

<%--<% =Guid.NewGuid.ToString()%>--%>

