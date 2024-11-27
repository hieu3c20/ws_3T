$(document).ready(function() {
    employeeCodeAutoComplete()
    //
    BindCalculateRequestDetails()
    //    
    $("#btnSelectBTTypeOK").click(function() {
        if (!PreValidate()) {
            return
        }
        $.ajax({
            url: "/AjaxHandle.ashx?action=check-employee-code",
            async: true,
            type: "get",
            data: { code: $("[id$=txtSelectEmployeeCode]").val().split("-")[0].trim() },
            beforeSend: function() {
                $("[id$=UpdateProgress]").show()
            }
        }).done(function(response) {
            if (response == "session") {
                location.reload()
            }
            else if (response == "invalid") {
                $("[id$=txtSelectEmployeeCode]").addClass("validate-error")
                $("#ulPreErrorMessage").append("<li>Employee code's invalid</li>")
            }
            else if (response == "exist") {
                $("#next-container").find("[id$=btnAdd]").click()
                HideSelectBTType()
            }
            else if (response == "null") {
                $("[id$=txtSelectEmployeeCode]").addClass("validate-error")
                $("#ulPreErrorMessage").append("<li>Employee code's not found</li>")
            }
            else {
                $("#ulPreErrorMessage").append("<li>" + response + "</li>")
            }
        }).fail(function(xhr, text, status) {
            ShowErrorMessage(text)
        }).always(function() {
            $("[id$=UpdateProgress]").hide()
        })
    })
    //
    $("#btnSelectBTTypeCancel").click(function() {
        HideSelectBTType()
    })
    //
    $("#btnAddConfirm").click(function() {
        $("[id$=txtSelectEmployeeCode]").val($("[id$=txtSelectEmployeeCode]").attr("data-code"))
        $("#ulPreErrorMessage").html("")
        $("#panSelectBTInfo .validate-error").removeClass("validate-error")
        ShowSelectBTType()
    })
    //
    $("#btnCopyConfirm").click(function() {
        var tabIndex = $(".HRTabControl .HRTabNav li.current").index()
        var $tabContainer = $(".HRTabControl .HRTabList .HRTab:eq(" + tabIndex + ")")
        if ($tabContainer.find(".chkSelect:checked").size() != 1) {
            ShowErrorMessage("Please choose 1 record!")
            return
        }
        showChooseEmployee()
    })
    //
    $("#btnDeleteConfirm").click(function() {
        var tabIndex = $(".HRTabControl .HRTabNav li.current").index()
        var $tabContainer = $(".HRTabControl .HRTabList .HRTab:eq(" + tabIndex + ")")
        if (!$tabContainer.find(".chkSelect:checked").size()) {
            ShowErrorMessage("You must choose at least 1 record. Try again!")
            return
        }
        ShowConfirmMessage({
            message: "Are you sure to delete selected BT registers and relate information?",
            OK: function() {
                var users = ""
                $tabContainer.find(".chkSelect:checked").each(function() {
                    users = "," + $(this).parent().parent().find("td.id").text() + users
                })
                if (users.trim().length > 0) {
                    users = users.substr(1)
                }
                $("[id$=hDeleteUsers]").val(users)
                $("#next-container").find("[id$=btnDelete]").click()
            }
        })
    })
    //    
    $("#oraErrorContainer").mouseup(function() {
        return false
    })
    $(document).mouseup(function() {
        hideErrorMessage()
    })
})

var _y
function showErrorOraMessage(me) {
    var $this = $(me)
    var oraMessage = $this.attr("data-message")
    if (oraMessage && oraMessage.trim().length > 0) {
        var $oraErrorContainer = $("#oraErrorContainer")
        $("#oraErrorDetails").html($this.attr("data-message"))
        var sidebarWidth = $("#sidebar").outerWidth()
        var x = $this.offset().left - sidebarWidth - $oraErrorContainer.outerWidth() + 50
        var headerHeight = $("#header").outerHeight()
        _y = $this.offset().top - headerHeight - $oraErrorContainer.outerHeight() - 30
        $oraErrorContainer.css({ top: _y + 10, left: x, opacity: 0, display: "block" })
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

function PreValidate() {
    var isValid = true
    var $msgContainer = $("#ulPreErrorMessage")
    $msgContainer.html("")
    var $empCode = $("[id$=txtSelectEmployeeCode]")
    var empCode = $empCode.val().trim()
    var $btType = $("[id$=ddlSelectBTType]")
    var btType = $btType.val()
    var $budgetCode = $("[id$=ddlSelectBudgetName]")
    var budgetCode = $budgetCode.val()
    var $projectBudgetCode = $("[id$=txtSelectProjectBudgetCode]")
    var projectBudgetCode = $projectBudgetCode.val().trim()
    //    var $country = $("[id$=ddlSelectDestinationCountry]")
    //    var country = $country.val()
    if (!empCode) {
        isValid = false
        $empCode.addClass("validate-error")
        $msgContainer.append("<li>Employee code is required</li>")
    }
    else {
        $empCode.removeClass("validate-error")
    }
    //
    if (!btType) {
        isValid = false
        $btType.addClass("validate-error")
        $msgContainer.append("<li>Business trip type is required</li>")
    }
    else {
        $btType.removeClass("validate-error")
    }
    //
    if (!budgetCode && !projectBudgetCode) {
        isValid = false
        $budgetCode.addClass("validate-error")
        $projectBudgetCode.addClass("validate-error")
        $msgContainer.append("<li>Budget or Project Budget is required</li>")
    }
    else {
        $budgetCode.removeClass("validate-error")
        $projectBudgetCode.removeClass("validate-error")
    }
    //
    //    if (!country) {
    //        isValid = false
    //        $country.addClass("validate-error")
    //        $msgContainer.append("<li>Country is required</li>")
    //    }
    //    else {
    //        $msgContainer.removeClass("validate-error")
    //    }
    return isValid
}


function ShowSelectBTType() {
    $("body").css("overflow", "hidden")
    $("#panSelectBTInfoContent").css({ "margin-top": boardContentTop * 2 / 3, opacity: 0 })
    $("#panSelectBTInfo").fadeIn(messageSpeed)
    $("#panSelectBTInfoContent").animate({ "margin-top": boardContentTop, opacity: 1 }, messageSpeed, "linear")
}

function HideSelectBTType() {
    $("#panSelectBTInfoContent").animate({ "margin-top": boardContentTop * 2 / 3, opacity: 0 }, messageSpeed + 100, "linear", function() {
        $("body").removeAttr("style")
    })
    $("#panSelectBTInfo").fadeOut(messageSpeed + 100)
}

function employeeCodeAutoComplete() {

    $('.employee-code:not([readonly]):not([disabled])')
    .attr("autocomplete", "off")
    .devbridgeAutocomplete({
        lookup: _authorizedAccounts,
        minChars: 0,
        onSelect: function(suggestion) {
            $(this).val(suggestion.data);
        },
        showNoSuggestionNotice: true,
        noSuggestionNotice: 'Not found',
        maxHeight: 150
    });

    $('.select-employee-code:not([readonly]):not([disabled])')
    .attr("autocomplete", "off")
    .devbridgeAutocomplete({
        lookup: _selectAuthorizedAccounts,
        minChars: 0,
        onSelect: function(suggestion) {
            $(this).val(suggestion.data);
            var $chk = $("[id$=chkSelectBudgetAll]")
            $("[id$=hSelectBudgetByEmp]").val("Y")
            $chk.click()
        },
        showNoSuggestionNotice: true,
        noSuggestionNotice: 'Not found',
        maxHeight: 150
    });
}

var checkPostbackTimeout
function checkPostback(id) {
    if ($("#" + id).attr("data-status") == "done") {
        ScrollDocument()
        ShowMovingTimeAllowance()
        LoadExpenseNorm()
        CalculateRequest(true)
        DoCalculateForm()
        chkCommonCCAmountChanged(true)
        chkOtherCCAmountChanged(true)
        SetNumberInputType()
        employeeCodeAutoComplete()
        BindCalculateRequestDetails()
        BindCurrencyInformation()
        BindGetExrateEvent()
        $(".error-summary").html("")
        checkPostbackTimeout = null
        return
    }
    checkPostbackTimeout = setTimeout("checkPostback('" + id + "')", 10)
}

var scrollTop = 0
function ScrollDocument() {
    if (scrollTop && scrollTop > 0) {
        $("body, html").animate({ "scrollTop": scrollTop }, 300, "swing")
        scrollTop = 0
    }
}

function BindCurrencyInformation() {
    var ddlAirCurrency = $("[id$=ddlAirCurrency]")[0]
    var ddlAirCurrencyOption = ddlAirCurrency.options[ddlAirCurrency.selectedIndex]
    $("[id$=lblAirExrate]").html("(" + (ddlAirCurrencyOption ? ddlAirCurrencyOption.text : '') + " &rarr; VND)")
    $("[id$=ddlAirCurrency]").change(function() {
        $("[id$=lblAirExrate]").html("(" + $(this)[0].options[$(this)[0].selectedIndex].text + " &rarr; VND)")
    })
}

function bindStartupEvents(me) {
    $(me).removeAttr("data-status")
    var id = $(me).attr("id")
    checkPostback(id)
}

function BindCalculateRequestDetails() {
    $(".tblRequestDetails .amount .dxeSBC, .tblRequestDetails .quantity .dxeSBC").click(function() {
        $(this).prev().find(":text").focus()
        CalculateRequestDetails($(this).prev().find(":text"), false)
    })
    $(".tblRequestDetails .quantity :text").keyup(function() {
        CalculateRequestDetails($(this), false)
    }).blur(function() {
        CalculateRequestDetails($(this), false)
    })
    $(".tblRequestDetails .amount :text").keyup(function() {
        CalculateRequestDetails($(this), false)
    }).blur(function() {
        CalculateRequestDetails($(this), false)
    })
    //
    $(".estimate-transportation-fee .dxeSBC").click(function() {
        $(this).prev().find(":text").focus()
        CalculateTransportation(this)
    })
    $(".estimate-transportation-fee :text").keyup(function() {
        CalculateTransportation(this)
    }).blur(function() {
        CalculateTransportation(this)
    })
    //
    $(".motobike-qty .dxeSBC").click(function() {
        $(this).prev().find(":text").focus()
        CalculateMotobike()
    })
    $(".motobike-qty :text").keyup(function() {
        CalculateMotobike()
    }).blur(function() {
        CalculateMotobike()
    })
    //
    $(".expense-other-amount :text").keyup(function() {
        chkOtherCCAmountChanged(true)
    }).blur(function() {
        chkOtherCCAmountChanged(true)
    })
    //
    $(".inv-cost .dxeSBC, .inv-vat .dxeSBC").click(function() {
        $(this).prev().find(":text").focus()
        CalculateInvoice()
    })
    $(".inv-cost :text, .inv-vat :text").keyup(function() {
        CalculateInvoice()
    }).blur(function() {
        CalculateInvoice()
    })
    //    
    $(".air-payment .dxeSBC").click(function() {
        $(this).prev().find(":text").focus()
        CalculateAirTicket()
    })
    $(".air-payment :text").keyup(function() {
        CalculateAirTicket()
    }).blur(function() {
        CalculateAirTicket()
    })
}

function CalculateRequestDetails($me, afterChange) {
    var $td = $me.parent().parent().parent().parent()
    if (!$td.is("td")) {
        $td = $td.parent()
    }
    var $detailsParent = $td.parent().parent()
    var index = $td.index()
    var quantityText = $(".quantity > td:eq(" + index + ") :text", $detailsParent).val()
    if (quantityText) {
        quantityText = quantityText.replace(/,/g, "")
    }
    var quantity = parseInt(quantityText)
    var amount = 0
    if (!afterChange) {
        var amountText = $(".amount > td:eq(" + index + ") :text", $detailsParent).val()
        if (amountText) {
            amountText = amountText.replace(/,/g, "")
        }
        amount = parseFloat(amountText)
    }
    else {
        amount = window[$(".amount > td:eq(" + index + ") :text", $detailsParent).attr("id").replace("_I", "")].GetNumber()
    }
    var calAmount = quantity * amount
    //$("#tblRequestDetails .total-amount > td:eq("+index+") :text").val(isNaN(calAmount) ? "0" : calAmount.toString())
    var $totalAmount = $(".total-amount > td:eq(" + index + ")", $detailsParent)
    //    if ($totalAmount.hasClass("other-total-amount")) {
    //        if (calAmount > 0) {
    //            $(".other-explaination").show()
    //        }
    //        else {
    //            $(".other-explaination").hide()
    //        }
    //    }
    window[$totalAmount.find(":text").attr("id").replace("_I", "")].SetNumber(isNaN(calAmount) ? 0 : calAmount)
    var totalAmount = 0
    $(".total-amount > td:not('.cal-total-amount'):gt(0)", $detailsParent).each(function() {
        var totalAmountText = $(this).find(":text").val()
        if (totalAmountText) {
            totalAmountText = totalAmountText.replace(/,/g, "")
        }
        totalAmount += parseFloat(totalAmountText)
        //totalAmount += window[$(this).find(":text").attr("id").replace("_I", "")].GetValue()
    })
    //$("#total-amount :text").val(isNaN(totalAmount) ? "0" : totalAmount.toString())
    window[$(".cal-total-amount :text", $detailsParent).attr("id").replace("_I", "")].SetNumber(isNaN(totalAmount) ? 0 : totalAmount)
    chkCommonCCAmountChanged(true)
}

function HandlePartialMessageBoard(me) {
    var $partialForm = $(me).parent().parent().parent()
    $partialForm.append(BuildPartialMessageBoard())
    $(me).removeAttr("data-status").removeAttr("data-process")
    CheckPartialMessageBoard($(me).attr("id"))
}

var CheckPartialMessageBoardTimeout
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
        //
        CheckPartialMessageBoardTimeout = null
        return
    }
    CheckPartialMessageBoardTimeout = setTimeout("CheckPartialMessageBoard('" + id + "')", 10)
}

function btnBack_Click() {
    $("#totalErrorSummary").html("").hide()
    $("#back-container, .currency").fadeOut(300)
    $("#panRegister").slideUp(300, "linear", function() {
        //
        $(".ui-widget-content:not(.default-hide)").show()
        $(".ui-widget-content:not(.default-hide)").prev().addClass("active")
        $(".default-hide").hide()
        $(".default-hide").prev().removeClass("active")
        //
        $("#panGetInfo").slideDown(300, "linear")
        $("#next-container").fadeIn(300)
        $("#general-title").text("Search Condition")
        submitClicked = false
    })
}

function btnAdd_Click() {
    DoCheckEnableForm()
    //
    $("#next-container").fadeOut(300)
    $(".currency").fadeIn(300)
    //
    $(".HRTabControl.edit-form .HRTabNav li").removeClass("current")
    $(".HRTabControl.edit-form .HRTabNav li:first-child").addClass("current")
    $(".HRTabControl.edit-form .HRTab").hide()
    $(".HRTabControl.edit-form .HRTab:first-child").show()
    //
    $(".ui-widget-content:not(.default-hide)").show()
    $(".ui-widget-content:not(.default-hide)").prev().addClass("active")
    $(".default-hide").hide()
    $(".default-hide").prev().removeClass("active")
    //    
    $("#panGetInfo").slideUp(300, "linear", function() {
        $("#panRegister").slideDown(300, "linear")
        $("#back-container").fadeIn(300)
        $(".add-edit-form").hide()
        $("#general-title").text("Traveller Information")
    })
}

function btnEditBTClick(me) {
    var $tr = $(me).parent().parent()
    $("[id$=hID]").val($tr.find("td.id").text())//$(me).attr("data-id")
    btnAdd_Click()
}

function DoCheckEnableForm() {
    $("[id$=btnCancel]").removeAttr("enable-form").removeAttr("enable-invoice-form")
    CheckEnableForm()
}

var CheckEnableFormTimeout
function CheckEnableForm() {
    var done = true
    var enable = $("[id$=btnCancel]").attr("enable-form")

    if (enable) {
        if (enable == "true") {
            $(".add-btn").show()
            $(".add-btn.hidden").css("visibility", "visible")
        }
        else {
            $(".add-btn").hide()
            $(".add-btn.hidden").show().css("visibility", "hidden")
        }
    }
    else {
        done = false
    }
    //invoice form
    var enableInv = $("[id$=btnCancel]").attr("enable-invoice-form")
    if (enableInv) {
        if (enableInv == "true") {
            $(".add-btn.add-btn-invoice").show()
        }
        else {
            $(".add-btn.add-btn-invoice").hide()
        }
    }
    else {
        done = false
    }
    //
    //air form
    var enableAir = $("[id$=btnCancel]").attr("enable-air-form")
    if (enableAir) {
        if (enableAir == "true") {
            $(".add-btn.add-btn-air").show()
        }
        else {
            $(".add-btn.add-btn-air").hide()
        }
    }
    else {
        done = false
    }
    //
    if (!done) {
        CheckEnableFormTimeout = setTimeout("CheckEnableForm()", 10)
    }
    else {
        CheckEnableFormTimeout = null
    }
}

function btnDeleteBTClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this BT register and relate information?",
        OK: function() {
            $tr = $(me).parent().parent()
            $("[id$=hID]").val($tr.find("td.id").text()) //$(me).attr("data-id")
            $(me).find("+:submit").click()
        }
    })
}

function btnEditRequestClick(me) {
    var $partialForm = GetParent(me, ".HRTab").find(".add-edit-form")
    if ($partialForm.size() > 1) {
        $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    }
    $partialForm.find(".add-edit-action").text("Edit")
    $partialForm.stop(true, true)
    $partialForm.slideDown(100)
    scrollTop = $partialForm.offset().top - 250
}

function clickDeleteButton(me) {
    var $partialForm = GetParent(me, ".HRTab").find(".add-edit-form")
    if ($partialForm.size() > 1) {
        $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    }
    var $btnCancel = $partialForm.find(".btn-cancel-sub")
    btnCancelSub_Click($btnCancel[0])
    $(me).find("+:submit").click()
}

function btnDeleteInvoiceClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this invoice?",
        OK: function() {
            clickDeleteButton(me)
        }
    })
}

function btnDeleteAirTicketClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this air ticket?",
        OK: function() {
            clickDeleteButton(me)
        }
    })
}

function btnDeleteRequestClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this payment request?",
        OK: function() {
            clickDeleteButton(me)
        }
    })
}

function btnAddSub_Click(me) {
    var $partialForm = $(me).parent().find(".add-edit-form")
    var $btnCancel = $partialForm.find(".btn-cancel-sub")
    $btnCancel.addClass("add-clicked").click()
    $partialForm.stop(true, true)
    $partialForm.find(".add-edit-action").text("Add")
    //            $partialForm.find(">*:not(.action-pan)").find(":text, select, textarea, :password, input[type='hidden']").val("")
    //            $partialForm.find(".total-amount").find(":text, input[type='hidden']").val("0")
    $partialForm.slideDown(100, function() {
        if (needScroll) {
            needScroll = false
            ScrollDocument()
        }
    })
    scrollTop = $partialForm.offset().top - 250
}

function btnCancelSub_Click(me) {
    var $partialForm = $(me).parent().parent().parent()
    $partialForm.find(".error-summary").html("")
    if ($(me).hasClass("add-clicked")) {
        $(me).removeClass("add-clicked")
        ClearRequestForm(true)
        return
    }
    $partialForm.stop(true, true)
    $partialForm.slideUp(100, function() {
        $partialForm.find(".add-edit-action").text("")
        //                $partialForm.find(">*:not(.action-pan)").find(":text, select, textarea, :password, input[type='hidden']").val("")
        //                $partialForm.find(".total-amount").find(":text, input[type='hidden']").val("0")
    })
    //
    ClearRequestForm()
}

function ChooseFile(me) {
    if ($(me).hasClass("uploading")) {
        return
    }
    var choosedFiles = me.files
    if (choosedFiles.length == 0) {
        return
    }
    var data = new FormData()
    data.append("btID", $("[id$=hID]").val())
    data.append("type", $(me).attr("data-type"))
    for (i = 0; i < choosedFiles.length; i++) {
        var regex = /^[0-9a-zA-Z_ \[\]\(\)\.]+$/
        if (!regex.test(choosedFiles[i].name)) {
            $(me).val("")
            ShowErrorMessage("File name is invalid.")
            return
        }
        data.append("file" + i, choosedFiles[i])
    }
    $(me).addClass("uploading")
    var chooseText = $(me).prev().text()
    $.ajax({
        url: "/AjaxHandle.ashx?action=attach",
        async: true,
        type: "post",
        data: data,
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function() {
            $(me).prev().text("Uploading...")
        }
    }).done(function(response) {
        if (response == "session") {
            location.reload()
        } else {
            try {
                var responseObj = eval("(" + response + ")")
                if (responseObj.message == "success") {
                    $(me).parent().parent().prev().find("div.pan-attachment-container").html(responseObj.files)
                }
                else {
                    ShowErrorMessage(responseObj.message)
                }
            }
            catch (e) {
                ShowErrorMessage(e.message)
            }
        }
    }).fail(function(xhr, text, status) {
        ShowErrorMessage(text)
    }).always(function() {
        $(me).val("")
        $(me).prev().text(chooseText)
        $(me).removeClass("uploading")
        ValidateSubmit()
    })
}

function DeleteAttachment(me) {
    var id = $(me).attr("data-id")
    $(me).parent().addClass("selected")
    ShowConfirmMessage({
        message: "Are you sure to delete this attachment file?",
        OK: function() {
            $("[id$=UpdateProgress]").show()
            $.ajax({
                url: '/AjaxHandle.ashx?action=delete-attachment',
                async: true,
                type: 'get',
                data: { id: id }
            }).done(function(response) {
                if (response == "session") {
                    location.reload()
                }
                else if (response == "success") {
                    ShowInfoMessage("Your data is deleted successfully!")
                    var $ol = $(me).parent().parent()
                    $(me).parent().fadeOut("fast", function() {
                        $(this).remove()
                        ValidateSubmit()
                        if (!$ol.find("li").size()) {
                            //$ol.parent().parent().parent().next().find(".full-fill").val("")
                            $ol.remove()
                        }
                    })
                }
                else {
                    ShowErrorMessage(response)
                }
            }).fail(function(xhr, text, status) {
                ShowErrorMessage(text)
            }).always(function() {
                $("[id$=UpdateProgress]").hide()
            })
        },
        Cancel: function() {
            $(me).parent().removeClass("selected")
        }
    })
}

function BudgetCodeChange(me, nameID) {
    var budgetCode = $(me).val()
    if (budgetCode) {
        budgetCode = budgetCode.split("-")[1]
    }
    $("[id$=" + nameID + "]").val(budgetCode)
    ValidateSubmit()
}

function ValidateTransportationForm(me) {
    var isValid = ValidateRequire('TransportationForm', 'TransportationSummary')
    if (isValid) {
        $(me).parent().find("+:submit").click()
    }
}

function ValidateInvoiceForm(me) {
    var isValid = ValidateRequire('InvoiceForm', 'InvoiceSummary')
    if (isValid) {
        $(me).parent().find("+:submit").click()
    }
}

function ValidateAirTicketForm(me) {
    var isValid = ValidateRequire('AirTicketForm', 'AirTicketSummary')
    if (isValid) {
        $(me).parent().find("+:submit").click()
    }
}

var isValidBT

function ValidateRejectBudget() {
    var $summary = $("#totalErrorSummary")
    $summary.html("").hide()
    isValidBT = true
    ValidateRequireSingle('ddlBudgetName', "Budget is required")
    if (!isValidBT) {
        $("[id$=UpdateProgress]").hide()
        $summary.show()
        $("body, html").animate({ "scrollTop": $summary.offset().top }, 300, "swing")
    }
    else {
        var oldBudget = $("[id$=hOldBudget]").val()
        var ddlBudgetName = $("[id$=ddlBudgetName]")[0]
        var budget = $("[id$=txtBudgetCode]").val() + "/" + ddlBudgetName.options[ddlBudgetName.options.selectedIndex].text
        if (oldBudget == budget) {
            ShowErrorMessage("Budget has not been changed!")
            isValidBT = false
        }
    }
    return isValidBT
}

function ValidateBudget() {
    $("[id$=UpdateProgress]").show()
    var $summary = $("#totalErrorSummary")
    $summary.html("")
    isValidBT = true
    ValidateRequireSingle('ddlBudgetName', "Budget is required")
    if (!isValidBT) {
        $("[id$=UpdateProgress]").hide()
        $summary.show()
        $("body, html").animate({ "scrollTop": $summary.offset().top }, 300, "swing")
    }
    else {
        $summary.hide()
        var budgetCode = $("[id$=txtBudgetCode]").val()
        var budgetID = $("[id$=ddlBudgetName]").val().split("-")[0]
        if (!budgetCode || budgetCode.trim().length === 0) {
            $("[id$=UpdateProgress]").hide()
            showSubmitMessage()
            return
        }
        var isOverBudget = false
        $.ajax({
            url: "/AjaxHandle.ashx?action=check-expense-budget",
            async: true,
            type: "get",
            data: { btID: $("[id$=hID]").val(), budgetCode: budgetID },
            cache: false,
            beforeSend: function() {
                $("[id$=UpdateProgress]").show()
            }
        }).done(function(response) {
            if (response == "session") {
                location.reload()
            } else {
                var budgetRemaining = parseFloat(response)
                if (!isNaN(budgetRemaining)) {
                    if (budgetRemaining < 0) {
                        isOverBudget = true
                    }
                    //DoCalculateForm()
                }
                else {
                    ShowErrorMessage(response)
                }
            }
        }).fail(function(xhr, text, status) {
            ShowErrorMessage(text)
        }).always(function() {
            $("[id$=UpdateProgress]").hide()
            if (isOverBudget) {
                ShowConfirmMessage({
                    message: "It seem to be Over Budget! Do you want to continue?",
                    OK: function() {
                        showConfirmBudgetMessage()
                    }
                })
            }
            else {
                showConfirmBudgetMessage()
            }
        })
    }
}

function showConfirmBudgetMessage() {
    $("[id$=panConfirmBudget]").stop().fadeIn(100)
    $("[id$=txtConfirmComment]").focus()
}

function hideConfirmBudgetMessage() {
    $("[id$=panConfirmBudget]").stop().fadeOut(100)
}

var submitClicked = false

function ValidateSubmit(submit) {
    if (submit) {
        $("[id$=UpdateProgress]").show()
        submitClicked = true
        btnCancelSub_Click($("[id$=btnCancelRequest]")[0])
        btnCancelSub_Click($("[id$=btnCancelInvoice]")[0])
        btnCancelSub_Click($("[id$=btnCancelAirTicket]")[0])
        btnCancelSub_Click($("[id$=btnCancelOther]")[0])
    }
    if (!submitClicked) {
        return
    }
    var $summary = $("#totalErrorSummary")
    $summary.html("")
    isValidBT = true
    //
    ValidateRequireSingle('ddlCurrency', "Currency is required")
    ValidateRequireSingle('ddlBTType', "Bussiness trip type is required")
    ValidateRequireSingle('txtEmployeeCode', "Employee code is required")
    ValidateRequireSingle('ddlBudgetName', "Budget is required")
    //ValidateRequireSingle('dteRequestDate_I', "Request date is required")
    //
    ValidateGridEmpty("grvBTRequest", "Business trip request is required", submit, "tabCommon")
    //
    //    if ($("[id$=hIsOneDayExpense]").val() == "T") {
    //        ValidateAttachment("panExpenseAttachments", "Business trip approval attachment is required")
    //    }
    //ValidateAttachment("panPaymentAttachments", "Request for payment attachment is required")
    //
    if (!isValidBT) {
        $summary.show()
        if (submit) {
            $("[id$=UpdateProgress]").hide()
            $("body, html").animate({ "scrollTop": $summary.offset().top }, 300, "swing")
        }
    }
    else {
        $summary.hide()
        if (submit) {
            var budgetCode = $("[id$=txtBudgetCode]").val()
            var budgetID = $("[id$=ddlBudgetName]").val().split("-")[0]
            if (!budgetCode || budgetCode.trim().length === 0) {
                $("[id$=UpdateProgress]").hide()
                showSubmitMessage()
                return
            }
            var isOverBudget = false
            var isDateValid = true
            $.ajax({
                url: "/AjaxHandle.ashx?action=check-expense-budget",
                async: true,
                type: "get",
                data: {
                    btID: $("[id$=hID]").val(),
                    budgetCode: budgetID,
                    bttype: "oneday"
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
                    var budgetRemaining = parseFloat(arrResponse[0])
                    if (!isNaN(budgetRemaining)) {
                        var $grv = $("[id$=grvBTRequest]").parent()
                        $grv.removeClass("validate-error")
                        //
                        if (arrResponse[1] == "conflict") {
                            ShowErrorMessage("This BT Date is conflicted with another existing BT date in other tab: Submitted, Rejected, Completed!")
                            $grv.addClass("validate-error")
                            var $parent = GetParent($grv[0], ".tab-container")
                            var $tab = $parent.find("[role='tab']")
                            if (!$tab.hasClass("active")) {
                                $tab.click()
                            }
                            isDateValid = false
                        }
                        else if (budgetRemaining < 0) {
                            isOverBudget = true
                        }
                        //DoCalculateForm()
                    }
                    else {
                        ShowErrorMessage(response)
                    }
                }
            }).fail(function(xhr, text, status) {
                ShowErrorMessage(text)
            }).always(function() {
                $("[id$=UpdateProgress]").hide()
                if (isOverBudget) {
                    ShowConfirmMessage({
                        message: "It seem to be Over Budget! Do you want to continue?",
                        OK: function() {
                            showSubmitMessage()
                            //$("[id$=btnSubmit]").click()
                        }
                    })
                }
                else if (isDateValid) {
                    showSubmitMessage()
                    //$("[id$=btnSubmit]").click()
                }
            })
        }
    }
}

function showSubmitMessage() {
    if ($("[id$=hIsOneDayExpense]").val() == "T") {
        $("#submitBTTitle").html("Submit to Finance Budget to check budget")
    }
    else {
        $("#submitBTTitle").html("Submit to Finance Accounting to check/process")
    }
    $("[id$=panSubmitInfo]").stop().fadeIn(100)
    $("[id$=txtSubmitComment]").focus()
}

function hideSubmitMessage() {
    $("[id$=panSubmitInfo]").stop().fadeOut(100)
}

function BuildErrorMessage(message) {
    return "<li>" + message + "</li>"
}

function ValidateRequireSingle(ctrlID, message) {
    var $ctrl = $("[id$=" + ctrlID + "]")
    var $ctrlContainer = $ctrl.parent().parent().parent()
    if (!$ctrlContainer.is("table")) {
        $ctrlContainer = $ctrlContainer.parent()
    }
    if (!$ctrlContainer.hasClass("dxeButtonEditSys")) {
        $ctrlContainer = $ctrl
    }
    if (!$ctrl.val() || !$ctrl.val().trim()) {
        if (!$ctrlContainer.hasClass("validate-error")) {
            $ctrlContainer.addClass("validate-error")
            $ctrlContainer.attr("title", message)
        }
        $("#totalErrorSummary").append(BuildErrorMessage(message))
        isValidBT = false
        //
        var $parent = GetParent($ctrl, ".tab-container")
        var $tab = $parent.find("[role='tab']")
        if (!$tab.hasClass("active")) {
            $tab.click()
        }
    }
    else {
        $ctrlContainer.removeClass("validate-error")
        $ctrlContainer.attr("title", "")
    }
}

function ValidateGridEmpty(gridID, message, isSubmit, tabID) {
    var $grid = $("[id$=" + gridID + "]")
    if (!$grid.find("tr.dxgvDataRow_Office2010Black").size()) {
        if (!$grid.parent().hasClass("validate-error")) {
            $grid.parent().addClass("validate-error")
            $grid.parent().attr("title", message)
        }
        var $parent = GetParent($grid[0], ".tab-container")
        var $tab = $parent.find("[role='tab']")
        if (!$tab.hasClass("active")) {
            $tab.click()
        }
        if (isSubmit) {
            $("#" + tabID).click()
        }
        $("#totalErrorSummary").append(BuildErrorMessage(message))
        isValidBT = false
    }
    else {
        $grid.parent().removeClass("validate-error")
        $grid.parent().attr("title", "")
    }
}

function ValidateAttachment(containerID, message) {
    var $container = $("[id$=" + containerID + "]")
    var $wrapper = $container.parent().parent().parent().parent()
    if ($container.text().trim().length === 0) {
        if (!$wrapper.hasClass("validate-error")) {
            $wrapper.addClass("validate-error")
            $wrapper.attr("title", message)
        }
        var $parent = GetParent($container[0], ".tab-container")
        var $tab = $parent.find("[role='tab']")
        if (!$tab.hasClass("active")) {
            $tab.click()
        }
        $("#totalErrorSummary").append(BuildErrorMessage(message))
        isValidBT = false
    }
    else {
        $wrapper.removeClass("validate-error")
        $wrapper.attr("title", "")
    }
}

var CheckValidateTimeout
function CheckValidate(meID) {
    if ($("#" + meID).attr("data-status") == "done") {
        ValidateSubmit()
        //
        CheckValidateTimeout = null
        return
    }
    CheckValidateTimeout = setTimeout("CheckValidate('" + meID + "')", 10)
}

function ShowHistory(type) {
    var $pan = $("#BTStatusHistory")
    var $panContent = $("#BTStatusContent")
    $panContent.find(".status-table").hide()
    $panContent.find(".status-table." + type).show()
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

function CalculateTransportation(me) {
    var type = GetParent(me, ".td-input").attr("data-type")
    var $tr = $(".estimate-transportation-fee")
    var totalAmount = 0
    $tr.find("[data-type=" + type + "]").each(function() {
        var amount = 0
        try {
            amount = parseFloat($(this).find(":text").val().replace(/,/g, ""))
        }
        catch (e) {
            amount = parseFloat(0)
        }
        if (isNaN(amount)) {
            amount = 0
        }
        totalAmount += amount
    })
    //$("." + type + "-summary :text").val(FormatNumber(totalAmount))
    if ($("." + type + "-summary :text").attr("id")) {
        window[$("." + type + "-summary :text").attr("id").replace("_I", "")].SetNumber(totalAmount)
    }
    chkCommonCCAmountChanged(true)
}

function CalculateInvoice() {
    //    var taxRate = 0
    //    var taxRateText = $(".tax-rate :text").val()
    //    if (taxRateText) {
    //        taxRateText = taxRateText.replace(/,/g, "")
    //    }
    //    taxRate = parseFloat(taxRateText)
    var vat = 0
    var vatText = $(".inv-vat :text").val()
    if (vatText) {
        vatText = vatText.replace(/,/g, "")
    }
    vat = parseFloat(vatText)
    if (isNaN(vat)) {
        vat = 0
    }
    var amount = 0
    var amountText = $(".inv-cost :text").val()
    if (amountText) {
        amountText = amountText.replace(/,/g, "")
    }
    amount = parseFloat(amountText)
    if (isNaN(amount)) {
        amount = 0
    }
    //$("[id$=spiInvVAT]").val(FormatNumber(Math.round(amount * (taxRate / 100))))
    $("[id$=spiInvTotal]").val(FormatNumber(amount + vat))
}

function CalculateAirTicket() {
    var totalAmount = 0
    $(".air-payment").each(function() {
        var amount = 0
        try {
            amount = parseFloat($(this).find(":text").val().replace(/,/g, ""))
        }
        catch (e) {
            amount = parseFloat(0)
        }
        if (isNaN(amount)) {
            amount = 0
        }
        totalAmount += amount
    })
    window[$("[id$=spiAirNetPayment]").attr("id")].SetNumber(totalAmount)
}

function BindGetExrateEvent() {
    $("[id$=ddlAirCurrency]").change(function() {
        GetExrate(this, $(this).val(), 'VND', $("[id$=dteAirDate_I]").val())
    })
}

function GetExrate(me, fromCurrency, toCurrency, exchangeDate) {
    var $txtExrate = $(".exrate-value[data-exrate='" + $(me).attr("data-exrate") + "']").find(":text")
    if (fromCurrency.toLowerCase() == toCurrency.toLowerCase()) {
        window[$txtExrate.attr("id").replace("_I", "")].SetNumber(1)
        DoCalculateForm()
    }
    else {
        if ($(me).hasClass("getting")) {
            return
        }
        $(me).addClass("getting")
        $.ajax({
            url: "/AjaxHandle.ashx?action=get-exrate",
            async: true,
            type: "get",
            data: { fromCurrency: fromCurrency, toCurrency: toCurrency, exchangeDate: exchangeDate },
            cache: false,
            beforeSend: function() {
                $("[id$=UpdateProgress]").show()
            }
        }).done(function(response) {
            if (response == "session") {
                location.reload()
            } else {
                var exrate = parseFloat(response)
                if (!isNaN(exrate)) {
                    window[$txtExrate.attr("id").replace("_I", "")].SetNumber(exrate)
                    DoCalculateForm()
                }
                else {
                    ShowErrorMessage(response)
                }
            }
        }).fail(function(xhr, text, status) {
            ShowErrorMessage(text)
        }).always(function() {
            $(me).removeClass("getting")
            if (!$(".getting").size()) {
                $("[id$=UpdateProgress]").hide()
            }
            ValidateSubmit()
        })
    }
}

var CheckExportFileTimeout
function CheckExportFile(id) {
    var $btn = $("#" + id)
    if ($btn.attr("data-status") == "done") {
        var filePath = $btn.attr("data-file-path")
        if (filePath && filePath.trim().length > 0) {
            location.href = filePath
        }
        //
        CheckExportFileTimeout = null
        return
    }
    CheckExportFileTimeout = setTimeout("CheckExportFile('" + id + "')", 10)
}

function GetExportFile(me) {
    $(me).removeAttr("data-file-path")
    CheckExportFile($(me).attr("id"))
}

function btnApproveBTClick() {
    var isOverBudget = false
    var isValidOraUser = true
    var budgetID = $("[id$=ddlBudgetName]").val().split("-")[0]
    $.ajax({
        url: "/AjaxHandle.ashx?action=check-expense-budget",
        async: true,
        type: "get",
        data: {
            btID: $("[id$=hID]").val(),
            budgetCode: budgetID,
            checkOraUser: 'y'
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
            var budgetRemaining = parseFloat(arrResponse[0])
            if (!isNaN(budgetRemaining)) {
                if (arrResponse[1] == "no-mapping") {
                    ShowErrorMessage("You do not have an mapping oracle account!")
                    isValidOraUser = false
                }
                else if (arrResponse[1] == "not-found") {
                    ShowErrorMessage("Your mapping oracle account is not found!")
                    isValidOraUser = false
                }
                else if (arrResponse[1] == "expired") {
                    ShowErrorMessage("Your mapping oracle account has disabled!")
                    isValidOraUser = false
                }
                else if (budgetRemaining < 0) {
                    isOverBudget = true
                }
                //DoCalculateForm()
            }
            else {
                ShowErrorMessage(response)
            }
        }
    }).fail(function(xhr, text, status) {
        ShowErrorMessage(text)
    }).always(function() {
        $("[id$=UpdateProgress]").hide()
        if (isOverBudget) {
            showApproveMessage("It seem to be Over Budget! Do you want to continue?")
        }
        else if (isValidOraUser) {
            showApproveMessage("")
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

function showApproveMessage(message) {
    $("[id$=approveMessage]").html(message)
    $("[id$=hApproveMessage]").val(message)
    $("[id$=tabApproveMessage]").stop().fadeIn(100)
    $('[id$=dteGLDate_I]').focus()
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
        ShowErrorMessage("Recommendation is required!")
        $txtRejectReason.addClass("validate-error").focus()
        return false
    }
    else {
        $txtRejectReason.removeClass("validate-error")
        return true
    }
}

function DoCalculateForm() {
    //    $(".tblRequestDetails .amount :text").each(function() {
    //        CalculateRequestDetails($(this), false)
    //    })
    CalculateTransportation($(".estimate-transportation-fee :text:eq(0)")[0])
    //    CalculateTransportation($(".estimate-transportation-fee :text:eq(1)")[0])
    CalculateInvoice()
    //CalculateAirTicket()
}

function CheckOraStatus() {
    $.ajax({
        url: "/AjaxHandle.ashx?action=check-expense-ora-status",
        async: true,
        type: "get",
        data: { btID: $("[id$=hID]").val() },
        cache: false,
        beforeSend: function() {
            $("[id$=UpdateProgress]").show()
        }
    }).done(function(response) {
        if (response == "session") {
            location.reload()
        } else if (response == "reject-allowed") {
            showRejectMessage()
        } else {
            ShowErrorMessage(response)
        }
    }).fail(function(xhr, text, status) {
        ShowErrorMessage(text)
    }).always(function() {
        $("[id$=UpdateProgress]").hide()
    })
}

function ConfirmCancel(enable) {
    if (enable) {
        ShowConfirmMessage({
            message: "All unsaved data will be lost? Do you want to continue?",
            OK: function() {
                $("[id$=btnCancel]").click()
            }
        })
    }
    else {
        $("[id$=btnCancel]").click()
    }
}

function showChooseEmployee(me) {
    $("#tabChoosemployee").stop().fadeIn(100)
    $("body, html").animate({ "scrollTop": $("#tabChoosemployee").offset().top }, 300, "swing")
}


function hideChooseEmployee() {
    $("[id$='hChooseEmployeeID']").val("")
    $("#tabChoosemployee").stop().fadeOut(100)
}

function CheckChoosedEmployess() {
    if (!$("#tabChoosemployee").find(".chkChooseEmployee:checked").size()) {
        ShowErrorMessage("Please choose at least 1 employee to copy!")
        return
    }
    var tabIndex = $(".HRTabControl .HRTabNav li.current").index()
    var $tabContainer = $(".HRTabControl .HRTabList .HRTab:eq(" + tabIndex + ")")
    var $tr = $tabContainer.find(".chkSelect:checked:eq(0)").parent().parent()
    $("[id$=hID]").val($tr.find("td.id").text())
    //
    var users = ""
    $("#tabChoosemployee").find(".chkChooseEmployee:checked").each(function() {
        users = "," + $(this).parent().parent().find("td.code").text() + users
    })
    if (users.trim().length > 0) {
        users = users.substr(1)
    }
    $("[id$=hCopyUsers]").val(users)
    //
    $("[id$=btnCopy]").click()
}

//function CompareTime(time1, time2) {
//    var result
//    var time1s = time1.split(":")
//    var time1h = parseInt(time1s[0])
//    var time1m = parseInt(time1s[1])
//    var time2s = time2.split(":")
//    var time2h = parseInt(time2s[0])
//    var time2m = parseInt(time2s[1])
//    if ((time2h === time1h && time2m > time1m) || time2h > time1h) {
//        result = 1
//    }
//    else if (time2h === time1h && time2m === time1m) {
//        result = 0
//    }
//    else {
//        result = -1
//    }
//    return result
//}

//function SetMealAmount() {
//    var fromTime = $("[id$=txeFromTime_I]").val()
//    var toTime = $("[id$=txeToTime_I]").val()
//    //var isDGMAndAbove = false//$("[id$=hIsDGMAndAbove]").val() == "Y"
//    var isGMAndAbove = $("[id$=hIsGMAndAbove]").val() == "Y"
//    var breakfast = 0
//    var lunch = 0
//    var dinner = 0
//    if (isGMAndAbove) {
//        breakfast = 1
//        lunch = 1
//        dinner = 1
//    }
//    else {
//        if (CompareTime(fromTime, "08:30") >= 0 && CompareTime(toTime, "08:30") <= 0) {
//            breakfast = 1
//        }
//        if (CompareTime(fromTime, "12:30") >= 0 && CompareTime(toTime, "12:30") <= 0) {
//            lunch = 1
//        }
//        if (CompareTime(fromTime, "18:00") >= 0 && CompareTime(toTime, "18:00") <= 0) {
//            dinner = 1
//        }
//    }
//    $("[id$=chkBreakfastAmount]").prop("checked", breakfast === 1).prop("disabled", breakfast < 1)
//    window[$("[id$=txtBreakfastQty_I]").attr("id").replace("_I", "")].SetNumber(breakfast)
//    $("[id$=chkLunchAmount]").prop("checked", lunch === 1).prop("disabled", lunch < 1)
//    window[$("[id$=txtLunchQty_I]").attr("id").replace("_I", "")].SetNumber(lunch)
//    $("[id$=chkDinnerAmount]").prop("checked", dinner === 1).prop("disabled", dinner < 1)
//    window[$("[id$=txtDinnerQty_I]").attr("id").replace("_I", "")].SetNumber(dinner)
//    DoCalculateForm()
//}

//function bindCountMealEvents(me) {
//    $(me).removeAttr("data-status")
//    var id = $(me).attr("id")
//    checkCountMealEvents(id)
//}

//function checkCountMealEvents(id) {
//    if ($("#" + id).attr("data-status") == "done") {
//        SetMealAmount()
//        return
//    }
//    setTimeout("checkCountMealEvents('" + id + "')", 10)
//}

//function CheckAmountChecked() {
//    window[$("[id$=txtBreakfastQty_I]").attr("id").replace("_I", "")].SetNumber($("[id$=chkBreakfastAmount]").is(":checked") ? 1 : 0)
//    window[$("[id$=txtLunchQty_I]").attr("id").replace("_I", "")].SetNumber($("[id$=chkLunchAmount]").is(":checked") ? 1 : 0)
//    window[$("[id$=txtDinnerQty_I]").attr("id").replace("_I", "")].SetNumber($("[id$=chkDinnerAmount]").is(":checked") ? 1 : 0)
//    DoCalculateForm()
//}

function btnDeleteOtherClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this information?",
        OK: function() {
            clickDeleteButton(me)
        }
    })
}

function ValidateOtherForm(me) {
    var isValid = ValidateRequire('ExpenseOtherForm', 'ExpenseOtherSummary')
    if (isValid) {
        $(me).parent().find("+:submit").click()
    }
}

function ShowMovingTimeAllowance(forAdd) {
    if (forAdd) {
        $("[id$=hActualMovingTimeAmount]").val($("[id$=hMovingTimeAmount]").val())
    }
    $("#spanMovingTimeAmount").text($("[id$=hActualMovingTimeAmount]").val())
}

var needScroll = false

function ClearRequestForm(forAdd) {
    $("[id$=hRequestID]").val("")
    dteDate.SetDate(new Date())
    var fromDate = new Date()
    fromDate.setHours(8)
    fromDate.setMinutes(0)
    txeFromTime.SetDate(fromDate)
    var toDate = new Date()
    toDate.setHours(16)
    toDate.setMinutes(45)
    txeToTime.SetDate(toDate)
    $("[id$=chkMovingTimeAllowance]").prop("checked", false)
    ShowMovingTimeAllowance(true)
    $("[id$=ddlToLocation]").val("")
    LoadExpenseNorm()
    CalculateRequest()
    txtMotobikeQty.SetValue(null)
    txtTaxiQty.SetValue(null)
    txtTaxiFee.SetValue(null)
    txtMotobikeFee.SetValue(null)
    CalculateTransportation($(".estimate-transportation-fee :text:eq(0)")[0])
    $("[id$=txtTaxiDesc]").val("")
    $("[id$=txtMotobikeDesc]").val("")
    $("[id$=chkCarRequest]").prop("checked", false)
    $("[id$=chkAirTicketRequest]").prop("checked", false)
    $("[id$=chkTrainTicketRequest]").prop("checked", false)
    //
    $("[id$=chkCommonCCAmount]").prop("checked", false)
    chkCommonCCAmountChanged()
    //
    $("[id$=txaPurpose]").val("")
    dteRequestDate.SetDate(new Date())
    //
    $("[id$=dteDate]").attr("style", "")
    $("[id$=txeFromTime]").attr("style", "width: 100px !important; display: inline-block")
    $("[id$=txeToTime]").attr("style", "width: 100px !important; display: inline-block")
    $("[id$=txtMotobikeFee]").attr("style", "height: 21px;")
    $("[id$=lblMovingTimeTitle]").attr("style", "position: relative;")
    //
    //needScroll = true
    //
    if (forAdd) {
        DepartureDateChanged()
    }
}

function LoadExpenseNorm() {
    txtBreakfastUnit.SetValue(ConvertToMoney($("[id$=hBreakfastUnit]").val()))
    txtLunchUnit.SetValue(ConvertToMoney($("[id$=hLunchUnit]").val()))
    txtDinnerUnit.SetValue(ConvertToMoney($("[id$=hDinnerUnit]").val()))
}

function CalculateRequest(onlyShow) {
    txtBreakfastUnit.SetMaxValue(SpinEditGenMaxValue(txtBreakfastUnit.GetValue()))
    txtLunchUnit.SetMaxValue(SpinEditGenMaxValue(txtLunchUnit.GetValue()))
    txtDinnerUnit.SetMaxValue(SpinEditGenMaxValue(txtDinnerUnit.GetValue()))
    var motobikeQty = txtMotobikeQty.GetNumber()
    txtMotobikeFee.SetMaxValue(SpinEditGenMaxValue(ConvertToMoney($("[id$=hMotobikeFee]").val()) * (motobikeQty < 0 ? 0 : motobikeQty)))
    //
    var fromTime = txeFromTime.GetDate()
    var toTime = txeToTime.GetDate()
    var breakfast = 1
    var lunch = 1
    var dinner = 1
    var isGMAndAbove = $("[id$=hIsGMAndAbove]").val() == "Y"
    //if (!isGMAndAbove) {
        if (CompareTime(fromTime.getHours(), fromTime.getMinutes(), 8, 30) > 0 || CompareTime(toTime.getHours(), toTime.getMinutes(), 8, 30) < 0) {
            breakfast -= 1
        }
        if (CompareTime(fromTime.getHours(), fromTime.getMinutes(), 12, 30) > 0 || CompareTime(toTime.getHours(), toTime.getMinutes(), 12, 30) < 0) {
            lunch -= 1
        }
        if (CompareTime(fromTime.getHours(), fromTime.getMinutes(), 18, 0) > 0 || CompareTime(toTime.getHours(), toTime.getMinutes(), 18, 0) < 0) {
            dinner -= 1
        }
    //}
    if (!onlyShow) {
        $("[id$=chkBreakfastAmount]").prop("checked", breakfast == 1)
        $("[id$=chkLunchAmount]").prop("checked", lunch == 1)
        $("[id$=chkDinnerAmount]").prop("checked", dinner == 1)
    }
    var enable = $("[id$=btnSaveRequest]").size()
    $("[id$=chkBreakfastAmount]").prop("disabled", breakfast < 1 || !enable)
    $("[id$=chkLunchAmount]").prop("disabled", lunch < 1 || !enable)
    $("[id$=chkDinnerAmount]").prop("disabled", dinner < 1 || !enable)
    txtBreakfastQty.SetValue($("[id$=chkBreakfastAmount]").is(":checked") ? 1 : 0)
    txtLunchQty.SetValue($("[id$=chkLunchAmount]").is(":checked") ? 1 : 0)
    txtDinnerQty.SetValue($("[id$=chkDinnerAmount]").is(":checked") ? 1 : 0)
    $(".tblRequestDetails .amount :text").each(function() {
        CalculateRequestDetails($(this), false)
    })
}

function CompareTime(h1, m1, h2, m2) {
    var result = 0
    if ((h1 == h2 && m1 > m2) || (h1 > h2)) {
        result = 1
    }
    else if (h1 == h2 && m1 == m2) {
        result = 0
    }
    else {
        result = -1
    }
    return result
}

function ClearOtherForm() {
    $("[id$=hExpenseOtherID]").val("")
    dteExpenseOtherDate.SetDate(new Date())
    $("[id$=txtOtherExpense]").val("")
    //spiExpenseOtherAmount.SetValue(null)
    window[$("[id$=spiExpenseOtherAmount]").attr("id")].SetValue(null)
    //
    $("[id$=chkOtherCCAmount]").prop("checked", false)
    chkOtherCCAmountChanged()
    //
    needScroll = true
}

function chkCommonCCAmountChanged(onlyShow) {
    if ($("[id$=chkCommonCCAmount]").size()) {
        if ($("[id$=chkCommonCCAmount]").is(":checked")) {
            var commonAmount = $("[id$=txtTotalAmount]").size() ? window[$("[id$=txtTotalAmount]").attr("id")].GetNumber() : 0
            var taxi = txtTaxiFee.GetNumber()
            var motobike = txtMotobikeFee.GetNumber()
            if (!onlyShow) {
                spiCommonCCAmount.SetValue(taxi)
            }
            spiCommonCCAmount.SetMaxValue(SpinEditGenMaxValue(commonAmount + taxi + motobike))
            $(".common-credit").show()
        }
        else {
            $(".common-credit").hide()
            spiCommonCCAmount.SetValue(null)
        }
    }
}

function chkOtherCCAmountChanged(onlyShow) {
    if ($("[id$=chkOtherCCAmount]").size()) {
        if ($("[id$=chkOtherCCAmount]").is(":checked")) {
            var amount = $("[id$=spiExpenseOtherAmount]").size() ? window[$("[id$=spiExpenseOtherAmount]").attr("id")].GetNumber() : 0
            if (!onlyShow) {
                spiOtherCCAmount.SetValue(amount)
            }
            spiOtherCCAmount.SetMaxValue(SpinEditGenMaxValue(amount))
            $(".other-credit").show()
        }
        else {
            $(".other-credit").hide()
            spiOtherCCAmount.SetValue(null)
        }
    }
}

function CalculateMotobike() {
    var motobikeQty = ConvertToMoney($("[id$=txtMotobikeQty_I]").val())
    var motobikeFee = ConvertToMoney($("[id$=hMotobikeFee]").val()) * (motobikeQty < 0 ? 0 : motobikeQty)
    txtMotobikeFee.SetMaxValue(SpinEditGenMaxValue(motobikeFee))
    txtMotobikeFee.SetValue(motobikeFee)
    CalculateTransportation($(".estimate-transportation-fee :text:eq(0)")[0])
}

function ConfirmRecall() {
    ShowConfirmMessage({
        message: "Are you sure to recall this BT Expense Declaration?",
        OK: function() {
            $("[id$=btnRecall]").click()
        }
    })
}