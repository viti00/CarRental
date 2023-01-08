
$("#fuel").on("input", ReplaceFuel)
$("#power").on("input", ReplacePower)
$("#power-max").on("input", ReplacePower)
$("#price").on("input", ReplacePrice)
$("#price-max").on("input", ReplacePrice)

function ReplaceFuel() {
    let fuelField = document.getElementById("fuel");

    if (fuelField.value.includes(',')) {
        fuelField.value = fuelField.value.replace(',', '.')
    }
}

function ReplacePower() {
    let powerField = document.getElementById("power");
    let powerFieldMax = document.getElementById("power-max");

    if (powerField.value.includes(',')) {
        powerField.value = powerField.value.replace(',', '.')
    }
    if (powerFieldMax.value.includes(',')) {
        powerFieldMax.value = powerFieldMax.value.replace(',', '.')
    }
}

function ReplacePrice() {
    let priceField = document.getElementById("price");
    let priceFieldMax = document.getElementById("price-max");


    if (priceField.value.includes(',')) {
        priceField.value = priceField.value.replace(',', '.')
    }
    if (priceFieldMax.value.includes(',')) {
        priceFieldMax.value = priceFieldMax.value.replace(',', '.')
    }
}