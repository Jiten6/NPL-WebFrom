<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.master" AutoEventWireup="true" CodeBehind="NominationInstructions.aspx.cs" Inherits="NewPortal2023.ESS.NominationInstructions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="assets/js/dynamic_table_init.js"></script>
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
    </style>
    <style type="text/css">
        .underline {
            text-decoration: underline;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('[id*=txtPassportFrom]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
        $(function () {
            $('[id*=txtPassportTo]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr"
            });
        });
    </script>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading">
                            NominationInstructions
                        </header>
                        <div id="blockUI" class="panel-body">
                            <asp:ScriptManager ID="scm" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/Assets/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/Assets/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>
                            <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vwCreate" runat="server">
                                    <div class="panel-body">
                                        <div class="adv-table">
                                            <div id="" class="form-horizontal">
                                                <fieldset>
                                                    <div id="divAlertCreate" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                        <asp:Label ID="lblMessageCreate" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="form-group">
                                                        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <b><span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>DETAILED INSTRUCTIONS FOR <span class="GramE">
                    EMPLOYEES :</span></span></b><span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                        mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri;
                        color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>The following instructions are to be
                carefully noted by the employee while filling up the Consolidated Nomination Form.&nbsp;&nbsp;The
                Nomination Form which&nbsp;<span class="GramE">were</span>&nbsp;hitherto being filed
                separately for Government Provident Fund (PF), Gratuity, and Other Dues have been
                consolidated into one Nomination System.</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>1.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>All details are required to be filled.</span><span
                            style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman";
                            mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black;
                            mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>2.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>Filling of form should be on-line.&nbsp;Thereafter,
                        printouts should be taken and the form must be signed, dated and attested by two
                        witnesses. Submission of the duly signed/witnessed hard copy in original to the
                        respective HR Authority is mandatory.</span><span style='mso-bidi-font-size: 11.0pt;
                            mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                            mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; line-height: normal'>
            <span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman";
                mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black;
                mso-fareast-language: EN-IN'>
                <o:p>&nbsp;</o:p>
            </span>
        </p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>3.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>Once the Forms are duly completed in
                        all respects i.e. after obtaining witness signature etc., the completed CNF(Consolidated
                        Nomination Form) should be scanned and uploaded on the portal. The same will thereafter
                        be confirmed by the concerned HR functionary.</span><span style='mso-bidi-font-size: 11.0pt;
                            mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                            mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>5.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>Where an employee has a family at the
                        time of making the nomination, any nomination made by an employee should be in&nbsp;favour&nbsp;of
                        the members of the family only. “Family” has been defined:</span><span style='mso-bidi-font-size: 11.0pt;
                            mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                            mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 63.8pt; margin-bottom: .0001pt; text-align: justify; text-indent: -63.8pt;
            line-height: normal'>
            <b><span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                i.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;For the purpose of PF&nbsp;:</span></b><span style='font-size: 12.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: red; mso-fareast-language: EN-IN'>-&nbsp;&nbsp;<span style='mso-spacerun: yes'>
                    </span></span><span style='font-size: 12.0pt; font-family: "Times New Roman","serif";
                        mso-fareast-font-family: "Times New Roman"; color: black; mso-fareast-language: EN-IN'>
                        The rules applicable to <span class="SpellE">Govt</span> PF to be replicated</span><span
                            style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                            color: #0070C0; mso-fareast-language: EN-IN'> </span><s><span style='mso-bidi-font-size: 11.0pt;
                                mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                                mso-bidi-font-family: Calibri; color: red; mso-fareast-language: EN-IN'>
                                <o:p></o:p>
                            </span></s>
        </p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 27.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: 18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 63.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -63.0pt;
            line-height: normal'>
            <b><span style='font-size: 7.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></b><b><span
                    style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>ii.</span></b><b><span style='font-size: 7.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></b><b><span
                            style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                            color: black; mso-fareast-language: EN-IN'>For the purpose of Gratuity (under Payment
                            of Gratuity Act)</span></b><span style='font-size: 12.0pt; font-family: "Times New Roman","serif";
                                mso-fareast-font-family: "Times New Roman"; color: black; mso-fareast-language: EN-IN'>&nbsp;:-(Section
                                2 (h) of the Payment of Gratuity Act, 1972</span><span style='mso-bidi-font-size: 11.0pt;
                                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 63.8pt; margin-bottom: .0001pt; text-align: justify; text-indent: -63.8pt;
            line-height: normal'>
            <span style='font-size: 7.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span
                    style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>(h)&nbsp;&quot;family&quot; in relation
                    to an employee, shall be deemed to consist of—</span><span style='mso-bidi-font-size: 11.0pt;
                        mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                        mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 63.8pt; margin-bottom: .0001pt; text-align: justify; text-indent: -63.8pt;
            line-height: normal'>
            <span style='font-size: 7.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span
                    style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>(i)&nbsp;in the case of a male employee,
                    himself, his wife, his children, whether married or unmarried, his dependent parents
                    7 [and the dependent parents of his wife and the widow] and children of his predeceased
                    son, if any,</span><span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                        mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri;
                        color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 63.8pt; margin-bottom: .0001pt; text-align: justify; text-indent: -63.8pt;
            line-height: normal'>
            <span style='font-size: 7.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span
                    style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>(ii)&nbsp;in the case of a female employee,
                    herself, her husband, her children, whether married or unmarried, her dependent
                    parents and the dependent parents of her husband and the widow and children of her
                    predeceased son, if any;</span><span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                        mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri;
                        color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 63.8pt; margin-bottom: .0001pt; text-align: justify; text-indent: -63.8pt;
            line-height: normal'>
            <span style='font-size: 7.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span
                    style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>Explanation.—Where the personal law of
                    an employee permits the adoption by him of a child, any child lawfully adopted by
                    him shall be deemed to be included in his family, and where a child of an employee
                    has been adopted by another person and such adoption is, under the personal law
                    of the person making such adoption, lawful, such child shall be deemed to be excluded
                    from the family of the employee;</span><s><span style='mso-bidi-font-size: 11.0pt;
                        mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                        mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></s></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 63.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 63.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -63.0pt;
            line-height: normal'>
            <b><span style='font-size: 7.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></b><b><span
                    style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>iii.</span></b><b><span style='font-size: 7.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></b><span
                            style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                            color: black; mso-fareast-language: EN-IN'>&nbsp;<b>For all Other&nbsp;Dues :&nbsp;</b>-
                            Wife/Husband, Child/Children, Dependents, Brother/Sister.</span><span style='mso-bidi-font-size: 11.0pt;
                                mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                                mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>6.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>If an employee nominates more than one
                        person of his family, he shall (in each of his Nomination Form), specify the percentage
                        of share to each of the nominee/s so as to cover the entire amount standing to his
                        credit.</span><span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                            mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri;
                            color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>7.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>If at the time of making the nomination
                        the employee has no family, the nomination may be made for Gratuity in&nbsp;favour&nbsp;of
                        any person(s).&nbsp;&nbsp;Upon subsequently acquiring a family, the nomination shall
                        be deemed to be invalid and the employee shall make a fresh Nomination in&nbsp;favour&nbsp;of
                        one or more person(s) belonging to his family, keeping in mind the definition of
                        family, given above.</span><span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                            mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri;
                            color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>8.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>The nomination made by the employee may
                        at any time be modified by submitting a fresh nomination bearing in mind the above
                        conditions.&nbsp;&nbsp;A nomination is valid till it is revoked or a fresh nomination
                        is made.</span><span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                            mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri;
                            color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman";
                mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black;
                mso-fareast-language: EN-IN'>
                <o:p>&nbsp;</o:p>
            </span>
        </p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>10.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>If a Nominee/s predeceases an employee
                        i.e. in case of demise of nominee/s while employee is service, the interest of the
                        Nominee/s shall revert to the employee who may thereupon make a fresh nomination
                        in respect of such interest. The nomination or modification thereto shall take effect
                        to the extent that it is valid on the date on which it is received by the Company.
                        In the absence of fresh nomination, the other nominee/s shall receive the benefits.
                        If there are no Primary Nominees, the Contingent Nominees will receive.</span><span
                            style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman";
                            mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black;
                            mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>11.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>If a nominee/s dies along with the employee
                        or before receiving the benefits, the Company shall pay to the surviving primary
                        nominee(s), if any.</span><span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                            mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri;
                            color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>12.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>The employee can take a copy of his nomination
                        and preserve it carefully at home for the benefit of the nominees/beneficiaries.</span><span
                            style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman";
                            mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black;
                            mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>13.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>The employee agrees, by making this nomination,
                        that the Company (HRRL) shall stand discharged on making payment as per the nomination.</span><span
                            style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman";
                            mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black;
                            mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>14.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>It may please be noted that employees
                        have to make separate physical nominations for Employees’ Pension Scheme(EPS-95)
                        etc.</span><span style='mso-bidi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                            mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri;
                            color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-bottom: 0cm; margin-bottom: .0001pt; text-align: justify;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>&nbsp;</span><span style='mso-bidi-font-size: 11.0pt;
                    mso-ascii-font-family: Calibri; mso-fareast-font-family: "Times New Roman"; mso-hansi-font-family: Calibri;
                    mso-bidi-font-family: Calibri; color: black; mso-fareast-language: EN-IN'><o:p></o:p></span></p>
        <p class="MsoNormal" style='margin-top: 0cm; margin-right: 0cm; margin-bottom: 0cm;
            margin-left: 18.0pt; margin-bottom: .0001pt; text-align: justify; text-indent: -18.0pt;
            line-height: normal'>
            <span style='font-size: 12.0pt; font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                color: black; mso-fareast-language: EN-IN'>15.</span><span style='font-size: 7.0pt;
                    font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                    color: black; mso-fareast-language: EN-IN'>&nbsp;&nbsp;</span><span style='font-size: 12.0pt;
                        font-family: "Times New Roman","serif"; mso-fareast-font-family: "Times New Roman";
                        color: black; mso-fareast-language: EN-IN'>The employee understands and agrees to
                        the above terms and conditions while making the nomination.</span><span style='font-size: 8.0pt;
                            mso-bidi-font-size: 7.0pt; font-family: "Arial","sans-serif"; mso-fareast-font-family: "Times New Roman";
                            mso-bidi-font-family: Mangal; display: none; mso-hide: all; mso-fareast-language: EN-IN'>
                            <o:p></o:p>
                        </span>
        </p>
        <p class="MsoNormal">
            <o:p>&nbsp;</o:p>
        </p>
        <div style="text-align:center;">
            <asp:Button ID="btnAgree" runat="server" CssClass="btn btn-success"  Text="I Agree" 
                onclick="btnAgree_Click" /></div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>

