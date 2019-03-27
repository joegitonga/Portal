<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<SaccoChapChap.Models.RegisterUserModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <%: Html.Action("BreadCrumb", "Account", new { PageCode=1})%>
    <!-- page start-->
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    User Registration
                </header>
                <div class="panel-body">

                    <% if (ViewData["Error"] != null)
                       {%>      
                        <div class="alert alert-block alert-danger fade in">
                            <button data-dismiss="alert" class="close close-sm" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <strong>Error!</strong> <%: ViewData["Error"] %> 
                        </div> 
                    <%}
                    if (TempData["Success"] != null)
                    {%> 
                        <div class="alert alert-success fade in">
                            <button data-dismiss="alert" class="close close-sm" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <strong>Success!</strong> User has been saved.
                        </div>
                    <%}%> 

                    <div class="form">
                    <% using (Html.BeginForm("RegisterUser", "Security", FormMethod.Post, new { id = "feedback_form", @class = "form-validate form-horizontal" }))
                       { %>
                            <div class="form-group ">
                                <label for="UserName" class="control-label col-lg-2">User Name: <span class="required">*</span></label>
                                <div class="col-lg-2">
                                    <%: Html.TextBoxFor(model => model.UserName, new { tabindex = "1", @class="form-control", required = "required", @placeholder="User Name"})%>
                                </div>
                            </div>
                                                
                            <div class="form-group ">
                                <label for="FirstName" class="control-label col-lg-2">First Name: <span class="required">*</span></label>
                                <div class="col-lg-4">
                                    <%: Html.TextBoxFor(model => model.FirstName, new { tabindex = "2", @class="form-control", required = "required", @placeholder="First Name"})%>
                                </div>
                            </div>

                            <div class="form-group ">
                                <label for="OtherNames" class="control-label col-lg-2">Other Names:<span class="required">*</span></label>
                                <div class="col-lg-4">
                                    <%: Html.TextBoxFor(model => model.OtherNames, new { tabindex = "3", @class="form-control", required = "required", @placeholder="Other Names"})%>
                                </div>
                            </div>

                            <div class="form-group ">
                                <label for="Email" class="control-label col-lg-2">Email: <span class="required">*</span></label>
                                <div class="col-lg-4">
                                    <%: Html.TextBoxFor(model => model.Email, new { tabindex = "4",type="email", @class="form-control", required = "required", @placeholder="Email"})%>
                                </div>
                            </div> 

                            <div class="form-group ">
                                <label for="Mobile" class="control-label col-lg-2">Mobile: <span class="required">*</span></label>
                                <div class="col-lg-4">
                                    <%: Html.TextBoxFor(model => model.Mobile, new { tabindex = "5", @class="form-control", required = "required", @placeholder="Mobile"})%>
                                </div>
                            </div>

                            <div class="form-group ">
                                <label for="Password" class="control-label col-lg-2">Password: <span class="required">*</span></label>
                                <div class="col-lg-4">
                                    <%: Html.TextBoxFor(model => model.Password, new { tabindex = "6",type="password", @class="form-control", required = "required", @placeholder="Password"})%>
                                </div>
                            </div> 

                            <div class="form-group ">
                                <label for="ConfirmPassword" class="control-label col-lg-2">Confirm Password: <span class="required">*</span></label>
                                <div class="col-lg-4">
                                    <%: Html.TextBoxFor(model => model.ConfirmPassword, new { tabindex = "7", @class="form-control",type="password", required = "required", @placeholder="Confirm Password"})%>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <button class="btn btn-primary" tabindex="8" type="submit">Save New User</button>
                                    <button class="btn btn-default" type="button">Cancel</button>
                                </div>
                            </div>
                        <% } %>
                    </div>

                </div>
            </section>
        </div>
    </div>
    <!-- page end-->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptsSection" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#UserName').focus();
        });
    </script>
</asp:Content>
