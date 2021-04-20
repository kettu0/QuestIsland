let sendButton = document.getElementsByName("sendButton")[0];
document.querySelectorAll('[message="message"], [nickname="nickname"], [email="email"]');
let messageError = document.getElementById("messageError");
let nicknameError = document.getElementById("nicknameError");
let emailError = document.getElementById("emailError");

let checkName = function () {

    let messages = "";
    if (nickname.value.trim().length == 0 || nickname.value.length > 15) {
        messages += "Name too short or too long (max 15 characters). "
    }

    if (!/^[a-z/\s/]+$/i.test(nickname.value)) {
        messages += "Nickname field can contain only letters and spaces. "
    }

    nicknameError.textContent = messages;
    nicknameError.style.color = "#dd9ce4";
}

let checkMessage = function () {

    let messages = "";
    if (message.value.trim().length == 0) {
        messages += "Comment field can not be empty. "
    }

    messageError.textContent = messages;
    messageError.style.color = "#dd9ce4";
}

let checkEmail = function () {
    let messages = "";
    if (email.value.trim().length == 0) {
        messages += "Email field can not be empty. "
    }

    emailError.textContent = messages;
    emailError.style.color = "#dd9ce4";
}

let validateForm = function () {

    if (messageError.textContent.length == 0 && nicknameError.textContent.length == 0 && emailError.textContent.length == 0) {

        alert("Thank you! Your message has been put in a bottle!");
    }
    else {
        return false
    }
}

sendButton.addEventListener('click', checkName);
sendButton.addEventListener('click', checkMessage);
sendButton.addEventListener('click', checkEmail);