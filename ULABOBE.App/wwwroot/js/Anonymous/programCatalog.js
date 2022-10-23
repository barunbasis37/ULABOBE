var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Anonymous/ProgramCatalog/GetAll"
        },
        "columns": [
            { "data": "id", "width": "1%" },
            {
                "data": "programData.name",
                "render": function (data, type, row, meta) {
                    return row.programData.name + " (" + row.programData.programCode+")" ;
                },
                "width": "10%"
            },
            { "data": "programData.department.name", "width": "10%" },            
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Anonymous/ProgramCatalog/DownloadFile/${data}" class="btn btn-warning text-white" style="cursor:pointer">
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