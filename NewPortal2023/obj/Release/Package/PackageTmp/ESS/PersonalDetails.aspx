<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="PersonalDetails.aspx.cs" Inherits="NewPortal2023.ESS.PersonalDetails" %>

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
        function ShowPopup(title, body)
        {
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
                            Complete Personal Details
                        </header>
                        <div class="panel-body">
                            <asp:ScriptManager ID="scm" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/Assets/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/Assets/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>
                            <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">

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
                                                            <h3 class="page-header" style="text-align: left; color: blue;"><u>Personal Details</u></h3>
                                                        </div>
                                                    </div>

                                                   <%-- <div id="div2" runat="server">
                                                        <div class="form-group">
                                                            <h4 style="color: dodgerblue; text-align: left">Aadhar Card Details</h4>
                                                        </div>
                                                    </div>--%>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Aadhar No.</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtAadharNo" runat="server" ReadOnly="false" AutoCompleteType="Disabled" class="form-control input-sm" MaxLength="12"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAadharNo"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>
                                                            <label for="lblTranche" class="col-sm-3  labels">Case No.</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCaseNo" runat="server" ReadOnly="true" class="form-control input-sm"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Name On PAN<span style="color: Red;">*</span></label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtPanName" runat="server" ReadOnly="true" AutoCompleteType="Disabled" class="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                            <label for="lblTranche" class="col-sm-3  labels">Name On Aadhar<span style="color: Red;">*</span></label>
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
                                                                    <asp:RadioButton GroupName="radio" id="rdMale"  Enabled="false"  runat="server" style="width: 20px; height: 20px; margin-right: 5px;" />
                                                                    Male
                                                                </label>
                                                                &emsp;
                                                                <label style="display: inline-flex; align-items: center; font-size: 16px; cursor: pointer; color: hotpink;">
                                                                    <asp:RadioButton GroupName="radio" id="rdFemale"  Enabled="false" runat="server" style="width: 20px; height: 20px; margin-right: 5px;" />
                                                                    Female
                                                                </label>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    &emsp;

                                                    <div id="div3" runat="server">
                                                        <div class="form-group">
                                                            <h4 style="color: dodgerblue; text-align: left">Address</h4>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Address Line 1<span style="color: Red;">*</span></label>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtAdd1" runat="server" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Address Line 2<span style="color: Red;">*</span></label>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtAdd2" runat="server" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Address Line 3</label>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtAdd3" runat="server" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">City<span style="color: Red;">*</span></label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCity" runat="server" ReadOnly="true" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
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

                                                    <div class="col-lg-12">

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
                                                    </div>

                                                    <div class="col-lg-12">

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
                                                    </div>

                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Expected DOJ</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtExpDOJ" runat="server" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled"></asp:TextBox>
                                                            </div>
                                                            <label for="lblTranche" class="col-sm-3  labels">Remarks<span style="color: Red;">*</span></label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtRemk" runat="server" ReadOnly="false" class="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <hr />

                                                    <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                        <asp:Button ID="btnEdit" CssClass="btn btn-info" runat="server" Text="Edit" Visible="false" OnClientClick="showLoader();" />
                                                        <asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" Text="Submit" Autopostback="true" OnClientClick="showLoader();" />
                                                        <asp:Button ID="btnReset" CssClass="btn btn-danger" runat="server" Text="Reject" OnClientClick="showLoader();" />
                                                        <asp:HiddenField ID="hdnAID" runat="server" />
                                                    </div>

                                                </fieldset>
                                            </div>
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
