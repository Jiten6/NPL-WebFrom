<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="TestMail.aspx.cs" Inherits="NewPortal2023.ESS.TestMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="max-width:600px; margin:20px auto;">

        <h3>Test Email</h3>

        <!-- Email To -->
        <div style="margin-bottom:10px;">
            <asp:Label ID="lblTo" runat="server" Text="Email To:" />
            <asp:TextBox ID="txtEmailTo" runat="server" CssClass="form-control" Width="100%" />
        </div>

        <!-- Email CC -->
        <div style="margin-bottom:10px;">
            <asp:Label ID="lblCC" runat="server" Text="Email CC:" />
            <asp:TextBox ID="txtEmailCC" runat="server" CssClass="form-control" Width="100%" />
        </div>

        <!-- Subject -->
        <div style="margin-bottom:10px;">
            <asp:Label ID="lblSubject" runat="server" Text="Subject:" />
            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Width="100%" />
        </div>

        <!-- Body -->
        <div style="margin-bottom:10px;">
            <asp:Label ID="lblBody" runat="server" Text="Body:" />
            <asp:TextBox ID="txtBody" runat="server" CssClass="form-control"
                TextMode="MultiLine" Rows="5" Width="100%" />
        </div>

        <!-- Send Button -->
        <div style="margin-top:15px;">
            <asp:Button ID="btnSend" runat="server" Text="Send Email"
                CssClass="btn btn-primary" OnClick="btnSend_Click" />
        </div>

        <!-- Result Label -->
        <div style="margin-top:15px;">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />
        </div>

    </div>

</asp:Content>
