<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="EmployeeMissingPunchReport.aspx.cs" Inherits="NewPortal2023.ESS.EmployeeMissingPunchReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                            <h3 style="color: white">Employee Missing Punch Report</h3>
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

                                        <div id="trSubmit" runat="server">
                                            <div class="form-group">
                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-2 labels">From Date<span style="color: Red;">*</span> :</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtFromDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm datepicker" placeholder="Date"></asp:TextBox>
                                                        </div>

                                                        <label class="col-sm-2 labels">To Date<span style="color: Red;">*</span> :</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtToDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm datepicker" placeholder="Date"></asp:TextBox>
                                                        </div>

                                                        <label class="col-sm-2 labels">Emp Code<span style="color: Red;">*</span> :</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtEmpCode" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm"></asp:TextBox>
                                                        </div>


                                                    </div>
                                                </div>

                                                <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                    <asp:Button ID="btnGenerateAttendanceReport" runat="server" CssClass="btn btn-success" Text="Generate Employee Missing Punch Report"
                                                        AutoPostBack="true" OnClick="btnGenerateAttendanceReport_Click" OnClientClick="showLoader();" />
                                                </div>

                                                <div class="col-sm-12" style="text-align: center; margin-top: 20px">
                                                    <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-primary" Text="Export"
                                                        AutoPostBack="true" OnClick="btnExport_Click" OnClientClick="showLoader();" />
                                                </div>

                                            </div>
                                        </div>

                                        <div id="trViewList" runat="server">
                                            <div id="DivgvMultipleList" visible="true" runat="server" style="overflow-x: scroll;">
                                                <%-- <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    Width="100%">--%>
                                                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Code" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkAppNo" runat="server" Text='<%# Eval("EMP_CODE") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="0%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="Server" Text='<%# Eval("Emp_Name") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="12%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Day">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldays" runat="Server" Text='<%# Eval("Days") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrigin" runat="Server" Text='<%# Eval("Date") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shift">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusTRANS" runat="Server" Text='<%# Eval("Shift_Schedule") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="In Time">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNewStatusREIMB" runat="Server" Text='<%# Eval("New_Time_In") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="12%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Out Time">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNewStatusTAX" runat="Server" Text='<%# Eval("New_Time_Out") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="12%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Time In" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusREIMB" runat="Server" Text='<%# Eval("Time_In") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="0%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Time Out" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusTAX" runat="Server" Text='<%# Eval("Time_Out") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="0%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Working hrs">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusLUP" runat="Server" Text='<%# Eval("Total_Working_hrs") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shift hrs">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusTRAVEL" runat="Server" Text='<%# Eval("Shift_hrs") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="5%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="OT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusJA" runat="Server" Text='<%# Eval("OT") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="5%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("CO") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="5%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="false" HeaderText="P">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPresent" Visible="false" runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="0%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderText="A">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAbsent" runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="0%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="false" HeaderText="L">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllate" runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="0%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField Visible="true" HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAttRemark" runat="server" CssClass="input"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridViewItem" Width="20%" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridViewHeader" />
                                </asp:TemplateField>--%>

                                                        <asp:TemplateField Visible="true" HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAttRemark" runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="40%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Punch Remark" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPunchRmk" Visible="false" runat="Server" Text='<%# Eval("PUNCH_REMARKS") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Punch Status" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPunchSts" Visible="false" runat="Server" Text='<%# Eval("PUNCH_STATUS") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rectification">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkRectification" runat="server" Text="Update"
                                                                    OnClick="lnkRectification_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldepartment" Visible="false" runat="Server" Text='<%# Eval("Department") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ENTRY_AID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEntryAid" runat="Server" Text='<%# Eval("ENTRY_AID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="STATUS" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStat" runat="Server" Text='<%# Eval("STATUS") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
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
                                                                    <asp:Literal ID="Literal6" runat="server" Text="Click on 'New Claim'" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div id="trEdit" runat="server" visible="false">
                                            <div id="" class="form-horizontal">
                                                <div style="text-align: center; background-color: orange; padding: 2px;">
                                                    <h3 style="color: white;">Attendance Rectification</h3>
                                                </div>

                                                <div>
                                                    <br />
                                                </div>

                                                <div>
                                                    <h4><u>Employee Details</u></h4>
                                                </div>

                                                <div class="form-group">
                                                    <div class="form-group" id="trEntryId1" runat="server" visible="false">
                                                        <label class="col-sm-3 labels">Employee AID :</label>
                                                        <div class="col-sm-3">
                                                            <asp:Label ID="lblEntryId" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">Employee Code :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblEmpCode" runat="server"></asp:Label>
                                                    </div>

                                                    <label class="col-sm-3 labels">Employee Name :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">Roster Date :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                    </div>

                                                    <label class="col-sm-3 labels">Total Working Hrs :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblTWhr" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <hr />

                                                <div>
                                                    <h4><u>Shift Rectification</u></h4>
                                                </div>


                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">Roster Schedule :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblRosterSch" runat="server"></asp:Label>

                                                    </div>

                                                    <label class="col-sm-3 labels">Update Schedule<span style="color: Red;"></span> :</label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="drpShiftType" runat="server" CssClass="input" Width="150px">
                                                            <asp:ListItem Value=" ">--Select Shift Type---</asp:ListItem>
                                                            <asp:ListItem Value="EV">EV</asp:ListItem>
                                                            <asp:ListItem Value="GH">GH</asp:ListItem>
                                                            <asp:ListItem Value="GN">GN</asp:ListItem>
                                                            <asp:ListItem Value="GS">GS</asp:ListItem>
                                                            <asp:ListItem Value="MS">MS</asp:ListItem>
                                                            <asp:ListItem Value="NS">NS</asp:ListItem>
                                                            <asp:ListItem Value="WO">WO</asp:ListItem>

                                                            <asp:ListItem Value="Extra MS">Extra MS</asp:ListItem>
                                                            <asp:ListItem Value="Extra EV">Extra EV</asp:ListItem>
                                                            <asp:ListItem Value="Extra NS">Extra NS</asp:ListItem>
                                                            <%--<asp:ListItem Value=" "> </asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <hr />

                                                <div>
                                                    <div>
                                                        <h4><u>Attendance Punch Rectification</u></h4>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">In Time<span style="color: Red;">*</span> :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblInTime" runat="server"></asp:Label>

                                                    </div>

                                                    <label class="col-sm-3 labels">Out Time<span style="color: Red;">*</span> :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblOuttime" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">Update In Date<span style="color: Red;">*</span> :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtInDate" runat="server" CssClass="form-control input-sm datepicker"></asp:TextBox>
                                                    </div>

                                                    <label class="col-sm-3 labels">Update Out Date<span style="color: Red;">*</span> :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtOutDate" runat="server" CssClass="form-control input-sm datepicker"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <hr />

                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">Update In Time<span style="color: Red;">*</span> :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtIntime" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>

                                                    <label class="col-sm-3 labels">Update Out Time<span style="color: Red;">*</span> :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtOuttime" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div>
                                                    <div>
                                                        <h4><u>OT and CO Rectification</u></h4>
                                                    </div>
                                                </div>

                                                <hr />

                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">OT :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblOt" runat="server"></asp:Label>

                                                    </div>

                                                    <label class="col-sm-3 labels">Update OT :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtUpdateOT" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                        <asp:Label ID="lblUpdateOT" Visible="false" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">CO :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblCo" runat="server"></asp:Label>
                                                    </div>

                                                    <label class="col-sm-3 labels">Update CO :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtUpdateCO" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                        <%--<asp:Label ID="Label5" runat="server"></asp:Label>--%>
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <label class="col-sm-3 labels">Remarks :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtRem" TextMode="MultiLine" runat="server" CssClass="form-control input-sm" MaxLength="500"></asp:TextBox>
                                                    </div>

                                                    <label class="col-sm-3 labels">Shift Hrs :<span style="color: Red;"></span> :</label>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblShr" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                    <asp:Button ID="btnApprove" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnApprove_Click" OnClientClick="showLoader();" />
                                                    <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="showLoader();" />
                                                    <asp:Button ID="btnCancel" CssClass="btn btn-warning" runat="server" Text="Cancel" OnClick="btnCancel_Click" OnClientClick="showLoader();" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:PostBackTrigger ControlID="chk1" />
                                    <asp:PostBackTrigger ControlID="chk2" />
                                    <asp:PostBackTrigger ControlID="chk3" />
                                    <asp:PostBackTrigger ControlID="chk4" />
                                    <asp:PostBackTrigger ControlID="chk5" />
                                    <asp:PostBackTrigger ControlID="chk6" />
                                    <asp:PostBackTrigger ControlID="chk7" />
                                    <asp:PostBackTrigger ControlID="chk8" />
                                    <asp:PostBackTrigger ControlID="radioPrimary1" />--%>
                                    <asp:PostBackTrigger ControlID="btnGenerateAttendanceReport" />
                                    <asp:PostBackTrigger ControlID="btnExport" />
                                    <asp:PostBackTrigger ControlID="btnApprove" />
                                    <asp:PostBackTrigger ControlID="btnDelete" />
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>

                        <div>
                            <rsweb:ReportViewer ID="rptPrint" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                interactivedeviceinfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                                BackColor="#CCCCFF" Height="600px" Width="100%" ZoomMode="PageWidth" PageCountMode="Actual">
                            </rsweb:ReportViewer>
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
                if (document.getElementById('<%= txtFromDate.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtFromDate.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }

            if (document.getElementById('<%= txtFromDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtFromDate.ClientID %>'),
                    format: 'DD-MM-YYYY'
                });
            }



            function dtpickerTo(sender, args) {
                if (document.getElementById('<%= txtToDate.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtToDate.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }


            if (document.getElementById('<%= txtToDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtToDate.ClientID %>'),
                    format: 'DD-MM-YYYY'
                });
            }

            function dtpickerRfFrom(sender, args) {
                if (document.getElementById('<%= txtInDate.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtInDate.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }

            if (document.getElementById('<%= txtInDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtInDate.ClientID %>'),
                    format: 'DD-MM-YYYY'
                });
            }



            function dtpickerRfTo(sender, args) {
                if (document.getElementById('<%= txtOutDate.ClientID %>')) {
                    const compPur = new Litepicker({
                        element: document.getElementById('<%= txtOutDate.ClientID %>'),
                        format: 'DD-MM-YYYY'
                    });
                }
            }


            if (document.getElementById('<%= txtOutDate.ClientID %>')) {
                const compPur = new Litepicker({
                    element: document.getElementById('<%= txtOutDate.ClientID %>'),
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
        <script>
            $(document).ready(function () {
                $('#<%= txtIntime.ClientID %>').timepicker({
                    timeFormat: 'HH:mm:ss', // 24-hour format
                    interval: 1,
                    scrollbar: true

                });
            });

            $(document).ready(function () {
                $('#<%= txtOuttime.ClientID %>').timepicker({
                    timeFormat: 'HH:mm:ss', // 24-hour format
                    interval: 1,
                    scrollbar: true

                });
            });

        </script>
    </section>


</asp:Content>
