<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UniqcoWebServiceLog.aspx.vb" Inherits="FMS.ServiceAccess.UniqcoWebServiceLog" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <h1 style="font-family:Arial;color:darkorange;">Uniqco webservice log</h1>

        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsWebService" EnableTheming="True" Theme="SoftOrange">
            <SettingsPager Visible="False">
            </SettingsPager>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
            <Columns>
                <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="WebServiceLogID" Visible="False" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RequestMethod" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Login" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Company" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="DateLogged" VisibleIndex="5">
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
                <dx:GridViewDataMemoColumn FieldName="XMLRequest" VisibleIndex="6">
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn FieldName="XMLResponse" VisibleIndex="7">
                </dx:GridViewDataMemoColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsWebService" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.WebServiceLogLine"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
