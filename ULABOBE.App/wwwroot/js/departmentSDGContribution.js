﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/DepartmentSDGContribution/GetAll"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "department.name", "width": "10%" },
            { "data": "sdgContribution.sdgCode", "width": "10%" },
            { "data": "description", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/DepartmentSDGContribution/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a onclick=Delete("/Admin/DepartmentSDGContribution/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="bi bi-trash"></i>
                                </a>
                            </div>
                           `;
                }, "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}


//<i class="fas fa-edit"></i>
//    <i class="fas fa-trash-alt"></i>