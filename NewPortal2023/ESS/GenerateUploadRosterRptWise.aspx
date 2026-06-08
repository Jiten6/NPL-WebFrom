<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="GenerateUploadRosterRptWise.aspx.cs" Inherits="NewPortal2023.ESS.GenerateUploadRosterRptWise" %>

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
    <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/litepicker.js" type="text/javascript"></script>

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
            background-color: #eeeeee;
        }

        .report {
            font-size: 11px;
        }

        .table4 TR {
            padding-right: 1px;
            padding-left: 1px;
            padding-bottom: 1px;
            padding-top: 1px;
            background-color: #fdfbf0;
        }

        .tableinnerColhead TD {
            filter: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#FFFFFF,endColorStr=#C1CDDD);
            vertical-align: middle;
            height: 20px;
            text-align: center;
        }

        .table5 {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
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

        .style1 {
            width: 5%;
        }
    </style>
    <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/litepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ConfirmDel() {
            return confirm("Are you sure you want to delete this record?");
        }

        //        function ValidateMaintenance(drp) {
        //            var val = drp.value;
        //            if (val == "259") {
        //                return true;
        //            }
        //            else {
        //                return false;
        //            }
        //        }

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
                            <h3 style="color: white">Roster Report</h3>
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
                                                <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success" Text="Generate Report" OnClick="btnGenerate_Click" />
                                            </div>

                                            <div class="col-sm-12" style="text-align: center; margin-top: 20px">
                                                <%--<img id="loader1" style="display: none; height: 50px; width: 25px; float: right;" src="Assets/progress.gif" />--%>
                                                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary" Text="Export" OnClick="btnExport_Click" />
                                            </div>
                                        </div>

                                        &emsp;

                                        <div class="form-group">
                                            <%--<asp:GridView ID="gvRoster" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                CssClass="table4" HorizontalAlign="Left" ToolTip="Shift Schedule">--%>
                                            <asp:GridView ID="gvRoster" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Employee Name" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Code" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("EMP_CODE") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIn" runat="Server" Text='<%# Eval("Date") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Day">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDays" runat="Server" Text='<%# Eval("Days") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift Schedule">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOut" runat="Server" Text='<%# Eval("Shift_Schedule") %>' />

                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <%--           <asp:TemplateField HeaderText="In Time">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltothr" runat="Server" />
                                                               
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Out Time">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOut" runat="server" Width="130px"></asp:Label>
                                                               
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Left" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRem" runat="Server"  /> 
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Left" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRem" runat="Server" /> 
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>--%>
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                        <tr>
                                                            <td class="GridViewHeader" style="width: 100px">Employee Name
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 100px">Employee Code
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 100px">Date
                                                            </td>
                                                            <td class="GridViewHeader" style="width: 100px">Shift Schedule
                                                            </td>
                                                            <%-- <td class="GridViewHeader" style="width: 100px">
                                                                    Total Hrs
                                                                </td>
                                                                <td class="GridViewHeader" style="width: 300px">
                                                                    Remarks
                                                                </td>--%>
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
        <script>
            function myFunction() {
                alert("Are you sure you want to delete this attendance punch Detail ? ");
            }
        </script>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerFrom);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerTo);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerRfFrom);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(dtpickerRfTo);

            function dtpickerFrom(sender, args) {
                if (document.getElementById('<%= txtDate.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtDate.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }

            if (document.getElementById('<%= txtDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtDate.ClientID %>'),
                    format: 'DD-MM-YYYY'
                });
            }



            function dtpickerTo(sender, args) {
                if (document.getElementById('<%= txtDateTo.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtDateTo.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }


            if (document.getElementById('<%= txtDateTo.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtDateTo.ClientID %>'),
                    format: 'DD-MM-YYYY'
                });
            }

            
        </script>
        <script>
            $(document).ready(function () {
                var date_input = $('input[name="txtDate"]'); //our date input has the name "date"
                var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
                var options = {
                    format: 'dd/mm/yyyy',
                    container: container,
                    todayHighlight: true,
                    autoclose: true,
                };
                date_input.datepicker(options);
            })
        </script>
    </section>


</asp:Content>
