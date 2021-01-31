let collections = null;
let sortList = Array.prototype.sort.bind(collections)

$('#sortProp, #sortPropParam').change(e => {
    let sortType = $('#sortProp').val();
    let param = $('#sortPropParam').val();
    let sort = sortType + param;
    collections.sort(sortsDic[sort]);
    $('#collections').empty().append(collections);
});




const sortDateAsc = (a, b) => {
    let aDate = new Date($(a).attr('data-date') / 1000);
    let bDate = new Date($(b).attr('data-date') / 1000);
    return (aDate < bDate ? -1 : aDate > bDate ? 1 : 0);
};

const sortDateDesc = (a, b) => {
    let aDate = new Date($(a).attr('data-date') / 1000);
    let bDate = new Date($(b).attr('data-date') / 1000);
    return (aDate < bDate ? 1 : aDate > bDate ? -1 : 0);
};

const sortLikeAsc = (a, b) => {
    let aLikes = Number($(a).attr('data-likes'));
    let bLikes = Number($(b).attr('data-likes'));
    return (aLikes < bLikes ? -1 : aLikes > bLikes ? 1 : 0);
}

const sortLikeDesc = (a, b) => {
    let aLikes = Number($(a).attr('data-likes'));
    let bLikes = Number($(b).attr('data-likes'));
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

const sortCountAsc = (a, b) => {
    let aCount = Number($(a).attr('data-count'));
    let bCount = Number($(b).attr('data-count'));
    return (aCount < bCount ? -1 : aCount > bCount ? 1 : 0);
};

const sortCountDesc = (a, b) => {
    let aCount = Number($(a).attr('data-count'));
    let bCount = Number($(b).attr('data-count'));
    return (aCount < bCount ? 1 : aCount > bCount ? -1 : 0);
};

const sortsDic = {
    dateAsc: sortDateAsc,
    dateDesc: sortDateDesc,
    likeAsc: sortLikeAsc,
    likeDesc: sortLikeDesc,
    nameAsc: sortNameAsc,
    nameDesc: sortNameDesc,
    countAsc: sortCountAsc,
    countDesc: sortCountDesc
};

$(() => {
    collections = $('[name=collection]');
    $('#sortProp').change();
});