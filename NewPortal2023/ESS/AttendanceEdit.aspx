<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="AttendanceEdit.aspx.cs" Inherits="NewPortal2023.ESS.AttendanceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ValidFile() {
            var file = $("#fupldDocument").val();
            if (file == "") {
                IN4_DisplayErrorMessage("Browse document.");
                return false;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ToggleVisible() {
            var trdate = document.getElementById("trdate");
            var trtime = document.getElementById("trtime");
            if (trdate.style.display == 'block') {
                trdate.style.display = 'none';
                trtime.style.display = 'none';
            }
            else {
                trdate.style.display = 'block';
                trtime.style.display = 'block';
            }
        }
    </script>
    <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/litepicker.js" type="text/javascript"></script>
    <div id="blockUI">
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
                background-color: #eeeeee;
            }

            .report {
                font-size: 11px;
            }

            .table4 TR {
                padding-right: 1px;
                padding-left: 1px;
                padding-bottom: 1px;
                padding-top: 1px;
                background-color: #fdfbf0;
            }

            .tableinnerColhead TD {
                filter: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#FFFFFF,endColorStr=#C1CDDD);
                vertical-align: middle;
                height: 20px;
                text-align: center;
            }

            .table5 {
                border-right: 0px;
                border-top: 0px;
                border-left: 0px;
                border-bottom: 0px;
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

            .style1 {
                width: 5%;
            }
        </style>
        <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
        <script src="../js/litepicker.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">ATTENDANCE RECTIFICATION DETAILS</h3>
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

                                        <div class="form-group" style="margin: 10px">
                                            <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Employee Code" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCID" runat="Server" Text='<%# Eval("EMP_CODE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="150px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("Emp_Name") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="150px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIn" runat="Server" Text='<%# Eval("DATE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift Schedule">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="150px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOut" runat="Server" Text='<%# Eval("Shift_Schedule") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="In Time">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltothr" runat="Server" Text='<%# Eval("Time_In") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Out Time">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOuts" runat="server" Text='<%# Eval("Time_Out") %>' Width="130px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Out Time (Edit)">
                                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtOut" runat="server" CssClass="input" MaxLength="8" Text='<%# Eval("OUT TIME EDIT") %>' Width="130px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("ATT_STATUS") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Left" Width="100px" />
                                                        <ItemTemplate>
                                                            <%--<asp:TextBox ID="txtRem" runat="server" CssClass="input" MaxLength="8" Text='<%# Eval("REMARKS") %>' Width="130px"></asp:TextBox>--%>
                                                            <asp:Label ID="lblRem" runat="Server" Text='<%# Eval("REMARKS") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                        <ItemTemplate>
                                                            <%--<asp:TextBox ID="txtRem" runat="server" CssClass="input" MaxLength="8" Text='<%# Eval("Status") %>' Width="130px"></asp:TextBox>--%>
                                                            <asp:Label ID="lblRems" runat="Server" Text='<%# Eval("Status") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <%--<table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                            <tr>
                                                                                <td class="GridViewHeader" style="width: 100px">Date
                                                                                </td>
                                                                                <td class="GridViewHeader" style="width: 100px">In Time
                                                                                </td>
                                                                                <td class="GridViewHeader" style="width: 100px">Out Time
                                                                                </td>
                                                                                <td class="GridViewHeader" style="width: 100px">Total Hrs
                                                                                </td>
                                                                                <td class="GridViewHeader" style="width: 300px">Remarks
                                                                                </td>
                                                                            </tr>
                                                                        </table>--%>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
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
