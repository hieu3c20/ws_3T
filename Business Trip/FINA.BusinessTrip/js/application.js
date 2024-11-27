var accountMenuShowing = false
var messageSpeed = 200
var boardContentTop = 200
var hideMessageTimeout
var datepickerTimeout
var accountMenuStartY = -10
var _tooltipCount = 0
$(document).ready(function() {
    //title
    $("head > title").text("BTS - " + $("#page-title").text())
    //Mouse click on sub menu
    $(".quick-link, .quick-link-submenu").mouseup(function() {
        return false
    });

    $(".quick-link").mouseover(function() {
        $(".quick-link-submenu").stop(true, false)
        if (!$(".quick-link-submenu").is(":visible")) {
            $(".quick-link-submenu").css({ right: 30, opacity: 0, display: "block" })
        }
        $(".quick-link-submenu").animate({ right: 0, opacity: 1 }, 100, "linear")
    })
    //
    $(".btnAccount").mousedown(function() {
        var $btn = $(this)
        var $menu = $("#account-menu")
        $menu.stop(true, false)
        if (!accountMenuShowing) {
            $btn.addClass("active")
            $menu.addClass("clicked")
            if (!$menu.is(":visible")) {
                $menu.css({ top: accountMenuStartY, opacity: 0, display: "block" })
            }
            $menu.animate({ top: 14, opacity: 1 }, 100, "linear")
            accountMenuShowing = true
        }
    })
    // Start jquery for Acount management menu 
    $("#account-menu").mouseup(function() {
        return false
    })
    $(document).mouseup(function() {
        $(".quick-link-submenu").stop(true, false).animate({ right: 30, opacity: 0 }, 200, "linear", function() {
            $(".quick-link-submenu").css({ display: "none" })
        })
        //
        var $menu = $("#account-menu")
        if ($menu.hasClass("clicked")) {
            $menu.removeClass("clicked")
            return
        }
        $menu.stop(true, false)
        $(".btnAccount").removeClass("active")
        $menu.animate({ top: accountMenuStartY, opacity: 0 }, 200, "linear", function() {
            $menu.css({ display: "none" })
        })
        accountMenuShowing = false
    })
    //
    $("[role='tab']").click(function() {
        var $btn = $(this)
        if (!$("+[role='tabpanel']", $btn).is(":visible")) {
            $btn.addClass("active")
        } else {
            $btn.removeClass("active")
        }
        $("+[role='tabpanel']", $btn).slideToggle("fast")
    })
    //
    //BindDatePicker(".datepicker")
    //
    $(".btnLogout").click(function() {
        var href = $(this).attr("href")
        ShowConfirmMessage({
            message: "Do you want to logout?",
            OK: function() {
                location.href = href
            }
        })

        return false;
    })
    //
    SetNumberInputType()
    //
    BindTabControl()
    //
    BindTooltip()
    //
    BindViewGridDetails()
})
//
function BindTooltip() {
    $("*").mouseover(function() {
        if ($(this).hasClass("focused")) {
            return
        }
        var mode = $(this).attr("data-tooltip")
        if (mode == "hover" || mode == "all") {
            ShowTooltip($(this))
        }
    }).mouseout(function() {
        var mode = $(this).attr("data-tooltip")
        if (mode == "hover" || mode == "all") {
            if (!$(this).hasClass("focused")) {
                HideTooltip($(this))
            }
        }
    }).focus(function() {
        var mode = $(this).attr("data-tooltip")
        if (mode == "focus") {
            if (mode == "focus") {
                ShowTooltip($(this))
            }
        }
        else if (mode == "all") {
            $(this).addClass("focused")
        }
    }).blur(function() {
        var mode = $(this).attr("data-tooltip")
        $(this).removeClass("focused")
        if (mode == "focus" || mode == "all") {
            HideTooltip($(this))
        }
    })
}
//
function ShowTooltip($me) {
    if (!$me.attr("title") || !(/[^\s]+/.test($me.attr("title")))) {
        return false
    }
    $me.attr("data-title", $me.attr("title"))
    $me.attr("title", "")
    _tooltipCount++
    var tooltipID = 'bts-tooltip' + _tooltipCount
    $me.attr("data-tooltip-id", tooltipID)
    var div = '<div class="bts-tooltip" id="' + tooltipID + '"><i class="tooltip-indicator"></i>'
    div += $me.attr("data-title")
    div += '</div>'
    $("body").append(div)
    var $tooltip = $("#" + tooltipID)
    var tooltipWidth = $me.attr("data-tooltip-width")
    if (tooltipWidth == 'auto') {
        $tooltip.css("width", "auto")
    }
    else {
        $tooltip.width(Math.max($me.outerWidth() - 40, 360))
    }
    var elementTop = $me.offset().top + $me.outerHeight() + 8
    var elementLeft = $me.offset().left
    var tooltipLeft = elementLeft
    var screenWidth = $(window).width()
    if (elementLeft + $tooltip.outerWidth() > screenWidth) {
        tooltipLeft -= elementLeft + $tooltip.outerWidth() - screenWidth + 10
    }
    $tooltip.css({ top: elementTop, left: tooltipLeft })
    $tooltip.find(".tooltip-indicator").css("left", elementLeft - tooltipLeft + Math.min(100, $me.outerWidth() / 3))
    $tooltip.fadeIn("fast")
}
//
function HideTooltip($me) {
    $me.attr("title", $me.attr("data-title"))
    $me.removeAttr("data-title")
    var tooltipID = $me.attr("data-tooltip-id")
    $me.removeAttr("data-tooltip-id")
    var $tooltip = $("#" + tooltipID)
    $tooltip.remove()
}
//
function BindTabControl() {
    $(".HRTabControl > .HRTabNav > ul li:first-child").addClass("current")
    $(".HRTabControl > .HRTabList > .HRTab").addClass("no-transition")
    $(".HRTabControl > .HRTabList > .HRTab:first-child").show()
    if ($(".HRTabControl > .HRTabList > .HRTab:first-child").hasClass("allow-delete")) {
        $("#btnDeleteConfirm").show()
        //$("#btnCopyConfirm").show()
    }
    else if ($(".HRTabControl > .HRTabList > .HRTab:first-child").hasClass("deny-delete")) {
        $("#btnDeleteConfirm").hide()
        //$("#btnCopyConfirm").hide()
    }
    //
    $(".HRTabControl > .HRTabNav > ul li").click(function() {
        $(this).parent().find("li").removeClass("current")
        $(this).addClass("current")
        $(this).parent().parent().parent().find(" > .HRTabList > .HRTab").hide()
        var $currentTab = $(this).parent().parent().parent().find(" > .HRTabList > .HRTab:eq(" + $(this).index() + ")")
        $currentTab.show()
        if ($currentTab.hasClass("allow-delete")) {
            $("#btnDeleteConfirm").show()
            //$("#btnCopyConfirm").show()
        }
        else if ($currentTab.hasClass("deny-delete")) {
            $("#btnDeleteConfirm").hide()
            //$("#btnCopyConfirm").hide()
        }
    })
}
//
function CheckAll(me) {
    var checked = $(me).prop("checked")
    var $parent = $(me).parent().parent().parent()
    if ($parent.is("tbody")) {
        $parent = $parent.parent()
    }
    $parent = GetParent($parent[0], "td")
    var $checkbox = $(".chkSelect", $parent)
    $checkbox.each(function() {
        var $chk = $(this)
        if (!$chk.is(":checkbox")) {
            $chk = $chk.find(":checkbox")
        }
        if ($chk.is(":visible")) {
            $chk.prop("checked", checked)
            var $itemRow = $chk.parent().parent()
            if (checked) {
                $itemRow.addClass("selected")
            }
            else {
                $itemRow.removeClass("selected")
            }
        }
    })
}
//
function CheckboxChecked(me) {
    var $item = $(me)
    if (!$item.is(":checkbox")) {
        $item = $(me).find(":checkbox")
    }
    var checked = $item.prop("checked")
    var $itemRow = $(me).parent().parent()
    if (checked) {
        $itemRow.addClass("selected")
    }
    else {
        $itemRow.removeClass("selected")
    }
}

//function BindDatePicker(selector) {
//    $(selector + ":not(.binded)").addClass("binded").datepicker({
//        format: "dd-MMM-yyyy", // set output format
//        effect: "slide", // none, slide, fade
//        position: "bottom", // top or bottom,
//        otherDays: false,
//        locale: 'en' // 'ru' or 'en', default is $.Metro.currentLocale
//    });
//}

function ShowConfirmMessage(objMessage) {
    BuildMessageBoard(objMessage.message, "confirm")
    $("#btnBoardOK").click(function() {
        if ($(this).hasClass("clicked")) {
            return false
        }
        $(this).addClass("clicked")
        HideMessageBoard()
        try {
            objMessage.OK()
        }
        catch (ex) {
        }
    })

    $("#btnBoardCancel").click(function() {
        HideMessageBoard()
        try {
            objMessage.Cancel()
        }
        catch (ex) {
        }
    })
    ShowMessageBoard()
}
//
function ShowAlertMessage(message) {
    BuildMessageBoard(message, "alert")
    $("#btnBoardOK").click(function() {
        HideMessageBoard()
    })
    ShowMessageBoard()
}
//
function ShowMessageBoard() {
    //$("body").css("overflow", "hidden")
    $("#BTMessageBoardContent").css({ "margin-top": boardContentTop * 2 / 3, opacity: 0 })
    $("#BTMessageBoard").fadeIn(messageSpeed)
    $("#BTMessageBoardContent").animate({ "margin-top": boardContentTop, opacity: 1 }, messageSpeed, "linear")
}
//
function HideMessageBoard() {

    $("#BTMessageBoardContent").animate({ "margin-top": boardContentTop * 2 / 3, opacity: 0 }, messageSpeed + 100, "linear", function() {
        $("#BTMessageBoard").remove()
        //$("body").removeAttr("style")
    })
    $("#BTMessageBoard").fadeOut(messageSpeed + 100)
}
//
function BuildPartialMessageBoard() {
    var board = "<div class='partial-message-board' style='position: absolute; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(256, 256, 256, 0.5)'></div>"
    return board
}
//
function BuildMessageBoard(message, type) {
    message = message ? message : (type == "confirm" ? "Confirm?" : "Alert")
    $("#BTMessageBoard").remove()
    var okButton = '<div id="btnBoardOK" class="btn" style="float: right;">'
                            + '<span>Yes</span>'
                        + '</div>'
    var cancelButton = '<div id="btnBoardCancel" class="btn secondary" style="float: right; margin-left: 5px;">'
            + '<span>No</span>'
            + '</div>'
    var result = '<div id="BTMessageBoard" style="display: none; transition: none; position: fixed; background-color: rgba(0, 0, 0, 0.7); width: 100%; height: 100%; left: 0; top: 0; z-index: 9450;">'
                    + '<div id="BTMessageBoardContent" style="width: 30%; transition: none; min-width: 400px; margin: ' + boardContentTop + 'px auto; position: relative; background-color: #fff; padding: 15px; box-shadow: 0 0 10px #fff; -moz-box-shadow: 0 0 10px #fff; webkit-box-shadow: 0 0 10px #fff; border-radius: 5px; -webkit-border-radius: 5px; -moz-border-radius: 5px;">'
                        + '<div class="close-board" style="position: absolute; top: -5px; right: -5px; background: url(/images/close.png) center center no-repeat transparent; width: 20px; height: 20px; cursor: pointer;"></div>'
                        + '<div style="margin-bottom: 50px; font-weight: bold; line-height: 1.5;">'
                            + message
                        + '</div>'
                        + (type == "confirm" ? cancelButton : "")
                        + okButton
                        + '<div style="clear: both"></div>'
                    + '</div>'
               + '</div>'
    $("body").append(result)
    $(".close-board").click(function() {
        $("#btnBoardCancel").click()
        //HideMessageBoard()
    })
}
//
function ShowInfoMessage(message) {
    BuildMessage(message, true)
    ShowMessage()
}
//
function ShowErrorMessage(message, showTime) {
    BuildMessage(message, false)
    ShowMessage(showTime)
}
//
function ShowMessage(showTime) {
    $("#PushMessage").fadeIn(messageSpeed, function() {
        hideMessageTimeout = setTimeout("HideMessage()", (showTime ? showTime : 5000))
    })
}
//
function HideMessage() {
    hideMessageTimeout = null
    $("#PushMessage").fadeOut(messageSpeed + 100, function() {
        $("#PushMessage").remove()
    })
}
//
function TruncString(str, length, suffix) {
    if (str.length > length) {
        str = str.substr(0, length) + suffix
    }
    return str
}
//
function BuildMessage(message, isInfo) {
    message = TruncString(message, 120, "...")
    $("#PushMessage").remove()
    clearTimeout(hideMessageTimeout)
    hideMessageTimeout = null
    var result = '<div id="PushMessage" style="display: none; transition: none; position: fixed; top: 150px; left: 134px; width: 100%; text-align: center; z-index: 9500"><span id="PushMessageContent" style="white-space: nowrap; padding: 8px 25px; color: #fff; border-radius: 15px; font-size: 1.2em; -webkit-border-radius: 15px; -moz-border-radius: 15px; box-shadow: 0 0 10px #666; -webkit-box-shadow: 0 0 10px #666; -moz-box-shadow: 0 0 10px #666; background-color: ' + (isInfo ? '#2DB63A' : '#CB3737') + '; ">' + message + '</span></div>'
    $("body").append(result)
}
//
var CheckMessageStatusTimeout
function CheckMessageStatus(id) {
    if ($("#" + id).attr("data-status") == "done") {
        BindTooltip()
        BindViewGridDetails()
        if ($("#PushMessage").size()) {
            ShowMessage()
        }
        CheckMessageStatusTimeout = null
        return
    }
    CheckMessageStatusTimeout = setTimeout("CheckMessageStatus('" + id + "')", 10)
}
function BindViewGridDetails() {
    $(".dxgvTable_Office2010Black tr").dblclick(function() {
        $(this).find(".viewDetails-btn, .edit-btn").click()
    })
}
//
function HandleMessage(me) {
    $(me).removeAttr("data-status")
    $("#PushMessage").remove()
    clearTimeout(hideMessageTimeout)
    var id = $(me).attr("id")
    CheckMessageStatus(id)
}
//set input type pattern
function SetNumberInputType() {
    var acceptance = [8, 9, 46, 37, 38, 39, 40]
    $("input[data-type='number']").keydown(function(e) {
        if (e.keyCode == 190) {
            if ($(this).val().indexOf(".") >= 0) {
                return false;
            }
        }
        else if (acceptance.indexOf(e.keyCode) < 0 && (e.keyCode < 48 || (e.keyCode > 57 && e.keyCode < 96) || (e.keyCode > 105 && e.keyCode < 112) || e.keyCode > 123)) {
            return false
        }
    })
    //
    $("input[data-type='int']").keydown(function(e) {
        if (acceptance.indexOf(e.keyCode) < 0 && (e.keyCode < 48 || (e.keyCode > 57 && e.keyCode < 96) || (e.keyCode > 105 && e.keyCode < 112) || e.keyCode > 123)) {
            return false
        }
    })
}

function FormatNumber(number) {
    if (!number || isNaN(number)) {
        return 0
    }
    //alert((parseFloat(number) - parseInt(number)).toString())
    var prefix = number < 0 ? "-" : ""
    number = Math.abs(number)
    var formatedNumber = ""
    var behindFloat = (parseFloat(number) - parseInt(number)).toFixed(2)
    if (parseInt(behindFloat * 100) > 0) {
        formatedNumber = "." + behindFloat.toString().replace(/(^0\.)|(0+$)/g, "")
    }
    var strNumber = parseInt(number).toString()
    while (true) {
        if (strNumber.length > 3) {
            formatedNumber = "," + strNumber.substr(strNumber.length - 3) + formatedNumber
            strNumber = strNumber.substr(0, strNumber.length - 3)
        }
        else {
            formatedNumber = strNumber + formatedNumber
            break
        }
    }
    return prefix + formatedNumber
}

function GetParent(me, selector) {
    var $parents = $(me).parentsUntil(selector)
    if ($parents.size() === 0) {
        return $(me).parent()
    }
    return $($parents[$parents.size() - 1]).parent()
}

function ValidateRequire(containerID, messageSummaryID) {
    $("#" + messageSummaryID).html("")
    var isValid = true
    $("#" + containerID).find(".validate-required").each(function() {
        var $input = $(this).find(":text, select, textarea")
        var $inputContainer
        if ($(this).hasClass("date-time-picker") || $(this).hasClass("spin-edit")) {
            $inputContainer = $(this).find(">.dxeButtonEditSys.dxeButtonEdit")
        }
        else {
            $inputContainer = $input
        }
        var inputValue = $input.val()
        //
        if ($(this).hasClass("spin-edit") && inputValue == '0') {
            inputValue = ""
        }
        //
        if (!inputValue || inputValue.trim().length === 0) {
            isValid = false
            if (!$inputContainer.hasClass("validate-error")) {
                $inputContainer.addClass("validate-error")
            }
        }
        else {
            $inputContainer.removeClass("validate-error")
        }
    })
    if (!isValid) {
        $("#" + messageSummaryID).append("<li>Complete field(s) with red mark</li>")
    }
    return isValid
}

function ConvertToMoney(obj) {
    var money = 0
    try {
        obj = obj.replace(/,/g, '')
        money = parseFloat(obj)
    }
    catch (e) {
        money = 0
    }
    if (isNaN(money) || money < 0) {
        money = 0
    }
    return money
}

function SpinEditGenMaxValue(obj) {
    return obj < 1 ? 1000000000000 : obj
}

function HideGlobalMessage() {
    $(".global-message").fadeOut('fast')
    $.ajax({
        url: "/AjaxHandle.ashx?action=hide-global-message",
        async: true,
        type: "get"
    }).done(function(response) {
        if (response == "session") {
            location.reload()
        }
    }).fail(function(xhr, text, status) {
        ShowErrorMessage(text)
    }).always(function() {
        $("[id$=UpdateProgress]").hide()
    })
}

function hidePartialForms() {
    $(".add-edit-form").stop(true, true).slideDown(100)
}