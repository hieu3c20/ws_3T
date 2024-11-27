$(document).ready(function() {
    employeeCodeAutoComplete()
    //            
    if (btid) {
        var $tab = GetParent($(".grid-btn.viewDetails-btn[data-id='" + btid + "']"), ".HRTab")
        if ($tab.size()) {
            var $tabControl = $tab.parent().parent()
            $tabControl.find(".HRTab").hide()
            $tabControl.find(".HRTabNav li").removeClass("current")
            $tabControl.find(".HRTabNav li:eq(" + $tab.index() + ")").addClass("current")
            $tab.show()
        }
    }
    //
    GetOraInvoiceStatus()
    CheckCurrentOraStatus()
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
    $this.parent().parent().addClass("selected")
    var $oraErrorContainer = $("#oraErrorContainer")
    $("#oraErrorDetails").html($this.attr("data-message"))
    //var x = $this.offset().left
    var headerHeight = $("#header").outerHeight()
    _y = $this.offset().top - headerHeight - $oraErrorContainer.outerHeight() - 35
    $oraErrorContainer.css({ top: _y + 10, opacity: 0, display: "block" })
    .stop(true, false).animate({ opacity: 1, top: _y }, 100, "linear")
}

function hideErrorMessage() {
    $("#oraErrorContainer").stop(true, false)
    .animate({ opacity: 0, top: _y + 10 }, 100, "linear", function() {
        $("#oraErrorContainer").hide()
        $("#oraErrorDetails").html("")
        $("tr.selected").removeClass("selected")
    })
}

function CheckCurrentOraStatus() {
    if (!$(".ora-status-grid .dxgvTable_Office2010Black.updated").size()) {
        if ($(".ora-status-grid .dxgvTable_Office2010Black .dxgvDataRow_Office2010Black").size() > 0) {
            $(".ora-last-update").html("Oracle status last update: getting...")
            GetOraInvoiceStatus()
        }
        else {
            $(".ora-last-update").html("")
            setTimeout("CheckCurrentOraStatus()", 10)
        }
    }
    else {
        setTimeout("CheckCurrentOraStatus()", 10)
    }
}

function GetOraInvoiceStatus() {
    var $grid = $(".ora-status-grid .dxgvTable_Office2010Black")
    var btIDs = '_'
    $grid.find(".dxgvDataRow_Office2010Black").each(function() {
        btIDs += "," + $(this).find("td.id").text()//find(".grid-btn:eq(0)").attr("data-id")
    })
    btIDs = btIDs.replace("_,", "").replace("_", "")
    if (btIDs) {
        $.ajax({
            url: "/AjaxHandle.ashx?action=get-ora-invoice-status",
            async: true,
            type: "get",
            data: { btIDs: btIDs, type: 'e' }
        }).done(function(response) {
            if (response == "session") {
                location.reload()
            }
            else {
                try {
                    var result = JSON.parse(response)
                    var ids = result.ids
                    var status = result.status
                    var allIDs = btIDs.split(",")

                    for (var i = 0; i < allIDs.length; i++) {
                        if (ids.indexOf(allIDs[i]) == -1) {
                            ids.push(allIDs[i])
                            status.push("not-found")
                        }
                    }
                    for (var i = 0; i < ids.length; i++) {
                        //var $btn = $grid.find('.dxgvDataRow_Office2010Black .grid-btn[data-id = "' + ids[i] + '"]')
                        //var $tr = GetParent($btn[0], ".dxgvDataRow_Office2010Black")
                        var $td
                        $grid.find("td.id").each(function() {
                            if ($(this).text() == ids[i]) {
                                $td = $(this)
                                return false
                            }
                        })
                        var $tr = $td.parent()
                        $tr.removeClass("rejected").removeClass("done").removeClass("new").removeClass("deleted").removeClass("not-found").removeClass("paid")
                        var $btnReject = $tr.find(".reject-btn")
                        var $btnApprove = $tr.find(".approval-btn")
                        var $btnOraError = $tr.find(".ora-error-btn")
                        if (status[i] == "rejected") {
                            $btnOraError.show()
                        }
                        else {
                            $btnOraError.hide()
                        }
                        if (status[i] != "done" && status[i] != "paid") {
                            $btnReject.show()
                            $btnApprove.show()
                        }
                        else {
                            $btnReject.hide()
                            $btnApprove.hide()
                        }
                        $tr.addClass(status[i])
                    }
                    //
                    var now = new Date()
                    var hour = now.getHours() < 10 ? '0' + now.getHours() : now.getHours()
                    var minute = now.getMinutes() < 10 ? '0' + now.getMinutes() : now.getMinutes()
                    var second = now.getSeconds() < 10 ? '0' + now.getSeconds() : now.getSeconds()
                    $(".ora-last-update").html("Oracle status last update: " + hour + ':' + minute + ':' + second)
                    //
                    $grid.addClass("updated")
                    setTimeout("CheckCurrentOraStatus()", 10)
                }
                catch (e) {
                    ShowErrorMessage(response)
                }
                //setTimeout("GetOraInvoiceStatus()", 10)
            }
        }).fail(function(xhr, text, status) {
            ShowErrorMessage(text)
            //setTimeout("GetOraInvoiceStatus()", 10)
        }).always(function() {
        })
    }
    //    else {
    //        setTimeout("GetOraInvoiceStatus()", 1000)
    //    }
}

function employeeCodeAutoComplete() {
    //            var nhlTeams = ['Anaheim Ducks', 'Atlanta Thrashers', 'Boston Bruins', 'Buffalo Sabres', 'Calgary Flames', 'Carolina Hurricanes', 'Chicago Blackhawks', 'Colorado Avalanche', 'Columbus Blue Jackets', 'Dallas Stars', 'Detroit Red Wings', 'Edmonton OIlers', 'Florida Panthers', 'Los Angeles Kings', 'Minnesota Wild', 'Montreal Canadiens', 'Nashville Predators', 'New Jersey Devils', 'New Rork Islanders', 'New York Rangers', 'Ottawa Senators', 'Philadelphia Flyers', 'Phoenix Coyotes', 'Pittsburgh Penguins', 'Saint Louis Blues', 'San Jose Sharks', 'Tampa Bay Lightning', 'Toronto Maple Leafs', 'Vancouver Canucks', 'Washington Capitals'];

    //            $('.employee-code').devbridgeAutocomplete({
    //                lookup: nhlTeams,
    //                minChars: 1,
    //                onSelect: function(suggestion) {
    //                    //$('#selection').html('You selected: ' + suggestion.value);
    //                },
    //                showNoSuggestionNotice: true,
    //                noSuggestionNotice: 'Not found',
    //                maxHeight: 150
    //            });
}

function bindStartupEvent() {
    $(".btnSearch").removeAttr("data-status")
    checkSearchStatus()
}

var searchTimeOut
function checkSearchStatus() {
    if ($(".btnSearch").attr("data-status") == "done") {
        employeeCodeAutoComplete()
        SetNumberInputType()
        return
    }
    searchTimeOut = setTimeout('checkSearchStatus()', 10)
}

function confirmApproval(me) {
    var btID = $(me).parent().parent().find("td.id").text()
    var empCode = $(me).parent().parent().find("td.code").text()
    $("[id$='hApproveBTRegisterID']").val(btID)
    $("[id$='hApproveEmployeeCode']").val(empCode)
    //
    var isOverBudget = false
    var isValidOraUser = true
    $.ajax({
        url: "/AjaxHandle.ashx?action=check-expense-budget",
        async: true,
        type: "get",
        data: {
            btID: btID,
            budgetCode: $(me).attr("data-budget-code"),
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


function showRejectMessage(me) {    
     $(me).parent().parent().addClass("selected")
    $("[id$=tabRejectMessage]").stop().fadeIn(100)
    $('[id$=txtRejectReason]').focus()
}

function hideRejectMessage() {
    $("[id$=tabRejectMessage]").stop().fadeOut(100, function() {
        $("tr.selected").removeClass("selected")
    })
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

function CheckOraStatus(me) {
    var btID = $(me).parent().parent().find("td.id").text()
    $("[id$='hRejectBTRegisterID']").val(btID)
    $.ajax({
        url: "/AjaxHandle.ashx?action=check-expense-ora-status",
        async: true,
        type: "get",
        data: { btID: btID },
        cache: false,
        beforeSend: function() {
            $("[id$=UpdateProgress]").show()
        }
    }).done(function(response) {
        if (response == "session") {
            location.reload()
        } else if (response == "reject-allowed") {
            showRejectMessage(me)
        } else {
            ShowErrorMessage(response)
        }
    }).fail(function(xhr, text, status) {
        ShowErrorMessage(text)
    }).always(function() {
        $("[id$=UpdateProgress]").hide()
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

function showApproveMessage(me) {
    $(me).parent().parent().addClass("selected")
    //$("[id$=tabApproveMessage]").stop().fadeIn(100)
    $('[id$=dteGLDate_I]').focus()
}

function hideApproveMessage() {
    $("[id$=tabApproveMessage]").stop().fadeOut(100, function() {
        $("tr.selected").removeClass("selected")
    })
}

function btnViewBTClick(me) {
    var $tr = $(me).parent().parent()
    $("[id$=hItemID]").val($tr.find("td.id").text())    
}