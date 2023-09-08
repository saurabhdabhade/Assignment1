$(document).ready(function () {
    $('#emailbutton').on('click', function (event) {
        event.preventDefault(); // Prevent the default form submission

        var inputEmail = $('#email').val(); // Get the input email

        // Perform AJAX request
        $.ajax({
            type: "GET",
            url: "/Register/ForgotPasswordEmailCheck",
            data: { 'email': inputEmail }, // Pass email as data
            contentType: "application/json",
            success: function (response) {
                if (response.redirectTo) {
                    // Redirect to the URL provided in the response
                    window.location.href = response.redirectTo;
                } else {
                    // Handle other parts of the response as needed
                }
            },
            error: function (xhr, status, error) {
                // Handle errors if the AJAX request fails
            }
        });
    });
});