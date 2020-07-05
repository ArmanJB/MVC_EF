$(function () {
    CleanAddress();

    $(".btn_remove_address").click(function () {
        if (confirm('¿Está seguro de retirar el item seleccionado?')) {
            $(this).closest('.list-group-item').find('.remove_address').val("True");
            return true;
        }
        return false;
    })
});

function CleanAddress() {
    $('#AddressDescription').val('');
}