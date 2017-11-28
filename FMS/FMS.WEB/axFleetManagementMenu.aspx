<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainMenu.master" CodeBehind="axFleetManagementMenu.aspx.vb" Inherits="FMS.WEB.axFleetManagementMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/font-awesome/font-awesome.css" rel="stylesheet" />
    <link href="Content/FleetmanagementMenu/FleetManagementMenu.css" rel="stylesheet" />
    <script src="Content/javascript/jquery-3.1.0.min.js"></script>
    <script src="Content/javascript/tether.js"></script>
    <script src="Content/javascript/bootstrap.min.js"></script>
    <script src="Content/javascript/FleetmanagementMenu/FleetManagementMenu.js"></script>
	<div class="nav-side-menu" id="mainContent">
		<i class="fa fa-bars fa-2x toggle-btn" data-toggle="collapse" data-target="#menu-content"></i>
	  
			<div class="menu-list">
	  
				<ul id="menu-content" class="menu-content collapse out">
					<li>
					  <a href="axCustomerDetailsMain.aspx" target="iframeMenu">
					  <i class="fa fa-address-card-o"></i> Customers Details 
					  </a>
					</li>
	                 <li>
					  <a href="axSiteDetailsMain.aspx" target="iframeMenu">
					  <i class="fa fa fa-sitemap"></i> Sites
					  </a>
					</li>
					<li  data-toggle="collapse" data-target="#products" class="collapsed">
					  <a href="#"><i class="fa fa-wrench fa-lg"></i> Maintenence <span class="arrow"></span></a>
					</li>
					<ul class="sub-menu collapse" id="products">
						<li><a href="#">Services</a></li>
						<li><a href="#">Driver Details</a></li>
						<li><a href="#">Runs</a></li>
						<li><a href="axIndustryGroups.aspx" target="iframeMenu">Industry Groups</a></li>
						<li><a href="axInvoicingFrequency.aspx" target="iframeMenu">Invoicing Frequency</a></li>
						<li><a href="#">Public Holiday Register</a></li>
						<li><a href="axCustomerAgents.aspx" target="iframeMenu">Customer Agents</a></li>
						<li><a href="#">Sales Persons</a></li>
						<li><a href="#">User Security</a></li>
						<li><a href="#">Fuel Levy</a></li>
                        <li><a href="axContractCeaseReasons.aspx" target="iframeMenu">Contract Cease Reasons</a></li>
						<li><a href="#">Contract Previous Suppliers</a></li>
						<li><a href="axCIRReasons.aspx" target="iframeMenu">CIR Reasons</a></li>
						<li><a href="#">Run F/N Cycles</a></li>
						<li><a href="#">Turn On/Off Auditing</a></li>
						<li><a href="#">Audit Change Reasons</a></li>
						<li><a href="axEstablishZones.aspx" target="iframeMenu">Zones</a></li>
                        <li><a href="#">Service Frequency</a></li>
						<li><a href="axCustomerRating.aspx" target="iframeMenu">Cust Rating</a></li>
						<li><a href="axRateIncreases.aspx" target="iframeMenu">Rate Increases</a></li>
					</ul>
	
	
					<li data-toggle="collapse" data-target="#service" class="collapsed">
					  <a href="#"><i class="fa fa-file-text fa-lg"></i> Reports <span class="arrow"></span></a>
					</li>  
					<ul class="sub-menu collapse" id="service">
					  <li><a href="axContractRenewalsReport.aspx" target="iframeMenu">Contract Renewals</a></li>
					  <li>Quick View By Suburb</li>
                      <li><a href="axServiceListReport.aspx" target="iframeMenu">Service List</a></li>
					  <li>Run Listing</li>
					  <li><a href="axRunValuesReport.aspx" target="iframeMenu">Run Values</a></li>
                      <li><a href="axRunValueSummaryReport.aspx" target="iframeMenu">Run Value Summary</a></li>
					  <li><a href="axServiceSummaryReport.aspx" target="iframeMenu">Service Summary</a></li>
					  <li>Annual Analysis</li>
                      <li><a href="axLengthOfServiceReport.aspx" target="iframeMenu">Length of Service</a></li>
					  <li><a href="axCustomerByCustZoneReport.aspx" target="iframeMenu">Customers By Cust Zone</a></li>
					  <li><a href="axCustomerContactDetailsReport.aspx" target="iframeMenu">Customer Contract Details</a></li>
                      <li>Site List</li>
					  <li><a href="axIndustryListReport.aspx" target="iframeMenu">Industry List</a></li>
					  <li>Revenue Report By Zone</li>
                      <li>Gains & Losses (Sales)</li>
					  <li><a href="axDriversLicenseExpiryReport.aspx" target="iframeMenu">Driver License Expiry</a></li>
					  <li>Per Annum Value</li>
					  <li><a href="axSitesWithNoContractsReport.aspx" target="iframeMenu">Sites With No Contracts</a></li>
					  <li><a href="axSiteBySiteZoneReport.aspx" target="iframeMenu">Sites By Site Zone</a></li>
					</ul>
                    <li data-toggle="collapse" data-target="#AuditReport" class="collapsed">
                        <a href="#"><i class="fa fa-list-alt fa-lg"></i> Audit Report & G & L<span class="arrow"></span></a>
                    </li>
                    <ul class="sub-menu collapse" id="AuditReport">
                        <li><a href="axGainsAndLossesReport.aspx" target="iframeMenu">Gains & Losses Units Report</a></li>
                        <li><a href="axGainsAndLossesPerAnnumReport.aspx" target="iframeMenu">Gains & Losses PA Change Report</a></li>
                        <li><a href="axStandardAuditReport.aspx" target="iframeMenu">Standard Audit Report</a></li>
                    </ul>
                    <li data-toggle="collapse" data-target="#Invoicing" class="collapsed">
                        <a href="#"><i class="fa fa-list-alt fa-lg"></i> Invoicing Reports<span class="arrow"></span></a>
                    </li>
                    <ul class="sub-menu collapse" id="Invoicing">
                        <li><a href="axInvoiceBasicCheckReport.aspx" target="iframeMenu">Basic Details Check</a></li>
                        <li><a href="axMYOBCustomerInvoiceReport.aspx" target="iframeMenu">Print Customer Invoice</a></li>
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
    <div style="float:left; padding-left:305px; " >
        <iframe name="iframeMenu"  style="overflow-y: auto; height:80vh; width:158vh"></iframe>
    </div>
</asp:Content>
