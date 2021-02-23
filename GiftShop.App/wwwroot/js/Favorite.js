$(document).ready(function () {
    $('.el-wrapper').each(function () {
        var id = $(this).find('#productId').html();
        var result = $(this).find('.cart .add-to-cart .txt');
        var button = $(this).find('.h-bg .h-bg-inner');
        $(this).find('#addToCart').click(function () {
            $.ajax({
                url: '/Cart/AddToCart',
                data: { productId: id },
                success: function (response) {
                    result.html("Added");
                    button.css({ "background-color": "#0bce2c" });
                },
                error: function (response) {
                    result.html("Product Unavailable");
                    button.css({ "background-color": "darkred" });
                }

            }) 
        })

   
    })
})