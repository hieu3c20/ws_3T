$(document).ready(function() {
    InitAutoComplete()
    //       
    $("#oraErrorContainer").mouseup(function() {
        return false
    })
    $(document).mouseup(function() {
        hideErrorMessage()
    })
})

function InitAutoComplete() {
    $('.employee-code:not([readonly]):not([disabled])')
    .attr("autocomplete", "off")
    .devbridgeAutocomplete({
        lookup: _authorizedAccounts,
        minChars: 1,
        onSelect: function(suggestion) {
            var info = suggestion.data.split("-")
            $(this).val(info[0]);
            $("[id$=hRequester]").val(suggestion.data)
            $("[id$=btnGetRequesterInfo]").click()
        },
        showNoSuggestionNotice: true,
        noSuggestionNotice: 'Not found',
        maxHeight: 150
    });
    //
    $('.hr-budgetcode:not([readonly]):not([disabled])')
    .attr("autocomplete", "off")
    .devbridgeAutocomplete({
        lookup: _budgetCodes,
        minChars: 0,
        //        onSelect: function(suggestion) {
        //            var info = suggestion.data.split("-")
        //            $(this).val(info[0]);
        //            $("[id$=hRequester]").val(suggestion.data)
        //            $("[id$=btnGetRequesterInfo]").click()
        //        },
        showNoSuggestionNotice: true,
        noSuggestionNotice: 'Not found',
        maxHeight: 150
    });
}

var _y
function showErrorOraMessage(me) {
    var $this = $(me)
    var oraMessage = $this.attr("data-message")
    if (oraMessage && oraMessage.trim().length > 0) {
        var $oraErrorContainer = $("#oraErrorContainer")
        $("#oraErrorDetails").html($this.attr("data-message"))
        //        var sidebarWidth = $("#sidebar").outerWidth()
        //        var x = $this.offset().left - sidebarWidth - $oraErrorContainer.outerWidth() + 50
        var headerHeight = $("#header").outerHeight()
        _y = $this.offset().top - headerHeight - $oraErrorContainer.outerHeight() - 30
        //
        $oraErrorContainer.css({ top: _y + 10, opacity: 0, display: "block" })
        .stop(true, false).animate({ opacity: 1, top: _y }, 100, "linear")
    }
}

function hideErrorMessage() {
    $("#oraErrorContainer").stop(true, false)
    .animate({ opacity: 0, top: _y + 10 }, 100, "linear", function() {
        $("#oraErrorContainer").hide()
        $("#oraErrorDetails").html("")
    })
}

function clickDeleteButton(me) {
    //    var $partialForm = GetParent(me, ".HRTab").find(".add-edit-form")
    //    if ($partialForm.size() > 1) {
    //        $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    //    }
    var $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    var $btnCancel = $partialForm.find(".btn-cancel-sub")
    btnCancelSub_Click($btnCancel[0])
    $(me).find("+:submit").click()
}

function btnDeleteOtherAirTicketClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this air ticket request?",
        OK: function() {
            $("[id$=hRequestID]").val($(me).parent().parent().find("td.id").text()) //$(me).attr("data-id")
            clickDeleteButton(me)
        }
    })
}

function ConfirmApprove() {
    ShowConfirmMessage({
        message: "Are you sure to approve this air ticket request?",
        OK: function() {
            $("[id$=btnApprove]").click()
        }
    })
}

function ConfirmRecall() {
    ShowConfirmMessage({
        message: "Are you sure to recall this air ticket request?",
        OK: function() {
            $("[id$=btnRecall]").click()
        }
    })
}

function checkPostback(id) {
    if ($("#" + id).attr("data-status") == "done") {
        InitAutoComplete()
        SetNumberInputType()
        ScrollDocument()
        $(".error-summary").html("")
        return
    }
    setTimeout("checkPostback('" + id + "')", 10)
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

function btnAddSub_Click(me) {
    //var $partialForm = $(me).parent().parent().find(".add-edit-form")
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

function ValidateOtherAirTicketForm(me) {
    var isValid = ValidateRequire('OtherAirTicketForm', 'OtherAirTicketSummary')
    if (isValid) {
        $(me).parent().find("+:submit").click()
    }
}

function btnEditOtherRequestClick(me) {
    //$("[id$=hRequestID]").val($(me).attr("data-id"))
    $("[id$=hRequestID]").val($(me).parent().parent().find("td.id").text())
    //    var $partialForm = GetParent(me, ".HRTab").find(".add-edit-form")
    //    if ($partialForm.size() > 1) {
    //        $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    //    }
    var $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    $partialForm.find(".add-edit-action").text("Edit")
    $partialForm.stop(true, true)
    $partialForm.slideDown(100)
    scrollTop = $partialForm.offset().top - 250
}

function HandlePartialMessageBoard(me) {
    var $partialForm = $(me).parent().parent().parent()
    $partialForm.append(BuildPartialMessageBoard())
    $(me).removeAttr("data-status").removeAttr("data-process")
    CheckPartialMessageBoard($(me).attr("id"))
}

function CheckPartialMessageBoard(id) {
    if ($("#" + id).attr("data-status") == "done") {
        var $partialForm = $("#" + id).parent().parent().parent()
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
        return
    }
    setTimeout("CheckPartialMessageBoard('" + id + "')", 10)
}

function btnConfirmTransfer() {
    ShowConfirmMessage({
        message: "Are you sure to transfer all air tickets of this period and supplier?",
        OK: function() {
            $("[id$=btnTransferToOra]").click()
        }
    })
}

function checkApprove() {
    $("#approve-summary").html("")
    var isValid = true
    var $dteInvoiceDate = $('[id$=dteInvoiceDate_I]')
    var $invoiceDateContainer = $("[id$=dteInvoiceDate]")
    var invoiceDate = $dteInvoiceDate.val()
    var $dteGLDate = $('[id$=dteGLDate_I]')
    var $glDateContainer = $("[id$=dteGLDate]")
    var glDate = $dteGLDate.val()
    var $ddlBatchName = $('[id$=ddlBatchName]')
    var batchName = $ddlBatchName.val()
    //
    if (!invoiceDate || invoiceDate.trim().length === 0) {
        $("#approve-summary").append("<li style='color: red; list-style-type: circle; margin-left: 15px; padding: 0px 5px 5px 0px;'>Invoice Date is required!</li>")
        $invoiceDateContainer.addClass("validate-error")
        isValid = false
    }
    else {
        $invoiceDateContainer.removeClass("validate-error")
    }
    //
    if (!glDate || glDate.trim().length === 0) {
        $("#approve-summary").append("<li style='color: red; list-style-type: circle; margin-left: 15px; padding: 0px 5px 5px 0px;'>GL Date is required!</li>")
        $glDateContainer.addClass("validate-error")
        isValid = false
    }
    else {
        $glDateContainer.removeClass("validate-error")
    }
    //
    if (!batchName || batchName.trim().length === 0) {
        $("#approve-summary").append("<li style='color: red; list-style-type: circle; margin-left: 15px; padding: 0px 5px 5px 0px;'>Batch Name is required!</li>")
        $ddlBatchName.addClass("validate-error")
        isValid = false
    }
    else {
        $ddlBatchName.removeClass("validate-error")
    }
    return isValid
}

function showApproveMessage() {
    $.ajax({
        url: "/AjaxHandle.ashx?action=check-air-ticket-budget",
        async: true,
        type: "get",
        data: {
            period: $("[id$=hSAirPeriod]").val(),
            supplier: $("[id$=hSOraSupplier]").val()
        },
        cache: false,
        beforeSend: function() {
            $("[id$=UpdateProgress]").show()
        }
    }).done(function(response) {
        if (response == "session") {
            location.reload()
        } else {
            var arrResponse = response.split("|_|")
            var count = arrResponse[0]
            if (count) {
                ShowErrorMessage("There are some air tickets " + count + " that haven't checked budget yet!")
            } else if (arrResponse[1] == "no-mapping") {
                ShowErrorMessage("You do not have an mapping oracle account!")
            }
            else if (arrResponse[1] == "not-found") {
                ShowErrorMessage("Your mapping oracle account is not found!")
            }
            else if (arrResponse[1] == "expired") {
                ShowErrorMessage("Your mapping oracle account has disabled!")
            }
            else {
                //    var ddlAirPeriod = $("[id$=ddlSAirPeriod]")[0]
                //    var airPeriod = ddlAirPeriod.options[ddlAirPeriod.selectedIndex].text
                //    var ddlSupplier = $("[id$=ddlSOraSupplier]")[0]
                //    var supplier = ddlSupplier.options[ddlSupplier.selectedIndex].text
                var message = "Are you sure to transfer all air tickets of this period and supplier?"
                $("#approve-message").html(message)
                $("[id$=tabApproveMessage]").stop().fadeIn(100)
                $('[id$=dteGLDate_I]').focus()
            }
        }
    }).fail(function(xhr, text, status) {
        ShowErrorMessage(text)
    }).always(function() {
        $("[id$=UpdateProgress]").hide()
    })
}

function showConfirmConfirmBudget() {
    var isValid = ValidateRequire('OtherAirTicketForm', 'OtherAirTicketSummary')
    if (isValid) {
        ShowConfirmMessage({
            message: "Are you sure to confirm this budget code?",
            OK: function() {
                $("[id$=btnConfirmBudget]").click()
            }
        })
    }
}

function showConfirmRejectBudget() {
    var isValid = ValidateRequire('OtherAirTicketForm', 'OtherAirTicketSummary')
    if (isValid) {
        ShowConfirmMessage({
            message: "Are you sure to reject this budget code?",
            OK: function() {
                $("[id$=btnRejectBudget]").click()
            }
        })
    }
}

function hideApproveMessage() {
    $("[id$=tabApproveMessage]").stop().fadeOut(100)
}

function showRejectMessage() {
    $("[id$=panRejectInfo]").stop().fadeIn(100)
    $('[id$=txtRejectReason]').focus()
}

function hideRejectMessage() {
    $("[id$=panRejectInfo]").stop().fadeOut(100)
}

function checkReject() {
    var $txtRejectReason = $('[id$=txtRejectReason]')
    var rejectReason = $txtRejectReason.val()
    if (!rejectReason || rejectReason.trim().length === 0) {
        ShowErrorMessage("Reject recommendation is required!")
        $txtRejectReason.addClass("validate-error").focus()
        return false
    }
    else {
        $txtRejectReason.removeClass("validate-error")
        return true
    }
}

function showReConfirmGAMessage() {
    var $ddlAirPeriod = $("[id$=hSAirPeriod]")
    var $ddlOraSupplier = $("[id$=hSOraSupplier]")
    $.ajax({
        url: "/AjaxHandle.ashx?action=check-airticket-confirmbudget",
        async: true,
        type: "get",
        data: {
            airPeriod: $ddlAirPeriod.val(),
            supplier: $ddlOraSupplier.val()
        },
        cache: false,
        beforeSend: function() {
            $("[id$=UpdateProgress]").show()
        }
    }).done(function(response) {
        if (response == "session") {
            location.reload()
        } else {
            if (response == "all") {
                ShowErrorMessage("All air tickets have allready checked budget!")
            }
            else {
                $("[id$=panReconfirmBudget]").stop().fadeIn(100)
                $("[id$=txtReconfirmComment]").focus()
            }
        }
    }).fail(function(xhr, text, status) {
        ShowErrorMessage(text)
    }).always(function() {
        $("[id$=UpdateProgress]").hide()
    })
}

function hideReconfirmBudgetMessage() {
    $("[id$=panReconfirmBudget]").stop().fadeOut(100)
}

function showConfirmBudgetMessage() {
    var $ddlAirPeriod = $("[id$=hSAirPeriod]")
    var $ddlOraSupplier = $("[id$=hSOraSupplier]")
    $.ajax({
        url: "/AjaxHandle.ashx?action=check-airticket-confirmbudget",
        async: true,
        type: "get",
        data: {
            airPeriod: $ddlAirPeriod.val(),
            supplier: $ddlOraSupplier.val()
        },
        cache: false,
        beforeSend: function() {
            $("[id$=UpdateProgress]").show()
        }
    }).done(function(response) {
        if (response == "session") {
            location.reload()
        } else {
            if (response == "all") {
                $("[id$=panConfirmBudget]").stop().fadeIn(100)
                $("[id$=txtConfirmComment]").focus()
            }
            else {
                ShowErrorMessage("There are some air tickets that haven't checked budget yet!")
            }
        }
    }).fail(function(xhr, text, status) {
        ShowErrorMessage(text)
    }).always(function() {
        $("[id$=UpdateProgress]").hide()
    })
}

function hideConfirmBudgetMessage() {
    $("[id$=panConfirmBudget]").stop().fadeOut(100)
}

function showSubmitMessage() {
    var isValid = ValidateRequire('OtherAirTicketForm', 'OtherAirTicketSummary')
    if (isValid) {
        $("[id$=panSubmitInfo]").stop().fadeIn(100)
        $("[id$=txtSubmitComment]").focus()
    }
}

function hideSubmitMessage() {
    $("[id$=panSubmitInfo]").stop().fadeOut(100)
}

function ShowHistory() {
    var $pan = $("#BTStatusHistory")
    var $panContent = $("#BTStatusContent")
    $panContent.css({ "margin-top": 100 })
    $pan.fadeIn(100)
    $panContent.animate({ "margin-top": 150 }, 200, "linear")
    return false
}

function HideHistory() {
    var $pan = $("#BTStatusHistory")
    var $panContent = $("#BTStatusContent")
    $pan.fadeOut(100)
    $panContent.animate({ "margin-top": 100 }, 200, "linear")
    return false
}