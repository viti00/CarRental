
$('#take-data-field').on('change', DisabledFalse);

function DisabledFalse() {
    let returnData = document.getElementById('return-data-field');
    let item = document.getElementById('take-data-field');
    if (item.value == "") {
        returnData.setAttribute("disabled", "true");
        returnData.value = "";
    }
    else {
        returnData.removeAttribute("disabled");
    }
}