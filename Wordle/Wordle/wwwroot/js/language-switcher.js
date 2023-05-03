let translations;

function loadTranslations() {
    fetch('../lang/en.json')
        .then(response => response.json())
        .then(data => {
            translations = data;
            applyLanguage(getStoredLanguage());
        });
}

function getStoredLanguage() {
    return localStorage.getItem('language') || 'en';
}

function storeLanguage(lang) {
    localStorage.setItem('language', lang);
}

function applyLanguage(lang) {
    document.querySelectorAll('[data-translate]').forEach(element => {
        const key = element.getAttribute('data-translate');
        element.textContent = translations[lang][key];
    });
}


loadTranslations();