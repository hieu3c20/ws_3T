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
})

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

function btnViewBTClick(me) {
    $("[id$=hItemID]").val($(me).parent().parent().find("td.id").text()) //$(me).attr("data-id")    
}