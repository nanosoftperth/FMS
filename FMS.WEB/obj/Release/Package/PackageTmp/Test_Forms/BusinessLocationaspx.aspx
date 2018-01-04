<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusinessLocationaspx.aspx.vb" Inherits="FMS.WEB.BusinessLocationaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.24&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g&libraries=places,visualization"></script>


        <script type="text/javascript">

            var initsearchlocationBusinessLocs = function (s, e) {
                var search_input_box = s.inputElement;
                search_input_box.placeholder = "Search Box";
                var searchBox = new google.maps.places.SearchBox(search_input_box);

                searchBox.addListener('places_changed', function () {
                    newItemSelected(searchBox);

                });
            }


            function newItemSelected(searchBox) {

                var places = searchBox.getPlaces();

                if (places.length == 0) {
                    return;
                }

                var nisLat = places[0].geometry.location.lat();
                var nisLng = places[0].geometry.location.lng();

                editLat.SetText(nisLat);
                editLng.SetText(nisLng);
            }

        </script>

        <div>

            <dx:ASPxGridView
                ID="dgvBusinessLocations"
                runat="server"
                AutoGenerateColumns="False"
                DataSourceID="odsApplicationLocations"
                KeyFieldName="ApplicationLocationID"
                Theme="SoftOrange">

                <Columns>
                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="ApplicationLocationID" Visible="False" VisibleIndex="6">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ApplicationID" Visible="False" VisibleIndex="7">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Longitude" VisibleIndex="3" EditCellStyle-CssClass="editLng" PropertiesTextEdit-ClientInstanceName="editLng" >
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Lattitude" VisibleIndex="4" EditCellStyle-CssClass="editLat" PropertiesTextEdit-ClientInstanceName="editLat">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Address"  VisibleIndex="2" PropertiesTextEdit-ClientSideEvents-Init="initsearchlocation">
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="ApplicationImageID" Visible="False" VisibleIndex="8">
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Image" FieldName="ApplicationImageID" VisibleIndex="5">
                        <PropertiesComboBox DataSourceID="odsHomeImages" ImageUrlField="ImgURL" TextField="Name" ValueField="ApplicationImageID">
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                </Columns>
            </dx:ASPxGridView>
        </div>

        <%--========================================================================--%>
        <%--                            OBJECT DATA SOURCES                         --%>
        <%--========================================================================--%>


        <asp:ObjectDataSource runat="server" ID="odsApplicationLocations" DataObjectTypeName="FMS.Business.DataObjects.ApplicationLocation" DeleteMethod="Delete" InsertMethod="Update" SelectMethod="GetAllIncludingDefault" TypeName="FMS.Business.DataObjects.ApplicationLocation" UpdateMethod="Update">
            <SelectParameters>
                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="ApplicationID"></asp:SessionParameter>
            </SelectParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="odsHomeImages" runat="server" SelectMethod="GetAllApplicationImages" TypeName="FMS.Business.DataObjects.ApplicationImage">
            <SelectParameters>
                <asp:QueryStringParameter DbType="Guid" Name="applicationid" QueryStringField="ApplicationID" />
                <asp:Parameter DefaultValue="home" Name="type" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>


    </form>
</body>
</html>
