<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="ApprovalDomesticPage.aspx.cs" Inherits="NewPortal2023.ESS.ApprovalDomesticPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style>
        .form-group.clearfix {
            margin-bottom: 10px; /* Adjust as needed */
        }

        .icheck-primary.d-inline {
            display: inline-block;
            margin-right: 10px; /* Adjust as needed */
        }

            .icheck-primary.d-inline label {
                margin-left: 5px; /* Adjust as needed */
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
                            <h3 style="color: white">Domestic Approval</h3>
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
                                        <section class="content" runat="server" id="SectionList">

                                            <div class="container-fluid">
                                                <!-- SELECT2 EXAMPLE -->
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="card">

                                                            <%-- <div class="card-header" runat="server" id="divNote">

                                                               <div runat="server" class="col-sm-12">
                                                                    <div class="col-sm-12 ">
                                                                        <div class="col-sm-12 table table-bordered form-horizontal">

                                                                            <label style="color: red">Notes :- </label>
                                                                            <div>
                                                                                &emsp;&emsp;
                                                                <asp:Label runat="server" BackColor="Orange">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                                <asp:Label runat="server" Text="Eligible- Beverage" Style="text-align: left;"></asp:Label>
                                                                                &emsp;&emsp;&emsp;&emsp;&emsp;
                                                                <asp:Label runat="server" BackColor="LightGreen">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                                <asp:Label runat="server" Text="Eligible Non-Beverage" Style="text-align: left;"></asp:Label>
                                                                            </div>

                                                                            <div>
                                                                                &emsp;&emsp;
                                                                <asp:Label runat="server" BackColor="Red">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                                <asp:Label runat="server" Text="Non-Eligible- Beverage" Style="text-align: left;"></asp:Label>
                                                                                &emsp;&emsp;&emsp;
                                                                <asp:Label runat="server" BackColor="Yellow">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                                <asp:Label runat="server" Text="Non-Eligible Non-Beverage" Style="text-align: left;"></asp:Label>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>-
                                                            </div>-%>

                                                           <%-- <div class="card-header" runat="server" id="divSqlNote">
                                                                <div runat="server" class="col-sm-12">
                                                                    <div class="col-sm-12 ">
                                                                        <div class="col-sm-12 table table-bordered form-horizontal">

                                                                            <label style="color: red">Notes :- </label>
                                                                            <div>
                                                                                &emsp;&emsp;
                                                                <asp:Label runat="server" BackColor="DeepSkyBlue">&emsp;&emsp;&emsp;</asp:Label>&emsp;-
                                                                <asp:Label runat="server" Text="Above Eligibility Amount" Style="text-align: left;"></asp:Label>

                                                                            </div>


                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>--%>

                                                            <div class="card-body">
                                                                <%--style="width: 1200px; overflow-x: scroll;"--%>

                                                                <label style="font-size: 30px;">List Of Domestic Claim</label>
                                                                <%--<asp:GridView ID="gvDomClaim" runat="server" ToolTip="DOMESTIC CLAIM LIST" Style="width: 1150px;" OnRowDataBound="gvDomClaim_RowDataBound" AutoGenerateColumns="false" OnPreRender="gvDomClaim_PreRender" class="table table-bordered table-condensed">--%>
                                                                <asp:GridView ID="gvDomClaim" runat="server" AutoGenerateColumns="False"
                                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvDomClaim_RowDataBound"
                                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTEAT" runat="Server" />
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
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCODataId" runat="Server" Text="0" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sr.No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSrNo" runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim No">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDOMClmNoClmNo" CssClass="form-group" OnClick="lnkDOMClmNoClmNo_Click" Style="width: 100%;" TextMode="MultiLine" runat="Server" Text='<%# Eval("App_AId") %>'
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
                                                                                <asp:Label ID="lblEmpCodes" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Employee Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmpName" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EMP_FNAME") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim Date" HeaderStyle-Width="74.2px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Date") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Claim Type" HeaderStyle-Width="74.2px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTravelType" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Traveltype") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Travel From Date" HeaderStyle-Width="96.2px" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txFrDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("FromDate") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Travel To Date" HeaderStyle-Width="96.2px">
                                            <ItemTemplate>
                                                <asp:Label ID="txToDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ToDate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Travelled From" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtFrDest" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("TravelledFrom") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Travelled To" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtToDest" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("TravelledTo") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Claim Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmAmt" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Total_Expense") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approved Amount" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtAppAmtr" TextMode="MultiLine" Visible="false" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ApprovedAmmount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="EntertainmaentChecked" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="EntertainmentChked" TextMode="MultiLine" Visible="false" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("EntertainmentChked") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="STATUS" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtClmType" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("STATUS") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action Type" Visible="false">
                                                                            <ItemTemplate>
                                                                                <%--<asp:Label ID="lblSolID" runat="Server" Text='<%# Eval("SOL_ID") %>' SelectedValue='<%# Eval("ACTIONTYPE") %>'/>--%>
                                                                                <asp:DropDownList ID="drpAction" runat="server" CssClass="form-control input-sm-3" AutoPostBack="false" SelectedValue='<%# Eval("ChkAction") %>'>
                                                                                    <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                                                                    <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                                    <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>

                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRmk" TextMode="MultiLine" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("CheckerRemarks") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <%-- <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnView" Text="view" runat="server" OnClientClick="getStyle()" CssClass="btn btn-primary" />
                                            </ItemTemplate>

                                            <ItemStyle />
                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkBtnSubmit" Text="Submit" runat="server" OnClick="lnkBtnSubmit_Click" CssClass="btn btn-success" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
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

                                                                <%-- </div>
                        </div>--%>
                                                                <div>
                                                                    <br />
                                                                    <br />
                                                                </div>
                                                                &emsp;

                                <div class="row">
                                    <label id="lblActionAll">Action :-</label>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="drActionAll" runat="server" CssClass="form-control" AutoPostBack="false">
                                            <asp:ListItem Value="">[Select Action Type]</asp:ListItem>
                                            <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                            <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>


                                        </asp:DropDownList>
                                    </div>
                                    &nbsp;&nbsp;
                                     
                                    <label id="lblRemarksAll">Remarks :-</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAllRmk" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-1">
                                        <asp:Button ID="btnSubmitAll" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmitAll_Click" OnClientClick="showLoader();" />
                                    </div>

                                </div>
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
                                                            <h1>Domestic Expense Form</h1>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <ol class="breadcrumb float-sm-right">
                                                                <li class="breadcrumb-item"><a href="#">Home</a></li>

                                                                <li class="breadcrumb-item active">Domestic Expense Form</li>
                                                            </ol>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- /.container-fluid -->
                                            </section>
                                            <section class="content">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-body">
                                                            <%--<label>Category I</label>--%>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label>Type</label>
                                                                        <asp:DropDownList ID="drpType" runat="server" Enabled="false" CssClass="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem Value="">[Select Type]</asp:ListItem>
                                                                            <asp:ListItem Value="Travel" Text="Travel"></asp:ListItem>
                                                                            <asp:ListItem Value="Entertainment" Text="Entertainment"></asp:ListItem>
                                                                            <asp:ListItem Value="Travel + Entertainment" Text="Travel + Entertainment"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <!-- /.form-group -->
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <br />
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="Label13" runat="server" class="col-sm-3 col-form-label"> Expense Description :-</asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox class="form-control" ReadOnly="true" ID="txtdiscript" runat="server" Style="width: 700px; height: 50px;"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                   
                                                                    <div>
                                                                        <asp:Label ID="lblClassTravel" runat="server" Visible="false"><b>Class Of Travels</b></asp:Label>
                                                                    </div>
                                                                    <br />

                                                                    <div id="" class="form-horizontal">

                                                                        <fieldset>

                                                                            <div class="form-group">
                                                                                <div class="col-md-3">
                                                                                    <asp:CheckBox ID="chk1" ReadOnly="true" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Air (Business Class)" />
                                                                                </div>
                                                                                <div class="col-sm-3" style="margin-top: 0px;">
                                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtchk1" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                                </div>

                                                                                &emsp;&emsp;
                                                                            <div class="col-md-3">
                                                                                <asp:CheckBox ID="chk2" ReadOnly="true" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Air (Economy Class)" />
                                                                            </div>
                                                                                <div class="col-sm-3" style="margin-top: -17px;">
                                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtchk2" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                                </div>


                                                                            </div>

                                                                            <div class="form-group">
                                                                                <div class="col-md-3">
                                                                                    <asp:CheckBox ID="chk3" ReadOnly="true" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Rail (AC 1st Class)" />
                                                                                </div>
                                                                                <div class="col-sm-3" style="margin-top: 0px;">
                                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtchk3" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                &emsp;&emsp;
                                                                                <div class="col-md-3">
                                                                                    <asp:CheckBox ID="chk4" ReadOnly="true" class="icheck-primary d-inline col-sm-2" runat="server" AutoPostBack="true" Text="Rail (AC 2nd Class)" />
                                                                                </div>
                                                                                <div class="col-sm-3" style="margin-top: 0px;">
                                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtchk4" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>

                                                                                </div>

                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="col-md-3">
                                                                                    <asp:CheckBox ID="chk5" ReadOnly="true" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Rail (AC 3rd Class)" />
                                                                                </div>
                                                                                <div class="col-sm-3" style="margin-top: 0px;">
                                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtchk5" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                &emsp;&emsp;
                                                                                   
                                                                                <div class="col-md-3">
                                                                                    <asp:CheckBox ID="chk6" ReadOnly="true" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Rail (AC Chair Car)" />
                                                                                </div>
                                                                                <div class="col-sm-3" style="margin-top: 0px;">
                                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtchk6" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                                </div>

                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="col-md-3">
                                                                                    <asp:CheckBox ID="chk7" ReadOnly="true" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" Text="Bus" />
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtchk7" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                &emsp;&emsp;
                                                                                  
                                                                                <div class="col-md-3">
                                                                                    <asp:CheckBox ID="chk8" ReadOnly="true" class="icheck-primary d-inline col-sm-2" runat="server" AutoPostBack="true" Text="Others" />
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtchk8" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>

                                                                                </div>

                                                                            </div>
                                                                        </fieldset>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <%--span2 datepicker-dropdown menu-open--%>
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label7" runat="server" class="col-sm-3 col-form-label">From Date :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtFromDate" runat="server"></asp:TextBox>
                                                                    <%--<div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                        </div>--%>
                                                                </div>
                                                                <asp:Label ID="Label9" runat="server" class="col-sm-3 col-form-label">To Date :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox CssClass="form-control" ReadOnly="true" ID="txtToDate" runat="server"></asp:TextBox>
                                                                    <%--<div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                        </div>--%>
                                                                </div>



                                                            </div>

                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label4" runat="server" class="col-sm-3 col-form-label">Travel Source :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtStartDest" runat="server"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label6" runat="server" class="col-sm-3 col-form-label">Travel Destination :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" ReadOnly="true" ID="txtEndDest" runat="server"></asp:TextBox>
                                                                </div>

                                                                <br />

                                                            </div>


                                                        </div>
                                                    </div>
                                                </div>

                                            </section>
                                            <section class="content" id="divTravel" runat="server" visible="false">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-header">
                                                            <h6 class="card-title"><b>Daily Reimbursement Amount in Rs. (Per day)</b></h6>
                                                            <br />
                                                            <!-- iCheck -->

                                                            <div class="card-body">
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <!-- radio -->
                                                                        <div class="form-group clearfix">
                                                                            <div class="icheck-primary d-inline">
                                                                                <asp:RadioButton ID="radioPrimary1" Enabled="false" Text="Metro Cities" runat="server" GroupName="GroupType"></asp:RadioButton>


                                                                                <label for="radioPrimary1">
                                                                                </label>
                                                                            </div>
                                                                            <div class="icheck-primary d-inline">
                                                                                <asp:RadioButton ID="radioPrimary2" Enabled="false" Text="Non Metro Cities" runat="server" GroupName="GroupType"></asp:RadioButton>

                                                                                <label for="radioPrimary2">
                                                                                </label>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" id="divMetro" runat="server" visible="false">
                                                                    <div class="col-sm-12">
                                                                        <label>
                                                                            Metro Cities :- (Mumbai, Chennai, Delhi, Kolkata, Hyderabad and Bangalore)
                                                                        </label>

                                                                        <asp:GridView ID="grMetro" runat="server" ToolTip="Metro Cities Daily Reimbursement" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Lodging">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLodging" runat="Server" Text='<%# Eval("LODGING_AMT") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Boarding">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBoarding" runat="Server" Text='<%# Eval("BOARDING_AMT") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Conveyance">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="txtBoarding" CssClass="form-group" runat="Server" Text='<%# Eval("CONVEYANCE") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Miscellaneous">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMiscellaneous" CssClass="form-group" runat="Server" Text='<%# Eval("MISCELLANEOUS") %>' />
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

                                                                <div class="row" id="divNonMetro" runat="server" visible="false">
                                                                    <div class="col-sm-12">
                                                                        <label for="radioPrimary1">
                                                                            Non Metro Cities :- Other than (Mumbai, Chennai, Delhi, Kolkata, Hyderabad and Bangalore)

                                                                        </label>

                                                                        <asp:GridView ID="grNonMetro" runat="server" ToolTip="Non Metro Cities Daily Reimbursement" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="Lodging">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNonLodging" runat="Server" Text='<%# Eval("LODGING_AMT") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Boarding">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNonBoarding" runat="Server" Text='<%# Eval("BOARDING_AMT") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Conveyance">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="txtBoarding" CssClass="form-group" runat="Server" Text='<%# Eval("CONVEYANCE") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Miscellaneous">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNonMiscellaneous" CssClass="form-group" runat="Server" Text='<%# Eval("MISCELLANEOUS") %>' />
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
                                                                <div class="form-group row" id="divAmt" runat="server" visible="false">
                                                                    <asp:Label ID="Label2" runat="server" class="col-sm-3 col-form-label">No of Days :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" ID="txtNoDays" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label ID="Label3" runat="server" class="col-sm-3 col-form-label">Eligibility Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" ID="txtligibility" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <br />

                                                                </div>

                                                                <div class="form-group row" id="divEligi" runat="server" visible="false">


                                                                    <asp:Label ID="Label10" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" ID="txtTravelAmt" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label ID="Label1" runat="server" class="col-sm-3 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" TextMode="MultiLine" ID="txtUserTravelRemarks" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <br />

                                                                </div>
                                                                <div class="form-group row" id="div1" runat="server" visible="false">


                                                                    <asp:Label ID="Label17" runat="server" class="col-sm-3 col-form-label">Advance Paid :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" ID="txtadvance" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <%-- <asp:Label ID="Label18" runat="server" class="col-sm-3 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ReadOnly="true" TextMode="MultiLine" ID="TextBox2" runat="server"></asp:TextBox>
                                                                    </div>--%>

                                                                    <br />

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
                                            <asp:GridView ID="gvDomesticFile" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
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
                                                                AutoPostback="true" OnClick="lnkBtnOpenFile_Click" />
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

                                                                <%--<div class="row" id="divFile" runat="server" visible="false">
                                    <div class="col-lg-6">
                                        Supporting files:
                                    </div>
                                </div>--%>
                                                            </div>
                                                            <%-- </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                            <section class="content" id="divEntertainment" runat="server" visible="false">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <div class="card-header">
                                                            <h6 class="card-title"><b>Entertainment</b></h6>

                                                            <%--<div class="card-tools">
                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                             <button type="button" class="btn btn-tool" data-card-widget="remove">
                                <i class="fas fa-times"></i>
                            </button>
                        </div>--%>
                                                            <br />
                                                            <!-- iCheck -->
                                                            <%--<div class="card card-success">--%>
                                                            <div class="card-body">

                                                                <div class="form-group row">

                                                                    <asp:Label ID="Label11" runat="server" class="col-sm-3 col-form-label">Entertainment Description :-</asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtEnterDesc" Enabled="false" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">


                                                                    <asp:Label ID="Label5" runat="server" class="col-sm-3 col-form-label">Claim Amount :-</asp:Label>
                                                                    <div class="col-sm-2">
                                                                        <asp:TextBox class="form-control" ID="txtEntAmount" Enabled="false" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <%--  <asp:Label ID="Label11" runat="server" class="col-sm-2 col-form-label">Entertainment Description :-</asp:Label>
                                    <div class="col-sm-2">
                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtEnterDesc" runat="server"></asp:TextBox>
                                    </div>--%>
                                                                    <asp:Label ID="Label8" runat="server" class="col-sm-2 col-form-label">Remarks :-</asp:Label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox class="form-control" TextMode="MultiLine" Enabled="false" ID="txtUserEligiRemark" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <br />

                                                                </div>
                                                                <div class="row" id="divUploadEnter" runat="server" visible="false">
                                                                    <div class="col-lg-6" id="divfileUploadEnter" runat="server" visible="false">
                                                                        <div class="btn-group w-100">
                                                                            <asp:FileUpload ID="fupldDocumentEnter" runat="server" CssClass="btn btn-success col fileinput-button fas fa-plus" />

                                                                            <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col start fas fa-upload" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-6" id="divfileDisplayEnter" runat="server" visible="false">
                                                                        Supporting files:
                                            <asp:GridView ID="gvEntertainment" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                class="table table-bordered table-striped" DataKeyNames="FILENAME">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileStorageEnter" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenFileEnter" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                AutoPostback="true" OnClick="lnkBtnOpenFileEnter_Click" />
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

                                                                <br />
                                                                <br />

                                                            </div>
                                                            <%-- </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>

                                            <section class="content">
                                                <div class="container-fluid">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="card card-default">
                                                        <br />

                                                        <div class="form-group row" id="eligibility" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="Label14" runat="server" class="col-sm-4 col-form-label"><b>Eligibility Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="elgAmount" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row" id="claimAmt" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="Label15" runat="server" class="col-sm-4 col-form-label"><b>Claim Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="claimAmount" AutoCompleteType="Disabled" Enabled="false" AutoPostBack="true" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row" id="Divadv" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="Label16" runat="server" class="col-sm-4 col-form-label"><b>Advance Paid Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="txtadvamt" AutoCompleteType="Disabled" Enabled="false" AutoPostBack="true" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row" id="TotalExpns" runat="server">
                                                            &emsp;&emsp;
                                            <asp:Label ID="lblTtlexp" runat="server" class="col-sm-4 col-form-label"><b>Total Expense (Travel fare + Claim Amount - Advance Amount) :-</b></asp:Label>
                                                            <%-- <asp:Button ID="btnCalTtl" CssClass="btn btn-primary" OnClick="btnCalTtl_Click" runat="server" Text="Calculate Total Expense" />--%>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox class="form-control" ID="txtTotalexp" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            &emsp;&emsp;<asp:Label ID="Label12" runat="server" class="col-sm-4 col-form-label"><b>Approved Amount :-</b></asp:Label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox class="form-control" ID="txtApprovedAmt" runat="server" AutoCompleteType="Disabled" placeholder="Enter Amount"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtApprovedAmt"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>
                                                        </div>

                                                        <div class="card-body">
                                                            <div class="form-group row">
                                                                <%--<div class="col-sm-2"> Entertainment
                                   <asp:CheckBox ID="chkAlchol" style="font-size:14px;" runat="server" />
                                </div>--%>
                                                                <div class="form-group clearfix">
                                                                    <div class="icheck-primary d-inline">
                                                                        <%--<asp:RadioButton ID="radiaoEntertainment1" Text="Entertainment" OnCheckedChanged="radiaoEntertainment_CheckedChanged" AutoPostBack="true" runat="server"></asp:RadioButton>--%>
                                                                        <asp:CheckBox ID="radiaoEntertainment1" runat="server" Text="Non-Eligible" />


                                                                        <label for="RadioButton1">
                                                                        </label>
                                                                    </div>
                                                                </div>

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
                                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" OnClick="btnClose_Click" runat="server" Text="Cancel" OnClientClick="showLoader();" />
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
                                    <asp:PostBackTrigger ControlID="gvDomClaim" />
                                    <asp:PostBackTrigger ControlID="gvEntertainment" />
                                    <asp:PostBackTrigger ControlID="gvDomesticFile" />
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
                0 !== $('#<%= gvDomClaim.ClientID %>').length &&
                    $('#<%= gvDomClaim.ClientID %>').DataTable({
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
                $('#<%= gvDomClaim.ClientID %>').dataTable();
            });
            TableManageButtons.init();
        </script>
    </section>
</asp:Content>


