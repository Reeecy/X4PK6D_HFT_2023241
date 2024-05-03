let passes = [];
let connection = null;

let passIdToUpdate = -1;

getPasses();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:20677/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    // Define SignalR event handlers for pass-related events
    connection.on("PassCreated", (user, message) => {
        getPasses();
    });

    connection.on("PassDeleted", (user, message) => {
        getPasses();
    });

    connection.on("PassUpdated", (user, message) => {
        getPasses();
    });

    // Reconnect SignalR in case of connection closure
    connection.onclose(async () => {
        await start();
    });

    // Start SignalR connection
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

async function getPasses() {
    await fetch('http://localhost:20677/pass')
        .then(response => response.json())
        .then(data => {
            passes = data;
            displayPasses();
        });
}

function displayPasses() {
    // Clear the existing pass table
    document.getElementById('resultarea').innerHTML = "";

    // Iterate through passes and populate the table
    passes.forEach(pass => {
        let usageOptions = getUsageOptions(pass);
        let actionsHtml = `
            <button type="button" onclick="removePass(${pass.id})">Remove</button>
            <button type="button" onclick="showUpdateForm(${pass.id})">Update</button>`;

        // Append pass information to table
        document.getElementById('resultarea').innerHTML += `
            <tr>
                <td>${pass.id}</td>
                <td>${pass.passType}</td>
                <td>${pass.startDate}</td>
                <td>${pass.endDate}</td>
                <td>${pass.price}</td>
                <td>${usageOptions}</td>
                <td>${actionsHtml}</td>
            </tr>`;
    });
}

function getUsageOptions(pass) {
    let options = [];
    if (pass.crossfitGymUsage) options.push("Crossfit Gym");
    if (pass.groupTrainingUsage) options.push("Group Training");
    if (pass.poolUsage) options.push("Pool");
    if (pass.saunaUsage) options.push("Sauna");
    if (pass.massageUsage) options.push("Massage");

    return options.join(", ");
}

function createPass() {
    // Get values from the add pass form
    let passType = document.getElementById('passType').value;
    let startDate = document.getElementById('startDate').value;
    let endDate = document.getElementById('endDate').value;
    let price = document.getElementById('price').value;
    let crossfitGymUsage = document.getElementById('crossfitGymUsage').checked;
    let groupTrainingUsage = document.getElementById('groupTrainingUsage').checked;
    let poolUsage = document.getElementById('poolUsage').checked;
    let saunaUsage = document.getElementById('saunaUsage').checked;
    let massageUsage = document.getElementById('massageUsage').checked;

    // Make POST request to create a new pass
    fetch('http://localhost:20677/pass', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            passType: passType,
            startDate: startDate,
            endDate: endDate,
            price: price,
            crossfitGymUsage: crossfitGymUsage,
            groupTrainingUsage: groupTrainingUsage,
            poolUsage: poolUsage,
            saunaUsage: saunaUsage,
            massageUsage: massageUsage
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('Pass created:', data);
            alert("Pass created successfully");
            getPasses();
        })
        .catch(error => {
            console.error('Error:', error);
            alert("Error occurred while creating pass");
        });
}

function removePass(passId) {
    // Make DELETE request to remove the pass
    fetch(`http://localhost:20677/pass/${passId}`, {
        method: 'DELETE',
    })
        .then(response => response)
        .then(data => {
            console.log('Pass deleted:', data);
            alert("Pass deleted successfully");
            getPasses();
        })
        .catch(error => {
            console.error('Error:', error);
            alert("Error occurred while deleting pass");
        });
}

function showUpdateForm(passId) {
    // Find the pass by its ID
    let passToUpdate = passes.find(pass => pass.id === passId);
    var startDate = new Date(passToUpdate['startDate']);
    // Get the year, month, and day from the date object
    var year = startDate.getFullYear();
    var month = (startDate.getMonth() + 1).toString().padStart(2, '0'); // Add 1 to month because JavaScript months are zero-based
    var day = startDate.getDate().toString().padStart(2, '0');
    let _startDate = year + '-' + month + '-' + day;

    var endDate = new Date(passToUpdate['endDate']);
    var year = endDate.getFullYear();
    var month = (endDate.getMonth() + 1).toString().padStart(2, '0'); // Add 1 to month because JavaScript months are zero-based
    var day = endDate.getDate().toString().padStart(2, '0');
    let _endDate = year + '-' + month + '-' + day;

    // Populate the update form with pass information
    document.getElementById('passTypeToUpdate').value = passToUpdate.passType;
    document.getElementById('startDateToUpdate').value = _startDate;
    document.getElementById('endDateToUpdate').value = _endDate;
    document.getElementById('priceToUpdate').value = passToUpdate.price;
    document.getElementById('crossfitGymUsageToUpdate').checked = passToUpdate.crossfitGymUsage;
    document.getElementById('groupTrainingUsageToUpdate').checked = passToUpdate.groupTrainingUsage;
    document.getElementById('poolUsageToUpdate').checked = passToUpdate.poolUsage;
    document.getElementById('saunaUsageToUpdate').checked = passToUpdate.saunaUsage;
    document.getElementById('massageUsageToUpdate').checked = passToUpdate.massageUsage;

    // Display the update form
    document.getElementById('updateformdiv').style.display = 'flex';

    passIdToUpdate = passId;
}

function updatePass() {
    // Get values from the update pass form
    let passType = document.getElementById('passTypeToUpdate').value;
    let startDate = document.getElementById('startDateToUpdate').value;
    let endDate = document.getElementById('endDateToUpdate').value;
    let price = document.getElementById('priceToUpdate').value;
    let crossfitGymUsage = document.getElementById('crossfitGymUsageToUpdate').checked;
    let groupTrainingUsage = document.getElementById('groupTrainingUsageToUpdate').checked;
    let poolUsage = document.getElementById('poolUsageToUpdate').checked;
    let saunaUsage = document.getElementById('saunaUsageToUpdate').checked;
    let massageUsage = document.getElementById('massageUsageToUpdate').checked;

    // Make PUT request to update the pass
    fetch('http://localhost:20677/pass', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            id: passIdToUpdate,
            passType: passType,
            startDate: startDate,
            endDate: endDate,
            price: price,
            crossfitGymUsage: crossfitGymUsage,
            groupTrainingUsage: groupTrainingUsage,
            poolUsage: poolUsage,
            saunaUsage: saunaUsage,
            massageUsage: massageUsage
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('Pass updated:', data);
            alert("Pass updated successfully");
            getPasses();
        })
        .catch(error => {
            console.error('Error:', error);
            alert("Error occurred while updating pass");
        });

    // Hide the update form after updating the pass
    document.getElementById('updateformdiv').style.display = 'none';
}