// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(function () {
        $("#search").click(function () {
            var location = $('#location').val();
            $.get("/Home/GetWeather", { location: location }, function (result) {
                $("#weather").html(result);
            });
        });
        $("#clean").click(function () {
            $.get("/Home/CleanRecords", null, function (result) {
                $("#weather").html(result);
            });
        });
    });
});
