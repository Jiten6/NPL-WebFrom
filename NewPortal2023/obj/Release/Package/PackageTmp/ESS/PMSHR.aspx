<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="PMSHR.aspx.cs" Inherits="NewPortal2023.ESS.PMSHR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="assets/js/dynamic_table_init.js"></script>

    <script>
        $(function () {
            $("#gvList").DataTable({
                "responsive": true, "lengthChange": false, "autoWidth": false,
                "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
            }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            $('#example2').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false, lnkBtnAddRowSteps
                "ordering": true,
                "info": true,
                "autoWidth": false,
                "responsive": true,
            });
        });
    </script>

    <script type="text/javascript">
        $(function () {

            var currentDate = new Date();

            $('[id*=txtFromDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",
                autoclose: true // Disable dates after the current date
            });
        });
        $(function () {

            var currentDate = new Date();

            $('[id*=txtToDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",
                autoclose: true// Disable dates after the current date
            });
        });

    </script>

    <%--<script type="text/javascript">
        function ConfirmDel() {
            return confirm("Are you sure you want to delete this record?");
        }

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Select2);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({
                    format: 'dd-MMMM-yyyy',
                    autoclose: true
                });

                $(".datetimepicker").datetimepicker({
                    format: 'dd-mm-yyyy hh:ii',
                    autoclose: true
                });
            }

            $('.datepicker').datepicker({
                format: 'dd-MMMM-yyyy',
                autoclose: true
            });

            $(".datetimepicker").datetimepicker({
                format: 'yyyy-mm-dd hh:ii',
                autoclose: true
            });

            function Select2(sender, args) {
                $(".select2").select2();
            }

            $(".select2").select2();
        });
    </script>--%>

    <script type="text/javascript">

        function ConNext() {
            return confirm("Are you saving the details, and do you want to go to the next section?");
        }

        function ConPrev() {
            return confirm("Are you saving the details, and do you want to go to the privious section?");
        }

    </script>



    <style>
        .gridview-cell {
            white-space: pre-line;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <div class="d-flex flex-wrap">
                            <div class="flex-grow-1">
                                <header class="panel-heading" style="background-color: darkcyan">
                                    <h3 style="color: white">PMS - Enable/Disable</h3>
                                </header>
                                <div class="panel-body">
                                    <asp:ScriptManager ID="scm" runat="server">
                                        <Scripts>
                                            <asp:ScriptReference Path="~/Assets/jquery.blockUI.js" />
                                            <asp:ScriptReference Path="~/Assets/blockUI.js" />
                                        </Scripts>
                                    </asp:ScriptManager>

                                    <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="vwList" runat="server">

                                            <div class="col-lg-12 row">
                                                <div class="col-sm-3" style="margin-left: -20px">
                                                    <asp:Label ID="Label3" runat="server" Text="Financial Year :" CssClass="card-title" Style="font-size: 20px;"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="drpFinancialYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFinancialYear_SelectedIndexChanged" CssClass="form-control input-sm-3" Enabled="true" onchange="showLoader();">
                                                            <asp:ListItem Value="">Select Year</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <br />
                                                </div>
                                            </div>

                                            <div class="col-12" id="divKRAFin" runat="server" visible="false">
                                                <div class="col-12" style="margin-left: -5px; display: flex; justify-content: center; align-items: center;">
                                                    <asp:Label ID="lblCycle" runat="server" Text="" CssClass="card-title" Style="font-size: 30px;"></asp:Label>
                                                </div>

                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <hr />
                                                    </div>
                                                </div>
                                                <div class="adv-table">

                                                    <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                        <button data-dismiss="alert" class="close close-sm" type="button">
                                                            <i class="fa fa-times"></i>
                                                        </button>
                                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                                    </div>

                                                    <div class="card-body" style="margin: 20px">

                                                        <div class="form-group row">
                                                            <div class="col-06">
                                                                <asp:DropDownList ID="drpQuarter" runat="server" Visible="false" CssClass="form-control input-sm-3" AutoPostBack="true" Width="150px">

                                                                    <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                                                    <asp:ListItem Value="2">Second Quarter</asp:ListItem>
                                                                    <asp:ListItem Value="3">Third Quarter</asp:ListItem>
                                                                    <asp:ListItem Value="4">Fourth Quarter</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-3" style="margin-left: -20px">
                                                            <asp:Label ID="Label4" runat="server" Text="Enable PMS Inputs" CssClass="card-title" Style="font-size: 20px;"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <div class="form-group">
                                                                <label for="lblCostCenter" class="col-sm-1 labels" style="margin-left: -30px">From</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox ID="txtFromDateEmp" runat="server" ReadOnly="false" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled"></asp:TextBox>
                                                                </div>
                                                                <label for="lblCostCenter" class="col-sm-1 labels">To</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox ID="txtToDateEmp" runat="server" ReadOnly="false" OnTextChanged="txtToDateEmp_TextChanged" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled" AutoPostBack="true" OnChange="showLoader();"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-lg-12">
                                                                <br />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3" style="margin-left: -20px">
                                                            <asp:Label ID="Label5" runat="server" Text="Enable PMS Approval" CssClass="card-title" Style="font-size: 20px;"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <div class="form-group">
                                                                <label for="lblCostCenter" class="col-sm-1 labels" style="margin-left: -30px">From</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox ID="txtFromDateAppr" runat="server" ReadOnly="true" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled"></asp:TextBox>
                                                                </div>
                                                                <label for="lblCostCenter" class="col-sm-1 labels">To</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox ID="txtToDateAppr" runat="server" ReadOnly="false" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-lg-12">
                                                                <br />
                                                            </div>
                                                        </div>

                                                        <div class="modal-footer justify-content-between" style="text-align: center">
                                                            <asp:Button ID="btnSubmitDates" runat="server" OnClick="btnSubmitDates_Click" Text="Submit" CssClass="btn btn-primary" AutoPostBack="true" OnClientClick="showLoader();" />
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-lg-12">
                                                                <hr />
                                                            </div>
                                                        </div>

                                                        <div class="col-12" style="margin-left: -5px">
                                                            <asp:Label ID="Label2" runat="server" Text="Employee Wise KRA Activation" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                        </div>
                                                        <br />
                                                        <div id="divSteps" runat="server" class="form-group row">
                                                            <asp:UpdatePanel ID="updListAdd" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-12">
                                                                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false"
                                                                            PageSize="100" AllowPaging="true"
                                                                            HeaderStyle-BackColor="LightSteelBlue " OnPreRender="gvDataPointList_PreRender" class="table table-bordered table-striped">
                                                                            <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                                            <PagerStyle CssClass="pagination-ys" />
                                                                            <Columns>
                                                                                <%--  <asp:TemplateField ShowHeader="true" Visible="false">
                                                                        <ItemStyle VerticalAlign="Middle" />
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkSelectAll" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" runat="Server" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>


                                                                                <asp:TemplateField HeaderText="Employee ID" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="txtEmpID" runat="Server" Text='<%# Eval("EMP_MID") %>' /><%--<%# Eval("AREA") %>--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Employee Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="txtEmpName" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                                                        <%--<%# Eval("METRIC") %>--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Input Activation Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="txtStatus" runat="Server" Text='<%# Eval("KRA_FLAG_STATUS") %>' /><%-- <%# Eval("TARGET") %>--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Request For Input">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="drpAction" runat="server" CssClass="form-control input-sm-3" OnSelectedIndexChanged="drpAction_SelectedIndexChanged1" AutoPostBack="true" OnChange="showLoader();">
                                                                                            <asp:ListItem Value="">[--Select One--]</asp:ListItem>
                                                                                            <asp:ListItem Value="Reporting Manager" Text="Approver"></asp:ListItem>
                                                                                            <asp:ListItem Value="Employee" Text="Employee"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td style="width: 10%" class="Title">
                                                                                            <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>


                                                                    </div>
                                                                    <br />

                                                                    <div class="form-group row">
                                                                        <%-- <div class="adv-table">
                                                            <div id="" class="form-horizontal">
                                                                <fieldset>--%>
                                                                        <label visible="false" class="col-sm-1" id="lblActionAll"></label>
                                                                        <div class="col-sm-2">

                                                                            <asp:DropDownList ID="drActionAll" runat="server" CssClass="form-control" AutoPostBack="false" Visible="false">
                                                                                <asp:ListItem Value="">--Select Action Type--</asp:ListItem>
                                                                                <asp:ListItem Value="Enable" Text="Enable"></asp:ListItem>



                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <%--   &nbsp;&nbsp;--%>
                                                                        <div class="col-sm-2" visible="false">
                                                                            <asp:Button ID="BtnSave" runat="server" Visible="false" OnClick="BtnSave_Click" Text="Save changes" CssClass="btn btn-success" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        </div>

                                                                        <%--<div class="col-sm-2">
                                                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" OnClick="btnClose_Click" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        
                                                                    </div>--%>

                                                                        <%--    </fieldset>
                                                            </div>
                                                                 </div>--%>
                                                                </ContentTemplate>
                                                                <Triggers>

                                                                    <%--<asp:PostBackTrigger ControlID="btnClose" />--%>
                                                                    <asp:PostBackTrigger ControlID="BtnSave" />
                                                                    <asp:PostBackTrigger ControlID="gvList" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                        <br />
                                                        <hr>
                                                        <br />




                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>

                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
    <script>
        var handleDataTableButtons = function () {
            "use strict";
            0 !== $('#<%= gvList.ClientID %>').length &&
                $('#<%= gvList.ClientID %>').DataTable({
                    dom: "Bfrtip",
                    buttons: [{
                        extend: "copy",
                        className: "btn-sm"
                    }, {
                        extend: "csv",
                        className: "btn-sm"
                    }, {
                        extend: "excel",
                        className: "btn-sm"
                    }, {
                        extend: "pdf",
                        className: "btn-sm"
                    }, {
                        extend: "print",
                        className: "btn-sm"
                    }],
                    responsive: !0
                })
        },
            TableManageButtons = function () {
                "use strict";
                return {
                    init: function () {
                        handleDataTableButtons()
                    }
                }
            }();
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= gvList.ClientID %>').dataTable();
        });
        TableManageButtons.init();
    </script>
</asp:Content>
