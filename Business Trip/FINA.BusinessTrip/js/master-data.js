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
        ScrollDocument()
        return
    }
    setTimeout("checkPostback('" + id + "')", 10)
}

function btnAddSub_Click(me) {
    $(".btn-cancel-sub").each(function() {
        btnCancelSub_Click($(this)[0])
    })
    var $partialForm = $(me).parent().find(".add-edit-form")
    var $btnCancel = $partialForm.find(".btn-cancel-sub")
    $btnCancel.addClass("add-clicked").click()
    $partialForm.stop(true, true)
    $partialForm.find(".add-edit-action").text("Add")
    $partialForm.slideDown(100)
    scrollTop = $partialForm.offset().top - 250
}

function btnCancelSub_Click(me) {
    $('.error-summary').html('')
    if ($(me).hasClass("add-clicked")) {
        $(me).removeClass("add-clicked")
        return
    }
    var $partialForm = $(me).parent().parent().parent()
    $partialForm.stop(true, true)
    $partialForm.slideUp(100, function() {
        //$partialForm.find(".add-edit-action").text("")
    })
}

function HandlePartialMessageBoard(me) {
    var $partialForm = $(me).parent().parent().parent()
    // $partialForm.append(BuildPartialMessageBoard())
    $(me).removeAttr("data-status").removeAttr("data-process")
    CheckPartialMessageBoard($(me).attr("id"))
}

function CheckPartialMessageBoard(id) {
    if ($("#" + id).attr("data-status") == "done") {
        var $partialForm = $("#" + id).parent().parent().parent()
        if ($("#" + id).attr("data-process") == "success") {
            $partialForm.slideUp(100, "linear", function() {
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

function btnEditClick(me, idField) {
    $(".btn-cancel-sub").each(function() {
        btnCancelSub_Click($(this)[0])
    })
    $("[id$=" + idField + "]").val($(me).parent().parent().find("td.id").text()) //$(me).attr("data-id")
    var $partialForm = GetParent(me, ".HRTab").find(".add-edit-form")
    if ($partialForm.size() > 1) {
        $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    }
    $partialForm.find(".add-edit-action").text("Edit")
    $partialForm.stop(true, true)
    $partialForm.slideDown(100)
    $('.error-summary').html('')
    scrollTop = $partialForm.offset().top - 250
}

function btnDeleteClick(me, idField) {
    ShowConfirmMessage({
        message: "Are you sure to delete this record?",
        OK: function() {
            $("[id$=" + idField + "]").val($(me).parent().parent().find("td.id").text()) //$(me).attr("data-id")
            clickDeleteButton(me)
        }
    })
}

function clickDeleteButton(me) {
    //    var $partialForm = GetParent(me, ".HRTab").find(".add-edit-form")
    //    if ($partialForm.size() > 1) {
    //        $partialForm = GetParent(me, ".tab-container").find(".add-edit-form")
    //    }
    //    var $btnCancel = $partialForm.find(".btn-cancel-sub")
    //    btnCancelSub_Click($btnCancel[0])
    $(".btn-cancel-sub").each(function() {
        btnCancelSub_Click($(this)[0])
    })
    $(me).find("+:submit").click()
}

function check_Country() {
    var isValid = true
    var $mesCountry = $('#ulCountryMessage')
    $mesCountry.html('')

    var $txtCoutryCode = $('[id$=txtCountryCode]')
    var $txtCountryName = $('[id$=txtCountryName]')

    if (!$txtCoutryCode.val()) {
        $mesCountry.append('<li>Please input country code</li>')
        $txtCoutryCode.addClass('validate-error')
        isValid = false
    } else {
        $txtCoutryCode.removeClass('validate-error')
    }

    if (!$txtCountryName.val()) {
        $mesCountry.append('<li>Please input country name</li>')
        $txtCountryName.addClass('validate-error')
        isValid = false
    } else {
        $txtCountryName.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveCountry]').click()
    }
}

function check_mCountry() {
    var isValid = true
    var $mesCountry = $('#ulmCountryMessage')
    $mesCountry.html('')

    var $ddlCountry = $('[id$=cboCountry]')
    var $ddlGroup = $('[id$=ddlDesGroup]')
    var $txtDesName = $('[id$=txtaDesName]')

    if (!$ddlCountry.val()) {
        $mesCountry.append('<li>Please choose country</li>')
        $ddlCountry.addClass('validate-error')
        isValid = false
    } else {
        $ddlCountry.removeClass('validate-error')
    }

    if (!$ddlGroup.val()) {
        $mesCountry.append('<li>Please choose group</li>')
        $ddlGroup.addClass('validate-error')
        isValid = false
    } else {
        $ddlGroup.removeClass('validate-error')
    }

    if (!$txtDesName.val()) {
        $mesCountry.append('<li>Please input destination name</li>')
        $txtDesName.addClass('validate-error')
        isValid = false
    } else {
        $txtDesName.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveDestination]').click()
    }
}

function check_mInvoiceItem() {
    var isValid = true
    var $mesItem = $('#ulmInvoiceItemMessage')
    $mesItem.html('')
    var $txtItemName = $('[id$=txtInvoiceItemName]')

    if (!$txtItemName.val()) {
        $mesItem.append('<li>Please input invoice item name</li>')
        $txtItemName.addClass('validate-error')
        isValid = false
    } else {
        $txtItemName.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveItem]').click()
    }
}

function check_mDestinationGroup() {
    var isValid = true
    var $mesDesGroup = $('#ulmDesGroupMessage')
    $mesDesGroup.html('')
    var $txtGroupName = $('[id$=txtDesGroupName]')

    if (!$txtGroupName.val()) {
        $mesDesGroup.append('<li>Please input group name</li>')
        $txtGroupName.addClass('validate-error')
        isValid = false
    } else {
        $txtGroupName.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveDesGroup]').click()
    }
}

function check_mTitleGroup() {
    var isValid = true
    var $mesTitleGroup = $('#ulmTitleGroupMessage')
    $mesTitleGroup.html('')
    var $txtTitleName = $('[id$=txtTitleGroupName]')
    var $ddlTitleGroup = $('[id$=ddlTitle_I]')

    if (!$txtTitleName.val()) {
        $mesTitleGroup.append('<li>Please input title group name</li>')
        $txtTitleName.addClass('validate-error')
        isValid = false
    } else {
        $txtTitleName.removeClass('validate-error')
    }

    if (!$ddlTitleGroup.val()) {
        $mesTitleGroup.append('<li>Please choose titles</li>')
        $('[id$=ddlTitle]').addClass('validate-error')
        isValid = false
    } else {
        $('[id$=ddlTitle]').removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveTitleGroup]').click()
    }
}

function check_mExpense() {
    var isValid = true
    var $mesExpense = $('#ulmExpMessage')
    $mesExpense.html('')
    var $ddlJobBand = $('[id$=ddlJobBand]')
    //var $ddlDestinationGroup = $('[id$=ddlDestinationGroup]')
    var $ddlBTType = $("[id$=ddlExpenseBTType]")
    var $dteEffectiveDate = $("[id$=dteExpenseEffectiveDate_I]")
    if (!$ddlJobBand.val()) {
        $mesExpense.append('<li>Please choose title group</li>')
        $ddlJobBand.addClass('validate-error')
        isValid = false
    } else {
        $ddlJobBand.removeClass('validate-error')
    }

    //    if (!$ddlDestinationGroup.val()) {
    //        $mesExpense.append('<li>Please choose destination group</li>')
    //        $ddlDestinationGroup.addClass('validate-error')
    //        isValid = false
    //    } else {
    //        $ddlDestinationGroup.removeClass('validate-error')
    //    }

    if (!$ddlBTType.val()) {
        $mesExpense.append('<li>Please choose expense type</li>')
        $ddlBTType.addClass('validate-error')
        isValid = false
    } else {
        $ddlBTType.removeClass('validate-error')
    }

    if (!$dteEffectiveDate.val()) {
        $mesExpense.append('<li>Please select effective date</li>')
        $dteEffectiveDate.addClass('validate-error')
        isValid = false
    } else {
        $dteEffectiveDate.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveExpense]').click()
    }
}

function checkOraSupplier() {
    if (!$("[id$=grvGetDataOraSupplier]").find(".chkSelect:checked").size()) {
        ShowErrorMessage("You must choose at least 1 supplier!")
    }
    else {
        var selectSuppliers = ""
        $("[id$=grvGetDataOraSupplier]").find(".chkSelect:checked").each(function() {
            selectSuppliers = ";" + $(this).parent().parent().find("td.supplierNo").text() + "|" + $(this).parent().parent().find("td.supplierName").text() + selectSuppliers
        })
        if (selectSuppliers.trim().length > 0) {
            selectSuppliers = selectSuppliers.substr(1)
        }
        $("[id$=hSelectSuppliers]").val(selectSuppliers)
        $("[id$=btnSaveSupplier]").click()
    }
}

function check_mAirPeriod() {
    var isValid = true
    var $mesItem = $('#ulmAirPeriodMessage')
    $mesItem.html('')
    var $txtAirPeriodName = $('[id$=txtAirName]')

    if (!$txtAirPeriodName.val()) {
        $mesItem.append('<li>Please input air period name</li>')
        $txtAirPeriodName.addClass('validate-error')
        isValid = false
    } else {
        $txtAirPeriodName.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveAirPeriod]').click()
    }
}

function check_mBatchName() {
    var isValid = true
    var $mesItem = $('#ulmBatchNameMessage')
    $mesItem.html('')
    var $txtBatchName = $('[id$=txtBatchName]')

    if (!$txtBatchName.val()) {
        $mesItem.append('<li>Please input batch name</li>')
        $txtBatchName.addClass('validate-error')
        isValid = false
    } else {
        $txtBatchName.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveBatchName]').click()
    }
}

function check_mDailyRate() {
    var isValid = true
    var $mesItem = $('#ulmDailyRateMessage')
    $mesItem.html('')
    var $ddlFromCurrency = $('[id$=ddlFromCurrency]')
    var $ddlToCurrency = $('[id$=ddlToCurrency]')
    var $Rate = $('[id$=speConversionRate_I]')
    var $Date = $('[id$=dteConversionDate_I]')

    if (!$ddlFromCurrency.val()) {
        $mesItem.append('<li>Please choose from currency</li>')
        $ddlFromCurrency.addClass('validate-error')
        isValid = false
    } else {
        $ddlFromCurrency.removeClass('validate-error')
    }

    if (!$ddlToCurrency.val()) {
        $mesItem.append('<li>Please choose to currency</li>')
        $ddlToCurrency.addClass('validate-error')
        isValid = false
    } else {
        $ddlToCurrency.removeClass('validate-error')
    }

    if (!$Rate.val() || $Rate.val() == '0') {
        $mesItem.append('<li>Please choose conversion rate</li>')
        $Rate.addClass('validate-error')
        isValid = false
    } else {
        $Rate.removeClass('validate-error')
    }

    if (!$Date.val()) {
        $mesItem.append('<li>Please choose conversion date</li>')
        $Date.addClass('validate-error')
        isValid = false
    } else {
        $Date.removeClass('validate-error')
    }

    //            if (!$txtBatchName.val()) {
    //                $mesItem.append('<li>Please input Batch name</li>')
    //                $txtBatchName.addClass('validate-error')
    //                isValid = false
    //            } else {
    //                $txtBatchName.removeClass('validate-error')
    //            }

    if (isValid) {
        $('[id$=btnSaveDailyRate]').click()
    }
}

function check_mCompanyName() {
    var isValid = true
    var $mesItem = $('#ulmCompanyNameMessage')
    $mesItem.html('')
    var $txtCompanyName = $('[id$=txtCompanyName]')
    var $txtCompanyTaxCode = $('[id$=txtCompanyTaxCode]')

    if (!$txtCompanyName.val()) {
        $mesItem.append('<li>Please input seller name</li>')
        $txtCompanyName.addClass('validate-error')
        isValid = false
    } else {
        $txtCompanyName.removeClass('validate-error')
    }

    if (!$txtCompanyTaxCode.val()) {
        $mesItem.append('<li>Please input seller tax code</li>')
        $txtCompanyTaxCode.addClass('validate-error')
        isValid = false
    } else {
        $txtCompanyTaxCode.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveCompany]').click()
    }
}

function check_mCountryGroup() {
    var isValid = true
    var $mesCountryGroup = $('#ulmCountryGroupMessage')
    $mesCountryGroup.html('')
    var $txtCountryGroupName = $('[id$=txtCountryGroupName]')

    if (!$txtCountryGroupName.val()) {
        $mesCountryGroup.append('<li>Please input country group name</li>')
        $txtCountryGroupName.addClass('validate-error')
        isValid = false
    } else {
        $txtCountryGroupName.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveCountryGroup]').click()
    }
}

function check_mBudget() {
    var isValid = true
    var $mesBudget = $('#ulmBudMessage')
    $mesBudget.html('')
    var $txtOrg = $('[id$=txtOrg]')
    var $ddlDept = $('[id$=ddlDept]')
    var $txtBudName = $('[id$=txtBudgetName]')
    var $txtBudCode = $('[id$=txtBudgetCode]')
    var $txtBudAmount = $('[id$=txtBudgetAmount_I]')
    //var $ddlBudgetType = $('[id$=ddlBGType]')
    //
    if (!$txtOrg.val()) {
        $mesBudget.append('<li>Please input Org</li>')
        $txtOrg.addClass('validate-error')
        isValid = false
    } else {
        $txtOrg.removeClass('validate-error')
    }
    //
    if (!$ddlDept.val()) {
        $mesBudget.append('<li>Please choose department</li>')
        $ddlDept.addClass('validate-error')
        isValid = false
    } else {
        $ddlDept.removeClass('validate-error')
    }

    if (!$txtBudName.val()) {
        $mesBudget.append('<li>Please input budget name</li>')
        $txtBudName.addClass('validate-error')
        isValid = false
    } else {
        $txtBudName.removeClass('validate-error')
    }

    if (!$txtBudCode.val()) {
        $mesBudget.append('<li>Please input budget code</li>')
        $txtBudCode.addClass('validate-error')
        isValid = false
    } else {
        $txtBudCode.removeClass('validate-error')
    }

    if (!$txtBudAmount.val()) {
        $mesBudget.append('<li>Please input budget amount</li>')
        $txtBudAmount.addClass('validate-error')
        isValid = false
    } else {
        $txtBudAmount.removeClass('validate-error')
    }

    //    if (!$ddlBudgetType.val()) {
    //        $mesBudget.append('<li>Please select budget type</li>')
    //        $ddlBudgetType.addClass('validate-error')
    //        isValid = false
    //    } else {
    //        $ddlBudgetType.removeClass('validate-error')
    //    }

    if (isValid) {
        $('[id$=btnSaveBudget]').click()
    }
}

function check_budgetPIC() {
    var isValid = true
    var $mesBudgetPIC = $('#ulmBudgetPICMessage')
    $mesBudgetPIC.html('')

    var $txtBudgetPICOrg = $('[id$=txtBudgetPICOrg]')
    var $txtBudgetPICEmail = $('[id$=txtBudgetPICEmail]')

    if (!$txtBudgetPICOrg.val()) {
        $mesBudgetPIC.append('<li>Please input Org</li>')
        $txtBudgetPICOrg.addClass('validate-error')
        isValid = false
    } else {
        $txtBudgetPICOrg.removeClass('validate-error')
    }

    if (!$txtBudgetPICEmail.val()) {
        $mesBudgetPIC.append('<li>Please input P.I.C Email</li>')
        $txtBudgetPICEmail.addClass('validate-error')
        isValid = false
    } else {
        $txtBudgetPICEmail.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveBudgetPIC]').click()
    }
}

function check_mAllowance() {
    var isValid = true
    var $mesItem = $('#ulmAllowanceMessage')
    $mesItem.html('')
    var $ddlCountry = $('[id$=ddlAllowanceCountryGroup]')
    var $ddlCurrency = $('[id$=ddlAllowanceCurrency]')
    var $Amount = $('[id$=speAllowanceAmount_I]')


    if (!$ddlCountry.val()) {
        $mesItem.append('<li>Please choose group country</li>')
        $ddlCountry.addClass('validate-error')
        isValid = false
    } else {
        $ddlCountry.removeClass('validate-error')
    }

    if (!$Amount.val() || $Amount.val() == '0') {
        $mesItem.append('<li>Please choose amount</li>')
        $Amount.addClass('validate-error')
        isValid = false
    } else {
        $Amount.removeClass('validate-error')
    }

    if (!$ddlCurrency.val()) {
        $mesItem.append('<li>Please choose currency</li>')
        $ddlCurrency.addClass('validate-error')
        isValid = false
    } else {
        $ddlCurrency.removeClass('validate-error')
    }

    if (isValid) {
        $('[id$=btnSaveAllowance]').click()
    }
}

function showImportMessage(me) {
    var $this = $(me)
    $("[id$='hImportBudgetID']").val($this.attr("data-id"))
    $this.parent().parent().addClass("selected")
    $("#tabImportMessage").stop().fadeIn(100)
    $("[id$=hImportBudgetID]").val($(me).attr("data-reject"))
}

function hideImportMessage() {
    $("[id ='hImportBudgetID']").val("")
    $("#tabImportMessage").stop().fadeOut(100, function() {
        $("tr.selected").removeClass("selected")
    })
}

function ImportExcelBudget() {
    var $btnImport = $("#btnImportBudget")
    var $file = $("#fImportExcelBudget")
    if ($btnImport.hasClass("uploading")) {
        return
    }
    var choosedFiles = $file[0].files
    if (choosedFiles.length == 0) {
        return
    }
    var data = new FormData()
    for (i = 0; i < choosedFiles.length; i++) {
        var regex = /^[0-9a-zA-Z_ \[\]\(\)\.]+$/
        if (!regex.test(choosedFiles[i].name)) {
            ShowErrorMessage("File name is invalid.")
            return
        }
        data.append("file" + i, choosedFiles[i])
    }
    $btnImport.addClass("uploading")
    $.ajax({
        url: "/ImportAjaxHandle.ashx?action=import-budget",
        async: true,
        type: "post",
        data: data,
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function() {
            $btnImport.val("Importing...")
        }
    }).done(function(response) {
        if (response == "session") {
            location.reload()
        } else if (response == "success") {
            ShowInfoMessage("Success")
            $("[id$=btnLoadBudget]").click()
        }
        else {
            ShowErrorMessage(response)
        }
    }).fail(function(xhr, text, status) {
        ShowErrorMessage(text)
    }).always(function() {
        $btnImport.val("Import")
        $btnImport.removeClass("uploading")
        //ValidateSubmit()
    })
}