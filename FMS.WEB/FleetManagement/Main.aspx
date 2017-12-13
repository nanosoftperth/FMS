﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainMenu.master" CodeBehind="Main.aspx.vb" Inherits="FMS.WEB.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/font-awesome/font-awesome.css" rel="stylesheet" />
    <link href="../Content/FleetmanagementMenu/FleetManagementMenu.css" rel="stylesheet" />
    <script src="../Content/javascript/jquery-3.1.0.min.js"></script>
    <script src="../Content/javascript/tether.js"></script>
    <script src="../Content/javascript/bootstrap.min.js"></script>
    <script src="../Content/javascript/FleetmanagementMenu/FleetManagementMenu.js"></script>
    <script>
        $(function () {
            function AdjustWindowHeightAndWidth() {
                var windowHeight = $(window).height() - $(".headerTop").height() - 20;
                var windowWidth = $(window).width() - $('.nav-side-menu').width() - 20;
                $('#mainContent').css({
                    "height": windowHeight
                });
                $('#mainFrame').css({
                    "height": windowHeight,
                    "width": windowWidth
                });
            }

            $('a[target="iframeMenu"]').click(function (event) {
                LoadingPanel.SetText("");
                LoadingPanel.Show();
            });

            $('#mainFrame').on("load", function () {
                LoadingPanel.Hide();
            });
            
            AdjustWindowHeightAndWidth();
            $(window).resize(function () {
                AdjustWindowHeightAndWidth();
            })
        })
        
    </script>
	<div class="nav-side-menu" id="mainContent">
		<i class="fa fa-bars fa-2x toggle-btn" data-toggle="collapse" data-target="#menu-content"></i>
	  
			<div class="menu-list">
	  
				<ul id="menu-content" class="menu-content collapse out">
					<li>
					  <a href="CustomerDetailsMain.aspx" target="iframeMenu">
					  <i class="fa fa-address-card-o"></i> Customers Details 
					  </a>
					</li>
	                 <li>
					  <a href="SiteDetailsMain.aspx" target="iframeMenu">
					  <i class="fa fa fa-sitemap"></i> Sites
					  </a>
					</li>
					<li  data-toggle="collapse" data-target="#products" class="collapsed">
					  <a href="#"><i class="fa fa-wrench fa-lg"></i> Maintenence <span class="arrow"></span></a>
					</li>
					<ul class="sub-menu collapse" id="products">
						<li><a href="Services.aspx" target="iframeMenu">Services</a></li>
						<li><a href="#">Driver Details</a></li>
						<li><a href="#">Runs</a></li>
						<li><a href="IndustryGroups.aspx" target="iframeMenu">Industry Groups</a></li>
						<li><a href="InvoicingFrequency.aspx" target="iframeMenu">Invoicing Frequency</a></li>
						<li><a href="#">Public Holiday Register</a></li>
						<li><a href="CustomerAgents.aspx" target="iframeMenu">Customer Agents</a></li>
						<li><a href="SalesPerson.aspx" target="iframeMenu">Sales Persons</a></li>
						<li><a href="#">User Security</a></li>
						<li><a href="FuelLevy.aspx" target="iframeMenu">Fuel Levy</a></li>
                        <li><a href="ContractCeaseReasons.aspx" target="iframeMenu">Contract Cease Reasons</a></li>
						<li><a href="ContractPreviousSuppliers.aspx" target="iframeMenu">Contract Previous Suppliers</a></li>
						<li><a href="CIRReasons.aspx" target="iframeMenu">CIR Reasons</a></li>
						<li><a href="RunFNCycles.aspx" target="iframeMenu">Run F/N Cycles</a></li>
						<li><a href="#">Turn On/Off Auditing</a></li>
						<li><a href="AuditChangeReasons.aspx" target="iframeMenu">Audit Change Reasons</a></li>
						<li><a href="EstablishZones.aspx" target="iframeMenu">Zones</a></li>
                        <li><a href="ServiceFrequency.aspx" target="iframeMenu">Service Frequency</a></li>
						<li><a href="CustomerRating.aspx" target="iframeMenu">Cust Rating</a></li>
						<li><a href="RateIncreases.aspx" target="iframeMenu">Rate Increases</a></li>
					</ul>
	
	
					<li data-toggle="collapse" data-target="#service" class="collapsed">
					  <a href="#"><i class="fa fa-file-text fa-lg"></i> Reports <span class="arrow"></span></a>
					</li>  
					<ul class="sub-menu collapse" id="service">
					  <li><a href="ContractRenewalsReport.aspx" target="iframeMenu">Contract Renewals</a></li>
					  <li>Quick View By Suburb</li>
                      <li><a href="ServiceListReport.aspx" target="iframeMenu">Service List</a></li>
					  <li><a href="RunListingReport.aspx" target="iframeMenu">Run Listing</a></li>
					  <li><a href="RunValuesReport.aspx" target="iframeMenu">Run Values</a></li>
                      <li><a href="RunValueSummaryReport.aspx" target="iframeMenu">Run Value Summary</a></li>
					  <li><a href="ServiceSummaryReport.aspx" target="iframeMenu">Service Summary</a></li>
					  <li>Annual Analysis</li>
                      <li><a href="LengthOfServiceReport.aspx" target="iframeMenu">Length of Service</a></li>
					  <li><a href="CustomerByCustZoneReport.aspx" target="iframeMenu">Customers By Cust Zone</a></li>
					  <li><a href="CustomerContactDetailsReport.aspx" target="iframeMenu">Customer Contract Details</a></li>
                      <li>Site List</li>
					  <li><a href="IndustryListReport.aspx" target="iframeMenu">Industry List</a></li>
					  <li>Revenue Report By Zone</li>
                      <li>Gains & Losses (Sales)</li>
					  <li><a href="DriversLicenseExpiryReport.aspx" target="iframeMenu">Driver License Expiry</a></li>
					  <li>Per Annum Value</li>
					  <li><a href="SitesWithNoContractsReport.aspx" target="iframeMenu">Sites With No Contracts</a></li>
					  <li><a href="SiteBySiteZoneReport.aspx" target="iframeMenu">Sites By Site Zone</a></li>
					</ul>
                    <li data-toggle="collapse" data-target="#AuditReport" class="collapsed">
                        <a href="#"><i class="fa fa-list-alt fa-lg"></i> Audit Report & G & L<span class="arrow"></span></a>
                    </li>
                    <ul class="sub-menu collapse" id="AuditReport">
                        <li><a href="GainsAndLossesReport.aspx" target="iframeMenu">Gains & Losses Units Report</a></li>
                        <li><a href="GainsAndLossesPerAnnumReport.aspx" target="iframeMenu">Gains & Losses PA Change Report</a></li>
                        <li><a href="StandardAuditReport.aspx" target="iframeMenu">Standard Audit Report</a></li>
                    </ul>
                    <li data-toggle="collapse" data-target="#Invoicing" class="collapsed">
                        <a href="#"><i class="fa fa-list-alt fa-lg"></i> Invoicing Reports<span class="arrow"></span></a>
                    </li>
                    <ul class="sub-menu collapse" id="Invoicing">
                        <li><a href="InvoiceBasicCheckReport.aspx" target="iframeMenu">Basic Details Check</a></li>
                        <li><a href="MYOBCustomerInvoiceReport.aspx" target="iframeMenu">Print Customer Invoice</a></li>
                    </ul>
					<li data-toggle="collapse" data-target="#new" class="collapsed">
					  <a href="#"><i class="fa fa-sticky-note fa-lg"></i> Other Processes <span class="arrow"></span></a>
					</li>
					<ul class="sub-menu collapse" id="new">
					  <li>Generate Run Sheets</li>
					  <li>Produce MYOB File</li>
					</ul>
				</ul>
		 </div>
	</div>
    <%--<div style="float:left; padding-left:305px; " >
        <iframe name="iframeMenu" onload="this.width=screen.width+140;this.height=(screen.height-15);" style="overflow-y: scroll"></iframe>
    </div>--%>
    <%--<div style="float:left; padding-left:305px; " >
        <iframe name="iframeMenu"  style="overflow-y: auto; height:80vh; width:158vh" id="mainFrame"></iframe>
    </div>--%>
    <div style="float:left; padding-left:305px; " >
        <iframe name="iframeMenu"  style="overflow-y: auto;" id="mainFrame"></iframe>
    </div>
    <div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" 
            Modal="True">
            <Image Url="../Content/Images/drop.gif"/>
        </dx:ASPxLoadingPanel>
    </div>
</asp:Content>