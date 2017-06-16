<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusPropertyDisplay.aspx.vb" Inherits="FMS.WEB.CanBusPropertyDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<meta http-equiv="refresh" content="30" />--%>
    <title></title>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script src="Content/javascript/page.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <dx:ASPxGridView ID="grid" Settings-ShowColumnHeaders="false" ClientInstanceName="grid" runat="server" Width="250px"
        AutoGenerateColumns="False" OnDataBinding="grid_DataBinding" Settings-UseFixedTableLayout="true"  >
            <SettingsPager Visible="False"  >
            </SettingsPager>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="label" VisibleIndex="1" Width="100">
                    <%--<CellStyle Wrap="True"></CellStyle>--%>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="description" VisibleIndex="1" Width="150">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="dtTime" VisibleIndex="1" Width="40">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>

             
    </div>
    </form>
</body>
</html>
