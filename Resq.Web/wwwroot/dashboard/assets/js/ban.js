$(document).ready(function () {
    $('input[type="radio"]').click(function () {
        var inputValue = $(this).attr("value");
        console.log(inputValue);
        //var targetBox = $("." + inputValue);
        //$(".box").not(targetBox).hide();
        //$(targetBox).show();
        //$(targetBox).style.diplay = "flex";
    });
});