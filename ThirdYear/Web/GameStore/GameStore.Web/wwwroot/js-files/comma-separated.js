document.addEventListener("DOMContentLoaded", function (event) {
    let listGenres = document.querySelectorAll(".listGenres");
    listGenres.forEach(p => {
        p.addEventListener("click", addGenres, false);
    });
    let listPlatforms = document.querySelectorAll(".listPlatforms");
    listPlatforms.forEach(p => {
        p.addEventListener("click", addPlatforms, false);
    });
    let listPublishers = document.querySelectorAll(".listPublishers");
    listPublishers.forEach(p => {
        p.addEventListener("click", addPublishers, false);
    });
    addGenres();
    addPlatforms();
    addPublishers();
})

function addGenres() {
    let inputGenres = document.getElementById('inputGenres');
    let els = document.querySelectorAll(".listGenres");
    inputGenres.value = '';
    let strList = '';
    for (let i = 0; i < els.length; i++) {
        if (els[i].checked) {
            strList += els[i].value + ',';
        }
    }
    inputGenres.value = strList.slice(0, -1);
}

function addPlatforms() {
    let inputPlatforms = document.getElementById('inputPlatforms');
    let els = document.querySelectorAll(".listPlatforms");
    inputPlatforms.value = '';
    let strList = '';
    for (let i = 0; i < els.length; i++) {
        if (els[i].checked) {
            strList += els[i].value + ',';
        }
    }
    inputPlatforms.value = strList.slice(0, -1);
}

function addPublishers() {
    let inputPublishers = document.getElementById('inputPublishers');
    let els = document.querySelectorAll(".listPublishers");
    inputPublishers.value = '';
    let strList = '';
    for (let i = 0; i < els.length; i++) {
        if (els[i].checked) {
            strList += els[i].value + ',';
        }
    }
    inputPublishers.value = strList.slice(0, -1);
}