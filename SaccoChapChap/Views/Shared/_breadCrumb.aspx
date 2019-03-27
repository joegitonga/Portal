<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SaccoChapChap.Models.breadCrumbModel>" %>
	<div class="row">
		<div class="col-lg-12">
            <% foreach(var breadObj in Model.breadcrumb)  {%> 
			        <h3 class="page-header"><i class="fa fa fa-bars"></i><%: breadObj.controller%></h3>
			        <ol class="breadcrumb">
				        <li><i class="fa fa-home"></i><a href="/"> Home</a></li>
				        <li><i class="fa fa-bars"></i><%: breadObj.controller%></li>
				        <li><i class="fa fa-square-o"></i><%: breadObj.action%></li>
			        </ol>
            <%}%>    
		</div>
	</div>
