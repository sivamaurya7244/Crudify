var id = 0;
$(document).ready(function () {
    $("#pass").show();
    
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const vData = urlParams.get('id');
    if (vData != null) {
        id = parseInt(vData);
        $("#pass").hide();
        getEmpolyeeDetails(id);
    }

});


function addEmpolyee() {
    //debugger;
    if (validation() == true) {


        var vParam = {

            "id": id,
            "UserID": $("#userId").val(),
            "Name": $("#name").val(),
            "Password": $("#password").val(),
            "Description": $("#description").val(),
            "isActive": $("#isActive").prop("checked")
        }

        var urls = "";

        if (id > 0) {

            urls = 'https://localhost:7201/api/Empolyee/UpdateEmpolyee';
        }

        else {
            urls = 'https://localhost:7201/api/Empolyee/InsertEmpolyee';
        }
        var strToken = sessionStorage.getItem("jwtToken");
        $.ajax({

            url: urls,
            type: 'POST',
            contentType: 'application/json',
            "data": JSON.stringify(vParam),
            "headers": {
                "Authorization": "Bearer " + strToken
            },
            success: function (response) {

                console.log(response);
                alert(response);
                location.reload();

            },

            error: function (xhr, status, error) {

                console.log("Request failed: " + error);
            },

            complete: function () {

                console.log("Requsted finished");
            }

        })
    }

}

function getEmpolyeeDetails(id) {
    var vParam = {
        id: id
    }
    var strToken = sessionStorage.getItem("jwtToken");

    $.ajax({
        url: 'https://localhost:7201/api/Empolyee/DetailEmpolyee',
        type: 'POST',
        contentType: 'application/json',
        "data": JSON.stringify(vParam),
        "headers": {
            "Authorization": "Bearer " + strToken
        },
        success: function (responce) {

            $("#userId").val(responce.userId);
            $("#name").val(responce.name);
            $("#password").val(responce.password);
            $("#description").val(responce.description);
            $("#isActive").prop('checked', responce.isActive);

            console.log("Data fetched: ", responce);
        },
        error: function (xhr, status, error) {
            console.log("Request failed", error);
        },
        complete: function () {
            console.log("Request finished");
        }
    })
}


function validation() {
    var isValid = true;
    
    
        if ($("#userId").val() == "") {
            $("#userIdError").removeClass("hide");
            isValid = false;
        }
        else {
            $("#userIdError").addClass("hide");
        }

        if ($("#name").val() == "") {
            $("#nameError").removeClass("hide");
            isValid = false;
        }
        else {
            $("#nameError").addClass("hide");
        }

        if (id > 0) {
            $("#pass").hide();
            isValid = true;
        }
        else {
            if ($("#password").val() == "") {
                $("#passwordError").removeClass("hide");
                isValid = false;
            }
            else {
                $("#passwordError").addClass("hide");
            }
        }


        if ($("#description").val() == "") {
            $("#descriptionError").removeClass("hide");
            isValid = false;
        }
        else {
            $("#descriptionError").addClass("hide");
        }

        return isValid;
    
}