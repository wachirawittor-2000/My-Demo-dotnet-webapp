document.getElementById('togglePassword').addEventListener('click', function () {
    var passwordField = document.getElementById('password');
    var passwordButton = document.getElementById('togglePassword');

    // เช็คว่า input เป็นแบบ password หรือไม่
    if (passwordField.type === 'password') {
        passwordField.type = 'text';  // แสดงรหัสผ่าน
        passwordButton.textContent = 'Hide'; // เปลี่ยนข้อความเป็น "Hide"
    } else {
        passwordField.type = 'password'; // ซ่อนรหัสผ่าน
        passwordButton.textContent = 'Show'; // เปลี่ยนข้อความเป็น "Show"
    }
});

document.getElementById('loginForm').addEventListener('submit', function (event) {
    event.preventDefault();  // ป้องกันการรีเฟรชหน้าเมื่อ submit

    // รับค่าจากฟอร์ม
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    // สร้างข้อมูลที่จะส่งไปยังเซิร์ฟเวอร์
    const formData = {
        username: username,
        password: password
    };

    // เรียกใช้ฟังก์ชัน submitForm
    submitForm('/api/LoginApi', formData)
        .then(response => {
            Swal.fire({
                title: 'Login success!',
                icon: 'success',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.replace("/Home/Index");
                }
            });
            window.location.href('/Home/Index')
            console.log(response);
        })
        .catch(error => {
            Swal.fire({
                title: "Login fail",
                text: error.responseJSON.errorCode + " : " + error.responseJSON.errorMessage,
                icon: 'error'
            });
            
        });
});

