var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/courseClassDocument/getAll"
        },
        "columns": [
            { "data": "id", "width": "2%" },
            {
                "data": "courseCode",
                "render": function (data, type, row, meta) {
                    return row.courseCode + " (" + row.sectionCode + ") <br/> Program: " + row.programCode + "<br/> Faculty: " + row.instructorName + "(" + row.instructorCode + ")";
                },
                "width": "15%"
            },
            {
                "data": "id",
                "render": function (data, type, row, meta) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/CourseClassDocument/DownloadFile/${data}" style="cursor:pointer; font-size: 12px;">
                                    ${row.classMonitoringFileName} <i class="fas fa-cloud-download-alt"></i>
                                </a>
, 
<a href="/Admin/CourseClassDocument/DownloadFile/${data}" style="cursor:pointer; font-size: 12px;">
                                    ${row.courseSessionFileName} <i class="fas fa-cloud-download-alt"></i>
                                </a>
<br/>
<a href="/Admin/CourseClassDocument/DownloadFile/${data}"  style="cursor:pointer; font-size: 12px;">
                                    ${row.semesterCourseFileName} <i class="fas fa-cloud-download-alt"></i>
                                </a>
, 
<a href="/Admin/CourseClassDocument/DownloadFile/${data}"  style="cursor:pointer; font-size: 12px;">
                                    ${row.lessonPlanFileName} <i class="fas fa-cloud-download-alt"></i>
                                </a>
<br/>
<a href="/Admin/CourseClassDocument/DownloadFile/${data}" style="cursor:pointer; font-size: 12px;">
                                    ${row.courseProgramFileName} <i class="fas fa-cloud-download-alt"></i>
                                </a>
<br/>

                            </div>
                           `;
                }, "width": "20%"
            },

            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/CourseClassDocument/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="bi bi-pencil-square"></i>
                                </a>                                
                            </div>
                           `;
                }, "width": "2%"
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