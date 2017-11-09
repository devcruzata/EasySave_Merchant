        $(function () {
            $('#loginBtn').click(function () {
                $('#messagepanel').attr("style", "display:none");
                if ($('#userid').val().length > 0 && $('#password').val().length) {
                    __doPostBack('btnLogin', 'OnClick');
                }
                else {
                    $('#messagepanel').attr("style", "display:block");
                }
            });
        });

    $(function () {
        if ($('#userid').val().length > 0)
            $('#password').focus();
    });
