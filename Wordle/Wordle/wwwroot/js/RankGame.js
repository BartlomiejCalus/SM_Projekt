const letterInputs = document.getElementsByClassName("letter-input");
const attempts = document.getElementById("attempts");
const alreadyUsedWords = new Set();

Array.from(letterInputs).forEach((input, index) => {
    input.addEventListener("input", (event) => {
        // Automatyczne przechodzenie do następnego pola wprowadzania
        if (input.value.length === 1 && index < letterInputs.length - 1) {
            letterInputs[index + 1].focus();
        }
    });

    input.addEventListener("keydown", (event) => {
        if (event.key === "Backspace" && index > 0) {
            // Automatyczne przechodzenie do poprzedniego pola wprowadzania przy usuwaniu tekstu
            if (input.value.length === 0) {
                letterInputs[index - 1].focus();
            }
        }
    });

    input.addEventListener("keyup", (event) => {
        if (event.key === "Enter") {
            const word = Array.from(letterInputs)
                .map((input) => input.value)
                .join("");
            if (word.length === 5) {
                if (alreadyUsedWords.has(word)) {
                    alert("To słowo zostało już wpisane!");
                } else {
                    alreadyUsedWords.add(word);
                    // Tu powinien być wywołanie funkcji, która będzie komunikować się z serwerem.
                    // Na razie, dla demonstracji, zakładam przykładową macierz bool.
                    const serverResponse = [
                        [true, true],
                        [true, false],
                        [false, false],
                        [true, true],
                        [true, false]
                    ];
                    handleAttempt(word, serverResponse);
                }
            }
            Array.from(letterInputs).forEach((input) => (input.value = ""));
            letterInputs[0].focus();
        }
    });
});


function handleAttempt(word, serverResponse) {
    const attemptDiv = document.createElement("div");
    attemptDiv.className = "attempt";

    for (let i = 0; i < word.length; i++) {
        const letter = word[i];
        const letterDiv = document.createElement("div");
        letterDiv.className = "letter";
        letterDiv.textContent = letter;

        if (serverResponse[i][0] && serverResponse[i][1]) {
            letterDiv.classList.add("correct");
        } else if (serverResponse[i][0]) {
            letterDiv.classList.add("partial");
        }

        attemptDiv.appendChild(letterDiv);
    }

    // Dodajemy próbę na początek, aby ostatnio wpisane słowo było na górze
    attempts.insertBefore(attemptDiv, attempts.firstChild);
}

window.addEventListener("DOMContentLoaded", (event) => {
    letterInputs[0].focus();
});

