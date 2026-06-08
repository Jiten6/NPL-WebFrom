<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="LocalSupporting.aspx.cs" Inherits="NewPortal2023.ESS.LocalSupporting" %>
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
                   <div class="col-sm-12">
                        <section class="panel">  
                             <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Local Supporting</h3>
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

                                                  <div class="card-body">

                                                        <label style="font-size: 30px;">List Of Local Supporting Claim</label>


                                                      <asp:GridView ID="gvLocalClaimList" runat="server" AutoGenerateColumns="False"
                                                                HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvLocalClaimList_RowDataBound"
                                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC" OnPreRender="gvLocalClaimList_PreRender"
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">

                                                      <Columns>

                                                                    <asp:TemplateField HeaderText="Sr.No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSrNo" runat="Server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Claim No">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkLOCClmNoClmNo" CssClass="form-group" OnClick="lnkLOCClmNoClmNo_Click" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_no") %>'   />
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

                                                                    <asp:TemplateField HeaderText="Claim Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtClmDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Date","{0:dd/MM/yyyy}") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Expenses Date" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txFrDate" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Expenses_Date","{0:dd/MM/yyyy}") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Claim Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtClmAmt" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Claim_Amount") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Approved Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtLOCApprAmt" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("ClaimApproved_Amount") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name of Business/Assignment">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtFrDest" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Name_Bussi_Ass") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Travelled Description">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtToDest" TextMode="MultiLine" ReadOnly="true" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("Travel_Description") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="STATUS">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtLOCSTS" CssClass="form-group" Style="width: 100%;" runat="Server" Text='<%# Eval("STATUS") %>' />
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


                                                </div>
                                               

                                                </section>

                                              <div id="divFrom" runat="server" visible="false">
                                            <section class="content-header">
                                                <div class="container-fluid">
                                                    <div class="row mb-2">
                                                        <div class="col-sm-6">
                                                            <h1>Local Expense Form</h1>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <ol class="breadcrumb float-sm-right">
                                                                <li class="breadcrumb-item"><a href="#">Home</a></li>

                                                                <li class="breadcrumb-item active">Local Expense Form</li>
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
                                                            <h2 class="card-title" style="font-size: 18px"><b>Travel Type</b></h2>
                                                            <br />
                                                            <br />
                                                            <div class="form-group row">
                                                                <div class="col-sm-12">
                                                                    <label>Local Conveyance</label>
                                                                </div>
                                                                <br />
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="grLocalReimb" runat="server" ToolTip="Local Reimbursement" AutoGenerateColumns="False" class="table table-bordered table-striped">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Mode">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMode" runat="Server" Text='Mode Of Travel' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type Of Travel">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTypeOfTravel" runat="Server" Text='<%# Eval("REIMBURSEMENT_TYPE") %>' />
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

                                                            <!-- checkbox -->
                                                        
                                                           
                                                            
                                                          <%--  <div class="form-group row">

                                                                <br />
                                                                <br />
                                                               
                                                                <asp:CheckBox ID="chk1" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="chk1_CheckedChanged" Text="Self/Company Car" />
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtchk1" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                </div>
                                                                &emsp;&emsp;
                                <asp:CheckBox ID="chk2" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="chk2_CheckedChanged" Text="Train" />
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtchk2" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                </div>

                                                            
                                                                </div>

                                                            <div class="form-group row">
                                                             
                                                                <asp:CheckBox ID="chk3" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="chk3_CheckedChanged" Text="Taxi/Auto" />
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtchk3" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                </div>
                                                                &emsp;&emsp;
                                <asp:CheckBox ID="chk4" Enabled="false" class="icheck-primary d-inline col-sm-2" runat="server" AutoPostBack="true" OnCheckedChanged="chk4_CheckedChanged" Text="Bus" />
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtchk4" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>

                                                                </div>
                                                                
                                                            </div>--%>


                                                     <%--       <kapil>--%>
                                                      
                                                  <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div id="" class="form-horizontal">

                                                                        <fieldset>

                                                                            <br />
                                                                            <br />
                                                                            <div class="form-group">
                                                                                <div class="col-md-3">
                                                                                    <asp:CheckBox ID="chk1" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="chk1_CheckedChanged" Text="Self/Company Car" />
                                                                                </div>
                                                                                <div class="col-sm-3" style="margin-top: 0px;">
                                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtchk1" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                &emsp;&emsp;
                                                                            <div class="col-md-3">
                                                                                <asp:CheckBox ID="chk2" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="chk2_CheckedChanged" Text="Train" />
                                                                            </div>
                                                                                <div class="col-sm-3" style="margin-top: 0px;">
                                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtchk2" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                                </div>

                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="col-md-3">
                                                                                    <asp:CheckBox ID="chk3" Enabled="false" class="icheck-primary d-inline col-sm-2" AutoPostBack="true" runat="server" OnCheckedChanged="chk3_CheckedChanged" Text="Taxi/Auto" />
                                                                                </div>
                                                                                <div class="col-sm-3" style="margin-top: 0px;">
                                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtchk3" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                &emsp;&emsp;
                                                                         <div class="col-md-3">
                                                                             <asp:CheckBox ID="chk4" Enabled="false" class="icheck-primary d-inline col-sm-2" runat="server" AutoPostBack="true" OnCheckedChanged="chk4_CheckedChanged" Text="Bus" />
                                                                         </div>
                                                                                <div class="col-sm-3" style="margin-top: 0px;">
                                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtchk4" placeholder="Amount" Visible="false" runat="server"></asp:TextBox>

                                                                                </div>

                                                                            </div>
                                                                        </fieldset>
                                                                    </div>
                                                                </div>
                                                            </div>
       

                                                         <%--   </kapil>--%>
                                                           

                                                        
                                                              

                                                            <%--span2 datepicker-dropdown menu-open--%>
                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="lblDate" runat="server" class="col-sm-3 col-form-label">Date of Expenses :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtDate" runat="server"></asp:TextBox>
                                                                    <%--<div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                        </div>--%>
                                                                </div>
                                                                &emsp;&emsp;
                                <asp:Label ID="lblNameAss" runat="server" class="col-sm-3 col-form-label">Name of Business / Assignment :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="txtNameAss" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="lblCashVocher" runat="server" class="col-sm-3 col-form-label">Cash Voucher, if any, attached No.:-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtCashVocher" runat="server"></asp:TextBox>
                                                                </div>
                                                                &emsp;&emsp;
                                <asp:Label ID="lblTravelDes" runat="server" class="col-sm-3 col-form-label">Travel Description :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" TextMode="MultiLine" ID="txtTravelDes" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="form-group row">
                                                                <asp:Label ID="lblMeal" runat="server" class="col-sm-3 col-form-label">Meal :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtMeal" runat="server"></asp:TextBox>
                                                                </div>
                                                                &emsp;&emsp;
                                <asp:Label ID="lblOtherExpenses" runat="server" class="col-sm-3 col-form-label">Other Expenses :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtOtherExpenses" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <br />
                                                             <div class="form-group row">
                                                                <asp:Label ID="lbladv" runat="server" class="col-sm-3 col-form-label">Advance Paid :-</asp:Label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox class="form-control" Enabled="false" ID="txtadv" runat="server"></asp:TextBox>
                                                                </div>
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
                                                            <h2 class="card-title" style="font-size: 18px"><b>Upload Supporting Documents</b></h2>
                                                            <br />
                                                            <!-- iCheck -->

                                                            <div class="card-body">
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
                                                                AutoPostback="true" OnClick="lnkBtnOpenFiles_Click" />
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
                                                        <br />
                                                        <div class="form-group row">
                                                            &emsp;<asp:Label ID="lblTtlexp" runat="server" class="col-sm-3 col-form-label"><b>Total Expense :-</b></asp:Label>
                                                            <asp:Button ID="btnCalTtl" Visible="false" CssClass="btn btn-primary" runat="server" Text="Calculate Total Expense" />
                                                            <div class="col-sm-4">
                                                                <asp:TextBox class="form-control" ID="txtTotalexp" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                         <div class="form-group row">
                                                            &emsp;<asp:Label ID="lblpaidamt" runat="server" class="col-sm-3 col-form-label"><b>Paid Amount :-</b></asp:Label>
                                                            <asp:Button ID="Button1" Visible="false" CssClass="btn btn-primary" runat="server" Text="Calculate Total Expense" />
                                                            <div class="col-sm-4">
                                                                <asp:TextBox class="form-control" ID="txtpaidamt" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="form-group row">
                                                            &emsp;
                                            <asp:Label ID="Label12" runat="server" class="col-sm-3 col-form-label"><b>Approved Amount :-</b></asp:Label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox class="form-control" ID="txtApprovedAmt" Enabled="false" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtApprovedAmt"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Please enter a valid numeric value."
                                                                    Display="Dynamic" ForeColor="Red" />
                                                            </div>

                                                            <br />

                                                        </div>


                                                          <div class="card-body">
                                                                <div class="form-group row">
                                                                <div class="form-group clearfix">
                                                                    <div class="icheck-primary d-inline">
                                                                         <%--<asp:RadioButton ID="radiaoEntertainment1" Text="Entertainment" OnCheckedChanged="radiaoEntertainment_CheckedChanged" AutoPostBack="true" runat="server"></asp:RadioButton>--%>
                                                                        <asp:CheckBox ID="radiaoEntertainment1" runat="server" Text="Non-Eligible" />


                                                                        <label for="RadioButton1">
                                                                        </label>
                                                                        </div>
                                                                    </div>
                                                                    </div>
                                                              </div>
                                                     


                                                        <div class="card-body">
                                                            <div class="col-sm-12" style="text-align: center;">
                                                                <%--<asp:Button ID="btnClose" CssClass="btn btn-danger" OnClick="btnClose_Click" runat="server" Text="Close" OnClientClick="showLoader();" />--%>
                                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" OnClick="btnClose_Click" runat="server" Text="Close" />
                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>



                                                </section>

                                                  </div>



                                           </div>
                                      </ContentTemplate>
                                 <Triggers>
                                    <asp:PostBackTrigger ControlID="gvDomesticFile" />
                                </Triggers>

                              </asp:UpdatePanel>


                                   </div>
                    </section>
                       </div>
               </section>
          </section>

</asp:Content>
