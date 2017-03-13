<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.master" CodeBehind="Home.aspx.vb" Inherits="FMS.WEB.Home" %>


    <asp:Content ID="ContentLeft" ContentPlaceHolderID="ContentLeft"  runat="server">

                  
        </asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    
    <div style="float: left;margin-left:200px;margin-top:20px;margin-right:200px;">
                
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" 
                            runat="server" 
                            Width="95%" 
                            EnableTheming="True" 
                            Theme="Moderno" 
                            HeaderText="Nanosofts Fleet Management System" 
                            Height="282px">

                                    <PanelCollection>

                <dx:PanelContent runat="server">
                    <div class="jumbotron">                        
                       <div style="margin:auto;">

                           <asp:Literal runat="server" ID="home_logo">

                            </asp:Literal>

                            

                            </div>    
                        <div>

                            
                               <%--<p class="lead">
                            Fleet Management Systems provides a very-tailored solution to the specific needs of the customer. 
                    This means the system changes to suit your needs, as opposed to you having to change your business, to suit the system.
                        </p>--%>

                              <p   class="lead">


                                  <asp:Literal runat="server" ID="home_message"></asp:Literal>

                            
                                
                            </div>
                            <div align="right">
                                <p><a href="http://www.nanosoft.com.au" class="btn btn-primary btn-large">Learn more &raquo;</a></p>
                            </div>

                    </div>
                </dx:PanelContent>
            </PanelCollection>
            
        </dx:ASPxRoundPanel>

        <div style="width:95%;" class="row">
            <div class="col-md-4">
                 <table>
                    <tr>
                        <td>
                             <h2>Google maps linked</h2>
                            <p>
                                The latest and greatest google maps are used
                                to ensure you are up-to-date on latest traffic conditions
                                and enable quick decisions on who to assign to what job
                            </p>
                             <p>
                    <a class="btn btn-default" href="http://www.businessinsider.com.au/the-reason-google-maps-is-the-best-traffic-app-2015-11">Learn more &raquo;</a>
                </p>
                        </td>
                        <td>
                            <img style="height: 150px;" src="Content/Images/download.jpg" />
                        </td>
                    </tr>

                </table> 
               
            </div>
            <div class="col-md-4">
               

                <table>
                    <tr>
                        <td>
                             <h2>GPS Location - Cheap and easy</h2>
                            <p>
                                Using the latest hardware from the raspberry pi foundation
                                means that the GPS locators on each vehicle are very well supported
                                upgradeable, robust and future-proof.
                            </p>
                             <p>           
                    <a class="btn btn-default" href="http://www.businessinsider.com.au/how-the-internet-of-things-market-will-grow-2014-10">Learn more &raquo;</a>
                </p>
                        </td>
                        <td>
                            <img style="height: 150px;" src="Content/Images/Raspberry_Pi_Logo.svg.png" />

                        </td>
                    </tr>

                </table>     
               
            </div>
            <div class="col-md-4">
               <table>
                    <tr>
                        <td>
                             <h2>Cloud based</h2>
                            <p>
                                As the application is stored in the cloud, this 
                                means you pay $0 hosting costs and avoids difficult
                                installtions and new hardware which has to be bought on site.
                            </p>
                              <p>           
                    <a class="btn btn-default" href="http://online.wsj.com/ad/article/cloudcomputing-changelives">Learn more &raquo;</a>
                </p>
                        </td>
                        <td>
                            <img style="height: 150px;"" src="Content/Images/cloud.jpg" />
                            
                        </td>
                    </tr>

                </table>     
              
            </div>
        </div>
                
    </div>



</asp:Content>

