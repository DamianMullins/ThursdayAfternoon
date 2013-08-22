$(function () {
    if ($('form').length) {
        $('form:first').find('input:first').focus();
    }

    $('#fullscreen').on('click', function (e) {
        var target = $(this).data('target');
        e.preventDefault();
        $(target).toggleClass('fullscreen gradient-bg');
        $('.container').toggleClass('top-left');
    });

    $('[data-toggle]').tooltip();
});
