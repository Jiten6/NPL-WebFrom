<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="NipponDashboard.aspx.cs" Inherits="NewPortal2023.ESS.NipponDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .labels {
            float: left;
            padding-top: 7px;
            margin-bottom: 0;
            text-align: left;
            font-weight: 300;
            font-size: 15px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function showLoader() {
            document.getElementById("loader").style.display = 'block';
        }

    </script>
    <script type="text/javascript" src="assets/js/dynamic_table_init.js"></script>
    <script type="text/javascript">
        function clearFileInputField(divId) {
            document.getElementById(divId).innerHTML = document.getElementById(tagId).innerHTML;
        }

        function ConfirmDel() {
            return confirm("Are you sure you want to delete this record?");
        }

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Select2);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({
                    format: 'dd-mm-yyyy',
                    autoclose: true
                });
            }

            $('.datepicker').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true
            });

            function Select2(sender, args) {
                $(".select2").select2();
            }

            $(".select2").select2();
        });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <header class="panel-heading">
                DashBoard
                       
            </header>
            <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                <button data-dismiss="alert" class="close close-sm" type="button">
                    <i class="fa fa-times"></i>
                </button>
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </div>
            <div id="divFinMakerAssignDataPoint" runat="server" visible="true">

                <div class="row">

                    <div class="col-md-4">
                        <div class="mini-stat clearfix">
                            <span class="mini-stat-icon orange"><i class="fa fa-edit"></i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:LinkButton ID="lnkCandidateRegister" OnClick="lnkCandidateRegister_Click" runat="server"></asp:LinkButton></span>
                                <%-- <asp:Label ID="lblPendingGL" runat="Server" Text='Pending GL' />--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="mini-stat clearfix">
                            <span class="mini-stat-icon orange"><i class="fa fa-edit"></i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:LinkButton ID="lnkMyDocumemt" OnClick="lnkMyDocumemt_Click" runat="server"></asp:LinkButton>

                                </span>
                                <%-- <asp:Label ID="lblPendingGL" runat="Server" Text='Pending GL' />--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4" id="AssignCXO" runat="server">
                        <div class="mini-stat clearfix">
                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:LinkButton ID="lnkAssignCXO" runat="server"></asp:LinkButton></span>
                                <%-- <asp:Label ID="lblApproveGL" runat="Server" Text='Approved GL' />--%>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">




                    <div class="col-md-4">
                        <div class="mini-stat clearfix">
                            <span class="mini-stat-icon orange"><i class="fa fa-edit"></i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:LinkButton ID="linkAssignPendingMaker" runat="server"></asp:LinkButton></span>
                                <%-- <asp:Label ID="lblPendingGL" runat="Server" Text='Pending GL' />--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4" id="AssignHOD" runat="server">
                        <div class="mini-stat clearfix">
                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:LinkButton ID="lnkAssignHOD" runat="server"></asp:LinkButton></span>
                                <%-- <asp:Label ID="lblApprove" runat="Server" Text='Approved GL' />--%>
                            </div>
                        </div>
                    </div>


                    <div class="col-md-4" id="AssignChecker" runat="server">
                        <div class="mini-stat clearfix">
                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                            <div class="mini-stat-info">
                                <span>
                                    <asp:LinkButton ID="lnkAssignChecker" runat="server"></asp:LinkButton></span>
                                <%-- <asp:Label ID="lblApprove" runat="Server" Text='Approved GL' />--%>
                            </div>
                        </div>
                    </div>

                </div>
            </div>




        </section>
    </section>
</asp:Content>
