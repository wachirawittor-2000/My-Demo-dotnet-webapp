function loginApi() {
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    console.log("Username : ", username, " and Password : ", password);

    var user = {
        username: username,
        password: password
    }

    $.ajax({
        url: 'api/LoginApi/',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(user),
    }).then(function (response) {
        if (response.status == "Success") {
            console.log("Login Success!!");
            console.log(response.result);
        }
    }, function (error) {
        console.log("Login failed");
    });
}
