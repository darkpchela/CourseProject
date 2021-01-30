const getSelectedNames = () => $('#table input:checked').map((i, e) => $(e).attr('data-username')).get();
const getSelectedIds = () => $('#table input:checked').map((i, e) => $(e).attr('data-userId')).get();
const updateTable = () => {
    $('tr').each((i, e) => {
        let isBlocked = $(e).attr('data-isBlocked');
        let isAdmin = $(e).attr('data-isAdmin')
        if (isBlocked == 'true') {
            $(e).find('td[name=status]').text("Blocked");
        }
        else {
            $(e).find('td[name=status]').text("Unblocked");
        }
        if (isAdmin == 'true') {
            $(e).find('td[name=role]').text("Admin");
            $(e).find('[name=btnAddRules]').hide()
            $(e).find('[name=btnDelRules]').show()
        }
        else {
            $(e).find('td[name=role]').text("User");
            $(e).find('[name=btnAddRules]').show()
            $(e).find('[name=btnDelRules]').hide()
        }
    });
};

const makeDisabled = () => {
    $('button').prop('disabled', true);
    $('#main').fadeTo(300, 0.5);
    $('#spinner').show();
};

const makeEnabled = () => {
    $('button').prop('disabled', false);
    $('#main').fadeTo(300, 1.0);
    $('#spinner').hide();
};

$('#checkBoxMain').click(function () {
    $('#table input:checkbox').prop('checked', $(this).is(':checked') ? true : false);
});

$('#table input:checkbox').click(function () {
    $('#checkBoxMain').prop('checked', false);
});

$('#btnBlock').click(async e => {
    e.preventDefault();
    makeDisabled();
    await $.post('/admin/Block/',
        {
            users: getSelectedIds()
        },
        result => result.blocked.forEach((id) => $(`tr[data-userId=${id}]`).attr('data-isBlocked', 'true'))
    );
    makeEnabled();
    updateTable();
});

$('#btnUnblock').click(async e => {
    e.preventDefault();
    makeDisabled();
    await $.post('/admin/Unblock/',
        {
            users: getSelectedIds()
        },
        result => result.unblocked.forEach(id => $(`tr[data-userId=${id}]`).attr('data-isBlocked', 'false'))
    );
    makeEnabled();
    updateTable();
});

$('#btnDelete').click(e => {
    e.preventDefault();
    let names = getSelectedNames();
    let namesStr = "";
    names.forEach(name => namesStr += ` ${name}`);
    $('#users').text(namesStr);
});

$('#btnConfirmDelUsers').click(async e => {
    e.preventDefault();
    makeDisabled();
    await $.post('/admin/DeleteUsers/',
        {
            users: getSelectedIds()
        },
        result => result.deleted.forEach(id => $(`tr[data-userId=${id}]`).remove())
    );
    makeEnabled();
});

$('[name=btnAddRules]').click(async e => {
    e.preventDefault();
    makeDisabled();
    let id = $(e.delegateTarget).attr('data-userId');
    await $.post('/admin/AddAdminRules/',
        {
            userId: id,
        },
        result => $(`tr[data-userId=${result.userId}]`).attr('data-isAdmin', 'true')
    );
    updateTable();
    makeEnabled();
});

$('[name=btnDelRules]').click(async e => {
    e.preventDefault();
    makeDisabled();
    let id = $(e.delegateTarget).attr('data-userId');
    await $.post('/admin/DropAdminRules/',
        {
            userId: id
        },
        result => $(`tr[data-userId=${result.userId}]`).attr('data-isAdmin', 'false')
    );
    updateTable();
    makeEnabled();
});

$(() => {
    updateTable();
});