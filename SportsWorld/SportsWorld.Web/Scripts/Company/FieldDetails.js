(function (sportsWorld, $, undefined) {
    sportsWorld.fieldDetails = function (id) {
        $('#EditButton').click(function () {
            if ($('#EditFieldPartial').is(':empty')) {
                $('#EditFieldPartial')
                .load('/Company/Field/Edit/' + id, function () {
                    sportsWorld.init();
                    hideEditButton();
                    var $form = $('#editDataForm');

                    $.validator.unobtrusive.parse($form);
                    $($form).validate();

                    $('#updateButton').on('click', function () {
                        if ($form.valid()) {
                            var token = $('[name=__RequestVerificationToken]').val();
                            var headers = {};
                            headers["__RequestVerificationToken"] = token;

                            $.ajax( {
                                url: '/Company/Field/Edit/',
                                type: 'POST',
                                headers: headers,
                                data: new FormData($form[0]),
                                processData: false,
                                contentType: false
                            }).done(function (data) {
                                if (!data.Success) {
                                    $("#serverMessage").text(data.Message)
                                        .show().fadeOut(10000);

                                    $('html, body').animate({ scrollTop: $("#EditFieldPartial").offset().top }, 2000);
                                } else {
                                    window.location.reload();

                                    $('#EditButton').show();
                                    $('#EditFieldPartial').hide();
                                }
                            });
                        }
                    });

                    $('#cancel').on('click', function () {
                        $('#EditButton').show();
                        $('#EditFieldPartial').hide();
                    })
                });
            } else {
                $('#EditFieldPartial').show();
                hideEditButton();
            }

            function hideEditButton() {
                $('#EditButton').hide();
            }
        });
    };
}(window.sportsWorld = window.sportsWorld || {}, jQuery));