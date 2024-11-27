$(document).ready(function() {
    employeeCodeAutoComplete()
})

function ShowReportCondition(popupID, popupTitle, reportType, showClass) {
    $("[id$=hReportType]").val(reportType)
    $("[id$=hShowClass]").val(showClass)
    $("[id$=hReportTitle]").val(popupTitle)
    //
    var divContentID = popupID + "Content"
    var $div = $("[id$=" + popupID + "]")
    var $divContent = $("#" + divContentID)
    //
    $divContent.find(".default-hide").hide()
    if (showClass) {
        $divContent.find("." + showClass).show()
    }
    //
    $divContent.find(".report-legend").text(popupTitle)
    $("body").css("overflow", "hidden")
    $divContent.css({ "margin-top": 50 * 2 / 3, opacity: 0 }).animate({ "margin-top": 50, opacity: 1 }, messageSpeed, "linear")
    $div.fadeIn(messageSpeed)
    return false
}

function HideReportCondition() {
    //    $(".report-condition-content").animate({ "margin-top": 50 * 2 / 3, opacity: 0 }, messageSpeed + 100, "linear", function() {
    //        $("body").removeAttr("style")
    //    })
    //    $(".report-condition").fadeOut(messageSpeed + 100)
    $("body").removeAttr("style")
}

function employeeCodeAutoComplete() {
    $('.employee-code:not([readonly]):not([disabled])')
    .attr("autocomplete", "off")
    .devbridgeAutocomplete({
        lookup: _authorizedAccounts,
        minChars: 1,
        onSelect: function(suggestion) {
            $(this).val(suggestion.data);
        },
        showNoSuggestionNotice: true,
        noSuggestionNotice: 'Not found',
        maxHeight: 150
    });
}

function checkPostback(id) {
    if ($("#" + id).attr("data-status") == "done") {
        employeeCodeAutoComplete()
        SetOriginState()
        return
    }
    setTimeout("checkPostback('" + id + "')", 10)
}

function bindStartupEvents() {
    var $btn = $("[id$=btnMessage]")
    $btn.removeAttr("data-status")
    HandleMessage($btn[0])
    checkPostback($btn.attr("id"))
}

function CheckExportFile(id) {
    var $btn = $("#" + id)
    if ($btn.attr("data-status") == "done") {
        var filePath = $btn.attr("data-file-path")
        if (filePath && filePath.trim().length > 0) {
            location.href = filePath
        }
        return
    }
    setTimeout("CheckExportFile('" + id + "')", 10)
}

function GetExportFile() {
    var $btn = $("[id$=btnMessage]")
    $btn.removeAttr("data-status").removeAttr("data-file-path")
    CheckExportFile($btn.attr("id"))
}

function SetOriginState() {
    var $divContent = $(".report-popup")
    //
    $divContent.find(".default-hide").hide()
    var showClass = $("[id$=hShowClass]").val()
    if (showClass) {
        $divContent.find("." + showClass).show()
    }
    $divContent.find(".report-legend").text($("[id$=hReportTitle]").val())
}

function BTTypeChange() {
    var $ddl = $("[id$=ddlBTType]")
    $ddl.removeAttr("data-status")
    CheckBTType($ddl.attr("id"))
}

function CheckBTType(id) {
    var $ddl = $("#" + id)
    if ($ddl.attr("data-status") == "done") {    
        $("#lnkWifi").click()
        return
    }
    setTimeout("CheckBTType('" + id + "')", 10)
}