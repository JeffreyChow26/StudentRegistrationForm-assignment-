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
    var emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;

    var emailAddress = document.getElementById('emailAddress').value;
    var password = document.getElementById('password').value;

    if (emailAddress == "") {
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
    if (password == "") {
        document.getElementById("message2").innerHTML = "<b>Please fill the required field.</b>"
        emptyField = true;
    }
    if (password !== "") {
        document.getElementById("message2").innerHTML = "<b></b>"
        emptyField = false;
    }
    if (emptyField == false && validEmail == true) {
        Login();
    }

}

function buildErrorMessage(ul, errorMessage) {
    var li = document.createElement('Li');
    li.innerHTML = errorMessage;
    ul.appendChild(li);

    return ul;
}

function Login() {
    var emailAddress = document.getElementById('emailAddress').value;
    var passwordHash = document.getElementById('password').value;

    var userObj = { EmailAddress: emailAddress, Password: passwordHash };
    console.log(emailAddress + " " + password);

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
    return fetch("/User/Login", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(dataObj)
    })
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}

