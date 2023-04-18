// Funkcja asynchroniczna, która inicjuje efekt pisania na ekranie.
async function init() {
    const node = document.querySelector("#type-text"); // Pobiera element HTML, który będzie zawierał tekst.

    await sleep(1000); // Czeka 1 sekundę przed rozpoczęciem pisania.

    node.innerText = ""; // Usuwa wcześniej wpisany tekst.

    // Pętla, która pisze i usuwa tekst w nieskończoność.
    while (true) {
        await node.type("Wordle"); // Pisze "Wordle".
        await sleep(2000); // Czeka 2 sekundy.
        await node.delete("Wordle"); // Usuwa "Wordle".
        await node.type("Słówka"); // Pisze "Słówka".
        await sleep(2000); // Czeka 2 sekundy.
        await node.delete("Słówka"); // Usuwa "Słówka".
        await node.type("W0rdl3"); // Pisze "Wordle".
        await sleep(2000); // Czeka 2 sekundy.
        await node.delete("W0rdl3"); // Usuwa "Wordle".
    }
}

// Funkcja asynchroniczna
const sleep = (time) => new Promise((resolve) => setTimeout(resolve, time));

// Klasa dziedzicząca po HTMLSpanElement, która reprezentuje element HTML z efektem pisania.
class TypeAsync extends HTMLSpanElement {
    // getter, który zwraca losowy interwał czasu między pisaniem kolejnych znaków.
    get typeInterval() {
        const randomMs = 100 * Math.random();
        return randomMs < 50 ? 10 : randomMs;
    }

    // Metoda asynchroniczna, która pisze dany tekst na ekranie.
    async type(text) {
        for (let character of text) {
            this.innerText += character; // Dodaje pojedynczy znak do tekstu.
            await sleep(this.typeInterval); // Czeka przez losowy czas.
        }
    }

    // Metoda asynchroniczna, która usuwa ostatni znak z tekstu.
    async delete(text) {
        for (let character of text) {
            this.innerText = this.innerText.slice(0, this.innerText.length - 1); // Usuwa ostatni znak z tekstu.
            await sleep(this.typeInterval); // Czeka przez losowy czas.
        }
    }
}

// Rejestruje klasę TypeAsync jako element HTML typu "span".
customElements.define("type-async", TypeAsync, { extends: "span" });

// Wywołuje funkcję inicjującą.
init();