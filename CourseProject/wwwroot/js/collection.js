let itemId;
$(() => {
    $('[name=btnDeleteItem]').click(e => {
        e.preventDefault();
        itemId = $(e.delegateTarget).attr('data-id');
        console.log(itemId)
        let name = $(`[name=item][data-id=${itemId}]`).attr('data-name');
        $('#delItemModal').find('#itemName').text(name);
    });
});

$('#confirmDelItem').click(async e => {
    e.preventDefault();
    $('#spinner').show();
    $('#view').fadeTo(300, 0.5);
    $('#view a').css('pointer-events', 'none');
    await $.post('/api/DeleteItem', { 'ItemId': itemId }, (result) => {
        if (result.succeed === true)
            $(`[name=item][data-id=${itemId}]`).remove()
        else
            console.log(result.errors)
    });
    $('#spinner').hide();
    $('#view a').css('pointer-events', 'auto');
    $('#view').fadeTo(300, 1.0);
});