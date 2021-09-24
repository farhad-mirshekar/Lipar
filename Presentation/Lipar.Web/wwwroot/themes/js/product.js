var product = {
    //add product to compare list
    addProductToCompareList: function (url) {
        $.ajax({
            cache: false,
            url: url,
            type: "POST",
            success: this.success_process,
            error: this.ajaxFailure
        })
    },

    addProductComment: function (url) {

        var frmProductCommentCreate = $('#productCommentCreate');

        if (frmProductCommentCreate.valid()) {

            $.ajax({
                cache: false,
                url: url,
                type: "POST",
                data: frmProductCommentCreate.serialize(),
                success: this.success_process,
                error: this.ajaxFailure
            })

        }
    },

    preapreProductQuestionAdd: function (url) {
        var productId = $('#js-product-page').data('product-id');
        $.ajax({
            type: 'GET',
            url: url,
            data: { productId: productId },
            success: function (result) {
                $('#js-product-question-create').html(result);

                $([document.documentElement, document.body]).animate({
                    scrollTop: $("#js-product-question-create").offset().top
                }, 2000)
            }
        })
    },

    addProductQuestion: function (url) {

        var frmProductQuestionCreate = $('#productQuestionCreate');

        if (frmProductQuestionCreate.valid()) {

            $.ajax({
                cache: false,
                url: url,
                type: "POST",
                data: frmProductQuestionCreate.serialize(),
                success: this.success_process,
                error: this.ajaxFailure
            })

        }
    },

    success_process: function (response) {

        new Noty({
            text: response.Message,
            theme: 'bootstrap-v4',
            timeout: 3500,
            layout: 'topCenter',
            progressBar: true,
            type: response.NotyType

        }).show();


        if (response.Url) {
            setTimeout(function () {
                location.href = response.Url;
                return true;
            }, 1000);
        }

        if (response.Clear) {
            $(response.DivName).html('');
        }
        return false;
    },
    ajaxFailure: function () {
        alert('Failed to add the product to the cart. Please refresh the page and try one more time.');
    }
}

function setLocation(url) {
    window.location.href = url;
}
// CSRF (XSRF) security
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};