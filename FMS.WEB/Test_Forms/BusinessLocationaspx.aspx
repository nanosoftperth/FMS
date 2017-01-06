<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusinessLocationaspx.aspx.vb" Inherits="FMS.WEB.BusinessLocationaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        

        <div>

            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsApplicationLocations">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ApplicationLocationID" VisibleIndex="0"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="1"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Longitude" VisibleIndex="3"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Lattitude" VisibleIndex="4"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="5"></dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>


            <asp:ObjectDataSource runat="server" ID="odsApplicationLocations" DataObjectTypeName="FMS.Business.DataObjects.ApplicationLocation" DeleteMethod="Delete" InsertMethod="Update" SelectMethod="GetAllIncludingDefault" TypeName="FMS.Business.DataObjects.ApplicationLocation" UpdateMethod="Update">
                <SelectParameters>
                    <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="ApplicationID"></asp:SessionParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
