var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Faculty/CourseClo/GetAll"
        },
        "columns": [
           
            {
                "data": "courseHistory.course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.courseHistory.course.courseCode + "<br/>(Section: " + row.courseHistory.section.sectionCode + ")";
                },
                "width": "5%"
            },
            { "data": "courseLearning.cloCode", "width": "2%" },
            { "data": "description", "width": "25%" },
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
                                <a href="/Faculty/CourseClo/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                
                            </div>
                           `;
                }, "width": "5%"
            }
        ]
    });
}

