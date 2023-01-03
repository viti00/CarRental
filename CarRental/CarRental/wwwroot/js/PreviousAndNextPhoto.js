let prevControl = document.querySelector(".carousel-control-prev");
let nextControl = document.querySelector(".carousel-control-next");
let photosCount = document.querySelectorAll(".carousel-item").length;

if (photosCount <= 1) {
    let element = document.getElementById("next-btn");
    element.classList.add("display-none");
}

function Previous() {
    let activeElement = document.querySelector(".active");

    let previousPosition = Number(activeElement.getAttribute("position")) - 1;

    if (previousPosition < 0) {
        previousPosition = 0;
    }

    let priviousElement = document.querySelector(`[position="${previousPosition}"]`);

    activeElement.classList.remove("active");

    priviousElement.classList.add("active");

    if (nextControl.classList.contains("display-none")) {
        nextControl.classList.remove("display-none");
    }

    if (previousPosition == 0) {

        prevControl.classList.add("display-none");
    }

}

function Next() {
    let activeElement = document.querySelector(".active");

    let nextPosition = Number(activeElement.getAttribute("position")) + 1;

    if (nextPosition > photosCount - 1) {
        nextPosition = photosCount - 1;
    }

    let nextElement = document.querySelector(`[position="${nextPosition}"]`);

    activeElement.classList.remove("active");

    nextElement.classList.add("active");


    if (prevControl.classList.contains("display-none")) {
        prevControl.classList.remove("display-none");
    }

    if (nextPosition == photosCount - 1) {
        nextControl.classList.add("display-none");
    }
}