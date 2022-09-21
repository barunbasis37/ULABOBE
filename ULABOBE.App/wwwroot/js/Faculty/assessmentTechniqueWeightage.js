var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/faculty/AssessmentTechniqueWeightage/GetAll"
        },
        "columns": [
           
            {
                "data": "courseHistory.course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.courseHistory.course.courseCode + "(" + row.courseHistory.section.sectionCode + ")";
                },
                "width": "5%"
            },
            { "data": "strategy", "width": "10%" },
            { "data": "percentage", "width": "2%" },
            { "data": "assessmentType.code", "width": "10%" },
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
                                <a href="/Faculty/AssessmentTechniqueWeightage/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                
                            </div>
                           `;
                }, "width": "3%"
            }
        ]
    });
}
