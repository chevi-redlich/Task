const uriUser= '/user'
let users=[]
let token=sessionStorage.getItem('token');
function getUsers() {
    fetch(uriUser,{method:'GET',headers:{'Authorization': `Bearer ${token}`}})
        .then(response => response.json())
        .then(data => displayUsers(data))
        .catch(error => console.error('Unable to get items.', error));
}
function displayUsers(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(item => {
        let isAdminCheckbox = document.createElement('input');
        isAdminCheckbox.type = 'checkbox';
        isAdminCheckbox.disabled = true;
        isAdminCheckbox.checked = item.isAdmin;

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isAdminCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        let textPassword = document.createTextNode(item.password);
        td3.appendChild(textPassword);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    users = data;
}
function deleteItem(id) {
    
    fetch(`${uriUser}/${id}`, {
            method: 'DELETE',
            headers:{'Authorization': `Bearer ${token}`}
        })
        .then(() => getUsers())
        .catch(error => console.error('Unable to delete item.', error));
}

function AddUser() {
    const name=document.getElementById('add-name');
    const password=document.getElementById('password');
    const isAdmin=document.getElementById('is-admin');
    const user = {
        name: name,
        password:password,
        isAdmin:isAdmin
    };
        fetch(uriUser, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(user)
        })
        .then(response => response.json())
        .then(() => {
            getUsers();
        })
        .catch(error => console.error('Unable to add item.', error));

}
function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const item = {
        isDone: false,
        name: addNameTextbox.value.trim()
    };

    fetch(urlTask, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}



