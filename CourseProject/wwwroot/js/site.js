jQuery.fn.showV = function () {
    this.css('visibility', 'visible');
};

jQuery.fn.hideV = function () {
    this.css('visibility', 'hidden');
};

const makeSearhTags = () => {
    $('[name=tag]').click(e => {
        e.preventDefault();
        let value = $(e.delegateTarget).attr('data-value');
        $('#searcher').val($('#searcher').val() + " " + value)
        $('#searcher').change();
    });
};

$('#searcher').change(async e => {
    console.log($(e.target).val());
    $('#searchSpinner').showV();
    $('main').fadeTo(300, 0.5);
    $('input, button').prop('disabled', true);
    await $.ajax({
        method: 'post',
        url: '/Store/SearchItems/',
        data: { text: $(e.target).val() },
        success: (data) => {
            $('main').empty().append(data);
            makeSearhTags();
        },
        error: (error) => console.log(error)
    });
    $('#searchSpinner').hideV();
    $('input, button').prop('disabled', false);
    $('main').fadeTo(300, 1.0);
});
$(() => {
    makeSearhTags();
});

$('#btnColorTheme').click(e => {
    e.preventDefault();
    $('html').attr('theme') == 'dark' ?
        $('html').attr('theme', 'light') :
        $('html').attr('theme', 'dark');
    $.cookie('theme', $('html').attr('theme'), { expires: 186, path: '/' });
});

$(() => {
    let theme = $.cookie('theme') ?? 'dark';
    $('html').attr('theme', theme);
});