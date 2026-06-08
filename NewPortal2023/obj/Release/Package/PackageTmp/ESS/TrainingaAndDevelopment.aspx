<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="TrainingaAndDevelopment.aspx.cs" Inherits="NewPortal2023.ESS.TrainingaAndDevelopment" %>

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
                                    <h3 style="color: white">Training And Development</h3>
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
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

                                                                <div class="form-group row">
                                                                    <asp:Button ID="btnFunTech" runat="server" OnClick="btnFunTech_Click" class="btn btn-warning" Text="Functional / Technical Skills" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                    <asp:Button ID="btnManBehaSkill" runat="server" OnClick="btnManBehaSkill_Click" class="btn btn-warning" Text="Managerial / Behavioral Skills" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                </div>

                                                                <div id="divGVSELF" runat="server" class="row">
                                                                    <div class="col-12">
                                                                        <asp:GridView ID="GVSELF" runat="server" ToolTip="Training & Development Plans" AutoGenerateColumns="False" Class="table table-bordered table-striped" OnRowDataBound="GVSELF_RowDataBound1">
                                                                            <Columns>

                                                                                <asp:TemplateField ShowHeader="true">
                                                                                    <ItemStyle VerticalAlign="Middle" />
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%-- <asp:TemplateField HeaderText="Sr.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo"  runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAddCORowId" runat="Server" Text='<%# Eval("Doc_Data_Id") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Identified By Appraisee">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtselfAssM" CssClass="form-group" Style="width: 100%;" TextMode="MultiLine" runat="Server" Text='<%# Eval("selfAssM") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Remarks">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtRmk" CssClass="form-group" Style="width: 100%;" TextMode="MultiLine" runat="Server" Text='<%# Eval("REMARKS") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Identified By Appraiser">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAssMAppriser" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="form-group" Style="background-color: #f0f0f0; color: #888; border: 1px solid #ddd; width: 100%" Text='<%# Eval("AssMAppriser") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="HR Remarks" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtSelfHRRmk" TextMode="MultiLine" ReadOnly="true" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("SelfHRRmk") %>' />
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


                                                                        <div class="form-group" id="divAddRow" runat="server" visible="false">
                                                                            <asp:LinkButton ID="linkAddRow" Text="Add Row" runat="server" Font-Bold="true"
                                                                                OnClick="linkAddRow_Click" CssClass="Title" ForeColor="blue" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                            <asp:Label ID="Separator" runat="server" Text="|" Font-Bold="true" ForeColor="blue" />
                                                                            <asp:LinkButton ID="lnkDeleteRows" runat="server" Font-Bold="true" OnClick="lnkDeleteRows_Click"
                                                                                Text="Delete Selected" CssClass="Title" ForeColor="blue" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        </div>

                                                                    </div>

                                                                    <div class="row" id="divSave" runat="server" visible="false">
                                                                        <div class="col-sm-10">
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                                                            <asp:Button ID="BtnSaveChanges" runat="server" OnClick="BtnSaveChanges_Click" Text="Save changes" CssClass="btn btn-success" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                &emsp;

                                                    <div id="divList" runat="server" class="row">
                                                        <div class="col-12">
                                                            <h3 class="card-title">Training And Development Summary</h3>
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:GridView ID="gvList" runat="server" ToolTip="Training & Development Plans" AutoGenerateColumns="False" Class="table table-bordered table-striped">
                                                                <Columns>

                                                                    <asp:TemplateField ShowHeader="true" Visible="false">
                                                                        <ItemStyle VerticalAlign="Middle" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- <asp:TemplateField HeaderText="Sr.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo"  runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAddCORowId" runat="Server" Text='<%# Eval("Doc_Data_Id") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblType" runat="Server" Enabled="false" Text='<%# Eval("TYPE") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Identified By Appraisee">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblselfAssM" runat="Server" Enabled="false" Text='<%# Eval("selfAssM") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRmk" runat="Server" Enabled="false" Text='<%# Eval("REMARKS") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Identified By Appraiser">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAssMAppriser" runat="Server" Enabled="false" Text='<%# Eval("AssMAppriser") %>' />
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
                                                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("TRAINDEV_STATUS") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="status" runat="Server" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="HR Remarks" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSelfHRRmk" runat="Server" Enabled="false" Text='<%# Eval("SelfHRRmk") %>' />
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
                                                    </div>

                                                            </div>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="linkAddRow" />
                                                            <asp:PostBackTrigger ControlID="lnkDeleteRows" />
                                                            <asp:PostBackTrigger ControlID="btnFunTech" />
                                                            <asp:PostBackTrigger ControlID="btnManBehaSkill" />
                                                            <asp:PostBackTrigger ControlID="BtnSaveChanges" />
                                                            <asp:PostBackTrigger ControlID="drpFinancialYear" />
                                                        </Triggers>
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
                                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" OnClientClick="return ConNext();" Text="Submit" CssClass="btn btn-success" />
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
