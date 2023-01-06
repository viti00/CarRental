
let makeSelector = document.getElementById("make-select");

if (makeSelector.value > 0) {
    let modelSelector = document.getElementById("model-select");

    modelSelector.removeAttribute("disabled");
}
