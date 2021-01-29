"use strict"

$('input[type=checkbox]').val('false').change(e => {
    $(e.delegateTarget).val($(e.delegateTarget).is(':checked'))
});

let filterElemsDic = {
    Number:  $('#numberProto'),
    Date: $('#dateProto'),
    Boolen: $('#boolenProto')
};

let filterFuncDic = {
    less : (a, b) => a < b,
    more : (a, b) => a > b,
    equal : (a, b) => a ==b
}

const getFilterElem = (fieldVM) => {
    let elem = $(filterElemsDic[fieldVM.type]).clone(true).removeAttr('id');
    elem.find('[data-prop=name]').text(fieldVM.name);
    elem.find('[name=filterProp]').attr('data-fieldId', fieldVM.fieldId)
    return elem;
};

let collectionFields = JSON.parse($('#filterTab').attr('data-fields'));
for (let i = 0; i < collectionFields.length; i++) {
    let filterElem = getFilterElem(collectionFields[i]);
    $('#filterTab').prepend(filterElem);
}

const filterItems = () => {
    let items = $('[name=item]');
    let filters = $('#filterTab [name=filterProp]').sort((a, b) => $(a).attr('data-fieldId') - $(b).attr('data-fieldId'));
    items.each((i, item) => {
        let fields = JSON.parse($(item).attr('data-fields')).sort((a, b) => a.fieldId - b.fieldId)
        filters.each((i, filter) => {
            let fieldId = $(filter).attr('data-fieldId');
            let field = fields.find(f => f.fieldId == fieldId)
            console.log(field)
            let op = $(filter).attr('data-op')
            let val = $(filter).val();
            let func = filterFuncDic[op];
            if (func(field.value, val) !== true) {
                $(item).hide();
            }

        });
    });
};

$('[name=filterProp]').change(e => {
    filterItems();
});