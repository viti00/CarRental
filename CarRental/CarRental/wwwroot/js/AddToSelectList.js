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