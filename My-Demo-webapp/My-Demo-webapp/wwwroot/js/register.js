function updateMessage(element, message, addClass, removeClass) {
    element.textContent = message;
    element.classList.add(addClass);
    element.classList.remove(removeClass);
}
function checkPasswordMatch() {
    const errorMessage = document.getElementById('error-message');
    const password = document.getElementById("password").value;
    const confirmPassword = document.getElementById("confirmPassword").value;

    if (password === confirmPassword) {
        updateMessage(errorMessage, "Passwords match!", 'valid', 'error');
    } else {
        updateMessage(errorMessage, "Passwords do not match!", 'error', 'valid');
    }
}

// จัดการเหตุการณ์ 'input' เมื่อผู้ใช้กรอก confirmPassword
document.getElementById('confirmPassword').addEventListener('input', checkPasswordMatch);

// จัดการการ submit ของฟอร์มลงทะเบียน
document.getElementById('registerForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const formData = {
        username: document.getElementById('username').value,
        password: document.getElementById('password').value,
        firstName: document.getElementById('firstname').value,
        lastName: document.getElementById('lastname').value,
        email: document.getElementById('email').value
    };
    const confirmPassword = document.getElementById('confirmPassword').value;

    // ตรวจสอบความถูกต้องของรหัสผ่าน
    if (formData.password === confirmPassword) {
        submitForm('/api/RegisterApi', formData)
            .then(response => {
                Swal.fire({
                    title: 'Register success!',
                    text: "",
                    icon: 'success',
                    showCancelButton: true,
                    confirmButtonText: 'Go to login page',
                    cancelButtonText: 'Stay here'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.replace("/Home/Login");
                    }
                })
                
            })
            .catch(error => {
                Swal.fire({
                    title: "Register fail",
                    text: error.responseJSON.errorCode + " : " + error.responseJSON.errorMessage,
                    icon: 'error'
                });
            });
    } else {
        Swal.fire({
            title: 'Register fail',
            text: 'Passwords do not match!',
            icon: 'error'
        });
    }
});