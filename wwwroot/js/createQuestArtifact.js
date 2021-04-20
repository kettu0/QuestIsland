let name = document.getElementById("quest");
let description = document.getElementById("description");
let price = document.getElementById("price");
let exp = document.getElementById("exp");
let radioBasic = document.getElementById("basictype");
let radioExtra = document.getElementById("extratype");
let errorName = document.getElementById("errorName");
let errorDescription = document.getElementById("errorDescription");
let errorValue = document.getElementById("errorValue");
let errorRadio = document.getElementById("errorRadio");
let button = document.getElementById("button");

function sendForm() {
    if (errorName.textContent.length === 0 && errorValue.textContent.length === 0 && errorDescription.textContent.length === 0 && errorRadio.textContent.length === 0) {
        alert("Database updated.");
    }
    else
        return false;
}

function checkName() {


    let errorMessage = "";

    if (name.value.trim().length == 0) {
        errorMessage += "\nThe name cannot contain only whitespaces!\n";
    }
    if ((!isNaN(name.value)) || (!/^[a-z/\s/]+$/i.test(name.value))) {
        errorMessage += "\nThe name can only contain letters and spaces!\n";
    }

    if (name.value.length >= 50) {
        errorMessage += "\nName too long! It should have no more than 50 characters!\n";
    }

    if (!checkCapitals()) {
        errorMessage += "\nName should start from a capital letter!\n";
    }

    errorName.textContent = errorMessage;
    errorName.style.color = "#dd9ce4";
}

function checkCapitals() {

    for (const element of name.value) {
        if (element[0] !== element[0].toUpperCase()) {
            return false;
        }
        return true;
    }
}

function checkRadios() {
    let errorMessage = "";

    if (!radioBasic.checked && !radioExtra.checked) {
        errorMessage += "You must choose a type!";
    }
        
    errorRadio.textContent = errorMessage;
    errorRadio.style.color = "#dd9ce4";
}

function checkDescription() {


    let errorMessage = "";

    if (description.value.trim().length == 0) {
        errorMessage += "\nThe description cannot contain only whitespaces!\n";
    }

    if (description.value.length >= 200) {
        errorMessage += "\nDescription too long! It should have no more than 200 characters!\n";
    }

    errorDescription.textContent = errorMessage;
    errorDescription.style.color = "#dd9ce4";
}

function checkNumeralValues() {

    let errorMessage = "";

    if (isNaN(price.value) || isNaN(exp.value)) {
        errorMessage += "The above value must be a number!";
    }

    errorValue.textContent = errorMessage;
    errorValue.style.color = "#dd9ce4";
}
    

button.addEventListener("click", checkName);
button.addEventListener("click", checkRadios);
button.addEventListener("click", checkDescription);
button.addEventListener("click", checkNumeralValues);