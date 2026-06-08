<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="ProfileMaster.aspx.cs" Inherits="NewPortal2023.ESS.ProfileMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <script type="text/javascript" src="assets/js/dynamic_table_init.js"></script>


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
    <script type="text/javascript">
        function CheckNumeric(event) {
            var _key = (window.Event) ? event.which : event.keyCode;

            if ((_key > 95 && _key < 106) || (_key > 47 && _key < 58) || _key == 8 || _key == 9 || _key == 37 || _key == 39 || _key == 190 || _key == 110) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to submit your action?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script>
        function disableFormValidation() {
            const form = document.querySelector('form');
            if (form) {
                form.noValidate = true;
            }
            return true;
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
                                    Profile Master
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
                                            <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                <button data-dismiss="alert" class="close close-sm" type="button">
                                                    <i class="fa fa-times"></i>
                                                </button>
                                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-12">
                                                    <h3 class="page-header" style="text-align: left; color: blue;"><u>Profile Master</u></h3>
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

                                           <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" EnableViewState="true"
                                                        HeaderStyle-BackColor="LightSteelBlue"
                                                        OnRowDataBound="gv_RowDataBound" class="table table-bordered table-striped">
                                                        <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                        <PagerStyle CssClass="pagination-ys" />
                                                <Columns>
                                                    <asp:TemplateField Visible="true">
                                                        <ItemStyle CssClass="GridViewItem" Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name" Visible="true">
                                                        <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("MENU_NAME") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="Server" Text='<%# Eval("CID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMenuID" runat="Server" Text='<%# Eval("MENU_ID") %>' />
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

                                            <div class="form-group">
                                                <div class="col-lg-12">
                                                    <hr />
                                                </div>
                                            </div>

                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="col-sm-12" style="text-align: center;">
                                                        <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnsubmit_Click" OnClientClick="Confirm(); showLoader();" />
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
