const getFieldElem = (props) => {
    let field = $('#fieldProto').clone(true).removeAttr('id');
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

const deleteField = async (delFieldVM) => {
    let succeed = false;
    await $.post("/api/DeleteField", delFieldVM, result => {
        if (result.succeed === false)
            console.log(result.errors)
        succeed = result.succeed;
    });
    return succeed;
};

const createNewField = async (createFieldVM) => {
    await $.post("/api/CreateField", createFieldVM, result => {
        if (result.succeed === true) {
            let field = getFieldElem(result);
            field.find('select [value=""]').remove();
            $('#fields #btnAddField').before(field);
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
    await createNewField({
        CollectionId: collectionId,
        OwnerId: ownerId
    });
});

$('[name=btnDeleteField]').click(async (e) => {
    e.preventDefault();
    let collectionId = $('#CollectionId').val();
    let ownerId = $('#OwnerId').val();
    let fieldId = $(e.target).parents('[name=field]').find('[data-prop=id]').val();
    let result = await deleteField({
        CollectionId: collectionId,
        OwnerId: ownerId,
        OptionalFieldId: fieldId
    });
    if (result === true) {
        $(e.target).parents('[name=field]').remove();
        updateFieldNames();
    }
});

$('#type').focus(e => {
    $(e.target).children('[value=""]').remove();
});

$(() => {
    let cashedFields = JSON.parse($('#cashedFields').text());
    for (let i = 0; i < cashedFields.length; i++) {
        let field = getFieldElem(cashedFields[i]);
        $('#fields #btnAddField').before(field);
    }
    updateFieldNames();
});