<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="LeaveHistory.aspx.cs" Inherits="NewPortal2023.ESS.LeaveHistory" %>

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

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">LEAVE HISTORY</h3>
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

                                        <%-- <div class="row">
                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Autopostback="true" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="showLoader();"/>
                                        </div>--%>

                                         <label><h3>List Of Leave Applications</h3></label>
                                            <br />

                                        <div class="form-group">
                                        <div class=col-sm-5 style="margin-top: 20px">
                                            <%--<asp:Label ID="lbldrp" Text="Search by Leave Type :-" runat="server" Font-Bold="True" ForeColor="black"></asp:Label>--%>
                                            <label id="lbldrp" class="col-sm-5 labels">Search by Leave Type :-</label>
                                            <asp:DropDownList ID="drpLeaveType" CssClass="form-control input-sm-3" Width="150px" runat="server" OnSelectedIndexChanged="drpLeaveType_SelectedIndexChanged" AutoPostBack="true" ForeColor="#205A94">
                                            </asp:DropDownList>
                                            <%--<asp:DropDownList ID="drpLeaveType" runat="server" CssClass="tableTitle" OnSelectedIndexChanged="drpLeaveType_SelectedIndexChanged" AutoPostBack="true" ForeColor="#205A94">
                                            <asp:ListItem Value="">Select Leave Type</asp:ListItem>
                                            <asp:ListItem Value="PL">Select Leave Type</asp:ListItem>
                                            <asp:ListItem Value="CL">CL</asp:ListItem>
                                            <asp:ListItem Value="SL">SL</asp:ListItem>
                                            <asp:ListItem Value="CO">CO</asp:ListItem>
                                        </asp:DropDownList>--%>
                                            </div>
                                            <div class=col-sm-5 style="margin-top: 20px">
                                              <label id="lbldrpyr" class="col-sm-5 labels">Search by Year :-</label>
                                            <asp:DropDownList ID="drpleaveyear" CssClass="form-control input-sm-3" Width="150px" OnSelectedIndexChanged="drpleaveyear_SelectedIndexChanged" runat="server" AutoPostBack="true" ForeColor="#205A94">
                                                <asp:ListItem Value="2025" Text="2025" />
                                                <asp:ListItem Value="2024" Text="2024" />
                                                
                                                 <asp:ListItem Value="2023" Text="2023" />
                                            </asp:DropDownList>
                                        </div>
                                        </div>
                                      <br />

                                        <div class="form-group">
                                            <br />
                                        </div>

                                        <div class="form-group" id="OtherDetails" visible="true" runat="server" style="margin-top: 0px">
                                            <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblId" runat="Server" Text='<%# Eval("CID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From Date">
                                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFrom" runat="Server" Text='<%# Eval("FROM DATE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To Date">
                                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTo" runat="Server" Text='<%# Eval("TO DATE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Type">
                                                        <ItemStyle CssClass="GridViewItem" Width="90px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("LEAVE TYPE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reason">
                                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReason" runat="Server" Text='<%# Eval("REASON") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Communication Address">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblReason0" runat="Server" Text='<%# Eval("COMMUNICATION ADDRESS") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>--%>
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
                                                            <asp:LinkButton ID="lnkstatus" runat="Server" Text='<%# Eval("STATUS") %>' ForeColor="Blue" OnClick="lnkstatus_Click"/> <%--OnClick="lnkstatus_Click"--%>
                                                            <asp:LinkButton ID="lnkcancel" runat="Server" Text='Cancel Request' OnClick="lnkcancel_Click1" Visible="false" ForeColor="Blue" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approver">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Left" Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapp" runat="Server" Text='<%# Eval("APPROVER") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Updated On">
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
                                                    <table id="EmptyTable1" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                        <tr>
                                                            <td class="GridViewHeader" style="width: 120px">To Date
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 120px">From Date
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 90px">Leave Type
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 130px">Reason
                                                            </td>
                                                            <%-- <td class="GridViewHeader" style="width: 250px">
                                                                            Communication Address
                                                                        </td>--%>
                                                            <td class="GridViewHeader" style="width: 250px">Remarks
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 200px">Status
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 100px">Approver
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 90px">Updated On
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:PostBackTrigger ControlID="lnkOther" />--%>
                                    <%--<asp:PostBackTrigger ControlID="chk2" />
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
