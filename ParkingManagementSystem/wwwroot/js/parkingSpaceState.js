$(document).ready(function () {
    $('.seat').each(function () {
        var state = $(this).attr('state');
        if (state == 'available') {
            $(this).css('background-color', '#82CD47');
        } else if (state == 'reserved') {
            $(this).css('background-color', '#62CDFF');
        } else if (state == 'occupied') {
            $(this).css('background-color', '#FC2947');
        }
    });
});