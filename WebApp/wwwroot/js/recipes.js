function addIngredient() {
    var ingredientContainer = document.getElementById("ingredientContainer");
    var input = document.createElement("input");
    input.type = "text";
    input.name = "Ingredients";
    input.classList.add("form-control");
    input.required = true;

    var button = document.createElement("button");
    button.type = "button";
    button.classList.add("btn", "btn-danger");
    button.textContent = "Remove";
    button.onclick = function () {
        removeIngredient(this);
    };

    var ingredientRow = document.createElement("div");
    ingredientRow.classList.add("ingredient-row");
    ingredientRow.appendChild(input);
    ingredientRow.appendChild(button);

    ingredientContainer.appendChild(ingredientRow);
}

function addStep() {
    var stepContainer = document.getElementById("stepContainer");
    var input = document.createElement("input");
    input.type = "text";
    input.name = "Steps";
    input.classList.add("form-control");
    input.required = true;

    var button = document.createElement("button");
    button.type = "button";
    button.classList.add("btn", "btn-danger");
    button.textContent = "Remove";
    button.onclick = function () {
        removeStep(this);
    };

    var stepRow = document.createElement("div");
    stepRow.classList.add("step-row");
    stepRow.appendChild(input);
    stepRow.appendChild(button);

    stepContainer.appendChild(stepRow);
}

function removeIngredient(button) {
    var row = button.closest('.row');
    row.remove();
}

function removeStep(button) {
    var row = button.closest('.row');
    row.remove();
}