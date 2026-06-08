<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadAttendanceWrapper.aspx.cs" Inherits="NewPortal2023.ESS.DownloadAttendanceWrapper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download Wrapper</title>
</head>
<body>
    <%-- <script type="text/javascript">
        // Start download
        window.location.href = 'DownloadAttendance.aspx';

        // Notify parent page
        window.onload = function () {
            if (parent && typeof parent.iframeDownloadCompleted === 'function') {
                parent.iframeDownloadCompleted();
            }
        };
    </script>--%>

    <script type="text/javascript">
        var tempIframe = document.createElement("iframe");
        tempIframe.style.display = "none";
        tempIframe.src = "DownloadAttendance.aspx";
        document.body.appendChild(tempIframe);

        // Slight delay to simulate "preparing" before stopping progress
        setTimeout(function () {
            if (parent && typeof parent.iframeDownloadCompleted === 'function') {
                parent.iframeDownloadCompleted();
            }
        }, 1500);
    </script>

</body>
</html>
