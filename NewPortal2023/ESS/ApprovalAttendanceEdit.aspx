<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ApprovalAttendanceEdit.aspx.cs" Inherits="NewPortal2023.ESS.ApprovalAttendanceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
    <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
        media="screen" />
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
    <style type="text/css">

        .page {
            width: 100%;
            background-color: #fff;
            margin: 20px auto 0px auto;
            border: 1px solid #496077;
        }

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
            background-color: #FFFFFF;
        }

        .table4N {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
            background-color: #cccccc;
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
            font-size: 13PX;
        }

        .GridViewHeader {
            font-weight: bold;
            font-size: 12PX;
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Pending OT/CO Approval</h3>
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

                                        <div style="overflow-y: auto; max-height: 300px;"> 
                                            <%--style="overflow-x: scroll; width: 930px;"--%>
                                            
                                           <%-- <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered table-striped dynamic-table"
                                                  OnRowDataBound="gvLeave_RowDataBound"
                                                DataKeyNames="ENTRY_AID" 
                                                 OnRowEditing="gvLeave_RowEditing"  OnRowUpdating="gvLeave_RowUpdating" OnRowCancelingEdit="gvLeave_RowCancelingEdit"
                                               GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="12px" >
                                                 <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                <PagerStyle CssClass="pagination-ys" />--%>
                                               <%-- OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound"
                                                DataKeyNames="ENTRY_AID" OnRowEditing="gvList_RowEditing" OnRowUpdating="gvList_RowUpdating" OnRowCancelingEdit="gvList_RowCancelingEdit">
                                                <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                                                <PagerStyle CssClass="pagination-ys" />--%>
                                             <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"  BorderWidth="0px" OnRowEditing="gvLeave_RowEditing"
                                                    OnRowUpdating="gvLeave_RowUpdating" OnRowCancelingEdit="gvLeave_RowCancelingEdit" DataKeyNames="ENTRY_AID" OnRowDataBound="gvLeave_RowDataBound"
                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="Time Sheet">
                                                <Columns>
                                                    <asp:TemplateField Visible="false" HeaderText="APPROVE">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="Server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aid" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEntryAid" runat="Server" Text='<%# Eval("ENTRY_AID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Emp Code">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpCode" runat="Server" Text='<%# Eval("EMP_CODE") %>' Width="70px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="Server" Text='<%# Eval("Emp_Name") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift Date">
                                                        <ItemStyle CssClass="GridViewItem" Width="20%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("DATE") %>' Width="90px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShift" runat="Server" Text='<%# Eval("[Old_Shift_Schedule]") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification Shift">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblupdateShift" runat="Server" Text='<%# Eval("Shift_Schedule") %>' Width="40px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="In Time">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTimeIn" runat="Server" Text='<%# Eval("[Old_Time_In]") %>' Width="90px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification InTime">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUpdateTimeIn" runat="Server" Text='<%# Eval("Time_In") %>' Width="90px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Out Time">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTimeOut" runat="server" Text='<%# Eval("Old_Time_Out") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification OutTime">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUpdateTimeOut" runat="server" Text='<%# Eval("Time_Out") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OT">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOT" runat="server" Text='<%# Eval("[Old_OT]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification OT">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUpdateOT" runat="server" Text='<%# Eval("[OT]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved OT" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSUMOT" runat="server" Text='<%# Eval("[SUMOT]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CO">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCO" runat="server" Text='<%# Eval("[Old_CO]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification CO">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUpdateCO" runat="server" Text='<%# Eval("[CO]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved CO" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSUMCO" runat="server" Text='<%# Eval("[SUMCO]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Remark">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblRemarks" TextMode="MultiLine" Enabled="false" runat="Server" Text='<%# Eval("REMARKS") %>' Width="120px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Check Approved OT and CO">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" Text="View" CssClass="btn btn-info" AutoPostBack="true" OnClick="lnkView_Click" runat="server" Width="120px" OnClientClick="showLoader();" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="grddrpAction" runat="server" CssClass="form-control input-sm-3" AutoPostBack="true" Enabled="false" Width="100px" Onchange="showLoader();">
                                                                <asp:ListItem Value="" Text="---Select Action---"></asp:ListItem>
                                                                <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                                <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approver Remark">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="Server" Width="100px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton Text="Edit" runat="server" AutoPostBack="true" CssClass="btn btn-warning" CommandName="Edit" OnClientClick="showLoader();" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="Update" Text="Update" AutoPostBack="true" CssClass="btn btn-success" runat="server" CommandName="Update" OnClientClick="showLoader();"/>
                                                        </EditItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Cancel" runat="server" Text="Cancel" AutoPostBack="true" CssClass="btn btn-danger" CommandName="Cancel" OnClientClick="showLoader();"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>


                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    </tr>
                                                                 </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                            <div id="MyPopup" class="modal fade" role="dialog">
                                                <div class="modal-dialog">
                                                    <!-- Modal content-->
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal">
                                                                &times;</button>
                                                            <h4 class="modal-title"></h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <%-- <asp:Label ID="lblmessage1" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>--%>
                                                                                Employee Code : -
                                                                                <asp:Label ID="lblEmpCodes" runat="server">  </asp:Label>
                                                            <%-- <asp:Label ID="lblEmpCode" runat="server" class=" input-sm"></asp:Label>--%>

                                                            <hr />
                                                            Approved OT :-
                                                                                <asp:Label ID="lblAppOT" runat="server">  </asp:Label>
                                                            <hr />
                                                            Approved CO :-
                                                                                <asp:Label ID="lblAppCO" runat="server">  </asp:Label>
                                                            <hr />
                                                            <asp:Label ID="Label2" runat="server" Style="font-weight: bold; text-align: left; color: blue" Text=" Absent Record "> </asp:Label>
                                                            <hr />
                                                            <asp:GridView ID="gvAbsectList" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                CssClass="table4" Width="100%">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="EMPLOYEE CODE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblName" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOrigin" runat="Server" Text='<%# Eval("EMP_FNAME") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                        <tr>
                                                                            <td class="GridViewHeader" style="width: 10%">
                                                                                <asp:Literal ID="Literal6" runat="server" Text="No Record Found ." />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>

                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                                                Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        &emsp;

                                        
                                        <div class="form-group col-sm-12">
                                            <label class="labels" for="lblPrimary" style="">Select All :-</label>
                                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" />
                                        </div>

                                        <div style="margin: 15px">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <label class="labels" for="lblPrimary">Action Type:</label>
                                                        <asp:DropDownList ID="drpAllAction" runat="server" CssClass="form-control-sm form-control input-s-sm" Enabled="false" Onchange="showLoader();">
                                                            <asp:ListItem Value="" Text="---Select Action---"></asp:ListItem>
                                                            <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                            <asp:ListItem Value="Reject" Text="Reject"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <label class="labels" for="lblPrimary">Remark:</label>
                                                        <asp:TextBox ID="txtAllRemarks" runat="Server" Enabled="false" TextMode="MultiLine" CssClass="form-control" />
                                                    </div>

                                                    <div class="col-sm-3" style=" margin-top: 25px">
                                                        <asp:Button ID="btnApprove" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnApprove_Click" OnClientClick="showLoader();"/>
                                                        <asp:Button ID="btnReject" CssClass="btn btn-danger" runat="server" Visible="false" Text="Reject" OnClick="btnReject_Click" OnClientClick="showLoader();"/>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>





                                        <div class="col-sm-12" style="text-align: center; margin-top: 10px">
                                            <asp:Button ID="lnkApp" runat="server" CssClass="btn btn-info" Text="View Attendance Approved List" OnClick="lnkApp_Click" OnClientClick="showLoader();"/>
                                        </div>

                                        &emsp;

                                        <div class="form-group" id="divApproTCO" visible="true" runat="server">
                                            <asp:GridView ID="grApprOTCO" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" ToolTip="Approved OT/CO" CellPadding="5"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Aid" Visible="false">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEntryAid" runat="Server" Text='<%# Eval("ENTRY_AID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Emp Code">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpCode" runat="Server" Text='<%# Eval("EMP_CODE") %>' Width="70px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="Server" Text='<%# Eval("Emp_Name") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift Date">
                                                        <ItemStyle CssClass="GridViewItem" Width="20%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("DATE") %>' Width="90px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShift" runat="Server" Text='<%# Eval("[Old_Shift_Schedule]") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification Shift">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblupdateShift" runat="Server" Text='<%# Eval("Shift_Schedule") %>' Width="40px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="In Time">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTimeIn" runat="Server" Text='<%# Eval("[Old_Time_In]") %>' Width="90px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification InTime">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUpdateTimeIn" runat="Server" Text='<%# Eval("Time_In") %>' Width="90px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Out Time">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTimeOut" runat="server" Text='<%# Eval("Old_Time_Out") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification OutTime">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUpdateTimeOut" runat="server" Text='<%# Eval("Time_Out") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OT">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOT" runat="server" Text='<%# Eval("[Old_OT]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification OT">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUpdateOT" runat="server" Text='<%# Eval("[OT]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CO">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCO" runat="server" Text='<%# Eval("[Old_CO]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rectification CO">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUpdateCO" runat="server" Text='<%# Eval("[CO]") %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Employee Remark">
                                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="lblRemarks" TextMode="MultiLine" Enabled="false" runat="Server" Text='<%# Eval("REMARKS") %>' Width="120px" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                                        </asp:TemplateField>--%>

                                                    <asp:TemplateField Visible="true" HeaderText="Status">
                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkstatus" runat="Server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Approved Date" Visible="true">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="250px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblApprDate" runat="Server" Text='<%# Eval("APPR_DATE") %>' />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>--%>

                                                    <%--  <asp:TemplateField HeaderText="Approver Remark">
                                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="Server" Width="100px" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                                        </asp:TemplateField>--%>
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>



                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnApprove" />
                                    <asp:PostBackTrigger ControlID="btnReject" />
                                    <asp:PostBackTrigger ControlID="lnkApp" />
                                    <asp:PostBackTrigger ControlID="gvLeave" />
                                   <%-- <asp:PostBackTrigger ControlID="chk5" />
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
        <script type="text/javascript">
            function ShowPopup(title, body) {
                $("#MyPopup .modal-title").html(title);
                //$("#MyPopup .modal-body").html(body);
                $("#MyPopup").modal("show");
            }
        </script>
    </section>


</asp:Content>
