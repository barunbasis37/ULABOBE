var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/AssessmentPattern/GetAll"
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
            { "data": "assessmentTechniqueWeightage.courseHistory.course.title", "width": "4%" },
            
            { "data": "assessmentTechniqueWeightage.courseHistory.semester.name", "width": "2%" },
            {
                "data": "assessmentTechniqueWeightage.strategy",
                "render": function(data, type, row, meta) {
                    return row.assessmentTechniqueWeightage.strategy + "(" + row.assessmentTechniqueWeightage.assessmentType.code+")";
                },

                "width": "5%"
            },
            { "data": "bloomsCategory.name", "width": "2%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/AssessmentPattern/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a onclick=Delete("/Admin/AssessmentPattern/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
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