<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.master" AutoEventWireup="true" CodeBehind="OtherDuesNomination.aspx.cs" Inherits="NewPortal2023.ESS.OtherDuesNomination" %>

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
                            Other Dues Nomination
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
                                                        <div class="col-sm-6">
                                                            <a href="NominationInstructions.aspx" style="font-size: 20px; color: blue" class="underline">Nomination Instructions</a>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="form-group">
                                                        <div class="col-lg-12">
                                                            <h3 class="page-header" style="text-align: center; color: black;">Other Dues Nomination Form</h3>
                                                        </div>
                                                    </div>

                                                    <div id="div2" runat="server">
                                                        <div class="form-group">
                                                            <h4 style="color: dodgerblue; text-align: left">Employee Details</h4>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Employee Name</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtName" runat="server" class="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                            <label for="lblTranche" class="col-sm-3  labels">Employee Code</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtEmpCode" runat="server" class="form-control input-sm"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Designation</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtDesignation" runat="server" class="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                            <label for="lblTranche" class="col-sm-3  labels">Location</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtLocation" runat="server" class="form-control input-sm"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <label for="lblTranche" class="col-sm-3  labels">Permanent Address</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" Height="47px" runat="server" class="form-control input-sm"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <hr />
                                                        </div>
                                                    </div>

                                                    <div id="div1" runat="server">
                                                        <div class="form-group">
                                                            <h4 style="color: dodgerblue; text-align: left">Nominee Details</h4>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                                                                <Columns>
                                                                    <asp:TemplateField ShowHeader="true">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" class="form-control input-sm" runat="Server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nominee Name">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineeName" class="form-control input-sm"  runat="server" Text=''></asp:TextBox><%-- <%# Eval("NOMINEE_NAME") %>--%>
                                                                        </ItemTemplate>
                                                                       <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nominee Address">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineeAddr" class="form-control input-sm" runat="server" Text=''></asp:TextBox>
                                                                            <%--<%# Eval("NOMINEE_ADDR") %>--%>
                                                                        </ItemTemplate>
                                                                       <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Relationship">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpRelationship" class="form-control input-sm" runat="server">
                                                                                <%-- SelectedValue='' <%# Eval("RELATIONSHIP") %>--%>
                                                                                <asp:ListItem Value="">Select Status</asp:ListItem>
                                                                                <asp:ListItem Value="Mother">Mother</asp:ListItem>
                                                                                <asp:ListItem Value="Father">Father</asp:ListItem>
                                                                                <asp:ListItem Value="Spouse">Spouse</asp:ListItem>
                                                                                <asp:ListItem Value="Son">Son</asp:ListItem>
                                                                                <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Age">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineeAge" class="form-control input-sm" runat="server" Text='' onkeypress="return validateFloat(event, 0);"></asp:TextBox>
                                                                            <%--<%# Eval("NOMINEE_AGE") %>--%>
                                                                        </ItemTemplate>
                                                                       <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Percentage">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineePerc" class="form-control input-sm" runat="server" Text='' onkeypress="return validateFloat(event, 0);"></asp:TextBox>
                                                                            <%--<%# Eval("PERCANTAGE") %>--%>
                                                                        </ItemTemplate>
                                                                      <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Guardian Name">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineeGuardian" class="form-control input-sm" runat="server" Text=''></asp:TextBox>
                                                                            <%-- <%# Eval("GUARDIAN") %>--%>
                                                                        </ItemTemplate>
                                                                      <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDocDataId" runat="Server" class="form-control input-sm" Text='' />
                                                                            <%--<%# Eval("Doc_Data_Id") %>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <td style="width: 10%" class="Title">
                                                                                <asp:Literal ID="Literal6" runat="server" Text="Click on 'Add Row' link to add nominees." />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="lnkBtnAddDocRow" Text="Add row" runat="server" Font-Bold="true"
                                                                OnClick="lnkBtnAddDocRow_Click" CssClass="Title" />
                                                            <asp:Label ID="Label2" runat="server" Text="|" Font-Bold="true" ForeColor="blue" />
                                                            <asp:LinkButton ID="lnkBtnDeleteDocRow" runat="server" Font-Bold="true" OnClick="lnkBtnDeleteDocRow_Click"
                                                                Text="Delete Selected" CssClass="Title" />
                                                        </div>
                                                    </div>




                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <hr />
                                                        </div>
                                                    </div>

                                                    <div id="div3" runat="server">
                                                        <div class="form-group">
                                                            <h4 style="color: dodgerblue; text-align: left">Contingent Nominee Details</h4>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">

                                                        <div class="form-group">
                                                            <asp:GridView ID="gvNomineesCont" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-striped">
                                                                <Columns>
                                                                    <asp:TemplateField ShowHeader="true">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" class="form-control input-sm" runat="Server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nominee Name">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineeName" class="form-control input-sm" runat="server" Text=''></asp:TextBox><%-- <%# Eval("NOMINEE_NAME") %>--%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nominee Address">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineeAddr" class="form-control input-sm" runat="server" Text=''></asp:TextBox>
                                                                            <%--<%# Eval("NOMINEE_ADDR") %>--%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Relationship">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpRelationship" class="form-control input-sm" runat="server">
                                                                                <%-- SelectedValue='' <%# Eval("RELATIONSHIP") %>--%>
                                                                                <asp:ListItem Value="">Select Status</asp:ListItem>
                                                                                <asp:ListItem Value="Mother">Mother</asp:ListItem>
                                                                                <asp:ListItem Value="Father">Father</asp:ListItem>
                                                                                <asp:ListItem Value="Spouse">Spouse</asp:ListItem>
                                                                                <asp:ListItem Value="Son">Son</asp:ListItem>
                                                                                <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Age">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineeAge" runat="server" class="form-control input-sm" Text='' onkeypress="return validateFloat(event, 0);"></asp:TextBox>
                                                                            <%--<%# Eval("NOMINEE_AGE") %>--%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Percentage">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineePerc" runat="server" class="form-control input-sm" Text='' onkeypress="return validateFloat(event, 0);"></asp:TextBox>
                                                                            <%--<%# Eval("PERCANTAGE") %>--%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Guardian Name">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNomineeGuardian" runat="server" class="form-control input-sm" Text=''></asp:TextBox>
                                                                            <%-- <%# Eval("GUARDIAN") %>--%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                        <HeaderStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDocDataId" runat="Server" class="form-control input-sm" Text='' />
                                                                            <%--<%# Eval("Doc_Data_Id") %>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <td style="width: 10%" class="Title">
                                                                                <asp:Literal ID="Literal6" runat="server"  Text="Click on 'Add Row' link to add nominees." />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="lnkBtnAddDocRowCont" Text="Add row" runat="server" Font-Bold="true"
                                                                OnClick="lnkBtnAddDocRowCont_Click" CssClass="Title" />
                                                            <asp:Label ID="Label1" runat="server" Text="|" Font-Bold="true" ForeColor="blue" />
                                                            <asp:LinkButton ID="lnkBtnDeleteDocRowCont" runat="server" Font-Bold="true" OnClick="lnkBtnDeleteDocRowCont_Click"
                                                                Text="Delete Selected" CssClass="Title" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <hr />
                                                        </div>
                                                    </div>

                                                    <div id="div4" runat="server">
                                                        <div class="form-group">
                                                            <h4 style="color: dodgerblue; text-align: left">Witnesses</h4>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <table  CssClass="table table-bordered table-striped">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <table class="nominee"  CssClass="table table-bordered table-striped">
                                                                            <thead style="font-weight: bold; text-align: center;">
                                                                                <tr>
                                                                                    <td>Employee No.
                                                                                    </td>
                                                                                    <td>Employee Name
                                                                                    </td>
                                                                                    <td>Address
                                                                                    </td>
                                                                                    <td>Witness Confirmation
                                                                                    </td>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWitnessNo" class="form-control input-sm" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWitnessName" class="form-control input-sm" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWitnessAddr" class="form-control input-sm" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWitnessConfirm" class="form-control input-sm" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWitnessNo2" class="form-control input-sm" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWitnessName2" class="form-control input-sm" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWitnessAddr2" class="form-control input-sm" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWitnessConfirm2" class="form-control input-sm" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                                <asp:LinkButton ID="btnDownloadForm" runat="server" Style="font-weight: bold; font-size: 16px; color: blue" Class="underline"
                                                                    OnClick="btnDownloadForm_Click">Download Saved Form</asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-2" style="text-align: center;">
                                                                <img id="loader1" style="display: none; height: 50px; width: 25px; float: right;" src="Assets/progress.gif" />
                                                                <asp:Button ID="btnForm" runat="server" Text="Upload Form" CssClass="btn btn-primary" OnClick="btnUploadForm_Click" OnClientClick="showLoader();" />


                                                            </div>

                                                        </div>


                                                    </div>
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
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
