let persons = [];
let connection = null;

let personIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:20677/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PersonCreated", (user, message) => {
        getdata();
    });

    connection.on("PersonDeleted", (user, message) => {
        getdata();
    });

    connection.on("PersonUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


async function getdata() {
    await fetch('http://localhost:20677/person')
        .then(x => x.json())
        .then(y => {
            persons = y;
            //console.log(persons);
            display();
        });
}




function display() {
    document.getElementById('resultarea').innerHTML = "";
    persons.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.fullName + "</td><td>" + t.dateOfBirth + "</td><td>"
            + t.address + "</td><td>" + t.phoneNumber + "</td><td>" + t.email + "</td><td>" + t.isRetired
            + "</td><td>" + t.isStudent + "</td><td>" + `
            <button type="button" onclick="remove(${t.id})">Remove</button>` + `
            <button type="button" onclick="showupdate(${t.id})">Update</button >` + "</td></tr>";
        console.log(t.fullName);
    });
}

function create() {
    let name = document.getElementById('personname').value;
    let dateOfBirth = document.getElementById('dateofbirth').value;
    const birthdate = new Date(dateOfBirth);
    let address = document.getElementById('address').value;
    let phoneNumber = document.getElementById('phonenumber').value;
    let email = document.getElementById('email').value;
    let isRetired = document.getElementById('isretired').checked;
    let isStudent = document.getElementById('isstudent').checked;


    fetch('http://localhost:20677/person', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                fullName: name,
                dateOfBirth: birthdate,
                address: address,
                phoneNumber: phoneNumber,
                email: email,
                isStudent: isStudent,
                isRetired: isRetired
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            alert("Person created successfully");
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:20677/person/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            alert('Person deleted successfully.');
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); })
}

function showupdate(id) {
    personToUpdate = persons.find(t => t['id'] == id);
    var dateOfBirth = new Date(personToUpdate['dateOfBirth']);
    // Get the year, month, and day from the date object
    var year = dateOfBirth.getFullYear();
    var month = (dateOfBirth.getMonth() + 1).toString().padStart(2, '0'); // Add 1 to month because JavaScript months are zero-based
    var day = dateOfBirth.getDate().toString().padStart(2, '0');

    document.getElementById('personnametoupdate').value = personToUpdate['fullName'];
    document.getElementById('dateofbirthtoupdate').value = year + '-' + month + '-' + day;
    document.getElementById('addresstoupdate').value = personToUpdate['address'];
    document.getElementById('phonenumbertoupdate').value = personToUpdate['phoneNumber'];
    document.getElementById('emailtoupdate').value = personToUpdate['email'];
    document.getElementById('isretiredtoupdate').checked = personToUpdate['isRetired'];
    document.getElementById('isstudenttoupdate').checked = personToUpdate['isStudent'];

    document.getElementById('updateformdiv').style.display = 'flex';
    document.getElementById('updateformdiv').style.flexDirection = 'row';
    document.getElementById('updateformdiv').style.flexWrap = 'wrap';
    personIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';

    let name = document.getElementById('personnametoupdate').value;
    let dateOfBirth = document.getElementById('dateofbirthtoupdate').value;
    const birthdate = new Date(dateOfBirth);
    let address = document.getElementById('addresstoupdate').value;
    let phoneNumber = document.getElementById('phonenumbertoupdate').value;
    let email = document.getElementById('emailtoupdate').value;
    let isRetired = document.getElementById('isretiredtoupdate').checked;
    let isStudent = document.getElementById('isstudenttoupdate').checked;


    fetch('http://localhost:20677/person', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                id: personIdToUpdate,
                fullName: name,
                dateOfBirth: birthdate,
                address: address,
                phoneNumber: phoneNumber,
                email: email,
                isStudent: isStudent,
                isRetired: isRetired
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            alert("Person updated successfully");
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}