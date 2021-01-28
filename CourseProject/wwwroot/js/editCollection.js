const getFieldElem = (props) => {
    let field = $('#fieldProto').clone(true).removeAttr('id').attr('data-id', props.fieldId).attr('data-name', props.name);
    let nameInput = field.children('[data-prop=name]');
    let typeInput = field.children('[data-prop=type]');
    let idInput = field.children('[data-prop=id]');
    if (props) {
        nameInput.val(props.name);
        typeInput.val(props.typeId);
        idInput.val(props.fieldId);
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
    idInputElem.attr('name', `Fields[${id}].FieldId`);
};

const updateFieldNames = () => {
    let fields = $('#fields [name=field]');
    for (let i = 0; i < fields.length; i++) {
        setFieldName(fields[i], i);
    }
};

const createNewField = async (createFieldVM) => {
    await $.post("/api/CreateField", createFieldVM, result => {
        if (result.succeed === true) {
            let field = getFieldElem(result);
            field.find('select [value=""]').remove();
            $('#fields #addFieldArea').before(field);
            updateFieldNames();
        }
        else {
            console.log(result.errors);
        }
    });
};

$('#btnAddField').click(async e => {
    e.preventDefault();
    let collectionId = $('#CollectionId').val();
    let ownerId = $('#OwnerId').val();
    $('#fields *').prop('disabled', true);
    $('#addSpinner').show();
    await createNewField({
        CollectionId: collectionId,
        OwnerId: ownerId
    });
    $('#addSpinner').hide();
    $('#fields *').prop('disabled', false);
});

let fieldData;
$('[name=btnDeleteField]').click(async (e) => {
    e.preventDefault();
    let field = $(e.target).parents('[name=field]');
    fieldData = {
        collectionId: $('#CollectionId').val(),
        ownerId: $('#OwnerId').val(),
        fieldId: field.attr('data-id')
    };
    $('#fieldNameModal').text(field.attr('data-name'));
});

$('#confirmDelField').click(async (e) => {
    e.preventDefault();
    $('#fields *').prop('disabled', true);
    $('#delSpinner').show();
    await $.post("/api/DeleteField", fieldData, result => {
        if (result.succeed === true) {
            $(`[name=field][data-id=${fieldData.fieldId}]`).remove();
            updateFieldNames();
        }
        else {
            console.log(result.errors)
        }
    });
    $('#delSpinner').hide();
    $('#fields *').prop('disabled', false);
});

$('#type').focus(e => {
    $(e.target).children('[value=""]').remove();
});

$('#changeImage').click(e => {
    e.preventDefault();
    $(e.target).parents('[name=imgContainer]').hide();
    $('#dzContainer').show();
    $.post('/api/abortUpload');
});

$(() => {
    let cashedFields = JSON.parse($('#cashedFields').text());
    for (let i = 0; i < cashedFields.length; i++) {
        let field = getFieldElem(cashedFields[i]);
        $('#fields #addFieldArea').before(field);
    }
    updateFieldNames();
});