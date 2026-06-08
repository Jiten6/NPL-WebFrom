<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="COGeneratedPage.aspx.cs" Inherits="NewPortal2023.ESS.COGeneratedPage" %>

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

            var currentDate = new Date();

            $('[id*=txtFRDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",
                autoclose: true,
                //  endDate: currentDate // Disable dates after the current date
            });
        });
        $(function () {
            $('[id*=txtTODate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",

                //autoclose: true,
                //endDate: currentDate // Disable dates after the current date
            });
        });
        $(function () {

            var currentDate = new Date();

            $('[id*=txtFRDate1]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",
                autoclose: true,
                //  endDate: currentDate // Disable dates after the current date
            });
        });
        $(function () {
            $('[id*=txtTODate1]').datepicker({
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">CO Application</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" EnableViewState="true">
                                <ContentTemplate>--%>
                            <div id="form1" runat="server">
                                <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                    <button data-dismiss="alert" class="close close-sm" type="button">
                                        <i class="fa fa-times"></i>
                                    </button>
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </div>
                                <div id="divAlertSucc" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                    <button data-dismiss="alert" class="close close-sm" type="button">
                                        <i class="fa fa-times"></i>
                                    </button>
                                    <asp:Label ID="lblMessageSucc" runat="server"></asp:Label>
                                </div>

                                &emsp;
                            </div>
                            <div class="row">
                                <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Autopostback="true" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" />
                            </div>

                            <div id="AppList" visible="true" runat="server">
                                <label>
                                    <h3>CO Application List</h3>
                                </label>
                                <br />
                                <div class="form-gro    up" visible="false">


                                    <%--<asp:Label ID="lbldrp" Text="Search by Leave Type :-" runat="server" Font-Bold="True" ForeColor="black"></asp:Label>--%>

                                    <asp:Label ID="lblYear" Visible="false" runat="server" class="col-lg-3 col-form-label"> Select Year :-</asp:Label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="drpLeaveType" Visible="false" runat="server" CssClass="form-control input-sm-3" OnSelectedIndexChanged="drpLeaveType_SelectedIndexChanged" AutoPostBack="true" ForeColor="#205A94">
                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                            <asp:ListItem Value="2023">2023</asp:ListItem>
                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <br />
                                </div>

                                <div class="form-group" id="OtherDetails" visible="true" runat="server" style="margin-top: 0px">
                                    <asp:GridView ID="gvCO" runat="server" AutoGenerateColumns="False"
                                        HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="Server" Text='<%# Eval("CID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CO Applied Date">
                                                <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFrom" runat="Server" Text='<%# Eval("FROM_DT") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridViewHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFR" runat="Server" Text='<%# Eval("FR_DATE") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridViewHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date" Visible="false">
                                                <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTO" runat="Server" Text='<%# Eval("TO_DATE") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridViewHeader" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Reason Type">
                                                <ItemStyle CssClass="GridViewItem" Width="130px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReason" runat="Server" Text='<%# Eval("REASON_TYPE") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridViewHeader" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReason1" runat="Server" Text='<%# Eval("REMARKS") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridViewHeader" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Applied Date">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Left" Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapp" runat="Server" Text='<%# Eval("CREATEDDT") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUP" runat="Server" Text='<%# Eval("STATUSTYPE") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridViewHeader" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EmptyDataTemplate>
                                            <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                <tr>

                                                    <td class="GridViewHeader" style="width: 120px">From Date
                                                    </td>
                                                    <td class="GridViewHeader" style="width: 400px">Reason Type
                                                    </td>

                                                    <td class="GridViewHeader" style="width: 250px">Remarks
                                                    </td>
                                                    <td class="GridViewHeader" style="width: 200px">Applied Date
                                                    </td>
                                                    <td class="GridViewHeader" style="width: 100px">Status
                                                    </td>

                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div id="AppForm" visible="false" runat="server">
                                <div class="panel-body">
                                    <div class="adv-table">
                                        <div id="" class="form-horizontal">
                                            <fieldset>
                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">Select CO Type :</label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlScheduleOptions" runat="server" CssClass="form-control input-sm-3" OnSelectedIndexChanged="ddlScheduleOptions_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                            <asp:ListItem Value="24 Hours Duty">24 Hours Duty</asp:ListItem>
                                                            <asp:ListItem Value="PH on Weekly Off">PH on Weekly Off</asp:ListItem>
                                                            <asp:ListItem Value="PH + 24 HRS on Weekly Off - 2 CO">PH + 24 HRS on Weekly Off - 2 CO</asp:ListItem>
                                                            <asp:ListItem Value="Working on 2nd Weekly Off">Working on 2nd Weekly Off</asp:ListItem>
                                                            <asp:ListItem Value="24 HRS on 2nd Weekly Off - 2 CO">24 HRS on 2nd Weekly Off - 2 CO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <label class="col-sm-3 labels">CO Generated Date :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox class="form-control datepicker" ID="txtFromDate" runat="server" AutoCompleteType="Disabled" OnTextChanged="txtCODate_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div id="firstleaveDiv" class="form-group" runat="server">
                                                    <div>
                                                        <label class="col-sm-3 labels" id="lblLeaveFromDate" runat="server">Leave From Date :</label>
                                                        <div class="col-sm-3">

                                                            <asp:TextBox class="form-control datepicker" ID="txtFRDate" runat="server" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="txtFRDate_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>




                                                    <%--  <div id="LeaveDiv" class="form-group" runat="server" visible="false">
                                                    <label class="col-sm-3 labels">Leave To Date :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox class="form-control datepicker" ID="txtTODate" runat="server" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="txtTODate_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>--%>

                                                    <div id="anotherleave" class="form-group" runat="server" >
                                                        <label class="col-sm-3 labels">Second leave date :</label>
                                                        <div class="col-sm-3" style="padding-right: 2.5%">
                                                            <asp:TextBox class="form-control datepicker" ID="txtTODate" runat="server" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="txtTODate_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>




                                                <div id="idlbltotaldays" class="form-group" runat="server" visible="false">
                                                    <label class="col-sm-3 labels">Total Days :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblTotalDays" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="form-group">

                                                    <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                        <asp:Button ID="btnApprove" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnApprove_Click" OnClientClick="showLoader();" />
                                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" OnClientClick="showLoader();" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </section>
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
