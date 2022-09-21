var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Faculty/CourseContent/GetAll"
        },
        "columns": [
            { "data": "id", "width": "2%" },
            {
                "data": "courseHistory.course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.courseHistory.course.courseCode + "(" + row.courseHistory.section.sectionCode + ")";
                },
                "width": "5%"
            },
            { "data": "topic", "width": "15%" },
            { "data": "sessionQuantity", "width": "2%" },
            { "data": "cLoSelectedIDNames", "width": "2%" },
            { "data": "arSelectedIDNames", "width": "2%" },
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
                                <a href="/Faculty/CourseContent/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>                                
                            </div>
                           `;
                }, "width": "3%"
            }
        ]
    });
}

