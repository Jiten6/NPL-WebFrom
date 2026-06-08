<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="PettyExpensex.aspx.cs" Inherits="NewPortal2023.ESS.PettyExpensex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>
    <style type="text/css">
        .underline {
            text-decoration: underline;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function ValidFile() {
            var file = $("#fupldDocument").val();
            if (file == "") {
                IN4_DisplayErrorMessage("Browse document.");
                return false;
            }
        }


    </script>


    <script type="text/javascript">
        $(function () {

            var currentDate = new Date();

            $('[id*=txtDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-mm-yyyy",
                language: "tr",
                autoclose: true,
                endDate: currentDate // Disable dates after the current date
            });
        });

    </script>

    <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/litepicker.js" type="text/javascript"></script>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />

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
                                                    <div class="container mt-6" style="overflow-x: scroll; width: 100%;">
                                                        <table width="100%" border="1" cellpadding="0" cssclass="table table-bordered" id="tblNewDataEntry" runat="server" visible="true">
                                                            <tr>
                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Expense type</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Date</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Particulars</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">On Behalf of</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Bill no</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Amount</th>

                                                                <th style="padding-bottom: 10px; text-align: center; width: 250px; background-color: ORANGE; font-size: medium">Upload Document</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpExpensetype" runat="server" CssClass="form-control" AutoPostBack="false" OnSelectedIndexChanged="drpExpensetype_SelectedIndexChanged">
                                                                        <%--  <asp:ListItem Text="Select one" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Chairman/Misc. Expenses" Value="Chairman/Misc. Expenses"></asp:ListItem>
                                                                        <asp:ListItem Text="Company Expenses (e.g., Gas Bill)" Value="Company Expenses (e.g., Gas Bill)"></asp:ListItem>
                                                                        <asp:ListItem Text="Computer Spares" Value="Computer Spares"></asp:ListItem>
                                                                        <asp:ListItem Text="New Joinee Expenses" Value="New Joinee Expenses"></asp:ListItem>
                                                                        <asp:ListItem Text="Staff Welfare" Value="Staff Welfare"></asp:ListItem>
                                                                        <asp:ListItem Text="Statutory Expenses" Value="Statutory Expenses"></asp:ListItem>
                                                                        <asp:ListItem Text="Stationery" Value="Stationery"></asp:ListItem>
                                                                        <asp:ListItem Text="Vehicle Expenses" Value="Vehicle Expenses"></asp:ListItem>
                                                                        <asp:ListItem Text="Workmen Expenses" Value="Workmen Expenses"></asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDate" AutoCompleteType="Disabled" CssClass="form-control input-sm datepicker" placeholder="Date" runat="server" />
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtNatureofexpense" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOnbehalfof" runat="server" CssClass="form-control" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBillno" runat="server" CssClass="form-control" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" />
                                                                    <asp:RegularExpressionValidator ID="revAmount" runat="server"
                                                                        ControlToValidate="txtAmount"
                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                        ErrorMessage="Only numbers are allowed!"
                                                                        ForeColor="Red" Display="Dynamic" />
                                                                </td>

                                                                <td>
                                                                    <asp:FileUpload ID="fupUpload" runat="server" Style="width: 150px; font-size: 11px;" EnableViewState="true" />
                                                                    <asp:LinkButton ID="lnkShowDoc" runat="server" Visible="false" OnClick="lnkShowDoc_Click">Download</asp:LinkButton>
                                                                    <asp:Label ID="lblDocAddr" runat="server" Text="" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" class="text-center">
                                                                    <asp:Button ID="btnPcktReimb" runat="server" Style="" Text="Add Row" OnClick="btnSave_Click" CssClass="btn btn-info" Visible="true" />
                                                                    <asp:Button ID="btnBackToYearList" runat="server" Style="" Text="Back   " CssClass="btn btn-danger" OnClick="btnBack_Click" Visible="true" />
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </div>
                                                </div>
                                                <div id="divList" visible="false" runat="server" style="overflow-x: scroll; width: 100%;">
                                                    <br />
                                                    <br />
                                                    <label style="font-size: 30px;"><u>LIST OF MISCELLANEOUS CLAIMS</u></label>
                                                    <br />
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
                                                            <asp:TemplateField HeaderText="Voucher No" ControlStyle-BorderColor="Orange">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkVoucherNo" OnClick="lnkVoucherno_Click" Class="underline" runat="Server" Text='<%# Eval("EXPENSES_NO") %>'
                                                                        OnClientClick="showLoader();" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Voucher Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVoucherDate" runat="Server" Text='<%# Eval("VOUCHER_DATE") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Voucher Amount">
                                                                <ItemStyle CssClass="GridViewItem" />
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
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtChkRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("CHKRMK") %>'></asp:TextBox>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HOD Remark" Visible="false">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="tctHodRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("HODRMK") %>'></asp:TextBox>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HR Remark" Visible="false">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtHrRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("HRRMK") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Finance Remark">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtFinRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("FINRMK") %>'></asp:TextBox>
                                                                </ItemTemplate>


                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("STATUS") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatusId" runat="Server" Text='<%# Eval("STATUSID") %>' />
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
                                                <div id="divallList" visible="false" runat="server" style="overflow-x: scroll; width: 100%;">
                                                    <br />
                                                    <br />
                                                    <label style="font-size: 30px;"><u>List Of Expenses</u></label>
                                                    <br />
                                                    <asp:GridView ID="gvallList" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Size="Medium" HeaderStyle-BackColor="SkyBlue"
                                                        HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvallList_RowDataBound"
                                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEntryAid" runat="Server" Text='<%# Eval("ENTRY_AID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Expense type">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExpenseType" runat="Server" Text='<%# Eval("EXPENSE_TYPE") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Voucher Date">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVoucherDate" runat="Server" Text='<%# Eval("DATE") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="On behalf of">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOnbehalfof" runat="Server" Text='<%# Eval("ON_BEHALF_OF") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Particulars">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lblNatureofexpense" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("NATURE_OF_EXPENSE") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bill no">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBillno" runat="Server" Text='<%# Eval("BILL_NUMBER") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Voucher Amount">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="Server" Text='<%# Eval("AMOUNT") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Supporting File">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>

                                                                    <asp:FileUpload ID="fupUpload" runat="server" Visible="false" />
                                                                    <asp:LinkButton ID="lnkShowDoc" runat="server" Visible="true" OnClick="lnkShowDoc_Click1" Style="font-size: 14px; color: blue;" CssClass="underline" Text=""></asp:LinkButton>
                                                                    <asp:Label ID="lblDocAddr" runat="server" Text="" Visible="false"></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" Font-Bold="true" OnClick="lnkEdit_Click" Text="Update" CssClass="btn btn-warning" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemStyle CssClass="GridViewItem" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" Font-Bold="true" Text="Delete" OnClick="lnkDelete_Click" CssClass="btn btn-danger" />
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
                                                        <div style="margin-left: 39%; font-size: 14PX; font-weight: 500;">
                                                            Total Amount :-
                                                    <asp:TextBox ID="txtTotalAmt" runat="server" Style="border: solid; margin-left: 20%; margin-top: -19%; width: 100px;" ReadOnly="true" CssClass="form-control input-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />


                                                    <div class="row" id="divUploadEnter" runat="server">
                                                        <div class="btn-group w-100">
                                                            <asp:FileUpload ID="fupldDocumentEnter" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />
                                                            <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="col-lg-6" id="divfileDisplayEnter" runat="server">
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
                                                    <div class="form-group row">
                                                        <div class="col-12">
                                                            <div style="margin-left: 25%; font-size: 14PX; color: black; font-weight: 500;">
                                                                Remarks :-
                                                               <asp:TextBox ID="txtRmk" runat="server" TextMode="MultiLine" Style="border: solid; width: 400px;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-12">
                                                            <asp:Button ID="SubmitBtn" runat="server" Style="margin-left: 49%;"
                                                                align="center" Text="Submit" OnClick="SubmitBtn_Click" CssClass="btn btn-success" Visible="false" />

                                                            <asp:Button ID="btnBackList" runat="server" Style="margin-left: 49%;" Text="Close" OnClick="btnBackList_Click" CssClass="btn btn-danger" Visible="true"
                                                                Height="30px" />
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
                            <%--    <script>
                                $(document).ready(function () {
                                    $('.datepicker').datepicker({
                                        dateFormat: 'dd/mm/yy', maxDate: new Date
                                        // Adjust the date format as per your requirement

                                    });
                                });
                            </script>--%>


                            <%--<script type="text/javascript">

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerTo);





            function dtpickerTo(sender, args) {
                if (document.getElementById('<%= txtDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtDate.ClientID %>'),
                        format: 'DD-MM-YYYY',
                   startDate: false,
                maxDate: 0,
                step: 1,
                closeOnDateSelect: false                    });
                }
            }


            if (document.getElementById('<%= txtDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtDate.ClientID %>'),
                    format: 'DD-MM-YYYY',
                    startDate: false,
                maxDate: 0,
                step: 1,
                closeOnDateSelect: false
                });
            }


        </script>--%>
                        </div>

                    </section>
                </div>

            </div>

        </section>
    </section>
</asp:Content>
