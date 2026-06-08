<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="AadhaarPanCheck.aspx.cs" Inherits="NewPortal2023.ESS.AadhaarPanCheck" Async="true" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


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

    <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/litepicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading">
                            Aadhaar-Pan Info
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
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>
                                        <br />
                                        <asp:Button ID="btnAadhar" runat="server" Text="Aadhaar Check" CssClass="btn btn-primary" OnClick="btnAadhar_Click" Style="margin-bottom: 5px; margin-top: 20px; margin-left: 20px;"></asp:Button>
                                        <asp:Button ID="btnPan" runat="server" Text="Pan Check" CssClass="btn btn-info" OnClick="btnPan_Click" Style="margin-bottom: 5px; margin-top: 20px; margin-left: 20px;"></asp:Button>

                                        <br />
                                        <br />
                                        <div class="form-group" id="divAadhaar" runat="server" visible="false">
                                            <asp:Label ID="lblAadhaarNo" runat="server" class="col-sm-2 col-form-label">Aadhaar No :- </asp:Label>
                                            <div class="col-sm-1">
                                                <asp:TextBox class="form-control" ID="txtfirst" onkeydown="return CheckNumeric(event);" MaxLength="4" Placeholder="1234" AutoPostBack="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1">
                                                <asp:TextBox class="form-control" ID="txtSecond" onkeydown="return CheckNumeric(event);" MaxLength="4" Placeholder="1234" AutoPostBack="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1">
                                                <asp:TextBox class="form-control" ID="txtThird" onkeydown="return CheckNumeric(event);" MaxLength="4" Placeholder="1234" AutoPostBack="false" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1">
                                                <asp:Button ID="btnAadhaarCheck" runat="server" CssClass="btn btn-success" Text="Check" OnClick="btnAadhaarCheck_Click" />
                                            </div>
                                          
                                            <div class="col-sm-2">
                                                <asp:TextBox class="form-control" ID="txtveryfy" onkeydown="return CheckNumeric(event);" MaxLength="6" Placeholder="123456" AutoPostBack="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Button ID="btnveryfy" runat="server" CssClass="btn btn-success" Text="OTP Verify" OnClick="btnveryfy_Click" />
                                            </div>

                                            <div>
                                                <br />

                                                <div>
                                                    <asp:Label ID="errorLabel" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <br />
                                            <div>
                                                <asp:Label ID="nameLabel" runat="server"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="genderLabel" runat="server"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="dobLabel" runat="server"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="addressLabel" runat="server"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                            </div>
                                           

                                        </div>
                                        <div class="form-group row" id="divPan" runat="server" visible="false">
                                            <asp:Label ID="lblPanNp" runat="server" class="col-sm-2 col-form-label">Pan No :- </asp:Label>
                                            <div class="col-sm-2">
                                                <asp:TextBox class="form-control" ID="txtPanValidation" MaxLength="10" Placeholder="ABCTY1234D" AutoPostBack="false" runat="server"></asp:TextBox>
                                                <%-- <asp:RegularExpressionValidator ID="rgxPANCard" runat="server" ValidationExpression="([A-Z]){5}([0-9]){4}([A-Z]){1}$" ControlToValidate="txtPanValidation" ErrorMessage="Invalid PAN Number" CssClass = "error"></asp:RegularExpressionValidator>--%>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPanValidation"
                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}"></asp:RegularExpressionValidator>
                                            </div>


                                            <div class="col-sm-1">
                                                <asp:Button ID="btnPanCheck" runat="server" CssClass="btn btn-success" Text="Check" OnClick="btnPanCheck_Click" />
                                            </div>
                                             <div>
                                                <asp:Label ID="lblpan" Visible="false" runat="server"></asp:Label>
                                            </div>
                                            <div> 
                                                <asp:Label ID="Lblpanname" Visible="false" runat="server"></asp:Label>
                                            </div>


                                            <br />

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
