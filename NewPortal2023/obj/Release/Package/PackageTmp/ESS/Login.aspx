<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NewPortal2023.ESS.Login" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">

    <script type="text/javascript" src="../Assets/cdnjs.cloudflare.com/ajax/libs/crypto-js.min.js"></script>

    <script type="text/javascript">
        //Secret Key.
        var pb = "$DOTtEqMONgcPOPoADa0QWq#";
        var pb1 = "$QRTaQqSAMdtDODoAFa0QWq#";

        // const pb = document.getElementById("hiddenKeyPassword").value;
        //const pb1 = document.getElementById("hiddenKeyUsername").value;


        //Secret Bytes.
        var secretBytes = CryptoJS.enc.Utf8.parse(pb);
        var secretBytes1 = CryptoJS.enc.Utf8.parse(pb1);

        function Encrypt() {
            //Read the Plain text.
            console.log("Encrypt function called ✅");

            var txtPassword = document.getElementById("<%=txtPassword.ClientID%>");
            var txtEmpCode = document.getElementById("<%=txtEmpCode.ClientID%>");

            //Encrypt with AES Alogorithm using Secret Key.
            var encrypted = CryptoJS.AES.encrypt(txtPassword.value, secretBytes, {
                mode: CryptoJS.mode.ECB,
                padding: CryptoJS.pad.Pkcs7
            });

            var encrypted1 = CryptoJS.AES.encrypt(txtEmpCode.value, secretBytes1, {
                mode: CryptoJS.mode.ECB,
                padding: CryptoJS.pad.Pkcs7
            });

            //Set the encrypted Text in TextBox.
            txtPassword.value = encrypted.toString();;
            txtEmpCode.value = encrypted1.toString();

            console.log("Encrypted value set to field: " + txtEmpCode.value);

        }

    </script>

    <script type="text/javascript" language="javascript">


        document.addEventListener("contextmenu", function (e) {
            e.preventDefault();
        }, false);
        document.addEventListener("keydown", function (e) {

            if (e.ctrlKey && e.shiftKey && e.keyCode == 73) {
                disabledEvent(e);
            }

            if (e.ctrlKey && e.shiftKey && e.keyCode == 74) {
                disabledEvent(e);
            }

            if (e.keyCode == 83 && (navigator.platform.match("Mac") ? e.metaKey : e.ctrlKey)) {
                disabledEvent(e);
            }

            if (e.ctrlKey && e.keyCode == 85) {
                disabledEvent(e);
            }

            if (event.keyCode == 123) {
                disabledEvent(e);
            }

            if (e.ctrlKey && event.keyCode == 67) {
                disabledEvent(e);
            }
        }, false);

        function disabledEvent(e) {
            if (e.stopPropagation) {
                e.stopPropagation();
            } else if (window.event) {
                window.event.cancelBubble = true;
            }
            e.preventDefault();
            return false;
        }</script>

    <title>Login</title>


    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />




    <!--Icon -->
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />

    <!--Core CSS -->
    <link href="../Assets/bs3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Assets/css/bootstrap-reset.css" rel="stylesheet" />
    <link href="../Assets/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <%--<link href="Assets/bs3/css/bootstrap.min.css" rel="stylesheet" />--%>
    <%--<link href="assets/css/bootstrap-reset.css" rel="stylesheet" />
    <link href="assets/font-awesome/css/font-awesome.css" rel="stylesheet" />--%>

    <!-- Custom styles for this template -->
    <%--    <link href="assets/css/style.css" rel="stylesheet">
    <link href="assets/css/style-responsive.css" rel="stylesheet" />--%>
    <link href="../Assets/css/style.css" rel="stylesheet" />
    <link href="../Assets/css/style-responsive.css" rel="stylesheet" />

    <%--      <script src="../jquery-ui.js"></script>
    <link href="../jquery-ui.css" rel="stylesheet" />--%>
    <%--  <link href="../jquery-ui.min.css" rel="stylesheet" />
    <script src="../jquery-ui.min.js"></script>--%>


    <%--   <script type="text/javascript">
        $(document).ready(function () {
            $('#txtName').autocomplete({
                source: 'NameHandler.ashx'
            });
        });
    </script>--%>



</head>
<body class="login-body" style="background-color: lightblue">
    <div class="container">
        <form id="Form1" class="form-signin" runat="server">
            <h2 class="form-signin-heading" style="background-color: #6c65bc; border-bottom: 10px solid #fff">
                <%--<img src="NPL_Logo_Home.png" alt="ESIC management" style="height:150px;width:250px; " />--%>
                <%--<img src="NPL_Logo_Home.png" alt="ESIC management" width="250" height="100" style="vertical-align: middle;">--%>
                <img src="../image/NPL_logo.png" alt="ESIC management" width="250" height="100" style="vertical-align: middle;">
            </h2>
            <div class="login-wrap">
                <div class="user-login-info">

                    <asp:TextBox ID="txtCompCode" runat="server" CssClass="form-control" placeholder="Company" Text="NPL" Visible="false" Enabled="false"></asp:TextBox>

                    <%--<asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control" placeholder="Employee Code" Font-Bold="true"></asp:TextBox>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password" Font-Bold="true"></asp:TextBox>--%>
                    <%--    <asp:TextBox ID="txtName" placeholder="text" runat="server" Width="160px" />--%>
                    <%--  <asp:DropDownList ID="drpAccYear" runat="server" CssClass="form-control">
                        </asp:DropDownList>--%>

                    <div style="margin-bottom: 10px; position: relative; display: inline-block; width: 100%;">
                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"
                            placeholder="Employee Code" Style="padding-right: 30px;" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>

                    <div style="position: relative; display: inline-block; width: 100%;">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"
                            placeholder="Password" TextMode="Password"
                            Style="padding-right: 40px;" AutoCompleteType="Disabled"></asp:TextBox>
                        <img id="eyeIcon" src="../image/eye.svg" alt="Toggle Password"
                            onclick="togglePassword()"
                            style="position: absolute; right: 10px; top: 50%; transform: translateY(-80%); width: 20px; height: 20px; cursor: pointer; z-index: 10;">
                    </div>
                </div>
                <asp:Button ID="btnLogin" CssClass="btn btn-lg btn-login btn-block"
                    runat="server" Text="Sign in" OnClick="btnLogin_Click" Style="background-color: #34aadf" OnClientClick="Encrypt(); return true;" />
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkForgot" runat="server" Font-Names="Arial"
                    Font-Size="10pt" Height="20px" Style="display: block; text-align: right; margin-top: 20px; font-weight: bold; color: darkblue;" PostBackUrl="ResetPassword.aspx">Forgot Password?</asp:LinkButton>

            </div>
        </form>
    </div>
    <!-- Placed js at the end of the document so the pages load faster -->
    <!--Core js-->
    <script type="text/javascript" src="../Assets/js/jquery.js"></script>
    <script type="text/javascript" src="../Assets/bs3/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        function togglePassword() {
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
            var eyeButton = document.getElementById('eyeButton');

            if (passwordField.type === "password") {
                passwordField.type = "text";
                eyeIcon.src = "../image/eye-slash.svg";
            } else {
                passwordField.type = "password";
                eyeIcon.src = "../image/eye.svg";
            }
        }
</script>
</body>
<!-- Mirrored from bucketadmin.themebucket.net/login.html by HTTrack Website Copier/3.x [XR&CO'2014], Mon, 18 Feb 2019 10:27:47 GMT -->
</html>
