$('#btnColorTheme').click(e => {
    e.preventDefault();
    $('html').attr('theme') == 'dark'?
     $('html').attr('theme', 'light'):
     $('html').attr('theme', 'dark');
});

$(() => {
    $('[data-toggle="tooltip"]').tooltip();
});