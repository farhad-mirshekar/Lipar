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
    },
    prepareUserAddressAdd: function (url) {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (result) {
                $('#js-user-address-create').html(result);

                $([document.documentElement, document.body]).animate({
                    scrollTop: $("#js-user-address-create").offset().top
                }, 2000)
            }
        })
    },

    addUserAddress: function (url) {

        var frmUserAddressCreate = $('#userAddressCreate');

        if (frmUserAddressCreate.valid()) {

            $.ajax({
                cache: false,
                url: url,
                type: "POST",
                data: frmUserAddressCreate.serialize(),
                success: function (response) {

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
            })

        }
    },
    prepareUserAddressEdit: function (url) {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (result) {
                $('#js-user-address-create').html(result);

                $([document.documentElement, document.body]).animate({
                    scrollTop: $("#js-user-address-create").offset().top
                }, 2000)
            }
        })
    },
    deleteUserAddress: function (url, message) {
        if (confirm(message) == true) {
            $.ajax({
                type: 'POST',
                url: url,
                success: function (response) {
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
            })
        }
    },
    editUserAddress: function (url) {

        var frmUserAddressEdit = $('#userAddressEdit');

        if (frmUserAddressEdit.valid()) {

            $.ajax({
                cache: false,
                url: url,
                type: "POST",
                data: frmUserAddressEdit.serialize(),
                success: function (response) {

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
            })

        }
    },
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

/*bank payment function*/
$(document).off('click', '#btn-bank');
$(document).on('click', '#btn-bank', function (e) {
    e.preventDefault();

    var userAddress = $("input[name='UserAddressId']:checked").val();
    var bankPort = $("input[name='BankPortId']:checked").val();
    if (userAddress == null || userAddress == '' || userAddress == undefined) {

        Message('آدرس مورد نظر را انتخاب نمایید', 'error');
        return false;
    }

    if (bankPort == null || bankPort == '' || bankPort == undefined) {

        Message('درگاه مورد نظر را انتخاب نمایید', 'error');
        return false;
    }

})

function Message(text, type) {
    new Noty({
        text: text,
        theme: 'bootstrap-v4',
        timeout: 3500,
        layout: 'topCenter',
        progressBar: true,
        type: type

    }).show();
}