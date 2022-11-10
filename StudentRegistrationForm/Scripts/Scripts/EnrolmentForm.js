document.addEventListener("DOMContentLoaded", () => {
    let form = document.querySelector('Form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});


const maxsubjects = 3;
const minsubjects = 1;
let currentsubjects = 0;
let index = 1;
let resultSelection = document.getElementById('resultSelection');

function AddSubject() {
    let selectSubject = document.createElement("select");
    let selectGrade = document.createElement("select");
    selectSubject.setAttribute('id', 'subjects' + index);
    selectGrade.setAttribute('id', 'grades' + index);
    var option = document.createElement("option");
    if (currentsubjects < maxsubjects) {
        currentsubjects++;
        index++;
        selectSubject.appendChild(option);
        selectGrade.appendChild(option);
        resultSelection.appendChild(selectSubject);
        resultSelection.appendChild(selectGrade);

        DisplaySubject().then((response) => {
            console.log(response);
            var select = document.getElementById("subjects"+ (index-1));
            select.innerHTML = response.map(x => `<option value="${x.Id}">${x.Name}</option>`).join('');
        })
        .catch((error) => {
            toastr.error('Unable to make request!!');
       });

        DisplayGrade().then((response) => {
            console.log(response);
            var select = document.getElementById("grades" + (index - 1));
            select.innerHTML = response.map(x => `<option value="${x.Mark}">${x.Name}</option>`).join('');
        })
            .catch((error) => {
                toastr.error('Unable to make request!!');
            });
    }
}

function RemoveSubject() {
    var subjectId = document.getElementById("subjects" + (index - 1));
    var gradeId = document.getElementById("grades" + (index - 1));
    if (currentsubjects > minsubjects) {
        currentsubjects--;
        index--;
        resultSelection.removeChild(subjectId);
        resultSelection.removeChild(gradeId);
    }
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


function DisplayGrade() {
    return fetch("/Grade/SendGrade", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}

function Validation() {
    var emailAddress = document.getElementById('emailAddress').value;
    var nid = document.getElementById('NID').value;
    var phoneNumber = document.getElementById('phoneNumber').value;
    var dateOfBirth = document.getElementById('dateOfBirth').value;
    var subject1 = document.getElementById("subjects1").value;
    var subject2 = document.getElementById("subjects2").value;
    var subject3 = document.getElementById("subjects3").value;

    var validEmail = false;
    var validNID = false;
    var isDuplicate = false;
    var validPhoneNumber = false;
    var emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    var phoneNumberPattern = /^[0-9]+$/;

    if (!emailAddress.match(emailPattern) && emailAddress !== "") {
        document.getElementById("message3").innerHTML = "<b>Please enter a valid email.</b>"
        validEmail = false;
    }
    if (emailAddress.match(emailPattern) && emailAddress !== "") {
        document.getElementById("message3").innerHTML = "<b></b>"
        validEmail = true;
    }
    if (nid.length > 14) {
        validNID = false;
        document.getElementById("message4").innerHTML = "<b>Please enter a valid Identity number.</b>"
    }
    if (nid.length < 14) {
        validNID = true;
        document.getElementById("message4").innerHTML = "<b</b>"
    }
    if (phoneNumber == "") {
        document.getElementById("message5").innerHTML = "<b>Please fill the required field.</b>"
        emptyField = true;
    }
    if (phoneNumber !== "") {
        document.getElementById("message5").innerHTML = "<b></b>"
        emptyField = false;
    }
    if (!phoneNumber.match(phoneNumberPattern) && phoneNumber !== "") {
        document.getElementById("message5").innerHTML = "<b>Please enter a valid phone number.</b>"
        validPhoneNumber = false;
    }
    if (phoneNumber.match(phoneNumberPattern) && phoneNumber !== "") {
        document.getElementById("message5").innerHTML = "<b></b>"
        emptyField = false;
        validPhoneNumber = true;
    }
    if (dateOfBirth == "") {
        document.getElementById("message6").innerHTML = "<b>Please fill the required field.</b>"
        emptyField = true;
    }
    if (dateOfBirth !== "") {
        document.getElementById("message6").innerHTML = "<b></b>"
        emptyField = false;
    }
    if (subject1 == subject2 || subject1 == subject3 || subject2 == subject1 || subject2 == subject3 || subject3 == subject1 || subject3 == subject2) {
        isDuplicate = true;
        document.getElementById("message9").innerHTML = "<b>Cannot choose same subject.</b>"
    }
    else { 
        isDuplicate == false;
        document.getElementById("message9").innerHTML = "<b></b>"
    }
    if (validEmail == true && validPhoneNumber == true && validNID == true && isDuplicate == false) {
        SubmitForm();
    }
}

function buildErrorMessage(ul, errorMessage) {
    var li = document.createElement('Li');
    li.innerHTML = errorMessage;
    ul.appendChild(li);

    return ul;
}

function SubmitForm() {
    var name = document.getElementById('name').value;
    var surname = document.getElementById('surname').value;
    var emailAddress = document.getElementById('emailAddress').value;
    var nid = document.getElementById('NID').value;
    var phoneNumber = document.getElementById('phoneNumber').value;
    var dateOfBirth = document.getElementById('dateOfBirth').value;
    var guardianName = document.getElementById('guardianName').value;
    var address = document.getElementById('address').value;
    var resultArray = new Array();
  
    for (let i = 1; i <= 3; ++i) {
        var subject = document.getElementById('subjects'+i).value;
        var grade = document.getElementById('grades' + i).value;
        resultArray.push({ "SubjectId": subject, "GradeId": grade });

    }
    var studentObj = {
        Name: name, Surname: surname, EmailAddress: emailAddress, NationalIdentityNumber: nid, PhoneNumber: phoneNumber,
        DateOfBirth: dateOfBirth, GuardianName: guardianName, Address: address, Result: resultArray
    }
    sendData(studentObj).then((result) => {
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
            toastr.success("The form has been submitted successfully.");
            window.location = result.url;
        }
    })
    .catch((error) => {
        toastr.error('Unable to make request!!');
    });
}
function sendData(Obj) {
    return fetch("/Student/EnrolmentForm", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Obj),
    })
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}



