function decreaseQuantity(sweetId) {
    var quantityElement = document.getElementById('quantity_' + sweetId);
    var quantityInput = document.getElementById('quantityInput_' + sweetId);
    var currentQuantity = parseInt(quantityElement.innerHTML);

    if (currentQuantity > 1) {
        quantityElement.innerHTML = currentQuantity - 1;
        quantityInput.value = currentQuantity - 1;
    }
}

function increaseQuantity(sweetId) {
    var quantityElement = document.getElementById('quantity_' + sweetId);
    var quantityInput = document.getElementById('quantityInput_' + sweetId);
    var currentQuantity = parseInt(quantityElement.innerHTML);

    quantityElement.innerHTML = currentQuantity + 1;
    quantityInput.value = currentQuantity + 1;
}

