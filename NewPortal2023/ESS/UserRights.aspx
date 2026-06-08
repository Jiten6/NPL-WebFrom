<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="UserRights.aspx.cs" Inherits="NewPortal2023.ESS.UserRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Datatables-->
    <link href="bootstrap/vendors/datatables.net-bs4/css/dataTables.bootstrap4.min.css"
        rel="stylesheet" />
    <link href="bootstrap/vendors/datatables.net-buttons-bs4/css/buttons.bootstrap4.css"
        rel="stylesheet" />
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
    <script type="text/javascript">
        function showLoader() {
            document.getElementById("loader").style.display = 'block';
        }

    </script>
    <script type="text/javascript" src="assets/js/dynamic_table_init.js"></script>


    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        function showLoader() {
            document.getElementById("loadingOverlay").style.display = 'block';
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <div class="d-flex flex-wrap">
                            <div class="flex-grow-1">
                                <header class="panel-heading">
                                    User Rights
                                </header>
                                <div id="blockUI" class="panel-body">
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
                                                    <button data-dismiss="alert" class="close close-sm" type="button">
                                                        <i class="fa fa-times"></i>
                                                    </button>
                                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-12">
                                                        <h3 class="page-header" style="text-align: left; color: blue;"><u>User Rights</u></h3>
                                                    </div>
                                                </div>

                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <label for="drpBank" class="col-sm-3  labels">Select Profile</label>
                                                        <div class="col-sm-3">
                                                            <asp:DropDownList ID="drpEmp" runat="server" CssClass="form-control input-sm-3" AutoPostBack="true" OnSelectedIndexChanged="drpEmp_SelectedIndexChanged" onchange="showLoader();">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-12">
                                                        <hr />
                                                    </div>
                                                </div>


                                                <%--       <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" EnableViewState="true"
                                                HeaderStyle-BackColor="LightSteelBlue"
                                                OnRowDataBound="gv_RowDataBound" OnPreRender="gv_PreRender"
                                                class="table table-bordered table-striped">
                                                <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                <PagerStyle CssClass="pagination-ys" />--%>

                                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" OnPreRender="gvDataPointList_PreRender" class="table table-bordered table-striped"
                                                    DataKeyNames="EMP_ID" OnRowDataBound="gv_RowDataBound"
                                                    OnRowEditing="gvDataPointList_RowEditing" HeaderStyle-Font-Size="Medium" HeaderStyle-BackColor="LightSteelBlue "
                                                    OnRowUpdating="gvDataPointList_RowUpdating" OnRowCancelingEdit="gvDataPointList_RowCancelingEdit">

                                                    <Columns>
                                                        <asp:TemplateField Visible="true">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" runat="Server" OnClick="showLoader();" />
                                                            </HeaderTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="20px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="Server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Code" Visible="true">
                                                            <ItemStyle CssClass="GridViewItem" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMid" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name" Visible="true">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="Server" Text='<%# Eval("EMP_AID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMenuID" runat="Server" Text='<%# Eval("EMP_ID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProfDesc" runat="Server" Text='<%# Eval("PROF_DESC") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <td style="width: 10%" class="Title">
                                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-12">
                                                    <hr />
                                                </div>
                                            </div>

                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="col-sm-12" style="text-align: center;">
                                                        <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="lnkUpdate_Click" OnClientClick="Confirm(); showLoader();" />
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:View>
                                    </asp:MultiView>

                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
