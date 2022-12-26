
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

function AddToSelectList(parent, childs) {
    let emptyOption = document.createElement("option");
    parent.appendChild(emptyOption);
    for (var i = 0; i < childs.length - 1; i++) {
        let optionElement = document.createElement("option");
        optionElement.value = childs[i].id;
        optionElement.text = childs[i].name

        parent.appendChild(optionElement);
    }
    parent.removeAttribute("disabled");
}