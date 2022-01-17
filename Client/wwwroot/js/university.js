$(document).ready(function () {
    var table = $('#uniTable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                text: '<i class="fa fa-files-o"></i>',
                exportOptions: {
                    columns: [0, 1]
                }
            },
            {
                extend: 'csv',
                text: '<i class="fa fa-file-text-o"></i>',
                exportOptions: {
                    columns: [0, 1]
                }
            },
            {
                extend: 'excel',
                text: '<i class="fa fa-file-excel-o"></i>',
                exportOptions: {
                    columns: [0, 1]
                }
            },
            {
                extend: 'pdf',
                text: '<i class="fa fa-file-pdf-o"></i>',
                orientation: 'portrait',
                title: 'Registered Data',
                pageSize: 'LEGAL',
                exportOptions: {
                    columns: [0, 1]
                }
            },
            {
                extend: 'print',
                text: '<i class="fas fa-print"></i>',
                title: 'Registered Data',
                exportOptions: {
                    columns: [0, 1]
                }
            }
        ],
        'ajax': {
            'url': 'https://localhost:44351/universities/getall',
            'dataType': 'json',
            'dataSrc': ''
        },
        'columns': [
            {
                'data': 'id'
            },
            {
                'data': 'name'
            },
            {
                "data": null,
                'bSortable': false,
                "defaultContent": `<button class="btn btn-sm btn-outline-secondary" data-toggle="modal" data-target="#insertModal" id="btn-edit"><i class="fas fa-edit"></i></button>
                                   <button class="btn btn-sm btn-outline-danger" id="btn-delete"><i class="fas fa-trash"></i></button>`
            }
        ]
    });
    $('#uniTable').on('click', '#btn-delete', function () {
        var data = table.row($(this).closest('tr')).data();
        deleteUni(data.id);
    });
    $('#uniTable').on('click', '#btn-edit', function () {
        var data = table.row($(this).closest('tr')).data();
        editUni(data);
    });
    $('#universityForm').on('click', '#btn-insert', function () {
        table.ajax.reload();
    });
    $('#universityForm').on('click', '#btn-update', function () {
        table.ajax.reload();
    });
});

function editUni(data) {
    $.ajax({
        url: 'https://localhost:44351/universities/get/' + data.id
    }).done((uni) => {
        $("#universityId").val(data.id);
        $("#uniName").val(data.name);
        //$('#employeeModal').modal('show');
    }).fail((error) => {
        console.log(error)
    })
}

function insertUni() {
    var obj = new Object();
    obj.ID = $("#universityId").val()
    obj.Name = $("#uniName").val()

    console.log(obj);

    $.ajax({
        url: 'https://localhost:44351/universities/post',
        type: "POST",
        data: obj
    }).done((result) => {
        console.log(result);
        Swal.fire({
            title: 'Input Success',
            text: 'Insert data Success!',
            icon: 'success'
        })
        $('#insertModal').modal('hide');
        table.ajax.reload()
    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
            footer: `<a href=${'https://httpstatuses.com/' + error.status} target="_blank"/>Why do I have this issue?</a>`
        })

    })
}

//function updateUni() {
//    var obj = new Object();
//    obj.Name = $("#uniName").val()
//    console.log(obj)
//    $.ajax({
//        url: 'https://localhost:44351/universities/put/' + $("#universityId").val(),
//        type: "PUT",
//        data: obj
//    }).done((result) => {
//        console.log(result);
//        Swal.fire({
//            title: 'Input Success',
//            text: 'Insert data Success!',
//            icon: 'success'
//        })
//        $('#insertModal').modal('hide');
//        table.ajax.reload()
//    }).fail((error) => {
//        console.log(error);
//        Swal.fire({
//            icon: 'error',
//            title: 'Oops...',
//            text: 'Something went wrong!',
//            footer: `<a href=${'https://httpstatuses.com/' + error.status} target="_blank"/>Why do I have this issue?</a>`
//        })
//    })
//}

function updateUni() {
    var obj = new Object();
    obj.id = $("#universityId").val()
    obj.name = $("#uniName").val()
    console.log(obj)
    $.ajax({
        url: 'https://localhost:44351/universities/update/',
        type: "PUT",
        data: obj
    }).done((result) => {
        console.log(result);
        Swal.fire({
            title: 'Input Success',
            text: 'Insert data Success!',
            icon: 'success'
        })
        $('#insertModal').modal('hide');
        table.ajax.reload()
    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
            footer: `<a href=${'https://httpstatuses.com/' + error.status} target="_blank"/>Why do I have this issue?</a>`
        })
    })
}

function deleteUni(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: 'https://localhost:44351/universities/delete/' + id,
                type: "DELETE",
                contentType: "application/json;charset=utf-8",
                traditional: true,
                data: ""
            }).done((result) => {
                console.log(result);
                Swal.fire({
                    title: 'Delete Success',
                    //text: 'Input Success!',
                    icon: 'success'
                })
                $('#insertModal').modal('hide');
                table.ajax.reload()
            }).fail((error) => {
                console.log(error);
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!',
                    footer: `<a href=${'https://httpstatuses.com/' + error.status} target="_blank"/>Why do I have this issue?</a>`
                })
            })
        }
    })
}

(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                else {
                    insertUni();
                    $('#insertModal').modal('hide');
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

$('#insertModal').on('hidden.bs.modal', function (e) {
    document.getElementById('universityForm').classList.remove('was-validated');
    $('#universityForm')
        .find("input[type=text]")
        .val("");
})