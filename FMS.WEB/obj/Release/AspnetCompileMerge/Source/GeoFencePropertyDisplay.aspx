<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GeoFencePropertyDisplay.aspx.vb" Inherits="FMS.WEB.GeoFencePropertyDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxGridView KeyFieldName="ApplicationGeoFencePropertyID" ID="dgvGeoFenceProperty" runat="server" AutoGenerateColumns="False" DataSourceID="odsApplicationGeoFenceProperties" EnableTheming="True" Theme="DevEx" Width="100%">
            <SettingsPager Visible="False">
            </SettingsPager>
            <EditFormLayoutProperties ColCount="2">
                <Items>
                    <dx:GridViewColumnLayoutItem ColumnName="PropertyName" ColSpan="2"></dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColumnName="PropertyValue" ColSpan="2"></dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem Caption=""></dx:GridViewColumnLayoutItem>
                    <dx:EditModeCommandLayoutItem HorizontalAlign="Right"></dx:EditModeCommandLayoutItem>
                </Items>
            </EditFormLayoutProperties>
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="20px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="ApplicationGeoFencePropertyID" Visible="False" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ApplicationGeoFenceID" Visible="False" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Property" FieldName="PropertyName" MinWidth="80" VisibleIndex="3" Width="80px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Value" FieldName="PropertyValue" MinWidth="80" VisibleIndex="4" Width="80px">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsApplicationGeoFenceProperties" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationGeoFenceProperty" DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetForGeoFence" TypeName="FMS.Business.DataObjects.ApplicationGeoFenceProperty" UpdateMethod="Update">
            <SelectParameters>
                <asp:QueryStringParameter DbType="Guid" DefaultValue="" Name="GeoFenceID" QueryStringField="ApplicationGeoFenceID" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
