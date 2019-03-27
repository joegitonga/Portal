<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <%: Html.Action("BreadCrumb", "Account", new { PageCode=12})%>
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
                title: 'Sales',
                paging: true,
                messages: {
                    editRecord: 'Send SMS'
                },
                actions: {
                    listAction: '/Sales/SalesListing',
                },
                fields: {
                    TrxDate: {
                        key: true,
                        title: 'Date',
                        width: '10%',
                        create: true,
                        edit: false
                    },
                    TrxDescription: {
                        title: 'Description',
                        width: '10%',
                        create: true,
                        edit: false
                    },
                    AccountTypeID: {
                        title: 'Account',
                        width: '10%',
                        create: true,
                        edit: false,
                        //list: false
                    },
                    TrxAmount: {
                        title: 'Amount',
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