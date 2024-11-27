$(document).ready(function() {
    employeeCodeAutoComplete()
})

function showSubmitMessage() {
    var isValid = ValidateRequire('WifiDeviceForm', 'WifiDeviceSummary')
    if (isValid) {
        $("[id$=panSubmitInfo]").stop().fadeIn(100)
        $("[id$=txtSubmitComment]").focus()
    }
}

function hideSubmitMessage() {
    $("[id$=panSubmitInfo]").stop().fadeOut(100)
}

function btnAddSub_Click(me) {
    //var $partialForm = $(me).parent().find(".add-edit-form")
    var $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    var $btnCancel = $partialForm.find(".btn-cancel-sub")
    $btnCancel.addClass("add-clicked").click()
    $partialForm.stop(true, true)
    $partialForm.find(".add-edit-action").text("Add")
    //            $partialForm.find(">*:not(.action-pan)").find(":text, select, textarea, :password, input[type='hidden']").val("")
    //            $partialForm.find(".total-amount").find(":text, input[type='hidden']").val("0")    
    $partialForm.slideDown(100)
    scrollTop = $partialForm.offset().top - 250
}

function btnCancelSub_Click(me) {
    var $partialForm = $(me).parent().parent().parent()
    $partialForm.find(".error-summary").html("")
    if ($(me).hasClass("add-clicked")) {
        $(me).removeClass("add-clicked")
        return
    }
    $partialForm.stop(true, true)
    $partialForm.slideUp(100, function() {
        $partialForm.find(".add-edit-action").text("")
        //                $partialForm.find(">*:not(.action-pan)").find(":text, select, textarea, :password, input[type='hidden']").val("")
        //                $partialForm.find(".total-amount").find(":text, input[type='hidden']").val("0")
    })
}

function employeeCodeAutoComplete() {

    $('.employee-code:not([readonly]):not([disabled])')
    .attr("autocomplete", "off")
    .devbridgeAutocomplete({
        lookup: _authorizedAccounts,
        minChars: 0,
        onSelect: function(suggestion) {
            var info = suggestion.data.split("-")
            $(this).val(info[0]);
            $("[id$=" + $(this).attr("data-hidden") + "]").val(suggestion.data)
            $("[id$=" + $(this).attr("data-button") + "]").click()
        },
        showNoSuggestionNotice: true,
        noSuggestionNotice: 'Not found',
        maxHeight: 150
    });
}

var scrollTop = 0
function ScrollDocument() {
    if (scrollTop && scrollTop > 0) {
        $("body, html").animate({ "scrollTop": scrollTop }, 300, "swing")
        scrollTop = 0
    }
}

function bindStartupEvents(me) {
    $(me).removeAttr("data-status")
    var id = $(me).attr("id")
    checkPostback(id)
}

function checkPostback(id) {
    if ($("#" + id).attr("data-status") == "done") {
        employeeCodeAutoComplete()
        SetNumberInputType()
        ScrollDocument()
        $(".error-summary").html("")
        return
    }
    setTimeout("checkPostback('" + id + "')", 10)
}

function clickDeleteButton(me) {
    var $partialForm = GetParent(me, ".HRTab").find(".add-edit-form")
    if ($partialForm.size() != 1) {
        $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    }
    var $btnCancel = $partialForm.find(".btn-cancel-sub")
    btnCancelSub_Click($btnCancel[0])
    $(me).find("+:submit").click()
}

function btnDeleteWifiDeviceClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this wifi device request?",
        OK: function() {
            $("[id$=hWifiDeviceID]").val($(me).parent().parent().find("td.id").text())
            clickDeleteButton(me)
        }
    })
}

function btnEditRequestClick(me) {
    $("[id$=hWifiDeviceID]").val($(me).parent().parent().find("td.id").text())
    var $partialForm = GetParent(me, ".HRTab").find(".add-edit-form")
    if ($partialForm.size() != 1) {
        $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    }
    $partialForm.find(".add-edit-action").text("Edit")
    $partialForm.stop(true, true)
    $partialForm.slideDown(100)
    scrollTop = $partialForm.offset().top - 250
}

function ValidateWifiForm(me) {
    var isValid = ValidateRequire('WifiDeviceForm', 'WifiDeviceSummary')
    return isValid
}

function HandlePartialMessageBoard(me, ctrl) {    
    $(me).removeAttr("data-status").removeAttr("data-process")
    CheckPartialMessageBoard($(me).attr("id"), $(ctrl).attr("id"))
}

var CheckPartialMessageBoardTimeout
function CheckPartialMessageBoard(id, ctrlID) {
    if ($("#" + id).attr("data-status") == "done") {
        var $partialForm = $("#" + ctrlID).parent().parent().parent()
        if ($("#" + id).attr("data-process") == "success") {
            $partialForm.slideUp(100, "linear", function() {
                //                $partialForm.find(">*:not(.action-pan)").find(":text, select, textarea, :password, input[type='hidden']").val("")
                //                $partialForm.find(".total-amount").find(":text, input[type='hidden']").val("0")
                $partialForm.find(".partial-message-board").remove()
            })
        }
        else {
            $partialForm.find(".partial-message-board").remove()
        }
        //
        CheckPartialMessageBoardTimeout = null
        return
    }
    CheckPartialMessageBoardTimeout = setTimeout("CheckPartialMessageBoard('" + id + "', '" + ctrlID + "')", 10)
}