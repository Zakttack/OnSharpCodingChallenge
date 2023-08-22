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