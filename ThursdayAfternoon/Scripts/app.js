$(function () {
    if ($('form').length) {
        $('form:first').find('input:first').focus();
    }

    $('.make-fullscreen').on('click', function (e) {
        var target = $(this).data('target');
        e.preventDefault();
        $('.' + target).toggleClass('fullscreen gradient-bg');
    });
});
