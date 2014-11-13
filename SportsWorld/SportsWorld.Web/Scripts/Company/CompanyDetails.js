$(function (sportsWorld, $, undefined) {
    $('#EditPartialContainer').hide();

    sportsWorld.companyDetails = function () {
        $('#EditButton').on('click', function () {
            $('#EditPartialContainer').show();
            $('#EditButton').hide();
        });

        $('#cancel').on('click', function () {
            $('#EditPartialContainer').hide();
            $('#EditButton').show();
        });
    }

    sportsWorld.companyDetails();
}(window.sportsWorld = window.sportsWorld || {}, jQuery));