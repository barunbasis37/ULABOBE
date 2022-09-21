var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/faculty/CourseLearningResource/GetAll"
        },
        "columns": [
            {
                "data": "courseHistory.course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.courseHistory.course.courseCode + "(" + row.courseHistory.section.sectionCode + ")";
                },
                "width": "5%"
            },
           
            { "data": "learningResourceType..name", "width": "8%" },
            { "data": "bookInfo", "width": "20%" },
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
                                <a href="/Faculty/CourseLearningResource/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>                                
                            </div>
                           `;
                }, "width": "5%"
            }
        ]
    });
}

