﻿const getInputFuncDictionary = {
    "Boolen": (value) => {
        if (value != 'true')
            value = false;
        let elem = $('<div />', {
            class: "d-inline-block text-center w-75"
        });
        let input = $('<input />', {
            type: 'hidden',
            'data-prop': 'value'
        });
        input.val(value);
        let checkbox = $('<input />', {
            type: 'checkbox',
            class: "form-control"
        });
        checkbox.prop('checked', value);
        checkbox.change(e => {
            e.preventDefault();
            $(e.target).is(':checked') ? input.val('true') : input.val('false');
        });
        elem.append(checkbox).append(input)
        return elem;
    },
    "Date": (value) => {
        let elem = $('<input />', {
            type: "date",
            class: "d-inline-block text-center form-control w-75",
            'data-prop': 'value'
        });
        elem.val(value);
        return elem;
    },
    "ShortString": (value) => {
        let elem = $('<input />', {
            type: "text",
            class: "d-inline-block text-center form-control w-75",
            'data-prop': 'value'
        });
        elem.val(value);
        return elem;
    },
    "LongString": (value) => {
        let elem = $('<textarea />', {
            class: "d-inline-block text-center form-control w-75",
            value: value,
            'data-prop': 'value'
        });
        elem.val(value);
        return elem;
    },
    "Number": (value) => {
        let elem = $('<input />', {
            type: 'number',
            class: "d-inline-block text-center form-control w-75",
            value: value,
            'data-prop': 'value'
        });
        elem.val(value);
        return elem;
    },
    "null": (value) => {
        let elem = $('<input />', {
            type: 'hidden'
        });
        elem.val(value);
        return elem;
    }
};
const fieldsElem = $('#fields');
const updateFieldNames = () => {
    let fields = $('#fields [name=field]');
    for (let i = 0; i < fields.length; i++) {
        setFieldName(fields[i], i);
    }
};
const setFieldName = (field, id) => {
    $(field).find('[data-prop=value]').attr('name', `Fields[${id}].Value`);
    $(field).find('[data-prop=fieldId]').attr('name', `Fields[${id}].FieldId`);
    $(field).find('[data-prop=name]').attr('name', `Fields[${id}].Name`);
    $(field).find('[data-prop=type]').attr('name', `Fields[${id}].Type`);
    $(field).find('[data-prop=itemFieldId]').attr('name', `Fields[${id}].ItemFieldId`);
};

const getFieldElem = (fieldVM) => {
    let field = $('#fieldProto').clone(true).removeAttr('id');
    field.find('[name=fieldName]').text(fieldVM.name);
    field.find('[data-prop=name]').val(fieldVM.name);
    field.find('[data-prop=fieldId]').val(fieldVM.fieldId);
    field.find('[data-prop=type]').val(fieldVM.type);
    field.find('[data-prop=itemFieldId]').val(fieldVM.itemFieldId);
    let inputElem = getInputFuncDictionary[fieldVM.type](fieldVM.value);
    field.find('[name=fieldBody]').append(inputElem);
    return field;
};
const loadFields = id => {
    fieldsElem.empty();
    $.post("/api/GetCollectionFields",
        { id: id },
        data => {
            for (let i = 0; i < data.length; i++) {
                let field = getFieldElem(data[i]);
                fieldsElem.append(field);
            }
            updateFieldNames();
        }
    );
};

$('#CollectionId').change(e => {
    $(e.target).children('[value=""]').remove();
    e.preventDefault();
    let id = $(e.target).val();
    loadFields(id);
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
        fieldsElem.append(field);
    }
    updateFieldNames();
    let mkEditor = $('[data-mkd=editor]');
    let mkView = $('[data-mkd=view]');
    makeMarkDown(mkEditor, mkView);
    parseMarkDownTo(mkEditor.val(), mkView);
});