$(document).ready(function () {
    $('.userRow').each(function () {
        var userId = $(this).find("#userId").html();
        var radio = $(this).find("#userRole");
        
        radio.click(function () {
            var role = $(this).val();
            $.ajax({
                url: '/Administrator/ChangeUserRole',
                data: {
                    userId: userId,
                    roleId: role
                },
                success: function () {
                    radio.each(function () { 
                            $(this).prop("checked", false);
                    })
                },
                error: function () {
                    console.log('error')
                }
            })
            
        })
    })
})