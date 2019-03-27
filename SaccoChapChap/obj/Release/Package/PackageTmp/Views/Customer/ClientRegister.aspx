<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<SaccoChapChap.Models.RegisterClientModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <%: Html.Action("BreadCrumb", "Account", new { PageCode=2})%>
    <!-- page start-->
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    Client Registration
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
                            <strong>Success!</strong> Client has been saved.
                        </div>
                    <%}%> 


                    <div class="form">
                    <% using (Html.BeginForm("RegisterClient", "Customer", FormMethod.Post, new { id = "feedback_form", @class = "form-validate form-horizontal" }))
                       { %>
                            <div class="form-group ">
                                <label for="ClientID" class="control-label col-lg-2">Customer Code: <span class="required">*</span></label>
                                <div class="col-lg-2">
                                    <%: Html.TextBoxFor(model => model.ClientID, new { tabindex = "1", @class="form-control", required = "required", @placeholder="Customer Code"})%>
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
                            
                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <button class="btn btn-primary" tabindex="6" type="submit">Save</button>
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
        //$(document).ready(function () {

        //    $('#thebox').jtable({
        //        title: 'Client Registration',
        //        paging: true,
        //        messages: {
        //            addNewRecord: 'New Client Registration',
        //        },
        //        actions: {
        //            listAction: '/Customer/FetchClients',
        //            createAction: '/Customer/RegisterClient',
        //        },
        //        fields: {
        //            ColumnID: {
        //                title: 'Sno',
        //                key: true,
        //                width: '3%',
        //                create: false,
        //                edit: false
        //            },
        //            UniqueID: {
        //                key: true,
        //                create: false,
        //                edit: false,
        //                list: false
        //            },
        //            ClientID: {
        //                title: 'Client ID',
        //                width: '10%',
        //                create: true,
        //                edit: true
        //            },
        
        //            FirstName: {
        //                title: 'FirstName',
        //                width: '10%',
        //                create: true,
        //                edit: true
        //            },
        //            OtherNames: {
        //                title: 'Other Names',
        //                width: '10%',
        //                create: true,
        //                edit: true
        //            },
        //            Email: {
        //                title: 'Email',
        //                width: '10%',
        //                create: true,
        //                edit: true,
        //                list: false
        //            },
        //            Mobile: {
        //                title: 'Mobile',
        //                create: true,
        //                edit: true,
        //                width: '8%'
        //            },
        //            CreatedOn: {
        //                title: 'Date',
        //                create: false,
        //                edit: false,
        //                width: '8%',
        //            },
        //            CreatedBy: {
        //                title: 'Operator',
        //                create: false,
        //                edit: false,
        //                width: '5%'
        //            }
        //        }
        //    });
        //    $('#thebox').jtable('load');
        //});
    </script>
</asp:Content>
