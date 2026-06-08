<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ApprovalTrainingAndDevelopment.aspx.cs" Inherits="NewPortal2023.ESS.ApprovalTrainingAndDevelopment" %>

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
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to submit your action?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
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
                                    <h3 style="color: white">Training And Development Approval</h3>
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
                                                <div class="adv-table">
                                                    <div id="divAlertListView" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                        <button data-dismiss="alert" class="close close-sm" type="button">
                                                            <i class="fa fa-times"></i>
                                                        </button>
                                                        <asp:Label ID="lblMessageListView" runat="server"></asp:Label>
                                                    </div>
                                                    <div id="Sec2" runat="server" class="card-body" style="margin: 20px">
                                                        <div class="row">
                                                            <div class="card-body" style="margin: 20px">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
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
                                                                                                <%--<%# Eval("METRIC") %>--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="View">
                                                                                            <ItemTemplate>
                                                                                                <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" class="btn btn-info" Text="View" AutoPoastBack="true" OnClientClick="showLoader();" />
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
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="gvLIstVIew" />
                                                                        <%--        <asp:PostBackTrigger ControlID="btnClose" />--%>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%-- <div class="modal-footer justify-content-between" style="text-align: center;">
                                            <asp:Button ID="Button1" runat="server" OnClick="btnPrev_Click" OnClientClick="return ConPrev();" Text="Previous" CssClass="btn btn-info" />
                                            <asp:Button ID="Button2" runat="server" OnClick="btnNext_Click" OnClientClick="return ConNext();" Text="Next" CssClass="btn btn-info" />
                                        </div>--%>
                                                </div>
                                            </div>
                                        </asp:View>

                                        <asp:View ID="vwList" runat="server">
                                            <div class="adv-table">
                                                <div id="divAlertList" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                    <button data-dismiss="alert" class="close close-sm" type="button">
                                                        <i class="fa fa-times"></i>
                                                    </button>
                                                    <asp:Label ID="lblMessageList" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-12">
                                                    <h3 class="card-title">
                                                        <asp:Literal ID="EmpName" runat="server"></asp:Literal>
                                                        (<asp:Literal ID="EmpCode" runat="server"></asp:Literal>)
                                                    </h3>
                                                    <hr />
                                                </div>
                                                <div class="card-body" style="margin: 20px">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group row">

                                                                <asp:Button ID="btnFunTech" runat="server" OnClick="btnFunTech_Click" class="btn btn-warning" Text="Functional / Technical Skills" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                <asp:Button ID="btnManBehaSkill" runat="server" OnClick="btnManBehaSkill_Click" class="btn btn-warning" Text="Managerial / Behavioral Skills" AutoPostBack="true" OnClientClick="showLoader();" />

                                                            </div>

                                                            <br />
                                                            <br />

                                                            <div class="form-group row">
                                                                <div class="col-06">
                                                                    <asp:DropDownList ID="drpQuarter" runat="server" Visible="false" CssClass="form-control input-sm-3" AutoPostBack="true" OnSelectedIndexChanged="drpQuarter_SelectedIndexChanged">

                                                                        <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                                                        <asp:ListItem Value="2">Second Quarter</asp:ListItem>
                                                                        <asp:ListItem Value="3">Third Quarter</asp:ListItem>
                                                                        <asp:ListItem Value="4">Fourth Quarter</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div id="divGVSELF" runat="server" class="form-group row ">
                                                                <div class="col-12">
                                                                    <asp:GridView ID="GVSELF" runat="server" ToolTip="Training & Development Plans" AutoGenerateColumns="False" Class="table table-bordered table-striped">
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
                                                                            <asp:TemplateField HeaderText="Identified By Appraisee">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtselfAssM" CssClass="form-group" ReadOnly="true" Style="background-color: #f0f0f0; color: #888; border: 1px solid #ddd; width: 100%;" TextMode="MultiLine" runat="Server" Text='<%# Eval("selfAssM") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRmk" CssClass="form-group" ReadOnly="true" Style="background-color: #f0f0f0; color: #888; border: 1px solid #ddd; width: 100%;" TextMode="MultiLine" runat="Server" Text='<%# Eval("REMARKS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Identified By Appraiser">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAssMAppriser" TextMode="MultiLine" ReadOnly="false" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("AssMAppriser") %>' />
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

                                                                    <%-- <asp:LinkButton ID="linkAddRow" Text="Add Row" runat="server" Font-Bold="true"
                                                                OnClick="linkAddRow_Click" CssClass="Title" ForeColor="blue" AutoPostBack="true" OnClientClick="showLoader();" />
                                                            <asp:Label ID="Separator" runat="server" Text="|" Font-Bold="true" ForeColor="blue" />
                                                            <asp:LinkButton ID="lnkDeleteRows" runat="server" Font-Bold="true" OnClick="lnkDeleteRows_Click"
                                                                Text="Delete Selected" CssClass="Title" ForeColor="blue" AutoPostBack="true" OnClientClick="showLoader();" />--%>
                                                                </div>

                                                                <%--<div class="row">
                                                            <div class="col-sm-10">
                                                            </div>
                                                            <div class="col-sm-1">
                                                               
                                                                <asp:Button ID="BtnSaveChanges" runat="server" OnClick="BtnSaveChanges_Click" Text="Save changes" CssClass="btn btn-success" />
                                                            </div>
                                                        </div>--%>

                                                                <div id="divAction" runat="server" class="row">
                                                                    <div class="modal-footer justify-content-between">
                                                                        <asp:Button ID="BtnSaveChanges" runat="server" OnClick="BtnSaveChanges_Click" Text="Save changes" CssClass="btn btn-success" AutoPostBack="true" OnClientClick="Confirm(); showLoader();" />
                                                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" OnClick="btnClose_Click" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <br />
                                                            <br />

                                                            <div class="form-group row">
                                                                <div class="col-12">
                                                                    <h3 class="card-title">Training And Development Approval Summary</h3>
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

                                                            <div class="modal-footer justify-content-between" style="text-align: center">
                                                                <asp:Button ID="btnList" runat="server" OnClick="btnList_Click" Text="View Employee List" CssClass="btn btn-info" OnClientClick="showLoader();" />
                                                            </div>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="BtnSaveChanges" />
                                                            <asp:PostBackTrigger ControlID="btnClose" />
                                                            <asp:PostBackTrigger ControlID="btnFunTech" />
                                                            <asp:PostBackTrigger ControlID="btnManBehaSkill" />
                                                            <asp:PostBackTrigger ControlID="btnList" />
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
                                <asp:Button ID="btnPrev" runat="server" OnClick="btnPrev_Click" Visible="false" OnClientClick="return ConPrev();" Text="Previous" CssClass="btn btn-info" />
                                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" OnClientClick="return ConNext();" Text="Next" CssClass="btn btn-info ml-auto" />
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
