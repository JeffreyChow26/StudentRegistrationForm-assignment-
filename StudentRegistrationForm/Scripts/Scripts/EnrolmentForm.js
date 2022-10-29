document.addEventListener("DOMContentLoaded", () => {
    let form = document.querySelector('Form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});

function Validation() {

}


function SubmitForm() {

    console.log("Submit form");

    var name = document.getElementById('name').value;
    var surname = document.getElementById('surname').value;
    var address = document.getElementById('address').value;
    var phoneNumber = document.getElementById('phoneNumber').value;
    var date = document.getElementById('date').value;
    var guardianName = document.getElementById('guardianName').value;
    var emailAddress = document.getElementById('emailAddress').value;
    var nid = document.getElementById('nid').value;
    var subject = document.getElementById('subject').value;

    var studentObj = {
        Name: name, Surname: surname, Address: address, PhoneNumber: phoneNumber,
        DateOfBirth: date, GuardianName: guardianName, EmailAddress: emailAddress,
        NID: nid
    }

    var testObj = {
        PhoneNumber: phoneNumber,EmailAddress: emailAddress,NID: nid
    }


    sendData(testObj).then((response) => {
        if (response.result) {
            toastr.success("The form has been submitted successfully.");
            window.location = response.url;
        }
        else {
            toastr.error('Unable to submit the form.');
            return false;
        }
    })
        .catch((error) => {
            toastr.error('Unable to make request!!');
        });

}

function sendData(testObj) {
    return fetch("/Student/RegisterForm", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}


window.onload = function () {
    DisplaySubject().then((response) => {
        console.log(response);
        var select = document.getElementById("subjects");
        select.innerHTML = response.map(x => `<option value="${x.Id}">${x.Name}</option>`).join('');
    })
        .catch((error) => {
            toastr.error('Unable to make request!!');
        });
}


function DisplaySubject() {
    return fetch("/Subject/SendSubject", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}