var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/faculty/MappingCourseProgramLO/GetAll"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            {
                "data": "courseHistory.course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.courseHistory.course.courseCode + "(" + row.courseHistory.section.sectionCode + ")-" + row.courseHistory.instructor.shortCode;
                },
                "width": "5%"
            },
            { "data": "program.programCode", "width": "2%" },
            { "data": "courseLearning.cloCode", "width": "2%" },
            { "data": "programPLO.programLearning.ploCode", "width": "5%" },
            { "data": "correlation.stage", "width": "5%" },
            {
                "data": "createdDate", "width": "2%",
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY');
                },
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Faculty/MappingCourseProgramLO/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                
                            </div>
                           `;
                }, "width": "2%"
            }
        ]
    });
}

