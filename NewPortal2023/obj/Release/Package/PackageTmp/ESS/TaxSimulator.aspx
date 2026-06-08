<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="TaxSimulator.aspx.cs" Inherits="NewPortal2023.ESS.TaxSimulator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Tax Simulator</h3>
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

                                        <div class="form-group" style="border: medium">
                                        </div>
                                        <div class="form-horizontal" style="margin: 10px">
                                            <div id="trSerachBy" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Annual Income (Fixed CTC) :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtAnnualCTC" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <hr />

                                            <div id="divPerq" runat="server" visible="false" class="form-group row">
                                                <label class="col-sm-4 labels">Perquisite (Contributions exceeding Rs.7,50,000 would be considered as taxable) :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPerquisite" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div1" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Taxable Amount under Old Regime :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtAmountTaxableOld" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div2" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Taxfree Amount under Old Regime :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtAmountTaxfreeOld" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <hr />

                                            <div id="Div3" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Taxable Amount under New Regime :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtAmountTaxableNew" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div4" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Taxfree Amount under New Regime :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtAmountTaxfreeNew" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <hr />

                                            <div id="Div5" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Annual Rent :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtRent" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:DropDownList ID="drpRent" runat="server" CssClass="form-control input-sm-3" Width="250px"
                                                        AutoPostBack="true" ForeColor="#205A94">
                                                        <asp:ListItem Value="0">Select Metro/Non-Metro</asp:ListItem>
                                                        <asp:ListItem Value="1">Metro</asp:ListItem>
                                                        <asp:ListItem Value="2">Non-Metro</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>

                                             <div id="Div25" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Rent Exemption :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRentExemption" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div6" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Old Standard Deduction :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtStandardDeduction" runat="server" ReadOnly="true" Text="50000.00" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div24" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">New Standard Deduction:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtStandardDeductionNew" runat="server" ReadOnly="true" Text="75000.00" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div23" runat="server" class="form-group row" visible="false">
                                                <label class="col-sm-4 labels">HRA Exemption :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtHRAExemption" runat="server" ReadOnly="true" Text="50000.00" Visible="false" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div7" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Housing Interest :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtHousingInterest" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:TextBox ID="txtHousingInterestAbs" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div8" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Gross Total Income :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtTotalGrossIncome" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <hr />

                                            <div id="Div9" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Section 80C :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txt80C" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div10" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Exemption allowed under Section 80C (Max Rs.150000/-) :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txt80CEx" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <hr />

                                            <div id="Div11" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Exemption allowed under Section 80D :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtTotalInvExemption" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div12" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">National Pension Scheme 80CCD IB (Employee) :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtNPS" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div13" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Other Exemption :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtOtherExemption" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <hr />

                                        <div class="col-sm-12" style="text-align: center;">
                                            <asp:Button ID="btnCalculateTax" CssClass="btn btn-primary" runat="server" Text="CALCULATE TAX" OnClick="btnCalculateTax_Click" OnClientClick="showLoader();" />

                                        </div>

                                        <div>
                                            <br />
                                            <br />
                                        </div>
                                        <hr />

                                        <div class="form-horizontal" style="margin: 10px">
                                            <div id="Div14" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels"></label>
                                                <div class="col-sm-3">
                                                    <label class="labels">As Per Old Regime</label>
                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <label class="labels">As Per New Regime</label>
                                                </div>

                                            </div>

                                            <div id="Div15" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Select Option :</label>
                                                <div class="col-sm-3">
                                                    <asp:RadioButton ID="rdbOld" runat="server" GroupName="TaxOption" />

                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:RadioButton ID="rdbNew" runat="server" GroupName="TaxOption" />
                                                </div>

                                            </div>

                                            <hr />

                                            <div id="Div16" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Total Taxable Income :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtTotalTaxableIncome" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:TextBox ID="txtTotalTaxableIncomeNew" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div17" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Tax On Total Income :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtTaxOnIncome" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:TextBox ID="txtTaxOnIncomeNew" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="divBreakup" runat="server" visible="false" class="form-group row">
                                                <%--<asp:GridView ID="gvCTC" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="Flexipay">--%>
                                                <asp:GridView ID="gvCTC" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField Visible="true" HeaderText="Breakup (Old Regime)">
                                                            <ItemStyle CssClass="GridViewItem" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("BREAKUP_OLD") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Tax (Old Regime)">
                                                            <ItemStyle CssClass="GridViewItem" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("SLAB_TAX") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Breakup (New Regime)">
                                                            <ItemStyle CssClass="GridViewItem" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("BREAKUP_NEW") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Tax (New Regime)">
                                                            <ItemStyle CssClass="GridViewItem" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("SLAB_TAX_NEW") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
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
                                            </div>

                                            <div id="Div18" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Tax Credit :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtTaxCredit" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:TextBox ID="txtTaxCreditNew" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div19" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Surcharge :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtSurcharge" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:TextBox ID="txtSurchargeNew" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div20" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Education Cess :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtEduCess" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:TextBox ID="txtEduCessNew" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div21" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Total Annual Tax :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtAnnualTax" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:TextBox ID="txtAnnualTaxNew" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div id="Div22" runat="server" class="form-group row">
                                                <label class="col-sm-4 labels">Total Monthly Tax :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtMonthlyTax" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-sm-5">
                                                    <asp:TextBox ID="txtMonthlyTaxNew" runat="server" ReadOnly="true" CssClass="form-control input-sm" Width="250px"></asp:TextBox>
                                                </div>

                                            </div>

                                            <hr />

                                        </div>

                                        <div class="form-horizontal" style="margin: 10px">
                                            <div id="instructionsDiv" runat="server" visible="true" style="border: 2px solid red; margin-top: 5px; padding: 10px;">
                                                <b><u><span style="font-size: 20px;">Existing Employees:</span></u></b><br />
                                                <ol>
                                                    <li style="font-weight: bold;">Other section in the OLD Regime like 80 DD, 80 E etc. are allowed. To keep the calculation simple we have not included in this list. However while arriving at your correct tax slab we will plot all the old exemption list as displayed in last year in invesment portal.</li>
                                                    <li style="font-weight: bold;">The tax calculator is shared to choose the tax regime. Kindly note employee need to update their investment declaration in the portal after choosing the Old tax regime.</li>
                                                    <li style="font-weight: bold;">Employees who choose the NEW Tax regime need not update the investment details in investment portal.</li>
                                                    <li style="color: Red; font-weight: bold;">Once you select the Regime option, you can't change the tax regime for the current financial year.</li>
                                                    <li style="color: Red; font-weight: bold;">In absence of any selection of Tax Regime, the default regime i.e NEW regime will be considered.</li>
                                                </ol>
                                                <b><u><span style="font-size: 20px;">New Employees:</span></u></b><br />
                                                <ol>
                                                    <li style="font-weight: bold;">New joinees in between current Financial Year are requested to opt the tax regime which was opted / followed in your previous employer.</li>
                                                    <li style="font-weight: bold;">Kindly note any mid term changing of Tax Regime is not allowed as Income Tax Act.</li>
                                                    <%--<li style="font-weight: bold;">Changing of Tax Regime in the middle of any Financial Year will lead to problems while filing your Income Tax Returns.</li>--%>
                                                </ol>
                                            </div>
                                        </div>

                                        <hr />

                                        <div class="col-sm-12" style="text-align: center;">
                                            <asp:Button ID="btnSaveOption" CssClass="btn btn-primary" runat="server" Text="SAVE SELECTED TAX OPTION" OnClick="btnSaveOption_Click" OnClientClick="showLoader();" />

                                        </div>


                                    </div>
                                    <asp:HiddenField ID="hdnBasic" runat="server" />
                                    <asp:HiddenField ID="hdnHRA" runat="server" />
                                    <asp:HiddenField ID="hdnGains" runat="server" />
                                    <asp:HiddenField ID="hdnTDS" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnCalculateTax" />
                                    <asp:PostBackTrigger ControlID="btnSaveOption" />
                                    <%--<asp:PostBackTrigger ControlID="chk3" />
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
