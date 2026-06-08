<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="NPL_OutSideDuty.aspx.cs" Inherits="NewPortal2023.ESS.NPL_OutSideDuty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>
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
                            <h3 style="color: white">OutDoor Duty Application</h3>
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

                                        &emsp;

                                        <div class="row">
                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" Autopostback="true" Style="margin-left: 7px;" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="showLoader();" />
                                        </div>


                                        <div id="AppList" runat="server">
                                            <label>
                                                <h3>List Of OutDoor Duty Applications</h3>
                                            </label>
                                            <br />
                                            <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvLeave_RowDataBound"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblId" runat="Server" Text='<%# Eval("CID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From DateTime">
                                                        <ItemStyle CssClass="GridViewItem" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFrom" runat="Server" Text='<%# Eval("FROM_DT") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To DateTime">
                                                        <ItemStyle CssClass="GridViewItem" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTo" runat="Server" Text='<%# Eval("TO_DATE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Out Door Type">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOdType" runat="Server" Text='<%# Eval("ODTYPE") %>' />

                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" HeaderText="Reason">
                                                        <ItemStyle CssClass="GridViewItem" Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReason" runat="Server" Text='<%# Eval("REASON_ID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Create DateTime">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReason0" runat="Server" Text='<%# Eval("CREATEDDT") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReason1" runat="Server" Text='<%# Eval("REMARKS") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkstatus" runat="Server" Text='<%# Eval("STATUS") %>' OnClick="lnkstatus_Click" ForeColor="Blue"/>
                                                            <asp:LinkButton ID="lnkcancel" runat="Server" Text=' / Cancel Request' OnClick="lnkcancel_Click1" ForeColor="Blue"/>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Updated On" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUP" runat="Server" Text='<%# Eval("UPDATED") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <table class="table table-bordered table-condensed">
                                                        <tr>
                                                            <td style="width: 10%" class="Title">
                                                                <asp:Literal ID="Literal6" runat="server" Text="No record found." />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>

                                        <div id="AppForm" runat="server" class="form-group row">

                                            <div class="form-group row">
                                                <label class="col-sm-3 labels">Select OutDoor Type :</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="drpType" runat="server" Visible="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged" CssClass="form-control input-sm-3" Width="150px" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Select Type</asp:ListItem>
                                                        <asp:ListItem Value="Full_Day">Full_Day</asp:ListItem>
                                                        <asp:ListItem Value="First_Half_Day">First_Half_Day</asp:ListItem>
                                                        <asp:ListItem Value="Second_Half_Day">Second_Half_Day</asp:ListItem>
                                                        <asp:ListItem Value="Specific_Time">Specific_Time</asp:ListItem>
                                                        <asp:ListItem Value="Work_From_Home">Work_From_Home</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-3 labels">From Date :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled" placeholder="Date" AutoPostBack="true"></asp:TextBox>

                                                </div>

                                                <label class="col-sm-3 labels">To Date :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled" placeholder="Date" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div id="trTime1" runat="server" class="form-group">
                                                <label class="col-sm-3 labels">From Time :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtFormTime" Placeholder="00:00" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RegularExpressionValidator
                                                        ID="regextxtSessionTime" runat="server"
                                                        ControlToValidate="txtFormTime"
                                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"
                                                        ErrorMessage="You must enter a valid time. Format: HH:MM:SS"
                                                        Display="Dynamic"
                                                        SetFocusOnError="true">
                                                    </asp:RegularExpressionValidator>
                                                </div>

                                                <label class="col-sm-3 labels">To Time :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtToTime" Placeholder="00:00" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RegularExpressionValidator
                                                        ID="RegularExpressionValidator1" runat="server"
                                                        ControlToValidate="txtToTime"
                                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"
                                                        ErrorMessage="You must enter a valid time. Format: HH:MM:SS"
                                                        Display="Dynamic"
                                                        SetFocusOnError="true">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="form-group" id="trTotalhrTime" runat="server" visible="false">
                                                <label>Total Hours:-</label>
                                                <asp:Label ID="txtTotalHrTime" runat="server" CssClass="input"></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblNotifymessage" runat="server" Style="width: 100%; font-size: 14px;" ForeColor="#FF3300"></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-3 labels">Status :</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="drpStatus" OnTextChanged="drpStatus_OnSelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control input-sm-3" Width="150px">
                                                    </asp:DropDownList>
                                                </div>

                                                <label class="col-sm-3 labels">Remarks :</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtRem" runat="server" CssClass="input" Height="35px" MaxLength="500"
                                                        TextMode="MultiLine" Width="170px"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblLeaves" Visible="false" runat="server" Text="0"></asp:Label>
                                                <asp:Label ID="lblcrdate" Visible="false" runat="server"></asp:Label>
                                            </div>

                                            <%--  <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnApprove_Click" />
                                            </div>--%>

                                            <%--<div class="col-sm-12" style="text-align: center; margin-top: 20px">
                                                <asp:Button ID="btnDraft" runat="server" CssClass="btn btn-primary" Text="Draft" OnClick="btnDraft_Click" />
                                            </div>--%>

                                            <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                <asp:Button ID="btnApprove" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnApprove_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                                                <asp:Button ID="btnDraft" CssClass="btn btn-warning" runat="server" Text="Draft" OnClick="btnDraft_Click" OnClientClick="showLoader();" />
                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" runat="server" Text="Close" OnClick="btnClose_Click" OnClientClick="showLoader();" />
                                                <asp:HiddenField ID="hdnAID" runat="server" />
                                            </div>

                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnApprove" />
                                    <asp:PostBackTrigger ControlID="btnDraft" />
                                    <asp:PostBackTrigger ControlID="btnAddNew" />
                                    <%--            <asp:PostBackTrigger ControlID="chk4" />
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
        <script>
            $(document).ready(function () {
                $('#<%= txtFormTime.ClientID %>').timepicker({
                    timeFormat: 'HH:mm:ss', // 24-hour format
                    interval: 1,
                    scrollbar: true

                });
            });

            $(document).ready(function () {
                $('#<%= txtToTime.ClientID %>').timepicker({
                    timeFormat: 'HH:mm:ss', // 24-hour format
                    interval: 1,
                    scrollbar: true

                });
            });

        </script>
    </section>


</asp:Content>

