<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="PaySlip.aspx.cs" Inherits="NewPortal2023.ESS.PaySlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="assets/js/dynamic_table_init.js"></script>


       <script src="../Assets/pdfjs/pdf.min.js"></script>
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
    <style type="text/css">
        .labels {
            float: left;
            padding-top: 7px;
            margin-bottom: 0;
            text-align: left;
            font-weight: 300;
            font-size: 15px;
            font-weight: bold;
        }

        .HeaderBarThreshold {
            padding-left: 10px;
            font-weight: bold;
            width: 300px;
            height: 30px;
            border: 1px solid #000;
            text-align: left;
        }

            .HeaderBarThreshold:hover {
                color: blue;
                background: lightblue;
            }
    </style>
    <script type="text/javascript">
        function showLoader() {
            document.getElementById("loader").style.display = 'block';
        }

    </script>

    <script type="text/javascript">
        function clearFileInputField(divId) {
            document.getElementById(divId).innerHTML = document.getElementById(tagId).innerHTML;
        }

        function ConfirmDel() {
            return confirm("Are you sure you want to delete this record?");
        }
    </script>
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
                            <h3 style="color: white">Payslip</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" EnableViewState="true">
                                <ContentTemplate>--%>
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

                                <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="vwList" runat="server">
                                        <div class="form-group">
                                            <%--class="adv-table" style="border: solid; border-color: darkcyan; margin-top: 30px"--%>


                                            <div class="form-group">
                                                <div class="col-sm-6" style="text-align: center;" id="trPaySlipType" runat="server" visible="false">
                                                    <label for="lblRemark" class="col-sm-4"><span><b>Select Payslip Type :-</b></span></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="drpPaySlipType" runat="server" CssClass="form-control input-sm-4">
                                                            <asp:ListItem Value="">[Select One]</asp:ListItem>
                                                            <asp:ListItem Value="Current">Current</asp:ListItem>
                                                            <asp:ListItem Value="Previous">Previous</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-12" id="trMonth" runat="server">
                                                    <label for="lblRemark" class="col-sm-4"><span><b>Select Month/Year:-</b></span></label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control input-sm-4" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged"
                                                             AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <br />
                                                <br />
                                            </div>
                                            <div class="form-group">
                                                <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    CssClass="table table-bordered table-striped" DataKeyNames="FILENAME">


                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="true" HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Width="100%" Text='<%# Eval("FILENAME") %>' CssClass="HeaderBarThreshold"
                                                                    OnClick="lnkBtnOpenFile_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField Visible="true" HeaderText="Preview">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkBtnOpenPreviewFile" runat="server" Width="100%" Text="Preview" CssClass="HeaderBarThreshold"
                                                                            OnClick="lnkBtnOpenPreviewFile_Click" OnClientClick="SetTarget();" />
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                    </Columns>
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
                                            <%-- <div class="form-group">
                                                  <%--  <div class="col-sm-12">
                                                        <iframe src="<%=filePath%>" style="width: 100%; height: 500px;"></iframe>
                                                    </div>


                                                </div>--%>

                                            <div id="pdf-container" style="display: flex; flex-direction: column; align-items: center;"></div>

                                            <script>
                                                function loadPdf(url) {
                                                    
                                                    const container = document.getElementById('pdf-container');
                                                    container.innerHTML = "";

                                                    pdfjsLib.getDocument(url).promise.then(function (pdf) {

                                                        for (let pageNum = 1; pageNum <= pdf.numPages; pageNum++) {
                                                            pdf.getPage(pageNum).then(function (page) {
                                                                const viewport = page.getViewport({ scale: 1 });
                                                                // Create a new canvas for each page
                                                                const canvas = document.createElement('canvas');
                                                                canvas.style.display = "block";
                                                                canvas.style.marginBottom = "20px";
                                                                canvas.style.width = "100%"; // full width
                                                                const context = canvas.getContext('2d');

                                                                // Scale canvas to container width
                                                                const containerWidth = container.clientWidth;
                                                                const scale = containerWidth / viewport.width;
                                                                const scaledViewport = page.getViewport({ scale: scale });

                                                                canvas.height = scaledViewport.height;
                                                                canvas.width = scaledViewport.width;

                                                                // Append canvas to container
                                                                container.appendChild(canvas);

                                                                // Render page into canvas context
                                                                page.render({
                                                                    canvasContext: context,
                                                                    viewport: scaledViewport
                                                                });
                                                            });
                                                        }
                                                    });
                                                }
                                            </script>




                                            <script>
                                                document.getElementById('pdf-container').addEventListener('contextmenu', function (e) {
                                                    e.preventDefault();
                                                });
                                            </script>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>

                            </div>


                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>


</asp:Content>
