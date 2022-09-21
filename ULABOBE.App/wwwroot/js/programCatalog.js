var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/ProgramCatalog/GetAll"
        },
        "columns": [
            { "data": "id", "width": "2%" },
            {
                "data": "programData.name",
                "render": function (data, type, row, meta) {
                    return row.programData.name + "<br/> Section: " + row.programData.programCode ;
                },
                "width": "5%"
            },
            { "data": "programData.department.name", "width": "2%" },
            { "data": "isActive", "width": "2%" },
            { "data": "createdDate", "width": "2%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ProgramCatalog/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a href="/Admin/ProgramCatalog/DownloadFile/${data}" class="btn btn-warning text-white" style="cursor:pointer">
                                    <i class="fas fa-cloud-download-alt"></i>
                                </a>
                            </div>
                           `;
                }, "width": "5%"
            }
        ]
    });
}

//function Delete(url) {
//    swal({
//        title: "Are you sure you want to Delete?",
//        text: "You will not be able to restore the data!",
//        icon: "warning",
//        buttons: true,
//        dangerMode: true
//    }).then((willDelete) => {
//        if (willDelete) {
//            $.ajax({
//                type: "DELETE",
//                url: url,
//                success: function (data) {
//                    if (data.success) {
//                        toastr.success(data.message);
//                        dataTable.ajax.reload();
//                    }
//                    else {
//                        toastr.error(data.message);
//                    }
//                }
//            });
//        }
//    });
//}


//<i class="fas fa-edit"></i>
//    <i class="fas fa-trash-alt"></i>