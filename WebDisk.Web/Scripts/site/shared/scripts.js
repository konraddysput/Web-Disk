$(function () {
    $.material.init();


    $('#filter').keyup(function () {

        var rex = new RegExp($(this).val(), 'i');
        $('.searchable tr').hide();
        $('.searchable tr').filter(function () {
            return rex.test($(this).text());
        }).show();

    });
})

function joinCourse(courseId, url) {
    $.ajax({
        type: "GET",
        url: url,
        data: {
            courseId: courseId
        },
        success: function (data) {
            if (!data.accept) {
                $("#postive-result").snackbar("show");
            }
            else {
                $("#courseId").val(courseId);
                $("#join-modal").modal();
            }
        }
    });
}

function editCourseDetails(clickElement, courseId) {
    //span  //td     //td
    var row = clickElement.parent().parent().parent();
    $("#short-cut-edit").val(row.find('.course-shortcut').text().trim())
                        .parent().addClass("is-focused"),
    $("#course-name-edit").val(row.find('.course-name').text().trim())
                        .parent().addClass("is-focused"),
    $("#description-edit").val(row.find('.course-description').text().trim())
                        .parent().addClass("is-focused");
    $("#courseId").val(courseId);
    $("#change-description-modal").modal();
}

function openModal(id) {
    $("#element-to-delete").val(id);
    $("#DeletePopup").modal();

}

function getUserCourses(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            $("#courses-content").html(data);
        }
    })
}
var edit=false;

function enableCourseEdit() {
    $(".read-only").toggle();
    $(".edit").toggle();
    $("#save-btn").toggle();
    if(!edit){
        $("#edit-btn").html("Anuluj");
        console.log($(".form-group").length);
        $(".form-group").removeClass("form-group");
    }
    else {
        $("#edit-btn").html("Edytuj");
        
    }
    edit = !edit;
}

function saveEdit(passwordEnable) {
    if (passwordEnable == false && $("select[name='IsPasswordEnable']").val() == true) {
        $("#modal-pass").click();
    }
    else {
        $("desc-edit").submit();
    }
}


function addPass() {
    
    if ($("select[name='IsPasswordEnable']").val()=="true") {
        console.log("change pass");
        $("#password-requirement").show();
    }
    else {
        $("#password-requirement").hide();
    }
}