<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="LeaveApplication.aspx.cs" Inherits="NewPortal2023.ESS.LeaveApplication1" %>

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
    <script type="text/javascript">
        $(function () {

            var currentDate = new Date();

            $('[id*=txtDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",
                autoclose: true,
              //  endDate: currentDate // Disable dates after the current date
            });
        });
        $(function () {
            $('[id*=txtToDt]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",

                //autoclose: true,
               // endDate: currentDate // Disable dates after the current date
            });
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
            font-family: Tahoma;
            /* height: 20px; */
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
                            <h3 style="color: white">Leave Application</h3>
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
                                        <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>
                                        <div id="divAlertSucc" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessageSucc" runat="server"></asp:Label>
                                        </div>

                                        &emsp;

                                      

                                        <div id="AppForm" visible="true" runat="server">
                                            <div class="panel-body">
                                                <div class="adv-table">
                                                    <div id="" class="form-horizontal">
                                                        <fieldset>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 labels">From Date :</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled" placeholder="Date" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>

                                                                </div>

                                                                <label class="col-sm-3 labels">To Date :</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control input-sm datepicker" AutoCompleteType="Disabled" placeholder="Date" AutoPostBack="true" OnTextChanged="txtToDt_TextChanged"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-sm-3 labels">Address for Communication :</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox ID="txtAdd" TextMode="MultiLine" runat="server" CssClass="form-control input-sm"></asp:TextBox>

                                                                </div>

                                                                <label class="col-sm-3 labels">Remarks :</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox ID="txtRem" TextMode="MultiLine" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-sm-3 labels">Total Days :</label>
                                                                <div class="col-sm-3">
                                                                    <asp:Label ID="lblLeaves" runat="server" Text="0"></asp:Label>
                                                                </div>

                                                                <label class="col-sm-3 labels">Status :</label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control input-sm-3" Width="150px">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-sm-3 labels">Leave Type :</label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="drpLeave" runat="server" OnSelectedIndexChanged="drpLeave_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                                    </asp:DropDownList>

                                                                </div>

                                                                <label class="col-sm-3 labels">Reason :</label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="drpReason" OnSelectedIndexChanged="drpReason_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control input-sm-3" Width="150px">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">

                                                                <div style="text-align: left;">
                                                                    <%--<asp:Label class="col-sm-6 labels" ID="lblUpload" runat="server">Upload Medical Certificate :</asp:Label>--%>
                                                                    <label class="col-sm-3 labels" id="lblUpload" runat="server" style="margin-top: 20px">Upload Medical Certificate :</label>
                                                                    <div class="col-sm-4" id="FileUpload" runat="server" style="text-align: center; margin-top: 20px">
                                                                        <asp:FileUpload ID="fupldDocument" runat="server" CssClass="btn fileinput-button"/>&nbsp;
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="form-group">
                                                                <asp:Label class="col-sm-12 labels" ID="lblcrdate" runat="server" Visible="false">Creation Date :</asp:Label>
                                                            </div>


                                                            <div id="uploadedDoc" runat="server">
                                                                <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
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
                                                                                    OnClick="lnkBtnOpenFile_Click" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                            <tr>
                                                                                <td class="GridViewHeader" style="width: 10%">
                                                                                    <asp:Literal ID="Literal6" runat="server" Text="No Files." />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                <asp:Button ID="btnApprove" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnApprove_Click"  Visible="true" OnClientClick="showLoader();" />
                                                <asp:Button ID="btnDraft" CssClass="btn" runat="server" Text="Draft" OnClick="btnDraft_Click" Visible="False" OnClientClick="showLoader();" />
                                                <asp:Button ID="btnClose" CssClass="btn btn-danger" runat="server" Text="Close" Visible="False" OnClientClick="showLoader();" />
                                            </div>

                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="txtDate" /> 
                                    <asp:PostBackTrigger ControlID="txtToDt" />
                                    <asp:PostBackTrigger ControlID="btnApprove" />
                                    <asp:PostBackTrigger ControlID="btnDraft" />
                                    <%--  <asp:PostBackTrigger ControlID="btnAddNew" />--%>
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="drpLeave" />
                                    <%--<asp:PostBackTrigger ControlID="drpLeaveType" />--%>
                                    <%--  <asp:PostBackTrigger ControlID="chk8" />
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
        <%--<script>
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
        </script>--%>
    </section>


</asp:Content>
