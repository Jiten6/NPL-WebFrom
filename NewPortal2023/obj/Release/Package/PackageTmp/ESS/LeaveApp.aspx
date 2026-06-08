<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="LeaveApp.aspx.cs" Inherits="NewPortal2023.ESS.LeaveApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="row">
                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Autopostback="true" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="showLoader();" />
                                        </div>

                                        <div id="AppList" visible="true" runat="server">
                                            <label>
                                                <h3>List Of Leave Applications</h3>
                                            </label>
                                            <br />

                                            <div style="margin-top: 20px">
                                         
                                                <label id="lbldrp" class="col-sm-3 labels">Search by Leave Type :-</label>
                                                <asp:DropDownList ID="drpLeaveType" CssClass="form-control input-sm loader" Width="150px" runat="server" ForeColor="#205A94" Onchange="showLoader();">
                                                </asp:DropDownList>
                                             
                                            </div>

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
                                                      
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReason1" runat="Server" Text='<%# Eval("REMARKS") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                     
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkstatus" runat="Server" Text='<%# Eval("STATUS") %>'  />
                                                                <asp:LinkButton ID="lnkcancel" runat="Server" Text='Cancel Request'  Visible="false" />
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
</asp:Content>
