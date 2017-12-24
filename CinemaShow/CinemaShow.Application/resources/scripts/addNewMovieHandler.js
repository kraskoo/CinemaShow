(function () {
    var categories = document.getElementById("categories");
    var selectionList = document.getElementById("selection");
    var newCategoryField = document.getElementById("new-cat-field");
    var addNewCategory = document.getElementById("new-cat");
    function getAllCategories(data) {
        for (var i = 0; i < data.length; i++) {
            categories.innerHTML += "<option value='" + data[i] + "'>" + data[i] + "</option>";
        }
    };

    addNewCategory.addEventListener("click", function () {
        if (newCategoryField.value === "") {
            return;
        } else {
            $.ajax({
                url: "/api/Service/AddCategory?category=" + newCategoryField.value,
                method: "POST"
            }).done(function () { location.reload(); });
        }
    });

    document.getElementById("move-cat").addEventListener("click", function () {
        var allCat = Object.values(categories);
        for (var i = 0; i < allCat.length; i++) {
            if ((allCat[i] instanceof HTMLElement) && allCat[i].selected) {
                categories.removeChild(allCat[i]);
                selectionList.appendChild(allCat[i]);
            }
        }
    });

    $.ajax({
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
        },
        url: "/api/Service/GetAllCategories",
        method: "GET"
    }).done(getAllCategories);
}());