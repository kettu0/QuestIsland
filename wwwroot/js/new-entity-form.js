const boxes = ["quest", "description", "price"];
const types = ["basictype", "extratype"];
const button = document.getElementById("submit");
let errorName = document.getElementById("error-name");
let errorNumber = document.getElementById("error-number");

function checkIfBoxFilled(id) {
    const content = document.getElementById(id).value;
    if (content.length === 0)
        return false;
    return true;
}

function checkIfRadioChecked(id) {
    if (!document.getElementById(id).checked)
        return false;
    return true;
}


function checkAllBoxes(array) {
    let counter = 0;
    for (const element of array) {
        if (checkIfBoxFilled(element) === true) {
            counter++;
            if (counter === array.length)
                return true;
        }
    }
    return false;
}


function checkAllRadio(array) {
    for (const element of array) {
        if (checkIfRadioChecked(element) === true) {
            return true;
        }
    }
    return false;
}


function checkCapitals() {
    let content = document.getElementById("quest").value;
    for (const element of content) {
        if (element[0] !== element[0].toUpperCase()) {
            return false;
        }
        return true;
    }
}


function checkName() {
    const content = document.getElementById("quest").value;
    let errorMessage = "";

    if (!content.trim()) {
        errorMessage += "\nThe name cannot contain only whitespaces!\n";
    }
    if ((!isNaN(content)) || (!/^[a-z/\s/]+$/i.test(content))) {
        errorMessage += "\nThe name can only contain letters and spaces!\n";
    }

    if (content.length >= 50) {
        errorMessage += "\nName too long! It should have no more than 50 characters!\n";
    }

    if (!checkCapitals()) {
        errorMessage += "Name should start from a capital letter";
    }

    errorName.textContent = errorMessage;
    errorName.style.color = "#d17171";
}


function checkNumeralValues() {
    const content = document.getElementById("price").value;
    let errorMessage = "";

    if (!isInteger(content)) {
        errorMessage += "This value should be a number!";
    }

    errorNumber.textContent = errorMessage;
    errorNumber.style.color = "#d17171";
}



function enableButton() {
    if (checkAllBoxes(boxes) && checkAllRadio(types)) {
        document.getElementById("submit").disabled = false;
    }

    if (!checkAllBoxes(boxes)) {
        document.getElementById("submit").disabled = true;
    }
}


function sendForm() {
    if (errorName.textContent.length === 0 && errorNumber.textContent.length === 0) {
        alert("Database updated.");
    }
    else
        return false;
}


button.addEventListener("click", checkName);
button.addEventListener("click", checkNumeralValues);
document.addEventListener("input", enableButton);


