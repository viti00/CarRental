function RemoveAll() {

    let btn = document.getElementById("delete-all-btn");

    let carId = btn.getAttribute("car-id");


    let table = document.getElementById("table");

    table.innerHTML = "";

    Hide();

    $.get(`/api/all/${carId}`, () => {

    });
}