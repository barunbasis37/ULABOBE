var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/CourseHistory/GetAll"
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
                                <a href="/Admin/CourseHistory/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a onclick=Delete("/Admin/CourseHistory/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="bi bi-trash"></i>
                                </a>
                            </div>
                           `;
                }, "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}


//<i class="fas fa-edit"></i>
//    <i class="fas fa-trash-alt"></i>