<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <%: Html.Action("BreadCrumb", "Account", new { PageCode=5})%>
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
                title: 'User Supervision/Approval',
                paging: true,
                messages: {
                    editRecord: 'Approve Registered User'
                },
                actions: {
                    listAction: '/Security/UnsupervisedUsers',
                    updateAction: '/Security/SuperviseUser',
                },
                fields: {
                    ColumnID: {
                        title: 'Sno',
                        key: true,
                        width: '3%',
                        create: false,
                        edit: false
                    },
                    UserID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    UserName: {
                        key: true,
                        title: 'Login Name',
                        width: '8%',
                        create: false,
                        edit: false
                    },
                    FirstName: {
                        key: true,
                        title: 'FirstName',
                        width: '10%',
                        create: false,
                        edit: false
                    },
                    OtherNames: {
                        key: true,
                        title: 'Other Names',
                        width: '10%',
                        create: false,
                        edit: false
                    },
                    Password: {
                        key: true,
                        title: 'Password',
                        type: 'password',
                        width: '10%',
                        create: true,
                        edit: false,
                        list: false
                    },
                    ConfirmPassword: {
                        key: true,
                        title: 'Confirm Password',
                        type: 'password',
                        width: '10%',
                        create: true,
                        edit: false,
                        list: false
                    },
                    Email: {
                        key: true,
                        title: 'Email',
                        width: '10%',
                        create: true,
                        edit: false,
                        list: false
                    },
                    Mobile: {
                        key: true,
                        title: 'Mobile',
                        create: true,
                        edit: false,
                        width: '8%'
                    },
                    Remarks: {
                        title: 'Approval Reason',
                        width: '10%',
                        create: false,
                        edit: true,
                        list: false
                    },
                    CreatedOn: {
                        title: 'Date',
                        create: false,
                        edit: false,
                        width: '8%',
                    },
                    CreatedBy: {
                        title: 'Operator',
                        create: false,
                        edit: false,
                        width: '5%'
                    }
                }
            });
            $('#thebox').jtable('load');
        });
    </script>
</asp:Content>
