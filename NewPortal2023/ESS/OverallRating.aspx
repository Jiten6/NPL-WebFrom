<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="OverallRating.aspx.cs" Inherits="NewPortal2023.ESS.OverallRating" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#gvList").DataTable({
                "responsive": true, "lengthChange": false, "autoWidth": false,
                "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
            }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            $('#example2').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false, lnkBtnAddRowSteps
                "ordering": true,
                "info": true,
                "autoWidth": false,
                "responsive": true,
            });
        });
    </script>
    <script type="text/javascript">

        function ConNext() {
            return confirm("Are you saving the details, and do you want to go to the next section?");
        }

        function ConPrev() {
            return confirm("Are you saving the details, and do you want to go to the privious section?");
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Overall Rating</h3>
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
                                    <div class="adv-table">
                                        <div class="card-body" style="margin: 20px">
                                            <asp:UpdatePanel ID="updProofEdit" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group row">
                                                        <div class="col-06">
                                                            <asp:DropDownList ID="drpQuarter" runat="server" Visible="false" CssClass="form-control input-sm-3" AutoPostBack="true" OnSelectedIndexChanged="drpQuarter_SelectedIndexChanged" Width="150px">

                                                                <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                                                <asp:ListItem Value="2">Second Quarter</asp:ListItem>
                                                                <asp:ListItem Value="3">Third Quarter</asp:ListItem>
                                                                <asp:ListItem Value="4">Fourth Quarter</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="form-group row">
                                                        <div class="col-12">
                                                            <h3 class="card-title">List</h3>
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:UpdatePanel ID="updListAdd" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="gvList" runat="server" OnRowUpdating="gvList_RowUpdating" OnRowCancelingEdit="gvList_RowCancelingEdit" OnRowDataBound="gvList_RowDataBound" OnRowEditing="gvList_RowEditing" AutoGenerateColumns="False"
                                                                        CssClass="display table table-bordered table-striped dynamic-table">
                                                                        <Columns>
                                                                            <asp:TemplateField ShowHeader="true">
                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btn_Edit" Visible="false" runat="server" Text="Edit" CommandName="Edit" />
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:Button ID="btn_Update" Visible="false" runat="server" Text="Update" CommandName="Update" />
                                                                                    <asp:Button ID="btn_Cancel" Visible="false" runat="server" Text="Cancel" CommandName="Cancel" />
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Area">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtArea" runat="Server" CssClass="form-control input-sm " TextMode="MultiLine" MaxLength="50" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtMetric" runat="Server" CssClass="form-control input-sm " TextMode="MultiLine" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtTarget" runat="Server" CssClass="form-control input-sm " TextMode="MultiLine" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Indivdiual KRA Achievement %">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtIndAch" runat="Server" CssClass="form-control input-sm " TextMode="MultiLine" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="true" HeaderText="KRA Rating" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="drpKRARating" runat="server" CssClass="form-control input-sm-3" AutoPostBack="true" Enabled="true">
                                                                                        <asp:ListItem Value="">[Select KRA Rating]</asp:ListItem>
                                                                                        <asp:ListItem Value="Exceeds Expectations" Text="Exceeds Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Exceeds Expectations" Text="Exceeds Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Exceeds Expectations" Text="Exceeds Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Meets Expectations" Text="Meets Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Meets Expectations" Text="Meets Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Meets Expectations" Text="Meets Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Needs Improvement" Text="Needs Improvement"></asp:ListItem>
                                                                                        <asp:ListItem Value="Needs Improvement" Text="Needs Improvement"></asp:ListItem>
                                                                                        <asp:ListItem Value="Needs Improvement" Text="Needs Improvement"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="true" HeaderText="Leadership Behaviour" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="drpLedBehav" runat="server" CssClass="form-control input-sm-3" AutoPostBack="true" Enabled="true">
                                                                                        <asp:ListItem Value="">[Select Leadership Behaviour]</asp:ListItem>
                                                                                        <asp:ListItem Value="Exceeds Expectations" Text="Exceeds Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Meets Expectations" Text="Meets Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Needs Improvement" Text="Needs Improvement"></asp:ListItem>
                                                                                        <asp:ListItem Value="Exceeds Expectations" Text="Exceeds Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Meets Expectations" Text="Meets Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Needs Improvement" Text="Needs Improvement"></asp:ListItem>
                                                                                        <asp:ListItem Value="Exceeds Expectations" Text="Exceeds Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Meets Expectations" Text="Meets Expectations"></asp:ListItem>
                                                                                        <asp:ListItem Value="Needs Improvement" Text="Needs Improvement"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Overall Rating">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtOverRat" runat="Server" CssClass="form-control input-sm " Enabled="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAddCORowId" runat="Server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtaid" runat="Server" CssClass="form-control input-sm " />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="width: 10%" class="Title">
                                                                                        <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                </asp:View>
                            </asp:MultiView>

                        </div>

                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
