const uriUser= '/user'
const uriLogin= '/user/login'
let users=[]
let token=""

function formSabmit() {
    const userName=document.getElementById('name')
    const password=document.getElementById('password')
    login(userName.value, password.value)
}

function login(userName, password) {
    const user={
        "id": 0,
        "name": userName,
        "password": password,
        "isAdmin": true
    };
        fetch(uriLogin,  {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(user)
        })
        .then( response=>response.text())
        .then((data) => {
            token="Bearer "+data;
            localStorage.setItem('token', token)
            window.location.href = "html/user.html";
            alert(token)
        })
        .catch(error => console.error('Unable to add item.', error));

}
function getUsers() {
    fetch(uriUser,{method:'GET',headers:{Authorization:token}})
        .then(response => response.json())
        .then(data => displayUsers(data))
        .catch(error => console.error('Unable to get items.', error));
}
function displayUsers(data) {
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isDoenCheckbox = document.createElement('input');
        isDoenCheckbox.type = 'checkbox';
        isDoenCheckbox.disabled = true;
        isDoenCheckbox.checked = item.done;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isDoenCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;
}
