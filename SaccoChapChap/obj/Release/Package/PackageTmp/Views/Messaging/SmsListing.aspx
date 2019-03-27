<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <%: Html.Action("BreadCrumb", "Account", new { PageCode=10})%>
    <!-- page start-->
    <div class="row">
        <div class="col-lg-12">
            <div id="thebox" class="parent-blue"></div>
        </div>
    </div>
    <!-- page end-->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptsSection" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $('#thebox').jtable({
                title: 'Sent SMS',
                paging: true,
                messages: {
                    editRecord: 'Send SMS'
                },
                actions: {
                    listAction: '/Messaging/SmsListing',
                },
                fields: {
                    PhoneNumber: {
                        key: true,
                        title: 'PhoneNumber',
                        width: '10%',
                        create: true,
                        edit: false
                    },
                    Description: {
                        title: 'Description',
                        width: '10%',
                        create: true,
                        edit: false
                    },
                    Status: {
                        title: 'Status',
                        width: '10%',
                        create: true,
                        edit: false,
                        //list: false
                    },
                    CreatedOn: {
                        title: 'Created On',
                        width: '10%',
                        create: true,
                        edit: false
                    },
                }
            });
            $('#thebox').jtable('load');
        });
    </script>
</asp:Content>