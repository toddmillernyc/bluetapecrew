var getCart = function () {
    $.get("../../Cart/Index", function (data) {
        $(".top-cart-block").html(data);
        var isRtl = false;
        $(".scroller").each(function () {
            var height;
            if ($(this).attr("data-height")) {
                height = $(this).attr("data-height");
            } else {
                height = $(this).css("height");
            }
            $(this).slimScroll({
                allowPageScroll: true, // allow page scroll when the element scroll is ended
                size: "7px",
                color: ($(this).attr("data-handle-color") ? $(this).attr("data-handle-color") : "#bbb"),
                railColor: ($(this).attr("data-rail-color") ? $(this).attr("data-rail-color") : "#eaeaea"),
                position: isRtl ? "left" : "right",
                height: height,
                alwaysVisible: ($(this).attr("data-always-visible") === "1" ? true : false),
                railVisible: ($(this).attr("data-rail-visible") === "1" ? true : false),
                disableFadeOut: true
            });
        });
    });
}
var getPopUp = function (name) {
    alert(name);
}
var removeItem = function (id) {
    $.post("../../Cart/Delete/" + id, function () {
        getCart();
        $(".top-cart-content").show();
    });
}

var removeFromCart = function (id) {
    $.post("../../Cart/Delete/" + id, function () {
        document.location.reload(true);
    });
}
var removeItemCheckout = function (id) {
    $.post("../../Cart/Delete/" + id, function () {
        document.location.reload(true);
    });
}

var addItem = function () {
    $.post("../../Cart/Post/", { styleId: $("#StyleId").val(), quantity: $("#quantity").val() }, function () {
        $("#quantity").val(1);
        getCart();
    });
}

var getPrice = function (id) {
    $.get("/Products/GetStylePrice/" + id, function (data) {
        $("#price").html(data);
    });
}
getCart();
Layout.init();
Layout.initTwitter();
