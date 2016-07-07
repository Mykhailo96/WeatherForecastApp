$(document).ready(function () {
    $('input[type="checkbox"]').click(function () {
        if ($(this).prop("checked") == true) {
            $("#editorId").prop("disabled", false);
            $("#enumId").attr("disabled", "disabled");
        }
        else if ($(this).prop("checked") == false) {
            $("#editorId").prop("disabled", true);
            $("#editorId").val("");
            $("#enumId").removeAttr("disabled", "disabled");
        }
    });
});