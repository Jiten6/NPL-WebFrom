<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="ApprovalStaffWelfare.aspx.cs" Inherits="NewPortal2023.ESS.ApprovalStaffWelfare" %>

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
                                                    <div class="card-header" runat="server" id="divNote" visible="false">

                                                        <div runat="server" class="col-sm-12">
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
                                                    <div class="card-header" runat="server" id="divSqlNote" visible="false">
                                                        <div runat="server" class="col-sm-12">
                                                            <div class="col-sm-12 ">
                                                                <div class="col-sm-12 table table-bordered form-horizontal">

                                                                    <label style="color: red">Notes :- </label>
                                                                    <div>
                                                                        &emsp;&emsp;
                                                                <asp:Label runat="server" BackColor="DeepSkyBlue">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                                <asp:Label runat="server" Text="Above Eligibility Amount" Style="text-align: left;"></asp:Label>

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


                                                    <div class="card-body" id="Divdom" runat="server" visible="false">
                                                        <%--style="width: 1200px; overflow-x: scroll;"--%>

                                                        <label style="font-size: 30px;">List Of Domestic Staff Welfare Claim</label>
                                                        <%--<asp:GridView ID="gvDomClaim" runat="server" ToolTip="DOMESTIC CLAIM LIST" Style="width: 1150px;" OnRowDataBound="gvDomClaim_RowDataBound" AutoGenerateColumns="false" OnPreRender="gvDomClaim_PreRender" class="table table-bordered table-condensed">--%>
                                                        <asp:GridView ID="gvDomClaim" runat="server" AutoGenerateColumns="False"
                                                            HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvDomClaim_RowDataBound"
                                                            GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTEAT" runat="Server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="true">
                                                                    <ItemStyle VerticalAlign="Middle" />
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkSelectAll" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" runat="Server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Claim No">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDOMClmNoClmNo" CssClass="form-group" OnClick="lnkDOMClmNoClmNo_Click" Style="width: 100%;" TextMode="MultiLine" runat="Server" Text='<%# Eval("App_AId") %>'
                                                                            OnClientClick="showLoader();" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Employee AId" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpAId" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Emp_AId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Employee Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpCodes" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Employee Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpName" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_FNAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Claim Date" HeaderStyle-Width="74.2px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtClmDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Date") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Claim Type" HeaderStyle-Width="74.2px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTravelType" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Traveltype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Travel From Date" HeaderStyle-Width="96.2px" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txFrDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("FromDate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="Travel To Date" HeaderStyle-Width="96.2px">
                                            <ItemTemplate>
                                                <asp:Label ID="txToDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ToDate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Travelled From" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtFrDest" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("TravelledFrom") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Travelled To" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtToDest" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("TravelledTo") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Claim Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtClmAmt" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Total_Expense") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approved Amount" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtAppAmtr" TextMode="MultiLine" Visible="false" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ApprovedAmmount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EntertainmaentChecked" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="EntertainmentChked" TextMode="MultiLine" Visible="false" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EntertainmentChked") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="STATUS" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtClmType" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("STATUS") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action Type" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%--<asp:Label ID="lblSolID" runat="Server" Text='<%# Eval("SOL_ID") %>' SelectedValue='<%# Eval("ACTIONTYPE") %>'/>--%>
                                                                        <asp:DropDownList ID="drpAction" runat="server" CssClass="form-control input-sm-3" AutoPostBack="false" SelectedValue='<%# Eval("ChkAction") %>'>
                                                                            <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                            <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                            <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>

                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRmk" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("CheckerRemarks") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <%-- <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnView" Text="view" runat="server" OnClientClick="getStyle()" CssClass="btn btn-primary" />
                                            </ItemTemplate>

                                            <ItemStyle />
                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkBtnSubmit" Text="Submit" runat="server" OnClick="lnkBtnSubmit_Click" CssClass="btn btn-success" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
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

                                                        <%-- </div>
                        </div>--%>
                                                        <div>
                                                            <br />
                                                            <br />
                                                        </div>
                                                        &emsp;

                                <div class="row">
                                    <label id="lblActionAll">Action :-</label>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="drActionAll" runat="server" CssClass="form-control" AutoPostBack="false">
                                            <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                            <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                            <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>


                                        </asp:DropDownList>
                                    </div>
                                    &nbsp;&nbsp;
                                     
                                    <label id="lblRemarksAll">Remarks :-</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAllRmk" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-1">
                                        <asp:Button ID="btnSubmitAll" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmitAll_Click" OnClientClick="showLoader();" />
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
                                                            <h1>Domestic Expense Form</h1>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <ol class="breadcrumb float-sm-right">
                                                                <li class="breadcrumb-item"><a href="#">Home</a></li>

                                                                <li class="breadcrumb-item active">Domestic Expense Form</li>
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
                                                            <%--<label>Category I</label>--%>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label>Type</label>
                                                                        <asp:DropDownList ID="drpType" runat="server" Enabled="false" CssClass="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem Value="">[Select Type]</asp:ListItem>
                                                                            <asp:ListItem Value="Travel" Text="Travel"></asp:ListItem>
                                                                            <asp:ListItem Value="Entertainment" Text="Entertainment"></asp:ListItem>
                                                                            <asp:ListItem Value="Travel + Entertainment" Text="Travel + Entertainment"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <!-- /.form-group -->
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <br />
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="Label13" runat="server" class="col-sm-3 col-form-label"> Expense Description :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" ReadOnly="true" ID="txtdiscript" runat="server" Style="width: 700px; height: 50px;"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                    <!-- checkbox -->
                                                                    <%--<div class="form-group">
                                        <asp:Label ID="lblClassTravel" runat="server" Visible="false"><b>Class Of Travels</b></asp:Label>
                                        <br />
                                        <br />
                                        <div class="icheck-primary d-inline">
                                            <asp:CheckBox ID="chk1" runat="server" Visible="false" Text="Air (Business Class)" />
                                        </div>
                                        <div class="icheck-primary d-inline">
                                            <asp:CheckBox ID="chk2" runat="server" Visible="false" Text="Air (Economy Class)" />
                                        </div>

                                        <div class="icheck-primary d-inline">
                                            <asp:CheckBox ID="chk4" runat="server" Visible="false" Text="Rail (AC 1st Class)" />
                                        </div>
                                        <div class="icheck-primary d-inline">
                                            <asp:CheckBox ID="chk5" runat="server" Visible="false" Text="Rail (AC 2nd Class)" />
                                        </div>
                                        <div class="icheck-primary d-inline">
                                            <asp:CheckBox ID="chk6" runat="server" Visible="false" Text="Rail (AC 3rd Class)" />
                                        </div>
                                        <div class="icheck-primary d-inline">
                                            <asp:CheckBox ID="chk3" runat="server" Visible="false" Text="Rail (AC Chair Car)" />
                                        </div>
                                        <div class="icheck-primary d-inline">
                                            <asp:CheckBox ID="chk7" runat="server" Visible="false" Text="Bus" />
                                        </div>
                                    </div>--%>

                                                                    <div>
                                                                        <asp:Label ID="lblClassTravel" runat="server" Visible="false"><b>Class Of Travels</b></asp:Label>
                                                                    </div>
                                                                    <br />

                                                                    <div class="form-group row">

                                                                        <asp:CheckBox ID="chk1" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Air (Business Class)" />
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" Enabled="false" ID="txtchk1" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                        </div>

                                                                        &emsp;&emsp;
                                        <asp:CheckBox ID="chk2" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Air (Economy Class)" />
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" Enabled="false" ID="txtchk2" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <asp:CheckBox ID="chk3" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Rail (AC 1st Class)" />
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" Enabled="false" ID="txtchk3" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        &emsp;&emsp;
                                        <asp:CheckBox ID="chk4" Enabled="false" class="icheck-primary d-inline col-sm-2" runat="server" AutoPostBack="true" Text="Rail (AC 2nd Class)" />
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" Enabled="false" ID="txtchk4" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>

                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group row">



                                                                        <asp:CheckBox ID="chk5" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Rail (AC 3rd Class)" />
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" Enabled="false" ID="txtchk5" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        &emsp;&emsp;
                                        <asp:CheckBox ID="chk6" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Rail (AC Chair Car)" />
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" Enabled="false" ID="txtchk6" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <asp:CheckBox ID="chk7" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Bus" />
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" Enabled="false" ID="txtchk7" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        &emsp;&emsp;
                                        <asp:CheckBox ID="chk8" Enabled="false" class="icheck-primary d-inline col-sm-2" runat="server" AutoPostBack="true" Text="Others" />
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" Enabled="false" ID="txtchk8" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>

                                                                        </div>

                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <%--span2 datepicker-dropdown menu-open--%>
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label7" runat="server" class="col-sm-3 col-form-label">From Date :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtFromDate" runat="server"></asp:TextBox>
                                                                    <%--<div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                        </div>--%>
                                                                </div>
                                                                <asp:Label ID="Label9" runat="server" class="col-sm-3 col-form-label">To Date :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtToDate" runat="server"></asp:TextBox>
                                                                    <%--<div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                        </div>--%>
                                                                </div>



                                                            </div>

                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label4" runat="server" class="col-sm-3 col-form-label">Travel Source :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtStartDest" runat="server"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label6" runat="server" class="col-sm-3 col-form-label">Travel Destination :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtEndDest" runat="server"></asp:TextBox>
                                                                </div>

                                                                <br />

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
                                                            <h6 class="card-title"><b>Daily Reimbursement Amount in Rs. (Per day)</b></h6>
                                                            <br />
                                                            <!-- iCheck -->

                                                            <div class="card-body">
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <!-- radio -->
                                                                        <div class="form-group clearfix">
                                                                            <div class="icheck-primary d-inline">
                                                                                <asp:RadioButton ID="radioPrimary1" Enabled="false" Text="Metro Cities" runat="server" GroupName="GroupType"></asp:RadioButton>


                                                                                <label for="radioPrimary1">
                                                                                </label>
                                                                            </div>
                                                                            <div class="icheck-primary d-inline">
                                                                                <asp:RadioButton ID="radioPrimary2" Enabled="false" Text="Non Metro Cities" runat="server" GroupName="GroupType"></asp:RadioButton>

                                                                                <label for="radioPrimary2">
                                                                                </label>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
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

                                                                        <asp:GridView ID="grNonMetro" runat="server" ToolTip="Non Metro Cities Daily Reimbursement" AutoGenerateColumns="False" class="table table-bordered table-striped">
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
                                                                                <%--<table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width: 10%" class="Title">--%>
                                                                                <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                <%-- </td>
                                                    </tr>
                                                </table>--%>
                                                                            </EmptyDataTemplate>

                                                                        </asp:GridView>

                                                                    </div>
                                                                </div>
                                                                <div class="form-group row" id="divAmt" runat="server" visible="false">
                                                                    <asp:Label ID="Label2" runat="server" class="col-sm-3 col-form-label">No of Days :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" ID="txtNoDays" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label ID="Label3" runat="server" class="col-sm-3 col-form-label">Eligibility Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" ID="txtligibility" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <br />

                                                                </div>

                                                                <div class="form-group row" id="divEligi" runat="server" visible="false">


                                                                    <asp:Label ID="Label10" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" ID="txtTravelAmt" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label ID="Label1" runat="server" class="col-sm-3 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" TextMode="MultiLine" ID="txtUserTravelRemarks" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <br />

                                                                </div>
                                                                <div class="form-group row" id="div1" runat="server" visible="false">


                                                                    <asp:Label ID="Label5" runat="server" class="col-sm-3 col-form-label">Advance Paid :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" ID="txtadvance" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <%-- <asp:Label ID="Label18" runat="server" class="col-sm-3 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" TextMode="MultiLine" ID="TextBox2" runat="server"></asp:TextBox>
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
                                                                AutoPostback="true" OnClick="lnkBtnOpenFile_Click" />
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

                                                                <%--<div class="row" id="divFile" runat="server" visible="false">
                                    <div class="col-lg-6">
                                        Supporting files:
                                    </div>
                                </div>--%>
                                                            </div>
                                                            <%-- </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                            <section class="content" id="divEntertainment" runat="server" visible="false">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-header">
                                                            <h6 class="card-title"><b>Entertainment</b></h6>

                                                            <%--<div class="card-tools">
                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                             <button type="button" class="btn btn-tool" data-card-widget="remove">
                                <i class="fas fa-times"></i>
                            </button>
                        </div>--%>
                                                            <br />
                                                            <!-- iCheck -->
                                                            <%--<div class="card card-success">--%>
                                                            <div class="card-body">

                                                                <div class="form-group row">

                                                                    <asp:Label ID="Label11" runat="server" class="col-sm-3 col-form-label">Entertainment Description :-</asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtEnterDesc" Enabled="false" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">


                                                                    <asp:Label ID="Label8" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtEntAmount" Enabled="false" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <%--  <asp:Label ID="Label11" runat="server" class="col-sm-2 col-form-label">Entertainment Description :-</asp:Label>
                                    <div class="col-sm-2">
                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtEnterDesc" runat="server"></asp:TextBox>
                                    </div>--%>
                                                                    <asp:Label ID="Label12" runat="server" class="col-sm-2 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" Enabled="false" ID="txtUserEligiRemark" runat="server"></asp:TextBox>
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

                                                                <br />
                                                                <br />

                                                            </div>
                                                            <%-- </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>

                                            <section class="content">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <br />

                                                        <div class="form-group row" id="eligibility" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="Label14" runat="server" class="col-sm-4 col-form-label"><b>Eligibility Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="elgAmount" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row" id="claimAmt" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="Label15" runat="server" class="col-sm-4 col-form-label"><b>Claim Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="claimAmount" AutoCompleteType="Disabled" Enabled="false" AutoPostBack="true" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row" id="Divadv" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="Label16" runat="server" class="col-sm-4 col-form-label"><b>Advance Paid Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="txtadvamt" AutoCompleteType="Disabled" Enabled="false" AutoPostBack="true" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row" id="TotalExpns" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="lblTtlexp" runat="server" class="col-sm-4 col-form-label"><b>Total Expense (Travel fare + Claim Amount - Advance Amount) :-</b></asp:Label>
                                                            <%-- <asp:Button ID="btnCalTtl" CssClass="btn btn-primary" OnClick="btnCalTtl_Click" runat="server" Text="Calculate Total Expense" />--%>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox class="form-control" ID="txtTotalexp" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            &emsp;&emsp;<asp:Label ID="Label20" runat="server" class="col-sm-4 col-form-label"><b>Approved Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="txtApprovedAmt" runat="server" AutoCompleteType="Disabled" placeholder="Enter Amount"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtApprovedAmt"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>
                                                        </div>

                                                        <div class="card-body">
                                                            <div class="form-group row">
                                                                <%--<div class="col-sm-2"> Entertainment
                                   <asp:CheckBox ID="chkAlchol" style="font-size:14px;" runat="server" />
                                </div>--%>
                                                                <div class="form-group clearfix">
                                                                    <div class="icheck-primary d-inline">
                                                                        <%--<asp:RadioButton ID="radiaoEntertainment1" Text="Entertainment" OnCheckedChanged="radiaoEntertainment_CheckedChanged" AutoPostBack="true" runat="server"></asp:RadioButton>--%>
                                                                        <asp:CheckBox ID="radiaoEntertainment1" runat="server" Text="Non-Eligible" />


                                                                        <label for="RadioButton1">
                                                                        </label>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="drpActionType" runat="server" CssClass="form-control" AutoPostBack="false">
                                                                        <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                        <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                        <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <asp:Label ID="lblRmk" runat="server" class="col-sm-1 col-form-label">Remarks :-</asp:Label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox ID="txtRmk" runat="server" TextMode="MultiLine" CssClass="form-control col-sm-6"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>


                                                        <div class="card-body">
                                                            <div class="col-sm-12" style="text-align: center;">
                                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" OnClick="btnClose_Click" runat="server" Text="Cancel" OnClientClick="showLoader();" />
                                                                <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Submit" CssClass="btn btn-success" OnClientClick="showLoader();" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </div>

                                        <div id="Divlocal" runat="server" visible="false">
                                            <div id="div3" class="alert alert-block alert-success fade in" runat="server" visible="true">
                                                <asp:Label ID="Label17" runat="server"></asp:Label>
                                            </div>
                                            <section class="content-header" runat="server" id="Section1">

                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">

                                                        <div class="card-body">

                                                            <div class="card-header" runat="server" id="div4">

                                                                <div id="div5" runat="server" class="col-sm-12">

                                                                    <div class="col-sm-12 ">
                                                                        <div class="col-sm-12 table table-bordered">

                                                                            <label style="color: red">Notes :- </label>
                                                                            &emsp;&emsp;&emsp;
                                <asp:Label runat="server" BackColor="Orange">&emsp;&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                <asp:Label runat="server" Text="Beverages" Style="text-align: left;"></asp:Label>
                                                                            &emsp;&emsp;&emsp;&emsp;
                                <asp:Label runat="server" BackColor="LightGreen">&emsp;&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                <asp:Label runat="server" Text="Non-Beverages" Style="text-align: left;"></asp:Label>

                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Visible="false" runat="server" CssClass="btn btn-primary" Text="Add New" />
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <label style="font-size: 30px;">List Of Local Claim</label>

                                                            <br />

                                                            <div style="overflow-x: scroll; width: 100%">

                                                                <%--<asp:GridView ID="gvLocalClaimList" runat="server" ToolTip="LOCAL LIST CLAIM" AutoGenerateColumns="false" OnRowDataBound="gvLocalClaimList_RowDataBound" OnPreRender="gvLocalClaimList_PreRender" class="table table-bordered table-striped">--%>
                                                                <asp:GridView ID="gvLocalClaimList" runat="server" AutoGenerateColumns="False"
                                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvLocalClaimList_RowDataBound"
                                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC" OnPreRender="gvLocalClaimList_PreRender"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCODataId" runat="Server" Text="" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ShowHeader="true">
                                                                            <ItemStyle VerticalAlign="Middle" />
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkSelectAll" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" runat="Server" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Sr.No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSrNo" runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim No">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkLOCClmNoClmNo" CssClass="form-group" OnClick="lnkLOCClmNoClmNo_Click" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_no") %>'
                                                                                    OnClientClick="showLoader();" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee AId" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmpAId" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Emp_AId") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmpCode" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmpName" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_FNAME") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Claim Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Date","{0:dd/MM/yyyy}") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Expenses Date" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txFrDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Expenses_Date","{0:dd/MM/yyyy}") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Claim Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtLOCClmAmt" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Amount") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Claim Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmAmt" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Amount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtLOCApprAmt" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ClaimApproved_Amount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Name of Business/Assignment">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtFrDest" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Name_Bussi_Ass") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Travelled Description">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtToDest" TextMode="MultiLine" ReadOnly="true" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Travel_Description") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="STATUS">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtLOCSTS" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("STATUS") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approved Amount" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtAppAmtr" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ClaimApproved_Amount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="EntertainmaentChecked" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="EntertainmentChked" TextMode="MultiLine" Visible="false" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EntertainmentChked") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="STATUS" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmType" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ClmType") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Action Type" Visible="false">
                                                                            <ItemTemplate>
                                                                                <%--<asp:Label ID="lblSolID" runat="Server" Text='<%# Eval("SOL_ID") %>' SelectedValue='<%# Eval("ACTIONTYPE") %>'/>--%>
                                                                                <asp:DropDownList ID="drpAction" runat="server" CssClass="form-control input-sm-3" AutoPostBack="false" SelectedValue='<%# Eval("ChkAction") %>'>
                                                                                    <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                                    <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                                    <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                                                    <%--  <asp:ListItem Value="Revert" Text="Revert"></asp:ListItem>--%>
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRmk" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("CheckerRemarks") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkBtnlocSubmit" Text="Submit" runat="server" OnClick="lnkBtnlocSubmit_Click" CssClass="btn btn-success" />
                                                                            </ItemTemplate>

                                                                            <ItemStyle HorizontalAlign="Center" />
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
                                                                <br />
                                                                <br />

                                                                <div id="actionAll" runat="server" class="row" style="padding: 10px">
                                                                    <label id="lblActionAll">Action :-</label>
                                                                    <div class="col-sm-2">
                                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="false">
                                                                            <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                            <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                            <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>


                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    &nbsp;&nbsp;
                                     
                                                <label id="lblRemarksAll">Remarks :-</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                    </div>



                                                                    <div class="col-sm-1">
                                                                        <asp:Button ID="btnlocSubmitAll" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnlocSubmitAll_Click" OnClientClick="showLoader();" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                            </section>

                                            <div id="divlocalform" runat="server" visible="false">
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
                                                                <div class="form-group row">

                                                                    <br />
                                                                    <br />

                                                                    <asp:CheckBox ID="CheckBox1" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Self/Company Car" />
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" ID="TextBox2" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    &emsp;&emsp;
                                                                      <asp:CheckBox ID="CheckBox2" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged" Text="Train" />
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" ID="TextBox3" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <div class="form-group row">
                                                                    <asp:CheckBox ID="CheckBox3" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox3_CheckedChanged" Text="Taxi/Auto" />
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" ID="TextBox4" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    &emsp;&emsp;
                                                                       <asp:CheckBox ID="CheckBox4" Enabled="false" class="icheck-primary d-inline col-sm-2" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox4_CheckedChanged" Text="Bus" />
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" ID="TextBox5" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>

                                                                    </div>

                                                                </div>

                                                                <%--span2 datepicker-dropdown menu-open--%>
                                                                <br />
                                                                <div class="form-group row">
                                                                    <asp:Label ID="lblDate" runat="server" class="col-sm-3 col-form-label">Date of Expenses :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" ID="txtDate" runat="server"></asp:TextBox>
                                                                        <%--<div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                        </div>--%>
                                                                    </div>
                                                                    &emsp;&emsp;
                                <asp:Label ID="lblNameAss" runat="server" class="col-sm-3 col-form-label">Name of Business / Assignment :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox CssClass="form-control" Enabled="false" ID="txtNameAss" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <br />
                                                                <div class="form-group row">
                                                                    <asp:Label ID="lblCashVocher" runat="server" class="col-sm-3 col-form-label">Cash Voucher, if any, attached No.:-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" ID="txtCashVocher" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    &emsp;&emsp;
                                <asp:Label ID="lblTravelDes" runat="server" class="col-sm-3 col-form-label">Travel Description :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" TextMode="MultiLine" ID="txtTravelDes" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="form-group row">
                                                                    <asp:Label ID="lblMeal" runat="server" class="col-sm-3 col-form-label">Meal :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" ID="txtMeal" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    &emsp;&emsp;
                                <asp:Label ID="lblOtherExpenses" runat="server" class="col-sm-3 col-form-label">Other Expenses :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" ID="txtOtherExpenses" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="form-group row">
                                                                    <asp:Label ID="lbladv" runat="server" class="col-sm-3 col-form-label">Advance Paid :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" Enabled="false" ID="txtadv" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </section>
                                                <section class="content" id="Section3" runat="server" visible="false">
                                                    <div class="container-fluid">
                                                        <!-- SELECT2 EXAMPLE -->
                                                        <div class="card card-default">
                                                            <div class="card-header">
                                                                <h2 class="card-title" style="font-size: 18px"><b>Upload Supporting Documents</b></h2>
                                                                <br />
                                                                <!-- iCheck -->

                                                                <div class="card-body">
                                                                    <div class="row" id="div7" runat="server" visible="false">
                                                                        <div class="col-lg-6" id="div8" runat="server" visible="false">
                                                                            <div class="btn-group w-100">
                                                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                                <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-6" id="div9" runat="server" visible="false">
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
                                                                AutoPostback="true" OnClick="lnkBtnOpenFiles_Click" />
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
                                                            <br />
                                                            <div class="form-group row">
                                                                &emsp;<asp:Label ID="Label18" runat="server" class="col-sm-3 col-form-label"><b>Total Expense :-</b></asp:Label>
                                                                <asp:Button ID="btnCalTtl" Visible="false" CssClass="btn btn-primary" runat="server" Text="Calculate Total Expense" />
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox class="form-control" ID="txtlocaltotal" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                             <div class="form-group row">
                                                            &emsp;<asp:Label ID="lblpaidamt" runat="server" class="col-sm-3 col-form-label"><b>Paid Amount :-</b></asp:Label>
                                                            <asp:Button ID="Button1" Visible="false" CssClass="btn btn-primary" runat="server" Text="Calculate Total Expense" />
                                                            <div class="col-sm-4">
                                                                <asp:TextBox class="form-control" ID="txtpaidamt" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                            <div class="form-group row">
                                                                &emsp;
                                            <asp:Label ID="Label19" runat="server" class="col-sm-3 col-form-label"><b>Approved Amount :-</b></asp:Label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox class="form-control" ID="txtlocapprovedAmt" runat="server"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtlocapprovedAmt"
                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                        Display="Dynamic" ForeColor="Red" />
                                                                </div>

                                                                <br />

                                                            </div>

                                                            <div class="card-body">
                                                                <div class="form-group row">
                                                                    <div class="form-group clearfix">
                                                                        <div class="icheck-primary d-inline">
                                                                            <%--<asp:RadioButton ID="radiaoEntertainment1" Text="Entertainment" OnCheckedChanged="radiaoEntertainment_CheckedChanged" AutoPostBack="true" runat="server"></asp:RadioButton>--%>
                                                                            <asp:CheckBox ID="radiaoEntertainmentloc1" runat="server" Text="Non-Eligible" />


                                                                            <label for="RadioButton1">
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <asp:DropDownList ID="drpactiontypeloc" runat="server" CssClass="form-control" AutoPostBack="false">
                                                                            <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                            <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                            <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <asp:Label ID="Label21" runat="server" class="col-sm-2 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="txtrmkloc" runat="server" TextMode="MultiLine" CssClass="form-control col-sm-6"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>


                                                            <div class="card-body">
                                                                <div class="col-sm-12" style="text-align: center;">
                                                                    <asp:Button ID="btnLocClose" CssClass="btn btn-danger" OnClick="btnLocClose_Click" runat="server" Text="Close" OnClientClick="showLoader();" />
                                                                    <asp:Button ID="btnLocApprove" runat="server" OnClick="btnLocApprove_Click" Text="Submit" CssClass="btn btn-success" OnClientClick="showLoader();" />
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
                                    <asp:PostBackTrigger ControlID="btnApprove" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="btnSubmitAll" />
                                    <asp:PostBackTrigger ControlID="gvDomClaim" />
                                    <asp:PostBackTrigger ControlID="gvEntertainment" />
                                    <asp:PostBackTrigger ControlID="gvDomesticFile" />
                                    <asp:PostBackTrigger ControlID="drpTypeexpense" />
                                    <asp:PostBackTrigger ControlID="btnLocClose" />
                                    <asp:PostBackTrigger ControlID="btnLocApprove" />
                                    <asp:PostBackTrigger ControlID="btnlocSubmitAll" />
                                    <asp:PostBackTrigger ControlID="gvLocalClaimList" />
                                    <%--<asp:PostBackTrigger ControlID="chk1" />
                                    <asp:PostBackTrigger ControlID="chk2" />
                                    <asp:PostBackTrigger ControlID="lnkLOCClmNoClmNo" />
                                   
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
                                    <asp:PostBackTrigger ControlID="gvLocalClaimList" />--%>
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
        <script>
            var handleDataTableButtons = function () {
                "use strict";
                0 !== $('#<%= gvLocalClaimList.ClientID %>').length &&
                    $('#<%= gvLocalClaimList.ClientID %>').DataTable({
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
                $('#<%= gvLocalClaimList.ClientID %>').dataTable();
            });
            TableManageButtons.init();
        </script>
    </section>

</asp:Content>
