<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="StaffWellfare.aspx.cs" Inherits="NewPortal2023.ESS.StaffWellfare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/litepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ConfirmDel() {
            return confirm("Are you sure you want to delete this record?");
        }

        //        function ValidateMaintenance(drp) {
        //            var val = drp.value;
        //            if (val == "259") {
        //                return true;
        //            }
        //            else {
        //                return false;
        //            }
        //        }

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


    <style>
        .hidden {
            display: none;
        }

        .visible {
            display: block;
        }

        .custom-checkbox {
            /* Add your custom styles here */
            /* For example: */
            margin-right: 10px; /* Add margin to separate checkbox from text */
            font-weight: bold; /* Make the text bold */
            color: #333; /* Set text color */
            /* You can add more styles as needed */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <header class="panel-heading" style="background-color: darkcyan">
                    <h3 style="color: white">Staff Welfare Expense</h3>
                </header>
                <div class="panel-body">
                    <asp:ScriptManager ID="ScriptManager2" runat="server">
                        <Scripts>
                            <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                            <asp:ScriptReference Path="~/ESS/blockUI.js" />
                        </Scripts>
                    </asp:ScriptManager>

                    <div id="Domastic" runat="server" visible="false" class="col-sm-12">
                        <section class="panel">

                            <%--  <header class="panel-heading" style="background-color: darkcyan">
                                <h3 style="color: white">Staff Welfare Expenses</h3>
                            </header>--%>

                            <%--   <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>--%>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" EnableViewState="true">
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
                                                    <div class="card-header" id="notes">

                                                        <div id="div1" runat="server" class="col-sm-12">

                                                            <div class="col-sm-12 ">
                                                                <div class="col-sm-12 table table-bordered form-horizontal">

                                                                    <label style="color: red">Notes :- </label>
                                                                    <div>
                                                                        &emsp;&emsp;
                                                    <asp:Label runat="server" BackColor="Orange">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                    <asp:Label runat="server" Text="Eligible- Beverage" Style="text-align: left;"></asp:Label>
                                                                        &emsp;&emsp;&emsp;&emsp;&emsp;
                                                    <asp:Label runat="server" BackColor="LightGreen">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                    <asp:Label runat="server" Text="Eligible Non-Beverage" Style="text-align: left;"></asp:Label>
                                                                    </div>

                                                                    <div>
                                                                        &emsp;&emsp;
                                                    <asp:Label runat="server" BackColor="Red">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                    <asp:Label runat="server" Text="Non-Eligible- Beverage" Style="text-align: left;"></asp:Label>
                                                                        &emsp;&emsp;&emsp;
                                                    <asp:Label runat="server" BackColor="Yellow">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                    <asp:Label runat="server" Text="Non-Eligible Non-Beverage" Style="text-align: left;"></asp:Label>
                                                                    </div>


                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <section class="content" id="Section2" runat="server">
                                                        <div class="container-fluid">
                                                            <div class="card card-default">
                                                                <div class="card-body">
                                                                    <div class="form-group col-md-6">
                                                                        <h3 class="page-header" style="color: darkcyan; margin-left: -15px">Expense Type</h3>

                                                                        <asp:DropDownList ID="drpTypeexpense" runat="server" CssClass="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpTypeexpense_SelectedIndexChanged"
                                                                            AutoPostBack="true" onchange="showLoader();">
                                                                            <asp:ListItem Value="">[Select Type]</asp:ListItem>
                                                                            <asp:ListItem Value="Domestic" Text="Domestic"></asp:ListItem>
                                                                            <asp:ListItem Value="Local" Text="Local"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </section>
                                                    <div id="divdom" runat="server" class="card-body">

                                                        <div class="row">
                                                            <div class="col-6">
                                                                <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="showLoader();" />
                                                                &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
                                                                 <asp:Button ID="btnback" OnClick="btnback_Click" Style="margin-right: 150px;" runat="server" CssClass="btn btn-secondary" Text="Back" OnClientClick="showLoader();" />
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <label style="font-size: 30px;">List Of Staff Welfare Claim</label>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-12">

                                                                <%--<asp:GridView ID="gvDomClaim" runat="server" OnRowDataBound="gvDomClaim_RowDataBound" ToolTip="DOMESTIC LIST CLAIM" AutoGenerateColumns="False" class="table table-bordered table-striped">--%>
                                                                <asp:GridView ID="gvDomClaim" runat="server" AutoGenerateColumns="False"
                                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvDomClaim_RowDataBound"
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
                                                                                <asp:LinkButton ID="lnkDOMClmNoClmNo" CssClass="form-group" OnClick="lnkDOMClmNoClmNo_Click" Style="width: 100%;" TextMode="MultiLine" runat="Server" ForeColor="Blue" Text='<%# Eval("App_AId") %>'
                                                                                    OnClientClick="showLoader();" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("CreateDate","{0: dd-MMM-yyyy hh:mm tt }") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Travel Date" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txFrDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("FromDate","{0: dd-MMM-yyyy}") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Travelled From">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtFrDest" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("StartDestination") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Travelled To">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtToDest" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EndDestination") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim Type" HeaderStyle-Width="74.2px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTravelType" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Traveltype") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="DESCRIPTION_EXP">
                                            <ItemTemplate>
                                                <asp:Label ID="txtdescri" TextMode="multiline" CssClass="from-group" Style ="width: 100%;" runat="Server" Text="<%# Eval("DESCRIPTION_EXP") %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Total Claim Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmAmt" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Total_Expense") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtAppAmtr" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ApprovedAmmount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRmk" CssClass="form-group" Style="width: 100%;" Enabled="false" runat="Server" Text='<%# Eval("Status") %>' />
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

                                        <div id="divFrom" runat="server" visible="false">

                                            <section class="content-header">
                                                <div class="container-fluid">
                                                    <div class="row mb-2">
                                                        <div class="col-sm-6">
                                                            <h1 style="color: darkorange">Staff Welfare Expense Form</h1>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- /.container-fluid -->
                                            </section>

                                            <section class="content" id="traveltype" visible="false" runat="server">
                                                <div class="container-fluid">
                                                    <div class="card card-default">
                                                        <div class="card-body">
                                                            <div class="form-group col-md-6">

                                                                <h3 class="page-header" style="color: darkcyan; margin-left: -15px">Type</h3>

                                                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpType_SelectedIndexChanged"
                                                                    AutoPostBack="true" onchange="showLoader();">
                                                                    <asp:ListItem Value="">[Select Type]</asp:ListItem>
                                                                    <asp:ListItem Value="Travel" Text="Travel"></asp:ListItem>
                                                                    <asp:ListItem Value="Entertainment" Text="Entertainment"></asp:ListItem>
                                                                    <asp:ListItem Value="Travel + Entertainment" Text="Travel + Entertainment"></asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>

                                            <hr />

                                            <section class="content" id="travelClass" runat="server">
                                                <div class="form-group" runat="server" id="Class_Travel">
                                                    <div class="container-fluid">
                                                        <div class="card card-default">
                                                            <div class="card-body">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <br />
                                                                        <div class="form-group row" style="padding-left: 20px">
                                                                            <asp:Label ID="Label13" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold; font-size: 15px"> Expense Description :-</asp:Label>
                                                                            <div class="col-sm-2">
                                                                                <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtdescription" runat="server" Style="width: 700px; height: 50px;"></asp:TextBox>

                                                                            </div>
                                                                        </div>

                                                                        <hr />

                                                                        <div class="form-group">

                                                                            <h4 id="lblClassTravel1" runat="server" visible="false" class="page-header" style="color: darkcyan">Class Of Travels</h4>

                                                                            <br />


                                                                            <div class="form-group" style="padding-left: 20px">
                                                                                <div class="form-group row">
                                                                                    <div id="divChk1" runat="server" class="icheck-primary d-inline col-sm-3">
                                                                                        <asp:CheckBox ID="chk1" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" Text="&emsp;Air (Business Class)" />

                                                                                    </div>
                                                                                    <div id="divTxtChk1" runat="server" class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" ID="txtchk1" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtchk1"
                                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                                            Display="Dynamic" ForeColor="Red" />
                                                                                    </div>

                                                                                    <div class="icheck-primary d-inline col-sm-3">
                                                                                        <asp:CheckBox ID="chk2" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk2_CheckedChanged" Text="&emsp;Air (Economy Class)" />

                                                                                    </div>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" ID="txtchk2" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtchk2"
                                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                                            Display="Dynamic" ForeColor="Red" />

                                                                                    </div>
                                                                                </div>

                                                                                <div class="form-group row">
                                                                                    <div class="icheck-primary d-inline col-sm-3">
                                                                                        <asp:CheckBox ID="chk3" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk3_CheckedChanged" Text="&emsp;Rail (AC 1st Class)" />

                                                                                    </div>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" ID="txtchk3" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk3_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtchk3"
                                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                                            Display="Dynamic" ForeColor="Red" />
                                                                                    </div>

                                                                                    <div class="icheck-primary d-inline col-sm-3">
                                                                                        <asp:CheckBox ID="chk4" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk4_CheckedChanged" Text="&emsp;Rail (AC 2nd Class)" />

                                                                                    </div>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" ID="txtchk4" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk4_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtchk4"
                                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                                            Display="Dynamic" ForeColor="Red" />
                                                                                    </div>
                                                                                </div>

                                                                                <div class="form-group row">
                                                                                    <div class="icheck-primary d-inline col-sm-3">
                                                                                        <asp:CheckBox ID="chk5" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk5_CheckedChanged" Text="&emsp;Rail (AC 3rd Class)" />

                                                                                    </div>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" ID="txtchk5" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk5_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtchk5"
                                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                                            Display="Dynamic" ForeColor="Red" />
                                                                                    </div>

                                                                                    <div class="icheck-primary d-inline col-sm-3">
                                                                                        <asp:CheckBox ID="chk6" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk6_CheckedChanged" Text="&emsp;Rail (AC Chair Car)" />

                                                                                    </div>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" ID="txtchk6" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk6_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtchk6"
                                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                                            Display="Dynamic" ForeColor="Red" />
                                                                                    </div>
                                                                                </div>

                                                                                <div class="form-group row">
                                                                                    <div class="icheck-primary d-inline col-sm-3">
                                                                                        <asp:CheckBox ID="chk7" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk7_CheckedChanged" Text="&emsp;Bus" />

                                                                                    </div>

                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" ID="txtchk7" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk7_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtchk7"
                                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                                            Display="Dynamic" ForeColor="Red" />
                                                                                    </div>

                                                                                    <div class="icheck-primary d-inline col-sm-3">
                                                                                        <asp:CheckBox ID="chk8" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk8_CheckedChanged" Text="&emsp;Other" />

                                                                                    </div>

                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" ID="txtchk8" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk8_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtchk8"
                                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                                            Display="Dynamic" ForeColor="Red" />
                                                                                    </div>
                                                                                </div>

                                                                                <hr />

                                                                                <div class="form-group row">
                                                                                    <asp:Label ID="Label7" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">From Date :-</asp:Label>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control datepicker" ID="txtFromDate" runat="server" AutoCompleteType="Disabled" OnTextChanged="txtFromDate_TextChanged" placeholder="Date" onchange="validateDates()"></asp:TextBox>

                                                                                    </div>
                                                                                    <asp:Label ID="Label9" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">To Date :-</asp:Label>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox Class="form-control datepicker" ID="txtToDate" runat="server" AutoCompleteType="Disabled" OnTextChanged="txtToDate_TextChanged" placeholder="Date" onchange="validateDates()"></asp:TextBox>

                                                                                    </div>
                                                                                </div>

                                                                                <br />
                                                                                <div class="form-group row">
                                                                                    <asp:Label ID="Label4" runat="server" AutoCompleteType="Disabled" class="col-sm-3 col-form-label" Style="font-weight: bold">Travel Source :-</asp:Label>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" ID="txtStartDest" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                    <asp:Label ID="Label6" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Travel Destination :-</asp:Label>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:TextBox class="form-control" AutoCompleteType="Disabled" ID="txtEndDest" runat="server"></asp:TextBox>

                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </section>

                                            <section class="content" id="divTravel" runat="server" visible="false">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-header">

                                                            <h4 class="page-header" style="color: darkcyan">Daily Reimbursement Amount in Rs. (Per day)</h4>
                                                            <br />
                                                            <!-- iCheck -->
                                                            <div class="card-body">
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <!-- radio -->
                                                                        <div class="form-group clearfix">
                                                                            <div class="icheck-primary d-inline">
                                                                                <%--<asp:RadioButton ID="radioPrimary1" Text="Metro Cities" runat="server" OnCheckedChanged="radioPrimary1_CheckedChanged" AutoPostBack="true" GroupName="GroupType" Style="width: 20px; height: 20px; margin-right: 5px;"></asp:RadioButton>

                                                                                <asp:RadioButton GroupName="radio" ID="rdMale" Enabled="false" runat="server" Style="width: 20px; height: 20px; margin-right: 5px;" />--%>

                                                                                <label style="display: inline-flex; align-items: center; font-size: 16px; cursor: pointer; color: cornflowerblue;">
                                                                                    <asp:RadioButton GroupName="GroupType" ID="radioPrimary1" runat="server" Style="width: 20px; height: 20px; margin-right: 5px;" AutoPostBack="true" OnCheckedChanged="radioPrimary1_CheckedChanged" />
                                                                                    Metro Cities
                                                                                </label>
                                                                            </div>
                                                                            <div class="icheck-primary d-inline">
                                                                                <%--<asp:RadioButton ID="radioPrimary2" Text="Non Metro Cities" runat="server" OnCheckedChanged="radioPrimary1_CheckedChanged" AutoPostBack="true" GroupName="GroupType" Style="font-size: 15px"></asp:RadioButton>--%>

                                                                                <label style="display: inline-flex; align-items: center; font-size: 16px; cursor: pointer; color: maroon;">
                                                                                    <asp:RadioButton GroupName="GroupType" ID="radioPrimary2" runat="server" Style="width: 20px; height: 20px; margin-right: 5px;" AutoPostBack="true" OnCheckedChanged="radioPrimary1_CheckedChanged" />
                                                                                    Non Metro Cities
                                                                                </label>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group" style="padding-left: 20px">
                                                                    <div class="row" id="divMetro" runat="server" visible="false">
                                                                        <div class="col-sm-12">
                                                                            <label>
                                                                                Metro Cities :- (Mumbai, Chennai, Delhi, Kolkata, Hyderabad and Bangalore)
                                                                            </label>

                                                                            <asp:GridView ID="grMetro" runat="server" ToolTip="Metro Cities Daily Reimbursement" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Lodging">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblLodging" runat="Server" Text='<%# Eval("LODGING_AMT") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Boarding">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblBoarding" runat="Server" Text='<%# Eval("BOARDING_AMT") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Conveyance">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="txtBoarding" CssClass="form-group" runat="Server" Text='<%# Eval("CONVEYANCE") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Miscellaneous">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblMiscellaneous" CssClass="form-group" runat="Server" Text='<%# Eval("MISCELLANEOUS") %>' />
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

                                                                    <div class="row" id="divNonMetro" runat="server" visible="false">
                                                                        <div class="col-sm-12">
                                                                            <label for="radioPrimary1">
                                                                                Non Metro Cities :- Other than (Mumbai, Chennai, Delhi, Kolkata, Hyderabad and Bangalore)

                                                                            </label>

                                                                            <asp:GridView ID="grNonMetro" runat="server" ToolTip="Non Metro Cities Daily Reimbursement" AutoGenerateColumns="False" class="table table-bordered table-condensed">
                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderText="Lodging">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNonLodging" runat="Server" Text='<%# Eval("LODGING_AMT") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Boarding">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNonBoarding" runat="Server" Text='<%# Eval("BOARDING_AMT") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Conveyance">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="txtBoarding" CssClass="form-group" runat="Server" Text='<%# Eval("CONVEYANCE") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Miscellaneous">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNonMiscellaneous" CssClass="form-group" runat="Server" Text='<%# Eval("MISCELLANEOUS") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>

                                                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />

                                                                                </EmptyDataTemplate>

                                                                            </asp:GridView>

                                                                        </div>
                                                                    </div>

                                                                    <br />

                                                                    <div class="form-group row" id="divAmt" runat="server" visible="false">
                                                                        <asp:Label ID="Label2" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">No of Days :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" ID="txtNoDays" Enabled="false" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        &emsp;&emsp;
                                                                    <asp:Label ID="Label3" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Eligibility Amount :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" ID="txtligibility" Enabled="false" runat="server"></asp:TextBox>
                                                                        </div>

                                                                        <br />

                                                                    </div>

                                                                    <div class="form-group row" id="divEligi" runat="server" visible="false">
                                                                        <asp:Label ID="Label10" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Claim Amount :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" ID="txtTravelAmt" AutoCompleteType="Disabled" OnTextChanged="txtTravelAmt_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="txtTravelAmt"
                                                                                ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                ErrorMessage="Please enter a valid numeric value."
                                                                                Display="Dynamic" ForeColor="Red" />
                                                                        </div>
                                                                        &emsp;&emsp;
                                                                    <asp:Label ID="Label1" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Remarks :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtUserTravelRemarks" runat="server"></asp:TextBox>
                                                                        </div>

                                                                        <br />

                                                                    </div>
                                                                    <div class="form-group row" id="divadv" runat="server" visible="false">
                                                                        <asp:Label ID="lbladvanceamt" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Advance Paid :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" ID="txtadvance" AutoCompleteType="Disabled" runat="server" AutoPostBack="true" OnTextChanged="txtadvance_TextChanged"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtadvance"
                                                                                ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                ErrorMessage="Please enter a valid numeric value."
                                                                                Display="Dynamic" ForeColor="Red" />
                                                                        </div>
                                                                        <%--   &emsp;&emsp;
                                                    <asp:Label ID="Label17" runat="server" class="col-sm-3 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="TextBox2" runat="server"></asp:TextBox>
                                                                    </div>--%>

                                                                        <br />

                                                                    </div>

                                                                    <div class="row" id="divUpload" runat="server" visible="false">
                                                                        <div class="col-lg-6" id="divfileUpload" runat="server" visible="false">
                                                                            <div class="btn-group w-100">
                                                                                <asp:FileUpload ID="fupldDocument" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                                <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-6" id="divfileDisplay" runat="server" visible="false" style="font-weight: bold">
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
                                                                                AutoPostBack="true" OnClick="lnkBtnOpenFile_Click" />
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
                                                </div>
                                            </section>

                                            <section class="content" id="divEntertainment" runat="server" visible="false">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-header">

                                                            <h4 class="page-header" style="color: darkcyan">Entertainment</h4>

                                                            <div class="card-body" style="padding-left: 20px">

                                                                <div class="form-group row">

                                                                    <asp:Label ID="Label11" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Entertainment Description :-</asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtEnterDesc" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">


                                                                    <asp:Label ID="Label5" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Claim Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" AutoCompleteType="Disabled" ID="txtEntAmount" OnTextChanged="txtEntAmount_TextChanged" AutoPostBack="true" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <asp:Label ID="Label8" runat="server" class="col-sm-2 col-form-label" Style="font-weight: bold">Remarks :-</asp:Label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtUserEligiRemark" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <br />

                                                                </div>

                                                                <div class="row" id="divUploadEnter" runat="server" visible="false">
                                                                    <div class="col-lg-6" id="divfileUploadEnter" runat="server" visible="false">
                                                                        <div class="btn-group w-100">
                                                                            <asp:FileUpload ID="fupldDocumentEnter" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                            <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-6" id="divfileDisplayEnter" runat="server" visible="false">
                                                                        Supporting files:
                                                                        <asp:GridView ID="gvEntertainment" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                            class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                                            <Columns>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFileStorageEnter" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Uploaded Files">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkBtnOpenFileEnter" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                            AutoPostback="true" OnClick="lnkBtnOpenFileEnter_Click" />
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

                                            <hr />

                                            <section class="content" id="abs" runat="server">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-body">
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>

                                            <section class="content" id="DivAction" runat="server">
                                                <div class="container-fluid">

                                                    <div class="card card-default">
                                                        <div>
                                                            <br />
                                                        </div>

                                                        <div class="form-group row" id="eligibility" runat="server">
                                                            &emsp;&emsp;
                                                            <asp:Label ID="Label14" runat="server" class="col-sm-4 col-form-label" Style="font-size: 15px"><b>Eligibility Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="elgAmount" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row" id="claimAmt" runat="server">
                                                            &emsp;&emsp;
                                                            <asp:Label ID="Label15" runat="server" class="col-sm-4 col-form-label" Style="font-size: 15px"><b>Claim Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="claimAmount" AutoCompleteType="Disabled" Enabled="false" AutoPostBack="true" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row" id="advamt" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="Label16" runat="server" class="col-sm-4 col-form-label"><b>Advance Paid :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="txtadvamt" AutoCompleteType="Disabled" Enabled="false" AutoPostBack="true" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>

                                                        <div class="form-group row" id="TotalExpns" runat="server">
                                                            &emsp;&emsp;
                                                            <asp:Label ID="lblTtlexp" runat="server" class="col-sm-4 col-form-label" Style="font-size: 15px"><b>Total Expense (Travel fare + Claim Amount - Advance Amount) :-</b></asp:Label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox class="form-control" ID="txtTotalexp" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row" id="Section1" runat="server">
                                                            <asp:Label ID="Label12" runat="server" class="col-sm-4 col-form-label" Style="font-size: 15px"><b>Approved Amount :-</b></asp:Label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox class="form-control" ID="txtApprovedAmt" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <hr />

                                                        <div class="card-body">
                                                            <div class="col-sm-12" style="text-align: center;">
                                                                <%--class="modal-footer justify-content-between"--%>
                                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" OnClick="btnClose_Click" runat="server" Text="Close" OnClientClick="showLoader();" />
                                                                <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" OnClick="btnSave_Click" Text="Submit" OnClientClick="showLoader();" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </div>

                                        <div id="Div9" runat="server" visible="false">
                                            <div id="div3" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                                <button data-dismiss="alert" class="close close-sm" type="button">
                                                    <i class="fa fa-times"></i>
                                                </button>
                                                <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
                                            </div>

                                            <section id="Section3" class="content-header" runat="server">

                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">

                                                        <div class="card-body">

                                                            <div class="card-header" runat="server" Style="margin-top: 15px;" id="divnotes">

                                                                <div id="div4" runat="server"  class="col-sm-12">

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
                                                                <asp:Button ID="btnAddLOCNew" OnClick="btnAddLOCNew_Click" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="showLoader();" />
                                                                &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
                                                                <asp:Button ID="btnlocback" OnClick="btnlocback_Click" Style="margin-left: 150px;" runat="server" CssClass="btn btn-primary" Text="Back" OnClientClick="showLoader();" />
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <label style="font-size: 30px;">List Of Local Staff Welfare Claim</label>
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

                                            <div id="div5" runat="server" visible="false">
                                                <section class="content-header">
                                                    <div class="container-fluid">
                                                        <div class="row mb-2">
                                                            <div class="col-sm-6">
                                                                <h1>Local Staff Welfare Expense Form</h1>
                                                            </div>
                                                            <%--<div class="col-sm-6">
                                                                <ol class="breadcrumb float-sm-right">
                                                                    <li class="breadcrumb-item"><a href="#">Home</a></li>

                                                                    <li class="breadcrumb-item active">Local Staff Welfare Expense Form</li>
                                                                </ol>
                                                            </div>--%>
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
                                                                <div class="form-group row">

                                                                    <br />
                                                                    <br />

                                                                    &emsp;<asp:CheckBox ID="CheckBox1" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Self/Company Car" />
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="TextBox1" placeholder="Amount" Visible="false" runat="server" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="TextBox1"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>

                                                                    <asp:CheckBox ID="CheckBox2" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged" Text="Train" />
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="TextBox2" placeholder="Amount" Visible="false" runat="server" OnTextChanged="TextBox2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="TextBox2"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>

                                                                </div>
                                                                <div class="form-group row">
                                                                    &emsp;<asp:CheckBox ID="CheckBox3" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox3_CheckedChanged" Text="Taxi/Auto" />
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="TextBox3" placeholder="Amount" Visible="false" runat="server" OnTextChanged="TextBox3_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="TextBox3"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>

                                                                    <asp:CheckBox ID="CheckBox4" class="icheck-primary d-inline col-sm-2" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox4_CheckedChanged" Text="Bus" />
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="TextBox4" placeholder="Amount" Visible="false" runat="server" OnTextChanged="TextBox4_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="TextBox4"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />

                                                                    </div>

                                                                </div>

                                                                <%--span2 datepicker-dropdown menu-open--%>
                                                                <br />
                                                                <div class="form-group row">
                                                                    <asp:Label ID="lblDate" runat="server" class="col-sm-3 col-form-label">Date of Expense :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled" ID="TextBox5" runat="server" AutoPostBack="true"></asp:TextBox>

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
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtMeal"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>
                                                                    <asp:Label ID="lblOtherExpenses" runat="server" class="col-sm-3 col-form-label">Other Expenses :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtOtherExpenses" runat="server" OnTextChanged="txtOtherExpenses_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtOtherExpenses"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <asp:Label ID="lbladv" runat="server" class="col-sm-3 col-form-label">Advance Paid :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtadv" runat="server" OnTextChanged="txtadv_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtadv"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>
                                                                    <asp:Label ID="Label18" runat="server" class="col-sm-3 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtUserRemarks" runat="server"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                                <div class="form-group row">
                                                                    <asp:Label ID="Label19" runat="server" class="col-sm-3 col-form-label"><b>Total Expense :-</b></asp:Label>
                                                                    <%--<asp:Button ID="btnCalTtl" CssClass="btn btn-primary" OnClick="btnCalTtl_Click" runat="server" Text="Calculate Total Expense" />--%>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox class="form-control" ID="TextBox6" Enabled="false" runat="server"></asp:TextBox>
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
                                                <section class="content" id="Section4" runat="server" visible="false">
                                                    <div class="container-fluid">
                                                        <!-- SELECT2 EXAMPLE -->
                                                        <div class="card card-default">
                                                            <div class="card-header">
                                                                <h2 class="card-title" style="font-size: 18px"><b>Upload Supporting Documents</b></h2>
                                                                <br />
                                                                <!-- iCheck -->

                                                                <div class="card-body">

                                                                    <div class="row" id="div6" runat="server" visible="false">
                                                                        <div class="col-lg-6" id="div7" runat="server" visible="false">
                                                                            <div class="btn-group w-100">
                                                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                                <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-6" id="div8" runat="server" visible="false">
                                                                            Supporting files:
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
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
                                                                    <asp:Button ID="btnlocClose" CssClass="btn btn-danger" OnClick="btnlocClose_Click" runat="server" Text="Close" OnClientClick="showLoader();" />
                                                                    <asp:Button ID="btnlocsave" runat="server" OnClick="btnlocsave_Click" Text="Submit" CssClass="btn btn-success" OnClientClick="showLoader();" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </section>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="chk1" />
                                    <asp:PostBackTrigger ControlID="chk2" />
                                    <asp:PostBackTrigger ControlID="chk3" />
                                    <asp:PostBackTrigger ControlID="chk4" />
                                    <asp:PostBackTrigger ControlID="chk5" />
                                    <asp:PostBackTrigger ControlID="chk6" />
                                    <asp:PostBackTrigger ControlID="chk7" />
                                    <asp:PostBackTrigger ControlID="chk8" />
                                    <asp:PostBackTrigger ControlID="txtchk1" />
                                    <asp:PostBackTrigger ControlID="txtchk2" />
                                    <asp:PostBackTrigger ControlID="txtchk3" />
                                    <asp:PostBackTrigger ControlID="txtchk4" />
                                    <asp:PostBackTrigger ControlID="txtchk5" />
                                    <asp:PostBackTrigger ControlID="txtchk6" />
                                    <asp:PostBackTrigger ControlID="txtchk7" />
                                    <asp:PostBackTrigger ControlID="txtchk8" />
                                    <asp:PostBackTrigger ControlID="radioPrimary1" />
                                    <asp:PostBackTrigger ControlID="radioPrimary2" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="drpType" />
                                    <asp:PostBackTrigger ControlID="txtTravelAmt" />
                                    <asp:PostBackTrigger ControlID="txtEntAmount" />
                                    <asp:PostBackTrigger ControlID="drpType" />
                                    <asp:PostBackTrigger ControlID="btnAddNew" />
                                    <asp:PostBackTrigger ControlID="gvDomClaim" />
                                    <asp:PostBackTrigger ControlID="gvEntertainment" />
                                    <asp:PostBackTrigger ControlID="gvDomesticFile" />
                                    <asp:PostBackTrigger ControlID="btnAddNew" />
                                    <asp:PostBackTrigger ControlID="chk1" />
                                    <asp:PostBackTrigger ControlID="chk2" />
                                    <asp:PostBackTrigger ControlID="chk3" />
                                    <asp:PostBackTrigger ControlID="chk4" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="drpTypeexpense" />
                                    <asp:PostBackTrigger ControlID="btnback" />
                                    <asp:PostBackTrigger ControlID="btnAddLOCNew" /> 
                                    <asp:PostBackTrigger ControlID="btnlocback" />
                                    <asp:PostBackTrigger ControlID="btnlocsave" />
                                    <asp:PostBackTrigger ControlID="TextBox5" />
                                    <asp:PostBackTrigger ControlID="btnlocClose" />
                                     <asp:PostBackTrigger ControlID="txtadv" />
                                     <asp:PostBackTrigger ControlID="gvLocalClaimList" />

                                </Triggers>
                            </asp:UpdatePanel>
                        </section>
                    </div>

                </div>
            </div>

        </section>
         <script>
            $(document).ready(function () {
                var date_input = $('input[name="txtDate"]'); //our date input has the name "date"
                var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
                var options = {
                    format: 'dd/mm/yyyy',
                    container: container,
                    todayHighlight: true,
                    autoclose: true,
                };
                date_input.datepicker(options);
            })
        </script>

    </section>


</asp:Content>
