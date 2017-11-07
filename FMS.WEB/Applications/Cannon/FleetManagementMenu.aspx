﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainMenu.master" CodeBehind="FleetManagementMenu.aspx.vb" Inherits="FMS.WEB.FleetManagementMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
    <link href="../../Content/font-awesome/font-awesome.css" rel="stylesheet" />
    <link href="../../Content/FleetmanagementMenu/FleetManagementMenu.css" rel="stylesheet" />
    <script src="../../Content/javascript/jquery-3.1.0.min.js"></script>
    <script src="../../Content/javascript/tether.js"></script>
    <script src="../../Content/javascript/bootstrap.min.js"></script>
    <script src="../../Content/javascript/FleetmanagementMenu/FleetManagementMenu.js"></script>
 
	<div class="nav-side-menu">
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
						<li><a href="#">Services</a></li>
						<li><a href="#">Driver Details</a></li>
						<li><a href="#">Runs</a></li>
						<li><a href="IndustryGroups.aspx" target="iframeMenu">Industry Groups</a></li>
						<li><a href="InvoicingFrequency.aspx" target="iframeMenu">Invoicing Frequency</a></li>
						<li><a href="#">Public Holiday Register</a></li>
						<li><a href="CustomerAgents.aspx" target="iframeMenu">Customer Agents</a></li>
						<li><a href="#">Sales Persons</a></li>
						<li><a href="#">User Security</a></li>
						<li><a href="#">Fuel Levy</a></li>
                        <li><a href="ContractCeaseReasons.aspx" target="iframeMenu">Contract Cease Reasons</a></li>
						<li><a href="#">Contract Previous Suppliers</a></li>
						<li><a href="CIRReasons.aspx" target="iframeMenu">CIR Reasons</a></li>
						<li><a href="#">Run F/N Cycles</a></li>
						<li><a href="#">Turn On/Off Auditing</a></li>
						<li><a href="#">Audit Change Reasons</a></li>
						<li><a href="EstablishZones.aspx" target="iframeMenu">Zones</a></li>
                        <li><a href="#">Service Frequency</a></li>
						<li><a href="CustomerRating.aspx" target="iframeMenu">Cust Rating</a></li>
						<li><a href="RateIncreases.aspx" target="iframeMenu">Rate Increases</a></li>
					</ul>
	
	
					<li data-toggle="collapse" data-target="#service" class="collapsed">
					  <a href="#"><i class="fa fa-file-text fa-lg"></i> Reports <span class="arrow"></span></a>
					</li>  
					<ul class="sub-menu collapse" id="service">
					  <li>Contract Renewals</li>
					  <li>Quick View By Suburb</li>
					  <li>Audit Report & G & L</li>
                      <li>Service List</li>
					  <li>Run Listing</li>
					  <li>Run Values</li>
                      <li>Run Value Summary</li>
					  <li>Service Summary</li>
					  <li>Annual Analysis</li>
                      <li>Length of Service</li>
					  <li>Customers By Cust Zone</li>
					  <li>Customer Contract Details</li>
                      <li>Site List</li>
					  <li>Industry List</li>
					  <li>Revenue Report By Zone</li>
                      <li>Gains & Losses (Sales)</li>
					  <li>Driver License Expiry</li>
					  <li>Per Annum Value</li>
                      <li>Invoicing</li>
					  <li>Sites With No Contracts</li>
					  <li>Sites By Site Zone</li>
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
    <div style="float:left; padding-left:305px; " >
        <iframe name="iframeMenu" onload="this.width=screen.width+140;this.height=(screen.height-15);" style="overflow-y: scroll"></iframe>
    </div>
</asp:Content>
