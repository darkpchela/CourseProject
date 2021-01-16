Dropzone.autoDiscover = false;
$('#dropzone').dropzone({
    addRemoveLinks: true,
    maxFiles: 1,
    maxFileSeize: 20,
    method: 'post',
    url: '/api/UploadImage',
    previewTemplate: $('#tpl').html(),
    success: (file, response) => {
        console.log(response);
        if (response.succeed === true) {
            $('#dropzone img').attr('src', response.url);
            $('#ResourceId').val(response.id);
            $('#ImageUrl').val(response.url);
        }
        else {
            console.log(response.error);
        }
    },
    removedfile: () => {
        $.post('/api/AbortUpload');
        $('#dropzone .dz-preview').remove();
    }
});
$(() => {
    if ($('#ImageUrl').val() != "") {
        $('#dzContainer').hide()
        $('[name=imgContainer]').show();
    };
});