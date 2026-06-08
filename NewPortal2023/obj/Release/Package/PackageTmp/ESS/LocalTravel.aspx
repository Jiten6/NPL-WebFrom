<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="LocalTravel.aspx.cs" Inherits="NewPortal2023.ESS.LocalTravel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--    <script type="text/javascript">
        $(function () {
            $('[id*=txtFromDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
        $(function () {
            $('[id*=txtToDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
    </script>--%>
    <%-- <script type="text/javascript">
        $(function () {
            $('[id*=txtFromDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
        $(function () {
            $('[id*=txtToDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
    </script>--%>

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

    <script>
        function showAlert() {
            var textboxValue = document.getElementById('txtTravelAmt').value;
            alert("Text changed to: " + textboxValue);
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Local Travel</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>



                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div id="form1" runat="server">
                                        <div id="divAlert" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                        </div>

                                        <section id="SectionList" class="content-header" runat="server">

                                            <div class="container-fluid">
                                                <!-- SELECT2 EXAMPLE -->
                                                <div class="card card-default">

                                                    <div class="card-body">

                                                        <div class="card-header" runat="server" id="divnotes">

                                                            <div id="div1" runat="server" class="col-sm-12">

                                                                <div class="col-sm-12 ">
                                                                    <div class="col-sm-12 table table-bordered">

                                                                        <label style="color: red">Notes :- </label>
                                                                        &emsp;&emsp;&emsp;
                                                    <asp:Label runat="server" BackColor="Orange">&emsp;&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                    <asp:Label runat="server" Text="Beverage" Style="text-align: left;"></asp:Label>
                                                                        &emsp;&emsp;&emsp;&emsp;
                                                    <asp:Label runat="server" BackColor="LightGreen">&emsp;&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                    <asp:Label runat="server" Text="Non-Beverage" Style="text-align: left;"></asp:Label>

                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <br />

                                                        <div class="row">
                                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="showLoader();" />
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <label style="font-size: 30px;">List Of Local Claim</label>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-12">

                                                                <%--<asp:GridView ID="gvLocalClaimList" OnRowDataBound="gvLocalClaimList_RowDataBound" runat="server" ToolTip="LOCAL LIST CLAIM" AutoGenerateColumns="False" class="table table-bordered table-condensed">--%>
                                                                <asp:GridView ID="gvLocalClaimList" runat="server" AutoGenerateColumns="False"
                                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvLocalClaimList_RowDataBound"
                                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                    <Columns>
                                                                        <%--<asp:TemplateField ShowHeader="true">
                                                        <ItemStyle VerticalAlign="Middle" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Sr.No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSrNo" runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee AId" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmpAId" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Emp_AId") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim No">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkLOCClmNoClmNo" CssClass="form-group" OnClick="lnkLOCClmNoClmNo_Click" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_no") %>'
                                                                                    OnClientClick="showLoader();" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Date","{0:dd/MMM/yyyy}") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Expense Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txFrDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Expenses_Date","{0:dd/MMM/yyyy}") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name of Business/Assignment">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtFrDest" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Name_Bussi_Ass") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Travelled Description">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtToDest" ReadOnly="true" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Travel_Description") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmAmt" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Amount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtAppAmtr" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ClaimApproved_Amount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtRmk" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Status") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="EntertainmaentChecked" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="EntertainmentChked" TextMode="MultiLine" Visible="false" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EntertainmentChked") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#0069d9" Font-Bold="True" ForeColor="White" />
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <EmptyDataTemplate>
                                                                        <table class="table table-bordered table-striped">
                                                                            <tr>
                                                                                <td style="width: 10%" class="Title">
                                                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </EmptyDataTemplate>

                                                                </asp:GridView>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </section>

                                        <section id="Sectionadvlist" class="content-header" runat="server">

                                            <div class="container-fluid">
                                                <!-- SELECT2 EXAMPLE -->
                                                <div class="card card-default">

                                                    <div class="card-body">

                                                        <br />
                                                        <br />
                                                        <label style="font-size: 30px;">List Of Advance Claim</label>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-12">

                                                                <%--<asp:GridView ID="gvLocalClaimList" OnRowDataBound="gvLocalClaimList_RowDataBound" runat="server" ToolTip="LOCAL LIST CLAIM" AutoGenerateColumns="False" class="table table-bordered table-condensed">--%>
                                                                <asp:GridView ID="gvAdvanceClaimList" runat="server" AutoGenerateColumns="False"
                                                                    HorizontalAlign="Left" CellPadding="5"
                                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                    <Columns>
                                                                        <%--<asp:TemplateField ShowHeader="true">
                                                        <ItemStyle VerticalAlign="Middle" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Sr.No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSrNo" runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Advance No" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lnkappClmNo" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ADVANCE_ID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Advance No">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkadvClmNoClmNo" CssClass="form-group" OnClick="lnkadvClmNoClmNo_Click" Style="width: 100%;" runat="Server" ForeColor="Blue" Text='<%# Eval("ADVANCE_ID") %>'
                                                                                    OnClientClick="showLoader();" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmpcode" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_CODE") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtempname" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Advance Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtadvDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ADVANCE_DATE","{0:dd/MMM/yyyy}") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Expense Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txttype" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EXPENSE_TYPE") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Advance Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtadvAmt" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ADVANCE_AMOUNT") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtstatus" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("STATUS") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim Advance">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnclaim" CssClass="btn btn-success" OnClick="btnclaim_Click" runat="server" Style="width: 100%;" Text="Claim" OnClientClick="showLoader();" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#0069d9" Font-Bold="True" ForeColor="White" />
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <EmptyDataTemplate>
                                                                        <table class="table table-bordered table-striped">
                                                                            <tr>
                                                                                <td style="width: 10%" class="Title">
                                                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </EmptyDataTemplate>

                                                                </asp:GridView>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </section>

                                        <div id="divFrom" runat="server" visible="false">
                                            <section class="content-header">
                                                <div class="container-fluid">
                                                    <div class="row mb-2">
                                                        <div class="col-sm-6">
                                                            <h1>Local Expense Form</h1>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <ol class="breadcrumb float-sm-right">
                                                                <li class="breadcrumb-item"><a href="#">Home</a></li>

                                                                <li class="breadcrumb-item active">Local Expense Form</li>
                                                            </ol>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- /.container-fluid -->
                                            </section>
                                            <section class="content">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">


                                                        <div class="card-body">

                                                            <h2 class="card-title" style="font-size: 18px"><b>Travel Type</b></h2>



                                                            <br />
                                                            <br />
                                                            <div class="form-group row">
                                                                <div class="col-sm-12">
                                                                    <label>Local Conveyance</label>
                                                                </div>
                                                                <br />
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="grLocalReimb" runat="server" ToolTip="Local Reimbursement" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Mode">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMode" runat="Server" Text='Mode Of Travel' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type Of Travel">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTypeOfTravel" runat="Server" Text='<%# Eval("REIMBURSEMENT_TYPE") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <%-- <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width: 10%" class="Title">--%>
                                                                            <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                            <%--</td>
                                                    </tr>
                                                </table>--%>
                                                                        </EmptyDataTemplate>

                                                                    </asp:GridView>

                                                                </div>
                                                            </div>

                                                            <!-- checkbox -->
                                                            <div class="form-group">
                                                                <div class="form-group row">

                                                                    <br />
                                                                    <br />
                                                                    <div class="icheck-primary d-inline col-sm-2">
                                                                        &emsp;<asp:CheckBox ID="chk1" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="chk1_CheckedChanged" Text="Self/Company Car" />
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtchk1" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtchk1"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>
                                                                    <div class="icheck-primary d-inline col-sm-2">
                                                                        <asp:CheckBox ID="chk2" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="chk2_CheckedChanged" Text="Train" />
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtchk2" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtchk2"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>

                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="icheck-primary d-inline col-sm-2">
                                                                        &emsp;<asp:CheckBox ID="chk3" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="chk3_CheckedChanged" Text="Taxi/Auto" />
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtchk3" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk3_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtchk3"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>
                                                                    <div class="icheck-primary d-inline col-sm-2">
                                                                        <asp:CheckBox ID="chk4" class="icheck-primary d-inline col-sm-2" runat="server" AutoPostBack="true" OnCheckedChanged="chk4_CheckedChanged" Text="Bus" />
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtchk4" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk4_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtchk4"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />

                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <%--span2 datepicker-dropdown menu-open--%>
                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="lblDate" runat="server" class="col-sm-3 col-form-label">Date of Expense :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control datepicker" AutoCompleteType="Disabled" ID="txtFromDate" runat="server"></asp:TextBox>

                                                                </div>
                                                                <asp:Label ID="lblNameAss" runat="server" class="col-sm-3 col-form-label">Name of Business / Assignment :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox CssClass="form-control" ID="txtNameAss" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="lblCashVocher" runat="server" class="col-sm-3 col-form-label">Cash Voucher, if any, attached No.:-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtCashVocher" runat="server"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="lblTravelDes" runat="server" class="col-sm-3 col-form-label">Travel Description :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtTravelDes" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="lblMeal" runat="server" class="col-sm-3 col-form-label">Meal :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtMeal" runat="server" OnTextChanged="txtMeal_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtMeal"
                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                        Display="Dynamic" ForeColor="Red" />
                                                                </div>
                                                                <asp:Label ID="lblOtherExpenses" runat="server" class="col-sm-3 col-form-label">Other Expenses :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtOtherExpenses" runat="server" OnTextChanged="txtOtherExpenses_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtOtherExpenses"
                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                        Display="Dynamic" ForeColor="Red" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <asp:Label ID="lbladvid" runat="server" AutoCompleteType="Disabled" class="col-sm-3 col-form-label">Advance ID :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtadvid" runat="server"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label2" runat="server" class="col-sm-3 col-form-label">Remarks :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtUserRemarks" runat="server"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="lbladv" runat="server" class="col-sm-3 col-form-label">Advance Paid :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtadv" runat="server" OnTextChanged="txtadv_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtadv"
                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                        Display="Dynamic" ForeColor="Red" />
                                                                </div>

                                                                <asp:Label ID="lbladvvoucher" runat="server" class="col-sm-3 col-form-label">Advance Voucher :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtadvvoucher" runat="server"></asp:TextBox>
                                                                </div>
                                                                <br />

                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="lblTtlexp" runat="server" class="col-sm-3 col-form-label"><b>Total Expense :-</b></asp:Label>
                                                                <%--<asp:Button ID="btnCalTtl" CssClass="btn btn-primary" OnClick="btnCalTtl_Click" runat="server" Text="Calculate Total Expense" />--%>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox class="form-control" ID="txtTotalexp" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="paidamt" runat="server" class="col-sm-3 col-form-label"><b>Paid Amount :-</b></asp:Label>
                                                                <%--<asp:Button ID="btnCalTtl" CssClass="btn btn-primary" OnClick="btnCalTtl_Click" runat="server" Text="Calculate Total Expense" />--%>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox class="form-control" ID="txtpaidamt" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>

                                            </section>
                                            <section class="content" id="divTravel" runat="server" visible="false">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-header">
                                                            <h2 class="card-title" style="font-size: 18px"><b>Upload Supporting Documents</b></h2>
                                                            <br />
                                                            <!-- iCheck -->

                                                            <div class="card-body">

                                                                <div class="row" id="divUpload" runat="server" visible="false">
                                                                    <div class="col-lg-6" id="divfileUpload" runat="server" visible="false">
                                                                        <div class="btn-group w-100">
                                                                            <asp:FileUpload ID="fupldDocument" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                            <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-6" id="divfileDisplay" runat="server" visible="false">
                                                                        Supporting files:
                                            <asp:GridView ID="gvDomesticFile" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileStorage" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenFiles" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                AutoPostBack="true" OnClick="lnkBtnOpenFiles_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                </EmptyDataTemplate>
                                            </asp:GridView>


                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </section>


                                            <section class="content">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">


                                                        <div class="card-body">
                                                            <div class="col-sm-12" style="text-align: center;">
                                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" OnClick="btnClose_Click" runat="server" Text="Close" OnClientClick="showLoader();" />
                                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Submit" CssClass="btn btn-success" OnClientClick="showLoader();" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </div>

                                        <div id="divadvclaim" runat="server" visible="false">
                                            <section class="content-header">
                                                <div class="container-fluid">
                                                    <div class="row mb-2">
                                                        <div class="col-sm-6">
                                                            <h1>Advance Expense Form</h1>
                                                        </div>

                                                    </div>
                                                </div>
                                                <!-- /.container-fluid -->
                                            </section>
                                            <br />
                                            <br />
                                            <section class="content">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">

                                                        <div class="card-body">
                                                            <div class="form-group row">
                                                                <asp:Label ID="lblempcode" runat="server" class="col-sm-3 col-form-label">Employee Code.:-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtempcode" runat="server"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="lblname" runat="server" class="col-sm-3 col-form-label">Employee Name.:-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtname" runat="server" Width="250px"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label1" runat="server" class="col-sm-3 col-form-label">Date of Advance:-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control datepicker-dropdown menu-open" AutoCompleteType="Disabled" ID="txtDate" runat="server"></asp:TextBox>

                                                                </div>
                                                                <asp:Label ID="lbltype" runat="server" class="col-sm-3 col-form-label">Expense Type.:-</asp:Label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="drptypelist" runat="server" CssClass="form-control" Width="150px">
                                                                        <asp:ListItem Value="">[Select Type]</asp:ListItem>
                                                                        <asp:ListItem Value="Domestic" Text="Domestic"></asp:ListItem>
                                                                        <asp:ListItem Value="Local" Text="Local"></asp:ListItem>
                                                                        <asp:ListItem Value="Welfare" Text="Staff Welfare"></asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <asp:Label ID="lblvoucher" runat="server" class="col-sm-3 col-form-label">Advance Voucher :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtvoucher" runat="server"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="lblamt" runat="server" class="col-sm-3 col-form-label">Advance Amount.:-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtadvclaimamt" runat="server"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtadvclaimamt"
                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                        Display="Dynamic" ForeColor="Red" />
                                                                </div>

                                                            </div>
                                                            <br />
                                                            <div class="form-group row">

                                                                <asp:Label ID="lblpur" runat="server" class="col-sm-3 col-form-label">Purpose of Advance :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtpuradv" runat="server"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                            </section>
                                            <section class="content">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">


                                                        <div class="card-body">
                                                            <div class="col-sm-12" style="text-align: center;">
                                                                <asp:Button ID="btnadvcancel" CssClass="btn btn-danger" OnClick="btnadvClose_Click" runat="server" Text="Close" OnClientClick="showLoader();" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>



                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="chk1" />
                                    <asp:PostBackTrigger ControlID="chk2" />
                                    <asp:PostBackTrigger ControlID="chk3" />
                                    <asp:PostBackTrigger ControlID="chk4" />
                                    <asp:PostBackTrigger ControlID="txtMeal" />
                                    <asp:PostBackTrigger ControlID="txtOtherExpenses" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnAddNew" />
                                    <asp:PostBackTrigger ControlID="gvLocalClaimList" />
                                    <asp:PostBackTrigger ControlID="gvDomesticFile" />
                                    <asp:PostBackTrigger ControlID="btnadvcancel" />
                                    <asp:PostBackTrigger ControlID="gvAdvanceClaimList" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
