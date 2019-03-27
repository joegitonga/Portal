<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<SaccoChapChap.Models.AdhocSMSModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <%: Html.Action("BreadCrumb", "Account", new { PageCode=6})%>
    <!-- page start-->
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    Compose New SMS
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
                            <strong>Success!</strong> SMS Message has been qeued.
                        </div>
                    <%}%> 

                    <div class="form">
                    <% using (Html.BeginForm(null, null, FormMethod.Post, new { id = "feedback_form", @class = "form-validate form-horizontal" }))
                       { %>
                            <div class="form-group ">
                                <label for="PhoneNumber" class="control-label col-lg-2">Phone Number(s): <span class="required">*</span></label>
                                <div class="col-lg-8">
                                    <%--<input name="PhoneNumber" id="PhoneNumber" class="tagsinput" />--%>
                                    <%: Html.TextAreaFor(model => model.PhoneNumber, new { tabindex = "1", @class="tagsinput", required = "required", @placeholder="Phone Numbers field seperate with semi-colon e.g. 0722000000;0733111111;0786222555"})%>
                                </div>
                            </div>
                                                
                            <div class="form-group ">
                                <label for="Description" class="control-label col-lg-2">SMS Message: <span class="required">*</span></label>
                                <div class="col-lg-8">
                                    <%: Html.TextAreaFor(model => model.Description, new { tabindex = "2", @class="form-control", required = "required",@maxlength=150, @placeholder="Sms Message"})%>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <button class="btn btn-primary" tabindex="3" type="submit">Schedule SMS</button>
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
            $('#PhoneNumber,#Description').val('');
            // Tags Input
            $("#PhoneNumber").tagsInput();
            $('#PhoneNumber').focus();
        });
    </script>
</asp:Content>
