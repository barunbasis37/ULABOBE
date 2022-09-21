var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Faculty/CourseHistory/GetAll"
        },
        "columns": [
            /*{ "data": "id", "width": "2%" },*/
            {
                "data": "course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.course.courseCode + "<br/> (" + row.course.title + ") <br/> Type: " + row.course.courseType.code;
                },
                "width": "5%"
            },
            { "data": "section.sectionCode", "width": "2%" },
            { "data": "semester.name", "width": "2%" },
            { "data": "instructor.name", "width": "5%" },
            { "data": "course.prerequisite", "width": "5%" },
            { "data": "course.creditHour", "width": "2%" },
            /*{ "data": "course.summary", "width": "10%" },*/
            { "data": "schedulesNames", "width": "2%" },
            { "data": "cieMarks", "width": "2%" },
            { "data": "seeMarks", "width": "2%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Faculty/CourseHistory/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                
                            </div>
                           `;
                }, "width": "20%"
            }
        ]
    });
}




//<i class="fas fa-edit"></i>
//    <i class="fas fa-trash-alt"></i>