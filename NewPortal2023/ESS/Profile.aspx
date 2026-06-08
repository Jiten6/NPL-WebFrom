<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="NewPortal2023.ESS.Profile" %>

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

        .tableTitleSubHeader {
            font-weight: bold;
            font-size: 9pt;
            vertical-align: middle;
            color: darkcyan;
            font-family: Tahoma;
            height: 18px;
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

        .employee-details {
            width: 100%;
        }

        .section-title u {
            /* Style for section title, underlined text */
        }

        .field {
            display: flex;
            margin-bottom: 10px;
        }

        .label {
            width: 30%;
            vertical-align: top;
            font-weight: bold;
        }

        .value {
            flex: 1;
        }
    </style>

    <style>
        hr {
            width: 90%;
            border-style: solid;
            border-color: darkcyan;
            margin-top: 20px;
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
                            <h3 style="color: white">Profile</h3>
                        </header>
                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>


                            <!-- Profile Image on the Left -->
                            <div style="float: left; margin-right: 20px;">
                                <asp:Image ID="imgProfile" runat="server" Width="150px" Height="150px" CssClass="img-thumbnail" />
                            </div>

                            <div class="col-lg-12">
                                <h3 class="page-header" style="color: darkcyan">Employee Details</h3>
                            </div>
                            <div class="form-horizontal" style="margin: 20px">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder1_lblEmpcode" class="col-sm-3 labels">Employee Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtEmpCode" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>

                                    <label for="ContentPlaceHolder1_lblEmpname" class="col-sm-3 labels">Employee Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtEmpName" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="ContentPlaceHolder1_lblBirthD" class="col-sm-3 labels">Birth Date</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtBirthDate" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>

                                    <label for="ContentPlaceHolder1_lblJoinD" class="col-sm-3 labels">Join Date</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtJoinDate" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="ContentPlaceHolder1_lblEmailID" class="col-sm-3 labels">Email ID</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtEmailId" runat="server" Enabled="false" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>

                                    <label for="ContentPlaceHolder1_lblPAN" class="col-sm-3 labels">PAN Number</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPanNumber" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="ContentPlaceHolder1_lblDesign" class="col-sm-3 labels">Designation</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtDesgination" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>

                                    <label for="ContentPlaceHolder1_lblDept" class="col-sm-3 labels">Department</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtDepartment" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="ContentPlaceHolder1_lblLoctn" class="col-sm-3 labels">Location</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtLocation" runat="server" Enabled="false" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-12">
                                <h3 class="page-header" style="color: darkcyan">Reporting Manager</h3>
                            </div>
                            <div class="form-horizontal" style="margin: 20px">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder1_lblMangrCd" class="col-sm-3 labels">Manager Code</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtHDCode" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>

                                    <label for="ContentPlaceHolder1_lblMangrNm" class="col-sm-3 labels">Manager Name</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtHDName" runat="server" Enabled="false" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div id="OtherDetails" visible="true" runat="server" class="col-lg-12">
                                <h3 class="page-header" style="color: darkcyan">Team Members</h3>

                                <div style="margin: 30px">

                                    <asp:GridView ID="gvProfileDteails" runat="server" AutoGenerateColumns="False"
                                        HorizontalAlign="Left" CellPadding="5"
                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Employee Code">
                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="500px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpMId" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                    <%--Text='<%# Eval("[EMP_MID]") %>' />--%>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridViewHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="500px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEMPNAME" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
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
                                                    <td class="GridViewHeader" style="width: 500px">Employee Code
                                                    </td>
                                                    <td class="GridViewHeader" style="width: 500px">Employee Name
                                                    </td>

                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>

                                </div>
                            </div>


                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
