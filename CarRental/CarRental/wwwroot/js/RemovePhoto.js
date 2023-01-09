

function Remove(id) {

    let element = document.querySelector(`[data-id="${id}"]`);
    let table = document.getElementById("table");

    table.removeChild(element);

    if (table.children.length == 0) {
        Hide();
    }

    $.get(`/api/delete/${id}`, () => {

    });
}