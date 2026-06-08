<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="LeaveStructure.aspx.cs" Inherits="NewPortal2023.ESS.Leave_LeaveStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>
    <style type="text/css">
        .tableTitle {
            font-weight: bold;
            font-size: 10pt;
            vertical-align: middle;
            color: #205a94;
            font-family: Tahoma;
            height: 20px;
            text-decoration: none;
        }

        .table4 {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
            background-color: #FFFFFF;
        }

        .total {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 1px;
            padding-bottom: 1px;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            height: 10px;
            width: 10%;
            font-weight: bold;
            background-color: #efefef;
        }

            .total TD {
                font-weight: bold;
                height: 20px;
                background-color: #efefef;
            }

        .GridViewItem {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 1px;
            padding-bottom: 1px;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            height: 10px;
            width: 10%;
            font-weight: normal;
        }

        .GridViewHeader {
            font-weight: bold;
            font-size: 8.3;
            filter: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#FFFFFF,endColorStr=#BBDDFF);
            text-transform: capitalize;
            color: #545454;
            border-top: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-bottom: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            text-align: center;
        }

        .input {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 7.5pt;
            color: #004B97;
            border-top: auto inset #CCCCCC;
            border-right: auto inset #DFDFDF;
            border-left: auto inset #CCCCCC;
            border-bottom: auto inset #DFDFDF;
            border-top-color: #CCCCCC;
            border-right-color: #DFDFDF;
            border-bottom-color: #DFDFDF;
            border-left-color: #CCCCCC;
            border-top-style: inset;
            border-right-style: inset;
            border-bottom-style: inset;
            border-left-style: inset;
            border-style: inset;
            border-color: #999999 #CCCCCC #CCCCCC #999999;
            border-top-width: 1px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
        }

        .GridViewEmpty {
            padding: 0px;
            margin: 0;
            border: solid 1px #d3dbdf;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            border-top: 1px solid #d3dbdf;
            width: 100%;
            border-spacing: 0px;
        }

        .title {
            font-weight: bold;
            font-size: 11pt;
            vertical-align: middle;
            text-transform: capitalize;
            color: #205a94;
            font-family: Tahoma; /* HEIGHT: 20px; */
            text-decoration: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Leave Structure</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" EnableViewState="true">
                                <ContentTemplate>
                                    <div id="form1" runat="server">
                                        <div id="divAlertSucc" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessageSucc" runat="server"></asp:Label>
                                        </div>
                                        <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>

                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkLeave" runat="server" Font-Size="Medium" ForeColor="DarkCyan">Leave Structure Year 2025-26</asp:LinkButton>
                                        </div>

                                        <div class="form-group" id="btnDtails" runat="server" visible="false">
                                            <asp:Button ID="btnExec" runat="server" Text="Executive" CssClass="tableTitle" ForeColor="#205A94" OnClick="btnExec_Click" />
                                            <asp:Button ID="btnWorkMen" runat="server" Text="WorkMen" CssClass="tableTitle" ForeColor="#205A94" OnClick="btnWorkMen_Click" />
                                        </div>

                                        <div class="form-group" id="OtherDetails" visible="false" runat="server">
                                            <%--<asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                CssClass="table4" HorizontalAlign="Left" ToolTip="Leave Structure" OnRowDataBound="gvLeave_RowDataBound">--%>
                                            <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvLeave_RowDataBound"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblId" runat="Server" Text='<%# Eval("CID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCheck" runat="Server" Text='<%# Eval("ISCARRY") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Code">
                                                        <ItemStyle CssClass="GridViewItem" Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCode" style="display: block; text-align: center;" runat="Server" Text='<%# Eval("LEAVE_CODE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Name">
                                                        <ItemStyle CssClass="GridViewItem" Width="400px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" style="display: block; text-align: center;" runat="Server" Text='<%# Eval("LEAVE_NAME") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Allocated">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAll" runat="server" Enabled="false" CssClass="input" MaxLength="2" oncopy="return false"
                                                                onpaste="return false" onkeypress="return validateFloat(event, 0);" ToolTip="Enter allocated details"
                                                                Text='<%# Eval("ALLOCATED") %>' Width="130px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is CarryForward">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSel" runat="server" ToolTip="Select if carry forwared leave"></asp:CheckBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                        <tr>
                                                            <td class="GridViewHeader" style="width: 100px">Leave Code
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 400px">Leave Name
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 130px">Allocated
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 130px">Is CarryForward
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>

                                       &emsp;
                                       &emsp;

                                        <div style="width: 100%; text-align: center;">
                                            <asp:Button ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click" class="btn btn-success" Style="width: 80px; height: 30px; font-weight: bold;"
                                                Text="Submit" />
                                        </div>



                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:PostBackTrigger ControlID="chk1" />
                                    <asp:PostBackTrigger ControlID="chk2" />
                                    <asp:PostBackTrigger ControlID="chk3" />
                                    <asp:PostBackTrigger ControlID="chk4" />
                                    <asp:PostBackTrigger ControlID="chk5" />
                                    <asp:PostBackTrigger ControlID="chk6" />
                                    <asp:PostBackTrigger ControlID="chk7" />
                                    <asp:PostBackTrigger ControlID="chk8" />
                                    <asp:PostBackTrigger ControlID="radioPrimary1" />
                                    <asp:PostBackTrigger ControlID="radioPrimary2" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="drpType" />
                                    <asp:PostBackTrigger ControlID="btnCalTtl" />--%>
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>


</asp:Content>
