$(document).ready(function () {

    //var buttons = $('.removeAll');
    //$.each(buttons, (index, value) => {
    //    var id = value.id;
    //    $('#' + id).click(function () {
    //        $.ajax({
    //            url: '/Cart/RemoveAll',
    //            data: { productId: id },
    //            succes: function () {
                    
    //            },
    //            error: function () {
    //                console.log('error')
    //            }
    //        })
    //    })
        
    //})

    $('#addToFav').click(function () {
        var productId = $(this).find('#productId').html();
        
        $.ajax({
            url: '/Favorite/AddToFav',
            data: { productId: productId },
            success: function (response) {
                console.log(response)
                $('#Message').html('Added to favorites').css({ "color": "lightgreen" });
            },
            error: function (response) {
                console.log(response)
                if (response.responseJSON.title == 'Unauthenticated')
                    window.location.href = "/Account/Login"
                else {
                    $('#Message').html('Product already added').css({ "color": "red" });

                }
            }
        })
    })
})