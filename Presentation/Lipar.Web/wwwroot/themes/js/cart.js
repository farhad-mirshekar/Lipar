//cart lipar implement
var cart = {
    quantityPlusOrMinusSuccess: function (response) {

        if (response.DivName != null) {
            if (response.Html != null) {
                $(response.DivName).html(response.Html);
            }
        }

        new Noty({
            text: response.Message,
            theme: 'bootstrap-v4',
            timeout: 3500,
            layout: 'topCenter',
            progressBar: true,
            type: response.NotyType

        }).show();
    }
};

$(document).on('click', '.quantity > i', function (e) {
    e.preventDefault();
    var $this = $(this);
    var url = $this.attr('data-href');
    if (url == null || url.length == 0) {
        $.alert('متاسفانه خطایی رخ داده است');
        return;
    }

    $.ajax({
        method: 'POST',
        url: url,
        cache: false,
        success: cart.quantityPlusOrMinusSuccess
    })
})