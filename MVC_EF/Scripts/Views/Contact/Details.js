$(function () {
    const birthdate = $('#contactAge').html().split('/');

    $('#contactAge').html(calculateAge(...birthdate));
});

function calculateAge(year, month, day) {
    const diff = Date.now() - new Date(year, month - 1, day);

    return Math.floor(diff / (1000 * 60 * 60 * 24 * 365.25));
}