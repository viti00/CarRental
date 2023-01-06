let takeData = document.getElementById("take-data-field");

if (takeData.value != "") {
    let returnData = document.getElementById("return-data-field");

    returnData.removeAttribute("disabled");
}