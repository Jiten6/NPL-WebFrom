<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.master" AutoEventWireup="true" CodeBehind="PFForm11.aspx.cs" Inherits="NewPortal2023.ESS.PFForm11" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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
        $(function () {
            $('[id*=txtPassportFrom]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
        $(function () {
            $('[id*=txtPassportTo]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading">
                            PF Form No. 11
                        </header>
                        <div id="blockUI" class="panel-body">
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
                                                        <div class="col-sm-6">
                                                            <a href="NominationInstructions.aspx" style="font-size: 20px; color: blue" Class="underline">Nomination Instructions</a>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="form-group">
                                                        <div class="col-lg-12">
                                                            <h3 class="page-header" style="text-align: center;color:black;">PF Form No. 11 - Declaration Form</h3>
                                                        </div>
                                                    </div>


                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Name of the member</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtTranche" runat="server" ReadOnly="true" class="form-control input-sm"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <label for="lblCashTrEnb" class="col-sm-3 labels">Father's Name / Spouse's Name</label>
                                                            <div class="col-sm-3">
                                                                <asp:RadioButton ID="rdbFather" GroupName="MIDDLETYPE" runat="server"
                                                                    Text="Father's Name" OnCheckedChanged="rdbFather_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rdbSpouse" GroupName="MIDDLETYPE" runat="server"
                                                                    Text="Spouse's Name" OnCheckedChanged="rdbSpouse_CheckedChanged" AutoPostBack="true" />
                                                                <br />

                                                                <asp:TextBox ID="txtFatherSpouse" class="form-control input-sm" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Date of Birth (DD/MM/YYYY)</label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="drpGender" CssClass="form-control input-sm-3" runat="server" Enabled="false">
                                                                    <asp:ListItem Value="">Select Gender</asp:ListItem>
                                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                                    <asp:ListItem Value="T">Transgender</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Marital Status</label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="drpMaritalStatus" CssClass="form-control input-sm-3" runat="server">
                                                                    <asp:ListItem Value="">Select Status</asp:ListItem>
                                                                    <asp:ListItem Value="Unmarried">Unmarried</asp:ListItem>
                                                                    <asp:ListItem Value="Married">Married</asp:ListItem>
                                                                    <asp:ListItem Value="Widowed">Widowed</asp:ListItem>
                                                                    <asp:ListItem Value="Divorced">Divorced</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Email ID</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtEmailID" CssClass="form-control input-sm-3" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Mobile No.</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control input-sm-3" MaxLength="10"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Whether earlier a member of Employees Provident Fund Scheme, 1952</label>
                                                            <div class="col-sm-3">
                                                                <asp:RadioButton ID="rdbPF1952Yes" GroupName="PF1952" runat="server" Text="Yes" OnCheckedChanged="rdbPF1952Yes_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rdbPF1952No" GroupName="PF1952" runat="server" Text="No" OnCheckedChanged="rdbPF1952No_CheckedChanged" AutoPostBack="true" />
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Whether earlier a member of Employees Pension Scheme, 1995</label>
                                                            <div class="col-sm-3">
                                                                <asp:RadioButton ID="rdbPF1995Yes" GroupName="PF1995" runat="server" Text="Yes" />&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rdbPF1995No" GroupName="PF1995" runat="server" Text="No" />
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Universal Account No (UAN)</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtUAN" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Previous PF A/C No.</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtPFNo" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Date of exit from previous employment (DD/MM/YYYY)</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtExitDate" runat="server" CssClass="form-control input-sm-3" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Scheme Certificate No. (if issued)</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtSchemeCertNo" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Pension Payment Order (PPO)No. (if issued)</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtPPO" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">International Worker</label>
                                                            <div class="col-sm-3">
                                                                <asp:RadioButton ID="rdbIntlWorkerYes" GroupName="IntlWorker" runat="server"
                                                                    Text="Yes" OnCheckedChanged="rdbIntlWorkerYes_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rdbIntlWorkerNo" GroupName="IntlWorker" runat="server"
                                                                    Text="No" OnCheckedChanged="rdbIntlWorkerNo_CheckedChanged" AutoPostBack="true" />
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">If Yes , State Country Of Origin (India/Name of Other Country)</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtOrigin" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Passport No.</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtPassportNo" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Validity Of Passport (DD/MM/YYY) to (DD/MM/YYY)</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtPassportFrom" runat="server" placeholder="DD/MM/YYYY" ReadOnly="false" CssClass="form-control input-sm-3"></asp:TextBox>

                                                            </div>
                                                            <div class="col-sm-1" style="width: 4.333%">
                                                                TO
                                                            </div>

                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtPassportTo" runat="server" placeholder="DD/MM/YYYY" ReadOnly="false" CssClass="form-control input-sm-3"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Bank Account No.</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtBankAccNo" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">IFS Code</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                                    ControlToValidate="txtPAN" ErrorMessage="Must be 10 Chharacter."
                                                                    ValidationExpression="[0-9][A-Z][a-z]{11}"></asp:RegularExpressionValidator>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">AADHAR No.</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtAADHAR" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                    ControlToValidate="txtPAN" ErrorMessage="Must be 16 Chharacter."
                                                                    ValidationExpression="[0-9]{16}"></asp:RegularExpressionValidator>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Permanent Account Number (PAN),If available</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control input-sm-3"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                    ControlToValidate="txtPAN" ErrorMessage="Must be 11 Chharacter."
                                                                    ValidationExpression="[0-9][A-Z][a-z]{10}"></asp:RegularExpressionValidator>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <div class="col-sm-12" style="text-align: center;">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save Form" OnClientClick="showLoader();" OnClick="btnSave_Click" />
                                                                <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-warning" Text="Generate Form" OnClick="btnGenerate_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <hr />
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-12" id="dvForm">
                                                        <div class="form-group">
                                                            <label for="lblSupportingfiles" class="col-sm-2 labels">Upload Signed Form:</label>


                                                            <div class="col-sm-3">
                                                                <asp:FileUpload ID="fupForm" runat="server" CssClass="btn fileinput-button" />
                                                                 <asp:LinkButton ID="btnDownloadForm" runat="server" Style="font-weight: bold;font-size:16px; color: blue" Class="underline"
                                                                    OnClick="btnDownloadForm_Click">Download Saved Form</asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-2" style="text-align: center;">
                                                                <img id="loader1" style="display: none; height: 50px; width: 25px; float: right;" src="Assets/progress.gif" />
                                                                <asp:Button ID="btnForm" runat="server" Text="Upload Form" CssClass="btn btn-primary" OnClick="btnUploadForm_Click" OnClientClick="showLoader();" />

                                                               
                                                            </div>

                                                        </div>


                                                    </div>
                                                    <div class="col-lg-12" id="divForm2" runat="server" visible="false">
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
                                                    </div>

                                                    <hr />

                                                    <hr />
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
