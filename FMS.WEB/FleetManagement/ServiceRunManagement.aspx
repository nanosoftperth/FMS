<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceRunManagement.aspx.vb" Inherits="FMS.WEB.ServiceRunManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Run Management</title>
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
                                                AutoPostBack="true" OnValueChanged="dteStart_ValueChanged">
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
                                                AutoPostBack="true" OnValueChanged="dteEnd_ValueChanged">
                                                <TimeSectionProperties>
                                                <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                                </TimeEditProperties>
                                                </TimeSectionProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:Button ID="Button1" runat="server" Text="try" onclick="Button1_Click"/>
                                        </td>
                                        <td style="width: 10%; text-align: right; padding-right: 5px;">
                                            <dx:ASPxImage ID="imgFilter" runat="server" ImageUrl="../Content/Images/FilterRecord.png" 
                                                Width="15px" Height="15px"></dx:ASPxImage>
                                        </td>
                                        <td style="width: 10%; text-align: left">
                                            <dx:ASPxLabel ID="lblFilter" runat="server" text="Filter"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 10%; text-align: right; padding-right: 5px;">
                                            <dx:ASPxImage ID="imgSearch" runat="server" ImageUrl="../Content/Images/SearchRecord.png" 
                                                Width="15px" Height="15px"></dx:ASPxImage>
                                        </td>
                                        <td style="width: 10%; padding-left: ">
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" text="Filter"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <dx:ASPxGridView ID="gvServiceRun" runat="server" ClientInstanceName="gvServiceRun">
		                        </dx:ASPxGridView>
                                <%--<dx:ASPxGridView ID="gvServiceRun" ClientInstanceName="gvServiceRun" 
                                    DataSourceID="odsRunDates" runat="server" 
                                    EnableTheming="True" 
                                    Theme="SoftOrange" AutoGenerateColumns="False">

                                    <Columns>
                                        <dx:GridViewDataDateColumn FieldName="rundate" ShowInCustomizationForm="True" VisibleIndex="0">
                                            <PropertiesDateEdit>
                                                <TimeSectionProperties>
                                                    <TimeEditProperties>
                                                        <ClearButton Visibility="Auto">
                                                        </ClearButton>
                                                    </TimeEditProperties>
                                                </TimeSectionProperties>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                    </Columns>

                                </dx:ASPxGridView>--%>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Run Definition">
                        <ContentCollection>
                            <dx:ContentControl runat="server"></dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text=" Data Entry">
                        <ContentCollection>
                            <dx:ContentControl runat="server"></dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </div>
        <asp:ObjectDataSource ID="odsRunDates" runat="server" SelectMethod="GetRunDates" TypeName="FMS.WEB.ServiceRunManagement">
            <SelectParameters>
                <asp:ControlParameter ControlID="carTabPage$dteStart" Name="StartDate" PropertyName="Value" Type="DateTime" />
                <asp:ControlParameter ControlID="carTabPage$dteEnd" Name="EndDate" PropertyName="Value" Type="DateTime" />
                <%--<asp:Parameter Name="StartDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />--%>
                
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
