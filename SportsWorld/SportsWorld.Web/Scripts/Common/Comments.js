(function (sportsWorld, $, undefined) {
    sportsWorld.commentSend = function (location) {
        $('#SendMessageButton').click(function () {
            if ($('#MessageBox').val().length == 0) {
                $('#ErrorMessage').text("You can't send empty message").show().fadeOut(5000);
            } else {
                var data = {
                    message: $('#MessageBox').val()
                };

                $.post(location, data, function (data) {
                    if (data.Success) {
                        window.location.reload();
                    } else {
                        $('#ErrorMessage').text(data.Message).show().fadeOut(5000);
                    }
                });
            }
        });
    }
}(window.sportsWorld = window.sportsWorld || {}, jQuery));