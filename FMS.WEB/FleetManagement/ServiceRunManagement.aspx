<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceRunManagement.aspx.vb" Inherits="FMS.WEB.ServiceRunManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Run Management</title>
    <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    <style>
        .ServiceRunHeader td {
            border: none !important;
            width: 50%;
            height: 100px;
            text-align: center;
            vertical-align: middle;
            -webkit-transform: rotate(320deg);
            -moz-transform: rotate(320deg);
            -o-transform: rotate(320deg);
            writing-mode: tb-rl;
        }
    </style>

    <script type="text/javascript">
        function ShowPopup(rundate) {
            var jsDate = clientdteStart.GetDate();
            var syear = jsDate.getFullYear(); // where getFullYear returns the year (four digits)
            var smonth = jsDate.getMonth(); // where getMonth returns the month (from 0-11)
            var sday = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)

            var jeDate = clientdteEnd.GetDate();
            var eyear = jeDate.getFullYear();
            var emonth = jeDate.getMonth();
            var eday = jeDate.getDate();

            var startdate = new Date(syear, smonth, sday);
            var intDateDiff = GetDateDiff(jsDate, jeDate);
            var tmpdate = new Date(jsDate.setDate(2));



            alert(rundate);
            clientpuUnassignedRun.Show();
        }

        //datediff in days
        function GetDateDiff(d1, d2) {
            var t2 = d2.getTime();
            var t1 = d1.getTime();

            return parseInt((t2 - t1) / (24 * 3600 * 1000));
        };

        function ContextMenuServiceRun(s, e) {
            var cellInfo = GetCellInfo(s, ASPxClientUtils.GetEventSource(e.htmlEvent));
            if (!cellInfo) return;
            var args = cellInfo.split("_");
            var visibleIndex = parseInt(args[0]);
            var fieldName = s.cpDataColumnMap[parseInt(args[1])];

            var TechExist = fieldName.indexOf("Tech");
            var DriverExist = fieldName.indexOf("Driver");
            var TechID = ""
            var DriderID = ""

            if (TechExist > -1) {
                var ndx = fieldName.indexOf("_") + 1;
                TechID = fieldName.substring(ndx, fieldName.length);
            }

            if (DriverExist > -1) {
                var ndx = fieldName.indexOf("_") + 1;
                DriderID = fieldName.substring(ndx, fieldName.length);
            }

            var src = ASPxClientUtils.GetEventSource(e.htmlEvent);
            //clientlblDriverID.SetText(src.innerText);

            //alert('tech: ' + TechID + ' - ' + 'driver: ' + DriderID);
            //alert("VisibleIndex = " + visibleIndex + "\nFieldName = " + fieldName);

            //var src = ASPxClientUtils.GetEventSource(e.htmlEvent);
            //clientlblDriverID.SetText(src.innerText);

            clientpuCompleteRun.Show();

        }

        function GetCellInfo(grid, element) {
            var gridMainElement = grid.GetMainElement();
            while (element && element !== gridMainElement && element.tagName !== "BODY") {
                var cellInfo = element.getAttribute("data-CI");
                if (cellInfo)
                    return cellInfo;
                element = element.parentNode;
            }
        }

        // function is called on changing focused row 
        function OnGridFocusedRowChanged(s,e) {
            var cellInfo = GetCellInfo(s, ASPxClientUtils.GetEventSource(e.htmlEvent));
            if (!cellInfo) return;
            var args = cellInfo.split("_");
            var visibleIndex = parseInt(args[0]);
            var fieldName = s.cpDataColumnMap[parseInt(args[1])];
            alert(fieldName);

            // The single value will be returned to the OnGetRowValues() function      
            //clientgvServiceRun.GetRowValues(clientgvServiceRun.GetFocusedRowIndex(), 'RunDate', OnGetRowValues);
        }
        // Value contains the "EmployeeID" field value returned from the server, not the list of values 
        function OnGetRowValues(Value) {

            if (Value != null)
            {
                alert(Value);
                
            }

            // Right code 
            //alert(Value);
            // This code will cause an error 
            // alert(Value[0]); 
        }

        //function Grid_ContextMenu(s, e) {


        //    //var cellInfo = GetCellInfo(s, ASPxClientUtils.GetEventSource(e.htmlEvent));
        //    //if (!cellInfo) return;
        //    //var args = cellInfo.split("_");
        //    //var visibleIndex = parseInt(args[0]);
        //    //var fieldName = s.cpDataColumnMap[parseInt(args[1])];
        //    //var strCellInfo = cellInfo;
        //    //var strargs = args;

        //    //alert(fieldName);
        //    ////alert("VisibleIndex = " + visibleIndex + "\nFieldName = " + fieldName);
        //    ////clientpuUnassignedRun.Show();
        //}

        //function OnGridFocusedRowChanged() {
        //    // Query the server for the "EmployeeID" and "Notes" fields from the focused row 
        //    // The values will be returned to the OnGetRowValues() function
        //    //DetailNotes.SetText("Loading...");
        //    grid.GetRowValues(grid.GetFocusedRowIndex(), 'EmployeeID;Notes', OnGetRowValues);
        //}

        //// Value array contains "EmployeeID" and "Notes" field values returned from the server 
        //function OnGetRowValues(values) {
        //    DetailImage.SetImageUrl("FocusedRow.aspx?Photo=" + values[0]);
        //    DetailImage.SetVisible(true);
        //    DetailNotes.SetText(values[1]);
        //}



        //function ContextMenuServiceRun(s, e) {
        //    clientpuCompleteRun.Show();

        //}

        //function ContextMenuServiceRun(event) {
        //    clientpuCompleteRun.Show();
        //    //if (popupMenu.GetVisible())
        //    //    popupMenu.Hide();
        //    //if (!popupMenu.GetVisible())
        //    //    popupMenu.ShowAtPos(ASPxClientUtils.GetEventX(evt), ASPxClientUtils.GetEventY(evt));
        //}

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
                                            <dx:ASPxDateEdit ID="dteStart" runat="server" NullText="Start Date" Width="95%"
                                                ClientInstanceName="clientdteStart"
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
                                            <dx:ASPxDateEdit ID="dteEnd" runat="server" NullText="End Date" Width="95%"
                                                ClientInstanceName="clientdteEnd"
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
                                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
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
                                <dx:ASPxGridView ID="gvServiceRun" runat="server" ClientInstanceName="clientgvServiceRun"
                                    OnHtmlDataCellPrepared="gvServiceRun_HtmlDataCellPrepared"
                                    OnCustomJSProperties="gvServiceRun_CustomJSProperties"  
                                    EnableTheming="True" Theme="SoftOrange"
                                    KeyFieldName="ID">
                                    <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                    <Styles>
                                        <Header CssClass="ServiceRunHeader" />
                                    </Styles>
                                    <ClientSideEvents ContextMenu=" function(s, e) { 
                                        if (e.objectType == 'row') {
                                            ContextMenuServiceRun(s,e);
                                        }
                                    } " />

                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
                                        Alert('Test');                                      
                                        }" />
                                    <%--<ClientSideEvents ContextMenu="Grid_ContextMenu" />--%>
                                    <%--<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />--%>
                                </dx:ASPxGridView>
                                <%--<dx:ASPxPopupControl ID="puUnassignedRun" runat="server" ClientInstanceName="puUnassignedRun" 
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
                                            <dx:ASPxButton ID="btnCancel" runat="server" text="Cancel"
                                                OnClick="btnCancel_Click"></dx:ASPxButton>
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                </dx:ASPxPopupControl>--%>
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
            <dx:ASPxPopupControl ID="puUnassignedRun" runat="server" ClientInstanceName="clientpuUnassignedRun"
                Height="83px" Modal="True" CloseAction="CloseButton" Width="300px"
                AllowDragging="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" ShowHeader="False">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblSelectRun" runat="server" Text="Select Run "></dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="cboRun" runat="server" AutoPostBack="true"></dx:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel"
                            OnClick="btnCancel_Click">
                        </dx:ASPxButton>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            <dx:ASPxPopupControl ID="puCompleteRun" runat="server" ClientInstanceName="clientpuCompleteRun"
                Height="83px" Modal="True" CloseAction="CloseButton" Width="300px"
                AllowDragging="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" ShowHeader="False">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxCheckBox ID="cbxCompleteRun" runat="server" Text="Run Completed"></dx:ASPxCheckBox>
                                    <%--<dx:ASPxLabel ID="lblCompleteRun" runat="server" Text="Complte Run"></dx:ASPxLabel>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<dx:ASPxLabel ID="lblDriverID" ClientInstanceName="clientlblDriverID" runat="server" Text="">
                                    </dx:ASPxLabel>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxDateEdit ID="dteCompleted" runat="server" NullText="Date Completed"
                                        AutoPostBack="false">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxComboBox runat="server" ID="cboDriverCompleted" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
                                        TextField="DriverName" ValueField="Did" Width="100%" DataSourceID="odsDriver"
                                        EnableSynchronization="False">
                                       <%-- <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }" />--%>
                                    </dx:ASPxComboBox>
                                    <%--<dx:ASPxComboBox ID="cboDriverCompleted" runat="server" AutoPostBack="true" DataSourceID="odsDriver">
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </dx:ASPxComboBox>--%>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <dx:ASPxButton ID="btnCancelComplte" runat="server" Text="Cancel"
                            OnClick="btnCancelComplte_Click">
                        </dx:ASPxButton>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
        <%--<asp:ObjectDataSource ID="odsRunDates" runat="server" SelectMethod="GetRunDates" TypeName="FMS.WEB.ServiceRunManagement">
            <SelectParameters>
                <asp:ControlParameter ControlID="carTabPage$dteStart" Name="StartDate" PropertyName="Value" Type="DateTime" />
                <asp:ControlParameter ControlID="carTabPage$dteEnd" Name="EndDate" PropertyName="Value" Type="DateTime" />
                
            </SelectParameters>
        </asp:ObjectDataSource>--%>
        <asp:ObjectDataSource ID="odsDriver" runat="server" SelectMethod="GetAllPerApplicationMinusInActive" TypeName="FMS.Business.DataObjects.tblDrivers"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsUnassignedRuns" runat="server" SelectMethod="GetAllPerApplication" TypeName="FMS.Business.DataObjects.usp_GetUnAssignedRuns">
            <SelectParameters>
                <asp:Parameter Name="StartDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </form>
</body>
</html>
