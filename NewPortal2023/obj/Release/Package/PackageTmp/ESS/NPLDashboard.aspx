<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="NPLDashboard.aspx.cs" Inherits="NewPortal2023.ESS.NPLDashboard" %>

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
    <style>
    .gridview-header {
        text-align: center;
    }
</style>
    <%--<script type="text/javascript">
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
    </script>--%>
    <style>
        #loader {
            display: none;
            /* Add your loader styles here */
        }
    </style>

      <style type="text/css">
        .underline {
            text-decoration: underline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <div class="panel-body">
            <asp:ScriptManager ID="smInv" runat="server">
                <Scripts>
                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                </Scripts>
            </asp:ScriptManager>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" EnableViewState="true">
                <ContentTemplate>
                    <section class="wrapper">
                        <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                            <button data-dismiss="alert" class="close close-sm" type="button">
                                <i class="fa fa-times"></i>
                            </button>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </div>

                        <div class="col-sm-12 row mini-stat clearfix" style="margin-left: 0px">
                            <div class="form-group col-sm-4">
                                <asp:Button ID="btnPending" runat="server" Text="Pending For Action" Class="underline" CssClass="btn btn-primary" Style="margin-bottom: 5px; margin-top: 20px; margin-left: 15%; font-size: 18px; font-weight: bold"
                                    OnClick="btnPending_Click" Width="250px" ></asp:Button>
                            </div>

                            <div class="form-group col-sm-4">
                                <asp:Button ID="btnApproved" runat="server" Text="Approved" Class="underline" CssClass="btn btn-success" Style="margin-bottom: 5px; margin-top: 20px; margin-left: 40px; font-size: 18px; font-weight: bold"
                                    OnClick="btnApproved_Click" Width="250px"></asp:Button>
                            </div>

                            <div class="form-group col-sm-4">
                                <asp:Button ID="btnApproval" runat="server" Text="Pending For Approval" Class="underline" CssClass="btn btn-warning" Style="margin-bottom: 5px; margin-top: 20px; margin-left: 15%; font-size: 18px; font-weight: bold"
                                    OnClick="btnApproval_Click" Width="250px"></asp:Button>
                            </div>
                        </div>

                        <div id="divPending" runat="server" visible="false" class="mini-stat clearfix" style="background-color: lightblue;">
                            <div class="wrapper">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon orange"><i class="fa fa-edit"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkPenAttendance" runat="server" Text='Attendance OT/CO' Class="underline"
                                                        OnClick="lnkPenAttendance_Click" ></asp:LinkButton>
                                                </span>
                                                <asp:Label ID="lblPenAttendanceCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon orange"><i class="fa fa-edit"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnlPenLeave" runat="server" Text='Leave/Half Day' Class="underline"
                                                        OnClick="lblPenLeave_Click" ></asp:LinkButton>
                                                </span>
                                                <asp:Label ID="lblPenLeaveCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="AssignCXO" runat="server">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkPenHandset" runat="server" Text='Mobile Handset' Class="underline"
                                                        OnClick="lblPenHandset_Click"></asp:LinkButton>
                                                </span>
                                                <asp:Label ID="lblPenHandsetCount" runat="Server" Text='0' />
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
                                                    <asp:LinkButton ID="lnkPenTele" runat="server" Text='Telephone Bills'  Class="underline"
                                                        OnClick="lblPenTele_Click"></asp:LinkButton>
                                                </span>
                                                <asp:Label ID="lblPenTeleCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="AssignHOD" runat="server">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkPenLocal" runat="server" Text='Local Expenses'  Class="underline"
                                                        OnClick="lblPenLocal_Click" ></asp:LinkButton>
                                                </span>
                                                <asp:Label ID="lblPenLocalCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4" id="AssignChecker" runat="server">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkPenDomestic" runat="server" Text='Domestic' Class="underline"
                                                        OnClick="lblPenDomestic_Click"></asp:LinkButton>
                                                </span>
                                                <asp:Label ID="lblPenDomesticCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>



                        <div id="divApproved" runat="server" visible="false" class="mini-stat clearfix" style="background-color: lightgreen">
                            <div class="wrapper">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon orange"><i class="fa fa-edit"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkApprAttendance" runat="server" Text='Attendance OT/CO'  Class="underline"
                                                        OnClick="lnkApprAttendance_Click"></asp:LinkButton>
                                                </span>

                                                <asp:Label ID="lblApprAttendanceCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon orange"><i class="fa fa-edit"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkApprLeave" runat="server" Text='Leave/Half Day' Class="underline"
                                                        OnClick="lnkApprLeave_Click"></asp:LinkButton>

                                                </span>

                                                <asp:Label ID="lblApprLeaveCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="Div1" runat="server">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkApprMob" runat="server" Text='Mobile Handset' Class="underline"
                                                        OnClick="lnkApprMob_Click"></asp:LinkButton>
                                                </span>
                                                <asp:Label ID="lblApprMobCount" runat="Server" Text='0' />
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
                                                    <asp:LinkButton ID="lnkApprTele" runat="server" Text='Telephone Bills' OnClick="lnkApprTele_Click" Class="underline"></asp:LinkButton></span>
                                                <asp:Label ID="lblApprTeleCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="Div3" runat="server">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkApprLoc" runat="server"  Text='Local Expenses' OnClick="lnkApprLoc_Click" Class="underline"></asp:LinkButton></span>
                                                <asp:Label ID="lblApprLocCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4" id="Div4" runat="server">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkApprDom" runat="server" Text='Domestic Expenses'  OnClick="lnkApprDom_Click" Class="underline"></asp:LinkButton></span>
                                                <asp:Label ID="lblApprDomCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div id="divApproval" runat="server" visible="false" class="mini-stat clearfix" style="background-color: lightyellow">
                            <div class="wrapper">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon orange"><i class="fa fa-edit"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkAprvlAttendance" runat="server" AutoPostBack="true" Text="Attendance OT/CO" Class="underline"
                                                        OnClick="lnkAprvlAttendance_Click" ></asp:LinkButton></span>
                                                <asp:Label ID="lblAprvlAttendanceCount" runat="Server" Text='8' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon orange"><i class="fa fa-edit"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkAprvlLeave" runat="server" AutoPostBack="true" Text="Leave/Half Day" Class="underline"
                                                        OnClick="lnkAprvlLeave_Click" ></asp:LinkButton>
                                                </span>
                                                <asp:Label ID="lblAprvlLeaveCount" runat="Server" Text='4' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="Div2" runat="server">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkAprvlMob" runat="server" Text='Mobile Handset' Class="underline"
                                                        OnClick="lnkAprvlMob_Click" ></asp:LinkButton>

                                                </span>

                                                <asp:Label ID="lblAprvlMobCount" runat="Server" Text='0' />
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
                                                    <asp:LinkButton ID="lnkAprvlTele" runat="server" Text='Telephone Bills' OnClick="lnkAprvlTele_Click" Class="underline"></asp:LinkButton></span>
                                              
                                                <asp:Label ID="lblAprvlTeleCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="Div5" runat="server">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkAprvlLoc" runat="server"  Text='Local Expenses' OnClick="lnkAprvlLoc_Click" Class="underline"></asp:LinkButton></span>
                                            
                                                <asp:Label ID="lblAprvlLocCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4" id="Div6" runat="server">
                                        <div class="mini-stat clearfix">
                                            <span class="mini-stat-icon tar"><i class="fa fa-address-card"></i></span>
                                            <div class="mini-stat-info">
                                                <span>
                                                    <asp:LinkButton ID="lnkAprvlDom" runat="server" Text='Domestic Expenses' OnClick="lnkAprvlDom_Click" Class="underline"></asp:LinkButton></span>
                                               
                                                <asp:Label ID="lblAprvlDomCount" runat="Server" Text='0' />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div id="divpencancel" runat="server" visible="false" style="text-align: right;">
                            <asp:Button ID="btnpenback" runat="server" Text="Back" CssClass="btn btn-primary"
                                Style="margin-bottom: 5px; margin-top: 20px; margin-right: 40px; font-size: 18px; font-weight: bold; width: 250px;"
                                OnClick="btnpenback_Click"  />
                        </div>
                        <div id="divapprcancel" runat="server" visible="false" style="text-align: right;">
                            <asp:Button ID="btnapprback" runat="server" Text="Back" CssClass="btn btn-success"
                                Style="margin-bottom: 5px; margin-top: 20px; margin-right: 40px; font-size: 18px; font-weight: bold; width: 250px;"
                                OnClick="btnapprback_Click" />
                        </div>
                        <div id="divpenapprback" runat="server" visible="false" style="text-align: right;">
                            <asp:Button ID="btnpenapprback" runat="server" Text="Back" CssClass="btn btn-warning"
                                Style="margin-bottom: 5px; margin-top: 20px; margin-right: 40px; font-size: 18px; font-weight: bold; width: 250px;"
                                OnClick="btnpenapprback_Click" />
                        </div>


                        <div class="form-group" id="OtherDetails" visible="true" runat="server" style="margin-top: 0px">
                            <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="From Date">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFrom" runat="Server" Text='<%# Eval("FROM_DATE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTo" runat="Server" Text='<%# Eval("TO_DATE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Days">
                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="Server" Text='<%# Eval("DAYS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("STATUS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Type">
                                        <ItemStyle CssClass="GridViewItem" Width="90px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("LEAVE_CODE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Created On">
                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR" runat="Server" Text='<%# Eval("CREATEDDT") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Pending Days">
                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPENDAYS" runat="Server" Text='<%# Eval("PENDING_DAYS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EmptyDataTemplate>
                                    <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                        <tr>
                                            <td class="GridViewHeader" style="width: 120px">To Date
                                            </td>
                                            <td class="GridViewHeader" style="width: 120px">From Date
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Days
                                            </td>
                                            <td class="GridViewHeader" style="width: 200px">Status
                                            </td>
                                            <td class="GridViewHeader" style="width: 90px">Leave Type
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Created Date
                                            </td>
                                            <td class="GridViewHeader" style="width: 250px">Pending Days 
                                            </td>

                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>

                        <div class="form-group" id="Div7" visible="true" runat="server" style="margin-top: 0px">
                            <asp:GridView ID="gvattendance" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="Server" Text='<%# Eval("DATE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift Schedule ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblshift" runat="Server" Text='<%# Eval("shift_schedule") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time In ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblin" runat="Server" Text='<%# Eval("time_in") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time Out">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblout" runat="Server" Text='<%# Eval("time_out") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CO">
                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblco" runat="Server" Text='<%# Eval("old_CO") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OT">
                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblot" runat="Server" Text='<%# Eval("OLD_OT") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle CssClass="GridViewItem" Width="90px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("STATUS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Pending Days">
                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPENDAYS" runat="Server" Text='<%# Eval("PENDING_DAYS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EmptyDataTemplate>
                                    <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                        <tr>
                                            <td class="GridViewHeader" style="width: 120px">Date
                                            </td>
                                            <td class="GridViewHeader" style="width: 120px">Shift Schedule
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Time In
                                            </td>
                                            <td class="GridViewHeader" style="width: 200px">Time Out
                                            </td>
                                            <td class="GridViewHeader" style="width: 90px">CO
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">OT
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Status
                                            </td>
                                            <td class="GridViewHeader" style="width: 250px">Pending Days 
                                            </td>

                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>

                          <div class="form-group" id="Div8" visible="true" runat="server" style="margin-top: 0px">
                            <asp:GridView ID="gvexpense" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="Claim No">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNO" runat="Server" Text='<%# Eval("claim_no") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Date ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="Server" Text='<%# Eval("claim_date") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Amount">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount" runat="Server" Text='<%# Eval("Claim_Amount") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle CssClass="GridViewItem" Width="90px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("STATUS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Pending Days">
                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPENDAYS" runat="Server" Text='<%# Eval("PENDING_DAYS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EmptyDataTemplate>
                                    <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                        <tr>
                                            <td class="GridViewHeader" style="width: 120px">Claim No
                                            </td>
                                            <td class="GridViewHeader" style="width: 120px">Claim Date
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Claim Amount
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Status
                                            </td>
                                            <td class="GridViewHeader" style="width: 250px">Pending Days 
                                            </td>

                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>

                         <div class="form-group" id="Div9" visible="true" runat="server" style="margin-top: 0px">
                            <asp:GridView ID="gvDom" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5" 
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="Claim No">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNO" runat="Server" Text='<%# Eval("app_aid") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Date ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="Server" Text='<%# Eval("createdate") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Travel Type">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbltype" runat="Server" Text='<%# Eval("traveltype") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Date ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromdate" runat="Server" Text='<%# Eval("fromdate") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbltodate" runat="Server" Text='<%# Eval("todate") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Expenses">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount" runat="Server" Text='<%# Eval("TOTAL_EXPENSE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle CssClass="GridViewItem" Width="90px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("STATUS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Pending Days">
                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPENDAYS" runat="Server" Text='<%# Eval("PENDING_DAYS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EmptyDataTemplate>
                                    <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                        <tr>
                                            <td class="GridViewHeader" style="width: 120px">Claim No
                                            </td>
                                            <td class="GridViewHeader" style="width: 120px">Claim Date
                                            </td>
                                             <td class="GridViewHeader" style="width: 130px">Travel Type
                                            </td>
                                             <td class="GridViewHeader" style="width: 130px">From Date 
                                            </td>
                                             <td class="GridViewHeader" style="width: 130px">To Date 
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Total Expenses
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Status
                                            </td>
                                            <td class="GridViewHeader" style="width: 250px">Pending Days 
                                            </td>

                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>

                         <div class="form-group" id="Div10" visible="true" runat="server" style="margin-top: 0px">
                            <asp:GridView ID="gvLeaveappr" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="From Date">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFrom" runat="Server" Text='<%# Eval("FROM_DATE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTo" runat="Server" Text='<%# Eval("TO_DATE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Days">
                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="Server" Text='<%# Eval("DAYS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("STATUS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Type">
                                        <ItemStyle CssClass="GridViewItem" Width="90px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("LEAVE_CODE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Created On">
                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR" runat="Server" Text='<%# Eval("CREATEDDT") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" /> 
                                        </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EmptyDataTemplate>
                                    <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                        <tr>
                                            <td class="GridViewHeader" style="width: 120px">To Date
                                            </td>
                                            <td class="GridViewHeader" style="width: 120px">From Date
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Days
                                            </td>
                                            <td class="GridViewHeader" style="width: 200px">Status
                                            </td>
                                            <td class="GridViewHeader" style="width: 90px">Leave Type
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Created Date
                                            </td>
                                           

                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>

                        <div class="form-group" id="Div11" visible="true" runat="server" style="margin-top: 0px">
                            <asp:GridView ID="gvattendanceappr" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="Server" Text='<%# Eval("DATE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift Schedule ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblshift" runat="Server" Text='<%# Eval("shift_schedule") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time In ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblin" runat="Server" Text='<%# Eval("time_in") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time Out">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblout" runat="Server" Text='<%# Eval("time_out") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CO">
                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblco" runat="Server" Text='<%# Eval("old_CO") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OT">
                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblot" runat="Server" Text='<%# Eval("OLD_OT") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle CssClass="GridViewItem" Width="90px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("STATUS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EmptyDataTemplate>
                                    <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                        <tr>
                                            <td class="GridViewHeader" style="width: 120px">Date
                                            </td>
                                            <td class="GridViewHeader" style="width: 120px">Shift Schedule
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Time In
                                            </td>
                                            <td class="GridViewHeader" style="width: 200px">Time Out
                                            </td>
                                            <td class="GridViewHeader" style="width: 90px">CO
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">OT
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Status
                                            </td>
                                           

                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>

                          <div class="form-group" id="Div12" visible="true" runat="server" style="margin-top: 0px">
                            <asp:GridView ID="gvexpenseappr" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="Claim No">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNO" runat="Server" Text='<%# Eval("claim_no") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Date ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="Server" Text='<%# Eval("claim_date") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Amount">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount" runat="Server" Text='<%# Eval("Claim_Amount") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle CssClass="GridViewItem" Width="90px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("STATUS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EmptyDataTemplate>
                                    <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                        <tr>
                                            <td class="GridViewHeader" style="width: 120px">Claim No
                                            </td>
                                            <td class="GridViewHeader" style="width: 120px">Claim Date
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Claim Amount
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Status
                                            </td>
                                           

                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>

                         <div class="form-group" id="Div13" visible="true" runat="server" style="margin-top: 0px">
                            <asp:GridView ID="gvDomappr" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5" 
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="Claim No">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNO" runat="Server" Text='<%# Eval("app_aid") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Date ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="Server" Text='<%# Eval("createdate") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Travel Type">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbltype" runat="Server" Text='<%# Eval("traveltype") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Date ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromdate" runat="Server" Text='<%# Eval("fromdate") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date ">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbltodate" runat="Server" Text='<%# Eval("todate") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Expenses">
                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount" runat="Server" Text='<%# Eval("TOTAL_EXPENSE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle CssClass="GridViewItem" Width="90px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("STATUS") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridViewHeader" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EmptyDataTemplate>
                                    <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                        <tr>
                                            <td class="GridViewHeader" style="width: 120px">Claim No
                                            </td>
                                            <td class="GridViewHeader" style="width: 120px">Claim Date
                                            </td>
                                             <td class="GridViewHeader" style="width: 130px">Travel Type
                                            </td>
                                             <td class="GridViewHeader" style="width: 130px">From Date 
                                            </td>
                                             <td class="GridViewHeader" style="width: 130px">To Date 
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Total Expenses
                                            </td>
                                            <td class="GridViewHeader" style="width: 130px">Status
                                            </td>
                                          

                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                    </section>

                </ContentTemplate>
                <Triggers>
                   <%-- <asp:PostBackTrigger ControlID="btnPending" />
                    <asp:PostBackTrigger ControlID="btnApproved" />
                    <asp:PostBackTrigger ControlID="btnApproval" />
                    <%--<asp:PostBackTrigger ControlID="lnkPenAttendance" />--%>
                   <%-- <asp:PostBackTrigger ControlID="lnlPenLeave" />
                    <asp:PostBackTrigger ControlID="lnkPenHandset" />
                    <asp:PostBackTrigger ControlID="lnkPenTele" />
                    <asp:PostBackTrigger ControlID="lnkPenLocal" />
                    <asp:PostBackTrigger ControlID="lnkPenDomestic" />--%>
                  <%--  <asp:PostBackTrigger ControlID="btnpenback" />
                    <asp:PostBackTrigger ControlID="btnapprback" />
                    <asp:PostBackTrigger ControlID="btnpenapprback" />--%>
                   <%-- <asp:PostBackTrigger ControlID="radioPrimary1" />
                    <asp:PostBackTrigger ControlID="btnGenerateAttendanceReport" />
                    <asp:PostBackTrigger ControlID="btnExport" />
                    <asp:PostBackTrigger ControlID="btnApprove" />
                    <asp:PostBackTrigger ControlID="btnDelete" />
                    <asp:PostBackTrigger ControlID="btnCancel" />--%>
                </Triggers>
            </asp:UpdatePanel>

        </div>
         <div style="text-align: center;">
       <%-- <asp:Image ID="imgLogo" runat="server" ImageUrl="~/ESS/darwin_logo.png" AlternateText="SoplLogo"
            Style="margin-top: 100px;" Visible="false" />
        <asp:Image ID="imgpeperlogo" runat="server" ImageUrl="~/ESS/img_pepper.png" AlternateText="PepperAdvantageLogo"
            Style="margin-top: 100px;" Visible="false" />
        <asp:Image ID="imgnpllogo" runat="server" ImageUrl="~/ESS/NPL_Logo_Home.png" AlternateText="NplLogo"
            Style="margin-top: 70px; height: 250px; width: 600px;" Visible="false" />--%>

        <div id="MyPopup" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title"></h4>
                    </div>
                    <div class="modal-body">
                         <asp:Label ID="lblNotifymessage" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                          <hr />
                        <asp:Label ID="lblAttend" runat="server" style="font-weight: bold;">Attendance Date : - </asp:Label>
                        <asp:Label ID="lblAttendDate" runat="server" class=" input-sm"></asp:Label>

<%--                         <asp:HiddenField ID="lblLatitude" runat="server"  > </asp:HiddenField>
                        <asp:HiddenField ID="lblLongitude" runat="server"  ></asp:HiddenField>--%>

                        <asp:Button ID="btnAttendClick" runat="server" class="btn btn-success" Text="IN" OnClick="btnAttendClick_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                        <asp:Button ID="btnAttendClickOut" runat="server" class="btn btn-danger" Text="OUT" OnClick="btnAttendClickOut_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                        <hr />
                         <asp:Label ID="Label2" runat="server" style="font-weight: bold;text-align: left;color:blue" Text="Today Attendance Details">  </asp:Label>
                        <hr />
                         <table>
                            <tr>
                                <td valign="top"><%--10-05-2023 09:20:20--%>
                                    <span style="font-weight: bold;">LOGIN : - </span> <asp:Label ID="lblInTime" runat="server" Text=""></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp
                                   
                                     <span style="margin-left: 50px;font-weight: bold;">LOGOUT : - </span><asp:Label ID="lblOutTime" runat="server" Text="" ></asp:Label>
                                    
                                </td>
                            </tr>
                        </table>
                         <hr />
                        <asp:Label ID="Label1" runat="server" style="font-weight: bold;text-align: left;color:blue" Text="Pervious Attendance Record "> </asp:Label>
                        <hr />
                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False"

                                                                    HorizontalAlign="center" CellPadding="5" 
                                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntryId" runat="Server" Text='<%# Eval("ENTRY_AID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="NAME">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAppNo" runat="server" Text='<%# Eval("EMP_FNAME") %>'
                                             />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridViewHeader" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="LOGIN DATE AND TIME" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="Server" Text='<%# Eval("ATTEND_LOGINDATE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridview-header" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LOGOUT DATE AND TIME" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrigin" runat="Server" Text='<%# Eval("ATTEND_LOGOUTDATE") %>' />
                                    </ItemTemplate>
                                    
                                   <HeaderStyle CssClass="gridview-header" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="REMARKS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusTRANS" runat="Server" Text='<%# Eval("REMARKS") %>' />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridViewHeader" />
                                </asp:TemplateField>--%>
                            </Columns>
                            <EmptyDataTemplate>
                                <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                    <tr>
                                        <td class="GridViewHeader" style="width: 10%">
                                            <asp:Literal ID="Literal6" runat="server" Text="Click on 'Present Button ' to add Attendance." />
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">

                        <button type="button" class="btn btn-danger" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <script type="text/javascript">
        function ShowPopup(title, body) {
            $("#MyPopup .modal-title").html(title);
            //$("#MyPopup .modal-body").html(body);
            $("#MyPopup").modal("show");
        }
    </script>
    </section>
</asp:Content>
