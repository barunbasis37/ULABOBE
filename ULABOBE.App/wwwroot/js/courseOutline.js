var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/CourseOutline/GetAll"
        },
        "columns": [
            { "data": "id", "width": "2%" },
            {
                "data": "courseHistory.course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.courseHistory.course.courseCode + "<br/> Section: " + row.courseHistory.section.sectionCode ;
                },
                "width": "5%"
            },
            { "data": "courseHistory.course.title", "width": "2%" },
            { "data": "courseHistory.semester.name", "width": "2%" },
            { "data": "courseHistory.instructor.name", "width": "2%" },            
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/CourseOutline/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a href="/Admin/CourseOutline/DownloadFile/${data}" class="btn btn-warning text-white" style="cursor:pointer">
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