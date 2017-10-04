<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="CustomerDetails.aspx.vb" Inherits="FMS.WEB.CustomerDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <link href="../../Content/grid/bootstrap.css" rel="stylesheet">
    <link href="../../Content/grid/grid.css" rel="stylesheet">

    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblCustomerName" runat="server" Text="Customer&nbsp;Name:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtCustomerName" runat="server" Width="260px" MaxLength="50"></dx:ASPxTextBox>
            </div>
            <div class="col-md-1">
                <dx:ASPxButton ID="btnViewSites" runat="server" Text="View Sites"></dx:ASPxButton>
            </div>
            <div class="col-md-1">
                <dx:ASPxTextBox ID="txtViewID" runat="server" Text="" Width="50px"></dx:ASPxTextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblAddressLine1" runat="server" Text="Address&nbsp;Line&nbsp;1:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtAddressLine1" runat="server" Width="260px" MaxLength="50"></dx:ASPxTextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblAddressLine2" runat="server" Text="Address&nbsp;Line&nbsp;2:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtAddressLine2" runat="server" Width="260px" MaxLength="50"></dx:ASPxTextBox>
            </div>
        </div>
        <div class="row ">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblSuburb" runat="server" Text="Suburb:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-5">
                <div class="container">
                    <div class="row row-md-margin-top">
                        <dx:ASPxTextBox ID="txtSuburb" runat="server" Width="111px" MaxLength="22"></dx:ASPxTextBox>&nbsp;
                        <dx:ASPxLabel ID="lblState" runat="server" Text="State:"></dx:ASPxLabel>&nbsp;
                        <dx:ASPxComboBox ID="cbState" runat="server" Width="112px" Height="20px"></dx:ASPxComboBox>&nbsp;&nbsp;&nbsp;&nbsp;
                        <dx:ASPxLabel ID="lblPCode" runat="server" Text="P/Code:"></dx:ASPxLabel>&nbsp;
                        <dx:ASPxTextBox ID="txtPCode" runat="server" Width="50px" MaxLength="22"></dx:ASPxTextBox>&nbsp;
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblCustCommencementDate" runat="server" Text="Customer&nbsp;Commencement&nbsp;Date:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-5">
                <div class="container">
                    <div class="row row-md-margin-top">
                        <dx:ASPxDateEdit ID="dtCustCommencementDate" runat="server"></dx:ASPxDateEdit>&nbsp;
                        <dx:ASPxLabel ID="lblYears" runat="server" Text="Years:"></dx:ASPxLabel>&nbsp;
                        <dx:ASPxTextBox ID="txtYears" runat="server" Width="50px" MaxLength="22"></dx:ASPxTextBox>&nbsp;
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblCustomerRating" runat="server" Text="Customer&nbsp;Rating:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxComboBox ID="cbCustomerRating" runat="server" Width="170px" Height="20px"></dx:ASPxComboBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblZone" runat="server" Text="Zone:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxComboBox ID="cbZone" runat="server" Width="170px" Height="20px"></dx:ASPxComboBox>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-3">
                <dx:ASPxLabel ID="lblPerAnnumValue" runat="server" Text="Per Annum Value:" ForeColor="Blue" Font-Bold="true" Width="300px"></dx:ASPxLabel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblCustomerContactName" runat="server" Text="Customer&nbsp;Contact&nbsp;Name:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtCustomerContactName" runat="server" Width="170px" MaxLength="50"></dx:ASPxTextBox>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtPerAnnumValue" runat="server" Width="100px" MaxLength="50"></dx:ASPxTextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblCustomerPhone" runat="server" Text="Customer&nbsp;Phone:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtCustomerPhone" runat="server" Width="170px" MaxLength="50"></dx:ASPxTextBox>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-3">
                <dx:ASPxButton ID="btnUpdateValue" runat="server" Text="Update Value"></dx:ASPxButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblCustomerMobile" runat="server" Text="Customer&nbsp;Mobile:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtCustomerMobile" runat="server" Width="170px" MaxLength="50"></dx:ASPxTextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblCustomerFax" runat="server" Text="Customer&nbsp;Fax:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtCustomerFax" runat="server" Width="170px" MaxLength="50"></dx:ASPxTextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblCustomerComments" runat="server" Text="Customer&nbsp;Comments:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtCustomerComments" runat="server" Width="270px" Height="100px"></dx:ASPxTextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblCustomerAgentName" runat="server" Text="Customer&nbsp;Agent&nbsp;Name:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxComboBox ID="cbCustomerAgentName" runat="server" Width="260px" Height="20px"></dx:ASPxComboBox>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-1 col-md-1_5">
                <dx:ASPxButton ID="btnAddNew" runat="server" Text="Add New"></dx:ASPxButton>
            </div>
             <div class="col-md-1 col-md-1_5">
                <dx:ASPxButton ID="btnExit" runat="server" Text="Exit"></dx:ASPxButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblMYOBCustomerNumber" runat="server" Text="MYOB&nbsp;Customer&nbsp;Number:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-3">
                <dx:ASPxTextBox ID="txtMYOBCustomerNumber" runat="server" Width="60px" MaxLength="50"></dx:ASPxTextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-1_5">
                <dx:ASPxLabel ID="lblInactiveCustomer" runat="server" Text="Inactive&nbsp;Customer:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-1 col-md-1_5">
                <dx:ASPxCheckBox ID="chkInActiveCustomer" runat="server" ></dx:ASPxCheckBox>
            </div>
            <div class="col-md-1 col-md-1_5">
                <dx:ASPxLabel ID="lblExcludeFuelLevy" runat="server" Text="Exclude&nbsp;Fuel&nbsp;Levy:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-1 col-md-1_5">
                <dx:ASPxCheckBox ID="chkExcludeFuelLevy" runat="server" ></dx:ASPxCheckBox>
            </div>
            <div class="col-md-1 col-md-1_5">
                <dx:ASPxLabel ID="lblRateIncrease" runat="server" Text="Rate&nbsp;Increase:" Width="100px"></dx:ASPxLabel>
            </div>
            <div class="col-md-1 col-md-1_5">
                <dx:ASPxComboBox ID="cbRateIncrease" runat="server" Width="100px" Height="20px"></dx:ASPxComboBox>
            </div>
        </div>
    </div>
</asp:Content>
