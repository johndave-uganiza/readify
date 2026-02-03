document.addEventListener('DOMContentLoaded', function () {
    const modal = document.getElementById('successModal');
    if (modal) {
        const successModal = new bootstrap.Modal(modal);
        successModal.show();
    }
});
