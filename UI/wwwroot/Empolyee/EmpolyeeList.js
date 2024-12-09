$(document).ready(function () {
    getEmpolyeeList();
    


    $("#search-input").on("input", function () {
        var value = $(this).val().toLowerCase();
        var visibleRows = $("#Empolyee-table tr").filter(function () {
            return $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1).is(':visible');
        });

        if (visibleRows.length === 0) {
            $("#no-records").show();
        } else {
            $("#no-records").hide();
        }
    });


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
    const isConfirmed = confirm("Are you sure you want to delete this record?");

    if (!isConfirmed) {
        console.log("Deletion canceled");
        return false;
    }

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