<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="ApprovedORRejectOTAndCOReport.aspx.cs" Inherits="NewPortal2023.ESS.ApprovedORRejectOTAndCOReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function ValidFile() {
            var file = $("#fupldDocument").val();
            if (file == "") {
                IN4_DisplayErrorMessage("Browse document.");
                return false;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ToggleVisible() {
            var trdate = document.getElementById("trdate");
            var trtime = document.getElementById("trtime");
            if (trdate.style.display == 'block') {
                trdate.style.display = 'none';
                trtime.style.display = 'none';
            }
            else {
                trdate.style.display = 'block';
                trtime.style.display = 'block';
            }
        }
    </script>

    <style type="text/css">
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
    </style>

    <%--     <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/litepicker.js" type="text/javascript"></script>--%>

    <%--    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />--%>

    <script type="text/javascript">
        function ConfirmDel() {
            return confirm("Are you sure you want to delete this record?");
        }

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Select2);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({
                    format: 'dd-mm-yyyy',
                    autoclose: true
                });

                $(".datetimepicker").datetimepicker({
                    format: 'dd-mm-yyyy hh:ii',
                    autoclose: true
                });
            }

            $('.datepicker').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true
            });

            $(".datetimepicker").datetimepicker({
                format: 'yyyy-mm-dd hh:ii',
                autoclose: true
            });

            function Select2(sender, args) {
                $(".select2").select2();
            }

            $(".select2").select2();
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
                            <h3 style="color: white">OT AND CO REPORT</h3>
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

                                        <div id="trSubmit1" runat="server" class="form-group">
                                            <div class="col-lg-12">
                                                <div id="trViewListByOTAndCOWise" runat="server" visible="true" class="form-group">
                                                <label class="col-sm-2 labels">Search By :</label>
                                                <asp:DropDownList ID="drpReportsType" runat="server" Visible="true" OnSelectedIndexChanged="drpReportsType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm" Width="150px">
                                                    <asp:ListItem Value=" ">All</asp:ListItem>
                                                    <asp:ListItem Value="EmployeeWise" Text="EmployeeWise"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div id="tdEmpCode" runat="server" visible="false" class="form-group">
                                                <label id="lblEmpCode1" runat="server" class="col-sm-2 labels">Employee Code :</label>
                                                <asp:TextBox ID="txtEmpCode" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm" Width="150px"></asp:TextBox>
                                            </div>
                                            </div>
                                            

                                            <div class="form-group">
                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 labels">From Date<span style="color: Red;">*</span> :</label>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm datepicker" placeholder="Date"></asp:TextBox>
                                                        </div>

                                                        <label class="col-sm-3 labels">To Date<span style="color: Red;">*</span> :</label>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtDateTo" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm datepicker" placeholder="Date"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                    <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success" Text="Generate OT and CO Report"
                                                        OnClick="btnGenerate_Click" OnClientClick="showLoader();" />
                                                </div>

                                                <div class="col-sm-12" style="text-align: center; margin-top: 20px">
                                                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary" Autopostback="true" Text="Export" OnClick="btnExport_Click" />
                                                </div>
                                            </div>
                                        </div>

                                        &emsp;

                                        <div id="trOTCO" runat="server" visible="false" class="form-group">
                                            <label class="col-sm-2 labels">Search By OT/CO :</label>
                                            <asp:DropDownList ID="drpOtCOType" runat="server" CssClass="form-control input-sm" Width="150px" OnSelectedIndexChanged="drpOtCOType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value=" ">[Search By]</asp:ListItem>
                                                <asp:ListItem Value="OT" Text="OT"></asp:ListItem>
                                                <asp:ListItem Value="CO" Text="CO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="drpSelectType" runat="server" Visible="false" CssClass="form-control input-sm" Width="150px" OnSelectedIndexChanged="drpSelectType_SelectedIndexChanged" AutoPostBack="false">
                                                <asp:ListItem Value=" ">[Select Type]</asp:ListItem>
                                                <asp:ListItem Value="All" Text="All"></asp:ListItem>
                                                <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
                                                <asp:ListItem Value="Rejected" Text="Rejected"></asp:ListItem>
                                                <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div id="trList" runat="server" visible="true" class="form-group">
                                            <%--<asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="Time Sheet">--%>
                                            <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Employee Code">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCode" runat="Server" Text='<%# Eval("EMP_CODE") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("Date") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShiftSchedule" runat="Server" Text='<%# Eval("ACTTYP") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OT">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOT" runat="Server" Text='<%# Eval("OT") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CO">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCO" runat="Server" Text='<%# Eval("CO") %>' />

                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                        <tr>
                                                            <td class="GridViewHeader" style="width: 10%">
                                                                <asp:Literal ID="Literal6" runat="server" Text="No Records Founds." />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnGenerate" />
                                    <asp:PostBackTrigger ControlID="btnExport" />
                                    <asp:PostBackTrigger ControlID="drpOtCOType" />
                                    <asp:PostBackTrigger ControlID="drpReportsType" />
                       <%--             <asp:PostBackTrigger ControlID="chk5" />
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
