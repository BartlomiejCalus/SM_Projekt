﻿
// Pobierz elementy HTML do manipulacji
const startButton = document.getElementById("start_button");
const gameContainer = document.querySelector(".game-container");
const gameBody = document.querySelector(".game-body");
const timer = document.getElementById("timer");
const keyboard = document.getElementsByClassName("keyboard-container")[0];
const ready = document.getElementById("ready");
const countdown = document.getElementById("countdown");

// Ukryj element z timerem na początku
timer.style.display = "none";
countdown.style.visibility = "hidden";

// Funkcja, która animuje i zmniejsza wartość odliczania
function animateCountdown() {
    countdown.classList.remove("countdown-animation");
    void countdown.offsetWidth; // Przerwa między animacjami
    countdown.classList.add("countdown-animation");
}

let timerInterval; // Przenieś tutaj deklarację zmiennej timerInterval

function stopTimer() {
    clearInterval(timerInterval);
}

startButton.addEventListener("click", function () {
    startButton.style.visibility = "hidden";
    ready.style.visibility = "hidden";
    countdown.style.visibility = "visible";

    // Rozpocznij odliczanie
    let counter = 3;
    countdown.innerText = counter.toString();
    animateCountdown();

    const countdownInterval = setInterval(() => {
        counter--;

        if (counter === 0) {
            clearInterval(countdownInterval);
            countdown.style.display = "none";

            // Pokaż planszę gry i timer
            gameBody.style.display = "block";
            gameContainer.style.paddingTop = "0";
            timer.style.display = "flex";
            keyboard.style.display = "flex";
            startButton.style.display = "none";
            ready.style.display = "none";

            // Rozpocznij odliczanie czasu
            let startTime = Date.now();
            timerInterval = setInterval(function () {
                let elapsedTime = Date.now() - startTime;
                let minutes = Math.floor(elapsedTime / 60000);
                let seconds = Math.floor((elapsedTime % 60000) / 1000);
                timer.innerText = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
            }, 1000);



        } else {
            countdown.innerText = counter.toString();
            animateCountdown();
        }
    }, 1000);
});


//Wyświetlanie do debugowania
//alert("@ranked.getWordLength()");
//alert("@ranked.wordInfo.word");
let zmienna = ("@ranked.wordInfo.word");
let zmienna2 = zmienna.toLocaleUpperCase();


//---------------------------------------------
const letterInputs = document.getElementsByClassName("letter-input");
const keyboardKeys = document.getElementsByClassName("keyboard-key");
const attempts = document.getElementById("attempts");
const submitButton = document.getElementById("submit_button");
const alreadyUsedWords = new Set();
let currentAttemptIndex = 0;
const backspaceButton = document.getElementById("backspace_button");



let gameOver = false;

backspaceButton.addEventListener("click", function () {
    // Sprawdź, czy gra się zakończyła
    if (!gameOver) {
        const currentInputIndex = currentAttemptIndex * 5;
        const currentLetterInputs = Array.from(letterInputs).slice(currentInputIndex, currentInputIndex + 5);
        const nonEmptyInputs = currentLetterInputs.filter((input) => input.value !== "");

        if (nonEmptyInputs.length > 0) {
            const lastNonEmptyInput = nonEmptyInputs[nonEmptyInputs.length - 1];
            lastNonEmptyInput.value = "";
            lastNonEmptyInput.focus();
        }
    }
});

function displayGameOverMessage() {
    const gameOverMessage = document.createElement("div");
    gameOverMessage.className = "game-over-message";
    //gameOverMessage.innerHTML = "<h2>Koniec gry!</h2><br> <h3>Otrzymujesz X punktów!</h3>";

    gameOverMessage.innerHTML = `<div class="game-over-message-content"><h2>Koniec gry!</h2><h3>Zgadywane słowo to: ` + zmienna2 +`</h3><br><br>
        <h3>Otrzymujesz <span class="highlight">20</span> punktów!</h3>
        <p>Aktualna liczba punktów: <span class="highlight">100</span></p>
        <h4>Top 3 graczy:</h4>
        <ol>
            <li><i class="fa-solid fa-trophy gold"></i> <span class="highlight">Gracz 1</span>: 150 punktów</li>
            <li><i class="fa-solid fa-trophy silver"></i> <span class="highlight">Gracz 2</span>: 130 punktów</li>
            <li><i class="fa-solid fa-trophy bronze"></i> <span class="highlight">Gracz 3</span>: 110 punktów</li>
        </ol>
<br>

<a href="RankGame"> <button class="afterwin" type="submit"><i class="fa-solid fa-rotate-right"></i> Zagraj ponownie</button></a>
<a href="Ranking"> <button class="afterwin" type="submit"><i class="fa-solid fa-ranking-star"></i> Ranking</button></a>
    </div>`;



    const gameContainer = document.querySelector(".game-container");
    gameContainer.appendChild(gameOverMessage);
    const attempts = document.getElementById("attempts");
    attempts.style.display = "none";
    const keyboard_container = document.getElementsByClassName("keyboard-container")[0];
    keyboard_container.style.display = "none";

}


Array.from(letterInputs).forEach((input, index) => {
    input.addEventListener("input", (event) => {
        input.value = input.value.toUpperCase();
        if (input.value.length === 1 && index < letterInputs.length - 1) {
            if (index % 5 !== 4) {
                letterInputs[index + 1].focus();
            }
        }
        if (input.value.length > 1) {
            input.value = input.value.charAt(0);
        }
    });

    input.addEventListener("keydown", (event) => {
        if (event.key === "Backspace" && index > 0) {
            if (input.value.length === 0) {
                letterInputs[index - 1].focus();
            }
        } else if (event.key !== "Backspace" && event.key !== "Enter" && event.key.length === 1 && index % 5 === 4 && input.value.length === 1) {
            event.preventDefault();
            input.blur();
        }
    });

    input.addEventListener("keyup", (event) => {
        if (!gameOver && event.key === "Enter") {
            handleSubmit();
        }
    });


});

submitButton.addEventListener("click", function () {
    if (!gameOver) {
        handleSubmit();
    }
});

Array.from(keyboardKeys).forEach((key) => {
    key.addEventListener("click", function () {
        const currentInputIndex = currentAttemptIndex * 5;
        const currentLetterInputs = Array.from(letterInputs).slice(currentInputIndex, currentInputIndex + 5);
        const currentInput = currentLetterInputs.find((input) => input.value === "");

        if (currentInput) {
            currentInput.value = key.textContent;
            if (currentInput.nextElementSibling) {
                currentInput.nextElementSibling.focus();
            }
        }
    });
});

function flashRed() {
    Array.from(letterInputs).forEach((input) => {
        input.style.backgroundColor = "red";
    });

    setTimeout(() => {
        Array.from(letterInputs).forEach((input) => {
            input.style.backgroundColor = "";
            input.style.transition = "0.5s";
        });
    }, 500);
}



function handleSubmit() {
    const word = Array.from(letterInputs)
        .slice(currentAttemptIndex * 5, currentAttemptIndex * 5 + 5)
        .map((input) => input.value)
        .join("");
    if (word.length < 5) {
        flashRed();
    } else if (word.length === 5) {
        if (alreadyUsedWords.has(word)) {
            alert("To słowo zostało już wpisane!");
        } else {

            alreadyUsedWords.add(word);


            //-----------
            $(document).ready(function () {
                var generatedWord = word.toLowerCase(); // Tutaj wprowadź swoją zmienną JavaScript

                $.ajax({
                    type: "POST",
                    url: "/Home/Play", // Zaktualizuj URL zgodnie z właściwym kontrolerem i akcją
                    data: { generatedWord: generatedWord },
                    success: function (response) {
                        handleAttempt(word, response);
                    },
                    error: function (error) {
                        // Tutaj obsłuż błędy, np. wyświetlając komunikat o błędzie
                        alert("Wystąpił błąd:");
                    }
                });
            });
        }
    }
}

function handleAttempt(word, serverResponse) {
    //alert("ODPOWIEDZ HANDLE: "+serverResponse);
    //console.error("Nieprawidłowy format danych wejściowych:", serverResponse);
    for (let i = 0; i < word.length; i++) {
        const letterInput = letterInputs[currentAttemptIndex * 5 + i];

        if (serverResponse[0][i] && serverResponse[1][i]) {
            letterInput.classList.add("correct");
        } else if (serverResponse[0][i]) {
            letterInput.classList.add("partial");
        }
    }

    let count = 0;
    for (i = 0; i < 5; i++) {
        if (serverResponse[0][i] && serverResponse[1][i]) {
            count++;
        }
    }
    if (count == 5) {
        // Zablokuj pola tekstowe w ostatnim rzędzie po zatwierdzeniu
        Array.from(letterInputs)
            .slice(currentAttemptIndex + 1, currentAttemptIndex + 5)
            .forEach((input) => (input.disabled = true));
        gameOver = true;
        stopTimer(); // Zatrzymaj timer

        //Gdy użytkownik przegra
        //alert(currentAttemptIndex);
    }


    if (currentAttemptIndex < 4 && !gameOver) {
        currentAttemptIndex++;
        Array.from(letterInputs)
            .slice(0, currentAttemptIndex * 5)
            .forEach((input) => (input.disabled = true));
        Array.from(letterInputs)
            .slice(currentAttemptIndex * 5, currentAttemptIndex * 5 + 5)
            .forEach((input) => (input.disabled = false));
        letterInputs[currentAttemptIndex * 5].focus();
    } else {
        // Zablokuj pola tekstowe w ostatnim rzędzie po zatwierdzeniu
        Array.from(letterInputs)
            .slice(currentAttemptIndex * 5, currentAttemptIndex * 5 + 5)
            .forEach((input) => (input.disabled = true));
        gameOver = true;
        displayGameOverMessage();
        stopTimer(); // Zatrzymaj timer

        //Gdy użytkownik wygra
        //alert(currentAttemptIndex+1);
        
    }
}

window.addEventListener("DOMContentLoaded", (event) => {
    letterInputs[0].focus();
    Array.from(letterInputs)
        .slice(5)
        .forEach((input) => (input.disabled = true));
});
