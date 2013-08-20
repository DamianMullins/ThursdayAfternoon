$(function () {
    $('textarea.tinymce').tinymce({
        script_url: '/scripts/tinymce/tinymce.min.js',
        schema: 'html5',
        //inline: true,
        toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
        menubar: false,
        statusbar: false
    });
});