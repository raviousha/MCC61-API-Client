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
            'url': 'https://localhost:44321/api/employee/show/',
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
                'data': 'fullName'
            },
            {
                'data': 'phone',
                'bSortable': false
            },
            {
                'data': null,
                'render': function (data, type, row) {
                    var salary = row['salary']
                    return ("Rp." + salary)
                }
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
            "defaultContent": `<button class="btn btn-sm btn-outline-primary" onclick="detailEmployee()"><i class="fas fa-info-circle"></i></button>
                               <button class="btn btn-sm btn-outline-secondary" data-toggle="modal" data-target="#insertModal"><i class="fas fa-edit"></i></button>
                               <button class="btn btn-sm btn-outline-danger" id="btn-delete"><i class="fas fa-trash"></i></button>
                               `
        }]
    });
    $('#EmployeeTable').on('click', '#btn-delete', function () {
        var data = table.row($(this).closest('tr')).data();
        console.log(data.nik);
        Delete(data);
    });
    getUni();
});

function detailEmployee() {
    $.ajax({
        url: 'https://localhost:44321/api/employee/202201/'
    }).done((employee) => {
        console.log(employee);
        var text = `<tr>
                        <td>NIK : </td>
                        <td>${employee.account.nik}</td>
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
                        <td>University : </td>
                        <td>${employee.account.profiling.education.university.name}</td>
                   </tr>
                    <tr>
                        <td>Degree : </td>
                        <td>${employee.account.profiling.education.degree}</td>
                   </tr>
                    <tr>
                        <td>GPA : </td>
                        <td>${employee.account.profiling.education.gpa}</td>
                   </tr>`
        $("#infoTable").html(text)
        $('#employeeModal').modal('show');
    }).fail((error) => {
        console.log(error)
    })
}

function getUni() {
    $.ajax({
        url: 'https://localhost:44351/universities/getall'
    }).done((data) => {
        var uniSelect = `<option value="" >Select University</option>`;
        $.each(data, function (key, val) {
            uniSelect += `<option value='${val.id}'>${val.name}</option>`
        });
        $("#uniSelect").html(uniSelect);
        console.log(uniSelect)
    }).fail((error) => {
        console.log(error)
    })
}

function Insert() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.birthDate = $("#birthDate").val();
    obj.phone = $("#phoneInput").val();
    obj.email = $("#emailInput").val();
    obj.gender = parseInt($("#genderSelect").val());
    obj.salary = parseInt($("#salaryInput").val());
    obj.password = $("#passwordInput").val();
    obj.degree = $("#degreeInput").val();
    obj.gpa = parseFloat($("#gpaInput").val());
    obj.UniversityId = $("#uniSelect").val();

    console.log(JSON.stringify(obj))

    $.ajax({
        url: 'https://localhost:44321/api/employee/register',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        traditional: true,
        data: JSON.stringify(obj)
    }).done((result) => {
        console.log(result)
        Swal.fire({
            title: 'Input Success!',
            /*    text: 'Input Success!',*/
            icon: 'success'
        })
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

function Delete(data) {
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
                url: 'https://localhost:44351/employees/deleteregister/' + data.nik,
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
                    Insert();
                    $('#insertModal').modal('hide');
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();