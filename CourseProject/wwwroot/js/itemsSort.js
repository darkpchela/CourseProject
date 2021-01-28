﻿let items = null;
let sortList = Array.prototype.sort.bind(items)

$('#sortProp, #sortPropParam').change(e => {
    let sortType = $('#sortProp').val();
    let param = $('#sortPropParam').val();
    let sort = sortType + param;
    items.sort(sortsDic[sort]);
    $('#items').empty().append(items);
});




const sortDateAsc = (a, b) => {
    let aDate = $(a).attr('data-date');
    let bDate = $(b).attr('data-date');
    return (aDate < bDate ? -1 : aDate > bDate ? 1 : 0);
};

const sortDateDesc = (a, b) => {
    let aDate = $(a).attr('data-date');
    let bDate = $(b).attr('data-date');
    return (aDate < bDate ? 1 : aDate > bDate ? -1 : 0);
};

const sortLikeAsc = (a, b) => {
    let aLikes = $(a).attr('data-likes');
    let bLikes = $(b).attr('data-likes');
    return (aLikes < bLikes ? -1 : aLikes > bLikes ? 1 : 0);
}

const sortLikeDesc = (a, b) => {
    let aLikes = $(a).attr('data-likes');
    let bLikes = $(b).attr('data-likes');
    return (aLikes < bLikes ? 1 : aLikes > bLikes ? -1 : 0);
}

const sortNameAsc = (a, b) => {
    let aName = $(a).attr('data-name');
    let bName = $(b).attr('data-name');
    return (aName < bName ? -1 : aName > bName ? 1 : 0);
};

const sortNameDesc = (a, b) => {
    let aName = $(a).attr('data-name');
    let bName = $(b).attr('data-name');
    return (aName < bName ? 1 : aName > bName ? -1 : 0);
};

const sortsDic = {
    dateAsc: sortDateAsc,
    dateDesc: sortDateDesc,
    likeAsc: sortLikeAsc,
    likeDesc: sortLikeDesc,
    nameAsc: sortNameAsc,
    nameDesc: sortNameDesc
};

$(() => {
    items = $('[name=item],[name=collection]');
    $('#sortProp').change();
});