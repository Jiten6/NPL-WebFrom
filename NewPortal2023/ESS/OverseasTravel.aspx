<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="OverseasTravel.aspx.cs" Inherits="NewPortal2023.ESS.OverseasTravel" %>

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
            $('[id*=txtToDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",

                //autoclose: true,
                //endDate: currentDate // Disable dates after the current date
            });
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
                            <h3 style="color: white">Overseas Travel</h3>
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

                                                        <div class="row">
                                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" AutoPostBack="true" OnClientClick="showLoader();" />
                                                        </div>

                                                        <%--<div class="col-12">
                                                            <h3 class="card-title">List</h3>
                                                        </div>--%>

                                                        <br />
                                                        <br />
                                                        <label style="font-size: 30px;">List Of Overseas Claim</label>
                                                        <br />

                                                        <div class="col-12">
                                                            <asp:GridView ID="gvOvrseasClaim" runat="server" OnRowDataBound="gvOvrseasClaim_RowDataBound" ToolTip="OVERSEAS CLAIM LIST" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                <Columns>
                                                                    <%--<asp:TemplateField ShowHeader="true">
                                                                        <ItemStyle VerticalAlign="Middle" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Sr.No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSrNo" runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Claim No">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkTROVRClmNoClmNo" CssClass="form-group" Style="width: 100%;" TextMode="MultiLine" runat="Server" Text='<%# Eval("App_AId") %>' ForeColor="Blue"
                                                                                OnClick="lnkTROVRClmNoClmNo_Click" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Claim Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtClmDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("CreateDate","{0: dd-MMM-yyyy hh:mm tt }") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Purpose Of Visit">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtPrpOfVst" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("PurposeOfVisit") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Visit Place">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtVisitplc" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("VisitPlace") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--      <asp:TemplateField HeaderText="Travelled To">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtToDest" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EndDestination") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <%--    <asp:TemplateField HeaderText="Claim Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtClmAmt" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("TotalAmt") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <%--  <asp:TemplateField HeaderText="Approved Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtAppAmtr" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ApprovedAmmount") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                    <asp:TemplateField HeaderText="Claim Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRemark" runat="Server" CssClass="form-group" Style="width: 100%;" Enabled="false" Text='<%# Eval("Claim_Status") %>' />
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
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnAddNew" />
                                                    <asp:PostBackTrigger ControlID="gvOvrseasClaim" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                </asp:View>

                                <asp:View ID="vwCreate" runat="server">
                                    <div class="adv-table">
                                        <div id="divAlertCreate" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessageCreate" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="card-body" style="margin: 20px">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-horizontal">
                                                        <div class="form-group row">

                                                            <asp:Label ID="lblDtreq" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Date Of Requisition :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtDtreq" runat="server" class="form-control datepicker" placeholder="Date"></asp:TextBox>
                                                            </div>

                                                            <asp:Label ID="lblPurpose" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Purpose Of Visit :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox CssClass="form-control" TextMode="MultiLine" ID="txtPurpose" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                        <div class="form-group row">

                                                            <asp:Label ID="lblPlcvst" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Place Of Visit :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox class="form-control datepicker-dropdown menu-open" ID="txtPlcvst" runat="server"></asp:TextBox>
                                                            </div>

                                                            <asp:Label ID="lblRecHOD" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Recommended by (H.O.D) :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox class="form-control datepicker-dropdown menu-open" ID="txtRecHOD" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                        <div class="form-group row">

                                                            <asp:Label ID="lblDod" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Date Of Departure From India :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtFromDate" runat="server" class="form-control datepicker" placeholder="Date"></asp:TextBox>
                                                            </div>

                                                            <asp:Label ID="lblDoa" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Date Of Arrival To India :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtToDate" runat="server" class="form-control datepicker" placeholder="Date"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                        <div class="form-group row">

                                                            <asp:Label ID="Label1" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Date of Landing the country to be visited :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="TextBox1" runat="server" class="form-control datepicker" placeholder="Date"></asp:TextBox>
                                                            </div>

                                                            <asp:Label ID="Label2" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Date of Departure from the country to arrive India :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="TextBox2" runat="server" class="form-control datepicker" placeholder="Date"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <br />
                                                        <hr />
                                                        <br />
                                                    </div>



                                                    <div class="form-horizontal">
                                                        <div class="form-group row">
                                                            <asp:Label ID="Label7" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Travel Type :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="">[Select Type]</asp:ListItem>
                                                                    <asp:ListItem Value="Travel" Text="Travel"></asp:ListItem>
                                                                    <%--<asp:ListItem Value="Entertainment" Text="Entertainment"></asp:ListItem>
                                                                    <asp:ListItem Value="Travel + Entertainment" Text="Travel + Entertainment"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <asp:Label ID="Label3" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Class Of Travel :-</asp:Label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="drpClassTravel" runat="server" AutoPostBack="true" Style="cursor: pointer"
                                                                    CssClass="form-control" OnSelectedIndexChanged="drpClassTravel_SelectedIndexChanged">
                                                                    <asp:ListItem Value="">[Select Class]</asp:ListItem>
                                                                    <%--<asp:ListItem Value="Air (Business Class)" Text="Air (Business Class)"></asp:ListItem>--%>
                                                                    <%--<asp:ListItem Value="Air (Economy Class)" Text="Air (Economy Class)"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <br />
                                                    </div>



                                                    <div id="divAirBuisn" runat="server" visible="false" class="form-group row">
                                                        <asp:Label ID="Label17" runat="server" class="col-sm-3" Style="font-weight: bold">Air (Business Class) :-</asp:Label>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox class="form-control col-sm-3" ID="txtchk1" AutoCompleteType="Disabled" placeholder="Amount" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtchk1"
                                                                ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                ErrorMessage="Please enter a valid numeric value."
                                                                Display="Dynamic" ForeColor="Red" />
                                                        </div>
                                                    </div>

                                                    <div id="divAirEco" runat="server" visible="false" class="form-group row">
                                                        <asp:Label ID="Label18" runat="server" class="col-sm-3" Style="font-weight: bold">Air (Economy Class) :-</asp:Label>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox class="form-control col-sm-3" ID="TextBox11" AutoCompleteType="Disabled" placeholder="Amount" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox11"
                                                                ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                ErrorMessage="Please enter a valid numeric value."
                                                                Display="Dynamic" ForeColor="Red" />
                                                        </div>
                                                        <hr />

                                                        <br />
                                                    </div>




                                                    <div id="dvgroup" runat="server" visible="false" class="form-group">
                                                        <label id="lblgroup" runat="server" style="font-size: 20px; margin-left:-10px"><u>Daily Reimbursement Amount in Rs. (Per day)</u></label>
                                                        <br />
                                                        <br />
                                                        <div class="col-sm-12">
                                                            <label style="display: inline-flex; align-items: center; font-size: 16px; cursor: pointer; color: cornflowerblue;">
                                                                <asp:RadioButton GroupName="GroupType" ID="rdbSAAR" runat="server" Style="width: 20px; height: 20px; margin-right: 5px;" AutoPostBack="true" OnCheckedChanged="rdbSAAR_CheckedChanged" />
                                                                SAARC Countries
                                                            </label>
                                                            &emsp;
                                                        <label style="display: inline-flex; align-items: center; font-size: 16px; cursor: pointer; color: cornflowerblue;">
                                                            <asp:RadioButton GroupName="GroupType" ID="rdbEastCon" runat="server" Style="width: 20px; height: 20px; margin-right: 5px;" AutoPostBack="true" OnCheckedChanged="rdbSAAR_CheckedChanged" />
                                                            Eastern Countries
                                                        </label>
                                                            &emsp;
                                                         <label style="display: inline-flex; align-items: center; font-size: 16px; cursor: pointer; color: cornflowerblue;">
                                                             <asp:RadioButton GroupName="GroupType" ID="rdbOtherCon" runat="server" Style="width: 20px; height: 20px; margin-right: 5px;" AutoPostBack="true" OnCheckedChanged="rdbSAAR_CheckedChanged" />
                                                             Other then Eastern Countries
                                                         </label>
                                                        </div>
                                                    </div>

                                                    <div id="divDailyReimb" runat="server" class="form-group">
                                                        <div class="row" id="divSAAR" runat="server" visible="false">
                                                            <div class="col-sm-12">
                                                                <label>
                                                                    SAARC Countries
                                                                </label>

                                                                <asp:GridView ID="gvSAAR" runat="server" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="All Allowances">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAllwncDesc" CssClass="form-group" runat="Server" Text='<%# Eval("ALLOWANCE_DESC") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount (Rate per Day in US $)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAllwncAmt" CssClass="form-group" runat="Server" Text='<%# Eval("ALLOWANCE_AMT") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                    </EmptyDataTemplate>

                                                                </asp:GridView>

                                                            </div>
                                                            <br />
                                                        </div>

                                                        <div class="row" id="divEastern" runat="server" visible="false">
                                                            <div class="col-sm-12">
                                                                <label>
                                                                    Eastern Countries
                                                                </label>

                                                                <asp:GridView ID="gvEasternCon" runat="server" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="All Allowances">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAllwncDesc" CssClass="form-group" runat="Server" Text='<%# Eval("ALLOWANCE_DESC") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount (Rate per Day in US $)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAllwncAmt" CssClass="form-group" runat="Server" Text='<%# Eval("ALLOWANCE_AMT") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                    </EmptyDataTemplate>

                                                                </asp:GridView>

                                                            </div>
                                                            <br />
                                                        </div>



                                                        <div class="row" id="divOtherCountries" runat="server" visible="false">
                                                            <div class="col-sm-12">
                                                                <label>
                                                                    Other countries
                                                                </label>

                                                                <asp:GridView ID="gvOtherCon" runat="server" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="All Allowances">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAllwncDesc" CssClass="form-group" runat="Server" Text='<%# Eval("ALLOWANCE_DESC") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount (Rate per Day in US $)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAllwncAmt" CssClass="form-group" runat="Server" Text='<%# Eval("ALLOWANCE_AMT") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                    </EmptyDataTemplate>

                                                                </asp:GridView>

                                                            </div>
                                                            <br />
                                                            <br />
                                                        </div>



                                                        <div class="form-group row" id="divAmt" runat="server" visible="false">
                                                            <asp:Label ID="Label4" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">No of Days :-</asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="txtNoDays" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                            &emsp;&emsp;
                                                                    <asp:Label ID="Label5" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Eligibility Amount :-</asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="txtligibility" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>

                                                            <br />

                                                        </div>

                                                        <div class="form-group row" id="divEligi" runat="server" visible="false">
                                                            <asp:Label ID="Label10" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Advnace Claim Amount :-</asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="txtTravelAmt" AutoCompleteType="Disabled" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <%--OnTextChanged="txtTravelAmt_TextChanged"--%>
                                                                <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="txtTravelAmt"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>
                                                            &emsp;&emsp;
                                                                    <asp:Label ID="Label6" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Remarks :-</asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtUserTravelRemarks" runat="server"></asp:TextBox>
                                                            </div>

                                                            <br />

                                                        </div>

                                                        <div class="row" id="divUpload" runat="server" visible="false">
                                                            <div class="col-lg-6" id="divfileUpload" runat="server" visible="false">
                                                                <div class="btn-group w-100">
                                                                    <asp:FileUpload ID="fupldDocument" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                    <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6" id="divfileDisplay" runat="server" visible="false" style="font-weight: bold">
                                                                Supporting files:
                                                            <asp:GridView ID="gvDomesticFile" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFileStorage" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBtnOpenFiles" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                AutoPostBack="true" OnClick="lnkBtnOpenFile_Click" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divEntertainment" runat="server" class="form-group" visible="false">
                                                        <br />
                                                            <label style="font-size: 20px; margin-left:-10px""><u>Entertainment</u></label>
                                                            <br />
                                                            <br />
                                                        <div class="col-sm-12">
                                                           
                                                            <asp:GridView ID="gvEnt" runat="server" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Allowances">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAllwncDesc" CssClass="form-group" runat="Server" Text='<%# Eval("ENTERTAINMENT_ALLOWANCE") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount (Rate per Day in US $)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAllwncAmt" CssClass="form-group" runat="Server" Text='<%# Eval("ENTERTAINMENT_ALLW_AMT") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                </EmptyDataTemplate>

                                                            </asp:GridView>
                                                            <br />
                                                            <br />
                                                        </div>


                                                        <div class="form-group row" id="div1" runat="server" visible="false">
                                                            <div class="col-sm-12">
                                                                <asp:Label ID="Label8" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">No of Days :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="TextBox3" Enabled="false" Width="100%" runat="server"></asp:TextBox>
                                                                </div>
                                                                &emsp;&emsp;
                                                                    <asp:Label ID="Label9" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Eligibility Amount :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="TextBox4" Enabled="false" Width="100%" runat="server"></asp:TextBox>
                                                                </div>

                                                                <br />

                                                            </div>
                                                        </div>

                                                        <div class="form-group row" id="div2" runat="server" visible="false">
                                                            <div class="col-sm-12">
                                                                <asp:Label ID="Label11" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Advnace Claim Amount :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ID="TextBox5" AutoCompleteType="Disabled" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                    <%--OnTextChanged="txtTravelAmt_TextChanged"--%>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTravelAmt"
                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                        ErrorMessage="Please enter a valid numeric value."
                                                                        Display="Dynamic" ForeColor="Red" />
                                                                </div>
                                                                &emsp;&emsp;
                                                                    <asp:Label ID="Label12" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Remarks :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" TextMode="MultiLine" ID="TextBox6" runat="server"></asp:TextBox>
                                                                </div>

                                                                <br />

                                                            </div>
                                                        </div>

                                                        <div class="row" id="div3" runat="server" visible="false">
                                                            <div class="col-lg-6" id="div4" runat="server" visible="false">
                                                                <div class="btn-group w-100">
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                    <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6" id="div5" runat="server" visible="false" style="font-weight: bold">
                                                                Supporting files:
                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFileStorage" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBtnOpenFiles" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                AutoPostBack="true" OnClick="lnkBtnOpenFile_Click" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <hr />

                                                        <br />
                                                    </div>


                                                    <div id="divWardrobe" class="form-group" runat="server" visible="false">
                                                         <br />
                                                            <label style="font-size: 20px; margin-left:-10px"><u>Wardrobe Allowance</u></label>
                                                            <br />
                                                            <br />
                                                        <div class="col-sm-12">
                                                           
                                                            <asp:GridView ID="gvWardrobe" runat="server" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Allowances">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAllwncDesc" CssClass="form-group" runat="Server" Text='<%# Eval("WARDROBE_ALLOWANCE") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount (Rate per Day in US $)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAllwncAmt" CssClass="form-group" runat="Server" Text='<%# Eval("WARDEOBE_ALLW_AMT") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                </EmptyDataTemplate>

                                                            </asp:GridView>
                                                            <br />
                                                            <br />
                                                        </div>





                                                        <div class="form-group row" id="div9" runat="server" visible="false">
                                                            <asp:Label ID="Label13" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">No of Days :-</asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="TextBox7" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                            &emsp;&emsp;
                                                                    <asp:Label ID="Label14" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Eligibility Amount :-</asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="TextBox8" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>

                                                            <br />
                                                            <br />
                                                        </div>



                                                        <div class="form-group row" id="div10" runat="server" visible="false">
                                                            <asp:Label ID="Label15" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Advnace Claim Amount :-</asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="TextBox9" AutoCompleteType="Disabled" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <%--OnTextChanged="txtTravelAmt_TextChanged"--%>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTravelAmt"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>
                                                            &emsp;&emsp;
                                                                    <asp:Label ID="Label16" runat="server" class="col-sm-3 col-form-label" Style="font-weight: bold">Remarks :-</asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" TextMode="MultiLine" ID="TextBox10" runat="server"></asp:TextBox>
                                                            </div>

                                                            <br />

                                                        </div>

                                                        <div class="form-group row" id="div11" runat="server" visible="false">
                                                            <div class="col-lg-6" id="div12" runat="server" visible="false">
                                                                <div class="btn-group w-100">
                                                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                    <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6" id="div13" runat="server" visible="false" style="font-weight: bold">
                                                                Supporting files:
                                                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFileStorage" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBtnOpenFiles" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                AutoPostBack="true" OnClick="lnkBtnOpenFile_Click" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </div>

                                                    <br />


                                                    <div class="modal-footer justify-content-between" style="text-align: center">
                                                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" CssClass="btn btn-danger" AutoPostBack="true" OnClientClick="showLoader();" />
                                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-success" AutoPostBack="true" OnClientClick="showLoader();" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnClose" />
                                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                                    <asp:PostBackTrigger ControlID="rdbSAAR" />
                                                    <asp:PostBackTrigger ControlID="rdbEastCon" />
                                                    <asp:PostBackTrigger ControlID="rdbOtherCon" />
                                                    <asp:PostBackTrigger ControlID="drpType" />
                                                    <asp:PostBackTrigger ControlID="drpClassTravel" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>

                        </div>

                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
