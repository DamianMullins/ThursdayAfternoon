$(function () {
    $('textarea.tinymce').tinymce({
        script_url: '/scripts/tinymce/tinymce.min.js',
        convert_fonts_to_spans : false,
        plugins : 'link code',
        toolbar: 'undo redo | bold italic | bullist numlist | link unlink | code',
        menubar: false,
        statusbar: false
    });
});