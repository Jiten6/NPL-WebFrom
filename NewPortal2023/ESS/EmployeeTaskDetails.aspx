<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="EmployeeTaskDetails.aspx.cs" Inherits="NewPortal2023.ESS.EmployeeTaskDetails" %>

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

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to submit your action?, You won't be able to make changes again.")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script>
        function LinkCompetitivenessClicked() {
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
        }

    </script>

    <style>
        .gridview-cell {
            white-space: pre-line;
        }
    </style>

    <script type="text/javascript">
        function autoResizeColumn(className) {
            var boxes = document.querySelectorAll("." + className);
            if (boxes.length === 0) return;

            let maxWidth = 0;
            let maxHeight = 0;

            // Find max width & height
            boxes.forEach(function (tb) {
                tb.style.width = "auto";
                tb.style.height = "auto";
                if (tb.scrollWidth > maxWidth) maxWidth = tb.scrollWidth;
                if (tb.scrollHeight > maxHeight) maxHeight = tb.scrollHeight;
            });

            // Apply to all
            boxes.forEach(function (tb) {
                tb.style.width = maxWidth + "px";
                tb.style.height = tb.scrollHeight + "px"; // height can still be row specific
            });
        }

        window.onload = function () {
            // For your Metric column textboxes
            autoResizeColumn("metric-col");

            // If you want multiple columns, call again with different class names
        };
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
                                    <h3 style="color: white">Individual Promise</h3>
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

                                                            <div style="text-align: right;">
                                                                <asp:LinkButton ID="lnkManual" runat="server" Style="font-weight: bold;" OnClick="lnkManual_Click" ForeColor="Blue">Download PMS Guidelines</asp:LinkButton>
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
                                                                    <asp:Label ID="lblCycle" runat="server" Text="" CssClass="card-title" Style="font-size: 30px;"></asp:Label>
                                                                </div>

                                                                <div class="col-lg-12">
                                                                    <div class="form-group">
                                                                        <hr />
                                                                    </div>
                                                                </div>

                                                                <div class="col-12" style="margin-left: -5px">
                                                                    <asp:Label ID="Label1" runat="server" Text="Insert Individual KRA" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                                </div>

                                                                <br />

                                                                <div class="col-sm-12 form-group row" style="margin-left: -20px">
                                                                    <asp:Button ID="btnFinancials" runat="server" OnClick="btnFinancials_Click" class="btn btn-warning" Text="Financials" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                    <asp:Button ID="btnCompetitiveness" runat="server" OnClick="btnCompetitiveness_Click" class="btn btn-warning" Text="Competitiveness" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                    <asp:Button ID="btnOperationalExcellence" runat="server" OnClick="btnOperationalExcellence_Click" class="btn btn-warning" Text="Operational Excellence" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                    <asp:Button ID="btnPeople" runat="server" OnClick="btnPeople_Click" class="btn btn-warning" Text="People" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                    <asp:Button ID="btnFutureReadiness" runat="server" OnClick="btnFutureReadiness_Click" class="btn btn-warning" Text="Future Readiness" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                </div>

                                                                <br />
                                                                <br />

                                                                <div id="divSteps" runat="server" class="form-group row">
                                                                    <asp:UpdatePanel ID="updListAdd" runat="server">
                                                                        <ContentTemplate>
                                                                            <div class="form-group col-12 flex-grow-1" style="overflow-x: scroll; width: 100%">
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
                                                                                        <asp:TemplateField HeaderText="Appraisal_AID" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblAid" runat="Server" Text='<%# Eval("Appraisal_AID") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Area" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblArea" runat="Server" Text='<%# Eval("AREA") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Metric">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMetric" runat="Server" CssClass="form-control input-sm" TextMode="MultiLine" Text='<%# Eval("METRIC") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Target">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtTarget" runat="Server" CssClass="form-control input-sm" TextMode="MultiLine" Text='<%# Eval("TARGET") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="% wt" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtwt" runat="Server" CssClass="form-control input-sm datepicker" TextMode="MultiLine" Text='<%# Eval("TWT") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Weightage" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtWeightage" TextMode="Number" runat="Server" CssClass="form-control input-sm" Text='<%# Eval("Weightage") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Mid Year Review - Progess Made" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMidYrReview" runat="Server" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" TextMode="MultiLine" Text='<%# Eval("MIDYEARPROGRESS") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Achieved Weightage %" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtAchPer" TextMode="Number" runat="Server" CssClass="form-control input-sm datepicker" Text='<%# Eval("AchievedWeightage_Perct") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Ach/ Remarks">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtRemarks" runat="Server" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" TextMode="MultiLine" Text='<%# Eval("Achievement_REMARKS") %>' />
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

                                                                                <div id="divAddRow" runat="server">
                                                                                    <asp:LinkButton ID="lnkBtnAddRowSteps" Text="Add Row" runat="server" Font-Bold="true" ForeColor="Blue"
                                                                                        OnClick="lnkBtnAddRowSteps_Click" CssClass="Title" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                                    <asp:Label ID="lblSeperatorSteps" runat="server" Text="|" Font-Bold="true" ForeColor="blue" />
                                                                                    <asp:LinkButton ID="lnkBtnDeleteRowSteps" Text="Delete Selected" runat="server" Font-Bold="true" ForeColor="Blue"
                                                                                        OnClick="lnkBtnDeleteRowSteps_Click" CssClass="Title" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                                </div>
                                                                            </div>
                                                                            <br />

                                                                            <div class="modal-footer justify-content-between">
                                                                                <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" Text="Save changes" CssClass="btn btn-success" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" OnClick="btnClose_Click" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                            </div>


                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="lnkBtnAddRowSteps" />
                                                                            <asp:PostBackTrigger ControlID="lnkBtnDeleteRowSteps" />
                                                                            <asp:PostBackTrigger ControlID="btnClose" />
                                                                            <asp:PostBackTrigger ControlID="BtnSave" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>

                                                                <br />
                                                                <hr>
                                                                <br />

                                                                <div id="divKRAUpload" runat="server" class="form-group row" visible="false">
                                                                    <div class="col-12" style="margin-left: 10px">
                                                                        <asp:Label ID="lblKRATitle" runat="server" Text="Upload Bulk KRA" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                                        <br />
                                                                        &nbsp;<asp:LinkButton ID="btnSampleKRA1" runat="server" Style="color: blue; font-weight: bold" OnClick="btnSampleKRA_Click" AutoPostBack="true">Download KRA Template</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-12">
                                                                        <div class="form-group">
                                                                            <label for="ContentPlaceHolder1_fupldDocument" class="col-sm-2 labels">Select File to upload</label>
                                                                            <div class="col-sm-4">
                                                                                <asp:FileUpload ID="fupldDocument" runat="server" CssClass="btn fileinput-button" />
                                                                            </div>
                                                                            <div class="col-sm-6" style="text-align: left; margin-left: -70px">
                                                                                <img id="loader1" style="display: none; height: 50px; width: 25px; float: right;" src="Assets/progress.gif" />
                                                                                <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary" OnClick="btnInsert_Click" Text="Upload" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <br />
                                                                <hr />

                                                                <div class="form-group row">
                                                                    <div class="col-12">
                                                                        <h3 class="card-title">Individual Promise Summary</h3>
                                                                    </div>

                                                                    <div class="col-12 flex-grow-1" style="overflow-x: scroll; width: 100%">
                                                                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound" ShowFooter="true" CssClass="table table-bordered table-striped" HtmlEncode="false">
                                                                            <Columns>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl" runat="Server" />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="one" runat="Server" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAid" runat="Server" Text='<%# Eval("Appraisal_AID") %>' />
                                                                                    </ItemTemplate>
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
                                                                                        <asp:TextBox ID="lblMetric" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("METRIC") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="three" runat="Server" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Target">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="lblTARGET" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("TARGET") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="FTtarget" runat="Server" Text="Total :-" Font-Bold="true" />
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
                                                                                        <asp:Label ID="lblTotalWeightage" runat="Server" Font-Bold="true" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Mid Year Review - Progess Made">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMidYrReview" runat="Server" Text='<%# Eval("MIDYEARPROGRESS") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Mid Year Review Remarks By RM">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMidYrReviewRM" runat="Server" Text='<%# Eval("MIDYEARPROGRESSREVIEWRM") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Achieved Weightage %">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAchivedWeightage" runat="Server" Text='<%# Eval("AchievedWeightage_Perct") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Achievement Against Weightage" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAchive" runat="Server" Text='<%# Eval("Achievement_Perct") %>' />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblTotalAchive" runat="Server" Visible="false" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Calculation" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCalculation" runat="Server" Text='<%# Eval("Calculation") %>' />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblTotalCalculation" runat="Server" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Score" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblScore" runat="Server" Text='<%# Eval("Achievement_Source") %>' />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblTotalSCORE" runat="Server" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Ach Remarks">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="lblAchiveRemark" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("Achievement_REMARKS") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="FTACCHIV" runat="Server" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Appraiser’s Comments">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblApprComm" runat="Server" TextMode="MultiLine" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("Modified_Score_MDs_Recommendation") %>' />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblAcComment" runat="Server" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="RM Remarks">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRemarks" runat="Server" TextMode="MultiLine" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("RPT_REMARK") %>' />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="FTR" runat="Server" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="HOD Remarks">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblHODrmk" runat="Server" TextMode="MultiLine" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("HOD_REMARK") %>' />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="HODRmk" runat="Server" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="KRA Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("KRA_STATUS") %>' />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="status" runat="Server" />
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Overall Rating Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblORStatus" runat="Server" Text='<%# Eval("OR_STATUS") %>' />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="ORstatus" runat="Server" />
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

                                                                    <div>
                                                                        <br />
                                                                    </div>

                                                                    <div class="modal-footer justify-content-between" style="text-align: center;">
                                                                        <asp:Button ID="btnSubmitDetails" runat="server" OnClick="btnSubmitDetails_Click" OnClientClick="Confirm(); showLoader();" Text="Submit Details" AutoPostBack="true" CssClass="btn btn-warning" />
                                                                        <asp:Button ID="btnRecall" runat="server" Visible="false" OnClick="btnRecall_Click" OnClientClick="Confirm(); showLoader();" Text="Recall" AutoPostBack="true" CssClass="btn btn-warning" />
                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnFinancials" />
                                                            <asp:PostBackTrigger ControlID="btnCompetitiveness" />
                                                            <asp:PostBackTrigger ControlID="btnOperationalExcellence" />
                                                            <asp:PostBackTrigger ControlID="btnPeople" />
                                                            <asp:PostBackTrigger ControlID="btnFutureReadiness" />
                                                            <asp:PostBackTrigger ControlID="btnInsert" />
                                                            <asp:PostBackTrigger ControlID="btnSampleKRA1" />
                                                            <asp:PostBackTrigger ControlID="drpFinancialYear" />
                                                            <asp:PostBackTrigger ControlID="btnSubmitDetails" />
                                                            <asp:PostBackTrigger ControlID="btnRecall" />
                                                            <asp:PostBackTrigger ControlID="lnkManual" />
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
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
