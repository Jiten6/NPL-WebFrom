<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="Flexipay.aspx.cs" Inherits="NewPortal2023.ESS.Flexipay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Flexi Pay</h3>
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

                                        <div id="trOldtol" runat="server" visible="false" class="form-horizontal" style="margin: 10px">
                                            <div id="trSerachBy" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Annual CTC :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtAnnCTC" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCTC" runat="server" CssClass="input" Width="98px" ReadOnly="true"
                                                        Visible="False"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="divPerq" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Monthly CTC :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtMonCTC" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div1" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Monthly Gross Salary :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtGross" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                    <asp:TextBox ID="txtBal" runat="server" Visible="False"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div id="trNewtol" runat="server" visible="false" class="form-horizontal" style="margin: 10px">
                                            <div id="Div3" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Total Flexi Amount :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtTotalAmount" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="div4" runat="server" visible="false" class="form-group row">
                                                <label class="col-sm-4 labels">Opt Total Flexi Amount :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtNewTotalAmount" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <hr />

                                        <div class="col-sm-12" style="text-align: center;">
                                            <asp:Button ID="btnFlexiPrint" CssClass="btn btn-primary" runat="server" Text="Print Flexipay Details" OnClick="btnFlexiPrint_Click" OnClientClick="showLoader();" />
                                        </div>

                                        <div>
                                            <br />
                                            <br />
                                        </div>
                                        <hr />

                                        <div id="InvDetails" visible="true" runat="server">
                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table18">
                                                <tr id="OldCTC" runat="server" visible="false">
                                                    <td valign="top">
                                                        <%--<asp:GridView ID="gvCTC" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            CssClass="table4" HorizontalAlign="Left" ToolTip="Flexipay" OnRowDataBound="gvCTC_RowDataBound">--%>
                                                        <asp:GridView ID="gvCTC" runat="server" AutoGenerateColumns="False"
                                                            HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvCTC_RowDataBound"
                                                            GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                            <Columns>
                                                                <asp:TemplateField Visible="true">
                                                                    <ItemStyle CssClass="GridViewItem" Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="Server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Allowances">
                                                                    <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Address">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAddr" runat="server" CssClass="input" TextMode="MultiLine" MaxLength="500"
                                                                            Height="20px" ToolTip="Enter address" Text='<%# Eval("FlexAddr") %>' Width="130px"
                                                                            AutoPostBack="False"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pin Code">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtPin" runat="server" CssClass="input" MaxLength="6" ToolTip="Enter Pin code"
                                                                            Text='<%# Eval("FlexPin") %>' onkeypress="return validateFloat(event, 0);" Width="70px"
                                                                            AutoPostBack="False"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Limit">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("Limit_Amount") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpct" runat="Server" Text='<%# Eval("PCNT") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgross" runat="Server" Text='<%# Eval("GROSSCTC") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("HEAD_AID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbledit" runat="Server" Text='<%# Eval("EDITABLEPCT") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="%">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="30px" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtpcnt" runat="Server" Width="50px" CssClass="input" onkeypress="return validateFloatN(event, 0);"
                                                                            AutoPostBack="True" OnTextChanged="txtpcnt_TextChanged" Text='<%# Eval("EPCNT") %>' />
                                                                        <asp:DropDownList ID="drpCardType" runat="server" Width="70px" CssClass="input" Visible="false">
                                                                            <asp:ListItem Value="[Select]">[Select]</asp:ListItem>
                                                                            <asp:ListItem Value="Sodexo">Sodexo</asp:ListItem>
                                                                            <asp:ListItem Value="Paytm">Paytm</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:DropDownList ID="drpGiftCard" runat="server" Width="70px" CssClass="input" Visible="false">
                                                                            <asp:ListItem Value="[Select]">[Select]</asp:ListItem>
                                                                            <asp:ListItem Value="Gift Card">Gift Card</asp:ListItem>
                                                                            <asp:ListItem Value="Paytm Gift Voucher">Paytm Gift Voucher</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:DropDownList ID="drpIsCar" runat="server" Width="70px" CssClass="input" Visible="false"
                                                                            OnSelectedIndexChanged="drpIsCar_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="[Select]">[Select]</asp:ListItem>
                                                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="No">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:DropDownList ID="drpCC" runat="server" Width="70px" CssClass="input" Visible="false"
                                                                            OnSelectedIndexChanged="drpCC_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="[Select]">[Select]</asp:ListItem>
                                                                            <asp:ListItem Value="CC-">Upto 1600CC</asp:ListItem>
                                                                            <asp:ListItem Value="CC+">Above 1600CC</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:DropDownList ID="drpIsDriver" runat="server" Width="70px" CssClass="input" Visible="false"
                                                                            OnSelectedIndexChanged="drpIsDriver_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="[Select]">[Select]</asp:ListItem>
                                                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="No">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:DropDownList ID="drpStatutoryPF" runat="server" Width="70px" CssClass="input" Visible="false"
                                                                            OnSelectedIndexChanged="drpStatutoryPF_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="Yes">[Select]</asp:ListItem>
                                                                            <%-- <asp:ListItem Value="Yes">Yes</asp:ListItem>--%>
                                                                            <asp:ListItem Value="No">Yes</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter amount"
                                                                            Text='<%# Eval("Fix_Amount") %>' onkeypress="return validateFloat(event, 0);"
                                                                            Width="130px" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGroup" runat="Server" Text='<%# Eval("GROUP_TYPE") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSel" runat="Server" Text='<%# Eval("CHKFLAG") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblshow" runat="Server" Text='<%# Eval("IsFlexAddrShow") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblman" runat="Server" Text='<%# Eval("IsFlexMandatory") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCardType" runat="Server" Text='<%# Eval("CARD_TYPE") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <EmptyDataTemplate>
                                                                <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                    <tr>
                                                                        <td class="GridViewHeader" style="width: 600px">Other Details
                                                                        </td>
                                                                        <td class="GridViewHeader" style="width: 130px">Amount
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr id="NewCTC" runat="server" visible="false">
                                                    <td valign="top">
                                                        <asp:GridView ID="gvNewCTC" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            CssClass="table4" HorizontalAlign="Left" ToolTip="Flexipay" OnRowDataBound="grNewCTC_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField Visible="true">
                                                                    <ItemStyle CssClass="GridViewItem" Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkNewSelect" runat="Server" AutoPostBack="True" OnCheckedChanged="chkNewSelect_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Allowances">
                                                                    <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNewDesc" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField Visible="true" HeaderText="Limit">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNewLimit" runat="Server" Text='<%# Eval("Fix_Amount") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNewdetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNewGroup" runat="Server" Text='<%# Eval("GROUP_TYPE") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNewSel" runat="Server" Text='<%# Eval("CHKFLAG") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                    <tr>
                                                                        <td class="GridViewHeader" style="width: 600px">Other Details
                                                                        </td>
                                                                        <td class="GridViewHeader" style="width: 130px">Amount
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td class="style2">Car Lease : This option is available for grade AVP and above in all functions.
                                                                    <asp:DropDownList ID="drpCarLease" runat="server" Height="22px">
                                                                        <asp:ListItem>No</asp:ListItem>
                                                                        <asp:ListItem>Yes</asp:ListItem>
                                                                    </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="supp" runat="server" visible="false">
                                                    <td>
                                                        <div style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: Maroon; font-weight: bold; text-align: center;">
                                                            **Only single file is allowed per flexi component. In case of multiple files, kindly zip and upload.**
                                                        </div>
                                                        <div style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: Red; font-weight: bold; text-align: center;">
                                                            **Please update no. of dependents including self, spouse, upto 2 children, parents and upto 2 dependent brother and/ or sister.**
                                                        </div>
                                                        <hr />
                                                        <span style="font-weight: bold;">Upload Supporting Documents:</span>
                                                        <br />
                                                        <asp:LinkButton ID="lnkForms" runat="server" Style="font-weight: bold;"
                                                            OnClick="lnkForms_Click">Download Flexi Forms</asp:LinkButton><br />
                                                        <asp:LinkButton ID="lnkFAQLTA" runat="server" Style="font-weight: bold;"
                                                            OnClick="lnkFAQLTA_Click">Download LTA FAQ</asp:LinkButton>
                                                        <table class="table4" width="100%" style="margin-top: 10px;">
                                                            <tr>
                                                                <td style="font-weight: bold; text-align: center;">Flexi Details
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">No. of dependents including self
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">Amount
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">Approved Amount
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">Rejected Amount
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">Remarks
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">Upload Support
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>LEAVE TRAVEL ASSISTANCE
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:TextBox ID="txtLTANo" CssClass="input" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:TextBox ID="txtLTAAmt" CssClass="input" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblLTAAmtVer" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblLTAAmtRej" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblLTARemarks" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:FileUpload ID="fupLTA" runat="server" />
                                                                    <hr />
                                                                    <asp:Button ID="btnUploadLTA" runat="server" Text="Upload"
                                                                        CssClass="button-xlarge-primary" OnClick="btnUploadLTA_Click" />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="lnkDownloadLTA" runat="server" Style="font-weight: bold;"
                                                                        OnClick="lnkDownloadLTA_Click" Visible="false">View</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>TELEPHONE LANDLINE REIMBURSEMENT
                                                                </td>
                                                                <td></td>
                                                                <td style="text-align: center;">
                                                                    <asp:TextBox ID="txtTelAmt" CssClass="input" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblTelAmtVer" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblTelAmtRej" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblTelRemarks" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:FileUpload ID="fupTel" runat="server" />
                                                                    <hr />
                                                                    <asp:Button ID="btnUploadTel" runat="server" Text="Upload"
                                                                        CssClass="button-xlarge-primary" OnClick="btnUploadTel_Click" />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="lnkDownloadTel" runat="server" Style="font-weight: bold;"
                                                                        OnClick="lnkDownloadTel_Click" Visible="false">View</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>CAR MAINTAINANCE AND FUEL
                                                                </td>
                                                                <td></td>
                                                                <td style="text-align: center;">
                                                                    <asp:TextBox ID="txtFuelAmt" CssClass="input" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblFuelAmtVer" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblFuelAmtRej" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblFuelRemarks" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:FileUpload ID="fupFuel" runat="server" />
                                                                    <hr />
                                                                    <asp:Button ID="btnUploadFuel" runat="server" Text="Upload"
                                                                        CssClass="button-xlarge-primary" OnClick="btnUploadFuel_Click" />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="lnkDownloadFuel" runat="server" Style="font-weight: bold;"
                                                                        OnClick="lnkDownloadFuel_Click" Visible="false">View</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>CAR DRIVERS SALARY
                                                                </td>
                                                                <td></td>
                                                                <td style="text-align: center;">
                                                                    <asp:TextBox ID="txtDriverAmt" CssClass="input" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblDriverAmtVer" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblDriverAmtRej" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lblDriverRemarks" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:FileUpload ID="fupDriver" runat="server" />
                                                                    <hr />
                                                                    <asp:Button ID="btnUploadDriver" runat="server" Text="Upload"
                                                                        CssClass="button-xlarge-primary" OnClick="btnUploadDriver_Click" />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="lnkDownloadDriver" runat="server" Style="font-weight: bold;"
                                                                        OnClick="lnkDownloadDriver_Click" Visible="false">View</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td>
                                                        <hr />
                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" class="table5" id="prevTable"
                                                            runat="server">
                                                            <tr runat="server" id="prevLabel">
                                                                <td class="title">Previous Uploads
                                                                </td>
                                                            </tr>
                                                            <tr valign="top" runat="server" id="prevGV">
                                                                <td valign="top">
                                                                    <div id="Div2" visible="true" runat="server">
                                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table2">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <asp:GridView ID="gvPrevFiles" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                                        CssClass="table4" DataKeyNames="FILENAME" Width="100%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Uploaded Files">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                                        OnClick="lnkBtnOpenFilePrev_Click" />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                                <tr>
                                                                                                    <td class="GridViewHeader" style="width: 10%">
                                                                                                        <asp:Literal ID="Literal6" runat="server" Text="Uploaded Files" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <%-- </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="drpMonth" 
                                                                    EventName="SelectedIndexChanged" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </div>

                                        <div>
                                            <br />
                                        </div>

                                        <div id="trAgree1" runat="server" class="form-group" style="border: double; margin: 5px">
                                            <div style="margin: 5px">
                                                <span style="font-weight: bold;">Disclaimer: </span>
                                                <ol style="font-weight: bold; text-align: justify; font-style: italic;">
                                                    I hereby declare that all the information given by me is true and correct. Any Income Tax liability arising out of a wrong declaration will be my responsibility, and I undertake to indemnify the Company and its officers from all consequences, monetary and otherwise, arising out of any incorrect and/or incomplete information provided in this declaration.
                                                    
                                            </div>

                                            </ol>
                                        </div>

                                        <div style="text-align: center;">
                                            <asp:CheckBox ID="chkAgree" Text="&nbsp;&nbsp;&nbsp; I Agree" runat="server" ForeColor="DarkSlateBlue" Font-Bold="True" Font-Size="17px" />
                                        </div>


                                        <hr />
                                        <div style="width: 100%; text-align: center;">
                                            <asp:Button ID="btnsubmit" runat="server" OnClick="lnkUpdate_Click" class="btn btn-success" Style="width: 100px; height: 50px; font-weight: bold;"
                                                Text="Submit" />
                                        </div>


                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnFlexiPrint" />
                                    <%--<asp:PostBackTrigger ControlID="chk2" />
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
