<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="Form16.aspx.cs" Inherits="NewPortal2023.ESS.Form16" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Form 16</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                           
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

                                        <div class="form-group" id="trPdf" runat="server" visible="false">
                                            Click to Download Digital Signature Validation Process for Form 16 :- 
                                            <asp:LinkButton ID="lnkbtnPdfDownload" runat="server" Text="Download" OnClick="lnkbtnPdfDownloadClick"></asp:LinkButton>

                                            <b style="color: Red">Note :- Kindly Open Form 16 in Adobe Acrobat PDF Software</b>

                                        </div>
                                        <div class="form-group">

                                            <div class="col-sm-12" id="year" runat="server" style="margin-top: 10px">
                                                <label for="lblRemark" class="col-sm-4"><span><b>Select Month/Year :-</b></span></label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control input-sm-4" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control custom-dropdown" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>--%>
                                                </div>
                                            </div>


                                        </div>

                                        <div class="form-group">
                                            <br />
                                            <br />
                                        </div>
                                        <div class="form-group" id="InvDetails" runat="server">
                                            <%--<asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                CssClass="table table-bordered table-striped" DataKeyNames="FILENAME">
                                            --%>



                                            <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField Visible="true" HeaderText="Download">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Width="100%" Text='<%# Eval("FILENAME") %>' ForeColor="Blue" class="underline"
                                                                OnClick="lnkBtnOpenFile_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField Visible="true" HeaderText="Preview">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenPreviewFile" runat="server" Width="100%" Text="Preview" CssClass="HeaderBarThreshold"
                                                                OnClick="lnkBtnOpenPreviewFile_Click" OnClientClick="SetTarget();" />
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <td style="width: 10%" class="Title">
                                                                <asp:Literal ID="Literal6" runat="server" Text="Download." />
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
