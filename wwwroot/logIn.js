
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
        .then( response=>response.json())
        .then((data) => {
            token=data.token.replace(/"/g, '');
            if(data.isAdmin)
                window.location.href = "html/admin.html";
            else
               window.location.href = "html/user.html";
            sessionStorage.setItem('token', token);
        })
        .catch(alert("אתה לא רשום במערכת"));

}
