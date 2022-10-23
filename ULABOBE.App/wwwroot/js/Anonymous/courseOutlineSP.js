var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Faculty/Home/GetAll"
        },
        "columns": [
            {
                "data": "semesterCode",
                "render": function (data, type, row, meta) {
                    return row.semesterName + " (" + row.semesterCode+")";
                },
                "width": "5%"
            },
            {
                "data": "courseCode",
                "render": function (data, type, row, meta) {
                    return row.course_Name + " (" + row.courseCode + ")-" + row.sectionCode;
                },
                "width": "8%"
            },
            {
                "data": "pro_URMS",
                "render": function (data, type, row, meta) {
                    return row.program_Name + " (" + row.department_Name + ")";
                },
                "width": "10%"
            },
                      
            {
                "data": "instructorName",
                "render": function (data, type, row, meta) {
                    return row.instructorName + " (" + row.instructorCode + ")";
                },
                "width": "3%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">                                
                                <a href="/Anonymous/Home/DownloadFile/${data}" class="btn btn-success text-white" style="cursor:pointer">
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