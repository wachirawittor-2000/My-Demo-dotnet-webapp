document.getElementById('togglePassword').addEventListener('click', function () {
    var passwordField = document.getElementById('password');
    var passwordButton = document.getElementById('togglePassword');

    // ����� input ��Ẻ password �������
    if (passwordField.type === 'password') {
        passwordField.type = 'text';  // �ʴ����ʼ�ҹ
        passwordButton.textContent = 'Hide'; // ����¹��ͤ����� "Hide"
    } else {
        passwordField.type = 'password'; // ��͹���ʼ�ҹ
        passwordButton.textContent = 'Show'; // ����¹��ͤ����� "Show"
    }
});

document.getElementById('loginForm').addEventListener('submit', function (event) {
    event.preventDefault();  // ��ͧ�ѹ������ê˹������� submit

    // �Ѻ��Ҩҡ�����
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    // ���ҧ�����ŷ�������ѧ���������
    const formData = {
        username: username,
        password: password
    };

    // ���¡��ѧ��ѹ submitForm
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

