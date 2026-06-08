<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="CumulativeSingle.aspx.cs" Inherits="NewPortal2023.ESS.CumulativeSingle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Comulative Payslip</h3>
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

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-3 labels">Employee Code :</label>
                                                <div class="col-sm-9">

                                                    <asp:Label ID="lblEmployeeCode" runat="server" Style="font-size: 15px; font-weight: bold" Text=""></asp:Label>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-3 labels">Employee Name :</label>
                                                <div class="col-sm-9">

                                                    <asp:Label ID="lblEmployeeName" runat="server" Style="font-size: 15px; font-weight: bold" Text=""></asp:Label>
                                                </div>
                                            </div>

                                            <div id="year" runat="server" visible="false" class="form-group">
                                                <label class="col-sm-3 labels">Select Year :</label>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control input-sm-3" Width="150px" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="InvDetails" visible="true" runat="server">
                                            <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                CssClass="table table-bordered table-striped"  DataKeyNames="FILENAME" Width="795px">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Download">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Width="100%" Text='<%# Eval("FILENAME") %>' ForeColor="Blue"
                                                                OnClick="lnkBtnOpenFile_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                        <tr>
                                                            <td class="GridViewHeader" style="width: 10%">
                                                                <asp:Literal ID="Literal6" runat="server" Text="Download" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%--          <asp:PostBackTrigger ControlID="chk1" />
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
                                     <asp:PostBackTrigger ControlID="gvViewDocDetails"/>
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>


</asp:Content>
