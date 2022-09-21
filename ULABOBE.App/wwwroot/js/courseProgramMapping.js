var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/CourseProgramMapping/GetAll"
        },
        "columns": [
            {
                "data": "courseHistory.course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.courseHistory.course.courseCode + "<br/> (" + row.courseHistory.course.title + ") <br/> Section: " + row.courseHistory.section.sectionCode + "<br/> Instructor: " + row.courseHistory.instructor.name;
                },
                "width": "5%"
            },
            { "data": "courseHistory.semester.name", "width": "2%" },
            { "data": "courseLearning.cloCode", "width": "2%" },
            { "data": "pLoSelectedIDNames", "width": "10%" },
            { "data": "gsSelectedIDNames", "width": "10%" },
            { "data": "psSelectedIDNames", "width": "10%" },
            { "data": "sdgSelectedIDNames", "width": "10%" },
            { "data": "arSelectedIDNames", "width": "10%" },
            { "data": "isApproved", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/CourseProgramMapping/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a onclick=Delete("/Admin/CourseProgramMapping/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="bi bi-trash"></i>
                                </a>
                            </div>
                           `;
                }, "width": "3%"
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