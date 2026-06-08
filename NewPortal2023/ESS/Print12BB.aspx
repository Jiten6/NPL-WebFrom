<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="Print12BB.aspx.cs" Inherits="NewPortal2023.ESS.Print12BB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
        Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Invetsment Declaration</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>




                            <a href="TaxSimulator.aspx">&lt;&lt; Back to Tax Calculator</a>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMessagecreate" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <rsweb:ReportViewer ID="rptPrint" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                                            BackColor="#CCCCFF" Height="1300px" Width="100%" ZoomMode="FullPage" PageCountMode="Actual">
                                            <%--<LocalReport ReportPath="ImageLib\Quotation.rdlc">
                    </LocalReport>--%>
                                        </rsweb:ReportViewer>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>
</asp:Content>
