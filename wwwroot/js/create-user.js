function hideNotWantedElements() {
    [].forEach.call(document.querySelectorAll('.display-when-mentor, .display-when-codecooler'), function (el) {
        el.style.display = 'none';
    });
}

function displayUserFieldsDependingOn() {
    const role = checkWhichRoleWasChosen();
    let elemClass = "_";
    if (role == "M") {
        elemClass = ".display-when-mentor";
    }
    else if (role == "C") {
        elemClass = ".display-when-codecooler";
    }
    hideNotWantedElements();
    [].forEach.call(document.querySelectorAll(elemClass), function (el) {
        el.style.display = 'inherit';
    });
}

function checkWhichRoleWasChosen() {
    var radios = Array.from(document.querySelectorAll('.user-role'));
    for (i=0;i<radios.length;i++) {
        if (radios[i].checked == true) {
            return radios[i].getAttribute("value");
        }
    }
}

hideNotWantedElements();

