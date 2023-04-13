
const uriLogin= '/user/login'
var token

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
        "isAdmin": false
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
            token=data.replace(/"/g, '');
            sessionStorage.setItem('token', token);
            window.location.href = "html/admin.html";
        })
        .catch(error => console.error('Unable to add item.', error));

}
