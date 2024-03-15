let deferredPrompt;
const installButton = document.getElementById('installButton'); // Assume you have an install button in your HTML

// ReSharper disable once Html.EventNotResolved
window.addEventListener("beforeinstallprompt", (e) => {
    e.preventDefault();
    deferredPrompt = e;
    console.log('listening for beforeinstallprompt..');
    installButton.style.display = 'block';
});

installButton.addEventListener('click', () => {
    // Hide our user interface that shows our A2HS button
    installButton.style.display = 'none';
    // Show the prompt
    deferredPrompt.prompt();
    // Wait for the user to respond to the prompt
    deferredPrompt.userChoice.then((choiceResult) => {
        if (choiceResult.outcome === 'accepted') {
            console.log('User accepted the A2HS prompt');
        } else {
            console.log('User dismissed the A2HS prompt');
        }
        deferredPrompt = null;
    });
});