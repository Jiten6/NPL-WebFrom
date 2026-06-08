<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="LeaveBalanceUpdate.aspx.cs" Inherits="NewPortal2023.ESS.LeaveBalanceUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <section class="panel">

                        <!-- Panel Header -->
                        <header class="panel-heading bg-info">
                            <h4 class="panel-title text-white">
                                Employee Leave Balance Maintenance
                            </h4>
                        </header>

                        <!-- Panel Body -->
                        <div class="panel-body">

                            <asp:ScriptManager ID="scm" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/Assets/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/Assets/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                            <div class="container-fluid">

                                <!-- Selection Row -->
                                <div class="row mb-3">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Employee Code</label>
                                            <asp:DropDownList ID="ddlEmployeeCode" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-- Select --" Value="" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Leave Code</label>
                                            <asp:DropDownList ID="ddlLeaveCode" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-- Select --" Value="" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Year</label>
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Month</label>
                                            <asp:DropDownList ID="ddlMonth" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>

                                <!-- Balance Row -->
                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Opening Balance</label>
                                            <asp:TextBox ID="txtOpeningBalance"
                                                runat="server"
                                                CssClass="form-control text-right"
                                                TextMode="Number"
                                                step="0.01"
                                                placeholder="0.00" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Availed</label>
                                            <asp:TextBox ID="txtAvailed"
                                                runat="server"
                                                CssClass="form-control text-right"
                                                TextMode="Number"
                                                step="0.01"
                                                placeholder="0.00" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Closing Balance</label>
                                            <asp:TextBox ID="txtClosingBalance"
                                                runat="server"
                                                CssClass="form-control text-right"
                                                TextMode="Number"
                                                step="0.01"
                                                placeholder="0.00" />
                                        </div>
                                    </div>
                                </div>

                                <!-- Button Row -->
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSubmit"
                                            runat="server"
                                            Text="Update Balance"
                                            CssClass="btn btn-success px-4"
                                            OnClick="btnSubmit_Click" />
                                    </div>
                                </div>

                            </div>
                        </div>

                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>

