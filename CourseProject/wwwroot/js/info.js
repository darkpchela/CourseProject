﻿let itemId;
let collectionId;

$('#btnGetCollections').click(e => {
    e.preventDefault();
    $('#items').hide();
    $('#collections').show();
});
$('#btnGetItems').click(e => {
    e.preventDefault();
    $('#collections').hide();
    $('#items').show();
});

$(() => {
    $('#btnGetCollections').click();

    $('[name=btnDeleteCollection]').click(e => {
        e.preventDefault();
        collectionId = $(e.delegateTarget).attr('data-id');
        let name = $(`[name=collection][data-id=${collectionId}]`).attr('data-name');
        $('#delCollectionModal').find('#collectionName').text(name);
    });
    $('[name=btnDeleteItem]').click(e => {
        e.preventDefault();
        itemId = $(e.delegateTarget).attr('data-id');
        let name = $(`[name=item][data-id=${itemId}]`).attr('data-name');
        $('#delItemModal').find('#itemName').text(name);
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

    $('#confirmDelCollection').click(async e => {
        e.preventDefault();
        $('#spinner').show();
        $('#view').fadeTo(300, 0.5);
        $('#view a').css('pointer-events', 'none');
        await $.post('/api/DeleteCollection/', { 'CollectionId': collectionId }, (result) => {
            if (result.succeed === true)
                $(`[name=collection][data-id=${collectionId}]`).remove()
            else
                console.log(result.errors)
        });
        $('#spinner').hide();
        $('#view a').css('pointer-events', 'auto');
        $('#view').fadeTo(300, 1.0);
    });

    $('[name=collection],[name=item]').hover(
        e => $(e.delegateTarget).find('[name=editTools]').show(),
        e => $(e.delegateTarget).find('[name=editTools]').hide()
    );
});