document.addEventListener("DOMContentLoaded", () => {
    let form = document.querySelector('Form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});

function Validation() {
    var emptyField = true;
    var validEmail = false;
    var passwordMatch = false;

    var emailAddress = document.getElementById('emailAddress').value;
    var passwordFirst = document.getElementById('password1').value;
    var passwordSecond = document.getElementById('password2').value;

    var emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;

    if (emailAddress == ""){
        document.getElementById("message1").innerHTML = "<b>Please fill the required field.</b>"
        emptyField = true;
    }
    if (emailAddress !== "") {
        document.getElementById("message1").innerHTML = "<b></b>"
        emptyField = false;
    }
    if (!emailAddress.match(emailPattern) && emailAddress !== "") {
        document.getElementById("message1").innerHTML = "<b>Please enter a valid email.</b>"
        validEmail = false;
        emptyField = true;
    }
    if (emailAddress.match(emailPattern) && emailAddress !== "") {
        document.getElementById("message1").innerHTML = "<b></b>"
        validEmail = true;
        emptyField = false;
    }
    if (passwordFirst == "") {
        document.getElementById("message2").innerHTML = "<b>Please fill the required field.</b>"
        emptyField = true;
    }
    if (passwordFirst !== "") {
        document.getElementById("message2").innerHTML = "<b></b>"
        emptyField = false;
    }

    if (passwordSecond == "") {
        document.getElementById("message3").innerHTML = "<b>Please fill the required field.</b>"
        emptyField = true;
    }
    if (passwordSecond !== "") {
        document.getElementById("message3").innerHTML = "<b></b>"
        emptyField = false;
    }

    if (passwordFirst !== passwordSecond) {
        document.getElementById("message2").innerHTML = "<b>Password does not match.</b>"
        document.getElementById("message3").innerHTML = "<b>Password does not match.</b>"
    }

    if (passwordFirst == passwordSecond && passwordFirst !== "" && passwordSecond !== "") {
        document.getElementById("message2").innerHTML = "<b>Password match.</b>"
        document.getElementById("message3").innerHTML = "<b>Password match.</b>"
        passwordMatch = true;
    }

    if (emptyField == false && validEmail == true && passwordMatch == true) {
        Register();
    }

}

function buildErrorMessage(ul, errorMessage) {
    var li = document.createElement('Li');
    li.innerHTML = errorMessage;
    ul.appendChild(li);
    return ul;
}

function Register() {
        var emailAddress = document.getElementById('emailAddress').value;
        var passwordHash = document.getElementById('password1').value;

        var userObj = { EmailAddress: emailAddress, Password: passwordHash };
        sendData(userObj).then((result) => {
            if (result.hasErrors) {
                var ul = document.createElement('UL');
                const errorPane = document.getElementById("errorPane");
                errorPane.innerHTML = "";
                for (var i = 0; i < result.data.length; i++) {
                    buildErrorMessage(ul, result.data[i].ErrorMessage);
                }
                errorPane.appendChild(ul);
            }
            else {
                window.location = result.url;
            }
        })
        .catch((error) => {
            toastr.error('Unable to make request!!');
        });
}
function sendData(dataObj) {
    return fetch("/User/Register", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(dataObj)
    })
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}



