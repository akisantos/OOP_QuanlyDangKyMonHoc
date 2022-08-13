
let screenWidth = 0;

$(".blackDiv").hide();

$(".toggleLeftSideMenu").click(() => {
    if ($("#leftside-menu").hasClass("hideLeftMenu")) {

        $("#leftside-menu").removeClass("hideLeftMenu");
        $("#leftside-menu").show();
        $('#leftside-menu').css('transform', 'translateX(0)');
        $('#leftside-menu').css('width', '0');
        $("#leftside-menu").animate({ width: '16.25rem' });
        if (screenWidth < 768)
            $(".blackDiv").show();
    } else {
        $('#leftside-menu').css('transform', 'translateX(-100%)');
        $("#leftside-menu").animate({ width: '0rem' });
        $("#leftside-menu").addClass("hideLeftMenu");

        setTimeout(() => {

            $("#leftside-menu").hide();
            if (screenWidth < 768) {
                $(".blackDiv").hide();
            }
        }, 500);
    }
})

$('.menu .menu-item').click(function () {
    $('.menu').find('.menu-item').removeClass("active");
    $(this).addClass("active");;
});

$(".blackDiv").click(() => {
    $('#leftside-menu').css('transform', 'translateX(-100%)');
    $("#leftside-menu").animate({ width: '0rem' });
    $("#leftside-menu").addClass("hideLeftMenu");

    setTimeout(() => {

        $("#leftside-menu").hide();
        $(".blackDiv").hide();
    }, 500);
})

$("#toggle-leftside").click(() => {

    if (screenWidth > 600) {
        if ($("#toggle-leftside").hasClass("spin")) {
            $("#toggle-leftside").removeClass("spin");
        } else {
            $("#toggle-leftside").addClass("spin");
        }


    } else {
        if ($("#toggle-leftside").hasClass("spin")) {
            $("#toggle-leftside").removeClass("spin");
        } else {
            $("#toggle-leftside").addClass("spin");
        }


    }
})



$("#theo-ke-hoach-btn").click(() => {
    $("#ngoai-ke-hoach-btn").removeClass("btnActive");
    $("#theo-ke-hoach-btn").addClass("btnActive");
    $(".contentWrapper").empty();
    for (let index = 0; index < 10; index++) {
        $('<div class="contentCard"> <div class="cardContent"> <div class="cardMainContent"> <p>Mã: 0001122333</p> <h5>Trong kế hoạch</h5> <br> <p>Số tín chỉ: 4</p> <p>Môn tiên quyết: Lập trình cơ bản</p> </div> <div class="cardSubContent"> </div> </div> <div class="cardImage"> <div class="buttonAction"> <a href="#" class="btn btn-outline dangKy" id="register-btn" value=' + index + '> Đăng ký</a> <a href="#" class="btn btn-outline"> Đánh dấu</a> </div> </div> </div>').appendTo(".contentWrapper");
    }
})

$("#ngoai-ke-hoach-btn").click(() => {
    $("#ngoai-ke-hoach-btn").addClass("btnActive");
    $("#theo-ke-hoach-btn").removeClass("btnActive");
    $(".contentWrapper").empty();
    for (let index = 0; index < 10; index++) {
        $('<div class="contentCard"> <div class="cardContent"> <div class="cardMainContent"> <p>Mã: 0001122333</p> <h5>Ngoài kế hoạch</h5> <br> <p>Số tín chỉ: 4</p> <p>Môn tiên quyết: Lập trình cơ bản</p> </div> <div class="cardSubContent"> </div> </div> <div class="cardImage"> <div class="buttonAction"> <a href="#" class="btn btn-outline dangKy" id="register-btn" value=' + index + '> Đăng ký</a> <a href="#" class="btn btn-outline"> Đánh dấu</a> </div> </div> </div>').appendTo(".contentWrapper");
    }
})


$(".buttonAction").click(() => {
    OpenModal("Thông báo", $(".buttonAction").find(this).attr('value'));
})

$(".profile").click(() => {
    if (!$(".dropdownMenu").hasClass("showDropDown")) {
        $(".dropdownMenu").addClass("showDropDown");
    } else {
        $(".dropdownMenu").removeClass("showDropDown");
    }
})

function OpenModal(title = "", mess = "", time = 5000) {

    $(".infoModal").removeClass("infoModalShow");

    $("#modal-title").html(title);
    $("#modal-content").html(mess);
    $(".infoModal").addClass("infoModalShow");
    timer = window.setTimeout(() => {
        $(".infoModal").removeClass("infoModalShow");
    }, time);

}

console.log("Loaded");

// Responsive
$(window).on('load', function () {
    var win = $(this); //this = window
    screenWidth = win.width();
    if (win.width() < 1280) {
        $('#leftside-menu').css('transform', 'translateX(-100%)');
        $("#leftside-menu").animate({ width: '0rem' });
        $("#leftside-menu").addClass("hideLeftMenu");
        setTimeout(() => {

            $("#leftside-menu").hide();

        }, 500);

    }
});

$(window).on('resize', function () {
    var win = $(this); //this = window
    screenWidth = win.width();


    if (win.width() < 1280) {
        $('#leftside-menu').css('transform', 'translateX(-100%)');
        $("#leftside-menu").animate({ width: '0rem' });
        $("#leftside-menu").addClass("hideLeftMenu");
        setTimeout(() => {

            $("#leftside-menu").hide();

        }, 500);

    }
});