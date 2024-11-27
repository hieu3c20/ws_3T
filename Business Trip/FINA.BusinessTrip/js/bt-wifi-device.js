$(document).ready(function() {

})

function showRejectMessage(me) {
    $("[id$=hID]").val($(me).parent().parent().find("td.id").text())
    $("[id$=panRejectInfo]").stop().fadeIn(100)
    $('[id$=txtRejectReason]').focus()
}

function hideRejectMessage() {
    $("[id$=hID]").val("")
    $("[id$=panRejectInfo]").stop().fadeOut(100)
}

function checkReject() {
    var $txtRejectReason = $('[id$=txtRejectReason]')
    var rejectReason = $txtRejectReason.val()
    if (!rejectReason || rejectReason.trim().length === 0) {
        ShowErrorMessage("Reject comment is required!")
        $txtRejectReason.addClass("validate-error").focus()
        return false
    }
    else {
        $txtRejectReason.removeClass("validate-error")
        return true
    }
}

function showConfirmMessage(me) {
    $("[id$=hID]").val($(me).parent().parent().find("td.id").text())
    $("[id$=panConfirmInfo]").stop().fadeIn(100)
    $("[id$=txtConfirmComment]").focus()
}

function hideConfirmMessage() {
    $("[id$=hID]").val("")
    $("[id$=panConfirmInfo]").stop().fadeOut(100)
}

function showReturnMessage(me) {
    ShowConfirmMessage({
        message: "Are you sure to set this wifi device request as returned?",
        OK: function() {
            $("[id$=hID]").val($(me).parent().parent().find("td.id").text()) //$(me).attr("data-id")        
            $("[id$=btnReturn]").click()
        }
    })
}