var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/faculty/CourseProgramMapping/GetAll"
        },
        "columns": [
            {
                "data": "courseHistory.course.courseCode",
                "render": function (data, type, row, meta) {
                    return row.courseHistory.course.courseCode + "(" + row.courseHistory.section.sectionCode + ")" ;
                },
                "width": "3%"
            },
            { "data": "courseHistory.semester.name", "width": "1%" },
            { "data": "courseLearning.cloCode", "width": "1%" },
            { "data": "pLoSelectedIDNames", "width": "1%" },
            { "data": "gsSelectedIDNames", "width": "1%" },
            { "data": "psSelectedIDNames", "width": "1%" },
            { "data": "sdgSelectedIDNames", "width": "1%" },
            { "data": "arSelectedIDNames", "width": "1%" },
            {
                "data": {
                    isApproved: "isApproved", isSubmitted: "isSubmitted"
                }, "width": "1%",

                render: function (data) {

                    if (data.isSubmitted === false) {

                        return '<b class="text-danger"><i class="fas fa-not-equal"></i> Not Submit Yet</b>';
                        
                        
                    }
                    else {
                        if (data.isApproved === true) {
                            return '<b class="text-success"><i class="fas fa-pause-circle"></i> Approved</b>';

                        } else {
                            return '<b class="text-warning"><i class="fas fa-pause-circle"></i> Pending</b>';
                        }
                        

                    }
                }
            },

            {
                "data": {
                    id: "id", isSubmitted: "isSubmitted"
                }, "width": "10%",
                render: function(data) {
                    if (data.isSubmitted == true) {
                        return '<b class="text-info"> <i class="fas fa-check-circle"></i> Submitted </b>';
                    } else {
                        return `
                            <div class="text-center">
                                <a href="/Faculty/CourseProgramMapping/Upsert/${data.id}" class="btn btn-success text-white btn-sm" style="cursor:pointer;"><i class="bi bi-pencil-square"></i> Edit</a>
                                <a onclick=Submission('${data.id}') class="btn btn-info text-white btn-sm" style="cursor:pointer;"><i class="far fa-paper-plane"></i> Submit</a>
                            </div>
                           `;
                    }

                }

            }
            
        ]
    });
}



function Submission(id) {

    $.ajax({
        type: "POST",
        url: '/Faculty/CourseProgramMapping/Submitted',
        data: JSON.stringify(id),
        contentType: "application/json",
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

