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




    // Function to get cookie value by name
    function getCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    // Get userId and password from cookies
    var userId = getCookie("userId");
    var password = getCookie("password");

    // Fill the input fields if cookies are found
    if (userId) {
        $("#userId").val(userId);
    }
    if (password) {
        $("#password").val(password);
    }




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

                    // Check if "Is Remember" checkbox is checked
                    if ($("#dropdownCheck2").prop("checked")) {
                        setCookie("userId", vParam.userId, 7); // Store for 7 days
                        setCookie("password", vParam.password, 7); // Store for 7 days
                    } else {
                        // Clear cookies if not checked
                        setCookie("userId", '', -1); // Clear cookie
                        setCookie("password", '', -1); // Clear cookie
                    }


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


function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}