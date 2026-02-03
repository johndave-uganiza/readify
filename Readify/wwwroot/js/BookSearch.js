const bookSearch = document.getElementById('bookSearch');


bookSearch.addEventListener('input', function () {
    const bookCards = document.querySelectorAll('.book-card');
    let strSearchText = this.value.toLowerCase();

    bookCards.forEach(function (card) {
        let strTitle = card.querySelector('.book-title').textContent.toLowerCase();
        let strAuthor = card.querySelector('.book-author').textContent.toLowerCase();
        let strSubject = card.querySelector('.book-subject').textContent.toLowerCase();

        if (strTitle.includes(strSearchText) || strAuthor.includes(strSearchText) || strSubject.includes(strSearchText)) {
            card.style.display = '';
        } else {
            card.style.display = 'none';
        }
    });
});