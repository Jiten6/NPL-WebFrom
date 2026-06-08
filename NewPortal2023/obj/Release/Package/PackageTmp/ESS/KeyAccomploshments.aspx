<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="KeyAccomploshments.aspx.cs" Inherits="NewPortal2023.ESS.Key_Accomploshments" %>

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
                                    <h3 style="color: white">KEY ACCOMPLISHMENTS</h3>
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

                                                        <div class="col-lg-12 row">
                                                            <div class="col-sm-3" style="margin-left: -20px">
                                                                <asp:Label ID="Label2" runat="server" Text="Financial Year :" CssClass="card-title" Style="font-size: 20px;"></asp:Label>
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
                                                                <asp:Label ID="lblCycle" runat="server" Visible="false" Text="" CssClass="card-title" Style="font-size: 30px;"></asp:Label>
                                                            </div>

                                                            <div class="col-lg-12">
                                                                <div class="form-group">
                                                                    <hr />
                                                                </div>
                                                            </div>

                                                            <div id="divKey" runat="server" class="col-12">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="grKey" runat="server" OnRowDataBound="grKey_RowDataBound" ToolTip="SECTION II (a)" AutoGenerateColumns="False" Class="table table-bordered table-striped">
                                                                            <Columns>
                                                                                <asp:TemplateField ShowHeader="true">
                                                                                    <ItemStyle VerticalAlign="Middle" />
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblHead" runat="Server" Text='' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAddCORowId" runat="Server" Text='<%# Eval("CO_Row_Id") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Major contributions Achievements not covered under KRAs for the year">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtMajor" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Major_contributions_Achievements_not_covered_under_KRAs_for_the_year") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Remarks">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtRmk" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("REMARKS") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Appraiser's comments">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtRptRmk" runat="Server" ReadOnly="true" Style="background-color: #f0f0f0; color: #888; border: 1px solid #ddd;" TextMode="MultiLine" Text='<%# Eval("Appraisers_comments") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="HR Remarks" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtHRRmk" TextMode="MultiLine" runat="Server" Text='' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <table class="table table-bordered table-striped">
                                                                                    <%--<tr>
                                                                            <td>Sr.No. </td>
                                                                            <td>List down the training programs attended during the year: </td>
                                                                            <td>Elaborate on how exposure to these programs has improved effectiveness in your work area:" </td>
                                                                            <td>Appraisers comments: </td>
                                                                            <td>HR Remarks: </td>

                                                                        </tr>--%>
                                                                                    <tr>
                                                                                        <td style="width: 10%" class="Title">
                                                                                            <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </EmptyDataTemplate>

                                                                        </asp:GridView>

                                                                        <div id="divAddRow" runat="server">
                                                                            <asp:LinkButton ID="lnkBtnAddRowKey" Text="Add Row" runat="server" Font-Bold="true" ForeColor="Blue"
                                                                                OnClick="lnkBtnAddRowKey_Click" CssClass="Title" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                            <asp:Label ID="Label1" runat="server" Text="|" Font-Bold="true" ForeColor="blue" />
                                                                            <asp:LinkButton ID="lnkBtnDeleteRowKey" Text="Delete Selected" runat="server" Font-Bold="true" ForeColor="Blue"
                                                                                OnClick="lnkBtnDeleteRowKey_Click" CssClass="Title" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        </div>

                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="lnkBtnAddRowKey" />
                                                                        <asp:PostBackTrigger ControlID="lnkBtnDeleteRowKey" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                <div class="row">
                                                                    <div class="col-sm-10">
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                                                        <asp:Button ID="BtnSaveOne" runat="server" OnClick="BtnSaveOne_Click" Text="Save changes" CssClass="btn btn-success" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div id="divList" runat="server">
                                                                <div class="col-12">
                                                                    <h3 class="card-title">Key Accomplishment Summary</h3>
                                                                </div>
                                                                <div class="col-12">
                                                                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" Class="table table-bordered table-striped">
                                                                        <Columns>
                                                                            <asp:TemplateField ShowHeader="true" Visible="false">
                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHead" runat="Server" Text='' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAddCORowId" runat="Server" Enabled="false" Text='<%# Eval("CO_Row_Id") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Major contributions Achievements not covered under KRAs for the year">
                                                                                <ItemTemplate>
                                                                                    <%--<asp:TextBox ID="" runat="Server" Enabled="false" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" Text='<%# Eval("Major_contributions_Achievements_not_covered_under_KRAs_for_the_year") %>' />--%>
                                                                                    <asp:Label ID="lblMajor" runat="Server" Text='<%# Eval("Major_contributions_Achievements_not_covered_under_KRAs_for_the_year") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRmk" runat="Server" Enabled="false" Text='<%# Eval("REMARKS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Appraiser's comments">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRptRmk" runat="Server" Enabled="false" Text='<%# Eval("Appraisers_comments") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="HOD’s Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHODrmk" runat="Server" Text='<%# Eval("HOD_REMARK") %>' />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="HODRmk" runat="Server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("KEYACC_STATUS") %>' />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="status" runat="Server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="HR Remarks" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtHRRmk" runat="Server" Enabled="false" TextMode="MultiLine" Text='' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <table class="table table-bordered table-striped">
                                                                                <%-- <tr>
                                                                                <td>Sr.No. </td>
                                                                                <td>List down the training programs attended during the year: </td>
                                                                                <td>Elaborate on how exposure to these programs has improved effectiveness in your work area:" </td>
                                                                                <td>Appraisers comments: </td>
                                                                                <td>HR Remarks: </td>

                                                                            </tr>--%>
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
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="modal-footer justify-content-between">
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save changes" CssClass="btn btn-success" />
                                                </div>
                                            </div>
                                            <!-- /.modal-content -->
                                        </div>
                                        <!-- /.modal-dialog -->
                                    </div>
                                    <%--<div class="modal-footer justify-content-between" style="text-align: center;">
                                <asp:Button ID="btnPrev" runat="server" OnClick="btnPrev_Click" OnClientClick="return ConPrev();" Text="Previous" CssClass="btn btn-info" />
                                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" OnClientClick="return ConNext();" Text="Next" CssClass="btn btn-info" />
                            </div>--%>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
