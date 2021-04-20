let password = document.getElementById("password");
let repeatedPass = document.getElementById("newpassword");
let oldPassword = document.getElementById("oldpassword");
let oldPasswordFromDB = document.getElementById("oldpasswordfromDB");
let errorPassword = document.getElementById("errorPassword");
let errorNotTheSame = document.getElementById("errorNotTheSame");
let errorOld = document.getElementById("errorOld");
let button = document.getElementById("button");

function sendForm() {
    if (errorPassword.textContent.length === 0 && errorNotTheSame.textContent.length === 0 && errorOld.textContent.length === 0) {
        alert("Password changed");
    }
    else
        return false;
}

function checkPswd() {

    let errorMessage = "";

    if (password.value.trim().length == 0) {
        errorMessage += "\nPassword cannot be empty\n";
    }

    if (password.value.length < 8) {
        errorMessage += "\nPassword should have at least 8 characters!\n";
    }

    if (!containsSpecialChars()) {
        errorMessage += "\nPassword should contain at least one special character!\n";
    }

    if (!containsNumbers()) {
        errorMessage += "\nPassword should contain at least one digit!\n";
    }

    if (containsWhiteSpace()) {
        errorMessage += "\nPassword should not contain whitespaces!\n";
    }

    if (!containsCapitals()) {
        errorMessage += "\nPassword should contain at least one uppercase letter!\n";
    }

    if (!containsLower()) {
        errorMessage += "\nPassword should contain at least one uppercase letter!\n";
    }

    errorPassword.textContent = errorMessage;
    errorPassword.style.color = "#dd9ce4";
}

function containsSpecialChars() {

    let counter = 0;
    const chars = "~`!\"\'\\@#$%^&*()_+=-|}{[]:;?><,.";

    for (const elem of password.value) {
        for (const char of chars) {
            if (elem == char) {
                counter++;
            }

        }
    }

    if (counter > 0) {
        return true;
    }
    else {
        return false;
    }
    
}

function containsNumbers() {
    let counter = 0;
    const nums = "1234567890";

    for (const elem of password.value) {
        for (const num of nums) {
            if (elem == num) {
                counter++;
            }
            
        }
    }

    if (counter > 0) {
        return true;
    }
    else {
        return false;
    }
}

function containsWhiteSpace() {
    if (password.value.includes(" ")) {
        return true;
    }
    else {
        return false;
    }

}

function containsCapitals() {
    let counter = 0;
    let letters = "qwertyuiopasdfghjklzxcvbnm".toUpperCase();

    for (const elem of password.value) {
        for (const letter of letters) {
            if (elem == letter) {
                counter++;
            }

        }
    }

    if (counter > 0) {
        return true;
    }
    else {
        return false;
    }
}

function containsLower() {
    let counter = 0;
    let letters = "qwertyuiopasdfghjklzxcvbnm";

    for (const elem of password.value) {
        for (const letter of letters) {
            if (elem == letter) {
                counter++;
            }

        }
    }

    if (counter > 0) {
        return true;
    }
    else {
        return false;
    }
}

function checkPasswords() {
    let errorMessage = "";
    if (repeatedPass.value !== password.value) {
        errorMessage += "\nPasswords do not match.\n";
    }
    errorNotTheSame.textContent = errorMessage;
    errorNotTheSame.style.color = "#dd9ce4";
}

function checkOldPassword() {
    let errorMessage = "";
    if (oldPassword.value !== oldPasswordFromDB.value) {
        errorMessage += "\nOld password invalid.\n";
    }
    
    errorOld.textContent = errorMessage;
    errorOld.style.color = "#dd9ce4";
}

button.addEventListener("click", checkPswd);
button.addEventListener("click", checkPasswords);
button.addEventListener("click", checkOldPassword);