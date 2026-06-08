<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="OutSideDutyApprovalView.aspx.cs" Inherits="NewPortal2023.ESS.OutSideDutyApprovalView" EnableEventValidation="false"%>

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
            font-family: Tahoma; /* height: 20px; */
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
                            <h3 style="color: white">OutDoor Duty Pending Approval</h3>
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
                                        <div id="divAlert" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>
                                        <div id="divAlertDan" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessageDan" runat="server"></asp:Label>
                                        </div>

                                        <div>
                                            <asp:Label ID="lbldrp" Text="Search by Leave Type :-" Visible="false" runat="server"
                                                Font-Bold="True" ForeColor="black"></asp:Label>
                                            <asp:DropDownList ID="drpLeaveType" runat="server" Visible="false" OnSelectedIndexChanged="drpLeaveType_SelectedIndexChanged"
                                                AutoPostBack="true" ForeColor="#205A94">
                                            </asp:DropDownList>
                                        </div>

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
                                                    <asp:TemplateField Visible="true" HeaderText="Action">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Code">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpMId" runat="Server" Text='<%# Eval("[EMP_MID]") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMPNAME" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Code" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpAId" runat="Server" Text='<%# Eval("EMP_AID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
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
                                                    <asp:TemplateField Visible="false" HeaderText="Reason">
                                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReason" runat="Server" Text='<%# Eval("REASON_ID") %>' />
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
                                                    <asp:TemplateField Visible="false" HeaderText="Status">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkstatus" runat="Server" Text='<%# Eval("STATUS") %>' OnClick="lnkstatus_Click" />
                                                            <asp:LinkButton ID="lnkcancel" runat="Server" Text=' / Cancel Request' OnClick="lnkcancel_Click1"
                                                                Visible="false" />
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
                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="Server" Text='View' OnClick="lnkstatus_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Updated On" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUP" runat="Server" Text='<%# Eval("UPDATED") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cr_Date" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCr_Date" runat="Server" Text='<%# Eval("CREATEDDT") %>' />
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
                                                            <td class="GridViewHeader" style="width: 130px">Reason
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 250px">Remarks
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 200px">Status
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 90px">Updated On
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>


                                        <div class="form-group" style="text-align: center;">
                                            <div style="display: inline-block; text-align: left;">
                                                <label class="labels" id="lblUpload1" runat="server" style="margin-top: 20px">Action :</label>
                                            </div>
                                            <div style="display: inline-block; text-align: left;">
                                                <asp:DropDownList ID="drpStatus" runat="server" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px"></asp:DropDownList>
                                            </div>
                                            <%--<div id="divRemark" runat="server" visible="false" class="form-group">
                                            <asp:TextBox ID="txtEmpMid" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>
                                        </div>--%>
                                        </div>



                                        <div class="form-group col-sm-12" style="text-align: center;">
                                            <asp:Button ID="btnSubmit" Enabled="false" CssClass="btn btn-success" runat="server" Text="Submit" AutoPostBack="true" OnClick="btnSubmit_Click" OnClientClick="showLoader();" />
                                        </div>



                                        <div class="form-group col-sm-12" style="text-align: center;">
                                            <asp:Button ID="lnkApp" CssClass="btn btn-warning" runat="server" Text="View OutDoor Duty Approved List" AutoPostBack="true" OnClick="lnkApp_Click" OnClientClick="showLoader();" />
                                        </div>

                                        <div class="col-sm-12" style="text-align: center; margin-top: 20px">
                                            <asp:Button ID="btnExport" Visible="false" runat="server" CssClass="btn btn-primary" Text="Export" OnClick="btnExport_Click" />
                                        </div>

                                        <div id="divApprOD" visible="true" runat="server">

                                            <%--<asp:GridView ID="gvApprOD" runat="server" BorderWidth="0px" CssClass="table4" HorizontalAlign="Left"
                                            ToolTip="Approved List" AutoGenerateColumns="False">--%>
                                            <asp:GridView ID="gvApprOD" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblId" runat="Server" Text='<%# Eval("CID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Employee Code">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpMId" runat="Server" Text='<%# Eval("[EMP_MID]") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMPNAME" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Code" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpAId" runat="Server" Text='<%# Eval("EMP_AID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
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
                                                    <asp:TemplateField Visible="false" HeaderText="Reason">
                                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReason" runat="Server" Text='<%# Eval("REASON_ID") %>' />
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
                                                     <asp:TemplateField HeaderText="Approved Date" Visible="true">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApprDate" runat="Server" Text='<%# Eval("UPDATED") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                                                                        DataNavigateUrlFormatString="LeaveApplication.aspx?Id={0}" 
                                                                        DataTextField="Status" HeaderText="Status" SortExpression="cid" />--%>
                                                    <asp:TemplateField Visible="true" HeaderText="Status">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkstatus" runat="Server" Text='<%# Eval("STATUS") %>'></asp:Label>
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
                                                    <%-- <asp:TemplateField HeaderText="Approved Date" Visible="true">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApprDate" runat="Server" Text='<%# Eval("APPR_DATE") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>--%>
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
                                                            <td class="GridViewHeader" style="width: 130px">Reason
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 250px">Remarks
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 200px">Status
                                                            </td>
                                                            <%--<td class="GridViewHeader" style="width: 90px">Approved Date
                                                                        </td>--%>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                    <asp:PostBackTrigger ControlID="lnkApp" />
                                    <asp:PostBackTrigger ControlID="drpStatus" />
                                    <asp:PostBackTrigger ControlID="btnExport" />

                                    <%-- <asp:PostBackTrigger ControlID="chk4" />
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
