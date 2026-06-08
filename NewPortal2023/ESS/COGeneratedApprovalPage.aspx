<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="COGeneratedApprovalPage.aspx.cs" Inherits="NewPortal2023.ESS.COGeneratedApprovalPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Pending CO Approval</h3>
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

                                        <div style="overflow-y: auto; max-height: 300px;">
                                            <%--style="overflow-x: scroll; width: 930px;"--%>

                                            <%-- <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered table-striped dynamic-table"
                                                  OnRowDataBound="gvLeave_RowDataBound"
                                                DataKeyNames="ENTRY_AID" 
                                                 OnRowEditing="gvLeave_RowEditing"  OnRowUpdating="gvLeave_RowUpdating" OnRowCancelingEdit="gvLeave_RowCancelingEdit"
                                               GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="12px" >
                                                 <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                <PagerStyle CssClass="pagination-ys" />--%>
                                            <%-- OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound"
                                                DataKeyNames="ENTRY_AID" OnRowEditing="gvList_RowEditing" OnRowUpdating="gvList_RowUpdating" OnRowCancelingEdit="gvList_RowCancelingEdit">
                                                <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                <PagerStyle CssClass="pagination-ys" />--%>
                                            <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                O DataKeyNames="CID" OnRowDataBound="gvLeave_RowDataBound"
                                                CssClass="table4" HorizontalAlign="Left" ToolTip="Time Sheet">
                                                <Columns>

                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblId" runat="Server" Text='<%# Eval("CID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Code">
                                                        <ItemStyle CssClass="GridViewItem" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpCode" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemStyle CssClass="GridViewItem" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CO Applied Date">
                                                        <ItemStyle CssClass="GridViewItem" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFrom" runat="Server" Text='<%# Eval("FROM_DT") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFR" runat="Server" Text='<%# Eval("FR_DATE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To Date" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTO" runat="Server" Text='<%# Eval("TO_DATE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reason Type">
                                                        <ItemStyle CssClass="GridViewItem" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReason" runat="Server" Text='<%# Eval("REASON_TYPE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="grddrpAction" runat="server" CssClass="form-control input-sm-3" AutoPostBack="true" Enabled="true" Width="200px">
                                                                <asp:ListItem Value="" Text="---Select Action---"></asp:ListItem>
                                                                <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approver Remark">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="Server" Width="200px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSubmit" Text="Submit" runat="server" AutoPostBack="true" CssClass="btn btn-warning" OnClick="lnkSubmit_Click" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Cancel" runat="server" Text="Cancel" AutoPostBack="true" CssClass="btn btn-danger" CommandName="Cancel"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>


                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    </tr>
                                                                 </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </section>
                </div>
            </div>
        </section>
</asp:Content>
