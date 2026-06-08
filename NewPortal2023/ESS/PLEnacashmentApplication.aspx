<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="PLEnacashmentApplication.aspx.cs" Inherits="NewPortal2023.ESS.PLEnacashmentApplication" %>

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
    <script language="javascript" src="Common.js" type="text/javascript"></script>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">PL_Enacashment</h3>
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

                                        <div class="form-group">
                                            <div class="form-group">
                                                <label class="col-sm-3 labels">Year :</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="drpYear" Visible="true" runat="server" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                    </asp:DropDownList>
                                                </div>

                                                <label class="col-sm-3 labels">Month :</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="drpMonth" Visible="true" runat="server" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-3 labels">Type :</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="drpLeaveType" Visible="true" runat="server" OnSelectedIndexChanged="drpLeaveType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                    </asp:DropDownList>

                                                </div>

                                                <label class="col-sm-3 labels">PL Opening :</label>
                                                <div class="col-sm-3">
                                                    
                                                    <asp:TextBox ID="txtopening1" runat="server" CssClass="form-control input-sm Readonly" Enabled="false" AutoPostBack="true" ></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-3 labels">PL Enacashment :</label>
                                                <div class="col-sm-3">
                                                    
                                                   <asp:TextBox ID="txtenacash1" runat="server" OnTextChanged="txtenacash_TextChanged" CssClass="form-control input-sm"  AutoPostBack="true" ></asp:TextBox>
                                                </div>

                                                <label class="col-sm-3 labels">Balance :</label>
                                                <div class="col-sm-3">
                                                 
                                                    <asp:TextBox ID="txtBal" runat="server" Enabled="false" OnTextChanged="txtenacash_TextChanged" CssClass="form-control input-sm readonly"  AutoPostBack="true" ></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                            <asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                    <%--<asp:PostBackTrigger ControlID="chk2" />
                                    <asp:PostBackTrigger ControlID="chk3" />
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
    </section>


</asp:Content>
