$(document).ready(function () {
    $('#buyButton').click(function () {
        var productId = $('#addToCartId').html();
        $.ajax({
            url: '/Cart/AddToCart',
            data: { productId: productId },
            success: function (response) {
                $('#buyButton').html('Added')
            },
            error: function (response) {
                if (response.responseJSON.message == 'Not authenticated') {
                    window.location.href = "/Account/Login";
                }
                if (response.responseJSON.message == 'Product no longer available') {
                    $('#Message').html('Product no longer available').css({
                        'color': 'red',
                        'transform': ' translateY(0px)',
                        'opacity': '1',
                        'transition': 'all ease-in-out .45s'
                    }).delay(2000).queue(function () {
                        $(this).css({
                            'color': 'red',
                            'transform': 'translateX(-500px)',
                            'opacity': '0',
                            'transition': 'all ease-in-out .45s'
                        }).dequeue();
                    });
                }
            }
        })

    })

    $('#addToFav').click(function () {
        var productId = $('#addToCartId').html();
        $.ajax({
            url: '/Favorite/AddToFav',
            data: { productId: productId },
            success: function () {
                $('#Message').html('Added to favorites').css({
                    'color': 'green',
                    'transform': ' translateY(0px)',
                    'opacity': '1',
                    'transition': 'all ease-in-out .45s'
                }).delay(2000).queue(function () {
                    $(this).css({
                        'color': 'green',
                        'transform': 'translateX(-500px)',
                        'opacity': '0',
                        'transition': 'all ease-in-out .45s'
                    }).dequeue();
                });
                
            },
            error: function (response) {
               
                if (response.responseJSON.title == 'Unauthenticated')
                    window.location.href = "/Account/Login"
                else {
                    $('#Message').html('Product already added').css({
                        'color': 'red',
                        'transform': ' translateY(0px)',
                        'opacity': '1',
                        'transition': 'all ease-in-out .45s'
                    }).delay(2000).queue(function () {
                        $(this).css({
                            'color': 'red',
                            'transform': 'translateX(-500px)',
                            'opacity': '0',
                            'transition': 'all ease-in-out .45s'
                        }).dequeue();
                    });
          
                }
            }
        })
    })

})