function handleBowl() {
    const pinsKnockedDown = +document.getElementById('pinsKnockedDownInput').value;
    fetch('/api/BowlingGame/Bowl', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body:
            JSON.stringify({value: pinsKnockedDown})
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
        document.getElementById('pinsKnockedDownError').textContent = error.message;
    });
}