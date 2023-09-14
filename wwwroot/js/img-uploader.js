$(".image-uploader").change( (e) => { 
    et = e.target;

    $("#IsOld").attr("value", "0");
    const file = et.files[0];
    if (file) {
        const reader = new FileReader();

        reader.onload = function (e) {
            // Set the background of the imageContainer to the uploaded image
            $(".uploadButton").css("background-image", `url(${e.target.result})`);
            // imageContainer.style.display = "block"; // Show the image container
        };
        
        reader.readAsDataURL(file);
        // $(".my-img-drop").css("visibility", "visible");
    }else{
        $(".uploadButton").css("background-image", `url(${"/images/add_image.png"})`);
    }
});


