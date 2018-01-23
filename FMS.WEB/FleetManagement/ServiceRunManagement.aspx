<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceRunManagement.aspx.vb" Inherits="FMS.WEB.ServiceRunManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Run Management</title>
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    <style>
         .ServiceRunHeader td
        {
            border: none !important;

            width: 50%;
            height: 100px;
            text-align:center;
            vertical-align: middle;
            -webkit-transform: rotate(320deg);
            -moz-transform: rotate(320deg);
            -o-transform: rotate(320deg);
            writing-mode: tb-rl;
            
        }
    </style>

    <script type="text/javascript">
        var FocusedCell;
        function onCellClick(columnName, cellValue, htmlId) {
            var strVal = cellValue.substr(0, 3).toUpperCase();
            if (strVal == 'RUN')
            {
                puUnassignedRun.Show();
            };
            
            //alert(cellValue);
            //if (FocusedCell != null) {
            //    FocusedCell.style.color = '';
            //    FocusedCell.style.border = '';
            //}
            //FocusedCell = document.getElementById(htmlId);
            //FocusedCell.style.color = 'Red';
            //FocusedCell.style.border = '1px solid Red';
            //clientLabelField.SetText(columnName);
            //clientTextBoxCellValue.SetText(cellValue);
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPageControl ID="carTabPage" Width="100%" runat="server" 
                CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                <TabPages>
                    <dx:TabPage Text="Service Run">
                        <ContentCollection>
                            <dx:ContentControl ID="ccServRun" runat="server">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <dx:ASPxDateEdit id="dteStart" runat="server" NullText="Start Date" Width="95%"
                                                AutoPostBack="false" OnValueChanged="dteStart_ValueChanged">
                                                <TimeSectionProperties>
                                                <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                                </TimeEditProperties>
                                                </TimeSectionProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td style="width: 20%; text-align: left">
                                            <dx:ASPxDateEdit id="dteEnd" runat="server" NullText="End Date" Width="95%"
                                                AutoPostBack="false" OnValueChanged="dteEnd_ValueChanged">
                                                <TimeSectionProperties>
                                                <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                                </TimeEditProperties>
                                                </TimeSectionProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:Button ID="btnLoad" runat="server" Text="Load" onclick="btnLoad_Click"/>
                                        </td>
                                        <td style="width: 10%; text-align: right; padding-right: 5px;">
                                            <%--<dx:ASPxImage ID="imgFilter" runat="server" ImageUrl="../Content/Images/FilterRecord.png" 
                                                Width="15px" Height="15px"></dx:ASPxImage>--%>
                                        </td>
                                        <td style="width: 10%; text-align: left">
                                            <%--<dx:ASPxLabel ID="lblFilter" runat="server" text="Filter"></dx:ASPxLabel>--%>
                                        </td>
                                        <td style="width: 10%; text-align: right; padding-right: 5px;">
                                            <%--<dx:ASPxImage ID="imgSearch" runat="server" ImageUrl="../Content/Images/SearchRecord.png" 
                                                Width="15px" Height="15px"></dx:ASPxImage>--%>
                                        </td>
                                        <td style="width: 10%; padding-left: ">
                                            <%--<dx:ASPxLabel ID="ASPxLabel1" runat="server" text="Search"></dx:ASPxLabel>--%>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <dx:ASPxGridView ID="gvServiceRun" runat="server" ClientInstanceName="gvServiceRun"
                                    OnHtmlDataCellPrepared="gvServiceRun_HtmlDataCellPrepared"
                                    FocusedCellChanging=""
                                    EnableTheming="True" Theme="SoftOrange">
                                    <Styles>
                                        <Header CssClass="ServiceRunHeader" />
                                    </Styles>

		                        </dx:ASPxGridView>
                                <dx:ASPxPopupControl ID="puUnassignedRun" runat="server" ClientInstanceName="puUnassignedRun" 
                                    Height="83px" Modal="True" CloseAction="CloseButton" Width="300px" 
                                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
                                    PopupVerticalAlign="WindowCenter" ShowHeader="False">
                                    <ContentCollection>
                                        <dx:PopupControlContentControl runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                         <dx:ASPxLabel ID="lblSelectRun" runat="server" text="Select Run "></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cboRun" runat="server" AutoPostBack="true"></dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <%--<dx:ASPxLabel ID="lbtest" runat="server" text="Test popup"></dx:ASPxLabel>--%>
                                            <dx:ASPxButton ID="btnCancel" runat="server" text="Cancel"
                                                OnClick="btnCancel_Click"></dx:ASPxButton>
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                </dx:ASPxPopupControl>
                                
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Run Definition">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxGridView ID="gvRun" runat="server" ClientInstanceName="gvServiceRun">                                    
		                        </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text=" Data Entry">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </div>
        <%--<asp:ObjectDataSource ID="odsRunDates" runat="server" SelectMethod="GetRunDates" TypeName="FMS.WEB.ServiceRunManagement">
            <SelectParameters>
                <asp:ControlParameter ControlID="carTabPage$dteStart" Name="StartDate" PropertyName="Value" Type="DateTime" />
                <asp:ControlParameter ControlID="carTabPage$dteEnd" Name="EndDate" PropertyName="Value" Type="DateTime" />
                
            </SelectParameters>
        </asp:ObjectDataSource>--%>
    </form>
</body>
</html>
