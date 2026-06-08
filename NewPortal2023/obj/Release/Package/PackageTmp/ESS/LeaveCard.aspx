<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="LeaveCard.aspx.cs" Inherits="NewPortal2023.ESS.LeaveCard" %>



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
                            Leave-Card
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

                                        <div class="form-group">
                                            <div class="col-sm-6" style="text-align: center;">
                                                <label for="lblchkAll" class="col-sm-6"><span><b>Month :- </b></span></label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="drpSelectMonth" runat="server" OnSelectedIndexChanged="drpSelectMonth_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-sm-6" style="text-align: center;">
                                                <label for="lblRemark" class="col-sm-6"><span><b>Year Type :-</b></span></label>
                                                <div class="col-sm-6">

                                                    <asp:DropDownList ID="drpSelectFinancialYear" runat="server" OnSelectedIndexChanged="drpSelectFinancialYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <%--<div class="col-sm-5">
                                                    <label for="lblRemark" class="col-sm-3"><span><b>Remark :-</b></span></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" OnTextChanged="txtRemark_TextChanged" Enabled="false" AutoPostBack="true" class="form-control input-sm"></asp:TextBox>
                                                    </div>

                                                </div>--%>
                                        </div>
                                        <div class="form-group">
                                            <br />
                                        
                                        </div>
                                        


                                            <div class="form-group">
                                                <div class="col-sm-12" style="text-align: center;">
                                                      <br />
                                                      
                                                    <img id="loader1" style="display: none; height: 50px; width: 25px; float: right;" src="Assets/progress.gif" />
                                                   <%-- <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search" OnClientClick="showLoader();" OnClick="btnSearch_Click" />--%>
                                                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-info" Text="Generate" OnClientClick="showLoader();" OnClick="btnExport_Click" />

                                                </div>
                                            </div>
                                        <br />
                                        <br />

                                              <hr />
                                             <asp:GridView ID="gvLeaveCards" 
                                                runat="server" AutoGenerateColumns="False"
                                                ToolTip="Leave Card" CellPadding="5" 
                                                 GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="display table table-bordered table-striped dynamic-table">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Leave_Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFrom" runat="Server" Text='<%# Eval("Leave_Name") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Code">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblId" runat="Server" Text='<%# Eval("Leave_Code") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Year">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTo" runat="Server" Text='<%# Eval("Year") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Month">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Month") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Opening_Bal">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Opening_Bal") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Credit") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Debit">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Debit") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Encashed">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Encashed") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Availed">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Availed") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Closing_Bal">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Closing_Bal") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                        <tr>
                                                            <td class="GridViewHeader" style="width: 10%">
                                                                <asp:Literal ID="Literal6" runat="server" Text="No Records Found" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                    </div>
                                </asp:View>
                            </asp:MultiView>
                               <rsweb:ReportViewer ID="rptPrint" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                interactivedeviceinfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                                BackColor="#CCCCFF" Height="600px" Width="100%" ZoomMode="PageWidth" PageCountMode="Actual">
                            </rsweb:ReportViewer>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
