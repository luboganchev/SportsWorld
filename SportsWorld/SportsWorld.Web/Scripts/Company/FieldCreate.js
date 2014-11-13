$(function (sportsWorld, $, undefined) {
    sportsWorld.init = function () {
        $('#sliderInput').slider().on('slide', function (ev) {
            $('#sliderInput').attr('value', ev.value);
        });

        var inputID = "#inputCitiesAutocomplete";
        $(inputID).autocomplete({
            source: function (request, response) {
                $.getJSON(
                   "http://gd.geobytes.com/AutoCompleteCity?callback=?&q=" + request.term,
                   function (data) {
                       var filteredData = [];
                       if (data.length && data[0] != "") {
                           for (var city in data) {
                               var citiInfoArray = data[city].split(",");
                               var usefulCityInfo = citiInfoArray[0] + "," + citiInfoArray[2];
                               filteredData.push(usefulCityInfo);
                           }
                       }

                       response(filteredData);
                   }
                );
            },
            minLength: 3,
            select: function (event, ui) {
                var selectedObj = ui.item;
                $(inputID).val(selectedObj.value);
                return false;
            },
            open: function () {
                $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
            },
            close: function () {
                $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
            }
        });
        $(inputID).autocomplete("option", "delay", 100);
    }

    sportsWorld.init();
}(window.sportsWorld = window.sportsWorld || {}, jQuery));