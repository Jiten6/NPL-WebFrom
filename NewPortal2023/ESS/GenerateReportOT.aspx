<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="GenerateReportOT.aspx.cs" Inherits="NewPortal2023.ESS.GenerateReportOT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ValidFile() {
            var file = $("#fupldDocument").val();
            if (file == "") {
                IN4_DisplayErrorMessage("Browse document.");
                return false;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ToggleVisible() {
            var trdate = document.getElementById("trdate");
            var trtime = document.getElementById("trtime");
            if (trdate.style.display == 'block') {
                trdate.style.display = 'none';
                trtime.style.display = 'none';
            }
            else {
                trdate.style.display = 'block';
                trtime.style.display = 'block';
            }
        }
    </script>
    <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/litepicker.js" type="text/javascript"></script>

    <script type="text/javascript">
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

                $(".datetimepicker").datetimepicker({
                    format: 'dd-mm-yyyy hh:ii',
                    autoclose: true
                });
            }

            $('.datepicker').datepicker({
                format: 'dd-mm-yyyy',
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
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Reporting Employees Master</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" EnableViewState="true">
                                <ContentTemplate>
                                    <div id="form1" runat="server">
                                        <div id="divAlertSucc" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessageSucc" runat="server"></asp:Label>
                                        </div>
                                        <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>


                                        <div id="trdate" runat="server" visible="false">
                                            <div class="form-group">
                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 labels">From Date<span style="color: Red;">*</span> :</label>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm datepicker" placeholder="Date"></asp:TextBox>
                                                        </div>

                                                        <label class="col-sm-3 labels">To Date<span style="color: Red;">*</span> :</label>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtDateTo" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm datepicker" placeholder="Date"></asp:TextBox>
                                                        </div>


                                                    </div>
                                                </div>

                                                <div id="trGenerateList" runat="server" class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                    <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success" Text="Generate"
                                                        OnClick="btnGenerate_Click" AutoPostBack="true" OnClientClick="showLoader();" />
                                                </div>

                                                <div id="trback" runat="server" visible="false" class="col-sm-12" style="text-align: center; margin-top: 20px">
                                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" OnClientClick="showLoader();" />
                                                </div>

                                                <div id="Div1" runat="server" visible="false" class="col-sm-12" style="text-align: center; margin-top: 20px">
                                                    <asp:Button ID="btnExport" Visible="false" AutoPostBack="true" runat="server" OnClick="btnExport_Click" CssClass="btn btn-primary" Text="Back" OnClientClick="showLoader();" />
                                                </div>
                                            </div>
                                        </div>

                                        <div id="trSerachBy" runat="server" visible="false">
                                            <label class="col-sm-3 labels">Search By Employee Name :-</label>
                                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="input"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                CssClass="btn btn-primary" OnClientClick="showLoader();" />
                                        </div>


                                        <div class="form-group row">
                                            <div id="trViewListByOTAndCOWise" runat="server" visible="false" class="form-group col-sm-3" style="margin-left:3%">
                                                <asp:DropDownList ID="drpOtCOType" runat="server" CssClass="form-control input-sm-3" Width="150px" OnSelectedIndexChanged="drpOtCOType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="">[Search By]</asp:ListItem>
                                                    <asp:ListItem Value="OT" Text="OT"></asp:ListItem>
                                                    <asp:ListItem Value="CO" Text="CO"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div id="trStatusOTCO" runat="server" visible="false" class="form-group col-sm-8" style="margin-left:-3%">
                                                <asp:DropDownList ID="drpStatusOTCO" runat="server" CssClass="form-control input-sm-3" Width="150px" OnSelectedIndexChanged="drpStatusOTCO_SelectedIndexChanged" AutoPostBack="true">
                                                    <%--  <asp:ListItem Value="">[Select Action Type]</asp:ListItem>--%>
                                                    <asp:ListItem Value="Hide Status" Text="Hide Status"></asp:ListItem>
                                                    <asp:ListItem Value="Show Status" Text="Show Status"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>


                                        <div>
                                            <br />
                                        </div>

                                        <div id="tdApproval" runat="server" visible="false" class="form-group">
                                            <div class="col-sm-6">
                                                <label class="col-sm-6 labels">Approved OT :</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtApprovalOT" runat="server" ReadOnly="true" Enabled="false" CssClass="input"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="col-sm-6">
                                                <label class="col-sm-6 labels">Approved CO :</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtApprovalCO" runat="server" ReadOnly="true" Enabled="false" CssClass="input"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div id="tdRejected" runat="server" visible="false" class="form-group">
                                            <div class="col-sm-6">
                                                <label class="col-sm-6 labels">Rejected OT :</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRejectedOT" runat="server" ReadOnly="true" Enabled="false" CssClass="input"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="col-sm-6">
                                                <label class="col-sm-6 labels">Rejected CO :</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRejectedCO" runat="server" ReadOnly="true" Enabled="false" CssClass="input"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div id="tdPending" runat="server" visible="false" class="form-group">
                                            <div class="col-sm-6">
                                                <label class="col-sm-6 labels">Pending OT :</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtPendingOT" runat="server" ReadOnly="true" Enabled="false" CssClass="input"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="col-sm-6">
                                                <label class="col-sm-6 labels">Pending CO :</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtPendingCO" runat="server" ReadOnly="true" Enabled="false" CssClass="input"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div id="trViewListOTCO" runat="server" visible="false" class="form-group">
                                            <div class="col-sm-6">
                                                <label class="col-sm-6 labels">Total OT :</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOT" runat="server" ReadOnly="true" Enabled="false" CssClass="input"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="col-sm-6">
                                                <label class="col-sm-6 labels">Total CO :</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtCO" runat="server" ReadOnly="true" Enabled="false" CssClass="input"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>


                                        <div id="trList" runat="server" visible="false">

                                            <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvLeave_RowDataBound"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Employee Code">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCode" runat="Server" Text='<%# Eval("EMPLOYEE_CODE") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <%--  <asp:TemplateField HeaderText="Total OT">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOT" runat="Server" Text='<%# Eval("OT") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total CO">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCO" runat="Server" Text='<%# Eval("CO") %>' />

                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Action" Visible="true">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkApprove" runat="server" Text="Check OT and CO" ForeColor="Blue"
                                                                OnClick="lnkApprove_Click" OnClientClick="showLoader();" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <%--    <asp:TemplateField HeaderText="Out Time">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOut" runat="server" Width="130px"></asp:Label>
                                                               
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Left" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRem" runat="Server"  /> 
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Left" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRem" runat="Server" /> 
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>--%>
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <%--<table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                           <tr>
                                                                <td class="GridViewHeader" style="width: 100px">Employee Code
                                                                </td>
                                                                <td class="GridViewHeader" style="width: 100px">Employee Name
                                                                </td>--%>
                                                    <%-- <td class="GridViewHeader" style="width: 100px">Total OT
                                                                </td>
                                                                <td class="GridViewHeader" style="width: 100px">Total CO
                                                                </td>--%>
                                                    <%-- <td class="GridViewHeader" style="width: 100px">Action
                                                                </td>--%>
                                                    <%-- <td class="GridViewHeader" style="width: 100px">
                                                                    Total Hrs
                                                                </td>
                                                                <td class="GridViewHeader" style="width: 300px">
                                                                    Remarks
                                                                </td>
                                                            </tr>
                                                        </table>--%>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                        </div>


                                        <div id="trViewList" runat="server" visible="false">
                                            <div id="DivgvMultipleList" visible="true" runat="server" style="overflow-x: scroll;">

                                                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false" HeaderText="Action">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="Server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Emp Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Emp_Code") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Emp Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="Server" Text='<%# Eval("Emp_Name") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("Date") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shift">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShiftSchedule" runat="Server" Text='<%# Eval("Shift_Schedule") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="In Time">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTimeIN" runat="Server" Text='<%# Eval("Time_In") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Out Time">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusTAX" runat="Server" Text='<%# Eval("Time_Out") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Working hrs">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusLUP" runat="Server" Text='<%# Eval("Total_Working_hrs") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <%--   <asp:TemplateField HeaderText="Shift hrs">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatusTRAVEL" runat="Server" Text='<%# Eval("Shift_hrs") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                            </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="OT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOT" runat="Server" Text='<%# Eval("OT") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCO" runat="Server" Text='<%# Eval("CO") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>


                                                        <%-- <asp:TemplateField HeaderText="P">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPresent" runat="Server" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="GridViewItem" Width="10%" HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" HeaderText="A">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAbsent" runat="Server" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="GridViewItem" Width="10%" HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="L">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbllate" runat="Server" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="GridViewItem" Width="10%" HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                            </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" runat="Server" TextMode="MultiLine" Text='<%# Eval("RMK") %>' CssClass="input" MaxLength="100" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drpAction" runat="server" CssClass="tableTitle" SelectedValue='<%# Eval("ACTTYP") %>'>
                                                                    <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                    <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                    <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSubmit" runat="server" Text="Submit"
                                                                    OnClick="lnkSubmit_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <EmptyDataTemplate>
                                                        <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                            <tr>
                                                                <td class="GridViewHeader" style="width: 10%">
                                                                    <asp:Literal ID="Literal6" runat="server" Text="Click on 'New Claim'" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div id="tblActionType" runat="server" visible="false" class="form-group">
                                            <div class="col-sm-6">
                                                <label class="col-sm-6 label">Remarks :</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtAllRemarks" runat="server" TextMode="MultiLine" CssClass="input"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="col-sm-6">

                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="drpStatus" runat="server" AutoPostBack="true" OnTextChanged="drpStatus_TextChanged"
                                                        CssClass="input" Width="150px">
                                                        <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                        <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                        <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-sm-6">
                                                    <asp:Button ID="btnSubmit" runat="server" Enabled="false" CssClass="btn btn-primary"
                                                        OnClick="btnSubmit_Click" Text="Submit" />
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnGenerate" />
                                    <asp:PostBackTrigger ControlID="btnBack" />
                                    <asp:PostBackTrigger ControlID="btnExport" />
                                    <asp:PostBackTrigger ControlID="gvLeave" />
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                    <%-- <asp:PostBackTrigger ControlID="chk6" />
                                    <asp:PostBackTrigger ControlID="chk7" />
                                    <asp:PostBackTrigger ControlID="chk8" />
                                    <asp:PostBackTrigger ControlID="radioPrimary1" />
                                    <asp:PostBackTrigger ControlID="radioPrimary2" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="drpType" />
                                    <asp:PostBackTrigger ControlID="btnCalTtl" />--%>
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>


</asp:Content>
