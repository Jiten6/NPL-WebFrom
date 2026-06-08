<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="CompanyFactorDetails.aspx.cs" Inherits="NewPortal2023.ESS.CompanyFactorDetails" %>

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

    <script>
<%--        function LinkCompetitivenessClicked() {
            var vals = "";
            if (document.getElementById("<%= btnCompetitiveness.ClientID %>").value == "Competitiveness") {
                vals = document.getElementById("<%= btnCompetitiveness.ClientID %>").value;
                document.getElementById("<%= lblTitle.ClientID %>").innerHTML = vals;
            }
        }
        function LinkOperationalExcellenceClicked() {
            var vals = "";
            if (document.getElementById("<%= btnOperationalExcellence.ClientID %>").value == "Operational Excellence") {
                vals = document.getElementById("<%= btnOperationalExcellence.ClientID %>").value;
                document.getElementById("<%= lblTitle.ClientID %>").innerHTML = vals;
            }
        }
        function LinkPeopleClicked() {
            var vals = "";
            if (document.getElementById("<%= btnPeople.ClientID %>").value == "People") {
                vals = document.getElementById("<%= btnPeople.ClientID %>").value;
                document.getElementById("<%= lblTitle.ClientID %>").innerHTML = vals;
            }
        }
        function LinkFutureReadinessClicked() {
            var vals = "";
            if (document.getElementById("<%= btnFutureReadiness.ClientID %>").value == "Future Readiness") {
                vals = document.getElementById("<%= btnFutureReadiness.ClientID %>").value;
                document.getElementById("<%= lblTitle.ClientID %>").innerHTML = vals;
            }
        }
        function LinkFinancialsClicked() {
            var vals = "";
            if (document.getElementById("<%= btnFinancials.ClientID %>").value == "Financials") {
                vals = document.getElementById("<%= btnFinancials.ClientID %>").value;
                document.getElementById("<%= lblTitle.ClientID %>").innerHTML = vals;
            }
        }--%>

        function LinkCompanyFactorClicked() {
            var vals = "";
            if (document.getElementById("<%= btnCompanyFactor.ClientID %>").value == "Company Factor") {
                vals = document.getElementById("<%= btnCompanyFactor.ClientID %>").value;
                document.getElementById("<%= lblTitle.ClientID %>").innerHTML = vals;
            }
        }
        function LinkCompanyPromisesClicked() {
            var vals = "";
            if (document.getElementById("<%= btnCompanyPromises.ClientID %>").value == "Company Promises") {
                vals = document.getElementById("<%= btnCompanyPromises.ClientID %>").value;
                document.getElementById("<%= lblTitle.ClientID %>").innerHTML = vals;
            }
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
                            <h3 style="color: white">Company Factor</h3>
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
                                                    <div class="col-sm-12 row">
                                                        <asp:Button ID="btnCompanyFactor" runat="server" OnClientClick="LinkCompanyFactorClicked()" OnClick="btnCompanyFactor_Click" class="btn btn-warning" Text="Company Factor" data-toggle="modal" data-target="#modal-xl" Style="margin-left:-10px" />

                                                        <asp:Button ID="btnCompanyPromises" runat="server" OnClientClick="LinkCompanyPromisesClicked()" OnClick="btnCompanyPromises_Click" class="btn btn-warning" Text="Company Promise" data-toggle="modal" data-target="#modal-xl" />
                                                    </div>

                                                    <br />
                                                    <br />

                                                    <div class="form-group row">
                                                        <div class="col-12">
                                                            <h3 class="card-title">List</h3>
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound" ShowFooter="true" CssClass="table table-bordered table-striped">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAID" runat="Server" />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="one" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Area">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblArea" runat="Server" Text='<%# Eval("AREA") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="two" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Metric">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMetric" runat="Server" Text='<%# Eval("METRIC") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="three" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTARGET" runat="Server" Text='<%# Eval("TARGET") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="FTtarget" runat="Server" Text="Total :-" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="% twt" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltwt" runat="Server" Text='<%# Eval("TWT") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="FTTWT" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Weightage">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWeightage" runat="Server" Text='<%# Eval("Weightage") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalWeightage" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ach/ Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAchiveRemark" runat="Server" Text='<%# Eval("Achievement_REMARKS") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="FTACCHIV" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ach%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAchive" runat="Server" Text='<%# Eval("Achievement_Perct") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="FTACHIVEPER" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Calculation">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCalculation" runat="Server" Text='<%# Eval("Calculation") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalCalculation" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Score">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblScore" runat="Server" Text='<%# Eval("Achievement_Source") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalSCORE" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Appraiser’s Comments">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="FTAP" runat="Server" Text='<%# Eval("Modified_Score_MDs_Recommendation") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAcComment" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRemarks" runat="Server" Text='<%# Eval("RPT_REMARK") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="FTR" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table class="table table-bordered">

                                                                        <tr>
                                                                            <td style="width: 100%" class="Title">
                                                                                <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>

                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>


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
                                                    <asp:UpdatePanel ID="updListAdd" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvSteps" runat="server" OnRowUpdating="gvSteps_RowUpdating" OnRowCancelingEdit="gvSteps_RowCancelingEdit" OnRowDataBound="gvSteps_RowDataBound" OnRowEditing="gvSteps_RowEditing" AutoGenerateColumns="False"
                                                                CssClass="display table table-bordered table-striped dynamic-table">
                                                                <Columns>
                                                                    <asp:TemplateField ShowHeader="true">
                                                                        <ItemStyle VerticalAlign="Middle" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btn_Edit" Visible="false" runat="server" Text="Edit" CommandName="Edit" />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:Button ID="btn_Update" Visible="false" runat="server" Text="Update" CommandName="Update" />
                                                                            <asp:Button ID="btn_Cancel" Visible="false" runat="server" Text="Cancel" CommandName="Cancel" />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Area" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtArea" runat="Server" CssClass="form-control input-sm datepicker" TextMode="MultiLine" Text='<%# Eval("AREA") %>' MaxLength="50" /><%--<%# Eval("AREA") %>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Metric">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtMetric" runat="Server" CssClass="form-control input-sm datepicker" TextMode="MultiLine" Text='<%# Eval("METRIC") %>' />
                                                                            <%--<%# Eval("METRIC") %>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtTarget" runat="Server" CssClass="form-control input-sm datepicker" TextMode="MultiLine" Text='<%# Eval("TARGET") %>' /><%-- <%# Eval("TARGET") %>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="% wt" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtwt" runat="Server" CssClass="form-control input-sm datepicker" TextMode="MultiLine" Text='<%# Eval("TWT") %>' /><%-- <%# Eval("TWT") %>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Weightage">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtWeightage" runat="Server" CssClass="form-control input-sm datepicker" TextMode="MultiLine" Text='<%# Eval("Weightage") %>' />
                                                                            <%--<%# Eval("Weightage") %>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ach/ Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtRemarks" runat="Server" CssClass="form-control input-sm datepicker" TextMode="MultiLine" Text='<%# Eval("Achievement_REMARKS") %>' /><%-- <%# Eval("Achievement_REMARKS") %>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ach%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtAchPer" runat="Server" CssClass="form-control input-sm datepicker" TextMode="MultiLine" Text='<%# Eval("Achievement_Perct") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:TemplateField HeaderText="Calculation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRevokes" runat="Server" Text='' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRevokes" runat="Server" Text='' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modified Score / MDs Recommendation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRevokes" runat="Server" Text='' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRevokes" runat="Server" Text='' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HR Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRevokes" runat="Server" Text='' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtaid" runat="Server" CssClass="form-control input-sm datepicker" Text='<%# Eval("Appraisal_AID") %>' /><%-- <%# Eval("Achievement_REMARKS") %>--%>
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
                                                            <asp:LinkButton ID="lnkBtnAddRowSteps" Text="Add Row" runat="server" Font-Bold="true" ForeColor="Blue"
                                                                OnClick="lnkBtnAddRowSteps_Click" CssClass="Title" />
                                                            <asp:Label ID="lblSeperatorSteps" runat="server" Text="|" Font-Bold="true" ForeColor="blue" />
                                                            <asp:LinkButton ID="lnkBtnDeleteRowSteps" Text="Delete Selected" runat="server" Font-Bold="true" ForeColor="Blue"
                                                                OnClick="lnkBtnDeleteRowSteps_Click" CssClass="Title" />                                                                
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="modal-footer justify-content-between">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                            <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" Text="Save changes" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>

                            <div class="modal-footer justify-content-between">
                                <asp:Button ID="Button1" runat="server" Visible="false" OnClick="btnNext_Click" Text="Section2" CssClass="btn btn-info" />
                                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Next" OnClientClick="return ConNext();" CssClass="btn btn-info ml-auto" />
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
