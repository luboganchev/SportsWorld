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
                        if($form.valid()) {
                            $.post("/Company/Field/Edit/", $form.serialize(), function (data) {
                                if (data.Success=="false") {
                                    $("#serverMessage").text(data.Message)
                                        .show().fadeOut(10000);

                                    $('html, body').animate({ scrollTop: $("#EditFieldPartial").offset().top }, 2000);
                                } else {
                                    $('#EditButton').show();
                                    $('#EditFieldPartial').hide();
                                }
                            })
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

            function updateView(data) {
                //TODO: Update Values
            }

            function hideEditButton() {
                $('#EditButton').hide();
            }
        });
    };


}(window.sportsWorld = window.sportsWorld || {}, jQuery));