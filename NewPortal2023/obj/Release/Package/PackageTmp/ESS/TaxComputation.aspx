<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="TaxComputation.aspx.cs" Inherits="NewPortal2023.ESS.TaxComputation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <script type="text/javascript" src="assets/js/dynamic_table_init.js"></script>--%>
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>

    <script src="../Assets/pdfjs/pdf.min.js"></script>
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
                            <h3 style="color: white">TAX Slip</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>


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
                                    <%--  <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                CssClass="table table-bordered table-striped" DataKeyNames="FILENAME">--%>
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
                                                    <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Width="100%" Text='<%# Eval("FILENAME") %>' CssClass="HeaderBarThreshold"
                                                        OnClick="lnkBtnOpenFile_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField Visible="true" HeaderText="Preview">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenPreviewFile" runat="server" Width="100%" Text="Preview" CssClass="HeaderBarThreshold"
                                                                OnClick="lnkBtnOpenPreviewFile_Click" OnClientClick="SetTarget();" />
                                                       
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
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
                                <%--   <div class="form-group">
                                    <div class="col-sm-12">
                                        <iframe src="<%=filePath%>" style="width: 100%; height: 500px;"></iframe>
                                    </div>
                                </div>--%>

                                <br />

                                    <div style="display: flex; justify-content: center; align-items: center; min-height: 80vh; width: 100%;">
                                            <!-- PDF container with fixed width -->
                                            <div id="pdf-container" style="overflow-y: auto; max-height: 600px; width: 795px;"></div>
                                        </div>


                             <%--   <div class="d-flex justify-content-center">--%>
                                  <%--  <div id="pdf-container" style="overflow-y: auto; max-height: 600px; width: 795px;"></div>--%>
                             <%--   </div>--%>


                                <script>
                                    const url = "<%=filePath%>"; // your server-side PDF path

                                    const container = document.getElementById('pdf-container');

                                    pdfjsLib.getDocument(url).promise.then(function (pdf) {
                                        // Loop through all pages
                                        for (let pageNum = 1; pageNum <= pdf.numPages; pageNum++) {
                                            pdf.getPage(pageNum).then(function (page) {
                                                const viewport = page.getViewport({ scale: 1.5 });

                                                // Create a new canvas for each page
                                                const canvas = document.createElement('canvas');
                                                canvas.style.display = "block";
                                                canvas.style.marginBottom = "20px";
                                                const context = canvas.getContext('2d');
                                                canvas.height = viewport.height;
                                                canvas.width = viewport.width;

                                                // Append canvas to container
                                                container.appendChild(canvas);

                                                // Render page into canvas context
                                                page.render({
                                                    canvasContext: context,
                                                    viewport: viewport
                                                });
                                            });
                                        }
                                    });

                                    // Disable right-click on entire container
                                    container.addEventListener('contextmenu', function (e) {
                                        e.preventDefault();
                                    });
                                </script>

                                <script>
                                    document.getElementById('pdf-container').addEventListener('contextmenu', function (e) {
                                        e.preventDefault();
                                    });
                                </script>
                            </div>


                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>

</asp:Content>
