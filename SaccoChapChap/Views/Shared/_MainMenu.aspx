<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SaccoChapChap.Models.UserAccessRights>" %>
      <aside>
          <div id="sidebar"  class="nav-collapse ">
              <!-- sidebar menu start-->
              <ul class="sidebar-menu">    
                    <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                        <% if (menuObj1.TaskID == 2 && menuObj1.TaskStatus == 1)
                        {%>     
                          <li class="">
                              <a class="" href="/Dashboard/Index">
                                  <i class="icon_house_alt"></i>
                                  <span><%: menuObj1.TaskName%></span>
                              </a>
                          </li>
                        <%}%>  
                    <%}%>                                

                  <%--Registration--%>
                <% foreach(var menuObj in Model.moduleaccess)  {%>
                    <% if (menuObj.TaskID == 3 && menuObj.TaskStatus == 1)
                   {%>
                  <li class="sub-menu ">
                      <a href="javascript:;" class="">
                          <i class="icon_desktop"></i>
                          <span><%: menuObj.TaskName%></span>
                          <span class="menu-arrow arrow_carrot-right"></span>
                      </a>
                      <ul class="sub">  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 4 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "MaintainClient", "Customer") %></li>    
                                <%}%>  
                            <%}%>        
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 7 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "ClientRegister", "Customer") %></li>    
                                <%}%>  
                            <%}%>  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 9 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "ApproveClient", "Customer") %></li>    
                                <%}%>  
                            <%}%>                                                                                                                                                             
                        </ul>
                    </li>
                    <%}%>
                <%}%>
                 
                   <%--Sales--%>
                <% foreach(var menuObj in Model.moduleaccess)  {%>
                    <% if (menuObj.TaskID == 54 && menuObj.TaskStatus == 1)
                   {%>
                  <li class="sub-menu ">
                      <a href="javascript:;" class="">
                          <i class="icon_piechart"></i>
                          <span><%: menuObj.TaskName%></span>
                          <span class="menu-arrow arrow_carrot-right"></span>
                      </a>
                      <ul class="sub">  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 55 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "SalesListing", "Sales") %></li>    
                                <%}%>  
                            <%}%>        
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 56 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "SalesListing", "Sales") %></li>    
                                <%}%>  
                            <%}%>                                                                                                                                                               
                        </ul>
                    </li>
                    <%}%>
                <%}%>

                  <%--Security--%>
                <% foreach(var menuObj in Model.moduleaccess)  {%>
                    <% if (menuObj.TaskID == 12 && menuObj.TaskStatus == 1)
                   {%>
                  <li class="sub-menu ">
                      <a href="javascript:;" class="">
                          <i class="icon_documents_alt"></i>
                          <span><%: menuObj.TaskName%></span>
                          <span class="menu-arrow arrow_carrot-right"></span>
                      </a>
                      <ul class="sub">  
<%--                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 13 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "Profile", "Security") %></li>    
                                <%}%>  
                            <%}%>   --%>     
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 15 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "MaintainUser", "Security") %></li>    
                                <%}%>  
                            <%}%>  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 18 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "CreateUser", "Security") %></li>    
                                <%}%>  
                            <%}%>        
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 20 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "ApproveUser", "Security") %></li>    
                                <%}%>  
                            <%}%>                                                                                                                                                               
                        </ul>
                    </li>
                    <%}%>
                <%}%>
                 
                  <%--SMS Management--%>
                <% foreach(var menuObj in Model.moduleaccess)  {%>
                    <% if (menuObj.TaskID == 23 && menuObj.TaskStatus == 1)
                   {%>
                  <li class="sub-menu ">
                      <a href="javascript:;" class="">
                          <i class="icon_documents_alt"></i>
                          <span><%: menuObj.TaskName%></span>
                          <span class="menu-arrow arrow_carrot-right"></span>
                      </a>
                      <ul class="sub">  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 24 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "AdHoc", "Messaging") %></li>    
                                <%}%>  
                            <%}%>  
                          <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID ==30 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "UploadExcelFile", "Messaging") %></li>    
                                <%}%>  
                            <%}%>    
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 27 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink("Disbursement Alerts", "Disbursement", "Messaging") %></li>    
                             <%}%>
                            <%}%> 
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 34 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink("Arrears Notifications", "Arrears", "Messaging") %></li>    
                                <%}%>  
                            <%}%> 
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 34 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink("PDC Processing", "Pdc", "Messaging") %></li>    
                                <%}%>  
                            <%}%>  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 35 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "SmsListing", "Messaging") %></li>    
                                <%}%>  
                            <%}%>   
<%--                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 27 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "BulkSMS", "Messaging") %></li>    
                                <%}%>  
                            <%}%>  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 30 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "Upload", "Messaging") %></li>    
                                <%}%>  
                            <%}%>        
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 33 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "Formats", "Messaging") %></li>    
                                <%}%>  --%> 
                                                                                                                                                                                         
                        </ul>
                    </li>
                    <%}%>
                <%}%>

                  <%--Administration--%>
<%--                <% foreach(var menuObj in Model.moduleaccess)  {%>
                    <% if (menuObj.TaskID == 36 && menuObj.TaskStatus == 1)
                   {%>
                  <li class="sub-menu ">
                      <a href="javascript:;" class="">
                          <i class="icon_desktop"></i>
                          <span><%: menuObj.TaskName%></span>
                          <span class="menu-arrow arrow_carrot-right"></span>
                      </a>
                      <ul class="sub">  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 37 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "Settings", "Admin") %></li>    
                                <%}%>  
                            <%}%>        
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 40 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "SettingA", "Admin") %></li>    
                                <%}%>  
                            <%}%>  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 43 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "SettingB", "Admin") %></li>    
                                <%}%>  
                            <%}%>                                                                                                                                                             
                        </ul>
                    </li>
                    <%}%>
                <%}%>    --%>

                  <%--Audit Trail--%>
<%--                <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                    <% if (menuObj1.TaskID == 46 && menuObj1.TaskStatus == 1)
                    {%>     
                        <li class="">
                            <a class="" href="Admin/AuditTrail">
                                <i class="icon_piechart"></i>
                                <span><%: menuObj1.TaskName%></span>
                            </a>
                        </li>
                    <%}%>  
                <%}%>--%>

                  <%--Reports--%>
<%--                <% foreach(var menuObj in Model.moduleaccess)  {%>
                    <% if (menuObj.TaskID == 47 && menuObj.TaskStatus == 1)
                   {%>
                  <li class="sub-menu ">
                      <a href="javascript:;" class="">
                          <i class="icon_desktop"></i>
                          <span><%: menuObj.TaskName%></span>
                          <span class="menu-arrow arrow_carrot-right"></span>
                      </a>
                      <ul class="sub">  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 48 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "Report1", "Reports") %></li>    
                                <%}%>  
                            <%}%>        
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 49 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "Report2", "Admin") %></li>    
                                <%}%>  
                            <%}%>  
                            <% foreach(var menuObj1 in Model.moduleaccess)  {%> 
                                <% if (menuObj1.TaskID == 50 && menuObj1.TaskStatus == 1)
                               {%>                              
                                    <li><%: Html.ActionLink(menuObj1.TaskName, "Report3", "Admin") %></li>    
                                <%}%>  
                            <%}%>                                                                                                                                                             
                        </ul>
                    </li>
                    <%}%>
                <%}%> --%>   
                  
              </ul>
              <!-- sidebar menu end-->
          </div>
      </aside>
