function uploadImage() {
    var fileInput = document.getElementById("Cover");
    var previewImage = document.getElementById("previewImage");

    var file = fileInput.files[0];
    if (file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            previewImage.src = e.target.result;

            // الآن يمكنك إرسال الصورة إلى الخادم إذا كنت بحاجة إلى ذلك.
            // يمكنك استخدام AJAX لإرسال الصورة إلى الخادم بشكل غير مزامن.
            // على سبيل المثال:
            // $.ajax({
            //     url: "/YourPageHandler",
            //     type: "POST",
            //     data: { image: e.target.result },
            //     success: function(response) {
            //         console.log("Image uploaded successfully");
            //     },
            //     error: function(error) {
            //         console.error("Error uploading image: ", error);
            //     }
            // });
        };

        reader.readAsDataURL(file);
    }
}