// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function adjustHeaderTextSize() {
    var windowWidth = $(window).width();
    var headerTextSize = windowWidth / 10;
    var maxSize = 60;
    var newTextSize = Math.min(headerTextSize, maxSize);
    $('#header-text').css('font-size', newTextSize + 'px');
}