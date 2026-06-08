<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="LeaveHistoryAdmin.aspx.cs" Inherits="NewPortal2023.ESS.LeaveHistoryAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $('.datepicker').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true
            });

        });

        function showLoader1() {
            document.getElementById("loader1").style.display = 'block';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <!-- Header -->
                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">LEAVE HISTORY</h3>
                        </header>

                        <div class="panel-body">

                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                            <!-- Alert -->
                            <div id="divAlert" class="alert alert-danger fade in" runat="server" visible="false">
                                <button data-dismiss="alert" class="close close-sm" type="button">
                                    <i class="fa fa-times"></i>
                                </button>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>

                            <br />
                             <br />
                             <br /> 





                            <!-- Form -->
                            <div class="form-group row" style="margin: 10px">

                                <!-- From Date -->
                                <div class="form-group row">
                                    <div class="col-sm-3">
                                        <label style="font-size: 15px;">From Date :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox
                                            ID="txtFromDate"
                                            runat="server"
                                            CssClass="form-control datepicker"
                                            Width="200px"
                                            placeholder="dd-mm-yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>

                                <!-- To Date -->
                                <div class="form-group row">
                                    <div class="col-sm-3">
                                        <label style="font-size: 15px;">To Date :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox
                                            ID="txtToDate"
                                            runat="server"
                                            CssClass="form-control datepicker"
                                            Width="200px"
                                            placeholder="dd-mm-yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>

                                <!-- Button -->
                                <div class="form-group row">
                                    <div class="col-sm-3"></div>
                                    <div class="col-sm-3">
                                        <asp:Button
                                            ID="btnDownload"
                                            runat="server"
                                            Text="Download Leave History"
                                            CssClass="btn btn-success"
                                            OnClick="btnDownload_Click"
                                            OnClientClick="showLoader1();" />
                                    </div>
                                </div>

                            </div>

                            <!-- Loader -->
                            <div id="loader1" style="display: none; text-align: center;">
                                <img src="../images/loader.gif" />
                            </div>

                        </div>

                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
