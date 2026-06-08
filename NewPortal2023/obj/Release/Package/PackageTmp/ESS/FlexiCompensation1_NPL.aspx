<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="FlexiCompensation1_NPL.aspx.cs" Inherits="NewPortal2023.ESS.FlexiCompensation1_NPL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ValidFile() {
            var file = $("#fupldDocument").val();
            if (file == "") {
                IN4_DisplayErrorMessage("Browse document.");
                return false;
            }
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
                            <h3 style="color: white">FLEXI COMPENSATION BENEFIT</h3>
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

                                        <div style="text-align: right;">
                                            <asp:LinkButton ID="lnkFAQ" runat="server" Style="font-size: 15px; font-weight: bold;" OnClick="lnkFAQ_Click" ForeColor="Blue">Download FAQ</asp:LinkButton>
                                        </div>
                                        <div id="instructionsDiv" runat="server" style="border: 2px solid gray; margin-top: 5px; padding: 10px;">
                                            <b><u><span style="font-size: 20px;">Steps for uploading bills data:</span></u></b>
                                            <br />
                                            <br />
                                            <b>Steps :</b>
                                            <ol>
                                                <li>Download the Support form (excel sheet).</li>
                                                <li>Update all the Flexi reimbursement amounts in the excel sheet.</li>
                                                <li>Upload the updated excel file in the <b>“Upload Support Form”</b> option.</li>
                                                <li>To make any changes to the details you have already uploaded, click on the link 'Download Saved Supports Form' and download the excel file you have already uploaded. Make changes to this file and upload again to update previously uploaded data.</li>
                                                <li>Kindly zip all your documents, preferably Flexi-head wise and then upload by clicking on <b>"Submit"</b>.</li>
                                                <li>A summary of the Flexi benefits will be generated on the screen for your ready reference.</li>
                                        </div>
                                        <div id="divOtherComp" runat="server" style="border: 2px solid red; margin-top: 5px; padding: 10px;">
                                            <b><u><span style="font-size: 20px;">Steps for uploading bills data:</span></u></b>
                                            <br />
                                            <br />

                                            <%--<b>Step 1:</b>--%>
                                            <ol>
                                                <li style="font-weight: bold;">Upload the bill proofs(scanned copies) under 'Upload Bills' section.</li>
                                            </ol>
                                        </div>

                                        <div id="divUplSupp" runat="server">
                                            <h2 style="font-weight: bold; margin-bottom: 10px;">Upload Supports Form:</h2>
                                            <asp:LinkButton ID="lnkXLTemplate" runat="server" Style="font-weight: bold;" OnClick="lnkXLTemplate_Click" ForeColor="Blue">Download Supports Form</asp:LinkButton>
                                            <div style="border: 2px solid gray; padding: 10px; margin-top: 10px;">
                                                <asp:FileUpload ID="fupInvXL" runat="server" />
                                                <asp:Button ID="btnUploadXL" runat="server" Text="Upload Supports Form" class="btn btn-primary" OnClick="btnUploadXL_Click" />
                                                <br />
                                                <asp:LinkButton ID="lnkXLUploaded" runat="server" Style="font-weight: bold;"
                                                    OnClick="lnkXLUploaded_Click" ForeColor="Blue">Download Saved Supports Form</asp:LinkButton>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="title" style="margin-bottom: 5px; text-decoration: underline; display: none;">
                                            SUPPORTS RECEIVED
                                        </div>
                                        <div id="Div1" visible="false" runat="server">
                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table4">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView ID="gvReceived" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            CssClass="table4" HorizontalAlign="Left" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField Visible="true" HeaderText="Particulars">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Accepted Amounts from April 2019 to September 2019">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="50%" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Amount"
                                                                            Text='<%# Eval("AMOUNT") %>' ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                    <tr>
                                                                        <td class="GridViewHeader" style="width: 600px">Details
                                                                        </td>
                                                                        <td class="GridViewHeader" style="width: 130px">Amount
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <hr />
                                        <div class="title" style="margin-bottom: 5px;">
                                            <label style="font-size: 20px; font-weight: bold; color: darkcyan;"><u>SUMMARY</u></label>
                                            <asp:LinkButton ID="btnlnkDwnl" runat="server" OnClick="btnlnkDwnl_Click" Visible="false" Style="background-color: #736AFF; color: white; padding: 6px; display: inline-block; margin-bottom: 2px;" Text="Generate Report In Excel"></asp:LinkButton>
                                        </div>

                                        <div class="form-group" runat="server" visible="false">
                                            <label>Select Quarter Type :-</label>
                                            <asp:DropDownList ID="drpPreviousQtr" runat="server" Visible="true" OnSelectedIndexChanged="drpPreviousQtr_SelectedIndexChanged"
                                                AutoPostBack="true" ForeColor="#205A94">
                                                <asp:ListItem Value="0">Select Quarter</asp:ListItem>

                                                <asp:ListItem Value="First Quarter">First Quarter</asp:ListItem>
                                                <asp:ListItem Value="Second Quarte">Second Quarter</asp:ListItem>
                                                <asp:ListItem Value="Third Quarter">Third Quarter</asp:ListItem>
                                                <asp:ListItem Value="Fourth Quarter">Fourth Quarter</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div id="PrevInvDetails" visible="true" runat="server">
                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table21">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView ID="gvPrevious" runat="server" AutoGenerateColumns="False" BorderWidth="0px" OnRowDataBound="gvPrevious_RowDataBound"
                                                            CssClass="table4" HorizontalAlign="Left" Width="100%">
                                                            <%-- OnRowDataBound="gvPrevious_RowDataBound"--%>
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAid" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Particulars">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Yearly Eligibility">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLimit" runat="Server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Current Eligibility">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCurAmt" runat="Server" Text='<%# Eval("Fix_Amount") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Quarter">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="50%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQtr" runat="Server" Text='<%# Eval("QUARTERS") %>' CssClass="form-control input-sm"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Submitted Bills">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="20%" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Amount"
                                                                            Text='<%# Eval("AMOUNT") %>' onkeypress="return validateFloat(event, 0);"
                                                                            Width="130px" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged" BackColor="LightGray" ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approved Amount">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAmountAccepted" runat="server" CssClass="input" MaxLength="13"
                                                                            Text='<%# Eval("AMOUNT_ACCEPTED") %>' Width="130px" ReadOnly="true" BackColor="LightGray"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rejected Amount">
                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAmountRejected" runat="server" CssClass="input" MaxLength="13"
                                                                            Text='<%# Eval("AMOUNT_REJECTED") %>' Width="130px" ReadOnly="true" BackColor="LightGray"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
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
                                                <tr>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="border: 2px solid gray; padding: 10px; margin-top: 25px; color: darkorange; font-weight: bold; text-align: center;">
                                            Please note that the below mentioned eligibility amount is for the entire financial year, however for employees joining in between the year the same will be considered on proportionate basis from the Date of Joining.
                                        </div>

                                        <div class="form-group">

                                            <div id="trQut" runat="server" class="title" style="margin-bottom: 5px; text-decoration: underline;">
                                                <label>Quarter</label>
                                            </div>

                                            <div class="form-group" id="trQuarter" runat="server" visible="false">
                                                <label>Select Quarter Type :-</label>
                                                <asp:DropDownList ID="drpQuarterType" runat="server" Visible="true" OnSelectedIndexChanged="drpQuarterType_SelectedIndexChanged"
                                                    AutoPostBack="true" ForeColor="#205A94">
                                                    <asp:ListItem Value="0">Select Quarter</asp:ListItem>

                                                    <asp:ListItem Value="First Quarter">First Quarter</asp:ListItem>
                                                    <asp:ListItem Value="Second Quarte">Second Quarter</asp:ListItem>
                                                    <asp:ListItem Value="Third Quarter">Third Quarter</asp:ListItem>
                                                    <asp:ListItem Value="Fourth Quarter">Fourth Quarter</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group" id="InvDetails" visible="true" runat="server">
                                            <div class="form-group">
                                                <%--<asp:GridView ID="gvCTC" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" Width="100%" OnRowDataBound="gvCTC_RowDataBound">--%>
                                                <asp:GridView ID="gvCTC" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvCTC_RowDataBound"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField Visible="true" HeaderText="Particulars">
                                                            <ItemStyle CssClass="GridViewItem" Width="60%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Eligibility">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="20%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("Fix_Amount") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submitted Bills">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="20%" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Amount"
                                                                    Text='<%# Eval("AMOUNT") %>' onkeypress="return validateFloat(event, 0);"
                                                                    Width="130px" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged" ReadOnly="false"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmountAccepted" runat="server" CssClass="input" MaxLength="13"
                                                                    Text='<%# Eval("AMOUNT_ACCEPTED") %>' Width="130px" ReadOnly="false"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmountRejected" runat="server" CssClass="input" MaxLength="13"
                                                                    Text='<%# Eval("AMOUNT_REJECTED") %>' Width="130px" ReadOnly="false"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemStyle CssClass="GridViewItem" Width="60%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAID" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
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

                                            <div class="form-group">
                                                <asp:Button ID="btnPrint" runat="server" OnClick="btnFlexiPrint_Click" Text="Print"
                                                    Style="width: 61px; height: 26px;" />
                                            </div>

                                        </div>
                                        <hr />
                                        <div class="title" style="margin-bottom: 5px; text-decoration: underline; display: none;">
                                            View Bills
                                        </div>

                                        <div style="display: none">
                                            <label>Flexi Heads:</label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="drpFlexiHeads" runat="server" CssClass="input"
                                                    AutoPostBack="true" OnSelectedIndexChanged="drpFlexiHeads_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <label>Balance not availed:</label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtEligibility" runat="server" CssClass="input" ReadOnly="true"></asp:TextBox>
                                            </div>

                                            <div class="form-group" id="reqDocs" runat="server">
                                                <label>Required Documents:</label>
                                                <ul id="meetingDocs" runat="server">
                                                    <li>Hotels Original Bills</li>
                                                    <li>Bills should be on employee name</li>
                                                </ul>
                                                <ul id="fuelDocs" runat="server">
                                                    <li>Copy of RC Book in employee name</li>
                                                    <li>Original Fuel Bills</li>
                                                </ul>
                                                <ul id="vehicleDocs" runat="server">
                                                    <li>Original Maintenance Bill</li>
                                                    <li>Original Car Service / Repairs Bills</li>
                                                    <li>Copy of RC Book in employee name</li>
                                                </ul>
                                                <ul id="driverDocs" runat="server">
                                                    <li>Original Driver Salary Bill</li>
                                                    <li>RC Book of Driver</li>
                                                </ul>
                                                <ul id="telDocs" runat="server">
                                                    <li>Telephone connection in employee name</li>
                                                    <li>Original telephone bill</li>
                                                    <li>Monthly Rental + Call charges are allowed</li>
                                                </ul>
                                            </div>
                                        </div>

                                        <div style="display: none;">
                                            <div class="form-group">
                                                <asp:GridView ID="gvBills" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" Width="100%"
                                                    OnRowDataBound="gvBills_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="true">
                                                            <ItemStyle CssClass="GridViewItem" Width="1%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="Server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtParticulars" runat="server" CssClass="input" ToolTip="Enter Particulars"
                                                                    Text='<%# Eval("PARTICUALRS") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtParticulars2" runat="server" CssClass="input" ToolTip="Enter Particulars"
                                                                    Text='<%# Eval("PARTICUALRS2") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill No.">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Bill No."
                                                                    Text='<%# Eval("BILL_NO") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Date">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBillDate" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Bill Date"
                                                                    Text='<%# Eval("BILL_DATE") %>' Placeholder="dd-MM-yyyy"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Amount"
                                                                    Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Accepted Amount">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center"  />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAcceptedAmount" runat="server" CssClass="input" ReadOnly="true"
                                                                                        Text='<%# Eval("AMOUNT_ACCEPTED") %>' style="background-color: LightYellow;" Width="100px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Rejected Amount">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRejectedAmount" runat="server" CssClass="input" ReadOnly="true"
                                                                                        Text='<%# Eval("AMOUNT_REJECTED") %>' style="background-color: LightYellow;" Width="100px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="input" TextMode="MultiLine"
                                                                                        Text='<%# Eval("REMARKS") %>' style="background-color: LightYellow;" Width="100px" ReadOnly="true"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>--%>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDateSaved" runat="Server" Text='<%# Eval("DATE_SAVED") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocDataId" runat="Server" Text='<%# Eval("Doc_Data_Id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                            <tr>
                                                                <td class="GridViewHeader" style="width: 100%;">No data.
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>

                                            <div class="form-group" id="rowButtons" runat="server">
                                                <asp:LinkButton ID="lnkBtnAddDocRow" Text="Add Row" runat="server" Font-Bold="true"
                                                    OnClick="lnkBtnAddDocRow_Click" CssClass="Title" Visible="false" />
                                                <asp:Label ID="Label2" runat="server" Text="|" Font-Bold="true" ForeColor="blue" Visible="false" />
                                                <asp:LinkButton ID="lnkBtnDeleteDocRow" runat="server" Font-Bold="true" OnClick="lnkBtnDeleteDocRow_Click"
                                                    Text="Delete Selected" CssClass="Title" Visible="false" />
                                            </div>

                                            <div class="form-group">
                                                <asp:Button ID="btnSubmit" runat="server" OnClick="lnkUpdate_Click" Text="Save"
                                                    Style="width: 61px; height: 26px;" Visible="false" />
                                            </div>
                                        </div>

                                        <div class="form-group" id="tblLTA" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>LTA Form</label>
                                            </div>

                                            <div class="form-group">
                                                <label>Upload LTA Form</label>
                                                <br />
                                                <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" Style="font-weight: bold;">Download Saved LTC Form</asp:LinkButton>
                                            </div>

                                            <div class="form-group">
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                            </div>

                                            <div class="form-group">
                                                <span style="font-weight: bold; text-decoration: underline; font-size: 15px;">Uploaded Documents:</span><br />
                                                <br />
                                                <div id="Div3" visible="true" runat="server">
                                                    <asp:GridView ID="gvViewLTADocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                        CssClass="table4" DataKeyNames="FILENAME" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLTATSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Uploaded Files">
                                                                <ItemTemplate>
                                                                    <%--<asp:LinkButton ID="lnkBtnOpenFileLTA" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                                            OnClick="lnkBtnOpenFileLTA_Click" />--%>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <%--<asp:LinkButton ID="lnkBtnDeleteFileLTA" runat="server" Width="150px" Text='Delete'
                                                                                                            OnClick="lnkBtnDeleteFileLTA_Click" />--%>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" />
                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                <tr>
                                                                    <td class="GridViewHeader" style="width: 10%">
                                                                        <asp:Literal ID="Literal6" runat="server" Text="Your uploaded supports will be listed here" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>

                                        &emsp;                                       
                                        &emsp;
                                        &emsp;

                                        <div class="title" style="margin-bottom: 5px; text-decoration: underline;">
                                            <label style="font-size: 20px; font-weight: bold; color: darkcyan;"><u>Upload Bills</u></label>
                                        </div>
                                        <div style="border: 2px solid gray; padding: 10px; margin-top: 25px; color: darkorange; font-weight: bold; text-align: center;">
                                            Only PDF,JPG/JPEG and ZIP files allowed.
                                        </div>

                                        &emsp;
                                        &emsp;

                                        <div class="form-group" id="rowFileUpload" runat="server" style="border: 2px solid #333; padding: 10px;">
                                            <asp:FileUpload ID="fupldDocument" runat="server" />&nbsp;
                                            <asp:Button ID="btnUpload" runat="server" Visible="false" Text="Upload" CssClass="button" OnClick="btnUpload_Click" Style="background-color: Aqua; color: Black; padding: 3px;" />
                                        </div>


                                        <div class="form-group">
                                            <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                CssClass="table4" DataKeyNames="FILENAME" Width="100%"
                                                OnRowDataBound="gvViewDocDetails_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                OnClick="lnkBtnOpenFile_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnDeleteFile" runat="server" Width="150px" Text='Delete'
                                                                OnClick="lnkBtnDeleteFile_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                        <tr>
                                                            <td class="GridViewHeader" style="width: 10%">
                                                                <asp:Literal ID="Literal6" runat="server" Text="No Files." />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>

                                        <div class="form-group">
                                            <span id="msg" runat="server" visible="false" style="color: Red; font-weight: bold;">First Select Quarter Type Then Click Submit Button .</span>
                                        </div>

                                        <div class="form-group" style="text-align: center;">
                                            <asp:Button ID="btnAllSubmit" class="btn btn-success" runat="server" Text="Submit" OnClick="btnAllSubmit_Click" />
                                        </div>

                                    </div>

                                    <hr />

                                    <div class="form-group" id="prevTable" runat="server">
                                        <div class="form-group" runat="server" id="prevLabel">
                                            <label style="font-size: 20px; font-weight: bold; color: darkcyan;"><u>Previous Uploads</u></label>
                                        </div>

                                        <div class="form-group" runat="server" id="prevGV">
                                            <div class="form-group" id="Div2" visible="true" runat="server">
                                                <asp:GridView ID="gvPrevFiles" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" DataKeyNames="FILENAME" Width="100%" OnRowDataBound="gvPrevFiles_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Uploaded Files">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                    OnClick="lnkBtnOpenFilePrev_Click" ClientIDMode="Static" />
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
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkFAQ" />
                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                    <asp:PostBackTrigger ControlID="btnAllSubmit" />
                                    <asp:PostBackTrigger ControlID="lnkXLTemplate" />
                                    <asp:PostBackTrigger ControlID="btnUploadXL" />
                                    <asp:PostBackTrigger ControlID="lnkXLUploaded" />
                                    <asp:PostBackTrigger ControlID="btnlnkDwnl" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>


</asp:Content>
