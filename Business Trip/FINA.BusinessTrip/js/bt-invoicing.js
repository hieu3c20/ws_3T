$(document).ready(function() {
    BindCalculateRequestDetails()
})

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
            $("[id$=hInvoiceID]").val($(me).parent().parent().find("td.id").text()) //$(me).attr("data-id")
            clickDeleteButton(me)
        }
    })
}

function checkPostback(id) {
    if ($("#" + id).attr("data-status") == "done") {
        BindCalculateRequestDetails()
        SetNumberInputType()
        CalculateInvoice()
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

function BindCalculateRequestDetails() {
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
}

function btnAddSub_Click(me) {
    var $partialForm = $(me).parent().find(".add-edit-form")
    var $btnCancel = $partialForm.find(".btn-cancel-sub")
    $btnCancel.addClass("add-clicked").click()
    $partialForm.stop(true, true)
    $partialForm.find(".add-edit-action").text("Add")
    //            $partialForm.find(">*:not(.action-pan)").find(":text, select, textarea, :password, input[type='hidden']").val("")
    //            $partialForm.find(".total-amount").find(":text, input[type='hidden']").val("0")
    $partialForm.slideDown(100)
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

function ValidateInvoiceForm(me) {
    var isValid = ValidateRequire('InvoiceForm', 'InvoiceSummary')
    if (isValid) {
        $(me).parent().find("+:submit").click()
    }
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

function btnEditRequestClick(me) {
    $("[id$=hInvoiceID]").val($(me).parent().parent().find("td.id").text()) //$(me).attr("data-id")
    var $partialForm = GetParent(me, ".HRTab").find(".add-edit-form")
    if ($partialForm.size() > 1) {
        $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    }
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

function btnViewBTClick(me) {
    $("[id$=hItemID]").val($(me).parent().parent().find("td.btid").text()) //$(me).attr("data-id")
}
