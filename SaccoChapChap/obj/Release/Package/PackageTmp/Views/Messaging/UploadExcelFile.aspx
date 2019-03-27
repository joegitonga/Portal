<%@ Page Language="C#"  MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<SaccoChapChap.Models.ExcelUploadModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <%: Html.Action("BreadCrumb", "Account", new { PageCode=7})%>

    <!-- page start-->
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    Upload New Excel File
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
                            <strong>Success!</strong> Excel File Has been Uploaded successfully.
                        </div>
                    <%}%> 

                    <div class="form">
                    <% using (Html.BeginForm(null, null, FormMethod.Post, new { id = "feedback_form", @class = "form-validate form-horizontal" , enctype = "multipart/form-data" }))
                       { %>
                          <%: Html.ValidationSummary(true) %>
                        <%: Html.AntiForgeryToken() %>
                            <div class="form-group ">                              
                                 <div class="col-lg-8">
                                    <input type="file" id="dataFile" name="file" />
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
                                    <input class="btn btn-primary" tabindex="3" type="submit" value="Schedule ExcelFile"/>
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
