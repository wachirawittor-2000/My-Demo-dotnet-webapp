function submitForm(url, data) {
    showLoadingAlert();

    return new Promise((resolve, reject) => {
        $.ajax({
            url: url,
            method: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            dataType: 'json',
            success: function (response) {
                Swal.hideLoading();
                resolve(response);
            },
            error: function (error) {
                Swal.hideLoading();
                reject(error);
            }
        });
    });
}

// ฟังก์ชันแสดงการแจ้งเตือนขณะโหลด
function showLoadingAlert() {
    Swal.fire({
        title: 'Loading...',
        text: 'Please wait while we process your request.',
        icon: 'info',
        showConfirmButton: false,
        willOpen: () => {
            Swal.showLoading();
        }
    });
}
