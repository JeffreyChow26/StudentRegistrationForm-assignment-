window.onload = function () {
    GetStudentSummary().then((response) => {
        console.log(response.StudentId);
        var table = document.getElementById('studentInfoTable');

        var tr = document.createElement('tr');

        var td = document.createElement('td');
        td.innerHTML = response.StudentId;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.FirstName;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.Surname;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.Address;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.PhoneNumber;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.EmailAddress;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.CustomDate;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.GuardianName;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.NationalIdentityNumber;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.UserId;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.SubjectTaken;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.TotalMark;
        tr.appendChild(td);

        var td = document.createElement('td');
        td.innerHTML = response.StatusName;
        tr.appendChild(td);

        table.appendChild(tr);
        
    })
}

function GetStudentSummary() {
    return fetch("/StudentSummary/GetStudentSummary", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}