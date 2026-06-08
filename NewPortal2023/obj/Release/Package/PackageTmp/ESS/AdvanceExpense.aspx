<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="AdvanceExpense.aspx.cs" Inherits="NewPortal2023.ESS.AdvanceExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function () {
            $('[id*=txtDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
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
                            <h3 style="color: white">Advance Expense</h3>
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
                                        
                                        <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>
                                        <section id="SectionList" class="content-header" runat="server">

                                            <div class="container-fluid">
                                                <!-- SELECT2 EXAMPLE -->
                                                <div class="card card-default">

                                                    <div class="card-body">

                                                       <%-- <div class="card-header" runat="server" id="divnotes">

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
                                                        </div>--%>

                                                        <br />

                                                        <div class="row">
                                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="showLoader();" />
                                                        </div>
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
                                                                    <asp:TextBox class="form-control" ID="txtempcode" runat="server" OnTextChanged="txtempcode_TextChanged" AutoPostBack="true" OnClientClick="showLoader();"></asp:TextBox>
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
                                                                    <asp:DropDownList ID="drptype" runat="server" CssClass="form-control" Width="150px">
                                                                        <asp:ListItem Value="">[Select Type]</asp:ListItem>
                                                                        <asp:ListItem Value="Domestic" Text="Domestic"></asp:ListItem>
                                                                        <asp:ListItem Value="Local" Text="Local"></asp:ListItem>
                                                                        <asp:ListItem Value="Welfare" Text="Staff Welfare"></asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                             <br />
                                                            <div class="form-group row">
                                                                  <asp:Label ID="lblvoucher" runat="server" class="col-sm-3 col-form-label">Advance Voucher :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control"  ID="txtvoucher" runat="server"></asp:TextBox>
                                                                </div>
                                                                 <asp:Label ID="lblamt" runat="server" class="col-sm-3 col-form-label">Advance Amount.:-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="txtadvamt" runat="server"></asp:TextBox>
                                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtadvamt"
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
                                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" OnClick="btnClose_Click" runat="server" Text="Close" OnClientClick="showLoader();" />
                                                                 &emsp;&emsp; &emsp;&emsp; &emsp;&emsp;
                                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Submit" CssClass="btn btn-success" OnClientClick="showLoader();" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnAddNew" />
                                    <asp:PostBackTrigger ControlID="gvAdvanceClaimList" />
                                     <asp:PostBackTrigger ControlID="txtempcode" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>

            </div>
        </section>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerFrom);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerTo);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerRfFrom);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerRfTo);

            function dtpickerFrom(sender, args) {
                if (document.getElementById('<%= txtDate.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtDate.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }


        </script>
    </section>


</asp:Content>
