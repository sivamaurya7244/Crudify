$(document).ready(function () {

    $('#togglePassword').on('click', function () {
        const passwordInput = $('#password');
        const icon = $(this);

        // Toggle the type attribute
        const type = passwordInput.attr('type') === 'password' ? 'text' : 'password';
        passwordInput.attr('type', type);

        // Toggle the eye icon
        icon.toggleClass('fa-eye fa-eye-slash');
    });
    $("#loginForm").submit(function (event) {
        // Prevent default form submission
        event.preventDefault();

        // Prepare the data to be sent
        var vParam = {
            "userId": $("#userId").val(),
            "password": $("#password").val(),
        };

        // AJAX request
        $.ajax({
            url: 'https://localhost:7231/Login/UserLogin',
            type: "POST",
            contentType: 'application/json',
            data: JSON.stringify(vParam),
            success: function (responce) {
                $("#errorMessage").addClass("hide");
                console.log("Response received:", responce);
                if (responce.isSuccess == true) {
                    sessionStorage.setItem("jwtToken", responce.data);
                    sessionStorage.setItem("userName", vParam.userId);
                    window.location.href = "/Admin/DashBoard/Index";
                }
                else {
                    $("#errorMessage").removeClass("hide");
                }
                
            },
            error: function (xhr, status, error) {
                console.error("Request failed:", error);
                alert("An error occurred. Please try again.");
            },
            complete: function () {
                console.log("Request finished");
            }
        });
    });
});
