<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="PLP_Letter.aspx.cs" Inherits="NewPortal2023.ESS.PLP_Letter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        .table5 {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
        }

        .table4 {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
            background-color: #cccccc;
        }

        .table4 TR {
                padding-right: 1px;
                padding-left: 1px;
                padding-bottom: 1px;
                padding-top: 1px;
                background-color: #ffffff;
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
    <style type="text/css">
        .underline {
            text-decoration: underline;
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
                            <h3 style="color: white">PLP Letter</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                          
                                    <div id="form1" runat="server">
                                        <div id="divAlert" class="alert alert-block alert-success fade in" runat="server" visible="true">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>
                                        
                                            <div class="form-container">
                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">Select Year :</label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="drpMonth" runat="server" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div>
                                                    <br />
                                                    <br />
                                                </div>
                                                <br />





                                                 <div class="form-group" >
                                         


                                            <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField Visible="true" HeaderText="Download">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Width="100%" Text='<%# Eval("FILENAME") %>' ForeColor="Blue" class="underline"
                                                                OnClick="lnkBtnOpenFile_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <td style="width: 10%" class="Title">
                                                                <asp:Literal ID="Literal6" runat="server" Text="Download." />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>

                                            </asp:GridView>
                                        </div>

                                                 <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <iframe src="<%=filePath%>" style="width: 100%; height: 500px;"></iframe>
                                                    </div>
                                                </div>


                                         <%--       <div class="form-group">
                                                    <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                        CssClass="table4" DataKeyNames="FILENAME" Width="795px">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Download">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Width="100%" Text='<%# Eval("FILENAME") %>'
                                                                        OnClick="lnkBtnOpenFile_Click" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" />
                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                <tr>
                                                                    <td class="GridViewHeader" style="width: 10%">
                                                                        <asp:Literal ID="Literal6" runat="server" Text="Download" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>--%>
                                            </div>
                                        
                                    </div>
                               

                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>
</asp:Content>
