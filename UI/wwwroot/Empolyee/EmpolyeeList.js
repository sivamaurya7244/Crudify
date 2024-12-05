$(document).ready(function () {
     getEmpolyeeList();
   
});

var strToken = sessionStorage.getItem("jwtToken");
function getEmpolyeeList() {
    $.ajax({
        url: 'https://localhost:7201/api/Empolyee/GetEmployeeList',
        type: 'Post',
        dataType: 'json',
        "headers": {
            "Authorization": "Bearer " + strToken
        },
        success: function (response) {

            console.log(response);

            $("#Empolyee-table tbody").empty();

            $("#empdata").tmpl(response).appendTo("#Empolyee-table tbody");
        },
        error: function (xhr, status, error) {
            console.log("Request failed:", error);
        },
        complete: function () {
            console.log("Request finished");
        }
    });
}


function DeleteData(id) {

    //debugger;
    $.ajax({

        url: 'https://localhost:7201/api/Empolyee/DeleteEmpolyee',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify({ id: id }),

        success: function (responce) {

            console.log(responce);
            alert(responce);
            location.reload();
        },

        error: function (xhr, status, error) {

            console.log(error);
        },

        complete: function () {

            console.log('Request finished');
        }
    })
}