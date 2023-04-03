
// Selektory elementów HTML
const loginForm = document.querySelector('#login-form');
const registerForm = document.querySelector('#register-form');

// Funkcja do logowania
function login(username, password) {
    // kod do przeprowadzenia autoryzacji
    if (empty(username)) {
        alert("Pole loginu jest puste");
    } else if (empty(username))
    alert("Login: " + username + "; Password: " + password);
    // Przekierowanie na inną stronę
    window.location.replace("/Home/Index");


}

// Funkcja do rejestracji

function register(username, email, password) {
    if (username.length < 4) {
        alert('Nazwa użytkownika musi mieć co najmniej 4 znaki');
        return;
    }
    else if (password.length < 8) {
        alert('Hasło musi mieć co najmniej 8 znaków');
        return;
    }
    else {
        alert('Login: ' + username + '; Password: ' + password);
        window.location.replace("/Home/Index");
    }
}



// Obsługa zdarzenia kliknięcia przycisku logowania
if (loginForm !== null) {
    loginForm.addEventListener('submit', (event) => {

        event.preventDefault();
        const username = loginForm.querySelector('#username').value;
        const password = loginForm.querySelector('#password').value;
        login(username, password);
        
    });
};

// Obsługa formularza rejestracji


if (registerForm !== null) {
    registerForm.addEventListener('submit', (event) => {

        event.preventDefault();
        const username = registerForm.querySelector('#username').value;
        const email = registerForm.querySelector('#email').value;
        const password = registerForm.querySelector('#password').value;
        const confirmPassword = registerForm.querySelector('#confirm-password').value;
        if (password === confirmPassword) {
            register(username, email, password);
        } else {
            alert('Hasła nie są identyczne');
        }
    });
};
