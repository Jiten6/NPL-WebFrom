<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ApprovalOverallRatingAdmin.aspx.cs" Inherits="NewPortal2023.ESS.ApprovalOverallRatingAdmin" %>

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

    <script type="text/javascript">
        function validateNumericInput(input) {
            // Allow only digits and one decimal point
            let value = input.value;

            // Remove all characters except digits and dots
            value = value.replace(/[^0-9.]/g, '');

            // Allow only one decimal point
            let parts = value.split('.');
            if (parts.length > 2) {
                value = parts[0] + '.' + parts.slice(1).join('');
            }

            input.value = value;
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
                                    <h3 style="color: white">Overall Rating Approval</h3>
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

                                                                &emsp;

                                                    <div id="divEmpList" runat="server" class="form-group row" visible="false">
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

                                                                &emsp;

                                                    <div class="form-group row">
                                                        <div class="col-12">
                                                            <h3 class="card-title">Overall Rating Summary</h3>
                                                        </div>
                                                        <div class="col-12 flex-grow-1" style="overflow-x: scroll; width: 100%">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False"
                                                                        CssClass="display table table-bordered table-striped dynamic-table">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Emp_Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblArea" runat="Server" Font-Bold="true" Text='<%# Eval("EMP_MID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Employee Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" runat="Server" Font-Bold="true" Text='<%# Eval("EMP_NAME") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblType" runat="Server" Font-Bold="true" Text='<%# Eval("TYPE") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total Weightage">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalWtg" runat="Server" Font-Bold="true" Text='<%# Eval("Weightage") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="KRA Score">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalAchPerc" runat="Server" Font-Bold="true" Text='<%# Eval("Achievement_Per") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Indivdiual KRA Achievement %">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAchKRA" runat="Server" Font-Bold="true" Text='<%# Eval("Individual_Ach") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Additional Achievement %">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAchKRAAdd" runat="Server" Font-Bold="true" Text='<%# Eval("ADDPERCENTAGE") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Indivdiual KRA Achievement % After Addition">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAchKRAAdd" runat="Server" Font-Bold="true" Text='<%# Eval("INDIVIDUAL_ACHADD") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="KRA Rating">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRatingKRA" runat="Server" Font-Bold="true" Text='<%# Eval("KRARating") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Leadership Behaviour">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLedBeh" runat="Server" Font-Bold="true" Text='<%# Eval("Leadership_Behav") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Overall Rating">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIndAch" runat="Server" Font-Bold="true" Text='<%# Eval("Overall_Rating") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Promotion">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPromotion" runat="Server" Font-Bold="true" Text='<%# Eval("Promotion") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Reasoning">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtPromotionRmk" runat="Server" Font-Bold="true" TextMode="MultiLine" Text='<%# Eval("Reasoning") %>' Style="border: 0px;" ReadOnly="true" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Old Designation">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOldDesg" runat="Server" Font-Bold="true" Text='<%# Eval("OLD_DESG") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="New Designation">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNewDesg" runat="Server" Font-Bold="true" Text='<%# Eval("NEW_DESG") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="HR Action">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtHODAction" runat="Server" Font-Bold="true" Text='<%# Eval("HR_ACTION") %>' Style="border: 0px;" ReadOnly="true" />
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
                                                                    <asp:PostBackTrigger ControlID="gvList" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="gvLIstVIew" />

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
                                                                <div class="col-12 flex-grow-1" style="overflow-x: scroll; width: 100%">
                                                                    <asp:UpdatePanel ID="updListAdd" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="gvList" AutoGenerateColumns="False" runat="server"
                                                                                PageSize="100" AllowPaging="true"
                                                                                HeaderStyle-BackColor="LightSteelBlue " OnPreRender="gvDataPointList_PreRender" class="table table-bordered table-striped"
                                                                                OnPageIndexChanging="gvList_PageIndexChanging" OnRowUpdating="gvList_RowUpdating" OnRowCancelingEdit="gvList_RowCancelingEdit"
                                                                                OnRowDataBound="gvList_RowDataBound" OnRowEditing="gvList_RowEditing">
                                                                                <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                                                <PagerStyle CssClass="pagination-ys" />
                                                                                <Columns>
                                                                                    <asp:TemplateField ShowHeader="true" Visible="false">
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
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblArea" runat="Server" Text="Individual" Font-Bold="true" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Total Weightage">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTotalWtg" runat="Server" Font-Bold="true" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="KRA Score">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTotalScore" runat="Server" Font-Bold="true" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Total Achievement Percentage" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTotalAchPerc" runat="Server" Font-Bold="true" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Indivdiual KRA Achievement %">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIndAch" runat="Server" Font-Bold="true" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Additional Achievement %">
                                                                                        <ItemTemplate>
                                                                                            <div style="display: flex; gap: 5px; align-items: center;">
                                                                                                <asp:TextBox ID="txtExtraScore" runat="server" AutoPostBack="true" Text="0" CssClass="form-control input-sm" Style="flex-grow: 1;" oninput="validateNumericInput(this)" />
                                                                                                <asp:Button ID="btnAddExtraScore" runat="server" Text="Add" CssClass="btn btn-warning btn-sm" OnClick="btnAddExtraScore_Click" Style="white-space: nowrap;" />
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Indivdiual KRA Achievement % After Addition">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIndAchAdd" Text="0" runat="Server" Font-Bold="true" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="true" HeaderText="KRA Rating(HOD)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="drpKRARating" runat="server" CssClass="form-control input-sm-3" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="drpKRARating_SelectedIndexChanged" onchange="showLoader();">
                                                                                                <asp:ListItem Value="">[Select KRA Rating]</asp:ListItem>
                                                                                                <asp:ListItem Value="Needs Improvement" Text="Needs Improvement"></asp:ListItem>
                                                                                                <asp:ListItem Value="Meets Expectations" Text="Meets Expectations"></asp:ListItem>
                                                                                                <asp:ListItem Value="Exceeds Expectations" Text="Exceeds Expectations"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="true" HeaderText="Leadership Behaviour(HOD)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="drpLedBehav" runat="server" CssClass="form-control input-sm-3" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="drpLedBehav_SelectedIndexChanged" onchange="showLoader();">
                                                                                                <asp:ListItem Value="">[Select Leadership Behaviour]</asp:ListItem>
                                                                                                <asp:ListItem Value="Needs Improvement" Text="Needs Improvement"></asp:ListItem>
                                                                                                <asp:ListItem Value="Meets Expectations" Text="Meets Expectations"></asp:ListItem>
                                                                                                <asp:ListItem Value="Exceeds Expectations" Text="Exceeds Expectations"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>


                                                                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkGetScore" Text="Get Score" CssClass="btn btn-warning" runat="server" CommandName="Update" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Overall Rating">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtOverRat" runat="Server" CssClass="form-control input-sm " Enabled="false" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="HOD's Promotion Remark">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtPromotion" runat="Server" CssClass="form-control input-sm " Enabled="false" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField Visible="true" HeaderText="Promotion" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="drpPromotion" runat="server" CssClass="form-control input-sm-3" AutoPostBack="true" Enabled="true"
                                                                                                OnSelectedIndexChanged="drpPromotion_SelectedIndexChanged" onchange="showLoader();">
                                                                                                <asp:ListItem Value="">[Select Promotion Factor]</asp:ListItem>
                                                                                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                                                                <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Reasoning">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtPromotionRmk" runat="Server" CssClass="form-control input-sm " TextMode="MultiLine" Style="border: 0px;" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Historic Rating">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkHisView" runat="server" Text="View" CssClass="btn btn-info" OnClick="lnkHisView_Click"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Last Promotion">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkLastPromView" runat="server" Text="View" CssClass="btn btn-info" OnClick="lnkLastPromView_Click"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblAddCORowId" runat="Server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtaid" runat="Server" CssClass="form-control input-sm " />
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
                                                                            <asp:PostBackTrigger ControlID="gvList" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>

                                                                <div id="divScore" runat="server" visible="false" class="col-12">
                                                                    <asp:GridView ID="gvScore" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvScore_RowDataBound" ShowFooter="true" CssClass="table table-bordered table-striped">
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
                                                                &emsp;
                                                        <div class="modal-footer justify-content-between" id="divAction" runat="server" visible="false">
                                                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                                            <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" Text="Save changes" CssClass="btn btn-success" AutoPostBack="true" OnClientClick="showLoader();" />
                                                        </div>

                                                                <%--<div class="row" id="divDesg" runat="server" visible="false">
                                                                    <label class="col-sm-2" id="lblOldDesg">Old Designation :-</label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtOldDesg" runat="server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    &nbsp;&nbsp;
                                                                    <label class="col-sm-2" id="lblNewDesg">New Designation :-</label>
                                                                    <div class="col-sm-2">
                                                                        <asp:DropDownList ID="drpDesgList" runat="server" OnSelectedIndexChanged="drpDesgList_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    &nbsp;&nbsp;
                                                      
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtOtherDesg" runat="server" Visible="false" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>--%>

                                                                <div class="row align-items-center" id="divDesg" runat="server" visible="false">
                                                                    <div class="col-sm-2">
                                                                        <label id="lblOldDesg">Old Designation :-</label>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox ID="txtOldDesg" runat="server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-sm-2">
                                                                        <label id="lblNewDesg">New Designation :-</label>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <asp:DropDownList ID="drpDesgList" runat="server"
                                                                            OnSelectedIndexChanged="drpDesgList_SelectedIndexChanged"
                                                                            AutoPostBack="true" CssClass="form-control" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtOtherDesg" runat="server" Visible="false"
                                                                            TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>


                                                                &nbsp;&nbsp;

                                                        <div class="row" id="divSubmitAll" runat="server">
                                                            <label class="col-sm-2" id="lblActionAll">Action :-</label>
                                                            <div class="col-sm-2">
                                                                <asp:DropDownList ID="drActionAll" runat="server" CssClass="form-control" AutoPostBack="false">
                                                                    <asp:ListItem Value="">--Select Action Type--</asp:ListItem>
                                                                    <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                    <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            &nbsp;&nbsp;
                                     
                                                        <label class="col-sm-2" id="lblRemarksAll">Remarks :-</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtAllRmk" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="col-sm-1">
                                                                <asp:Button ID="btnSubmitAll" runat="server" CssClass="btn btn-success" OnClick="btnSubmitAll_Click" Text="Submit" AutoPostBack="true" OnClientClick="Confirm(); showLoader();" />
                                                            </div>

                                                        </div>

                                                                <div class="modal-footer justify-content-between" style="text-align: center">
                                                                    <asp:Button ID="btnList" runat="server" OnClick="btnList_Click" Text="View Employee List" CssClass="btn btn-info" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="gvList" />
                                                            <asp:PostBackTrigger ControlID="BtnSave" />
                                                            <asp:PostBackTrigger ControlID="btnList" />
                                                            <asp:PostBackTrigger ControlID="btnSubmitAll" />
                                                            <%--                                                            <asp:PostBackTrigger ControlID="btnSubmitDesg" />--%>
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

            <%-----------------------------------------------------------------Pop up-----------------------------------------------------------%>


            <div id="HistoricPopup" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="gvHistoric" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="center" CellPadding="5"
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                <Columns>
                                    <asp:TemplateField HeaderText="EMPLOYEE CODE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridview-header" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMPLOYEE NAME" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridview-header" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="KRA SCORE" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKRA" runat="Server" Text='<%# Eval("KRA_SCROE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridview-header" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MANAGERIAL COMPETENCY SCORE" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMCS" runat="Server" Text='<%# Eval("MANAGERICAL_COMP_SCROE") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridview-header" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PERFORMANCE RATING" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPR" runat="Server" Text='<%# Eval("PERFORMANCE_RATING") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridview-header" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ACC YEAR" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAY" runat="Server" Text='<%# Eval("ACCOUNTING_YEAR") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridview-header" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                        <div class="modal-footer">

                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <script type="text/javascript">
                function ShowHistoricPopup(title, body) {
                    $("#HistoricPopup .modal-title").html(title);
                    //$("#MyPopup .modal-body").html(body);
                    $("#HistoricPopup").modal("show");
                }
            </script>

            <div id="LastPromotionPopup" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="gvLastPromotion" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="center" CellPadding="5"
                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                <Columns>
                                    <asp:TemplateField HeaderText="EMPLOYEE CODE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridview-header" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMPLOYEE NAME" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridview-header" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="LAST PROMOTION GIVEN (YEAR)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrigin" runat="Server" Text='<%# Eval("LAST_PERFORMANCE_YEAR") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridview-header" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                        <div class="modal-footer">

                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <script type="text/javascript">
                function ShowLastPromotionPopup(title, body) {
                    $("#LastPromotionPopup.modal-title").html(title);
                    //$("#MyPopup .modal-body").html(body);
                    $("#LastPromotionPopup").modal("show");
                }
            </script>


        </section>
    </section>
    <script>
        var handleDataTableButtons = function () {
            "use strict";
            0 !== $('#<%= gvList.ClientID %>').length &&
                $('#<%= gvList.ClientID %>').DataTable({
                    dom: "Bfrtip",
                    buttons: [{
                        extend: "copy",
                        className: "btn-sm"
                    }, {
                        extend: "csv",
                        className: "btn-sm"
                    }, {
                        extend: "excel",
                        className: "btn-sm"
                    }, {
                        extend: "pdf",
                        className: "btn-sm"
                    }, {
                        extend: "print",
                        className: "btn-sm"
                    }],
                    responsive: !0
                })
        },
            TableManageButtons = function () {
                "use strict";
                return {
                    init: function () {
                        handleDataTableButtons()
                    }
                }
            }();
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= gvList.ClientID %>').dataTable();
        });
        TableManageButtons.init();
    </script>
</asp:Content>
