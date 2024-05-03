fetch('http://localhost:20677/person')
    .then(x => x.json())
    .then(y => console.log(y));