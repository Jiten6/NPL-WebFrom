<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="LeaveCardAdmin.aspx.cs" Inherits="NewPortal2023.ESS.LeaveCardAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
                            <h3 style="color: white">LEAVE CARD</h3>
                        </header>

                        <div class="panel-body">

                            <!-- Script Manager -->
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                            <!-- Alert -->
                            <div id="divAlert" class="alert alert-danger fade in" runat="server" visible="false">
                                <button data-dismiss="alert" class="close close-sm" type="button">
                                    <i class="fa fa-times"></i>
                                </button>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>

                            <!-- Form -->
                            <div class="form-group row" style="margin: 10px">

                                <!-- Year -->
                                <div class="form-group row">
                                    <div class="col-sm-3">
                                        <label style="font-size: 15px;">Select Year :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Width="200px">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <!-- Month -->
                                <div class="form-group row">
                                    <div class="col-sm-3">
                                        <label style="font-size: 15px;">Select Month :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Width="200px">
                                            <asp:ListItem Value="">--Select Month--</asp:ListItem>
                                             <asp:ListItem Value="ALL">All</asp:ListItem>
                                            <asp:ListItem Value="JAN">January</asp:ListItem>
                                            <asp:ListItem Value="FEB">February</asp:ListItem>
                                            <asp:ListItem Value="MAR">March</asp:ListItem>
                                            <asp:ListItem Value="APR">April</asp:ListItem>
                                            <asp:ListItem Value="MAY">May</asp:ListItem>
                                            <asp:ListItem Value="JUN">June</asp:ListItem>
                                            <asp:ListItem Value="JUL">July</asp:ListItem>
                                            <asp:ListItem Value="AUG">August</asp:ListItem>
                                            <asp:ListItem Value="SEP">September</asp:ListItem>
                                            <asp:ListItem Value="OCT">October</asp:ListItem>
                                            <asp:ListItem Value="NOV">November</asp:ListItem>
                                            <asp:ListItem Value="DEC">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <!-- Download Button -->
                                <div class="form-group row">
                                    <div class="col-sm-3"></div>
                                    <div class="col-sm-3">
                                        <asp:Button
                                            ID="btnDownload"
                                            runat="server"
                                            Text="Download Leave Card"
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
