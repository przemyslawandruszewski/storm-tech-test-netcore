// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    $('.auto-submit').on('click', function(e) {
        const form = e.target.closest('form');
        form.submit();
    });
});