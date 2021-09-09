document.addEventListener("DOMContentLoaded", function (event)  {
    setInterval(window.onload = function () {
        const reg = /^[0-9]{12,13}$/,
            input = document.querySelectorAll('.numInput');
        for (var i = 0; i < input.length; i++) {
            input[i].addEventListener('input', function (e) {
                e.target.value = e.target.value.replace(reg, '');
            });
        }
    });
})

document.addEventListener("DOMContentLoaded", function (event) {
    let numInputMin = document.querySelectorAll("#numInputMin");
    numInputMin.forEach(p => {
        p.addEventListener("change", minValid, false);
    });

    let numInputMax = document.querySelectorAll("#numInputMax");
    numInputMax.forEach(p => {
        p.addEventListener("change", minValid, false);
    });

    let FilterBtn = document.getElementById('FilterBtn');
    FilterBtn.addEventListener('click', changeIsFilter, false);
})

function minValid() {
    let numInputMin = document.getElementById('numInputMin');
    let numInputMax = document.getElementById('numInputMax');
    if (numInputMin.value >= numInputMax.value) {
        numInputMin.value = 0;
    }
}

function changeIsFilter() {
    let IsFilter = document.getElementById('IsFilter');
    IsFilter.value = 'true';
    IsFilter.checked = true;
}