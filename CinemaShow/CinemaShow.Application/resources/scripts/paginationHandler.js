(function () {
    var pagesUl = document.getElementById("pages");

    function appendPageMarks(data) {
        var currentPage = location.href;
        var page = currentPage.split("=").length > 1 ? parseInt(currentPage.split("=")[1]) : 1;
        var pages = Math.ceil(data / 8);
        for (var i = 0; i < pages; i++) {
            pagesUl.innerHTML += ((i + 1) === page ? ("<li class='active'><a href='#'>" + (i + 1) + "</a></li>") : ("<li><a href='/Home/Index?page=" + (i + 1) + "'>" + (i + 1) + "</a></li>"));
        }
    };

    $.ajax({
        url: "/api/Service/GetMoviesCount",
        method: "GET"
    }).done(appendPageMarks);
}());