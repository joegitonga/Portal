<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <%: Html.Action("BreadCrumb", "Account", new { PageCode=4})%>
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
                title: 'Client Maintenance',
                paging: true,
                messages: {
                    editRecord: 'Modify Client Details'
                },
                actions: {
                    listAction: '/Customer/ActiveClients',
                    updateAction: '/Customer/UpdateClientRegister',
                },
                fields: {
                    PINReset: {
                        title: '',
                        width: '5%',
                        sorting: false,
                        edit: false,
                        create: false,
                        display: function (MISData) {
                            var $img = $('<img src="../img/toggle_expand.png" title="Reset PIN" />');
                            //Open child table when user clicks the image
                            $img.click(function () {
                                $('#thebox').jtable('openChildTable',
                                        $img.closest('tr'),
                                        {
                                            paging: true,
                                            title: MISData.record.FirstName + ' ' + MISData.record.OtherNames + ' - Client PIN Reset',
                                            actions: {
                                                listAction: '/Customer/FetchPassHistory?UniqueID=' + MISData.record.UniqueID + '&PasswordType=CLIENT',
                                                createAction: '/Customer/ClientPINChange?UniqueID=' + MISData.record.UniqueID,
                                            },
                                            messages: {
                                                addNewRecord: 'Reset Client PIN'
                                            },
                                            fields: {
                                                ID: {
                                                    key: true,
                                                    create: false,
                                                    edit: false,
                                                    list: false
                                                },
                                                PasswordType: {
                                                    title: '',
                                                    width: '5%',
                                                    create: false,
                                                    edit: false,
                                                    list: false
                                                },
                                                Remarks: {
                                                    title: 'Reasons For PIN Reset',
                                                    width: '5%',
                                                    create: true,
                                                    edit: false,
                                                    list: true
                                                },
                                                CreatedOn: {
                                                    title: 'Changed On',
                                                    width: '10%',
                                                    create: false,
                                                    edit: false,
                                                    list: true
                                                },
                                                CreatedBy: {
                                                    title: 'Changed By',
                                                    width: '10%',
                                                    create: false,
                                                    edit: false,
                                                    list: true
                                                },
                                            }
                                        }, function (data) { //opened handler
                                            data.childTable.jtable('load');
                                        });
                            });
                            //Return image to show on the person row
                            return $img;
                        }
                    },
                    ColumnID: {
                        title: 'Sno',
                        key: true,
                        width: '3%',
                        create: false,
                        edit: false
                    },
                    UniqueID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    ClientID: {
                        title: 'Client ID',
                        width: '10%',
                        create: true,
                        edit: true
                    },
                    FirstName: {
                        title: 'FirstName',
                        width: '10%',
                        create: true,
                        edit: true
                    },
                    OtherNames: {
                        title: 'Other Names',
                        width: '10%',
                        create: true,
                        edit: true
                    },
                    Email: {
                        title: 'Email',
                        width: '10%',
                        create: true,
                        edit: true,
                        list: false
                    },
                    Mobile: {
                        title: 'Mobile',
                        create: true,
                        edit: true,
                        width: '8%'
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
