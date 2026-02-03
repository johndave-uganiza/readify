document.addEventListener('DOMContentLoaded', () => {

    /* -------------------- STATE -------------------- */
    let strModalType = null;
    let intSelectedRentalId = null;

    /* -------------------- ELEMENTS -------------------- */
    const container = document.querySelector('[data-price-per-day]');
    const pricePerDay = container ? parseFloat(container.dataset.pricePerDay) : 0;

    const rentalDateInput = document.getElementById('rentalDate');
    const returnDateInput = document.getElementById('returnDate');
    const totalPriceDisplay = document.getElementById('dclTotalPriceDisplay');
    const totalPriceInput = document.getElementById('dclTotalPrice');

    const payNowBtn = document.getElementById('payNowBtn');
    const paymentMethodSelect = document.getElementById('paymentMethodSelect');

    const modal = new bootstrap.Modal(document.getElementById('confirmationModal'));

    const modalTitle = document.getElementById('modalTitle');
    const modalMessage = document.getElementById('modalMessage');
    const modalSubMessage = document.getElementById('modalSubMessage');
    const confirmBtn = document.getElementById('confirmActionBtn');

    /* -------------------- HELPERS -------------------- */
    function formatCurrency(amount) {
        return new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD'
        }).format(amount);
    }

    function calculateTotal() {
        if (!rentalDateInput || !returnDateInput) return;

        const rentalDate = new Date(rentalDateInput.value);
        const returnDate = new Date(returnDateInput.value);

        let total = pricePerDay;

        if (!isNaN(rentalDate) && !isNaN(returnDate) && returnDate >= rentalDate) {
            const diffTime = returnDate - rentalDate;
            const diffDays = Math.max(
                Math.ceil(diffTime / (1000 * 60 * 60 * 24)),
                1
            );
            total = diffDays * pricePerDay;
        }

        if (totalPriceInput) totalPriceInput.value = total.toFixed(2);
        if (totalPriceDisplay) totalPriceDisplay.textContent = formatCurrency(total);
    }

    /* -------------------- EVENTS -------------------- */

    rentalDateInput?.addEventListener('change', calculateTotal);
    returnDateInput?.addEventListener('change', calculateTotal);
    calculateTotal();

    // PAY NOW
    payNowBtn?.addEventListener('click', () => {
        strModalType = 'payment';

        modalTitle.textContent = 'Payment Confirmation';
        modalMessage.innerHTML =
            `You are about to pay <strong>${totalPriceDisplay.textContent}</strong>.`;
        modalSubMessage.innerHTML =
            `Payment Method: <strong>${paymentMethodSelect.value}</strong>`;

        confirmBtn.textContent = 'Confirm Payment';
        confirmBtn.className = 'btn btn-primary';

        modal.show();
    });

    // RETURN
    document.querySelectorAll('.returnRentalBtn').forEach(btn => {
        btn.addEventListener('click', () => {
            strModalType = 'return';
            intSelectedRentalId = btn.dataset.rentalId;

            modalTitle.textContent = 'Confirm Return';
            modalMessage.textContent = 'Are you sure you want to return this rental?';
            modalSubMessage.textContent = 'This action cannot be undone.';

            confirmBtn.textContent = 'Yes, Return';
            confirmBtn.className = 'btn btn-info';

            modal.show();
        });
    });

    // CONFIRM
    confirmBtn.addEventListener('click', () => {

        if (strModalType === 'payment') {
            document.getElementById('ysnPaid').value = true;
            document.getElementById('strPaymentMethod').value = paymentMethodSelect.value;
            document.getElementById('rentalForm').submit();
        }

        if (strModalType === 'return') {
            document.getElementById('intRentalId').value = intSelectedRentalId;
            document.getElementById('returnForm').submit();
        }
    });
});
