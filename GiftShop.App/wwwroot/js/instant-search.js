$(document).ready(function () {
    $("#searchInput").keyup(function () {
        $(".instant-search__results-container").addClass("visible");
        

        var query = $("#searchInput").val();
        $.ajax({
            url: "/Administrator/GetProductsForSearch",
            data: { name: query },
            success: function (result) {
                var items = result.results;
                console.log(items)
                $('.instant-search__results-container').html(items.map(i => {
                        var route = '/Products/ProductPage?productid=' + i.id;
                    return `<a href=${route} class="instant-search__result">
                                    <div class="instant-search__title"><img width="100px" src="/Media/GetImage/${i.image}"/></div>
                                    <div class="instant-search__title">${i.text}</div>
                                </a>`;
                    }));
            }
        })
    })

    $("#searchInput").focusout(function () {
       
        $(".instant-search__results-container").removeClass("visible");
    })

})