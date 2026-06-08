<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="PLPPayoutApproval.aspx.cs" Inherits="NewPortal2023.ESS.PLPPayoutApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#gvList").DataTable({
                "responsive": true, "lengthChange": false, "autoWidth": false,
                "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
            }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            $('#example2').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false, lnkBtnAddRowSteps
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <div class="d-flex flex-wrap">
                            <div class="flex-grow-1">
                                <header class="panel-heading" style="background-color: darkcyan">
                                    <h3 style="color: white">PLP Payout Approval</h3>
                                </header>
                                <div class="panel-body">
                                    <asp:ScriptManager ID="scm" runat="server">
                                        <Scripts>
                                            <asp:ScriptReference Path="~/Assets/jquery.blockUI.js" />
                                            <asp:ScriptReference Path="~/Assets/blockUI.js" />
                                        </Scripts>
                                    </asp:ScriptManager>

                                    <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="vwListView" runat="server">
                                            <div class="col-lg-12 row">
                                                <div class="col-sm-3" style="margin-left: -20px">
                                                    <asp:Label ID="Label6" runat="server" Text="Financial Year :" CssClass="card-title" Style="font-size: 20px;"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="drpFinancialYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFinancialYear_SelectedIndexChanged" CssClass="form-control input-sm-3" Enabled="true" onchange="showLoader();">
                                                            <asp:ListItem Value="">Select Year</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <br />
                                                </div>
                                            </div>

                                            <div class="col-12" id="divKRAFin" runat="server" visible="false">
                                                <div class="col-12" style="margin-left: -5px; display: flex; justify-content: center; align-items: center;">
                                                    <asp:Label ID="lblCycle" runat="server" Text="" CssClass="card-title" Style="font-size: 30px;"></asp:Label>
                                                </div>

                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <hr />
                                                    </div>
                                                </div>
                                                <div class="adv-table" id="accordion" runat="server">
                                                    <div id="divAlertListView" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                        <button data-dismiss="alert" class="close close-sm" type="button">
                                                            <i class="fa fa-times"></i>
                                                        </button>
                                                        <asp:Label ID="lblMessageListView" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="card-body" style="margin: 20px">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <div class="form-group row">
                                                                    <div class="col-06">
                                                                        <asp:DropDownList ID="DropDownList1" runat="server" Visible="false" CssClass="form-control input-sm-3" AutoPostBack="true" OnSelectedIndexChanged="drpQuarter_SelectedIndexChanged" Width="150px">

                                                                            <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                                                            <asp:ListItem Value="2">Second Quarter</asp:ListItem>
                                                                            <asp:ListItem Value="3">Third Quarter</asp:ListItem>
                                                                            <asp:ListItem Value="4">Fourth Quarter</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div id="divCompFAct" runat="server" visible="false">
                                                                    <div class="form-group row">
                                                                        <div class="col-12">
                                                                            <h3 class="card-title">Company Factors</h3>
                                                                        </div>
                                                                        <div class="col-12">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False"
                                                                                        CssClass="display table table-bordered table-striped dynamic-table" >
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Area">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblArea" runat="Server" Font-Bold="true" Text='<%# Eval("AREA") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Factors">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblFactor" runat="Server" Font-Bold="true" Text='<%# Eval("FACTORS") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Employee Type">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblArea" runat="Server" Font-Bold="true" Text='<%# Eval("EMPLOYEE_TYPE") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Target">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblTotalWtg" runat="Server" Font-Bold="true" Text='<%# Eval("TARGET") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Achieved">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblTotalAchPerc" runat="Server" Font-Bold="true" Text='<%# Eval("ACHIEVED") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td style="width: 10%" class="Title">
                                                                                                        <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:GridView>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row" style="margin: 10px">
                                                                        <div class="col-sm-12" style="margin-left: -40px">
                                                                            <h3 class="card-title">Input Company Factor And Company Promise</h3>
                                                                        </div>
                                                                        &emsp;
                                                        <div class="form-group row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="lblKRATitle" runat="server" Text="Select Employee Type :" Style="font-size: 15px;"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="drpEmpType" runat="server" CssClass="form-control input-sm-3" AutoPostBack="true" OnSelectedIndexChanged="drpEmpType_SelectedIndexChanged" onchange="showLoader();" Width="200px">
                                                                    <asp:ListItem Value="1">Select Employee Type</asp:ListItem>
                                                                    <asp:ListItem Value="ABOVE SR MANAGER">ABOVE SR MANAGER</asp:ListItem>
                                                                    <asp:ListItem Value="BELOW SR MANAGER">BELOW SR MANAGER</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                                    </div>


                                                                    &emsp;

                                                    <div id="divAboveSr" runat="server" visible="false" class="form-group row" style="margin: 10px">
                                                        <div class="form-group row">
                                                            <div class="col-sm-3">
                                                                <%--style="margin-left: 10px"--%>
                                                                <asp:Label ID="Label1" runat="server" Text="Company Factor(Sales Volume) (30%)" Style="font-size: 15px;"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCompFactAbv30" runat="Server" CssClass="form-control input-sm" Width="200px" Text='' Placeholder="Out Of 30%" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="Label2" runat="server" Text="Company Promise(PBT (Profit Before Tax)) (20%)" Style="font-size: 15px;"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCompFactAbv20" runat="Server" CssClass="form-control input-sm" Width="200px" Text='' Placeholder="Out Of 20%" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="Label5" runat="server" Text="Company Promise(Uninterrupted production (Downtime of ~ 2 days/month for Routine maintenance) (Production days - 341, Maintenance Days - 24)) (20%)" Style="font-size: 15px;"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCompPromAbv" runat="Server" CssClass="form-control input-sm" Width="200px" Text='' Placeholder="Out Of 20%" />
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer justify-content-between" style="text-align: right;">
                                                            <asp:Button ID="btnSubmitAbv" runat="server" AutoPostBack="true" OnClick="btnSubmitAbv_Click" OnClientClick="showLoader();" Text="Submit" CssClass="btn btn-success" />
                                                        </div>
                                                    </div>


                                                                    <div id="divBelowSr" runat="server" visible="false" class="form-group row" style="margin: 10px">
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-3">
                                                                                <asp:Label ID="Label3" runat="server" Text="Company Factor(18%)" Style="font-size: 15px;"></asp:Label>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <asp:TextBox ID="txtCompFactBlw18" runat="Server" CssClass="form-control input-sm" Width="200px" Text='' Placeholder="Out Of 18%" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-3">
                                                                                <asp:Label ID="Label4" runat="server" Text="Company Factor(12%)" Style="font-size: 15px;"></asp:Label>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <asp:TextBox ID="txtCompFactBlw12" runat="Server" CssClass="form-control input-sm" Width="200px" Text='' Placeholder="Out Of 12%" />
                                                                            </div>

                                                                        </div>
                                                                        <div class="modal-footer justify-content-between" style="text-align: right;">
                                                                            <asp:Button ID="btnSubmitBlw" runat="server" AutoPostBack="true" OnClick="btnSubmitBlw_Click" OnClientClick="showLoader();" Text="Submit" CssClass="btn btn-success" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                &emsp;

                                                    <div class="form-group row">
                                                        <div class="col-12">
                                                            <h3 class="card-title">Employee List</h3>
                                                        </div>
                                                        <div class="col-12">

                                                            <asp:GridView ID="gvLIstVIew" runat="server" AutoGenerateColumns="False"
                                                                CssClass="display table table-bordered table-striped dynamic-table">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Employee Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmp_name" runat="Server" Text='<%# Eval("EMP_NAME") %>' />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employee ID">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblEmpmid" runat="Server" Text='<%# Eval("EMP_MID") %>' />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="View">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" class="btn btn-info" Text="View" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAddCORowId" runat="Server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table>
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
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="gvLIstVIew" />
                                                                <asp:PostBackTrigger ControlID="drpEmpType" />
                                                                <asp:PostBackTrigger ControlID="btnSubmitAbv" />
                                                                <asp:PostBackTrigger ControlID="btnSubmitBlw" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>

                                        <asp:View ID="vwList" runat="server">
                                            <div class="adv-table" id="accordionTwo" runat="server">
                                                <div class="card-body" style="margin: 20px">
                                                    <asp:UpdatePanel ID="updProofEdit" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group row">
                                                                <div class="col-06">
                                                                    <asp:DropDownList ID="drpQuarter" runat="server" Visible="false" CssClass="form-control input-sm-3" AutoPostBack="true" OnSelectedIndexChanged="drpQuarter_SelectedIndexChanged" Width="150px">

                                                                        <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                                                        <asp:ListItem Value="2">Second Quarter</asp:ListItem>
                                                                        <asp:ListItem Value="3">Third Quarter</asp:ListItem>
                                                                        <asp:ListItem Value="4">Fourth Quarter</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <h3 class="card-title">
                                                                    <asp:Literal ID="EmpName" runat="server"></asp:Literal>
                                                                    (<asp:Literal ID="EmpCode" runat="server"></asp:Literal>)
                                                                </h3>
                                                                <hr />
                                                            </div>

                                                            <div class="form-group row">
                                                                <div class="col-12">
                                                                    <h3 class="card-title">List</h3>
                                                                </div>
                                                                <div class="col-12">
                                                                    <asp:UpdatePanel ID="updListAdd" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="gvPLP" runat="server" ShowFooter="true" OnRowDataBound="gvPLP_RowDataBound" AutoGenerateColumns="False"
                                                                                CssClass="display table table-bordered table-striped dynamic-table">
                                                                                <Columns>

                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Button ID="btn_Edit" Visible="false" runat="server" Text="Edit" CommandName="Edit" />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:Button ID="btn_Update" Visible="false" runat="server" Text="Update" CommandName="Update" />
                                                                                            <asp:Button ID="btn_Cancel" Visible="false" runat="server" Text="Cancel" CommandName="Cancel" />
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Area">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblArea" runat="Server" Text='<%# Eval("AREA") %>' />
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="FTtarget" runat="Server" Text="Total :-" Font-Bold="true" />
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Target">
                                                                                        <ItemTemplate>
                                                                                            <%--<asp:TextBox ID="txtTarget" Enabled="false" runat="Server" CssClass="form-control input-sm " TextMode="MultiLine" Text='<%# Eval("AREA") %>' />--%>
                                                                                            <asp:Label ID="lblTarget" runat="Server" Text='<%# Eval("TARGET") %>' />
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblTotalTarget" runat="Server" Font-Bold="true" />
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Achieved">
                                                                                        <ItemTemplate>
                                                                                            <%--<asp:TextBox ID="txtAcheived" Enabled="false" runat="Server" CssClass="form-control input-sm " TextMode="MultiLine" Text='<%# Eval("AREA") %>' />--%>
                                                                                            <asp:Label ID="lblAchieved" runat="Server" Text='<%# Eval("ACHIEVED") %>' />
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblTotalAchieved" runat="Server" Font-Bold="true" />
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="width: 10%" class="Title">
                                                                                                <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="modal-footer justify-content-between">
                                                                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                                                    <asp:Button ID="BtnSave" Visible="false" runat="server" OnClick="BtnSave_Click" Text="Save changes" CssClass="btn btn-success" />
                                                                </div>

                                                                <div class="modal-footer justify-content-between" style="text-align: center;">
                                                                    <%--<asp:Button ID="btnPrev" runat="server" OnClick="btnPrev_Click" Visible="false" OnClientClick="return ConPrev();" Text="Previous" CssClass="btn btn-info" />--%>
                                                                    <asp:Button ID="btnList" runat="server" AutoPostBack="true" OnClick="btnList_Click" OnClientClick="showLoader();" Text="View Employee List" CssClass="btn btn-info" />
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="BtnSave" />
                                                            <asp:PostBackTrigger ControlID="btnList" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>


                                                </div>
                                            </div>

                                        </asp:View>

                                    </asp:MultiView>

                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
