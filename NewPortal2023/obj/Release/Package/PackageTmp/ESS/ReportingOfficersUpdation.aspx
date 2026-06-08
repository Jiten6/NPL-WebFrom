<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="ReportingOfficersUpdation.aspx.cs" Inherits="NewPortal2023.ESS.ReportingOfficersUpdation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="Common.js" type="text/javascript"></script>

    <style>
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

        /*@keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        #loadingOverlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            z-index: 9999; 
            display: none; 
        }

        .loader-container {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }

        #loader {
            height: 50px;
            width: 50px; 
            animation: spin 1s linear infinite; 
        }*/
    </style>

    <%--<script type="text/javascript">

        function showLoader() {
            document.getElementById('loadingOverlay').style.display = 'block';
        }

        // Function to hide the loading overlay
        function hideLoader() {
            document.getElementById('loadingOverlay').style.display = 'none';
        }


    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Reporting officer</h3>
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
                                    <div id="loadingOverlayTarget" runat="server">
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

                                        <%-- <div id="loadingOverlay">
                                            <div class="loader-container">
                                                <img id="loader" src="Assets/load.gif" alt="Loading..." />
                                            </div>
                                        </div>--%>




                                        <div class="form-group container text-left col-sm-12" id="tr" runat="server" visible="true" style="text-align: left;">
                                            <%--       <asp:TextBox ID="txtsrEmpCode" runat="server" CssClass="form-control input-sm" Width="150px" Placeholder="Enter Employee Code"></asp:TextBox>--%>
                                            <asp:DropDownList ID="drpEmpType" runat="server" CssClass="select2" Width="25%" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpEmpType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <%--<asp:Button ID="btnSearch" CssClass="btn btn-info" runat="server" Text="Search" OnClick="btnSearchOnClcik" />--%>
                                            <%--<asp:Button ID="btnSearch" CssClass="btn btn-info" runat="server" Text="Search" OnClientClick="showLoadingOverlay();" OnClick="btnSearchOnClcik" />--%>
                                            <%--<asp:Button ID="btnSearch" CssClass="btn btn-info" runat="server" Text="Search" OnClientClick="showLoadingOverlay();" OnClick="btnSearchOnClcik" data-toggle="modal" data-target="#loadingOverlay" />--%>

                                            <%--<img id="loader" style="display: none; height: 50px; width: 25px; margin-left: 50%;" src="Assets/load.gif" />--%>
                                            <br />
                                            <asp:Button ID="btnSearch" CssClass="btn btn-info" runat="server" Text="Search" OnClick="btnSearchOnClcik" OnClientClick="showLoader();" />
                                            <asp:Label ID="lblReportingCode" runat="server" Style="color: RED; font-size: 12px;"></asp:Label>
                                        </div>



                                        <div class="form-group col-sm-12" style="text-align: left;">
                                            <asp:Button ID="btncheck" CssClass="btn btn-warning" runat="server" Text="Check Reporting Officers" Autopostback="true" OnClick="btnCheckOnClcik" OnClientClick="showLoader();" />
                                            <asp:Button ID="btnUpdateOption" runat="server" Text="Update Reporting Officer" Visible="false" OnClick="btnUpdateOptionOnClcik" />
                                        </div>

                                        <div class="form-group" id="InvDetails" visible="true" runat="server">
                                            <div class="form-group" id="tblRo" runat="server" visible="false">
                                                <%--<asp:GridView ID="gvListReportingOfficer" runat="server" AutoGenerateColumns="False"
                                                    BorderWidth="0px" CssClass="table4 rowhover" DataKeyNames="Emp_Code" Width="100%">--%>
                                                <asp:GridView ID="gvListReportingOfficer" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpId" runat="Server" Text='<%# Eval("Emp_Code") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REPORTING OFFICERS CODE">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEmpMID" runat="Server" Autopostback="true" ForeColor="Blue" Text='<%# Eval("Emp_Code") %>' OnClick="lnkbutonListRO" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REPORTING OFFICERS NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="Server" Text='<%# Eval("Emp_Name") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" HorizontalAlign="Center" />
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
                                                                    <asp:Literal ID="Literal6" runat="server" Text="No Records Founds." />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                            <div class="form-group" id="tblEmp" runat="server" visible="false">
                                                <%--<asp:GridView ID="gvEmplist" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4 rowhover" DataKeyNames="EMP_MID" Width="100%">--%>
                                                <asp:GridView ID="gvEmplist" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpId" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EMPLOYEE CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpMID" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="Server" Text='<%# Eval("EMP_FNAME") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" HorizontalAlign="Center" />
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
                                                                    <asp:Literal ID="Literal6" runat="server" Text="No Records Founds." />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                            <div class="form-group" id="tblup" runat="server" visible="false">
                                                <div class="form-group">
                                                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>&nbsp;
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblEmpCode" runat="server" Style="color: Black" Text="Enter Employee Code :-"></asp:Label>
                                                    <asp:TextBox ID="txtEmpCode" runat="server" CssClass="input" Placeholder="Employee Code" OnTextChanged="txtEmpCodeOnTextChanged" AutoPostBack="True"></asp:TextBox><br />
                                                    <br />
                                                    <asp:Label ID="lblEmpN" runat="server" Style="color: RED"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblRoCode" runat="server" Style="color: Black" Text="Enter Reporting Officer Code :-"></asp:Label>
                                                    <asp:TextBox ID="txtRoCode" runat="server" CssClass="input" Placeholder="Reporting Officer Code" OnTextChanged="txtRoCodeOnTextChanged" AutoPostBack="True"></asp:TextBox><br />
                                                    <br />
                                                    <asp:Label ID="lblRON" runat="server" Style="color: RED"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" AutoPostBack="True" OnClick="btnUpdateOnClcik"
                                                        Style="margin-left: 100%; background-color: LightGreen" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpdate" />
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                    <asp:PostBackTrigger ControlID="btnUpdateOption" />
                                    <asp:PostBackTrigger ControlID="btncheck" />
                                    <%--<asp:PostBackTrigger ControlID="lnkEmpMID" />
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
        <script type="text/javascript">
            // Initial load
            $(document).ready(function () {
                $('#<%= drpEmpType.ClientID %>').select2();
            });

            // After every postback
            Sys.Application.add_load(function () {
                $('#<%= drpEmpType.ClientID %>').select2();
            });
        </script>

    </section>

</asp:Content>
