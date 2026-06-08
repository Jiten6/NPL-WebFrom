<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="ApprovelTelephonePage.aspx.cs" Inherits="NewPortal2023.ESS.ApprovelTelephonePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Telephone Approval</h3>
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
                                        <div id="divAlert" class="alert alert-block alert-success fade in" runat="server" visible="true">
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>
                                        <section class="content-header" runat="server" id="SectionList">

                                            <div class="container-fluid">
                                                <!-- SELECT2 EXAMPLE -->
                                                <div class="card card-default">

                                                     <%--   <div class="card-body">

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
                                                        </div>--%>


                                                        <div class="row">
                                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Visible="false" runat="server" CssClass="btn btn-primary" Text="Add New" />
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <label style="font-size: 30px;">List Of Telephone Claim</label>
                                                        <br />
                                                        <%--<div class="row">--%>
                                                        <div style="overflow-x: scroll; width: 100%;">

                                                            <%--<asp:GridView ID="gvTelClaim" runat="server" ToolTip="TELEPHONE CLAIM LIST" AutoGenerateColumns="false" OnRowDataBound="gvTelClaim_RowDataBound" class="table table-bordered table-striped">--%>
                                                            <asp:GridView ID="gvTelClaim" runat="server" AutoGenerateColumns="False"
                                                                HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvTelClaim_RowDataBound"
                                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltest" runat="Server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ShowHeader="true">
                                                                        <ItemStyle VerticalAlign="Middle" />
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkSelectAll" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" runat="Server" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sr.No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSrNo" runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--    <asp:TemplateField HeaderText="Claim No">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDOMClmNoClmNo" CssClass="form-group" OnClick="lnkDOMClmNoClmNo_Click" Style="width: 100%;" TextMode="MultiLine" runat="Server" Text='<%# Eval("App_AId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Claim No">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkTelClmNoClmNo" CssClass="form-group" OnClick="lnkTelClmNoClmNo_Click" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_no") %>'
                                                                                OnClientClick="showLoader();" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employee AId" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpAId" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Emp_AId") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employee Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpCode" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employee Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpName" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_FNAME") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Claim Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtClmTypes" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ClaimTypes") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Claim Type" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtClmType" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ClmType") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mobile No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtClmDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Mobile_No") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Bill No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txFrDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Bill_No") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="10px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtFrDest" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Bill_Date") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bill Month">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtToDest" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Bill_Month") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Claim Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtClmAmt" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Amount") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Approved Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtAppAmtr" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ClaimApproved_Amount") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

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
                                                            <br />
                                                            <br />

                                                            <div class="row">
                                                                <div class="col-sm-2">
                                                                    <%--<asp:RadioButton ID="radiaoEntertainment1" Text="Entertainment" OnCheckedChanged="radiaoEntertainment_CheckedChanged" AutoPostBack="true" runat="server"></asp:RadioButton>--%>
                                                                    <asp:CheckBox ID="radiaoEntertainment1" Visible="false" Enabled="false" runat="server" Text="Non Eligible" />
                                                                    <label for="RadioButton1">
                                                                    </label>
                                                                </div>
                                                                <label id="lblActionAll" runat="server">Action :-</label>
                                                                <div class="col-sm-2">
                                                                    <asp:DropDownList ID="drActionAll" runat="server" CssClass="form-control" AutoPostBack="false">
                                                                        <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                        <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                        <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>


                                                                    </asp:DropDownList>
                                                                </div>
                                                                &nbsp;&nbsp;
                                     
                                    <label id="lblRemarksAll" runat="server">Remarks :-</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox ID="txtAllRmk" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                </div>



                                                                <div class="col-sm-1">
                                                                    <asp:Button ID="btnSubmitAll" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmitAll_Click" OnClientClick="showLoader();" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- </div>--%>
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

                                                                        <asp:GridView ID="grHandset" runat="server" ToolTip="Handset Reimbursement"  AutoGenerateColumns="False" class="table table-bordered table-striped">
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

                                                                    <br />
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="Label13" runat="server" class="col-sm-3 col-form-label"> Expense Description :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" ReadOnly="true" ID="txtdiscript" runat="server" Style="width: 700px; height: 50px;"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <%--<asp:Label ID="lblFinYearHand" runat="server" class="col-sm-3 col-form-label">Select Financial Year :- </asp:Label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="drpFinYearHand" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpFinYear_SelectedIndexChanged" AutoPostBack="true">
                                                <%--<asp:ListItem Value="">[Select Type]</asp:ListItem>
                                            <asp:ListItem Value="Travel" Text="Travel"></asp:ListItem>
                                            <asp:ListItem Value="Entertainment" Text="Entertainment"></asp:ListItem>
                                            <asp:ListItem Value="Travel + Entertainment" Text="Travel + Entertainment"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>

                                                                        <asp:Label ID="lblBillDateHand" runat="server" class="col-sm-3 col-form-label">Bill Date :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtBillDateHand" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <br />

                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <asp:Label ID="lblBillNoHand" runat="server" class="col-sm-3 col-form-label">Bill No :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtBillNoHand" runat="server"></asp:TextBox>

                                                                        </div>
                                                                        <asp:Label ID="lblClaimAmtHand" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtClaimAmtHand" runat="server"></asp:TextBox>
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
                                                                    <div class="form-group row" id="divAmt">
                                                                        <%-- <asp:Label ID="lblFinYear" runat="server" class="col-sm-3 col-form-label">Select Financial Year :- </asp:Label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="drpFinYear" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpFinYear_SelectedIndexChanged" AutoPostBack="true">
                                                <%--<asp:ListItem Value="">[Select Type]</asp:ListItem>
                                            <asp:ListItem Value="Travel" Text="Travel"></asp:ListItem>
                                            <asp:ListItem Value="Entertainment" Text="Entertainment"></asp:ListItem>
                                            <asp:ListItem Value="Travel + Entertainment" Text="Travel + Entertainment"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>

                                                                        <asp:Label ID="lblPhoneNo" runat="server" class="col-sm-3 col-form-label">MobileNo :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox class="form-control" ReadOnly="true" ID="txtPhoneNo" runat="server"></asp:TextBox>
                                                                        </div>

                                                                        <br />

                                                                    </div>


                                                                    <div class="form-group row">

                                                                        <asp:Label ID="lblBillDate" runat="server" class="col-sm-3 col-form-label">Bill Date :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtBillDate" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label ID="lblBillNo" runat="server" class="col-sm-3 col-form-label">Bill No :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtBillNo" runat="server"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">

                                                                        <asp:Label ID="lblBillMonth" runat="server" class="col-sm-3 col-form-label">Bill Month :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtBillMonth" runat="server"></asp:TextBox>

                                                                        </div>
                                                                        <asp:Label ID="lblClaimAmount" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtClaimAmount" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
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
                                                                AutoPostback="true" OnClick="lnkBtnOpenFilesTel_Click" />
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
                                                        <div>
                                                            <br />
                                                        </div>
                                                        <div class="form-group row">
                                                            &emsp;
                                            <asp:Label ID="Label12" runat="server" class="col-sm-2 col-form-label"><b>Approved Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="txtApprovedAmt" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtApprovedAmt"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>

                                                            <br />

                                                        </div>
                                                        <div class="card-body">
                                                            <div class="form-group row">

                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="drpActionType" runat="server" CssClass="form-control" AutoPostBack="false">
                                                                        <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                        <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                        <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <asp:Label ID="lblRmk" runat="server" class="col-sm-1 col-form-label">Remarks :-</asp:Label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox ID="txtRmk" runat="server" TextMode="MultiLine" CssClass="form-control col-sm-6"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>


                                                        <div class="card-body">
                                                            <div class="col-sm-12" style="text-align: center;">
                                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" OnClick="btnClose_Click" runat="server" Text="Close" OnClientClick="showLoader();" />
                                                                <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Submit" CssClass="btn btn-success" OnClientClick="showLoader();" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnApprove" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="btnSubmitAll" />
                                    <asp:PostBackTrigger ControlID="gvTelClaim" />
                                    <asp:PostBackTrigger ControlID="gvHandsetFile" />
                                    <asp:PostBackTrigger ControlID="grTelFile" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>

            </div>
        </section>
        <script>
            var handleDataTableButtons = function () {
                "use strict";
                0 !== $('#<%= gvTelClaim.ClientID %>').length &&
                $('#<%= gvTelClaim.ClientID %>').DataTable({
                        dom: "Bfrtip",
                        buttons: [{
                            extend: "copy",
                            className: "btn-sm"
                        }, {
                            extend: "csv",
                            className: "btn-sm"
                        }, {
                            extend: "excel",
                            className: "btn-sm"
                        }, {
                            extend: "pdf",
                            className: "btn-sm"
                        }, {
                            extend: "print",
                            className: "btn-sm"
                        }],
                        responsive: !0
                    })
            },
                TableManageButtons = function () {
                    "use strict";
                    return {
                        init: function () {
                            handleDataTableButtons()
                        }
                    }
                }();
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#<%= gvTelClaim.ClientID %>').dataTable();
            });
            TableManageButtons.init();
        </script>
    </section>
</asp:Content>


