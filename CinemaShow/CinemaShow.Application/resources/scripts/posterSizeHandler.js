(function () {
    var iframePanel = document.getElementById("iframe-panel");
    window.addEventListener("resize", function () {
        document.getElementById("img-poster").style.height = $(iframePanel).height() + "px";
    });
    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById("img-poster").style.height = $(iframePanel).height() + "px";
    });
}());