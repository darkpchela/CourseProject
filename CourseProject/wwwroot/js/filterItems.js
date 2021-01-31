$('input[type=checkbox]').val('').change(e => {
    $(e.delegateTarget).val($(e.delegateTarget).is(':checked')? 'true': '')
});

let filterElemsDic = {
    Number:  $('#numberProto'),
    Date: $('#dateProto'),
    Boolen: $('#boolenProto')
};

let filterFuncDic = {
    numMore: (a, b) => Number(a) >= Number(b),
    numLess: (a, b) => Number(a) <= Number(b),
    dateMore: (a, b) => new Date(a) >= new Date(b),
    dateLess: (a, b) => new Date(a) <= new Date(b),
    equal: (a, b) => a == b 
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
    let filters = $('#filterTab [name=filterProp]');
    items.each((i, item) => {
        let fields = JSON.parse($(item).attr('data-fields'));
        let filtered = false;
        filters.each((i, filter) => {
            let fieldId = $(filter).attr('data-fieldId');
            let field = fields.find(f => f.fieldId == fieldId)
            let op = $(filter).attr('data-op')
            let val = $(filter).val();
            if (!field || val=="") {
                return true;
            }
            let func = filterFuncDic[op];
            if (func(field.value, val) === false) {
                filtered = true
                return false;
            }
        });
        if (filtered === true)
            $(item).hide();
        else 
            $(item).show();
    });
};

$('[name=filterProp]').change(e => {
    filterItems();
});