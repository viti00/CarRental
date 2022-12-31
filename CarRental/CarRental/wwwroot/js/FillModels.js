
$('#make-select').on('change', FillModels)

function FillModels() {
    let models = document.getElementById('model-select');
    models.innerHTML = "";
    let item = document.getElementById('make-select');
    let makeId = item.value;
    if (makeId == 0) {
        models.setAttribute("disabled", "true");
    }
    else {
        $.get(`/api/models/${makeId}`, (result) => {
            AddToSelectList(models, result);
        });
    }
}