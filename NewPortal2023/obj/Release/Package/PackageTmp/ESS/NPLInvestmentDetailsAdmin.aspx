<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="NPLInvestmentDetailsAdmin.aspx.cs" Inherits="NewPortal2023.ESS.NPLInvestmentDetailsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .wrap {
            white-space: normal;
            width: 200px;
        }
    </style>




    <script language="javascript" src="Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to clear all investment data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }


        $(document).ready(function () {
            $('#<%= drpEmpType.ClientID %>').select2();
        });
    </script>

    <style type="text/css">
        .tableTitle {
            font-weight: bold;
            font-size: 10pt;
            vertical-align: middle;
            color: #205a94;
            font-family: Tahoma;
            height: 20px;
            text-decoration: none;
        }

        .table4 {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
            background-color: #FFFFFF;
        }

        .total {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 1px;
            padding-bottom: 1px;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            height: 10px;
            width: 10%;
            font-weight: bold;
            background-color: #efefef;
        }

            .total TD {
                font-weight: bold;
                height: 20px;
                background-color: #efefef;
            }

        .GridViewItem {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 1px;
            padding-bottom: 1px;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            height: 10px;
            width: 10%;
            font-weight: normal;
        }

        .GridViewHeader {
            font-weight: bold;
            font-size: 8.3;
            filter: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#FFFFFF,endColorStr=#BBDDFF);
            text-transform: capitalize;
            color: #545454;
            border-top: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-bottom: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            text-align: center;
        }

        .input {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 7.5pt;
            color: #004B97;
            border-top: auto inset #CCCCCC;
            border-right: auto inset #DFDFDF;
            border-left: auto inset #CCCCCC;
            border-bottom: auto inset #DFDFDF;
            border-top-color: #CCCCCC;
            border-right-color: #DFDFDF;
            border-bottom-color: #DFDFDF;
            border-left-color: #CCCCCC;
            border-top-style: inset;
            border-right-style: inset;
            border-bottom-style: inset;
            border-left-style: inset;
            border-style: inset;
            border-color: #999999 #CCCCCC #CCCCCC #999999;
            border-top-width: 1px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
        }

        .GridViewEmpty {
            padding: 0px;
            margin: 0;
            border: solid 1px #d3dbdf;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            border-top: 1px solid #d3dbdf;
            width: 100%;
            border-spacing: 0px;
        }

        .title {
            font-weight: bold;
            font-size: 11pt;
            vertical-align: middle;
            text-transform: capitalize;
            color: #205a94;
            font-family: Tahoma; /* height: 20px; */
            text-decoration: none;
        }

           .select2-container {
            z-index: 9999 !important;
            width: 100% !important;
        }

        .select2-selection {
            height: 38px !important; /* Match Bootstrap input height */
            padding: 5px 12px !important;
        }

        .select2-selection__rendered {
            line-height: 28px !important;
        }

        .select2-selection__arrow {
            height: 36px !important;
        }
    </style>








</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Investment Support</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>




                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <%-- <div id="trSerachBy" runat="server">
                                        <label class="col-sm-3 labels">Search By Employee Code :-</label>
                                        <asp:TextBox ID="txtEmpMid" runat="server" CssClass="input"></asp:TextBox>
                                        <asp:Button ID="btnLoadDetails" runat="server" Text="Search" OnClick="btnLoadDetails_Click"
                                            CssClass="btn btn-primary" OnClientClick="showLoader();" />
                                    </div>--%>

                                    <div id="trSerachBy" runat="server" class="row align-items-center mb-3">
                                        <!-- Label -->
                                        <div class="col-sm-4">
                                            <label class="labels mb-0">Search By Employee Code :</label>
                                        </div>

                                        <!-- DropDownList -->
                                       <%-- <div class="col-sm-4">
                                            <asp:DropDownList ID="drpEmpType" runat="server" CssClass="form-control select2" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpEmpType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>--%>

                                           <!-- DropDownList -->
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="drpEmpType" runat="server" CssClass="select2" Width="100%" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpEmpType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                        <!-- Button -->
                                        <div class="col-sm-4">
                                            <asp:Button ID="btnLoadDetails" runat="server" Text="Search" OnClick="btnLoadDetails_Click"
                                                CssClass="btn btn-primary" OnClientClick="showLoader();" />
                                        </div>
                                    </div>


                                    <div class="form-group" id="form1" runat="server" visible="false">
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

                                        <div style="text-align: right;">
                                            <asp:LinkButton ID="lnkManual" runat="server" Style="font-weight: bold;" OnClick="lnkManual_Click" ForeColor="Blue">Download Manual</asp:LinkButton>
                                            <br />
                                            <asp:LinkButton ID="lnkFAQ" runat="server" Style="font-weight: bold;" OnClick="lnkFAQ_Click" ForeColor="Blue">Download F.A.Q.</asp:LinkButton>
                                            <asp:LinkButton ID="lnkDownloadDeclarationForms" runat="server" Style="font-weight: bold;"
                                                OnClick="lnkDownloadDeclarationForms_Click" ForeColor="Blue">Download Declaration Forms</asp:LinkButton>
                                        </div>
                                        <div id="instructionsDiv" runat="server" visible="false" style="border: 2px solid gray; margin-top: 5px; padding: 10px;">
                                            <b><u><span style="font-size: 20px;">Steps for uploading investment documents on portal:</span></u></b>
                                            <br />
                                            <br />
                                            <%--<b>Step 1:</b>--%>
                                            <ol>
                                                <li>Fill out the details under the “Employee's Information” section.</li>
                                                <li>Under “Investment Declaration” section, enter amount against required investment
                                                    type. After entering amount, Upload file option will be enabled for that investment
                                                    type. Select the file by clicking on “Browse/Choose File” button and click on “Upload”
                                                    to upload the file.</li>
                                                <li>If you want to declare rent, fill details under “Rent Declaration” section and upload
                                                    the relevant rent documents. Follow same process for section 4 and 5.</li>
                                            </ol>
                                            <%--<b>Step 2:</b>
                                            <ol>
                                                <li>Click on the “Download Form 12BB” link to download PDF file for form 12BB.</li>
                                                <li>Select the file using “Browse/Choose” button in Form 12BB section.</li>
                                                <li>Click on the checkbox to agree to the disclaimer.</li>
                                                <li>Click on <b>“Upload Form 12BB”.</b></li>
                                            </ol>--%>
                                            <div style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: darkorange; font-weight: bold; text-align: center;">
                                                If the Investment proofs are not submitted on time and supports are not uploaded
                                                in the respective section eg. LIC support has to be uploaded in Life Insurance Premium
                                                (For Self, Spouse and Children only) section , it will be assumed that there is
                                                NIL submission and Taxes would be calculated and deducted accordingly.
                                            </div>
                                        </div>
                                        <div id="instructionsArcil" runat="server" visible="false" style="border: 2px solid gray; margin-top: 5px; padding: 10px;">
                                            <b><u><span style="font-size: 20px;">Notes:</span></u></b>
                                            <br />
                                            <br />
                                            <%--<b>Step 1:</b>--%>
                                            <ol>
                                                <li style="font-size: larger; font-weight: bold; color: darkorange">Employees who choose the New Tax Regime do not need to update their investment supports.</li>

                                            </ol>
                                        </div>
                                        <br />
                                        <hr />

                                        <div id="trUndertaking" runat="server" visible="false">
                                            <h2 style="font-weight: bold; margin-bottom: 10px;">Undertaking:</h2>
                                            <asp:LinkButton ID="btnUndertaking" runat="server" Style="font-weight: bold;" OnClick="btnUndertaking_Click">Download form for undertaking for payments due in March 2023</asp:LinkButton>
                                            <div style="border: 2px solid gray; padding: 10px; margin-top: 10px;">
                                                <asp:FileUpload ID="fupUndertaking" runat="server" />
                                                <asp:Button ID="btnUploadUndertaking" runat="server" Style="background-color: Aqua; color: Black; padding: 6px;"
                                                    Text="Upload Undertaking" CssClass="button-xlarge-primary"
                                                    OnClick="btnUploadUndertaking_Click" />
                                                <br />
                                                <asp:LinkButton ID="btnDownloadUndertaking" runat="server" Style="font-weight: bold;"
                                                    OnClick="btnDownloadUndertaking_Click">Download Saved Undertaking</asp:LinkButton>
                                            </div>
                                            <hr />
                                        </div>

                                        <div class="form-group">
                                            <div class="tableTitle">
                                                <asp:LinkButton ID="lnkOther" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                    OnClick="lnkOther_Click" Visible="true" AutoPostBack="true" OnClientClick="showLoader();">1. Employee's Information</asp:LinkButton>
                                                <%--<asp:Label ID="lblOtherLabel" runat="server" CssClass="tableTitle" ForeColor="#205A94"
                                                    Text="1. Employee&#39;s Information"></asp:Label>--%>
                                                <%--<label id="lblOtherLabel" runat="server" style="font-size: 20px; font-weight: bold; color: darkcyan;">1. Employee&#39;s Information</label>--%>
                                            </div>

                                            &emsp;                                       
                                            &emsp;

                                            <div id="OtherDetails" visible="true" runat="server" class="container" style="margin: 5px">
                                                <%--<asp:GridView ID="gvOther" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    Width="100%" CssClass="table4" HorizontalAlign="Left" OnRowDataBound="gvOther_RowDataBound"
                                                    ToolTip="Employee Details">--%>
                                                <asp:GridView ID="gvOther" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvOther_RowDataBound"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblentrytype" runat="Server" Text='<%# Eval("ENTRY_TYPE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblentrylen" runat="Server" Text='<%# Eval("ENTRY_LENGTH") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEntryMode" runat="Server" Text='<%# Eval("ENTRY_MODE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee's Information">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Header" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="200" ToolTip="Enter other details"
                                                                    Text='<%# Eval("OTHER_DETAILS") %>' Width="130px"></asp:TextBox>
                                                                <asp:CheckBox ID="chkAgree" runat="server" Visible="false" />
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
                                        </div>

                                        &emsp;                                       
                                        &emsp;
                                        &emsp;

                                        <div class="form-group">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lblInvDetails" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                    OnClick="lblInvDetails_Click" Visible="true" AutoPostBack="true" OnClientClick="showLoader();">2. Investment Declaration</asp:LinkButton>
                                                <%-- <asp:Label ID="lblInvDetailsLabel" runat="server" CssClass="tableTitle" ForeColor="#205A94"
                                                    Text="2. Investment Declaration"></asp:Label>--%>
                                                <%--<asp:label id="lblInvDetailsLabel" runat="server" style="font-size: 20px; font-weight: bold; color: darkcyan;">2. Investment Declaration</asp:label>--%>
                                            </div>



                                            <div id="InvDetails" visible="true" runat="server" class="container" style="margin: 5px">
                                                <%--<asp:GridView ID="gvInv" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="Investment details" OnRowDataBound="gvInv_RowDataBound"
                                                    ShowFooter="True">--%>

                                                <div class="form-group" id="divInvNote" runat="server" style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: darkorange; font-weight: bold; text-align: center;">
                                                    Only single file is allowed per section. Only PDF,JPG/JPEG,7Z,ZIP and RAR files
                                            allowed. In case of multiple files, kindly zip and upload.
                                                </div>
                                                <div class="form-group" id="divNote" runat="server" style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: darkorange; font-weight: bold; text-align: center;">
                                                    Update password for file/s wherever applicable before clicking on Upload, else
                                            the password won't get saved.
                                                </div>

                                                <asp:GridView ID="gvInv" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvInv_RowDataBound" ShowFooter="True"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <FooterStyle CssClass="total" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                        Font-Strikeout="False" Font-Underline="False" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHead" runat="Server" Text='<%# Eval("is_header") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTooltip" runat="Server" Text='<%# Eval("TOOLTIP") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details of Investments">
                                                            <ItemStyle CssClass="GridViewItem" Width="240px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                <%--<asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("INV_WEBDESC_MINI") %>' />--%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Total :
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Limit">
                                                            <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltxtAmt" runat="Server" Text='<%# Eval("AMOUNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invested Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                            ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="100px"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                    ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="100px"
                                                                    AutoPostBack="True" OnTextChanged="txtAmt_TextChanged"></asp:TextBox>
                                                                <asp:CheckBox ID="chkAgree" runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTot" runat="server" Text=""></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <FooterTemplate>
                                                                <div style="text-align: center;">
                                                                    <asp:Label ID="lblAprAmt" runat="server" Text="Label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtPerm" runat="server" Text='<%# Eval("AMOUNT_VERIFIED") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotPerm" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pending Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <FooterTemplate>
                                                                <div style="text-align: center;">
                                                                    <asp:Label ID="lblPndAmt" runat="server" Text="Label"></asp:Label>
                                                                </div>

                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtPend" runat="server" Text='<%# Eval("AMOUNT_PENDING") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotPerm" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <FooterTemplate>
                                                                <div style="text-align: center;">
                                                                    <asp:Label ID="lblRjcAmt" runat="server" Text="Label"></asp:Label>
                                                                </div>

                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtRej" runat="server" Text='<%# Eval("AMOUNT_REJECTED") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotRej" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected Reason">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select Support Document">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="40px" />
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="fupUpload" runat="server" />
                                                                <asp:LinkButton ID="lnkShowDoc" runat="server" Visible="false" OnClick="lnkShowDoc_Click" ForeColor="Blue">Download</asp:LinkButton>
                                                                <asp:Label ID="lblDocAddr" runat="server" Text="" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Password (if any)">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="input" MaxLength="50" ToolTip="Enter Document Password" placeholder="Password"
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="20px" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/images/up.jpg" ToolTip="Upload Documents"
                                                                    OnClick="btnUpload_Click" />
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
                                                                <td class="GridViewHeader" style="width: 600px">Details of Investments
                                                                </td>
                                                                <td class="GridViewHeader" style="width: 130px">Amount
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        &emsp;                                       
                                        &emsp;
                                        &emsp;

                                        <div class="form-group">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkRent" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                    OnClick="lnkRent_Click" Visible="true" AutoPostBack="true" OnClientClick="showLoader();">3. Rent Declaration</asp:LinkButton>
                                                <%-- <asp:Label ID="lblRentLabel" runat="server" CssClass="tableTitle" ForeColor="#205A94"
                                                    Text="3. Rent Declaration"></asp:Label>--%>
                                                <%--<asp:label id="lblRentLabel" runat="server" style="font-size: 20px; font-weight: bold; color: darkcyan;">3. Rent Declaration</asp:label>--%>
                                            </div>



                                            <div id="RentDetails" visible="true" runat="server" class="container" style="margin: 5px">

                                                <div class="form-group" style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: darkorange; font-weight: bold; text-align: center;">
                                                    Fields marked with asterisk (*) are mandatory (if you have declared rent).
                                                </div>
                                                <%--<asp:GridView ID="gvLandlordDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="Landlord Details" Width="100%"
                                                    OnRowDataBound="gvLandlordDetails_RowDataBound">--%>
                                                <asp:GridView ID="gvLandlordDetails" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvLandlordDetails_RowDataBound"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblentrytype" runat="Server" Text='<%# Eval("ENTRY_TYPE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblentrylen" runat="Server" Text='<%# Eval("ENTRY_LENGTH") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEntryMode" runat="Server" Text='<%# Eval("ENTRY_MODE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Landlord Information">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                <asp:Label ID="lblDescAsterisk" runat="Server" Text='*' Visible="false" ForeColor="Red" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Header" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtAmtLnd" runat="server" CssClass="input" MaxLength="500" ToolTip="Enter other details"
                                                                            Text='<%# Eval("OTHER_DETAILS") %>' Width="250px" OnTextChanged="txtAmtLnd_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                                <asp:Label ID="lblAsterisk" runat="Server" Text='*' Visible="false" ForeColor="Red" />
                                                                <asp:Label ID="lblAsterisk2" runat="Server" Text='&nbsp;&nbsp;' Visible="false" />
                                                                <asp:TextBox ID="txtAmtLnd" runat="server" CssClass="input" MaxLength="200" ToolTip="Enter other details"
                                                                    Text='<%# Eval("OTHER_DETAILS") %>' Width="250px"></asp:TextBox>
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

                                                &emsp;
                                                &emsp;

                                                <div class="form-group">
                                                    <label style="display: inline-block; vertical-align: top;">Rented Property Address<span style="color: Red;">*</span> :</label>
                                                    <asp:TextBox ID="txtAddress" runat="server" Height="53px" TextMode="MultiLine" Width="278px"></asp:TextBox>
                                                </div>

                                                &emsp;
                                                &emsp;

                                                <%--<asp:GridView ID="gvRent" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="Rent Declaration" ShowFooter="True"
                                                    OnRowDataBound="gvRent_RowDataBound">--%>

                                                <asp:GridView ID="gvRent" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvRent_RowDataBound" ShowFooter="True"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <FooterStyle CssClass="total" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                        Font-Strikeout="False" Font-Underline="False" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details of Rent Paid">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Total :
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="Header" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderText="Limit">
                                                            <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Limit">
                                                            <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLimitLand" runat="Server" Text='<%# Eval("LIMIT_LAND") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltxtAmt" runat="Server" Text='<%# Eval("AMOUNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSel" runat="server" AutoPostBack="True" OnCheckedChanged="chkSel_CheckedChanged" />
                                                                &nbsp;Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                            ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                            AutoPostBack="True" OnTextChanged="txtRentAmt_TextChanged"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                    ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                    AutoPostBack="True" OnTextChanged="txtRentAmt_TextChanged"></asp:TextBox>
                                                                <asp:CheckBox ID="chkAgree" runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTot" runat="server" Text="Label"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PAN of Landlord">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkPAN" runat="server" AutoPostBack="True" OnCheckedChanged="chkPan_CheckedChanged" />
                                                                &nbsp;PAN of Landlord
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPAN" runat="server" CssClass="input" Text='<%# Eval("LANLORD_PAN") %>'
                                                                    MaxLength="10" Width="130px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <FooterTemplate>
                                                                <div style="text-align: center;">
                                                                    <asp:Label ID="lblAprAmt" runat="server" Text="Label" Style="text-align: center;"></asp:Label>
                                                                </div>

                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtPerm" runat="server" Text='<%# Eval("AMOUNT_VERIFIED") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotPerm" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pending Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <FooterTemplate>
                                                                <div style="text-align: center;">
                                                                    <asp:Label ID="lblPndAmt" runat="server" Text="Label" Style="text-align: center;"></asp:Label>
                                                                </div>

                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtPend" runat="server" Text='<%# Eval("AMOUNT_PENDING") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotPerm" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <FooterTemplate>
                                                                <div style="text-align: center;">
                                                                    <asp:Label ID="lblRjcAmt" runat="server" Text="Label" Style="text-align: center;"></asp:Label>
                                                                </div>

                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtRej" runat="server" Text='<%# Eval("AMOUNT_REJECTED") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotRej" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected Reason">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
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
                                                                <td class="GridViewHeader" style="width: 600px">Details of Rent Paid
                                                                </td>
                                                                <td class="GridViewHeader" style="width: 130px">Amount
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                                &emsp;
                                            &emsp;
                                            <div id="div5" runat="server" visible="false" class="form-group">
                                                <div class="form-group" id="divRentUpload" runat="server" style="border: 2px solid gray; padding: 10px; margin-top: 10px;">
                                                    <div class="form-group" runat="server" id="rentDocs">
                                                        <asp:LinkButton ID="lnkDownloadHRADeclaration" runat="server" Style="font-weight: bold;"
                                                            OnClick="lnkDownloadHRADeclaration_Click" ForeColor="Blue">Download Declaration Form for both HRA and Loan benefits in the Same City</asp:LinkButton>
                                                    </div>
                                                    <asp:FileUpload ID="fupRent" runat="server" />
                                                    <asp:TextBox ID="txtPasswordRent" runat="server" placeholder="Password (if any)"></asp:TextBox>
                                                    <asp:Button ID="btnUploadRentSupport" runat="server" Style="background-color: Aqua; color: Black; padding: 6px;"
                                                        Text="Upload Rent Supports" CssClass="button-xlarge-primary"
                                                        OnClick="btnUploadRentSupport_Click" />
                                                    <br />
                                                    <asp:LinkButton ID="lnkDownloadRentSupport" runat="server" Style="font-weight: bold;"
                                                        OnClick="lnkDownloadRentSupport_Click" ForeColor="Blue">Download Saved Support Documents</asp:LinkButton>
                                                    <div class="form-group" id="divRentNote" runat="server" style="margin-top: 10px; color: orangered; font-weight: bold;">
                                                        Only single file is allowed. Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed. In
                                                        case of multiple files, kindly zip and upload.
                                                    </div>
                                                </div>
                                            </div>
                                            </div>


                                        </div>

                                        <div id="rentNew" runat="server" visible="false" class="form-group">
                                            <asp:LinkButton ID="lnkRentNew" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                OnClick="lnkRentNew_Click" AutoPostBack="true">3. Rent Declaration</asp:LinkButton>
                                        </div>

                                        &emsp;
                                        &emsp;

                                        <div class="form-group">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnk12" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                    OnClick="lnk12_Click" Visible="true" AutoPostBack="true" OnClientClick="showLoader();">4. Interest on Housing Loan / Details of Income / (Loss) From Other Than Salary</asp:LinkButton>
                                                <%--<asp:Label ID="lbl12Label" runat="server" CssClass="tableTitle" ForeColor="#205A94"
                                                    Text="4. Interest on Housing Loan / Details of Income / (Loss) From Other Than Salary"></asp:Label>--%>
                                                <%--<asp:label id="lbl12Label" runat="server" style="font-size: 20px; font-weight: bold; color: darkcyan;">4. Interest on Housing Loan / Details of Income / (Loss) From Other Than Salary</asp:label>--%>
                                            </div>

                                            <div id="TwelveDetails" visible="true" runat="server" class="container" style="margin: 5px">
                                                <%--<asp:GridView ID="gvLenderDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="Lender Details">--%>
                                                <asp:GridView ID="gvLenderDetails" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblentrytype" runat="Server" Text='<%# Eval("ENTRY_TYPE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblentrylen" runat="Server" Text='<%# Eval("ENTRY_LENGTH") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEntryMode" runat="Server" Text='<%# Eval("ENTRY_MODE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Lender Information">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Header" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtAmtLnd" runat="server" CssClass="input" MaxLength="500" ToolTip="Enter other details"
                                                                            Text='<%# Eval("OTHER_DETAILS") %>' Width="250px" OnTextChanged="txtAmtLnd_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                                <%--<asp:Label ID="lblAsterisk" runat="Server" Text='*'  ForeColor="Red" />--%>
                                                                <asp:TextBox ID="txtAmtLnd" runat="server" CssClass="input" MaxLength="500" MinLength="10" ToolTip="Enter other details"
                                                                    Text='<%# Eval("OTHER_DETAILS") %>' Width="250px" OnTextChanged="txtAmtLnd_TextChanged"
                                                                    AutoPostBack="true"></asp:TextBox>
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

                                                &emsp;
                                                &emsp; 

                                                <%--<asp:GridView ID="gvtwelve" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="12c Declaration" OnRowDataBound="gvtwelve_RowDataBound"
                                                    ShowFooter="True">--%>
                                                <asp:GridView ID="gvtwelve" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvtwelve_RowDataBound" ShowFooter="True"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <FooterStyle CssClass="total" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                        Font-Strikeout="False" Font-Underline="False" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHead" runat="Server" Text='<%# Eval("is_header") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRead" runat="Server" Text='<%# Eval("IS_READONLY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Formula" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFormula" runat="Server" Text='<%# Eval("FORMULA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IsTotal" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIsTotal" runat="Server" Text='<%# Eval("IS_TOTAL") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <FooterTemplate>
                                                                Total :
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true" HeaderText="Limit">
                                                            <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltxtAmt" runat="Server" Text='<%# Eval("AMOUNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTot" runat="server" Text="Label"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                    ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                    AutoPostBack="True" OnTextChanged="txttwelveAmt_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Property Address">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPropAdd" runat="server" CssClass="input" TextMode="MultiLine"
                                                                    Text='<%# Eval("PROP_ADDRESS") %>' MaxLength="500" Width="130px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <FooterTemplate>
                                                                <div style="text-align: center;">
                                                                    <asp:Label ID="lblAprAmt" runat="server" Text="Label" Style="text-align: center;"></asp:Label>
                                                                </div>

                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtPerm" runat="server" Text='<%# Eval("AMOUNT_VERIFIED") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotPerm" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pending Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <FooterTemplate>
                                                                <div style="text-align: center;">
                                                                    <asp:Label ID="lblPndAmt" runat="server" Text="Label" Style="text-align: center;"></asp:Label>
                                                                </div>

                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtPend" runat="server" Text='<%# Eval("AMOUNT_PENDING") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotPerm" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <FooterTemplate>
                                                                <div style="text-align: center;">
                                                                    <asp:Label ID="lblRjcAmt" runat="server" Text="Label" Style="text-align: center;"></asp:Label>
                                                                </div>

                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtRej" runat="server" Text='<%# Eval("AMOUNT_REJECTED") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotRej" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected Reason">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
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
                                                                <td class="GridViewHeader" style="width: 600px">Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary
                                                                </td>
                                                                <td class="GridViewHeader" style="width: 130px">Amount
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                &emsp;
                                            &emsp;

                                                <div id="div4" runat="server" visible="false" class="form-group">
                                                    <div class="form-group" id="div3" runat="server" style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: darkorange; font-weight: bold; text-align: center;">
                                                        Update password for file/s wherever applicable before clicking on Upload, else
                                                the password won't get saved.
                                                    </div>

                                                    <div class="form-group" id="divLoanUpload" runat="server" style="border: 2px solid gray; padding: 10px; margin-top: 10px;">
                                                        <div runat="server" id="loanDocs">
                                                            <asp:LinkButton ID="lnkDownloadLoanDeclaration" runat="server" Style="font-weight: bold;"
                                                                OnClick="lnkDownloadLoanDeclaration_Click" ForeColor="Blue">Download Joint Loan Declaration Form</asp:LinkButton>
                                                            <br />
                                                            <asp:LinkButton ID="lnkDownloadSelfOccupationDeclaration" runat="server" Style="font-weight: bold;"
                                                                OnClick="lnkDownloadSelfOccupationDeclaration_Click" ForeColor="Blue">Download Declaration Form for Self Occupation of House Property</asp:LinkButton>
                                                        </div>
                                                        <asp:FileUpload ID="fupLoan" runat="server" />
                                                        <asp:TextBox ID="txtPasswordLoan" runat="server" placeholder="Password (if any)"></asp:TextBox>
                                                        <asp:Button ID="btnUploadLoanSupport" runat="server" Style="background-color: Aqua; color: Black; padding: 6px;"
                                                            Text="Upload Support Documents" CssClass="button-xlarge-primary"
                                                            OnClick="btnUploadLoanSupport_Click" />
                                                        <br />
                                                        <asp:LinkButton ID="lnkDownloadLoanSupport" runat="server" Style="font-weight: bold;"
                                                            OnClick="lnkDownloadLoanSupport_Click" ForeColor="Blue">Download Saved Support Documents</asp:LinkButton>
                                                        <div id="divLoanNote" runat="server" style="margin-top: 10px; color: red; font-weight: bold;">
                                                            Only single file is allowed. Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed. In
                                                    case of multiple files, kindly zip and upload.
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>



                                        <div id="tbl12B" runat="server" visible="false" class="container" style="margin: 5px">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnk12b" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                    OnClick="lnk12b_Click" Visible="true" AutoPostBack="true" OnClientClick="showLoader();">5. Details of Income From Previous Employer</asp:LinkButton>
                                                <asp:Label ID="lbl12bLable" runat="server" CssClass="tableTitle" ForeColor="#205A94"
                                                    Text="5. Details of Income From Previous Employer"></asp:Label>
                                                <%--<asp:label id="lbl12bLable" runat="server" style="font-size: 20px; font-weight: bold; color: darkcyan;">5. Details of Income From Previous Employer</asp:label>--%>
                                            </div>

                                            <div class="form-group" id="TwelveBDetails" visible="true" runat="server">
                                                <%--<asp:GridView ID="gvTwelB" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="12c Declaration" ShowFooter="false">--%>
                                                <asp:GridView ID="gvTwelB" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <FooterStyle CssClass="total" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                        Font-Strikeout="False" Font-Underline="False" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHead" runat="Server" Text='<%# Eval("is_header") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRead" runat="Server" Text='<%# Eval("IS_READONLY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Formula" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFormula" runat="Server" Text='<%# Eval("FORMULA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IsTotal" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIsTotal" runat="Server" Text='<%# Eval("IS_TOTAL") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details of Income From Previous Employer">
                                                            <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                            <FooterTemplate>
                                                                Total :
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Header" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderText="Limit">
                                                            <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTot" runat="server" Text="Label"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                            ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                            AutoPostBack="True" OnTextChanged="txttwelveAmt_TextChanged"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                    ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                    AutoPostBack="True" OnTextChanged="txtTwelvBAmt_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtPerm" runat="server" Text='<%# Eval("AMOUNT_VERIFIED") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotPerm" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pending Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtPend" runat="server" Text='<%# Eval("AMOUNT_PENDING") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotPerm" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected Amount">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmtRej" runat="server" Text='<%# Eval("AMOUNT_REJECTED") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                        <asp:Label ID="lblTotRej" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>--%>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected Reason">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
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
                                                                <td class="GridViewHeader" style="width: 600px">Details of Income From Previous Employer
                                                                </td>
                                                                <td class="GridViewHeader" style="width: 130px">Amount
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                                <div id="divHiddenDoc5" runat="server" visible="false" class="form-group">
                                                    <div class="form-group" id="div12BNote" runat="server" style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: red; font-weight: bold; text-align: center;">
                                                        Only single file is allowed. In case of multiple files, kindly zip and upload.
                                                    </div>
                                                    <div class="form-group" id="div12BUpload" runat="server" style="border: 2px solid gray; padding: 10px; margin-top: 10px;">
                                                        <asp:FileUpload ID="fupPrev" runat="server" />
                                                        <asp:TextBox ID="txtPassword12B" runat="server" placeholder="Password (if any)"></asp:TextBox>
                                                        <asp:Button ID="btnUpload12BSupport" runat="server" Style="background-color: Aqua; color: Black; padding: 6px;"
                                                            Text="Upload Support Documents" CssClass="button-xlarge-primary"
                                                            OnClick="btnUpload12BSupport_Click" />
                                                        <br />
                                                        <asp:LinkButton ID="lnkDownload12BSupport" runat="server" Style="font-weight: bold;"
                                                            OnClick="lnkDownload12BSupport_Click">Download Saved Documents</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div id="supportsFlexi" runat="server" visible="true" class="container" style="margin: 5px">
                                        <div class="form-group">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="tableTitle" ForeColor="#205A94"
                                                OnClick="lnk12b_Click" Visible="true" AutoPostBack="true" OnClientClick="showLoader();">5. Flexipay Supports</asp:LinkButton>
                                            <%--<asp:Label ID="Label1" runat="server" CssClass="tableTitle" ForeColor="#205A94" Text="5. Flexipay Supports"></asp:Label>--%>
                                        </div>

                                        <div class="form-group" style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: Maroon; font-weight: bold; text-align: center;">
                                            Only single file is allowed per flexi component. In case of multiple files, kindly
                                            zip and upload.
                                        </div>
                                        <div class="form-group" style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: Red; font-weight: bold; text-align: center;">
                                            Please update no. of dependents including self, spouse, upto 2 children, parents
                                            and upto 2 dependent brother and/ or sister.
                                        </div>
                                        <hr />
                                        <span style="font-weight: bold;">Upload Supporting Documents:</span>
                                        <br />
                                        <asp:LinkButton ID="lnkForms" runat="server" Style="font-weight: bold;" OnClick="lnkForms_Click">Download Flexi Forms</asp:LinkButton><br />

                                        <table class="table4" border="1" style="border-collapse: collapse; margin-top: 10px;"
                                            width="100%">
                                            <tr>
                                                <td style="font-weight: bold; text-align: center;">Flexi Details
                                                </td>
                                                <td style="font-weight: bold; text-align: center; width: 30px;">No. of dependents including self
                                                </td>
                                                <td style="font-weight: bold; text-align: center;">Amount
                                                </td>
                                                <td style="font-weight: bold; text-align: center;">Approved Amount
                                                </td>
                                                <td style="font-weight: bold; text-align: center;">Rejected Amount
                                                </td>
                                                <td style="font-weight: bold; text-align: center;">Remarks
                                                </td>
                                                <td style="font-weight: bold; text-align: center;">Select File
                                                </td>
                                                <td style="font-weight: bold; text-align: center;">Upload
                                                </td>
                                                <td style="font-weight: bold; text-align: center;"></td>
                                            </tr>
                                            <tr id="trLTA" runat="server">
                                                <td>LEAVE TRAVEL ASSISTANCE
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:TextBox ID="txtLTANo" CssClass="input" runat="server" onkeypress="return validateFloat(event, 0);"
                                                        Width="35px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:TextBox ID="txtLTAAmt" CssClass="input" runat="server" OnTextChanged="txtLTAAmt_TextChanged"
                                                        AutoPostBack="true" onkeypress="return validateFloat(event, 0);"></asp:TextBox>
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
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnUploadLTA" runat="server" Text="Upload" CssClass="button-xlarge-primary"
                                                        OnClick="btnUploadLTA_Click" />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:LinkButton ID="lnkDownloadLTA" runat="server" Style="font-weight: bold;" OnClick="lnkDownloadLTA_Click"
                                                        Visible="false"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr id="trTLR" runat="server">
                                                <td>TELEPHONE LANDLINE REIMBURSEMENT
                                                </td>
                                                <td></td>
                                                <td style="text-align: center;">
                                                    <asp:TextBox ID="txtTelAmt" CssClass="input" runat="server" OnTextChanged="txtTelAmt_TextChanged"
                                                        AutoPostBack="true" onkeypress="return validateFloat(event, 0);"></asp:TextBox>
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
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnUploadTel" runat="server" Text="Upload" CssClass="button-xlarge-primary"
                                                        OnClick="btnUploadTel_Click" />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:LinkButton ID="lnkDownloadTel" runat="server" Style="font-weight: bold;" OnClick="lnkDownloadTel_Click"
                                                        Visible="false"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr id="trCMF" runat="server">
                                                <td>CAR MAINTAINANCE AND FUEL
                                                </td>
                                                <td></td>
                                                <td style="text-align: center;">
                                                    <asp:TextBox ID="txtFuelAmt" CssClass="input" runat="server" OnTextChanged="txtFuelAmt_TextChanged"
                                                        AutoPostBack="true" onkeypress="return validateFloat(event, 0);"></asp:TextBox>
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
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnUploadFuel" runat="server" Text="Upload" CssClass="button-xlarge-primary"
                                                        OnClick="btnUploadFuel_Click" />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:LinkButton ID="lnkDownloadFuel" runat="server" Style="font-weight: bold;" OnClick="lnkDownloadFuel_Click"
                                                        Visible="false"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr id="trCDS" runat="server">
                                                <td>CAR DRIVERS SALARY
                                                </td>
                                                <td></td>
                                                <td style="text-align: center;">
                                                    <asp:TextBox ID="txtDriverAmt" CssClass="input" runat="server" OnTextChanged="txtDriverAmt_TextChanged"
                                                        AutoPostBack="true" onkeypress="return validateFloat(event, 0);"></asp:TextBox>
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
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnUploadDriver" runat="server" Text="Upload" CssClass="button-xlarge-primary"
                                                        OnClick="btnUploadDriver_Click" />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:LinkButton ID="lnkDownloadDriver" runat="server" Style="font-weight: bold;"
                                                        OnClick="lnkDownloadDriver_Click" Visible="false"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    &emsp;
                                        &emsp;
                                        
                                            <div id="divHidden" runat="server" visible="false" class="form-group">

                                                <div class="form-group" style="border: double; margin: 5px">
                                                    <div style="margin: 5px">
                                                        <span style="font-weight: bold;">Disclaimer: </span>
                                                        <ol style="font-weight: bold; text-align: justify; font-style: italic;">
                                                            <li style="margin-bottom: 10px;">I will preserve the original receipts / certificates
                                                                                    as enclosed / shown above and produce the same for verification as and when called.</li>
                                                            <li style="margin-bottom: 10px;">This is to certify that the above-mentioned residential
                                                                                    accommodation for which I have produced rent receipts is not owned by me.</li>
                                                            <li style="margin-bottom: 10px;">I hereby declare that the claim for deduction / exemptions
                                                                                    is claimed only by me and not by any joint holder.</li>
                                                            <li>I do hereby declare and certify that the particulars and evidences given by me for
                                                                                    the purpose of claim of deduction of tax u/s 192 are true and correct. I hereby
                                                                                    authorise the company to convert this data in the statutorily prescribed format
                                                                                    under the Income Tax Act, 1961. Any Income Tax liability arising out of a wrong
                                                                                    declaration will be my responsibility, and I undertake to indemnify the Company
                                                                                    and its officers from all consequences, monetary and otherwise, arising out of any
                                                                                    incorrect and/or incomplete information provided in this declaration.</li>
                                                    </div>

                                                    </ol>
                                                </div>

                                                <div style="text-align: center;">
                                                    <asp:CheckBox ID="chkAgreAll" Text="&nbsp;&nbsp;&nbsp; I Agree" runat="server" ForeColor="DarkSlateBlue" Font-Bold="True" Font-Size="17px" />
                                                </div>


                                                <hr />
                                                <div style="width: 100%; text-align: center;">
                                                    <asp:Button ID="btnSubmitAll" runat="server" OnClick="btnSubmitAll_Click" class="btn btn-success" Style="width: 100px; height: 50px; font-weight: bold;"
                                                        Text="Submit" />
                                                </div>
                                                <hr />
                                                <div id="tbl12BB" runat="server" width="100%" border="0" cellspacing="6" cellpadding="0"
                                                    class="table5 form-group">
                                                    <h2 style="font-weight: bold; margin-bottom: 10px;">Form 12BB:</h2>
                                                    <asp:LinkButton ID="btnPrint" runat="server" Style="font-weight: bold;" OnClick="btnPrint_Click" ForeColor="Blue">Download Form 12BB</asp:LinkButton>
                                                    <div id="div12BB" runat="server" style="border: 2px solid gray; padding: 10px; margin-top: 10px;">
                                                        <asp:FileUpload ID="fup12BB" runat="server" />
                                                        <asp:Button ID="btnUploadForm12BB" runat="server" Style="background-color: Aqua; color: Black; padding: 6px;"
                                                            Text="Upload Form 12BB" CssClass="button-xlarge-primary"
                                                            OnClick="btnUploadForm12BB_Click" />
                                                        <br />
                                                        <table id="disclaimer" runat="server" class="table4N" style="margin-top: 5px; margin-bottom: 5px;"
                                                            width="100%">
                                                            <tr>
                                                                <td style="width: 10%; text-align: center;">
                                                                    <asp:CheckBox ID="chkAgree" runat="server" />
                                                                </td>
                                                                <td style="width: 90%; font-weight: bold;">Disclaimer:
                                                        <br />
                                                                    I hereby declare that all the information given by me is true and correct. Any Income
                                                        Tax liability arising out of a wrong declaration will be my responsibility, and
                                                        I undertake to indemnify the Company and its officers from all consequences, monetary
                                                        and otherwise, arising out of any incorrect and/or incomplete information provided
                                                        in this declaration.
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:LinkButton ID="btnDownloadForm12BB" runat="server" Style="font-weight: bold;"
                                                            OnClick="btnDownloadForm12BB_Click" ForeColor="Blue">Download Saved Form 12BB</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <hr />

                                                <div id="prevTable" runat="server" visible="false">
                                                    <h3>Previous Uploads</h3>
                                                </div>

                                                <div class="form-group" id="Div1" visible="true" runat="server">
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
                                                                        <asp:Literal ID="Literal6" runat="server" Text="No files uploaded." />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>

                                                <div id="filesFlexi" runat="server" visible="false">
                                                    <h3>Previous Uploads (Flexi)</h3>
                                                </div>

                                                <div class="form-group" id="Div2" visible="true" runat="server">
                                                    <asp:GridView ID="gvPrevFilesFlexi" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
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
                                                                        OnClick="lnkBtnOpenFilePrevFlexi_Click" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                <tr>
                                                                    <td class="GridViewHeader" style="width: 10%">
                                                                        <asp:Literal ID="Literal6" runat="server" Text="No files uploaded." />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>

                                            </div>

                                    </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>

                                    <asp:PostBackTrigger ControlID="btnLoadDetails" />
                                    <asp:PostBackTrigger ControlID="lnkOther" />
                                    <asp:PostBackTrigger ControlID="lblInvDetails" />
                                    <asp:PostBackTrigger ControlID="lnkRent" />
                                    <asp:PostBackTrigger ControlID="lnk12" />
                                    <asp:PostBackTrigger ControlID="lnk12b" />

                                    <asp:PostBackTrigger ControlID="lnkManual" />
                                    <asp:PostBackTrigger ControlID="lnkFAQ" />
                                    <asp:PostBackTrigger ControlID="btnPrint" />
                                    <asp:PostBackTrigger ControlID="btnUploadForm12BB" />
                                    <asp:PostBackTrigger ControlID="btnDownloadForm12BB" />
                                    <asp:PostBackTrigger ControlID="btnUndertaking" />
                                    <asp:PostBackTrigger ControlID="btnUploadUndertaking" />
                                    <asp:PostBackTrigger ControlID="btnDownloadUndertaking" />
                                    <asp:PostBackTrigger ControlID="lnkDownloadHRADeclaration" />
                                    <asp:PostBackTrigger ControlID="btnUploadRentSupport" />
                                    <asp:PostBackTrigger ControlID="lnkDownloadRentSupport" />
                                    <asp:PostBackTrigger ControlID="lnkDownloadLoanDeclaration" />
                                    <asp:PostBackTrigger ControlID="lnkDownloadSelfOccupationDeclaration" />
                                    <asp:PostBackTrigger ControlID="btnUploadLoanSupport" />
                                    <asp:PostBackTrigger ControlID="lnkDownloadLoanSupport" />
                                    <asp:PostBackTrigger ControlID="btnUpload12BSupport" />
                                    <asp:PostBackTrigger ControlID="lnkDownload12BSupport" />
                                    <asp:PostBackTrigger ControlID="lnkForms" />

                                    <asp:PostBackTrigger ControlID="btnUploadLTA" />
                                    <asp:PostBackTrigger ControlID="lnkDownloadLTA" />
                                    <asp:PostBackTrigger ControlID="btnUploadTel" />
                                    <asp:PostBackTrigger ControlID="lnkDownloadTel" />
                                    <asp:PostBackTrigger ControlID="btnUploadFuel" />
                                    <asp:PostBackTrigger ControlID="lnkDownloadFuel" />
                                    <asp:PostBackTrigger ControlID="btnUploadDriver" />
                                    <asp:PostBackTrigger ControlID="lnkDownloadDriver" />
                                    <asp:PostBackTrigger ControlID="btnSubmitAll" />
                                    <%--<asp:PostBackTrigger ControlID="Note" />--%>
                                    <%--<asp:PostBackTrigger ControlID="SelTaxOpt" />--%>
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>

            </div>
        </section>
        <script type="text/javascript">
            // Initial load
            $(document).ready(function () {
                $('#<%= drpEmpType.ClientID %>').select2();
            });

            // After every postback
            Sys.Application.add_load(function () {
                $('#<%= drpEmpType.ClientID %>').select2();
            });
        </script>

    </section>
</asp:Content>
