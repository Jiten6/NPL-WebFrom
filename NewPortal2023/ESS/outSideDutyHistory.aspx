<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="outSideDutyHistory.aspx.cs" Inherits="NewPortal2023.ESS.outSideDutyHistory" %>

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
            font-family: Tahoma;
            /* height: 20px; */
            text-decoration: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content" class="rcorners">
        <section class="wrapper">
            <div class="row rcorners" style="margin-left: 20px; margin-top: 10px">
                <section class="panel">
                    <header class="panel-heading" style="background-color: darkcyan">
                        <h3 style="color: white">OutDoor Duty Application History</h3>
                    </header>
                    <div id="blockUI">
                        <asp:ScriptManager ID="smInv" runat="server">
                            <Scripts>
                                <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                <asp:ScriptReference Path="~/ESS/blockUI.js" />
                            </Scripts>
                        </asp:ScriptManager>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="Table9">
                            <tr valign="top">
                                <td valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" class="table5" id="Table7">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                <td class="tableTitle">
                                                    <asp:LinkButton ID="lnkLeave" runat="server" CssClass="tableTitle" ForeColor="#205A94">OutDoor Duty Application History</asp:LinkButton>
                                                </td>
                                            </tr>--%>
                                                <tr>
                                                    <%--                                    <td align="left">
                                    </td>--%>
                                                    <td>
                                                        <asp:Label ID="lbldrp" Text="Search by Leave Type :-" Visible="false" runat="server" Font-Bold="True" ForeColor="black"></asp:Label>
                                                        <asp:DropDownList ID="drpLeaveType" runat="server" Visible="false" OnSelectedIndexChanged="drpLeaveType_SelectedIndexChanged" AutoPostBack="true" ForeColor="#205A94">
                                                        </asp:DropDownList>
                                                        <%--<asp:DropDownList ID="drpLeaveType" runat="server" CssClass="tableTitle" OnSelectedIndexChanged="drpLeaveType_SelectedIndexChanged" AutoPostBack="true" ForeColor="#205A94">
                                            <asp:ListItem Value="">Select Leave Type</asp:ListItem>
                                            <asp:ListItem Value="PL">Select Leave Type</asp:ListItem>
                                            <asp:ListItem Value="CL">CL</asp:ListItem>
                                            <asp:ListItem Value="SL">SL</asp:ListItem>
                                            <asp:ListItem Value="CO">CO</asp:ListItem>
                                        </asp:DropDownList>--%>
                                                    </td>
                                                </tr>

                                                <br />
                                                <br />

                      
                                                        <div id="OtherDetails" visible="true" runat="server">

                                                            <%--<asp:GridView ID="gvLeave" runat="server" BorderWidth="0px" CssClass="table4" HorizontalAlign="Left"
                                                                        ToolTip="Holiday List" OnRowDataBound="gvLeave_RowDataBound" AutoGenerateColumns="False">--%>
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
                                                                    <asp:TemplateField HeaderText="From DateTime">
                                                                        <ItemStyle CssClass="GridViewItem" Width="250px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFrom" runat="Server" Text='<%# Eval("FROM_DT") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="To DateTime">
                                                                        <ItemStyle CssClass="GridViewItem" Width="250px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTo" runat="Server" Text='<%# Eval("TO_DATE") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Out Door Type">
                                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOdType" runat="Server" Text='<%# Eval("ODTYPE") %>' />

                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false" HeaderText="Reason">
                                                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblReason" runat="Server" Text='<%# Eval("REASON_ID") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Create DateTime">
                                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblReason0" runat="Server" Text='<%# Eval("CREATEDDT") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblReason1" runat="Server" Text='<%# Eval("REMARKS") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                    <%-- <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                                                                        DataNavigateUrlFormatString="LeaveApplication.aspx?Id={0}" 
                                                                        DataTextField="Status" HeaderText="Status" SortExpression="cid" />--%>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkstatus" runat="Server" Text='<%# Eval("STATUS") %>' OnClick="lnkstatus_Click" />
                                                                            <asp:LinkButton ID="lnkcancel" runat="Server" Text=' / Cancel Request' OnClick="lnkcancel_Click1" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                    <%--  <asp:TemplateField HeaderText="Approver">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Left" Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblapp" runat="Server" Text='<%# Eval("APPROVER") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Updated On" Visible="false">
                                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUP" runat="Server" Text='<%# Eval("UPDATED") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <EmptyDataTemplate>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>

                                                        </div>
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </section>
            </div>
        </section>
    </section>
</asp:Content>
