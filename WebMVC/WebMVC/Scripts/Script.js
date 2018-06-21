
$(function () {
    $('[data-toggle="popover"]').popover({ trigger: "hover" })
});


function edit(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {
            $("#nav-new-tab").html("Edit")
            $("#nav-new-tab").tab("show")
            $("#nav-new").html(response)

        }
    })
}


function editClean() {
    $.ajax({
        type: "GET",
        url: 'Home/AddOrEdit',
        success: function (response) {
            $("#nav-new").html(response)

        }
    })
}



function List() {
    editClean()
    $.ajax({
        type: "GET",
        url: 'Home/List' + (document.URL.split('?')[1] == undefined ? "" : "/?"+document.URL.split('?')[1]), 
        success: function (response) {
            $("#nav-new-tab").html("Add New")
            $("#nav-view-tab").tab("show")
            $("#nav-view").html(response)

        }
    })
}



function deleteReg(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {
            if (response.success) {
                $("#result").html(response.message);
                $("#result").addClass("alert alert-success");
            }
            else {
                $("#result").html(response.message);
                //adding class
                $("#result").addClass("alert alert-danger");
            }
            $('#result').fadeIn();
            $('#result').fadeOut(2500, "linear");
        }
    })
}

function remove_tr(id) {
    var element_tr = "tr[data-id=" + id + "]"
    // Chrome don't hide popover after remove it's parent reference
    $(element_tr).find("[data-toggle=popover]").popover('hide');
    $(element_tr).remove()
    List()
    return false;
};