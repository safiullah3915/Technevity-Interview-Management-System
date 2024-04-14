
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onload = function () {
    const sidebar = document.querySelector(".sidebar");
    const closeBtn = document.querySelector("#btn");
    const searchBtn = document.querySelector(".bx-search")

    closeBtn.addEventListener("click", function () {
        sidebar.classList.toggle("open")
        menuBtnChange()
    })


    function menuBtnChange() {
        if (sidebar.classList.contains("open")) {
            closeBtn.classList.replace("bx-menu", "bx-menu-alt-right")
        } else {
            closeBtn.classList.replace("bx-menu-alt-right", "bx-menu")
        }
    }
}

showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title); // Use the 'title' parameter to set the modal title
            $("#form-modal").modal('show');
        }
    })
}
    // Function to show the registration page in the modal
        function showRegistrationPopup() {
            $.ajax({
                type: "GET",
                url: "/Identity/Account/Register", // Update the URL to the correct registration page URL
                success: function (res) {
                    $("#form-modal .modal-body").html(res);
                    $("#form-modal .modal-title").html("Register"); // Set the modal title
                    $("#form-modal").modal('show');
                }
            });
    }

        // Function to close the modal
        function closeModal() {
            $("#form-modal").modal('hide');
    }

function closeModal() {
    $("#form-modal").modal("hide");
}


