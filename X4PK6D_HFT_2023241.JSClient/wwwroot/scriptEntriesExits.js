let entriesExits = [];
let connection = null;
let entriesExitsToUpdate = -1;

getEntriesExits();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:20677/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("EntriesExitsCreated", (user, message) => {
        getEntriesExits();
    });

    connection.on("EntriesExitsDeleted", (user, message) => {
        getEntriesExits();
    });

    connection.on("EntriesExitsUpdated", (user, message) => {
        getEntriesExits();
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
}

async function getEntriesExits() {
    await fetch('http://localhost:20677/entriesexits')
        .then(response => response.json())
        .then(data => {
            entriesExits = data;
            displayEntriesExits();
        });
}

function displayEntriesExits() {
    let tableBody = document.getElementById('resultarea');
    tableBody.innerHTML = "";

    entriesExits.forEach(entryExit => {
        let row = document.createElement('tr');
        row.innerHTML = `
            <td>${entryExit.id}</td>
            <td>${entryExit.entryTime}</td>
            <td>${entryExit.exitTime}</td>
            <td>${entryExit.personId}</td>
            <td>
                <button onclick="removeEntryExit(${entryExit.id})">Remove</button>
                <button onclick="showUpdateForm(${entryExit.id})">Update</button>
            </td>`;
        tableBody.appendChild(row);
    });
}

function createEntryExit() {
    // Get values from the form fields
    let entryTime = document.getElementById('entryTime').value;
    let exitTime = document.getElementById('exitTime').value;
    let personId = document.getElementById('personId').value;

    // Make a POST request to create a new entry/exit
    fetch('http://localhost:20677/entriesexits', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            entryTime: entryTime,
            exitTime: exitTime,
            personId: personId
        }),
    })
        .then(response => response.json())
        .then(data => {
            console.log('Entry/Exit created:', data);
            alert("Entry/Exit created successfully");
            getEntriesExits(); // Refresh the entries/exits data after creating a new one
        })
        .catch(error => {
            console.error('Error:', error);
            alert("Error occurred while creating Entry/Exit");
        });
}

function removeEntryExit(id) {
    fetch(`http://localhost:20677/entriesexits/${id}`, {
        method: 'DELETE',
    })
        .then(response => response)
        .then(data => {
            console.log('EntriesExits deleted:', data);
            alert("EntriesExits deleted successfully");
            getEntriesExits();
        })
        .catch(error => {
            console.error('Error:', error);
            alert("Error occurred while deleting EntriesExits");
        });
}

function showUpdateForm(id) {
    // Find the entryExit by its ID
    let entryExitToUpdate = entriesExits.find(t => t['id'] == id)

    var entry = new Date(entryExitToUpdate['entryTime']);
    var year = entry.getFullYear();
    var month = (entry.getMonth() + 1).toString().padStart(2, '0'); // Add 1 to month because JavaScript months are zero-based
    var day = entry.getDate().toString().padStart(2, '0');
    let _entry = year + '-' + month + '-' + day;

    var exit = new Date(entryExitToUpdate['exitTime']);
    var year = exit.getFullYear();
    var month = (exit.getMonth() + 1).toString().padStart(2, '0'); // Add 1 to month because JavaScript months are zero-based
    var day = exit.getDate().toString().padStart(2, '0');
    let _exit = year + '-' + month + '-' + day;

    // Populate the update form with entryExit information
    document.getElementById('entryTimeToUpdate').value = _entry;
    document.getElementById('exitTimeToUpdate').value = _exit;
    document.getElementById('personIdToUpdate').value = entryExitToUpdate.personId;

    // Display the update form
    document.getElementById('updateformdiv').style.display = 'block';
    entriesExitsToUpdate = id;
}

function updateEntryExit() {
    // Get values from the update entryExit form
    let entryTime = document.getElementById('entryTimeToUpdate').value;
    let exitTime = document.getElementById('exitTimeToUpdate').value;
    let personId = document.getElementById('personIdToUpdate').value;

    // Make PUT request to update the entryExit
    fetch("http://localhost:20677/entriesexits/", {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            id: entriesExitsToUpdate,
            entryTime: entryTime,
            exitTime: exitTime,
            personId: personId
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('EntriesExits updated:', data);
            alert("EntriesExits updated successfully");
            getEntriesExits();
        })
        .catch(error => {
            console.error('Error:', error);
            alert("Error occurred while updating EntriesExits");
        });

    // Hide the update form after updating the entryExit
    document.getElementById('updateformdiv').style.display = 'none';
}