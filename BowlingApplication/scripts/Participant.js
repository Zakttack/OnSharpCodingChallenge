document.getElementById('addPlayerForm').addEventListener('submit', function(event) {
    event.preventDefault();
    startGame();
});
function handleText() {
    const textValue = document.getElementById('playerNameInput').value;
    fetch('/api/Player/AddPlayer', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body:
            JSON.stringify({ text: textValue})
    })
    .then (response => {
        if (response.ok) {
            location.reload();
        }
        else {
            return response.text().then(text => {
                throw new Error(text)
            });
        }
    })
    .catch(error => {
        document.getElementById('addPlayerError').textContent = error.message;
    });
}

function startGame() {
    fetch('/api/Player/VerifyNonEmptyPlayers', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body:
            JSON.stringify({})
    })
    .then(response => {
        if (!response.ok) {
            return response.text().then(text => {
                throw new Error(text)
            });
        }
        window.location.href = '/BowlingGame';
    })
    .catch(error => {
        document.getElementById('playerEmptyError').textContent = error.message;
    });
}