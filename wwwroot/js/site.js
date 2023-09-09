$(".bar-btn").click(function () { 
    $(".side-layer").css("visibility", "visible");
    $(".sidebar-container").css("transform", "translateX(0px)");
});


$(".side-layer").click(function () { 
    $(".side-layer").css("visibility", "hidden");
    $(".sidebar-container").css("transform", "translateX(-385px)");
});


$(".image").change(function (e) { 
    $(".uploadButton").css("background-image", `url(${1})`);
    
});