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
    gameOverMessage.textContent = "Koniec gry!";
    const gameContainer = document.querySelector(".game-container");
    gameContainer.appendChild(gameOverMessage);
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
            const serverResponse = [
                [true, true],
                [true, false],
                [false, false],
                [true, true],
                [true, false],
            ];
            handleAttempt(word, serverResponse);
        }
    }
}

function handleAttempt(word, serverResponse) {
    for (let i = 0; i < word.length; i++) {
        const letterInput = letterInputs[currentAttemptIndex * 5 + i];

        if (serverResponse[i][0] && serverResponse[i][1]) {
            letterInput.classList.add("correct");
        } else if (serverResponse[i][0]) {
            letterInput.classList.add("partial");
        }
    }

    if (currentAttemptIndex < 4) {
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
    }
}





window.addEventListener("DOMContentLoaded", (event) => {
    letterInputs[0].focus();
    Array.from(letterInputs)
        .slice(5)
        .forEach((input) => (input.disabled = true));
});
