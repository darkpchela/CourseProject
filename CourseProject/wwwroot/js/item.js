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
            $('#likeSpinner').hide();
            $('#likesCount').show();
            $('#btnLike').prop('disabled', false);
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
    if (result.succeed === true) {
        console.log(result)
        let comment = getCommentElem(result);
        $('#inputArea').before(comment);
    }
    else {
        console.log(result.errors)
    }
    if (connectionId == id) {
        $('#inputArea').find('textarea').prop('readonly', false).val('');
        $('#commentSpinner').hide();
        $('#btnComment').show();
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
    $('#inputArea').find('textarea').prop('readonly', true);
    $('#btnComment').hide();
    $('#commentSpinner').show();
    hubConnection.invoke("SendComment", itemId, value);
});

$('#btnLike').click(e => {
    e.preventDefault();
    let itemId = $('#itemId').val();
    $('#btnLike').prop('disabled', true);
    $('#likesCount').hide();
    $('#likeSpinner').show();
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