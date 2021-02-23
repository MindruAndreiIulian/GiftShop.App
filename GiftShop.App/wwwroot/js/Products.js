$(document).ready(function () {

    sortHelper = function (search, comparator) {
        $('.box-wrapper').each(function () {
            var data = $(this).find(search).text();
            $(this).attr('data-name', data);
        });

        var $products = $('.products'),
            $singleProduct = $('.box-wrapper');

        $singleProduct.sort(comparator);
        $singleProduct.detach().appendTo($products);
    };

    var cmpPrice = function (a, b) {
        var an = a.getAttribute('data-name');
        var bn = b.getAttribute('data-name');
        var numberA = Number(an.replace(/[^0-9\.]+/g, ""));
        var numberB = Number(bn.replace(/[^0-9\.]+/g, ""));
        return numberA - numberB;
    };

    var cmpName = function (a, b) {
        var an = a.getAttribute('data-name');
        var bn = b.getAttribute('data-name');
        if (an > bn) {
            return 1;
        }
        if (an < bn) {
            return -1;
        }
        return 0;
    };

    var cmpPriceDesc = function (a, b) {
        return cmpPrice(b, a);
    };



    $('#alphabetical').click(function () {
        sortHelper('#productName', cmpName);
    })

    $('#lowestPrice').click(function () {
        sortHelper('#productPrice', cmpPrice);
    })

    $('#highestPrice').click(function () {
        sortHelper('#productPrice', cmpPriceDesc);
    })
})

