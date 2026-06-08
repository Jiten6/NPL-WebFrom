<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="CandidateLoginPage.aspx.cs" Inherits="NewPortal2023.ESS.CandidateLoginPage" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">

    <title>Login</title>

    <!--Icon -->
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />

    <!--Core CSS -->
    <link href="../Assets/bs3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Assets/css/bootstrap-reset.css" rel="stylesheet" />
    <link href="../Assets/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="../Assets/css/style.css" rel="stylesheet" />
    <link href="../Assets/css/style-responsive.css" rel="stylesheet" />
</head>
    <body class="login-body">
        <div class="container">
            <form id="Form1" class="form-signin" runat="server">
                <h2 class="form-signin-heading">Candidate Sign in</h2>
                <div class="login-wrap">
                    <div class="user-login-info">
                        
                        <asp:TextBox ID="txtEmpCode" AutoCompleteType="Disabled" runat="server" CssClass="form-control" placeholder="Employee Code /PAN Number/"></asp:TextBox>
                        <asp:TextBox ID="txtPassword" AutoCompleteType="Disabled" runat="server" CssClass="form-control" placeholder="Mobile Number/Password" TextMode="Password"></asp:TextBox>

                      <%--  <asp:DropDownList ID="drpAccYear" runat="server" CssClass="form-control">
                        </asp:DropDownList>--%>
                    </div>
                    <asp:Button ID="btnLogin" CssClass="btn btn-lg btn-login btn-block" 
                        runat="server" Text="Sign in" onclick="btnLogin_Click" />
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
            </form>
        </div>
    <!-- Placed js at the end of the document so the pages load faster -->
    <!--Core js-->
    <script type="text/javascript" src="../Assets/js/jquery.js"></script>
    <script type="text/javascript" src="../Assets/bs3/js/bootstrap.min.js"></script>
    </body>
<!-- Mirrored from bucketadmin.themebucket.net/login.html by HTTrack Website Copier/3.x [XR&CO'2014], Mon, 18 Feb 2019 10:27:47 GMT -->
</html>
