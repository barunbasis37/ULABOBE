var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/faculty/AssessmentPattern/GetAll"
        },
        "columns": [
            //{ "data": "id", "width": "2%" },
            {
                "data": "assessmentTechniqueWeightage.courseHistory.course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.assessmentTechniqueWeightage.courseHistory.course.courseCode + "(" + row.assessmentTechniqueWeightage.courseHistory.section.sectionCode + ")-" + row.assessmentTechniqueWeightage.courseHistory.instructor.shortCode;
                },
                "width": "3%"
            },
            {
                "data": "assessmentTechniqueWeightage.strategy",
                "render": function(data, type, row, meta) {
                    return row.assessmentTechniqueWeightage.strategy + "(" + row.assessmentTechniqueWeightage.assessmentType.code+")";
                },

                "width": "5%"
            },
            { "data": "bloomsCategory.name", "width": "2%" },
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
                                <a href="/Faculty/AssessmentPattern/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                
                            </div>
                           `;
                }, "width": "3%"
            }
        ]
    });
}

