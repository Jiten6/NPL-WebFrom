<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="NewPortal2023.ESS.ResetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">

    <title>Reset Password</title>

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
</head>

<body class="login-body">
     <div class="container">
            <form id="Form1" class="form-signin" runat="server">
                 <h2 class="form-signin-heading">Reset Password</h2>
                <div class="login-wrap">
                     <div class="user-login-info">
                          <asp:TextBox ID="txtCompCode" runat="server" CssClass="form-control" placeholder="Company" Text="NPL" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control" placeholder="Employee Code"></asp:TextBox>
                          <asp:TextBox ID="txtPAN"  runat="server" CssClass="form-control" placeholder="PAN Card" ></asp:TextBox>

                          <div style="text-align: center; margin: 10px 0;">
                              <label>OR</label>
                          </div>

                           <asp:TextBox ID="txtJoiningDate"  runat="server" CssClass="form-control" placeholder="Joining Date (DD-MM-YYYY):" ></asp:TextBox>
                         
                         </div>

                       <asp:Button ID="btnVerify" runat="server" CssClass="btn btn-lg btn-login btn-block" Text="Verify" OnClick="btnVerify_Click" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>                             
                                                       
                    </div>
                </form>
         </div>

    <!-- Placed js at the end of the document so the pages load faster -->
    <!--Core js-->
    <script type="text/javascript" src="../Assets/js/jquery.js"></script>
    <script type="text/javascript" src="../Assets/bs3/js/bootstrap.min.js"></script>
</body>
</html>

