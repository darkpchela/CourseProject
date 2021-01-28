let connectionId;
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/itemHub")
    .build();

hubConnection.on("OnConnected", (id) => {
    connectionId = id;
    hubConnection.invoke("ConnectGroup", $('#itemId').val())
});

hubConnection.on("OnItemLiked", (result, id) => {
    if (result.succeed === true) {
        let count = $('#likesCount').text();
        result.result == 0 ? count++ : count--;
        $('#likesCount').text(count);
        if (id == connectionId) {
            if (result.result == 0) {
                $('#disliked').hide();
                $('#liked').show();
            }
            else {
                $('#liked').hide();
                $('#disliked').show();
            }
        }
    }
    else {
        console.log(result.errors)
    }
});

hubConnection.on("OnCommentMade", (result, id) => {
    if (result.succeed) {
        let comment = getCommentElem(result);
        $('#inputArea').before(comment);
    }
    else {
        console.log(result.errors)
    }
    if (connectionId == id) {
        $('#inputArea').find('textarea').prop('readonly', false).val('');
        $('#btnComment').prop('disabled', false);
    }
});

hubConnection.start();

const getCommentElem = (props) => {
    let elem = $('#commentProto').clone().removeAttr('id');
    elem.find('[data-prop=user]').text(props.userName);
    elem.find('[data-prop=value]').text(props.value);
    elem.find('[data-prop=date]').text(props.date);
    return elem;
};

$('#btnComment').click(e => {
    e.preventDefault();
    let itemId = $('#itemId').val();
    let value = $('#commentValue').val();
    $('#inputArea').find('input').prop('readonly', true);
    $('#btnComment').prop('disabled', true);
    hubConnection.invoke("SendComment", itemId, value);
});

$('#btnLike').click(e => {
    e.preventDefault();
    let itemId = $('#itemId').val();
    hubConnection.invoke("SendLike", itemId);
});

$(() => {
    if ($('#itemLiked').val() != "") {
        $('#disliked').hide();
        $('#liked').show();
    }
    else {
        $('#liked').hide();
        $('#disliked').show();
    }
});