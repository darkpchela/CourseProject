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

const makeMarkDown = (jqEditor, jqView) => {
    jqEditor.markdownEditor();
    console.log(jqView.html());
    jqView.click(e => switchViewEditor(jqEditor, jqView));
    jqEditor.blur(e => switchViewEditor(jqEditor, jqView));
    jqEditor.change(e => parseMarkDownTo(marked(jqEditor.val()), jqView))
};

const switchViewEditor = (jqEditor, jqView) => {
    if (jqEditor.is(':hidden')) {
        jqView.attr('hidden', 'hidden');
        jqEditor.removeAttr('hidden');
        jqEditor.focus();
    }
    else if (jqEditor.val() && jqEditor.val().trim() != '') {
        jqEditor.attr('hidden', 'hidden');
        jqView.removeAttr('hidden');
    }
};

const parseMarkDownTo = (mkdStr, jqView) => {
    jqView.html(marked(mkdStr));
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
    makeSearhTags();
    $('[data-mkd-value]').each((i, e) => parseMarkDownTo($(e).attr('data-mkd-value'), $(e)))
});