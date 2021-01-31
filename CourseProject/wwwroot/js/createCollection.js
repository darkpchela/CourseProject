const getFieldElem = (props) => {
    let field = $('#fieldProto').clone(true).removeAttr('id');
    let nameInput = field.find('[data-prop=name]');
    let typeInput = field.children('[data-prop=type]');
    let idInput = field.children('[data-prop=id]');
    if (props) {
        nameInput.val(props.name);
        typeInput.val(props.typeId);
        idInput.val(props.id);
    }
    typeInput.removeAttr('id');
    return field;
};

const setFieldName = (field, id) => {
    let nameInputElem = $(field).find('[data-prop=name]');
    let typeInputElem = $(field).find('[data-prop=type]');
    let idInputElem = $(field).find('[data-prop=id]');
    nameInputElem.attr('name', `Fields[${id}].Name`);
    typeInputElem.attr('name', `Fields[${id}].TypeId`);
    idInputElem.attr('name', `Fields[${id}].Id`);
};

const updateFieldNames = () => {
    let fields = $('#fields [name=field]');
    for (let i = 0; i < fields.length; i++) {
        setFieldName(fields[i], i);
    }
};

$('#btnAddField').click(e => {
    e.preventDefault();
    let field = getFieldElem();
    $("#fields #btnAddField").before(field);
    updateFieldNames();
});

$('select').focus(e => {
    $(e.target).children('[value=""]').remove();
});

$('[name=btnDeleteField]').click(e => {
    e.preventDefault();
    $(e.target).parents('[name=field]').remove();
    updateFieldNames();
});

$('#changeImage').click(e => {
    e.preventDefault();
    $(e.target).parents('[name=imgContainer]').hide();
    $('#dzContainer').show();
    $.post('/api/abortUpload');
});

$(() => {
    let cashedFields = JSON.parse($('#cashedFields').text()) || [];
    for (let i = 0; i < cashedFields.length; i++) {
        let field = getFieldElem(cashedFields[i]);
        $('#fields #btnAddField').before(field);
    }
    updateFieldNames();
    let mkEditor = $('[data-mkd=editor]');
    let mkView = $('[data-mkd=view]');
    makeMarkDown(mkEditor, mkView);
});