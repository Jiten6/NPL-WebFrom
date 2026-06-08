<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="MisellaneousSupporting.aspx.cs" Inherits="NewPortal2023.ESS.MisellaneousSupporting" %>
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
                            <h3 style="color: white">Miscellaneous Supporting</h3>
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
                                                        <label style="font-size: 30px;">List Of Miscellaneous Supporting Claim</label>    
                                                          <div style="overflow-x: scroll; width: 100%;">
                                                      <asp:GridView ID="gvList" Visible="false" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Size="Medium" HeaderStyle-BackColor="Orange"
                                                            HorizontalAlign="Left" ToolTip="Time Sheet" CellPadding="5"
                                                            GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-striped">
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAppNo" runat="Server" Text='<%# Eval("EXPENSES_NO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               
                                                                <asp:TemplateField HeaderText="Voucher No" ControlStyle-BorderColor="Orange">
                                                                    <ItemStyle CssClass="GridViewItem" Width="120px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkVoucherNo" Class="underline" OnClick="lnkVoucherno_Click" runat="Server" Text='<%# Eval("EXPENSES_NO") %>' />
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Employee Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpNAme" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Employee ID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpMid" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Voucher Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVoucherDate" runat="Server" Text='<%# Eval("VOUCHER_DATE") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Voucher Amount">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" runat="Server" Text='<%# Eval("AMOUNT") %>' />
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Apporved Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApprovedAmount" runat="Server" Text='<%# Eval("Approved_Amount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Validation Team Remark">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtChkRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("CHKRMK") %>'></asp:TextBox>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="HOD Remark">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="tctHodRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("HODRMK") %>'></asp:TextBox>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="HR Remark">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtHrRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("HRRMK") %>'></asp:TextBox>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Finance Remark">
                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtFinRmk" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("FINRMK") %>'></asp:TextBox>
                                                                    </ItemTemplate>


                                                                </asp:TemplateField>


                                                            </Columns>
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

                                                    

                                                 </section>
    
                                                    <div id="extradata" visible="false" runat="server">
                                                    <br />
                                                    <br />
                                                   
                                                  
                                                    <asp:GridView ID="gvallList" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Size="Medium" HeaderStyle-BackColor="SkyBlue"
                                                        HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvallList_RowDataBound"
                                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEntryAid" runat="Server" Text='<%# Eval("ENTRY_AID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Expense type">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExpenseType" runat="Server" Text='<%# Eval("EXPENSE_TYPE") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Voucher Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVoucherDate" runat="Server" Text='<%# Eval("DATE") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="On behalf of" Visible="true">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOnbehalfof" runat="Server" Text='<%# Eval("ON_BEHALF_OF") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Particulars">

                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lblNatureofexpense" TextMode="MultiLine" Style="border: 0px;" ReadOnly="true" runat="Server" Text='<%# Eval("NATURE_OF_EXPENSE") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bill no">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBillno" runat="Server" Text='<%# Eval("BILL_NUMBER") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Voucher Amount">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="Server" Text='<%# Eval("AMOUNT") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Supporting File">

                                                                <ItemTemplate>

                                                                    <asp:FileUpload ID="fupUpload" runat="server" Visible="false" />
                                                                    <asp:LinkButton ID="lnkShowDoc" runat="server" Visible="false" ReadOnly="true" OnClick="lnkShowDoc_Click1" Style="font-size: 14px; color: blue;" CssClass="underline">Download</asp:LinkButton>
                                                                    <asp:Label ID="lblDocAddr" runat="server" Text="" Visible="false"></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                           
                                                           
                                                        </Columns>
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
                                                    <div class="row">
                                                        <div style="margin-left: 49%; font-size: 14PX; color: black; font-weight: 500;">
                                                            Total Amount :-
                                                            <asp:TextBox ID="txtTotalAmt" runat="server" Style="border: solid; margin-left: 25%; margin-top: -19%; width: 100px;" ReadOnly="true" CssClass="form-control input-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div style="margin-left: 49%; font-size: 14PX; color: black; font-weight: 500;">
                                                            Approved Amount :-
                                                            <asp:TextBox ID="txtApprovedAmt" runat="server" Style="border: solid; margin-left: 25%; margin-top: -4%; width: 100px;" CssClass="form-control input-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div style="font-size: 14PX; color: black; font-weight: 500;">
                                                                Employee Remarks :-
                                                                    <asp:TextBox ID="txtEmpRmk" runat="server" ReadOnly="true" TextMode="MultiLine" Style="border: solid; width: 400px; margin-top: 5%;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row" id="divfileDisplayEnter" runat="server" visible="true">
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
                                                                OnClick="lnkBtnOpenFileEnter_Click" AutoPostback="true" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                                      <div class="card-body">
                                                            <div class="col-sm-12" style="text-align: center;">
                                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" OnClick="btnClose_Click" runat="server" Text="Close" visible="false"/>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br /> 

                                             </div>

                                            </div>
                                            </div>
                                        </div>
                                    </div>
                           












                                           </div>
                                      </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="gvEntertainment" />
                                     <asp:PostBackTrigger ControlID="gvallList" />
                                </Triggers>

                              </asp:UpdatePanel>


                                   </div>
                    </section>
                       </div>
               </section>
          </section>
   


</asp:Content>
