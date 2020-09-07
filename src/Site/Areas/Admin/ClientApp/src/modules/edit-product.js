export class ProductEditor {

    static deleteStyle(styleId) {
        console.log($("#base"));
        if (!confirm("Are you sure you want to delete this style?")) return;

        $.post("Admin/AdminProducts/DeleteStyle/" + styleId, function (data) {
            if (data === "1") {
                var row = $("#styleRow" + styleId);
                row.html("");
            } else {
                alert(data);
            }
        });
    }

    static deleteImage(imageId) {
        var answer = confirm("Are you sure you want to delete this image?");
        if (!answer) return;
        $.post("Admin/AdminProducts/DeleteImage?imageId=" + imageId, function (response) {
            if (response === "1") {
                $("#imageRow" + imageId).html("");
            } else {
                alert(response);
            }

        });
    }

    static saveImageForm(event) {
        event.preventDefault();
        var target = event.target;
        console.log(target);
        var formData = new FormData(target[0]);
        console.log(formData);
        //$.ajax({
        //    url: "Admin/AdminProducts/SaveProductImage",
        //    type: "POST",
        //    data: formData,
        //    cache: false,
        //    contentType: false,
        //    processData: false,
        //    success: function () {
        //        window.location.reload();
        //    }
        //});

        return false;
    }

    static saveAdditionImage(event) {
        console.log(event);
        //disable the default form submission
        event.preventDefault();

        //grab all form data
        var target = event.target;
        var formData = new FormData(target[0]);
        console.log(formData);


        //$.ajax({
        //    url: "Admin/AdminProducts/AddAdditionalImage",
        //    type: "POST",
        //    data: formData,
        //    cache: false,
        //    contentType: false,
        //    processData: false,
        //    success: function (data) {
        //        if (data === 0) {
        //            alert("An error Occured");
        //        } else {
        //            window.location.reload();
        //        }
        //    }
        //});
        return false;
    }
}