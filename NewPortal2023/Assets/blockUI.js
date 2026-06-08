Sys.Application.add_init(function() {

    $.blockUI.defaults.theme = false;
    $.blockUI.defaults.css = {};
    $.blockUI.defaults.themedCSS = {};
    $.blockUI.defaults.themedCSS.width = 100;
    $.blockUI.defaults.themedCSS.height = 64;
    $.blockUI.defaults.overlayCSS = {};
    $.blockUI.defaults.overlayCSS.backgroundColor = '#000000';
    $.blockUI.defaults.overlayCSS.opacity = 0.3;
    

    // Get a reference to the PageRequestManager.
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    // Unblock the form when a partial postback ends.
    prm.add_endRequest(function() {
        $('#blockUI').unblock();
    });

    prm.add_initializeRequest(function() {
        $('#blockUI').block({ message: "<p align='center'><img src='Assets/progress.gif'></img><br>Please wait...</p>" });
    });
});

