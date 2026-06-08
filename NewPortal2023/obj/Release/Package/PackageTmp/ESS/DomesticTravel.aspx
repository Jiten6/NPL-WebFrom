<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="DomesticTravel.aspx.cs" Inherits="NewPortal2023.ESS.DomesticTravel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function () {

            var currentDate = new Date();

            $('[id*=txtFromDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",
                autoclose: true,
                endDate: currentDate // Disable dates after the current date
            });
        });
        $(function () {
            $('[id*=txtToDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",

                //autoclose: true,
                //endDate: currentDate // Disable dates after the current date
            });
        });
    </script>
    <script>
        function showAlert() {
            var textboxValue = document.getElementById('txtTravelAmt').value;
            alert("Text changed to: " + textboxValue);
        }
    </script>
    <script>
        function validateDates() {
            var fromDate = document.getElementById('<%= txtFromDate.ClientID %>').value;
            var toDate = document.getElementById('<%= txtToDate.ClientID %>').value;

            if (new Date(fromDate) > new Date(toDate)) {
                alert("From Date cannot be less than To Date.");
                // Clear the TO Date field
                document.getElementById('<%= txtToDate.ClientID %>').value = '';
            }

           <%-- else {
                var noDaysTextBox = document.getElementById('<%= txtNoDays.ClientID %>');

                if (fromDate && toDate && fromDate <= toDate) {
                    var timeDiff = Math.abs(toDate.getTime() - fromDate.getTime());
                    var totalDays = Math.ceil(timeDiff / (1000 * 3600 * 24)); // Convert milliseconds to days
                    noDaysTextBox.value = totalDays;
                } else {
                    noDaysTextBox.value = '';
                }
            }--%>
        }
    </script>
    <%--<script type="text/javascript">
        function saveFileBeforePostBack() {
            // Get the file input
            var fileInput = document.getElementById('<%= fupldDocument.ClientID %>');

        // Check if a file is selected
        if (fileInput.files.length > 0) {
            // Store the file data in the hidden field
            var hiddenField = document.getElementById('<%= hiddenField.ClientID %>');
                hiddenField.value = fileInput.files[0].name;
            }
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Domestic Travel</h3>
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
                                                                      <%--  <asp:Label runat="server" BackColor="Orange">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                    <asp:Label runat="server" Text="Not As Per Policy" Style="text-align: left;"></asp:Label>
                                                                        &emsp;&emsp;&emsp;&emsp;&emsp;
                                                      <asp:Label runat="server" BackColor="Yellow">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                    <asp:Label runat="server" Text="Non-Eligible" Style="text-align: left;"></asp:Label>--%>
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

                                                    <div class="card-body">

                                                        <div class="row">
                                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="showLoader();" />
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <label style="font-size: 30px;">List Of Domestic Claim</label>
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
                                                                        <asp:TemplateField HeaderText="Claim Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmAmt" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("TotalAmt") %>' />
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
                                                            <h1>Domestic Expense Form</h1>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <ol class="breadcrumb float-sm-right">
                                                                <li class="breadcrumb-item"><a href="#">Expense Reimbursement</a></li>

                                                                <li class="breadcrumb-item active">Domestic Expense Form</li>
                                                            </ol>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- /.container-fluid -->
                                            </section>

                                            <section class="content" id="traveltype" runat="server">
                                                <div class="container-fluid">
                                                    <div class="card card-default">
                                                        <div class="card-body">
                                                            <%--<label>Category I</label>--%>
                                                            <div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label>Type</label>

                                                                        <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpType_SelectedIndexChanged"
                                                                            AutoPostBack="true" onchange="showLoader();">
                                                                            <asp:ListItem Value="">[Select Type]</asp:ListItem>
                                                                            <asp:ListItem Value="Travel" Text="Travel"></asp:ListItem>
                                                                            <asp:ListItem Value="Entertainment" Text="Entertainment"></asp:ListItem>
                                                                            <asp:ListItem Value="Travel + Entertainment" Text="Travel + Entertainment"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <!-- /.form-group -->
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>

                                            <section class="content" id="travelClass" runat="server">
                                                <div class="form-group" runat="server" id="Class_Travel">
                                                    <div class="container-fluid">
                                                        <div class="card card-default">
                                                            <div class="card-body">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <br />
                                                                        <div class="form-group row">
                                                                            <asp:Label ID="Label13" runat="server" class="col-sm-3 col-form-label"> Expense Description :-</asp:Label>
                                                                            <div class="col-sm-2">
                                                                                <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtdescription" runat="server" Style="width: 700px; height: 50px;"></asp:TextBox>

                                                                            </div>
                                                                        </div>

                                                                        <!-- checkbox -->
                                                                        <div class="form-group">
                                                                            <asp:Label ID="lblClassTravel" runat="server" Visible="false"><b>Class Of Travels</b></asp:Label>
                                                                            <br />
                                                                            <br />
                                                                            <div class="form-group row">
                                                                                <div id="divChk1" runat="server" class="icheck-primary d-inline col-sm-3">
                                                                                    <asp:CheckBox ID="chk1" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" Text="Air (Business Class)" />
                                                                                </div>
                                                                                <div id="divTxtChk1" runat="server" class="col-sm-3">
                                                                                    <asp:TextBox class="form-control" ID="txtchk1" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtchk1"
                                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                                        Display="Dynamic" ForeColor="Red" />
                                                                                </div>

                                                                                <div class="icheck-primary d-inline col-sm-3">
                                                                                    <asp:CheckBox ID="chk2" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk2_CheckedChanged" Text="Air (Economy Class)" />
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
                                                                                    <asp:CheckBox ID="chk3" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk3_CheckedChanged" Text="Rail (AC 1st Class)" />
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:TextBox class="form-control" ID="txtchk3" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk3_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtchk3"
                                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                                        Display="Dynamic" ForeColor="Red" />
                                                                                </div>

                                                                                <div class="icheck-primary d-inline col-sm-3">
                                                                                    <asp:CheckBox ID="chk4" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk4_CheckedChanged" Text="Rail (AC 2nd Class)" />
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
                                                                                    <asp:CheckBox ID="chk5" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk5_CheckedChanged" Text="Rail (AC 3rd Class)" />
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:TextBox class="form-control" ID="txtchk5" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk5_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtchk5"
                                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                                        Display="Dynamic" ForeColor="Red" />
                                                                                </div>

                                                                                <div class="icheck-primary d-inline col-sm-3">
                                                                                    <asp:CheckBox ID="chk6" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk6_CheckedChanged" Text="Rail (AC Chair Car)" />
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
                                                                                    <asp:CheckBox ID="chk7" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk7_CheckedChanged" Text="Bus" />
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:TextBox class="form-control" ID="txtchk7" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk7_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtchk7"
                                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                                        Display="Dynamic" ForeColor="Red" />
                                                                                </div>
                                                                                <div class="icheck-primary d-inline col-sm-3">
                                                                                    <asp:CheckBox ID="chk8" runat="server" class="icheck-primary d-inline" Visible="false" AutoPostBack="true" OnCheckedChanged="chk8_CheckedChanged" Text="Other" />
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:TextBox class="form-control" ID="txtchk8" AutoCompleteType="Disabled" placeholder="Amount" Visible="false" runat="server" OnTextChanged="txtchk8_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtchk8"
                                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                                        Display="Dynamic" ForeColor="Red" />
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <br />
                                                                <%--span2 datepicker-dropdown menu-open--%>
                                                                <div class="form-group row">
                                                                    <asp:Label ID="Label7" runat="server" class="col-sm-3 col-form-label">From Date :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control datepicker" ID="txtFromDate" runat="server" AutoCompleteType="Disabled" OnTextChanged="txtFromDate_TextChanged"  onchange="validateDates()"></asp:TextBox>

                                                                    </div>
                                                                    <asp:Label ID="Label9" runat="server" class="col-sm-3 col-form-label">To Date :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox CssClass="form-control datepicker" ID="txtToDate" runat="server" AutoCompleteType="Disabled" OnTextChanged="txtToDate_TextChanged" onchange="validateDates()"></asp:TextBox>

                                                                    </div>
                                                                </div>

                                                                <br />
                                                                <div class="form-group row">
                                                                    <asp:Label ID="Label4" runat="server" AutoCompleteType="Disabled" class="col-sm-3 col-form-label">Travel Source :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtStartDest" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label ID="Label6" runat="server" class="col-sm-3 col-form-label">Travel Destination :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" AutoCompleteType="Disabled" ID="txtEndDest" runat="server"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                                 <br />
                                                                <div class="form-group row">
                                                                    <asp:Label ID="lbladvid" runat="server" AutoCompleteType="Disabled" class="col-sm-3 col-form-label">Advance ID :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtadvid" runat="server"></asp:TextBox>
                                                                    </div>
                                                                   
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
                                                                                <asp:RadioButton ID="radioPrimary1" Text="Metro Cities" runat="server" OnCheckedChanged="radioPrimary1_CheckedChanged" AutoPostBack="true" GroupName="GroupType"></asp:RadioButton>


                                                                                <label for="radioPrimary1">
                                                                                </label>
                                                                            </div>
                                                                            <div class="icheck-primary d-inline">
                                                                                <asp:RadioButton ID="radioPrimary2" Text="Non Metro Cities" runat="server" OnCheckedChanged="radioPrimary1_CheckedChanged" AutoPostBack="true" GroupName="GroupType"></asp:RadioButton>

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
                                                                        <asp:TextBox class="form-control" ID="txtNoDays" Enabled="false" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    &emsp;&emsp;
                                                    <asp:Label ID="Label3" runat="server" class="col-sm-3 col-form-label">Eligibility Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtligibility" Enabled="false" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <br />

                                                                </div>

                                                                <div class="form-group row" id="divEligi" runat="server" visible="false">
                                                                    <asp:Label ID="Label10" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtTravelAmt" AutoCompleteType="Disabled" OnTextChanged="txtTravelAmt_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="txtTravelAmt"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>
                                                                    &emsp;&emsp;
                                                    <asp:Label ID="Label1" runat="server" class="col-sm-3 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtUserTravelRemarks" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <br />

                                                                </div>

                                                                <div class="form-group row" id="divadv" runat="server" visible="false">
                                                                    <asp:Label ID="lbladvanceamt" runat="server" class="col-sm-3 col-form-label">Advance Paid :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtadvance" AutoCompleteType="Disabled" runat="server" AutoPostBack="true" OnTextChanged="txtadvance_TextChanged"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtadvance"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                    </div>
                                                                    &emsp;&emsp;
                                                                     <asp:Label ID="lbladvvoucher" runat="server" class="col-sm-3 col-form-label">Advance Voucher :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtadvvoucher" runat="server"></asp:TextBox>
                                                                    </div>
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
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtEnterDesc" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">


                                                                    <asp:Label ID="Label5" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" AutoCompleteType="Disabled" ID="txtEntAmount" OnTextChanged="txtEntAmount_TextChanged" AutoPostBack="true" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <%--  <asp:Label ID="Label11" runat="server" class="col-sm-2 col-form-label">Entertainment Description :-</asp:Label>
                                    <div class="col-sm-2">
                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtEnterDesc" runat="server"></asp:TextBox>
                                    </div>--%>
                                                                    <asp:Label ID="Label8" runat="server" class="col-sm-2 col-form-label">Remarks :-</asp:Label>
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

                                                                <br />
                                                                <br />

                                                            </div>
                                                            <%-- </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>

                                            <section class="content" id="Section1" runat="server">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-body">
                                                            <div class="form-group row" runat="server" id="Approveamt">
                                                                <asp:Label ID="Label12" runat="server" class="col-sm-2 col-form-label">Approved Amount :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtApprovedAmt" Enabled="false" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
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
                                                        <div class="form-group row" id="advamt" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="Label16" runat="server" class="col-sm-4 col-form-label"><b>Advance Paid :-</b></asp:Label>
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
                                                                <asp:Label ID="lblDate" runat="server" class="col-sm-3 col-form-label">Date of Advance:-</asp:Label>
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
