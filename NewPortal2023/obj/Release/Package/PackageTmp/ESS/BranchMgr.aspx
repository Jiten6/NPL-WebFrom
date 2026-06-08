<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="BranchMgr.aspx.cs" Inherits="NewPortal2023.ESS.BranchMgr" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="assets/js/dynamic_table_init.js"></script>
    <style type="text/css">
        .labels {
            float: left;
            padding-top: 7px;
            margin-bottom: 0;
            text-align: left;
            font-weight: 300;
            font-size: 15px;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        .underline {
            text-decoration: underline;
        }
    </style>
    <script type="text/javascript">
        function showLoader() {
            document.getElementById("loader").style.display = 'block';
        }

    </script>

    <script type="text/javascript">
        function clearFileInputField(divId) {
            document.getElementById(divId).innerHTML = document.getElementById(tagId).innerHTML;
        }

        function ConfirmDel() {
            return confirm("Are you sure you want to delete this record?");
        }
    </script>
    <script type="text/javascript">
        function CheckNumeric(event) {
            var _key = (window.Event) ? event.which : event.keyCode;

            if ((_key > 95 && _key < 106) || (_key > 47 && _key < 58) || _key == 8 || _key == 9 || _key == 37 || _key == 39 || _key == 190 || _key == 110) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        function ShowPopup(title, body) {
            $("#MyPopup .modal-title").html(title);
            //$("#MyPopup .modal-body").html(body);
            $("#MyPopup").modal("show");
        }
    </script>
    
    <script type="text/javascript">
        function ConfirmDel() {
            return confirm("Are you sure you want to delete this record?");
        }

        //        function ValidateMaintenance(drp) {
        //            var val = drp.value;
        //            if (val == "259") {
        //                return true;
        //            }
        //            else {
        //                return false;
        //            }
        //        }

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Select2);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({
                    format: 'dd-mm-yyyy',
                    autoclose: true
                });

                $(".datetimepicker").datetimepicker({
                    format: 'dd-mm-yyyy hh:ii',
                    autoclose: true
                });
            }

            $('.datepicker').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true
            });

            $(".datetimepicker").datetimepicker({
                format: 'yyyy-mm-dd hh:ii',
                autoclose: true
            });

            function Select2(sender, args) {
                $(".select2").select2();
            }

            $(".select2").select2();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading">
                            Candidate Registration
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
                                                <asp:Button ID="btnAddNew" runat="server" Text="Add New Candidate Details" CssClass="btn btn-info" OnClick="btnAddNew_Click" Style="margin-bottom: 5px;"></asp:Button>

                                                <hr />
                                                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered table-striped dynamic-table"
                                                    OnPageIndexChanging="gvAssUserDataPointList_PageIndexChanging" DataKeyNames="ENTRY_AID">
                                                    <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAID" runat="Server" Text='<%# Eval("ENTRY_AID") %>' /><%--<%# Eval("") %>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="true" HeaderText="Case NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCaseNO" runat="Server" Text='<%# Eval("CASE_NO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                        
                                                        <asp:TemplateField Visible="true" HeaderText="Candidate Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCandidateName" runat="Server" Text='<%# Eval("AADHARNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderText="Aadhar No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAadharNo" runat="Server" Text='<%# Eval("AADHARNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderText="Pan No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPanNo" runat="Server" Text='<%# Eval("PANCODE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Employee Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpType" runat="Server" Text='<%# Eval("EMPTYPE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="State Of Posting">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatePost" runat="Server" Text='<%# Eval("STATEPOST") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Location Of Posting">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocPost" runat="Server" Text='<%# Eval("LOCPOST") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Department">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDept" runat="Server" Text='<%# Eval("DEPARTMENT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Grade">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrade" runat="Server" Text='<%# Eval("GRADE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesgn" runat="Server" Text='<%# Eval("DEGINATION") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="true" HeaderText="Mobile No" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobileNo" runat="Server" Text='<%# Eval("MOBILE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnView" runat="server" Text="View" CssClass="btn-sm btn-info" OnClick="lnkBtnView_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--   <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton Text="Edit" runat="server" CssClass="btn btn-warning" CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="Update" Text="Update" CssClass="btn btn-primary" runat="server" CommandName="Update" />
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-danger"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <td style="width: 10%" class="Title">
                                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                            </div>

                                            <hr />
                                        </asp:View>

                                        <asp:View ID="vwCreate" runat="server">
                                            <div class="panel-body">
                                                <div class="adv-table">
                                                    <div id="" class="form-horizontal">
                                                        <fieldset>
                                                            <div id="divAlertCreate" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                                <asp:Label ID="lblMessageCreate" runat="server"></asp:Label>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-lg-12">
                                                                    <h3 class="page-header" style="text-align: left; color: blue;"><u>Candidate Details</u></h3>
                                                                </div>
                                                            </div>


                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Employeement Type<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtEmployeementType" runat="server" ReadOnly="true" class="form-control input-sm"></asp:TextBox>
                                                                    </div>
                                                                    <label for="lblTranche" class="col-sm-3  labels">State Of Posting<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:DropDownList ID="drpStateOfPosting" CssClass="form-control input-sm-3" runat="server">
                                                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                                            <asp:ListItem Value="Andhra Pradesh">Andhra Pradesh</asp:ListItem>
                                                                            <asp:ListItem Value="Delhi">Delhi</asp:ListItem>
                                                                            <asp:ListItem Value="Maharashtra">Maharashtra</asp:ListItem>

                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Location Of Posting<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:DropDownList ID="drpLocationOfPosting" runat="server" CssClass="form-control input-sm-3" Enabled="true">
                                                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                                            <asp:ListItem Value="RNLIC-AP-Adani 1(APYQ)">RNLIC-AP-Adani 1(APYQ)</asp:ListItem>
                                                                            <asp:ListItem Value="RNLIC-AP-Adani 2(APYQ)">RNLIC-AP-Adani 2(APYQ)</asp:ListItem>

                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <label for="lblTranche" class="col-sm-3  labels">Department<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control input-sm-3" Enabled="true">
                                                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                                            <asp:ListItem Value="TDP">TDP</asp:ListItem>

                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Grade<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:DropDownList ID="drpGrade" runat="server" CssClass="form-control input-sm-3" Enabled="true">
                                                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                                            <asp:ListItem Value="IL-1(PO)">IL-1(PO)</asp:ListItem>
                                                                            <asp:ListItem Value="IL-2(PO)">IL-2(PO)</asp:ListItem>
                                                                            <asp:ListItem Value="IL-3(PO)">IL-3(PO)</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <label for="lblTranche" class="col-sm-3  labels">Designation<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:DropDownList ID="drpDegination" runat="server" CssClass="form-control input-sm-3" Enabled="true">
                                                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                                            <asp:ListItem Value="Area Manager">Area Manager</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">SU Code<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:DropDownList ID="drpSUCode" runat="server" CssClass="form-control input-sm-3" Enabled="true">
                                                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                                            <asp:ListItem Value="0672SU01TPTP">0672SU01TPTP</asp:ListItem>

                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <label for="lblTranche" class="col-sm-3  labels">Candidate Category<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:DropDownList ID="drpCandidateCategory" runat="server" CssClass="form-control input-sm-3" Enabled="true">
                                                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                                            <asp:ListItem Value="Employe-Front-Line Sales">Employe-Front-Line Sales</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <div class="col-lg-6">

                                                                        <label for="lblTranche" class="col-sm-6  labels" style="margin-left: -3%;">Aadhar Number<span style="color: Red;">*</span></label>
                                                                        <%--<div class="col-sm-1">
                                                                </div>--%>
                                                                        <div class="col-sm-4" style="margin-left: 3%;">
                                                                            <asp:TextBox ID="txtAadharNo" runat="server" onkeydown="return CheckNumeric(event);" MaxLength="12" placeholder="Aadhar No" class="form-control input-sm"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAadharNo"
                                                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                            ErrorMessage="Please enter a valid numeric value."
                                                                            Display="Dynamic" ForeColor="Red" />
                                                                        </div>

                                                                        <div class="col-sm-2">
                                                                            <asp:Button ID="btnCheck" runat="server" CssClass="btn btn-warning" Text="Check" OnClick="btnCheck_Click" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        </div>



                                                                    </div>
                                                                    <div class="col-lg-6">


                                                                        <label for="lblTranche" class="col-sm-6  labels" style="margin-left: -3%;">Pan No<span style="color: Red;">*</span></label>
                                                                        <div class="col-sm-4" style="margin-left: 3%;">
                                                                            <asp:TextBox ID="txtPanNo" runat="server" placeholder="PAN No" AutoPostBack="true" MaxLength="10" class="form-control input-sm"></asp:TextBox>
                                                                            <%--OnTextChanged="txtPanNo_TextChanged"--%>
                                                                        </div>

                                                                        <div class="col-sm-2">
                                                                            <asp:Button ID="btnValidatePAN" runat="server" CssClass="btn btn-info" Text="Validate" OnClick="btnValidatePAN_Click" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">
                                                                <div class="form-group">
                                                                    <div class="col-lg-6" id="divOTP" runat="server">
                                                                        <label for="lblTranche" class="col-sm-6  labels" style="margin-left: -3%;">Aadhar OTP<span style="color: Red;">*</span></label>
                                                                        <div class="col-sm-4" style="margin-left: 3%;">
                                                                            <asp:TextBox ID="txAadhartOTP" runat="server" placeholder="OTP" AutoPostBack="true" onkeydown="return CheckNumeric(event);" class="form-control input-sm"></asp:TextBox>
                                                                            <%--OnTextChanged="txAadhartOTP_TextChanged"--%>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:Button ID="btnValidate" runat="server" CssClass="btn btn-info" Text="Validate" OnClick="btnValidate_Click" AutoPostBack="true" OnClientClick="showLoader();" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-lg-6">
                                                                        <label for="lblTranche" class="col-sm-6  labels" style="margin-left: -3%;">Mobile Number<span style="color: Red;">*</span></label>
                                                                        <div class="col-sm-6" style="margin-left: 3%;">
                                                                            <asp:TextBox ID="txtMobileNumber" runat="server" class="form-control input-sm"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12">
                                                                <div class="form-group">
                                                                    <div class="col-lg-6">
                                                                        <label for="lblTranche" class="col-sm-6  labels" style="margin-left: -3%;">Aadhar-PAN Link Status<span style="color: Red;">*</span></label>
                                                                        <div class="col-sm-6" style="margin-left: 3%;">
                                                                            <asp:Label ID="lblAPLinkStatus" runat="server" Text=""></asp:Label>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-lg-12">
                                                                    <h3 class="page-header" style="text-align: left; color: blue;"><u>Personal Details</u></h3>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Aadhar No.</label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtAadharNoPer" runat="server" ReadOnly="true" AutoCompleteType="Disabled" AutoPostBack="true" class="form-control input-sm" MaxLength="12"></asp:TextBox>
                                                                        <%--OnTextChanged="txtAadharNoPer_Click"--%>
                                                                        
                                                                    </div>
                                                                    <label for="lblTranche" class="col-sm-3  labels">Case No.</label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtCaseNo" runat="server" ReadOnly="true" class="form-control input-sm"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Name As Per PAN<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtPanName" runat="server" ReadOnly="true" AutoCompleteType="Disabled" class="form-control input-sm"></asp:TextBox>
                                                                    </div>
                                                                    <label for="lblTranche" class="col-sm-3  labels">Name As Per Aadhar<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtAadharName" runat="server" ReadOnly="true" AutoCompleteType="Disabled" class="form-control input-sm"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Date Of Birth<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtDOB" runat="server" ReadOnly="true" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled"></asp:TextBox>
                                                                    </div>
                                                                    <label for="lblTranche" class="col-sm-3  labels">Gender<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <label style="display: inline-flex; align-items: center; font-size: 16px; cursor: pointer; color: cornflowerblue;">
                                                                            <asp:RadioButton GroupName="radio" ID="rdMale" Enabled="false" runat="server" Style="width: 20px; height: 20px; margin-right: 5px;" />
                                                                            Male
                                                                        </label>
                                                                        &emsp;
                                                                <label style="display: inline-flex; align-items: center; font-size: 16px; cursor: pointer; color: hotpink;">
                                                                    <asp:RadioButton GroupName="radio" ID="rdFemale" Enabled="false" runat="server" Style="width: 20px; height: 20px; margin-right: 5px;" />
                                                                    Female
                                                                </label>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            &emsp;

                                                  <%--  <div id="div3" runat="server">
                                                        <div class="form-group">
                                                            <h4 style="color: dodgerblue; text-align: left">Address</h4>
                                                        </div>
                                                    </div>--%>
                                                            <div class="col-lg-12">
                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Address As Per Aadhar</label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtAdd1" runat="server" ReadOnly="true" class="form-control input-sm" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12">
                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Current Address</label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtAdd2" runat="server" ReadOnly="false" class="form-control input-sm" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12" runat="server" visible="false">
                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Current Address</label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtAdd3" runat="server" ReadOnly="false" class="form-control input-sm" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Country<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtCountry" runat="server" ReadOnly="true" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                                    </div>
                                                                    <label for="lblTranche" class="col-sm-3  labels">State<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtState" runat="server" ReadOnly="true" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">PIN Code<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtPIN" runat="server" ReadOnly="true" class="form-control input-sm" AutoCompleteType="Disabled" MaxLength="6"></asp:TextBox>
                                                                    </div>
                                                                    <label for="lblTranche" class="col-sm-3  labels">Email ID<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="false" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEmail"
                                                                            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                                                                            ErrorMessage="Please enter a valid email address."
                                                                            Display="Dynamic" ForeColor="Red" />

                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <%--<div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Mobile No.<span style="color: Red;">*</span></label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtMobNo" runat="server" ReadOnly="true" class="form-control input-sm" AutoCompleteType="Disabled" MaxLength="10"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobNo"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>
                                                            <label for="lblTranche" class="col-sm-3  labels">Landline No.</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtLandNo" runat="server" ReadOnly="false" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtLandNo"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>

                                                        </div>
                                                    </div>--%>

                                                            <%--<div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Refferal Name / SAP Code<span style="color: Red;">*</span></label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtRefName_Code" runat="server" ReadOnly="true" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                            </div>
                                                            <label for="lblTranche" class="col-sm-3  labels">Refferal Mobile No.<span style="color: Red;">*</span></label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtRefMobNo" runat="server" ReadOnly="true" class="form-control input-sm" AutoCompleteType="Disabled" MaxLength="10"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtRefMobNo"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>

                                                        </div>
                                                    </div>--%>

                                                            <div class="col-lg-12">

                                                                <div class="form-group">
                                                                    <label for="lblTranche" class="col-sm-3  labels">Expected DOJ</label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtExpDOJ" runat="server" CssClass="form-control input-sm datepicker" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                                                    </div>

                                                                    <label for="lblTranche" class="col-sm-3  labels">Remarks<span style="color: Red;"></span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtRemk" runat="server" TextMode="MultiLine" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-lg-12">
                                                                    <h3 class="page-header" style="text-align: left; color: blue;"><u>Upload Document</u></h3>
                                                                </div>
                                                            </div>



                                                            <div class="col-lg-12">
                                                                <div class="form-group" id="divfileUploadAadhar" runat="server">
                                                                    <label for="ContentPlaceHolder1_fupldDocument" class="col-sm-4 labels">Upload Aadhar Card<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:FileUpload ID="fupldDocumentAadhar" runat="server" CssClass="btn fileinput-button" />
                                                                        <asp:LinkButton ID="lnkDnlAadhar" runat="server" Style="color: blue" Visible="false" OnClick="lnkDnlAadhar_Click">Download Saved File</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3" style="text-align: center;">

                                                                        <asp:Button ID="btnAadharUpload" runat="server" CssClass="btn btn-primary" Visible="false" Text="Upload" OnClick="btnAadharUpload_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12" id="divAadhar" runat="server" visible="false">
                                                                Supporting files: Aadhar Card
                                                        <asp:GridView ID="gvAadhar" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFileStorageAadhar" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Uploaded Files">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkBtnOpenFileAadhar" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                            OnClick="lnkBtnOpenFileAadhar_Click" ForeColor="Blue" class="underline" />
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

                                                            <div class="col-lg-12">
                                                                <div class="form-group" id="divfileUploadPan" runat="server">
                                                                    <label for="ContentPlaceHolder1_fupldDocument" class="col-sm-4 labels">Upload Pan Card<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:FileUpload ID="fupldDocumentPan" runat="server" CssClass="btn fileinput-button" />
                                                                        <asp:LinkButton ID="lnkDnlPan" runat="server" Style="color: blue" Visible="false" OnClick="lnkDnlPan_Click">Download Saved File</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3" style="text-align: center;">

                                                                        <asp:Button ID="btnPanUpload" runat="server" CssClass="btn btn-primary" Visible="false" Text="Upload" OnClick="btnPanUpload_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12" id="divPan" runat="server" visible="false">
                                                                Supporting files: Pan Card
                                                        <asp:GridView ID="gvPan" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFileStoragePan" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Uploaded Files">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkBtnOpenFilesPan" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                            OnClick="lnkBtnOpenFilesPan_Click" ForeColor="Blue" class="underline" />
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


                                                            <div class="col-lg-12">
                                                                <div class="form-group" id="divfileUploadEduc" runat="server">
                                                                    <label for="ContentPlaceHolder1_fupldDocument" class="col-sm-4 labels">Upload Education Document<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:FileUpload ID="fupldDocumentEduc" runat="server" CssClass="btn fileinput-button" />
                                                                        <asp:LinkButton ID="lnkDnlEduc" runat="server" Style="color: blue" Visible="false" OnClick="lnkDnlEduc_Click">Download Saved File</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3" style="text-align: center;">

                                                                        <asp:Button ID="btnEducUpload" runat="server" CssClass="btn btn-primary" Visible="false" Text="Upload" OnClick="btnEducUpload_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12" id="divEduc" runat="server" visible="false">
                                                                Supporting files: Education Document
                                                            <asp:GridView ID="gvEduc" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFileStorageEduc" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBtnOpenFilesEduc" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                OnClick="lnkBtnOpenFilesEduc_Click" ForeColor="Blue" class="underline" />
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
                                                            <div class="col-lg-12">
                                                                <div class="form-group" id="divfileUploadCC" runat="server">
                                                                    <label for="ContentPlaceHolder1_fupldDocument" class="col-sm-4 labels">Upload Cancelled Cheque<span style="color: Red;">*</span></label>
                                                                    <div class="col-sm-3">
                                                                        <asp:FileUpload ID="fupldDocumentCC" runat="server" CssClass="btn fileinput-button" />
                                                                        <asp:LinkButton ID="lnkDnlCC" runat="server" Style="color: blue" Visible="false" OnClick="lnkDnlCC_Click">Download Saved File</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3" style="text-align: center;">

                                                                        <asp:Button ID="btnCCUpload" runat="server" CssClass="btn btn-primary" Visible="false" Text="Upload" OnClick="btnCCUpload_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12" id="divCC" runat="server" visible="false">
                                                                Supporting files: Cancelled Cheque
                                                            <asp:GridView ID="gvCC" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFileStorageCC" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBtnOpenFilesCC" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                OnClick="lnkBtnOpenFilesCC_Click" ForeColor="Blue" class="underline" />
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



                                                            <div class="col-lg-12">
                                                                <div class="form-group">
                                                                    <div class="col-sm-12" style="text-align: center;">
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" OnClientClick="showLoader();" />
                                                                        <asp:Button ID="btnClose" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnClose_Click" OnClientClick="showLoader();" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">
                                                                <div class="form-group">
                                                                    <hr />
                                                                </div>
                                                            </div>

                                                            <%-- <div class="col-lg-12" id="dvForm">
                                                        <div class="form-group">
                                                            <label for="lblSupportingfiles" class="col-sm-2 labels">Upload Signed Form:</label>


                                                            <div class="col-sm-3">
                                                                <asp:FileUpload ID="fupForm" runat="server" CssClass="btn fileinput-button" />
                                                                <asp:LinkButton ID="btnDownloadForm" runat="server" Style="font-weight: bold; font-size: 16px; color: blue" Class="underline"
                                                                    OnClick="btnDownloadForm_Click">Download Saved Form</asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-2" style="text-align: center;">
                                                                <img id="loader1" style="display: none; height: 50px; width: 25px; float: right;" src="Assets/progress.gif" />
                                                                <asp:Button ID="btnForm" runat="server" Text="Upload Form" CssClass="btn btn-primary" OnClick="btnUploadForm_Click" OnClientClick="showLoader();" />


                                                            </div>

                                                        </div>


                                                    </div>--%>
                                                            <%--<div class="col-lg-12" id="divForm2" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label for="lblSupportingfiles" class="col-sm-3 labels">Upload Signed Form:</label>
                                                        </div>
                                                        <div style="border: 2px solid gray; padding: 10px; margin-top: 10px;">
                                                            <asp:FileUpload ID="fupPFForm" runat="server" CssClass="btn fileinput-button" />
                                                            <asp:Button ID="btnUplaodPFFrom" runat="server" Style="background-color: Aqua; color: Black; padding: 6px;"
                                                                Text="Upload Form" CssClass="btn btn-primary" OnClick="btnUploadForm_Click" />
                                                            <br />
                                                            <asp:LinkButton ID="btnDownloadPFForm" runat="server" Style="font-weight: bold; color: blue" Class="underline"
                                                                OnClick="btnDownloadPFForm_Click">Download Saved Form</asp:LinkButton>
                                                        </div>
                                                    </div>--%>

                                                            <hr />

                                                            <hr />
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>

                                    </asp:MultiView>

                        </div>

                        <div style="text-align: center;">
                            <div id="MyPopup" class="modal fade" role="dialog">
                                <div class="modal-dialog">
                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: dodgerblue">
                                            <button type="button" class="close" data-dismiss="modal">
                                                &times;</button>
                                            <h4 class="modal-title"></h4>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="lblNotifymessage" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                                            <hr />


                                            <asp:Button ID="btnFullTime" runat="server" class="btn btn-success" Text="Full Time" OnClick="btnFullTime_Click" AutoPostBack="true" />
                                            <asp:Button ID="btnPartTime" runat="server" class="btn btn-info" Text="Part Time" OnClick="btnPartTime_Click" AutoPostBack="true" />


                                        </div>
                                        <div class="modal-footer">

                                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                                Close</button>

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

