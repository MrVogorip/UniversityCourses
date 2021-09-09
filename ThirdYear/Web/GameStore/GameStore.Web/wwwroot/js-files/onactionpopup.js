document.addEventListener("DOMContentLoaded", function (event) {
    PopUpHide();
});

function PopUpShow() {
    document.querySelectorAll(".popup-link").forEach(p => { p.style.display = "block" });
}

function PopUpHide() {
    document.querySelectorAll(".popup-link").forEach(p => { p.style.display = "none" });
}

function PopUpShowById(id) {
    let el = document.getElementsByClassName(id)
    el[0].style.display = "block";
}