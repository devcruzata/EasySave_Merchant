$(function () {
    $("#loginBtn").click(function () {
        $("#messagepanel").attr("style", "display:none"),
        $("#userid").val().length > 0 && $("#password").val().length ? __doPostBack("btnLogin", "OnClick") : $("#messagepanel").attr("style", "display:block")
    })
}),
$(function () {
    $("#userid").val().length > 0 && $("#password").focus()
});