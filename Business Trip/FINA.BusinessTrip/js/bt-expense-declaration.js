$(document).ready(function() {
    employeeCodeAutoComplete()
    //
    BindCalculateRequestDetails()
    //
    requestDateChanged()
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
        var x = $this.offset().left - sidebarWidth - $oraErrorContainer.outerWidth() + 100
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
}

var checkPostbackTimeout
function checkPostback(id) {
    if ($("#" + id).attr("data-status") == "done") {
        currentDepartureDate = dteExpenseDepartureDate.GetDate()
        ScrollDocument()
        LoadExpenseNorm()
        CalculateRequest()
        DoCalculateForm()
        chkCommonCCAmountChanged(true)
        chkHotelCCAmountChanged(true)
        chkOtherCCAmountChanged(true)
        CalculateSummary()
        requestDateChanged()
        SetNumberInputType()
        employeeCodeAutoComplete()
        BindCalculateRequestDetails()
        BindCurrencyInformation()
        BindGetExrateEvent()
        $(".error-summary").html("")
        //
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
    var commonCurrency = $('[id$=ddlCommonCurrency]')[0]
    var commonCurrencyOption = commonCurrency.options[commonCurrency.selectedIndex]
    $('[id$=lblCommonCCCurrency]').text(commonCurrencyOption ? commonCurrencyOption.text : '')
    $(".exrate-caption").each(function() {
        var ddlCurrency = $(".exrate-currency[data-exrate=" + $(this).attr("data-exrate") + "]")[0]
        var selectedOption = ddlCurrency.options[ddlCurrency.selectedIndex]
        $(this).html("(USD &rarr; " + (selectedOption ? selectedOption.text : '') + ")")
    })
    //
    var ddlAirCurrency = $("[id$=ddlAirCurrency]")[0]
    var ddlAirCurrencyOption = ddlAirCurrency.options[ddlAirCurrency.selectedIndex]
    $("[id$=lblAirExrate]").html("(" + (ddlAirCurrencyOption ? ddlAirCurrencyOption.text : '') + " &rarr; VND)")
    $("[id$=ddlAirCurrency]").change(function() {
        $("[id$=lblAirExrate]").html("(" + $(this)[0].options[$(this)[0].selectedIndex].text + " &rarr; VND)")
    })
    //
    $(".exrate-currency").change(function() {
        $(".exrate-caption[data-exrate=" + $(this).attr("data-exrate") + "]").html("(USD &rarr; " + this.options[this.selectedIndex].text + ")")
    })
}

function requestDateChanged() {
    $("[id$=dteRequestDate_I]").change(function() {
        ValidateSubmit()
    }).focus(function() {
        ValidateSubmit()
    })
}

function bindStartupEvents(me) {
    $(me).removeAttr("data-status")
    var id = $(me).attr("id")
    checkPostback(id)
}

function BindCalculateRequestDetails() {
    $(".expense-amount .dxeSBC, .expense-exrate .dxeSBC").click(function() {
        $(this).prev().find(":text").focus()
        CalculateRequestDetails($(this).prev().find(":text"))
    })
    $(".expense-amount :text, .expense-exrate :text").keyup(function() {
        CalculateRequestDetails($(this))
    }).blur(function() {
        CalculateRequestDetails($(this))
    })
    //
    //    $(".transportation-fee .dxeSBC").click(function() {
    //        $(this).prev().find(":text").focus()
    //        CalculateTransportation(this)
    //    })
    //    $(".transportation-fee :text").keyup(function() {
    //        CalculateTransportation(this)
    //    }).blur(function() {
    //        CalculateTransportation(this)
    //    })
    //
    //    $(".expense-trans-exrate .dxeSBC").click(function() {
    //        $(this).prev().find(":text").focus()
    //        SummaryTransportation()
    //    })
    //    $(".expense-trans-exrate :text").keyup(function() {
    //        SummaryTransportation()
    //    }).blur(function() {
    //        SummaryTransportation()
    //    })
    //
    $(".expense-other-exrate .dxeSBC, .expense-other-amount .dxeSBC").click(function() {
        $(this).prev().find(":text").focus()
        CalculateOther()
    })
    $(".expense-other-exrate :text, .expense-other-amount :text").keyup(function() {
        CalculateOther()
    }).blur(function() {
        CalculateOther()
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
    //
}

function CalculateRequestDetails($me) {
    var $td = $me.parent().parent().parent().parent()
    if (!$td.is("td")) {
        $td = $td.parent()
    }
    var amount = 0
    var amountText = $me.val()
    if (amountText) {
        amountText = amountText.replace(/,/g, "")
    }
    amount = parseFloat(amountText)
    //    if ($td.hasClass("other-amount")) {
    //        if (amount > 0) {
    //            $(".expense-other-explaination").show()
    //        }
    //        else {
    //            $(".expense-other-explaination").hide()
    //        }
    //    }
    var totalAmount = 0
    $td.parent().find(".expense-amount").each(function() {
        var totalAmountText = $(this).find(":text").val()
        //        if (totalAmountText) {
        //            totalAmountText = totalAmountText.replace(/,/g, "")
        //        }
        totalAmount += ConvertToMoney(totalAmountText)
    })
    var exrate = 1
    var exrateText = $td.parent().find(".expense-exrate :text").val()
    //    if (exrateText) {
    //        exrateText = exrateText.replace(/,/g, "")
    //    }
    exrate = ConvertToMoney(exrateText)
    if (exrate < 1) {
        exrate = 1
    }
    var totalAmountConverted = totalAmount / exrate
    var $txtTotalAmount = $td.parent().find(".total-amount :text")
    if ($txtTotalAmount.size()) {
        window[$txtTotalAmount.attr("id").replace("_I", "")].SetValue(isNaN(totalAmount) ? 0 : totalAmount)
    }
    var $txtTotalAmountConverted = $td.parent().find(".total-converted :text")
    if ($txtTotalAmountConverted.size()) {
        window[$txtTotalAmountConverted.attr("id").replace("_I", "")].SetValue(isNaN(totalAmountConverted) ? 0 : totalAmountConverted)
    }
    chkCommonCCAmountChanged(true)
    chkHotelCCAmountChanged(true)
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
    $(".HRTabControl.edit-form > .HRTabNav > ul li").removeClass("current")
    $(".HRTabControl.edit-form > .HRTabNav > ul li:first-child").addClass("current")
    $(".HRTabControl.edit-form > .HRTabList > .HRTab").hide()
    $(".HRTabControl.edit-form > .HRTabList > .HRTab:first-child").show()
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
    $("[id$=hID]").val($tr.find("td.id").text())
    btnAdd_Click()
}

function DoCheckEnableForm() {
    $("[id$=btnCancel]").removeAttr("enable-form").removeAttr("enable-invoice-form").removeAttr("enable-air-form")
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
            $("[id$=hID]").val($(me).attr("data-id"))
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

function btnDeleteRequestClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this request?",
        OK: function() {
            clickDeleteButton(me)
        }
    })
}

function btnDeleteScheduleClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this schedule?",
        OK: function() {
            clickDeleteButton(me)
        }
    })
}

function btnDeleteTransportationClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this transportation?",
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

function btnDeleteOtherClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this information?",
        OK: function() {
            clickDeleteButton(me)
        }
    })
}

function btnDeleteInvoiceClick(me) {
    ShowConfirmMessage({
        message: "Are you sure to delete this invoice?",
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
        return
    }
    $partialForm.stop(true, true)
    $partialForm.slideUp(100, function() {
        $partialForm.find(".add-edit-action").text("")
        //                $partialForm.find(">*:not(.action-pan)").find(":text, select, textarea, :password, input[type='hidden']").val("")
        //                $partialForm.find(".total-amount").find(":text, input[type='hidden']").val("0")
    })
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

function ValidateAdvanceForm(me) {
    var isValid = ValidateRequire('ExpenseAdvanceForm', 'ExpenseAdvanceSummary')
    if (isValid) {
        $(me).parent().find("+:submit").click()
    }
}

function ValidateScheduleForm(me) {
    var isValid = ValidateRequire('ExpenseScheduleForm', 'ExpenseScheduleSummary')
    if (isValid) {
        $(me).parent().find("+:submit").click()
    }
}

function ValidateTransportationForm(me) {
    var isValid = ValidateRequire('ExpenseTransportationForm', 'ExpenseTransportationSummary')
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

function ValidateOtherForm(me) {
    var isValid = ValidateRequire('ExpenseOtherForm', 'ExpenseOtherSummary')
    if (isValid) {
        $(me).parent().find("+:submit").click()
    }
}

function ValidateInvoiceForm(me) {
    var isValid = ValidateRequire('InvoiceForm', 'InvoiceSummary')
    if (isValid) {
        if ($("[id$=lblInvNetCostCurrency]").text().toLowerCase() == 'vnd') {        
            $("[id$=spiInvNetCost]").removeClass("validate-error")
            $("[id$=spiInvVAT]").removeClass("validate-error")
            //
            var netCost = ConvertToMoney($("[id$=spiInvNetCost_I]").val())
            if (parseInt(netCost) < netCost) {
                $("[id$=spiInvNetCost]").addClass("validate-error")
                ShowErrorMessage("Net cost must be integer!")
                return
            }
            var vat = ConvertToMoney($("[id$=spiInvVAT_I]").val())
            if (parseInt(vat) < vat) {
                $("[id$=spiInvVAT]").addClass("validate-error")
                ShowErrorMessage("VAT must be integer!")
                return
            }
        }
        //
        $(me).parent().find("+:submit").click()
    }
}

var isValidBT
var submitClicked = false

function ValidateSubmit(submit) {
    if (submit) {
        $("[id$=UpdateProgress]").show()
        submitClicked = true
        btnCancelSub_Click($("[id$=btnCancelRequest]")[0])
        btnCancelSub_Click($("[id$=btnCancelSchedule]")[0])
        btnCancelSub_Click($("[id$=btnCancelTransportation]")[0])
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
    ValidateRequireSingle('dteExpenseDepartureDate_I', "Departure date is required")
    ValidateRequireSingle('dteExpenseReturnDate_I', "Return date is required")
    ValidateRequireSingle('ddlExpenseDestinationCountry', "Destination country is required")
    ValidateRequireSingle('txtExpensePurpose', "")//Purpose is required
    //
    ValidateGridEmpty("grvCommonExpense", "Expense common declaration is required", submit)
    //
    //ValidateAttachment("panExpenseAttachments", "Business trip expense attachment is required")
    //ValidateAttachment("panScheduleAttachments", "Business trip schedule attachment is required")
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
            var isOverBudget = false
            var isDateValid = true
            var budgetID = $("[id$=ddlBudgetName]").val().split("-")[0]
            $.ajax({
                url: "/AjaxHandle.ashx?action=check-expense-budget",
                async: true,
                type: "get",
                data: {
                    btID: $("[id$=hID]").val(),
                    budgetCode: budgetID,
                    bttype: "expense",
                    departureDate: $("[id$=dteExpenseDepartureDate_I]").val(),
                    returnDate: $("[id$=dteExpenseReturnDate_I]").val()
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
                        var $departureDate = $("[id$=dteExpenseDepartureDate]")
                        var $returnDate = $("[id$=dteExpenseReturnDate]")
                        var $grv = $("[id$=grvCommonExpense]").parent()
                        var $grvOther = $("[id$=grvBTExpenseOther]").parent()
                        $departureDate.removeClass("validate-error")
                        $returnDate.removeClass("validate-error")
                        $grv.removeClass("validate-error")
                        $grvOther.removeClass("validate-error")
                        //
                        if (arrResponse[1] == "invalid") {
                            ShowErrorMessage("Return date must be greater than departure date!")
                            $departureDate.addClass("validate-error")
                            $returnDate.addClass("validate-error").focus()
                            isDateValid = false
                        }
                        else if (arrResponse[1] == "out") {
                            ShowErrorMessage("Expense date must between departure and return date!")
                            $departureDate.addClass("validate-error")
                            $returnDate.addClass("validate-error").focus()
                            $grv.addClass("validate-error")
                            var $parent = GetParent($grv[0], ".tab-container")
                            var $tab = $parent.find("[role='tab']")
                            if (!$tab.hasClass("active")) {
                                $tab.click()
                            }
                            $("#tabExpenseCommon").click()
                            isDateValid = false
                        }
                        else if (arrResponse[1] == "other-out") {
                            ShowErrorMessage("Other date must between departure and return date!")
                            $departureDate.addClass("validate-error")
                            $returnDate.addClass("validate-error").focus()
                            $grvOther.addClass("validate-error")
                            var $parent = GetParent($grvOther[0], ".tab-container")
                            var $tab = $parent.find("[role='tab']")
                            if (!$tab.hasClass("active")) {
                                $tab.click()
                            }
                            $("#tabExpenseOther").click()
                            isDateValid = false
                        }
                        else if (arrResponse[1] == "conflict") {
                            ShowErrorMessage("This BT Date is conflicted with another existing BT date in other tab: Submitted, Rejected, Completed!")
                            $departureDate.addClass("validate-error")
                            $returnDate.addClass("validate-error").focus()
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
            if (message) {
                $ctrlContainer.attr("title", message)
            }
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
        if (message) {
            $ctrlContainer.attr("title", "")
        }
    }
}

function ValidateGridEmpty(gridID, message, submit) {
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
        $("#totalErrorSummary").append(BuildErrorMessage(message))
        isValidBT = false
        if (gridID == "grvCommonExpense") {
            if (submit) {
                $("#tabExpenseCommon").click()
            }
        }
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

//function CalculateTransportation(me) {
//    var type = GetParent(me, ".td-input").attr("data-type")
//    var $tr = $(".transportation-fee")
//    var totalAmount = 0
//    $tr.find("[data-type=" + type + "]").each(function() {
//        var amount = 0
//        try {
//            amount = parseFloat($(this).find(":text").val().replace(/,/g, ""))
//        }
//        catch (e) {
//            amount = parseFloat(0)
//        }
//        if (isNaN(amount)) {
//            amount = 0
//        }
//        totalAmount += amount
//    })
//    var exrate = 1
//    var exrateText = $(".expense-trans-exrate :text").val()
//    if (exrateText) {
//        exrateText = exrateText.replace(/,/g, "")
//    }
//    exrate = parseFloat(exrateText)
//    $("." + type + "-summary input").val(FormatNumber(totalAmount))
//    $("." + type + "-converted-summary input").val(FormatNumber(totalAmount / exrate))
//}

//function SummaryTransportation() {
//    var exrate = 1
//    var exrateText = $(".expense-trans-exrate :text").val()
//    if (exrateText) {
//        exrateText = exrateText.replace(/,/g, "")
//    }
//    exrate = parseFloat(exrateText)
//    var userPay = 0
//    var userPayText = $("[id$=txtActualUserPay]").val()
//    if (userPayText) {
//        userPayText = userPayText.replace(/,/g, "")
//    }
//    userPay = parseFloat(userPayText)
//    var gaPay = 0
//    var gaPayText = $("[id$=txtActualGAPay]").val()
//    if (gaPayText) {
//        gaPayText = gaPayText.replace(/,/g, "")
//    }
//    gaPay = parseFloat(gaPayText)
//    $("[id$=txtActualUserPayConverted]").val(FormatNumber(userPay / exrate))
//    $("[id$=txtActualGAPayConverted]").val(FormatNumber(gaPay / exrate))
//}

function CalculateOther() {
    var exrate = 1
    var exrateText = $(".expense-other-exrate :text").val()
    //    if (exrateText) {
    //        exrateText = exrateText.replace(/,/g, "")
    //    }
    exrate = ConvertToMoney(exrateText)
    if (exrate < 1) {
        exrate = 1
    }
    var amount = 0
    var amountText = $(".expense-other-amount :text").val()
    //    if (amountText) {
    //        amountText = amountText.replace(/,/g, "")
    //    }
    amount = ConvertToMoney(amountText)
    $(".expense-other-amount-converted :text").val(FormatNumber(amount / exrate))
    chkOtherCCAmountChanged(true)
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
    window[$("[id$=spiAirNetPayment]").attr("id")].SetValue(totalAmount)
}

var exchangeDateTimeout
var airDateTimeout

function BindGetExrateEvent() {
    $("[id$=ddlCommonCurrency]").change(function() {
        GetExrate(this, 'USD', $(this).val(), $("[id$=dteDate_I]").val())
    })

    $("[id$=ddlHotelCurrency]").change(function() {
        GetExrate(this, 'USD', $(this).val(), $("[id$=dteHotelExchangeDate_I]").val())
    })

    $("[id$=ddlExpenseOtherCurrency]").change(function() {
        GetExrate(this, 'USD', $(this).val(), $("[id$=dteExpenseOtherDate_I]").val())
    })

    $("[id$=ddlAirCurrency]").change(function() {
        GetExrate(this, $(this).val(), 'VND', $("[id$=dteAirDate_I]").val(), true)
    })
}

function GetExrate(me, fromCurrency, toCurrency, exchangeDate, isAir) {
    if (!isAir && $("[id$=ddlBTType]").val().indexOf("domestic") >= 0) {
        return
    }
    var $txtExrate = $(".exrate-value[data-exrate='" + $(me).attr("data-exrate") + "']").find(":text")
    if (fromCurrency.toLowerCase() == toCurrency.toLowerCase()) {
        window[$txtExrate.attr("id").replace("_I", "")].SetValue(1)
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
                    window[$txtExrate.attr("id").replace("_I", "")].SetValue(exrate)
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

function DoCalculateForm() {
    //CalculateAirTicket()
    CalculateInvoice()
    CalculateOther()
    //CalculateRequestDetails($(".expense-amount.daily-allowance-amount :text"))
    CalculateRequestDetails($(".expense-amount.hotel-amount :text"))
    //    CalculateTransportation($(".transportation-fee :text:eq(0)")[0])
    //    CalculateTransportation($(".transportation-fee :text:eq(1)")[0])
    //    SummaryTransportation()
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
        var hideOverlay = true
        if (isOverBudget) {
            $("[id$=hApproveMessage]").val("It seem to be Over Budget! Do you want to continue?")
            $("[id$=btnCheckExtInvoice]").click()
            //showApproveMessage(me)
            hideOverlay = false
        }
        else if (isValidOraUser) {
            $("[id$=hApproveMessage]").val("")
            $("[id$=btnCheckExtInvoice]").click()
            //showApproveMessage(me)
            hideOverlay = false
        }
        if (hideOverlay) {
            $("[id$=UpdateProgress]").hide()
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
        ShowErrorMessage("Reject recommendation is required!")
        $txtRejectReason.addClass("validate-error").focus()
        return false
    }
    else {
        $txtRejectReason.removeClass("validate-error")
        return true
    }
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

function CalculateSummary() {
    var disparity = 0
    var dailyAllowance = ConvertToMoney($("[id$=lblExpenseDailyAllowance]").text())
    var hotel = ConvertToMoney($("[id$=lblExpenseHotelExpense]").text())
    var movingTimeAllowance = ConvertToMoney($("[id$=chkExpenseMovingTimeAllowance]").is(":checked") ? $("[id$=hExpenseMovingTime]").val() : 0)
    var firstTimeOverSea = ConvertToMoney($("[id$=chkExpenseFirstTimeOversea]").is(":checked") ? $("[id$=hExpenseFirstTime]").val() : 0)
    var other = ConvertToMoney($("[id$=hExpenseOtherAmount]").val())
    //
    $("[id$=lblExpenseMovingTimeAllowance]").text(FormatNumber(movingTimeAllowance))
    $("[id$=lblExpenseOtherExpense]").text(FormatNumber(other + firstTimeOverSea))
    //
    var expense = dailyAllowance + hotel + movingTimeAllowance + firstTimeOverSea + other
    //    
    var $lblTotalAmount = $("[id$=lblExpenseTotalExpense]")
    $lblTotalAmount.text(FormatNumber(expense))
    //
    var advance = ConvertToMoney($("[id$=lblCashAdvance]").text())
    var credit = ConvertToMoney($("[id$=lblCreditAdvance]").text())
    disparity = expense - (advance + credit)
    var actualDisparity = disparity
    var currency = $("[id$=ddlCurrency]").val()
    if (currency == 'vnd') {
        disparity = Math.round(disparity / 1000) * 1000
    }
    //
    var $lblDisparity = $("[id$=lblDisparity]")
    $lblDisparity.text(FormatNumber(disparity))
    //
    if (disparity != actualDisparity) {
        $lblDisparity.attr("title", "This disparity value was rounded by finance rule (Actual value is " + actualDisparity + ")")
    }
    else {
        $lblDisparity.attr("title", "")
    }
}

function CalculateRequest() {
    var breakfastAmount = ConvertToMoney($("[id$=hBreakfastAmount]").val())
    var breakfast = $("[id$=chkBreakfastAmount]").is(":checked") ? (breakfastAmount > 0 ? breakfastAmount : ConvertToMoney($("[id$=hBreakFastUnit]").val())) : 0
    spiBreakfastAmount.SetValue(breakfast)
    spiBreakfastAmount.SetMaxValue(SpinEditGenMaxValue(breakfast))
    var lunchAmount = ConvertToMoney($("[id$=hLunchAmount]").val())
    var lunch = $("[id$=chkLunchAmount]").is(":checked") ? (lunchAmount > 0 ? lunchAmount : ConvertToMoney($("[id$=hLunchUnit]").val())) : 0
    spiLunchAmount.SetValue(lunch)
    spiLunchAmount.SetMaxValue(SpinEditGenMaxValue(lunch))
    var dinnerAmount = ConvertToMoney($("[id$=hDinnerAmount]").val())
    var dinner = $("[id$=chkDinnerAmount]").is(":checked") ? (dinnerAmount > 0 ? dinnerAmount : ConvertToMoney($("[id$=hDinnerUnit]").val())) : 0
    spiDinnerAmount.SetValue(dinner)
    spiDinnerAmount.SetMaxValue(SpinEditGenMaxValue(dinner))
    var otherAmount = ConvertToMoney($("[id$=hOtherAmount]").val())
    var other = $("[id$=chkOtherAmount]").is(":checked") ? (otherAmount > 0 ? otherAmount : ConvertToMoney($("[id$=hOtherUnit]").val())) : 0
    spiOtherAmount.SetValue(other)
    spiOtherAmount.SetMaxValue(SpinEditGenMaxValue(other))
    //
    var isGMAndAbove = $("[id$=hIsGMAndAbove]").val() == "Y"
    if ($("[id$=ddlBTType]").val().indexOf("domestic") < 0 || isGMAndAbove) {
        window[$("[id$=spiHotelAmount]").attr("id")].SetMaxValue(SpinEditGenMaxValue(0))
    }
    else {
        window[$("[id$=spiHotelAmount]").attr("id")].SetMaxValue(SpinEditGenMaxValue(Math.max(window[$("[id$=spiHotelAmount]").attr("id")].GetNumber(), ConvertToMoney($("[id$=hHotelUnit]").val()))))
    }
    //
    CalculateRequestDetails($(".expense-amount.daily-allowance-amount :text"))
}

function chkCommonCCAmountChanged(onlyShow) {
    if ($("[id$=chkCommonCCAmount]").size()) {
        if ($("[id$=chkCommonCCAmount]").is(":checked")) {
            var commonAmount = $("[id$=spiCommonTotalAmount]").size() ? window[$("[id$=spiCommonTotalAmount]").attr("id")].GetNumber() : 0
            if (!onlyShow) {
                spiCommonCCAmount.SetValue(commonAmount)
            }
            spiCommonCCAmount.SetMaxValue(SpinEditGenMaxValue(commonAmount))
            $(".common-credit").show()
        }
        else {
            $(".common-credit").hide()
            spiCommonCCAmount.SetValue(null)
        }
    }
}

function chkHotelCCAmountChanged(onlyShow) {
    if ($("[id$=chkHotelCCAmount]").size()) {
        if ($("[id$=chkHotelCCAmount]").is(":checked")) {
            var hotelAmount = $("[id$=spiHotelAmount]").size() ? window[$("[id$=spiHotelAmount]").attr("id")].GetNumber() : 0
            if (!onlyShow) {
                spiHotelCCAmount.SetValue(hotelAmount)
            }
            spiHotelCCAmount.SetMaxValue(SpinEditGenMaxValue(hotelAmount))
            $(".hotel-credit").show()
        }
        else {
            $(".hotel-credit").hide()
            spiHotelCCAmount.SetValue(null)
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

var needScroll = false

function ClearRequestForm() {
    $("[id$=hExpenseRequestID]").val("")
    $("[id$=ddlExpenseDestinationLocation]").val("")
    dteDate.SetDate(dteExpenseDepartureDate.GetDate())
    $("[id$=chkBreakfastAmount]").prop("checked", true)
    $("[id$=chkLunchAmount]").prop("checked", true)
    $("[id$=chkDinnerAmount]").prop("checked", true)
    $("[id$=chkOtherAmount]").prop("checked", true)
    var $ddlCommonCurrency = $("[id$=ddlCommonCurrency]")
    $ddlCommonCurrency.val($("[id$=ddlCurrency]").val())
    if ($ddlCommonCurrency.size()) {
        $("[id$=lblCommonCCCurrency]").text($ddlCommonCurrency[0].options[$ddlCommonCurrency[0].selectedIndex].text)
    }
    $("[id$=dteDate]").attr("style", "float: left; width: 50px !important;")
    $("[id$=spiCommonCCAmount]").attr("style", "float: left; width: 50px !important;")
    $("[id$=lblCommonCCMessage]").val("")
    $("[id$=spiHotelCCAmount]").attr("style", "float: left; width: 50px !important;")
    $("[id$=lblHotelCCMessage]").val("")
    LoadExpenseNorm(true)
    CalculateRequest()
    $("[id$=chkCommonCCAmount]").prop("checked", false)
    chkCommonCCAmountChanged()
    window[$("[id$=spiHotelAmount]").attr("id")].SetValue(null)
    $("[id$=ddlHotelCurrency]").val($("[id$=ddlCurrency]").val())
    $("[id$=lblHotelCCCurrency]").text($("[id$=lblCommonCCCurrency]").text())
    if ($("[id$=dteHotelExchangeDate]").size()) {
        dteHotelExchangeDate.SetDate(dteExpenseDepartureDate.GetDate())
        window[$("[id$=spiHotelExrate]").attr("id")].SetValue(1)
        window[$("[id$=spiHotelTotalConverted]").attr("id")].SetValue(null)
    }
    $("[id$=chkHotelCCAmount]").prop("checked", false)
    chkHotelCCAmountChanged()
    //    
    needScroll = true
}

function LoadExpenseNorm(withHotel) {
    $("[id$=hBreakfastAmount]").val($("[id$=hBreakFastUnit]").val())
    $("[id$=hLunchAmount]").val($("[id$=hLunchUnit]").val())
    $("[id$=hDinnerAmount]").val($("[id$=hDinnerUnit]").val())
    $("[id$=hOtherAmount]").val($("[id$=hOtherUnit]").val())
    if (withHotel) {
        var hotelAmount = ConvertToMoney($("[id$=hHotelUnit]").val())//$("[id$=chkCredit]").is(":checked") ? 0 : ConvertToMoney($("[id$=hHotelUnit]").val())
        window[$("[id$=spiHotelAmount]").attr("id")].SetValue(hotelAmount)
    }
}

function ClearOtherForm() {
    $("[id$=hExpenseOtherID]").val("")
    dteExpenseOtherDate.SetDate(dteExpenseDepartureDate.GetDate())
    $("[id$=txtOtherExpense]").val("")
    window[$("[id$=spiExpenseOtherAmount]").attr("id")].SetValue(null)
    var $ddlExpenseOtherCurrency = $("[id$=ddlExpenseOtherCurrency]")
    $ddlExpenseOtherCurrency.val($("[id$=ddlCurrency]").val())
    if ($ddlExpenseOtherCurrency.size()) {
        $("[id$=lblOtherCCCurrency]").text($ddlExpenseOtherCurrency[0].options[$ddlExpenseOtherCurrency[0].selectedIndex].text)
    }
    $("[id$=spiOtherCCAmount]").attr("style", "float: left; width: 50px !important;")
    $("[id$=lblOtherCCMessage]").val("")
    $("[id$=chkOtherCCAmount]").prop("checked", false)
    chkOtherCCAmountChanged()
    if ($("[id$=spiExpenseOtherExrate]").size()) {
        window[$("[id$=spiExpenseOtherExrate]").attr("id")].SetValue(1)
        $("[id$=txtExpenseOtherAmountConverted]").val("0")
    }
    //
    needScroll = true
}

function ClearScheduleForm() {
    $("[id$=hExpenseScheduleID]").val("")
    var departure = dteExpenseDepartureDate.GetDate()
    dteExpenseScheduleDate.SetDate(departure)
    departure.setHours(8)
    departure.setMinutes(0)
    txeExpenseFromTime.SetDate(departure)
    departure.setHours(16)
    departure.setMinutes(45)
    txeExpenseToTime.SetDate(departure)
    $("[id$=txtExpenseWorkingArea]").val("")
    $("[id$=txtExpenseTask]").val("")
    spiTransportationFee.SetValue(null)
    //
    needScroll = true
}

function ConfirmRecall() {
    ShowConfirmMessage({
        message: "Are you sure to recall this BT Expense Declaration?",
        OK: function() {
            $("[id$=btnRecall]").click()
        }
    })
}