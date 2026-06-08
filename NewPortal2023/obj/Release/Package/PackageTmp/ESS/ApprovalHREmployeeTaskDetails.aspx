<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="ApprovalHREmployeeTaskDetails.aspx.cs" Inherits="NewPortal2023.ESS.ApprovalHREmployeeTaskDetails" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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

    <script>
        $(function () {
            $("#gvLIstVIew").DataTable({
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
            if (confirm("Do you want to submit your action?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <%-- <script>
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

    </script>--%>

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
                                    <h3 style="color: white">INDIVIDUAL PROMISE Approval</h3>
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

                                                                <div id="divKRAUploadRM" runat="server" class="form-group row" visible="false">
                                                                    <div class="col-12" style="margin-left: 10px">
                                                                        <asp:Label ID="Label1" runat="server" Text="Upload Bulk KRA Approval" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                                        <br />
                                                                        &nbsp;<asp:LinkButton ID="btnSampleKRARM" runat="server" Style="color: blue; font-weight: bold" OnClick="btnSampleKRA_Click" AutoPostBack="true">Download KRA Template</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-12">
                                                                        <div class="form-group">
                                                                            <label for="ContentPlaceHolder1_fupldDocument" class="col-sm-2 labels">Select File to upload</label>
                                                                            <div class="col-sm-4">
                                                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn fileinput-button" />
                                                                            </div>
                                                                            <div class="col-sm-6" style="text-align: left; margin-left: -70px">
                                                                                <asp:Button ID="btnUploadRM" runat="server" CssClass="btn btn-info" OnClick="btnUploadRM_Click" Text="Upload" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div id="divKRAUploadHR" runat="server" class="form-group row" visible="false">
                                                                    <div class="col-12" style="margin-left: 10px">
                                                                        <asp:Label ID="lblKRATitle" runat="server" Text="Upload Bulk KRA (All Employees)" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                                        <br />
                                                                        &nbsp;<asp:LinkButton ID="btnSampleKRAHR" runat="server" Style="color: blue; font-weight: bold" OnClick="btnSampleKRA_Click" AutoPostBack="true">Download KRA Template</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-12">
                                                                        <div class="form-group">
                                                                            <label for="ContentPlaceHolder1_fupldDocument" class="col-sm-2 labels">Select File to upload</label>
                                                                            <div class="col-sm-4">
                                                                                <asp:FileUpload ID="fupldDocument" runat="server" CssClass="btn fileinput-button" />
                                                                            </div>
                                                                            <div class="col-sm-6" style="text-align: left; margin-left: -70px">
                                                                                <asp:Button ID="btnUploadHR" runat="server" CssClass="btn btn-info" OnClick="btnUploadHR_Click" Text="Upload" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                &emsp;

                                                    <div class="form-group row">
                                                        <div class="col-12">
                                                            <h3 class="card-title">Employee List</h3>

                                                            &nbsp;<asp:LinkButton ID="lnkEmpList" runat="server" Visible="false" Style="color: blue; font-weight: bold" OnClick="lnkEmpList_Click" AutoPostBack="true">Download All Employee's KRA</asp:LinkButton>
                                                        </div>

                                                        <div class="col-12">

                                                            <%-- <asp:GridView ID="gvLIstVIew" runat="server" OnRowUpdating="gvLIstVIew_RowUpdating" AutoGenerateColumns="False"
                                                                CssClass="display table table-bordered table-striped dynamic-table">--%>
                                                            <asp:GridView ID="gvLIstVIew" runat="server" AutoGenerateColumns="false"
                                                                PageSize="100" AllowPaging="true"
                                                                HeaderStyle-BackColor="LightSteelBlue " OnPreRender="gvDataPointList_PreRender" class="table table-bordered table-striped"
                                                                OnRowUpdating="gvLIstVIew_RowUpdating">
                                                                <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                                <PagerStyle CssClass="pagination-ys" />
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
                                                                <asp:PostBackTrigger ControlID="btnSampleKRAHR" />
                                                                <asp:PostBackTrigger ControlID="btnSampleKRARM" />
                                                                <asp:PostBackTrigger ControlID="btnUploadRM" />
                                                                <asp:PostBackTrigger ControlID="btnUploadHR" />
                                                                <asp:PostBackTrigger ControlID="lnkEmpList" />
                                                                <asp:PostBackTrigger ControlID="drpFinancialYear" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>

                                        <asp:View ID="vwList" runat="server">
                                            <div class="adv-table" id="accordionTwo" runat="server">

                                                <div id="divAlertList" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                    <button data-dismiss="alert" class="close close-sm" type="button">
                                                        <i class="fa fa-times"></i>
                                                    </button>
                                                    <asp:Label ID="lblMessageList" runat="server"></asp:Label>
                                                </div>

                                                <div style="text-align: left;">
                                                    <asp:Button ID="btnList" runat="server" OnClick="btnList_Click" OnClientClick="showLoader();" Text=" ← Back" AutoPostBack="true" CssClass="btn btn-secondary" />
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
                                                                                            <asp:TextBox ID="txtMetric" runat="Server" TextMode="MultiLine" ReadOnly="false" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("METRIC") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Target">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtTarget" runat="Server" TextMode="MultiLine" ReadOnly="false" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("TARGET") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="% wt" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtwt" runat="Server" TextMode="MultiLine" ReadOnly="false" CssClass="form-control input-sm datepicker" Text='<%# Eval("TWT") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Weightage">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtWeightage" runat="Server" TextMode="MultiLine" ReadOnly="false" CssClass="form-control input-sm datepicker" Text='<%# Eval("Weightage") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Mid Year Review - Progess Made By Employee" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtMidYrReview" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("MIDYEARPROGRESS") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Mid Year Review Remarks By RM">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtMidYrReviewRM" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("MIDYEARPROGRESSREVIEWRM") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Achieved Weightage %">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtAchivePer" runat="Server" TextMode="MultiLine" AutoPostBack="true" CssClass="form-control input-sm datepicker" Text='<%# Eval("AchievedWeightage_Perct") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Ach/ Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtRemarks" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("Achievement_REMARKS") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Calculation" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtCalculation" runat="Server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control input-sm datepicker" Text='<%# Eval("Calculation") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Score" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScore" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="form-control input-sm datepicker" Text='<%# Eval("Achievement_Source") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Appraiser’s Comments">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtModiMDRecom" runat="Server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("Modified_Score_MDs_Recommendation") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Appraiser’s Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtRemark" runat="Server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("RPT_REMARK") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="HOD’s Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtHODRemark" runat="Server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("HOD_REMARK") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField >
                                                                                        <HeaderTemplate>
                                                                                            HR Remarks <span style="color: red">*</span>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtHRRemark" runat="Server" TextMode="MultiLine" CssClass="form-control metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("HR_REMARK") %>' />
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
                                                                        <div id="divAction" runat="server" class="row">
                                                                            <div class="modal-footer justify-content-between">
                                                                                <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" Text="Save changes" CssClass="btn btn-success" AutoPostBack="true" OnClientClick="Confirm(); showLoader();" />
                                                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" OnClick="btnClose_Click" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnFinancials" />
                                                                        <asp:PostBackTrigger ControlID="btnCompetitiveness" />
                                                                        <asp:PostBackTrigger ControlID="btnOperationalExcellence" />
                                                                        <asp:PostBackTrigger ControlID="btnPeople" />
                                                                        <asp:PostBackTrigger ControlID="btnFutureReadiness" />
                                                                        <asp:PostBackTrigger ControlID="BtnSave" />
                                                                        <asp:PostBackTrigger ControlID="btnClose" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>

                                                            <hr />

                                                            <div class="form-group row">
                                                                <div class="col-12">
                                                                    <h3 class="card-title">Individual Promise Approval Summary</h3>
                                                                </div>
                                                                <div class="col-12 flex-grow-1" style="overflow-x: scroll; width: 100%">
                                                                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound" ShowFooter="true" CssClass="table table-bordered table-striped" HtmlEncode="false">
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="one" runat="Server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Appraisal_AID" Visible="false">
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
                                                                                    <asp:TextBox ID="txtMetric" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("METRIC") %>'></asp:TextBox>
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
                                                                            <asp:TemplateField HeaderText="Mid Year Review - Progess Made By Employee">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMidYrReview" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("MIDYEARPROGRESS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Mid Year Review Remarks By RM">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMidYrReviewRM" runat="Server" TextMode="MultiLine" ReadOnly="true" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("MIDYEARPROGRESSREVIEWRM") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Achieved Weightage %">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAchivedWeightage" runat="Server" Text='<%# Eval("AchievedWeightage_Perct") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Achievement Against Weightage">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAchive" runat="Server" Text='<%# Eval("Achievement_Perct") %>' />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotalAchive" runat="Server" />
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
                                                                            <asp:TemplateField HeaderText="Ach Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="lblAchiveRemark" runat="Server" TextMode="MultiLine" Text='<%# Eval("Achievement_REMARKS") %>' Style="border: 0px;" ReadOnly="true"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Appraiser’s Comments">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblApprComm" runat="Server" TextMode="MultiLine" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("Modified_Score_MDs_Recommendation") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Appraiser’s Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRemarks" runat="Server" TextMode="MultiLine" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("RPT_REMARK") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="HOD's Action">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHODAction" runat="Server" Text='<%# Eval("HOD_ACTION") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="HOD's Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHODRemarks" runat="Server" TextMode="MultiLine" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("HOD_REMARK") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="HR's Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHRRemarks" runat="Server" TextMode="MultiLine" CssClass="metric-col" Style="border: 0px; overflow: hidden; resize: none;" Text='<%# Eval("HR_REMARK") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="KRA Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("KRA_STATUS") %>' />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="status" runat="Server" />
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

                                                                <div class="modal-footer justify-content-between" style="text-align: center;" runat="server" visible="false">
                                                                    <asp:Button ID="btnSubmitDetails" runat="server" OnClick="btnSubmitDetails_Click" OnClientClick="Confirm(); showLoader();" Text="Submit Details" AutoPostBack="true" CssClass="btn btn-warning" />
                                                                </div>

                                                            </div>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="BtnSave" />
                                                            <asp:PostBackTrigger ControlID="btnClose" />
                                                            <asp:PostBackTrigger ControlID="btnList" />
                                                            <asp:PostBackTrigger ControlID="btnSubmitDetails" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>

                                </div>

                                <div>
                                    <rsweb:ReportViewer ID="rptPrint" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                        interactivedeviceinfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                                        BackColor="#CCCCFF" Height="600px" Width="100%" ZoomMode="PageWidth" PageCountMode="Actual">
                                    </rsweb:ReportViewer>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
    <script>
        var handleDataTableButtons = function () {
            "use strict";
            0 !== $('#<%= gvLIstVIew.ClientID %>').length &&
                $('#<%= gvLIstVIew.ClientID %>').DataTable({
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
            $('#<%= gvLIstVIew.ClientID %>').dataTable();
        });
        TableManageButtons.init();
    </script>
    l
</asp:Content>
