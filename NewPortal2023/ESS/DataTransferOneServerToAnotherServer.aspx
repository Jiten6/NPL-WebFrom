<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="DataTransferOneServerToAnotherServer.aspx.cs" Inherits="NewPortal2023.ESS.DataTransferOneServerToAnotherServer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <script type="text/javascript">
        $(function () {
            $('[id*=txtFromDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
        $(function () {
            $('[id*=txtToDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
    </script>--%>
    <script type="text/javascript">
        $(function () {
            $('[id*=txtFromDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
        $(function () {
            $('[id*=txtToDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
    </script>
    <script>
        function showAlert() {
            var textboxValue = document.getElementById('txtTravelAmt').value;
            alert("Text changed to: " + textboxValue);
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
                            <h3 style="color: white">Local Travel</h3>
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

                                    <div id="form1" runat="server">
                                        <div id="divAlert" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                        </div>

                                        <section id="SectionList" class="content-header" runat="server">

                                            <div class="container-fluid">
                                                <!-- SELECT2 EXAMPLE -->
                                                <div class="card card-default">

                                                    <div class="card-body">



                                                        <br />

                                                        <div class="row">
                                                            <asp:Button ID="btnGetData" OnClick="btnTransfer_Click" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Get data" />
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <label style="font-size: 30px;">List Of Local Claim</label>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-12">


                                                                <asp:GridView ID="gvLocalClaimList" runat="server" AutoGenerateColumns="False"
                                                                    HorizontalAlign="Left" CellPadding="5" GridLines="None" Width="100%"
                                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                    BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                    <Columns>
                                                                     
                                                                        <asp:TemplateField HeaderText="Employee Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmployeeCode" runat="Server" Text='<%# Eval("EmployeeCode") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    
                                                                        <asp:TemplateField HeaderText="Log DateTime">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLogDateTime" runat="Server" Text='<%# Eval("LogDateTime", "{0:dd/MM/yyyy HH:mm:ss}") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                     
                                                                        <asp:TemplateField HeaderText="Log Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLogDate" runat="Server" Text='<%# Eval("LogDate", "{0:dd/MM/yyyy}") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Log Time">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLogTime" runat="Server" Text='<%# Eval("LogTime", "{0:HH:mm:ss}") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Direction">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDirection" runat="Server" Text='<%# Eval("Direction") %>' />
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

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </section>





                                    </div>

                                </ContentTemplate>
                                <Triggers>

                                    <asp:PostBackTrigger ControlID="btnGetData" />

                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>
            </div>
        </section>



    </section>

</asp:Content>
