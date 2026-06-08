<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="NewPortal2023.ESS.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">

    <title>Change Password</title>

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
</head>

<body class="login-body">
        <div class="container">
            <form id="Form1" class="form-signin" runat="server">
                <h2 class="form-signin-heading">Change Password</h2>
                <div class="login-wrap">
                    <div class="user-login-info">
                        <asp:TextBox ID="txtOldPass" runat="server" CssClass="form-control" placeholder="Old Password" TextMode="Password"></asp:TextBox> <%--Visible="false" Enabled="false"--%>
                        <asp:TextBox ID="txtNewPass" runat="server" CssClass="form-control" placeholder="New Password" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#FF3300" Text="Password must be atleast 8 and atmost 15 characters long."></asp:Label>
                        <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="form-control" placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                      <%--  <asp:DropDownList ID="drpAccYear" runat="server" CssClass="form-control">
                        </asp:DropDownList>--%>
                    </div>
                    <asp:Button ID="btnLogin" CssClass="btn btn-lg btn-login btn-block" 
                        runat="server" Text="Update" onclick="btnLogin_Click" /> 
                    <asp:Button ID="Button1" CssClass="btn btn-lg btn-login btn-block" 
                        runat="server" Text="Cancel" onclick="btnCancel_Click" /> 

                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
            </form>
        </div>
    <!-- Placed js at the end of the document so the pages load faster -->
    <!--Core js-->
    <script type="text/javascript" src="../Assets/js/jquery.js"></script>
    <script type="text/javascript" src="../Assets/bs3/js/bootstrap.min.js"></script>
    </body>
</html>
