<%@ Page Title="" Language="C#"  MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="TelephoneReimb.aspx.cs" Inherits="NewPortal2023.ESS.TelephoneReimb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap -->
    <%--<script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script src='https:/
/cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'
        type="text/javascript"></script>
    <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
        media="screen" />
    <!-- Bootstrap -->
    <!-- Bootstrap DatePicker -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>--%>

    <%--    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">--%>

    <!-- Bootstrap DatePicker -->
    <script type="text/javascript">
        $(function () {
            $('[id*=txtBillDateHand]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
        $(function () {
            $('[id*=txtBillDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Telephone Reimbursement</h3>
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
                                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                        </div>
                                        <section class="content-header" runat="server" id="SectionList">

                                            <div class="container-fluid">
                                                <!-- SELECT2 EXAMPLE -->
                                                <div class="card card-default">


                                                    <div class="row mb-2" style="margin-top: 10px; margin-right: 2px;">
                                                        <div class="col-sm-6">
                                                        </div>
                                                        <div class="col-sm-6" style="">
                                                            <ol class="breadcrumb float-sm-right">
                                                                <li class="breadcrumb-item"><a href="#">Expense Reimbursement</a></li>

                                                                <li class="breadcrumb-item active">Telephone Reimbursement</li>
                                                            </ol>
                                                        </div>
                                                    </div>

                                                    <div class="card-header" runat="server" id="divNote">

                                                        <div class="col-sm-12">

                                                            <div class="col-sm-12 ">
                                                                <div class="col-sm-6 table table-bordered">

                                                                    <label style="color: red">Notes :- </label>
                                                                    &emsp;&emsp;&emsp;
                                <asp:Label runat="server" BackColor="LightGreen">&emsp;&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                <asp:Label runat="server" Text="Eligible" Style="text-align: left;"></asp:Label>
                                                                    &emsp;&emsp;&emsp;&emsp;
                                 <asp:Label runat="server" BackColor="Yellow">&emsp;&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                <asp:Label runat="server" Text="Non-Eligible" Style="text-align: left;"></asp:Label>

                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="card-body">
                                                        <div class="row">
                                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="showLoader();" />
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <label style="font-size: 30px;">List Of Telephone Claim</label>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-12">

                                                                <%--<asp:GridView ID="gvTelClaim" runat="server" ToolTip="TELEPHONE CLAIM LIST" OnRowDataBound="gvTelClaim_RowDataBound" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed">--%>
                                                                <asp:GridView ID="gvTelClaim" runat="server" AutoGenerateColumns="False"
                                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvTelClaim_RowDataBound"
                                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Sr.No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSrNo" runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee AId" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmpAId" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Emp_AId") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim No">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkTelClmNoClmNo" CssClass="form-group" OnClick="lnkTelClmNoClmNo_Click" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_no") %>'
                                                                                    OnClientClick="showLoader();" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Contact No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMobNo" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Mobile_No") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGrpType" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Group_Type") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bill No" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBillNo" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Bill_No") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bill Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblbillDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Bill_Date") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bill Month">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBillMonth" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Bill_Month") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClaimAmt" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Amount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtAppAmtr" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ClaimApproved_Amount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtRmks" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("CHECKERREMARKS") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtRmk" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Status") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Type" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblType" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Type_AID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Action Type">
                                                        <ItemTemplate>
                                                
                                                            <asp:DropDownList ID="drpAction" runat="server" CssClass="form-control input-sm-3" AutoPostBack="false" SelectedValue='<%# Eval("ChkAction") %>'>
                                                                <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                   
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                      
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnSubmit" Text="Submit" runat="server" OnClick="lnkBtnSubmit_Click" CssClass="btn btn-success" />
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#0069d9" Font-Bold="True" ForeColor="White" />
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <EmptyDataTemplate>
                                                                        <table class="table table-bordered table-striped">
                                                                            <tr>
                                                                                <td style="width: 10%" class="Title">
                                                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </EmptyDataTemplate>

                                                                </asp:GridView>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </section>
                                        <div id="divFrom" runat="server" visible="false">
                                            <section class="content-header">
                                                <div class="container-fluid">
                                                    <div class="row mb-2">
                                                        <div class="col-sm-6">
                                                            <h1>Telephone Reimbursement</h1>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <ol class="breadcrumb float-sm-right">
                                                                <li class="breadcrumb-item"><a href="#">Home</a></li>

                                                                <li class="breadcrumb-item active">Telephone Reimbursement</li>
                                                            </ol>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- /.container-fluid -->
                                            </section>

                                            <section class="content" id="divTravel" runat="server" visible="true">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-header">
                                                            <h3 class="card-title">Claim Type</h3>


                                                            <br />
                                                            <!-- iCheck -->

                                                            <div class="card-body">
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <!-- radio -->
                                                                        <div class="form-group clearfix">
                                                                            <div class="icheck-primary d-inline">
                                                                                <asp:RadioButton ID="radioPrimary1" Text="Handset" runat="server" OnCheckedChanged="radioPrimary1_CheckedChanged" AutoPostBack="true" GroupName="GroupType"></asp:RadioButton>


                                                                                <label for="radioPrimary1">
                                                                                </label>
                                                                            </div>
                                                                            <div class="icheck-primary d-inline">
                                                                                <asp:RadioButton ID="radioPrimary2" Text="Telephone Bill" runat="server" OnCheckedChanged="radioPrimary1_CheckedChanged" AutoPostBack="true" GroupName="GroupType"></asp:RadioButton>

                                                                                <label for="radioPrimary2">
                                                                                </label>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <div class="row" id="divHandset" runat="server" visible="false">
                                                                    <div class="col-sm-12">
                                                                        <label>
                                                                            Handset (Once in 2 Years)
                                                                        </label>

                                                                        <asp:GridView ID="grHandset" runat="server" ToolTip="Handset Reimbursement" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="HandSet">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblHandSet" runat="Server" Text='Once In 2 Year' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Eligibility Amount ">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEliAmt" runat="Server" Text='<%# Eval("Fix_Amount") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <%-- <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width: 10%" class="Title">--%>
                                                                                <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                <%--</td>
                                                    </tr>
                                                </table>--%>
                                                                            </EmptyDataTemplate>

                                                                        </asp:GridView>

                                                                    </div>
                                                                </div>

                                                                <div class="row" id="divTelephone" runat="server" visible="false">
                                                                    <div class="col-sm-12">
                                                                        <label for="radioPrimary1">
                                                                            Telephone Bill

                                                                        </label>

                                                                        <asp:GridView ID="grTelephone" runat="server" OnRowDataBound="grTelephone_RowDataBound" ToolTip="Telephone Reimbursement" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="TelePhone">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTele" runat="Server" Text='Maximum eligibility for Telephone Bills (Rs. Per month)' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Eligibility Amount">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBoarding" runat="Server" Text='<%# Eval("Fix_Amount") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <%--<table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width: 10%" class="Title">--%>
                                                                                <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                                                <%-- </td>
                                                    </tr>
                                                </table>--%>
                                                                            </EmptyDataTemplate>

                                                                        </asp:GridView>

                                                                    </div>
                                                                </div>

                                                                <div class="card-body" id="divHandsetReimbFill" runat="server" visible="false">
                                                                    <label></label>
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="Label2" runat="server" class="col-sm-3 col-form-label"> Expense Description :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox CssClass="form-control" TextMode="MultiLine" ID="txtdescript" runat="server" Style="width: 700px; height: 50px;"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="form-group row">


                                                                        <asp:Label ID="lblBillDateHand" runat="server" class="col-sm-3 col-form-label">Bill Date :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox CssClass="form-control" ID="txtBillDateHand" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label ID="lblBillNoHand" runat="server" class="col-sm-3 col-form-label">Bill No :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox CssClass="form-control" ID="txtBillNoHand" runat="server"></asp:TextBox>

                                                                        </div>


                                                                    </div>
                                                                    <br />
                                                                    <div class="form-group row">

                                                                        <asp:Label ID="lblClaimAmtHand" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox CssClass="form-control" ID="txtClaimAmtHand" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtClaimAmtHand"
                                                                                ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                ErrorMessage="Please enter a valid numeric value."
                                                                                Display="Dynamic" ForeColor="Red" />
                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="Label1" runat="server" class="col-sm-3 col-form-label">Remark :-</asp:Label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox CssClass="form-control" ID="txtUserRemark" runat="server"></asp:TextBox>

                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="row" id="divUpload" runat="server" visible="false">
                                                                    <div class="col-lg-6" id="divfileUpload" runat="server" visible="false">
                                                                        <div class="btn-group w-100">
                                                                            <asp:FileUpload ID="fupldDocument" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                            <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-6" id="divfileDisplay" runat="server" visible="false">
                                                                        Supporting files:
                                            <asp:GridView ID="gvHandsetFile" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileStorage" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenFiles" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                AutoPostback="true" OnClick="lnkBtnOpenFilesHandset_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                </EmptyDataTemplate>
                                            </asp:GridView>


                                                                    </div>
                                                                </div>


                                                                <div class="card-body" id="divTeleReimbFill" runat="server" visible="false">
                                                                    <label></label>
                                                                    <br />

                                                                    <div class="form-group row">
                                                                        <asp:Label ID="Label3" runat="server" class="col-sm-3 col-form-label"> Expense Description :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox CssClass="form-control" TextMode="MultiLine" ID="txtdiscript1" runat="server" Style="width: 700px; height: 50px;"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="form-group row" id="divAmt">
                                                                        <%--<asp:Label ID="lblFinYear" runat="server" class="col-sm-3 col-form-label">Select Financial Year :- </asp:Label>--%>
                                                                        <%-- <div class="col-sm-3">
                                            <asp:DropDownList ID="drpFinYear" runat="server" CssClass="form-control select2" Visible="false" OnSelectedIndexChanged="drpFinYear_SelectedIndexChanged" AutoPostBack="true">
                                                <%--<asp:ListItem Value="">[Select Type]</asp:ListItem>
                                            <asp:ListItem Value="Travel" Text="Travel"></asp:ListItem>
                                            <asp:ListItem Value="Entertainment" Text="Entertainment"></asp:ListItem>
                                            <asp:ListItem Value="Travel + Entertainment" Text="Travel + Entertainment"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>


                                                                        <asp:Label ID="lblPhoneNo" runat="server" class="col-sm-3 col-form-label">MobileNo :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox class="form-control" ID="txtPhoneNo" runat="server"></asp:TextBox>
                                                                        </div>

                                                                        <br />

                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <asp:Label ID="lblAnulAmt" Visible="false" runat="server" class="col-sm-3 col-form-label">Eligibility Amount (Yearly) :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox CssClass="form-control" Visible="false" ID="txtAnulAmt" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label ID="lblBal" runat="server" Visible="false" class="col-sm-3 col-form-label">Balance Amount :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox CssClass="form-control" Visible="false" ID="txtBal" runat="server"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">

                                                                        <asp:Label ID="lblBillDate" runat="server" class="col-sm-3 col-form-label">Bill Date :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox CssClass="form-control" ID="txtBillDate" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label ID="lblBillNo" runat="server" class="col-sm-3 col-form-label">Bill No :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox CssClass="form-control" ID="txtBillNo" runat="server"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">


                                                                        <%--<div class="col-sm-3">
                                                            <asp:TextBox CssClass="form-control" ID="txtBillMonth" runat="server"></asp:TextBox>

                                                        </div>--%>

                                                                        <asp:Label ID="lblBillMonth" runat="server" class="col-sm-3 col-form-label">Bill Month :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:DropDownList ID="drpBillMonth" runat="server" CssClass="form-control select2" Style="width: 100%;"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="drpBillMonth_SelectedIndexChanged">
                                                                                <asp:ListItem Value="">[Select Month]</asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                                                <asp:ListItem Value="9" Text="Sept"></asp:ListItem>
                                                                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label ID="lblClaimAmount" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox CssClass="form-control" ID="txtClaimAmount" runat="server"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtClaimAmount"
                                                                                ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                                ErrorMessage="Please enter a valid numeric value."
                                                                                Display="Dynamic" ForeColor="Red" />
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                </div>

                                                                <div class="row" id="divUplaodTel" runat="server" visible="false">
                                                                    <div class="col-lg-6" id="divfileUploadTel" runat="server" visible="false">
                                                                        <div class="btn-group w-100">
                                                                            <asp:FileUpload ID="FileUploadTel" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                            <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-6" id="divfileDisplayTel" runat="server" visible="false">
                                                                        Supporting files:
                                            <asp:GridView ID="grTelFile" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileStorageTel" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenFilesTel" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                AutoPostBack="true" OnClick="lnkBtnOpenFilesTel_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                            <section class="content">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">


                                                        <div class="card-body">
                                                            <div class="col-sm-12" style="text-align: center;">
                                                                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-danger" OnClick="btnClose_Click" Text="Close" OnClientClick="showLoader();" />
                                                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Submit" OnClientClick="showLoader();" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="drpBillMonth" />
                                    <asp:PostBackTrigger ControlID="gvHandsetFile" />
                                    <asp:PostBackTrigger ControlID="grTelFile" />
                                    <asp:PostBackTrigger ControlID="btnAddNew" />
                                    <asp:PostBackTrigger ControlID="radioPrimary1" />
                                    <asp:PostBackTrigger ControlID="radioPrimary2" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="gvTelClaim" />

                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>

            </div>
        </section>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerFrom);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerTo);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerRfFrom);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerRfTo);

            function dtpickerFrom(sender, args) {
                if (document.getElementById('<%= txtBillDate.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtBillDate.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }

            if (document.getElementById('<%= txtBillDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtBillDate.ClientID %>'),
                    format: 'DD-MM-YYYY'
                });
            }
        </script>
    </section>
</asp:Content>


