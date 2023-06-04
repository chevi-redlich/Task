const urlUser= '/user'
let users=[]
let token=sessionStorage.getItem('token');
function getUsers() {
    fetch(urlUser,{method:'GET',headers:{'Authorization': `Bearer ${token}`}})
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

        let td2 = tr.insertCell(0);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(1);
        let textPassword = document.createTextNode(item.password);
        td3.appendChild(textPassword);

        let td1 = tr.insertCell(2);
        td1.appendChild(isAdminCheckbox);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    users = data;
}
function deleteItem(id) {
    
    fetch(`${urlUser}/${id}`, {
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
        name: name.value,
        password:password.value,
        isAdmin:isAdmin.checked,
    };
        fetch(urlUser, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(user)
        })
        .then(() => {
            getUsers();
        })
        .catch(error => console.error('Unable to add item.', error));
    name.value="";
    password.value="";
    isAdmin.checked=false;
}
