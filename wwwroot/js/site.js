// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const aboutMe = document.querySelector("#AboutMe");
    const counter = document.querySelector("#characterCount");

    if (aboutMe && counter) {
        const updateCount = () => {
            counter.textContent = `${aboutMe.value.length} / 250`;
        };
        aboutMe.addEventListener("input", updateCount);
        updateCount();
    }

    document.querySelectorAll(".form-control, .form-select").forEach(function (field) {
        field.addEventListener("input", function () {
            if (this.classList.contains("input-validation-error") && this.value) {
                this.classList.remove("input-validation-error");
            }
        });
    });
});
