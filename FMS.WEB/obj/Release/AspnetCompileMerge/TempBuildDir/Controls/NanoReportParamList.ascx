<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NanoReportParamList.ascx.vb" Inherits="FMS.WEB.NanoReportParamList" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Src="~/Controls/NanoReportParam.ascx" TagPrefix="uc1" TagName="NanoReportParam" %>

<%--  --%>


<script type="text/javascript">

    //function comboDateSelected_ValueChanged(s, id) {

    //    var selectedVal = s.GetValue();

    //    $('.' + id).hide(id);

    //    if (selectedVal == 'Specific') {
    //        $('.' + id + '.specificDateEdit').show('slow');
    //    } else {
    //        //$('.specificTimeEdit').show();
    //    }

    //}


</script>

<table>

    <asp:PlaceHolder ID="mainDIV" runat="server"></asp:PlaceHolder>

</table>
<%--<div  id="" runat="server"></div>--%>

<%--<% =Guid.NewGuid.ToString()%>--%>

