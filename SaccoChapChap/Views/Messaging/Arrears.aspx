<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <%: Html.Action("BreadCrumb", "Account", new { PageCode=9})%>
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
                title: 'Arrears Notifications',
                paging: true,
                messages: {
                    editRecord: 'Send SMS'
                },
                actions: {
                    listAction: '/Messaging/ArrearsListing', 
                    updateAction: '/Messaging/UpdateArrearsSMS', //UpdateArrearsSMS
                },
                fields: {
                    UniqueID: {
                        title: 'UniqueID',
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    MemberNo: {
                        key: true,
                        title: 'Member No',
                        width: '10%',
                        create: true,
                        edit: false
                    },
                    Name: {
                        title: 'Member Names',
                        width: '10%',
                        create: true,
                        edit: false
                    },
                    PhoneNumber: {
                        title: 'Phone Number',
                        width: '10%',
                        create: true,
                        edit: false
                    },
                    DateDisb: {
                        title: 'Disbursement Date',
                        width: '10%',
                        create: true,
                        edit: false,
                        list: false
                    },
                    AmountDisb: {
                        title: 'Amount Disbursed',
                        create: true,
                        edit: false,
                        width: '8%'
                    },
                    ChequeID: {
                        title: 'Cheque ID',
                        create: false,
                        edit: true,
                        width: '8%',
                    },
                    Remarks: {
                        title: 'Remarks',
                        create: false,
                        edit: true,
                        width: '5%'
                    }
                }
            });
            $('#thebox').jtable('load');
        });
    </script>
</asp:Content>