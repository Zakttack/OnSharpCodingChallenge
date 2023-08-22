function handleText() {
    const textValue = document.getElementById('playerNameInput').value;
    fetch('/api/Player/AddPlayer', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body:
            JSON.stringify({ text: textValue})
    });
}