window.onload = function () {
    DisplayStudentInfo().then((response) => {
        console.log(response);
        var table = document.getElementById('studentInfoTable');
        for (var i = 0; i < response.length; i++) {
            var tr = document.createElement('tr');
            var data = response[i];

            var td = document.createElement('td');
            td.innerHTML = data.StudentId;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.FirstName;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.Surname;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.Address;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.PhoneNumber;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.EmailAddress;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.CustomDate;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.GuardianName;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.NationalIdentityNumber;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.UserId;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.SubjectTaken;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.TotalMark;
            tr.appendChild(td);

            var td = document.createElement('td');
            td.innerHTML = data.StatusName;
            tr.appendChild(td);

            table.appendChild(tr);
        }
    })
    .catch((error) => {
        toastr.error('Unable to make request!!');
    });
};


function DisplayStudentInfo() {
    return fetch("/Admin/GetStudentInfo", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}

function TableToCsv() {
    var csv_data = [];
    var seperator = ',';
    var rows = document.getElementsByTagName('tr');
    for (var i = 0; i < rows.length; i++) {
        var cols = rows[i].querySelectorAll('th, td');
        var csvRow = [];
        for (var j = 0; j < cols.length; j++) {
            var info = cols[j].innerHTML.replace(/(\r\n|\n|\r)/gm, '').replace(/(\s\s)/gm, ' ');
            info = info.replace(/"/g, '""');
            csvRow.push('"' + info + '"');
        }
        csv_data.push(csvRow.join(","));
    }
    csv_data = csv_data.join('\n');
    console.log(csv_data.length);
    DownloadCSVFile(csv_data);
}


function DownloadCSVFile(csv_data) {
    CSVFile = new Blob([csv_data], { type: "text/csv" });
    var temp_link = document.createElement('a');
    temp_link.download = "GfG.csv";
    var url = window.URL.createObjectURL(CSVFile);
    temp_link.href = url;
    temp_link.style.display = "none";
    document.body.appendChild(temp_link);
    temp_link.click();
    document.body.removeChild(temp_link);
}