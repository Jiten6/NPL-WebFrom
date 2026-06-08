<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="ApprovalPettyExpensex.aspx.cs" Inherits="NewPortal2023.ESS.ApprovalPettyExpensex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>
    <style type="text/css">
        .underline {
            text-decoration: underline;
        }
    </style>
    <script>
        $(function () {
            $("#gvList").DataTable({
                "responsive": true, "lengthChange": false, "autoWidth": false,
                "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
            }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            $('#example2').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false,
                "ordering": true,
                "info": true,
                "autoWidth": false,
                "responsive": true,
            });
        });
    </script>
    <script type="text/javascript">

        function ConNext() {
            return confirm("Are you saving the details, and do you want to go to the next section?");
        }

        function ConPrev() {
            return confirm("Are you saving the details, and do you want to go to the privious section?");
        }

    </script>


    <%--    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css">--%>

    <style type="text/css">
        .tableTitle {
            font-weight: bold;
            font-size: 10pt;
            vertical-align: middle;
            color: #205a94;
            font-family: Tahoma;
            height: 20px;
            text-decoration: none;
        }

        .tableTitleSubHeader {
            font-weight: bold;
            font-size: 9pt;
            vertical-align: middle;
            color: #205a94;
            font-family: Tahoma;
            height: 18px;
            text-decoration: none;
        }

        .table4 {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
            background-color: #FFFFFF;
        }

        .total {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 1px;
            padding-bottom: 1px;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            height: 10px;
            width: 10%;
            font-weight: bold;
            background-color: #efefef;
        }

            .total TD {
                font-weight: bold;
                height: 20px;
                background-color: #efefef;
            }

        .GridViewItem {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 1px;
            padding-bottom: 1px;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            height: 10px;
            width: 10%;
            font-weight: normal;
        }

        .GridViewHeader {
            font-weight: bold;
            font-size: 8.3;
            filter: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#FFFFFF,endColorStr=#BBDDFF);
            text-transform: capitalize;
            color: #545454;
            border-top: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-bottom: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            text-align: center;
        }

        .input {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 7.5pt;
            color: #004B97;
            border-top: auto inset #CCCCCC;
            border-right: auto inset #DFDFDF;
            border-left: auto inset #CCCCCC;
            border-bottom: auto inset #DFDFDF;
            border-top-color: #CCCCCC;
            border-right-color: #DFDFDF;
            border-bottom-color: #DFDFDF;
            border-left-color: #CCCCCC;
            border-top-style: inset;
            border-right-style: inset;
            border-bottom-style: inset;
            border-left-style: inset;
            border-style: inset;
            border-color: #999999 #CCCCCC #CCCCCC #999999;
            border-top-width: 1px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
        }

        .GridViewEmpty {
            padding: 0px;
            margin: 0;
            border: solid 1px #d3dbdf;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            border-top: 1px solid #d3dbdf;
            width: 100%;
            border-spacing: 0px;
        }

        .title {
            font-weight: bold;
            font-size: 11pt;
            vertical-align: middle;
            text-transform: capitalize;
            color: #205a94;
            font-family: Tahoma;
            /* height: 20px; */
            text-decoration: none;
        }

        label1 {
            font-size: 20px;
            font-weight: 500;
            color: darkcyan;
        }

        h4 {
            font-family: 'Arial', sans-serif;
            font-size: 1.5em;
            font-weight: 500;
            margin-bottom: 20px;
            color: darkcyan;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">MISCELLANEOUS EXPENSES</h3>
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
                                    <div class="adv-table">
                                        <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="card-body" style="margin: 20px">
                                            <div class="row">
                                                <div>
                                                    <asp:Button ID="btnAddNew" OnClick="btnAddNewVoucher_Click" Visible="false" runat="server" CssClass="btn btn-primary" Text="Add New Voucher" OnClientClick="showLoader();" />
                                                </div>
                                                <div id="DivNewDataEntry" visible="true" runat="server">
                                                    <div class="container mt-5">
                                                        <table width="100%" border="1" cellpadding="0" cssclass="table table-bordered" id="tblNewDataEntry" runat="server" visible="true">
                                                            <tr>
                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Expense type</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Date</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Particulars</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Bill no</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Amount</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Upload Document</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpExpensetype" runat="server" CssClass="form-control" AutoPostBack="false" OnSelectedIndexChanged="drpExpensetype_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Select one"></asp:ListItem>
                                                                        <asp:ListItem Text="Vehicle Expenses" Value="Vehicle Expenses"></asp:ListItem>
                                                                        <asp:ListItem Text="Company Expenses (e.g., Gas Bill)" Value="Company Expenses (e.g., Gas Bill)"></asp:ListItem>
                                                                        <asp:ListItem Text="Computer Spares" Value="Computer Spares"></asp:ListItem>
                                                                        <asp:ListItem Text="Staff Welfare" Value="Staff Welfare"></asp:ListItem>
                                                                        <asp:ListItem Text="New Joinee Expenses" Value="New Joinee Expenses"></asp:ListItem>
                                                                        <asp:ListItem Text="Statutory Expenses" Value="Statutory Expenses"></asp:ListItem>
                                                                        <asp:ListItem Text="Workmen Expenses" Value="Workmen Expenses"></asp:ListItem>
                                                                        <asp:ListItem Text="Stationery" Value="Stationery"></asp:ListItem>
                                                                        <asp:ListItem Text="Chairman/Misc. Expenses" Value="Chairman/Misc. Expenses"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDate" CssClass="form-control datepicker" runat="server" />
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtNatureofexpense" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBillno" runat="server" CssClass="form-control" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" />
                                                                </td>

                                                                <td>
                                                                    <asp:FileUpload ID="fupUpload" runat="server" Style="width: 150px; font-size: 11px;" EnableViewState="true" />
                                                                    <asp:LinkButton ID="lnkShowDoc" runat="server" Visible="false" OnClick="lnkShowDoc_Click">Download</asp:LinkButton>
                                                                    <asp:Label ID="lblDocAddr" runat="server" Text="" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" class="text-center">
                                                                    <asp:Button ID="btnPcktReimb" runat="server" Style="background-color: Green; color: White; padding: 6px;" Text="Add" OnClick="btnSave_Click" CssClass="btn btn-success mx-2" Visible="true" />
                                                                    <asp:Button ID="btnBackToYearList" runat="server" Style="background-color: Red; color: White;" Text="Back" CssClass="btn btn-danger mx-2" OnClick="btnBack_Click" Visible="true" />
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </div>
                                                </div>
                                                <div id="divList" visible="false" runat="server">
                                                    <br />
                                                    <br />
                                                    <label style="font-size: 30px;"><u>MISCELLANEOUS CLAIMS - APPROVAL PENDING</u></label>
                                                    <br />
                                                    <div style="overflow-x: scroll; width: 100%;">
                                                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Size="Medium" HeaderStyle-BackColor="Orange"
                                                            HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                                            GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAppNo" runat="Server" Text='<%# Eval("EXPENSES_NO") %>' />
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
                                                                <asp:TemplateField HeaderText="Voucher No" ControlStyle-BorderColor="Orange">
                                                                    <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkVoucherNo" Class="underline" OnClick="lnkVoucherno_Click" runat="Server" Text='<%# Eval("EXPENSES_NO") %>'
                                                                            OnClientClick="showLoader();" />
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Employee Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpNAme" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Employee ID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpMid" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Voucher Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVoucherDate" runat="Server" Text='<%# Eval("VOUCHER_DATE") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Voucher Amount">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" runat="Server" Text='<%# Eval("AMOUNT") %>' />
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Apporved Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApprovedAmount" runat="Server" Text='<%# Eval("Approved_Amount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Validation Team Remark">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtChkRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("CHKRMK") %>'></asp:TextBox>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="HOD Remark">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="tctHodRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("HODRMK") %>'></asp:TextBox>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="HR Remark">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtHrRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("HRRMK") %>'></asp:TextBox>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Finance Remark">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtFinRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("FINRMK") %>'></asp:TextBox>
                                                                    </ItemTemplate>


                                                                </asp:TemplateField>

                                                                   <asp:TemplateField HeaderText="Status" >
                                                                    <ItemStyle CssClass="GridViewItem" Width="50px"/>
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtMISCSTS" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("STATUS") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                            </Columns>
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
                                                    <div class="row">
                                                        <label class="col-sm-2" id="lblActionAll">Action :-</label>
                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="drActionAll" runat="server" CssClass="form-control" AutoPostBack="false">
                                                                <asp:ListItem Value="">--Select Action Type--</asp:ListItem>
                                                                <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>


                                                            </asp:DropDownList>
                                                        </div>
                                                        &nbsp;&nbsp;
                                     
                                                        <label class="col-sm-2" id="lblRemarksAll">Remarks :-</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtAllRmk" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1">
                                                            <asp:Button ID="btnSubmitAll" runat="server" CssClass="btn btn-success" OnClick="btnSubmitAll_Click" Text="Submit" OnClientClick="showLoader();" />
                                                        </div>

                                                    </div>

                                                </div>
                                                <div id="divallList" visible="false" runat="server">
                                                    <br />
                                                    <br />
                                                    <label style="font-size: 30px;"><u>Miscellaneous Claim Details</u></label>
                                                    <br />

                                                    <asp:GridView ID="gvallList" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Size="Medium" HeaderStyle-BackColor="SkyBlue"
                                                        HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvallList_RowDataBound"
                                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEntryAid" runat="Server" Text='<%# Eval("ENTRY_AID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Expense type">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExpenseType" runat="Server" Text='<%# Eval("EXPENSE_TYPE") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Voucher Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVoucherDate" runat="Server" Text='<%# Eval("DATE") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="On behalf of" Visible="true">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOnbehalfof" runat="Server" Text='<%# Eval("ON_BEHALF_OF") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Particulars">

                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lblNatureofexpense" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("NATURE_OF_EXPENSE") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bill no">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBillno" runat="Server" Text='<%# Eval("BILL_NUMBER") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Voucher Amount">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="Server" Text='<%# Eval("AMOUNT") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Supporting File">

                                                                <ItemTemplate>

                                                                    <asp:FileUpload ID="fupUpload" runat="server" Visible="false" />
                                                                    <asp:LinkButton ID="lnkShowDoc" runat="server" Visible="false" ReadOnly="true" OnClick="lnkShowDoc_Click1" Style="font-size: 14px; color: blue;" CssClass="underline">Download</asp:LinkButton>
                                                                    <asp:Label ID="lblDocAddr" runat="server" Text="" Visible="false"></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Action" Visible="false">
                                                                <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" Font-Bold="true" OnClick="lnkEdit_Click" Text="Update" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" Visible="false">
                                                                <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" Font-Bold="true" Text="Delete" OnClick="lnkDelete_Click" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                        </Columns>
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

                                                    <div class="row">
                                                        <div style="margin-left: 49%; font-size: 14PX; color: black; font-weight: 500;">
                                                            Total Amount :-
                                                    <%--<asp:Label ID="lblTotalAmt" runat="server" Style="border: solid; width: 50px;"></asp:Label>--%>
                                                            <asp:TextBox ID="txtTotalAmt" runat="server" Style="border: solid; margin-left: 25%; margin-top: -19%; width: 100px;" ReadOnly="true" CssClass="form-control input-sm"></asp:TextBox>
                                                        </div>


                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div style="margin-left: 49%; font-size: 14PX; color: black; font-weight: 500;">
                                                            Approved Amount :-
                                                   
                                                            <asp:TextBox ID="txtApprovedAmt" runat="server" Style="border: solid; margin-left: 25%; margin-top: -4%; width: 100px;" CssClass="form-control input-sm"></asp:TextBox>
                                                        </div>


                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12">

                                                            <div style="font-size: 14PX; color: black; font-weight: 500;">
                                                                Employee Remarks :-
                                                                    <asp:TextBox ID="txtEmpRmk" runat="server" ReadOnly="true" TextMode="MultiLine" Style="border: solid; width: 400px; margin-top: 5%;"></asp:TextBox>
                                                            </div>


                                                        </div>

                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row" id="divfileDisplayEnter" runat="server" visible="true">
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
                                                                OnClick="lnkBtnOpenFileEnter_Click" AutoPostback="true" />
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
                                                    <br />
                                                    <br />
                                                    <div class="form-group row">
                                                        <div class="col-12">

                                                            <div style="margin-left: 25%; font-size: 14PX; color: black; font-weight: 500;">
                                                                Remarks :-
                                                                    <asp:TextBox ID="txtRmk" runat="server" TextMode="MultiLine" Style="border: solid; width: 400px;"></asp:TextBox>
                                                            </div>


                                                        </div>

                                                    </div>

                                                    <div class="form-group row">
                                                        <div>
                                                            <asp:Button ID="SubmitBtn" runat="server" Style="margin-left: 31%;"
                                                                align="center" Text="Approve" OnClick="SubmitBtn_Click" CssClass="btn btn-success" Visible="true" />
                                                            <asp:Button ID="btnReject" runat="server" Style="margin-left: 6%;"
                                                                align="center" Text="Reject" OnClick="btnReject_Click" CssClass="btn btn-danger" Visible="true" />
                                                            <asp:Button ID="btnCancel" runat="server" Style="margin-left: 56%; margin-top: -4%;"
                                                                align="center" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" Visible="true" />
                                                        </div>
                                                        <div>
                                                        </div>
                                                        <div>
                                                        </div>







                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </asp:View>

                            </asp:MultiView>

                        </div>

                        <div style="text-align: center;">
                            <div class="modal fade" id="modal-xl">
                                <div class="modal-dialog modal-xl">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <asp:Label ID="lblTitle" runat="server" class="modal-title"></asp:Label>
                                            <%-- <h4 id="h4" class="modal-title">Extra Large Modal</h4>--%>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group row">
                                                <div class="col-12">
                                                    <div class="modal-footer justify-content-between">
                                                        <%-- <asp:Button ID="SubmitBtn" runat="server" Text="submit" CssClass="button" OnClick="SubmitBtn_OnClick" Visible="false" />--%>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                    <!-- /
                                </div>
                                <!-- /.modal-dialog -->
                                </div>
                            </div>
                            <script>
                                $(document).ready(function () {
                                    $('.datepicker').datepicker({
                                        dateFormat: 'mm/dd/yy' // Adjust the date format as per your requirement
                                    });
                                });
                            </script>
                        </div>

                    </section>
                </div>
            </div>

        </section>
    </section>

</asp:Content>
