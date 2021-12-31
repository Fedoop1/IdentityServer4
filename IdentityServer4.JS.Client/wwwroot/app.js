//variables

const resultEl = document.getElementById('result');
const loginEl = document.getElementById('login');
const logoutEl = document.getElementById('logout');
const apiEl = document.getElementById('api');

const config = {
    authority: "https://localhost:5001",
    client_id: "IdentityServer4JSClient",
    redirect_uri: "https://localhost:5003/callback.html",
    response_type: "code",
    scope: "openid profile IdentityServer4Provider",
    post_logout_redirect_uri: "https://localhost:5003/index.html"
};

const mgr = new Oidc.UserManager(config);

// logic

loginEl.addEventListener('click', login, false);
apiEl.addEventListener('click', api, false);
logoutEl.addEventListener('click', logout, false);

mgr.getUser().then((user) => {
    if (user) {
        log("User logged in", user.profile);
    } else {
        log("User not logged in");
    }
});

// functions

function log(message) {
    resultEl.innerText = '';
    resultEl.innerText = message;
}

function login() {
    mgr.signinRedirect();
}

function logout() {
    mgr.signoutRedirect();
}

function api() {
    mgr.getUser().then((user) => {
        const url = "https://localhost:6001/identity";
        const xhr = new XMLHttpRequest();

        xhr.open("GET", url);
        xhr.onload = () => { log(JSON.parse(xhr.responseText)) }

        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}