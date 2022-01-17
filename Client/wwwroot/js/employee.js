$(document).ready(function () {
    var count = 0
    var table = $('#EmployeeTable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                text: '<i class="fa fa-files-o"></i>',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                extend: 'csv',
                text: '<i class="fa fa-file-text-o"></i>',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                extend: 'excel',
                text: '<i class="fa fa-file-excel-o"></i>',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                extend: 'pdf',
                text: '<i class="fa fa-file-pdf-o"></i>',
                orientation: 'portrait',
                title: 'Registered Data',
                pageSize: 'LEGAL',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                extend: 'print',
                text: '<i class="fas fa-print"></i>',
                title: 'Registered Data',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            }
        ],
        'ajax': {
            'url': 'https://localhost:44351/employees/getall',
            'dataType': 'json',
            'dataSrc': ''
        },
        'columns': [
            {
                'data': null,
                'render': function (data, type, row, meta) {
                    return (meta.row + meta.settings._iDisplayStart + 1)
                }
            },
            {
                'data': 'firstName'
            },
            {
                'data': 'lastName'
            },
            {
                'data': 'phone',
                'bSortable': false
            },
            {
                'data': 'email',
                'bSortable': false
            },
            {
                'data': null
            }
        ],
        "columnDefs": [{
            "targets": -1,
            "data": null,
            'bSortable': false,
            "defaultContent": `<button class="btn btn-sm btn-outline-primary" id="btn-details"><i class="fas fa-info-circle"></i></button>
                               <button class="btn btn-sm btn-outline-secondary" data-toggle="modal" data-target="#insertModal" id="btn-edit"><i class="fas fa-edit"></i></button>
                               <button class="btn btn-sm btn-outline-danger" id="btn-delete"><i class="fas fa-trash"></i></button>
                               `
        }]
    });
    $('#EmployeeTable').on('click', '#btn-delete', function () {
        var data = table.row($(this).closest('tr')).data();
        deleteEmployee(data.nik);
    });
    $('#EmployeeTable').on('click', '#btn-edit', function () {
        var data = table.row($(this).closest('tr')).data();
        document.getElementById('btn-update').style.visibility = 'visible';
        document.getElementById('btn-insert').style.visibility = 'hidden';
        document.getElementById('btn-insert').style.display = 'none';
        document.getElementById('btn-update').style.display = 'inline';
        document.getElementById('employeeForm').classList.remove('was-validated');
        editEmployee(data);
    });
    $('#EmployeeTable').on('click', '#btn-details', function () {
        var data = table.row($(this).closest('tr')).data();
        detailEmployee(data);
    });
    setInterval(function () {
        table.ajax.reload();
    }, 3000);
});

$('.btn-add').on('click', function () {
    document.getElementById('btn-insert').style.visibility = 'visible';
    document.getElementById('btn-update').style.visibility = 'hidden';
    document.getElementById('btn-update').style.display = 'none';
    document.getElementById('btn-insert').style.display = 'inline';
    document.getElementById('employeeForm').classList.remove('was-validated');
}
);

function detailEmployee(data) {
    $.ajax({
        url: 'https://localhost:44351/employees/get/' + data.nik
    }).done((employee) => {
        console.log(employee);
        var text = `<tr>
                        <td>NIK : </td>
                        <td>${employee.nik}</td>
                   </tr>
                    <tr>
                        <td>Full Name : </td>
                        <td>${employee.firstName} ${employee.lastName}</td>
                   </tr>
                    <tr>
                        <td>Gender : </td>
                        <td>${employee.gender}</td>
                   </tr>
                    <tr>
                        <td>Birth Date : </td>
                        <td>${employee.birthDate}</td>
                   </tr>
                    <tr>
                        <td>phone : </td>
                        <td>${employee.phone}</td>
                   </tr>
                    <tr>
                        <td>email : </td>
                        <td>${employee.email}</td>
                   </tr>
                    <tr>
                        <td>Salary : </td>
                        <td>Rp.${employee.salary}</td>
                   </tr>`
        $("#infoTable").html(text)
        $('#employeeModal').modal('show');
    }).fail((error) => {
        console.log(error)
    })
}

function editEmployee(data) {
    $.ajax({
        url: 'https://localhost:44351/employees/get/' + data.nik
    }).done((employee) => {
        console.log(employee);
        $("#NIK").val(data.nik);
        $("#firstName").val(data.firstName);
        $("#lastName").val(data.lastName);
        $("#genderSelect").val(data.gender);
        $("#birthDate").val(data.birthDate);
        $("#phoneInput").val(data.phone);
        $("#emailInput").val(data.email);
        $("#salaryInput").val(data.salary);
        //$('#employeeModal').modal('show');

    }).fail((error) => {
        console.log(error)
    })
}

//function updateEmployee() {
//    var obj = new Object();
//    obj.nik = $("#NIK").val();
//    obj.firstName = $("#firstName").val();
//    obj.lastName = $("#lastName").val();
//    obj.birthDate = $("#birthDate").val();
//    obj.phone = $("#phoneInput").val();
//    obj.email = $("#emailInput").val();
//    obj.gender = parseInt($("#genderSelect").val());
//    obj.salary = parseInt($("#salaryInput").val());

//    $.ajax({
//        url: 'https://localhost:44321/api/employee/',
//        type: "PUT",
//        contentType: "application/json",
//        data: JSON.stringify(obj)
//    }).done((result) => {
//        console.log(result)
//        Swal.fire({
//            title: 'Update Success!',
//            /*    text: 'Input Success!',*/
//            icon: 'success'
//        })
//        $('#insertModal').modal('hide');
//    }).fail((error) => {
//        console.log(error)
//        Swal.fire({
//            icon: 'error',
//            title: 'Oops...',
//            text: 'Something went wrong!',
//            footer: `<a href=${'https://httpstatuses.com/' + error.status} target="_blank"/>Why do I have this issue?</a>`
//        })
//    })
//}

function updateEmployee() {
    var obj = new Object();
    obj.nik = $("#NIK").val();
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.birthDate = $("#birthDate").val();
    obj.phone = $("#phoneInput").val();
    obj.email = $("#emailInput").val();
    obj.gender = parseInt($("#genderSelect").val());
    obj.salary = parseInt($("#salaryInput").val());

    console.log(JSON.stringify(obj));

    $.ajax({
        url: 'https://localhost:44351/employees/update',
        type: "PUT",
        data: obj
    }).done((result) => {
        console.log(result)
        Swal.fire({
            title: 'Update Success!',
            /*    text: 'Input Success!',*/
            icon: 'success'
        })
        $('#insertModal').modal('hide');
    }).fail((error) => {
        console.log(error)
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
            footer: `<a href=${'https://httpstatuses.com/' + error.status} target="_blank"/>Why do I have this issue?</a>`
        })
    })
}

function insertEmployee() {
    var years = new Date().getFullYear();
    var obj = new Object();
    obj.nik = $("#NIK").val();
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.birthDate = $("#birthDate").val();
    obj.phone = $("#phoneInput").val();
    obj.email = $("#emailInput").val();
    obj.gender = parseInt($("#genderSelect").val());
    obj.salary = parseInt($("#salaryInput").val());

    console.log(JSON.stringify(obj))

    $.ajax({
        url: 'https://localhost:44351/employees/post/',
        type: "POST",
        data: obj
    }).done((result) => {
        console.log(result)
        Swal.fire({
            title: 'Input Success!',
            /*    text: 'Input Success!',*/
            icon: 'success'
        })
        $('#insertModal').modal('hide');
    }).fail((error) => {
        console.log(error)
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
            footer: `<a href=${'https://httpstatuses.com/' + error.status} target="_blank"/>Why do I have this issue?</a>`
        })
    })
}

function deleteEmployee(nik) {
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
                url: 'https://localhost:44351/employees/delete/' + nik,
                type: "DELETE",
                contentType: "application/json;charset=utf-8",
                traditional: true,
                data: ""
            }).done((result) => {
                console.log(result);
                Swal.fire({
                    title: 'Delete Success',
                    text: 'Data Deleted!',
                    icon: 'success'
                })
                $('#insertModal').modal('hide');
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

//$('.btn-insert').on('click', function () {
//    'use strict';
//    window.addEventListener('load', function () {
//        var forms = document.getElementsByClassName('needs-validation');
//        var validation = Array.prototype.filter.call(forms, function (form) {
//            form.addEventListener('submit', function (event) {
//                if (form.checkValidity() === false) {
//                    event.preventDefault();
//                    event.stopPropagation();
//                }
//                else {
//                    insertEmployee();
//                    $('#insertModal').modal('hide');
//                }
//                form.classList.add('was-validated');
//            }, false);
//        });
//    }, false);
//});

//$('.btn-update').on('click', function () {
//    'use strict';
//    window.addEventListener('load', function () {
//        var forms = document.getElementsByClassName('needs-validation');
//        var validation = Array.prototype.filter.call(forms, function (form) {
//            form.addEventListener('button', function (event) {
//                if (form.checkValidity() === false) {
//                    event.preventDefault();
//                    event.stopPropagation();
//                }
//                else {
//                    updateEmployee();
//                    $('#insertModal').modal('hide');
//                }
//                form.classList.add('was-validated');
//            }, false);
//        });
//    }, false);
//});

$('#insertModal').on('hidden.bs.modal', function (e) {

    $('#employeeForm')
        .find("input[type=text], input[type=datetime-local], input[type=number], input[type=email], textarea, select, checked")
        .val("");
})