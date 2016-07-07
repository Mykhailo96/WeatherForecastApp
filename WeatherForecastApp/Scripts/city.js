$(document).ready(function () {
    $('input[type="checkbox"]').on("click", function () {
        if ($(this).prop("checked") == true) {
            $("#editorId").prop("disabled", false);
            $("#editorId").prop("required", true);
            $("#enumId").attr("disabled", "disabled");
        }
        else if ($(this).prop("checked") == false) {
            $("#editorId").prop("disabled", true);
            $("#editorId").val("");
            $("#editorId").prop("required", false);
            $("#enumId").removeAttr("disabled", "disabled");
        }
    });
});

$("#buttonId").on("click", function () {
});