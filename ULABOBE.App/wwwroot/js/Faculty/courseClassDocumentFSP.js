var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/faculty/courseClassDocument/getAll"
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
<a href="/Admin/CourseClassDocument/DownloadFile/${data}" style="cursor:pointer; font-size: 12px;">
                                    ${row.attendanceSheetFileName} <i class="fas fa-cloud-download-alt"></i>
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
                                <a href="/faculty/CourseClassDocument/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="bi bi-pencil-square"></i>
                                </a>                                
                            </div>
                           `;
                }, "width": "2%"
            }
        ]
    });
}

