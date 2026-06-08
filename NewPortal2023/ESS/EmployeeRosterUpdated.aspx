<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="EmployeeRosterUpdated.aspx.cs" Inherits="NewPortal2023.ESS.EmployeeRosterUpdated" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
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
                            <h3 style="color: white">Employee Roaster Update</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>


                            <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                <button data-dismiss="alert" class="close close-sm" type="button">
                                    <i class="fa fa-times"></i>
                                </button>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>

                            <div id="div1" runat="server" class="form-group row" style="margin: 10px">


                                <div class="col-sm-3">
                                    <label style="font-size: 15px;">Select Type :</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="drpType" runat="server" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm" Width="200px" onchange="showLoader();">

                                        <asp:ListItem Value="" Text="All Employees"></asp:ListItem>
                                        <asp:ListItem Value="EmployeeWise" Text="Employee Wise"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div id="divEmp" runat="server" visible="false">
                                    <div class="col-sm-3">
                                        <asp:Label ID="Label1" runat="server" Text="Employee Code :" Style="font-size: 15px;"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--<asp:TextBox ID="txtEmpCode" runat="server" Visible="true" AutoCompleteType="Disabled" CssClass="form-control input-sm " Width="200px"></asp:TextBox>--%>
                                          <asp:DropDownList ID="drpEmpType" runat="server" CssClass="select2" Width="55%" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpEmpType_SelectedIndexChanged">
                                            </asp:DropDownList>

                                    </div>
                                </div>
                            </div>


                            <div id="trSubmit1" runat="server">
                                <div class="form-group row" style="margin: 27px">
                                    <div class="form-group row">
                                        <asp:Label ID="lblfDt" runat="server" class="col-sm-3" Style="font-size: 15px;">From Date<span style="color: Red;">*</span> :</asp:Label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtFromDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm datepicker" placeholder="Date" Width="200px"></asp:TextBox>
                                        </div>

                                        <asp:Label ID="lbltDt" runat="server" class="col-sm-3" Style="font-size: 15px;">To Date<span style="color: Red;">*</span> :</asp:Label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtToDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm datepicker" placeholder="Date" Width="200px"></asp:TextBox>
                                        </div>






                                        <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                            <asp:Button ID="btnGenerateReport" runat="server" CssClass="btn btn-success" Text="Generate Report"
                                                OnClick="btnGenerateReport_Click" OnClientClick="showLoader();" />
                                        </div>

                                        <div class="col-sm-12" style="text-align: center; margin-top: 20px">
                                            <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-primary" Text="Export" OnClick="btnExport_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            &emsp;

                            <div id="divViewList" runat="server">
                                <%--<div id="DivgvMultipleList" visible="true" runat="server" style="overflow-x: scroll;">--%>
                                <asp:GridView ID="gvRoster" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Left" CellPadding="5"
                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Emp Code" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkAppNo" runat="server" Text='<%# Eval("Emp_Code") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridViewHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="Server" Text='<%# Eval("Emp_Name") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridViewHeader" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Department" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldepartment" runat="Server" Text='<%# Eval("Department") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="GridViewItem" Width="12%" BackColor="white" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridViewHeader" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrigin" runat="Server" Text='<%# Eval("Date") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridViewHeader" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldays" runat="Server" Text='<%# Eval("Days") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridViewHeader" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Old Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatusTRANS" runat="Server" Text='<%# Eval("Shift_Schedule") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridViewHeader" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="New Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatusTRANS" runat="Server" Text='<%# Eval("New_Shift_Schedule") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridViewHeader" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="New Shift">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlNewShift" runat="server">
                                                    <asp:ListItem Text="--Select One--" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="GS" Value="GS"></asp:ListItem>
                                                    <asp:ListItem Text="NS" Value="NS"></asp:ListItem>
                                                    <asp:ListItem Text="EV" Value="EV"></asp:ListItem>
                                                    <asp:ListItem Text="WO" Value="WO"></asp:ListItem>
                                                    <asp:ListItem Text="MS" Value="MS"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridViewHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" OnClick="lnkUpdate_Click" ForeColor="Blue"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
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
                                <%--</div>--%>
                            </div>
                        </div>

                    </section>
                </div>

            </div>
        </section>


        <script>
            function myFunction() {
                alert("Are you sure you want to delete this attendance punch Detail ? ");
            }
        </script>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerFrom);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerTo);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerRfFrom);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerRfTo);

            function dtpickerFrom(sender, args) {
                if (document.getElementById('<%= txtFromDate.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtFromDate.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }

            if (document.getElementById('<%= txtFromDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtFromDate.ClientID %>'),
                    format: 'DD-MM-YYYY'
                });
            }



            function dtpickerTo(sender, args) {
                if (document.getElementById('<%= txtToDate.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtToDate.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }


            if (document.getElementById('<%= txtToDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtToDate.ClientID %>'),
                    format: 'DD-MM-YYYY'
                });
            }

            }
        </script>
        <%--<script>
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
        </script>--%>
          <script type="text/javascript">
            // Initial load
            $(document).ready(function () {
                $('#<%= drpEmpType.ClientID %>').select2();
            });

            // After every postback
            Sys.Application.add_load(function () {
                $('#<%= drpEmpType.ClientID %>').select2();
            });
        </script>
    </section>

</asp:Content>
