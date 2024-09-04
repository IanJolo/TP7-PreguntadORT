document.getElementById('spinButton').addEventListener('click', spinWheel);

function spinWheel() {
    const wheel = document.getElementById('wheel');
    const confettiContainer = document.getElementById('confetti-container');
    const result = document.getElementById('result');
    const spinButton = document.getElementById('spinButton');

    // Desactivar el botón mientras gira
    spinButton.disabled = true;
    result.classList.add('hidden');

    // Generar un ángulo aleatorio para la ruleta
    const totalSections = 4;
    const sectionAngle = 360 / totalSections;
    const randomSection = Math.floor(Math.random() * totalSections);
    const randomAngleOffset = Math.random() * sectionAngle;
    const angle = (randomSection * sectionAngle) + randomAngleOffset + 720; // Asegurar al menos 2 vueltas completas

    // Girar la ruleta
    wheel.style.setProperty('--rotate', angle);

    // Mostrar confeti y resultado después de que termine de girar
    setTimeout(() => {
        createConfetti();

        // Calcular la opción seleccionada basada en el ángulo final
        const options = ['BESO', 'CHUPITO', 'TORTAZO', 'JUEGO'];
        let selectedOption = options[randomSection];

        // Ajustar el resultado para los casos específicos
        if (selectedOption === 'JUEGO') {
            selectedOption = 'CHUPITO';
        } else if (selectedOption === 'CHUPITO') {
            selectedOption = 'JUEGO';
        }

        result.innerText = selectedOption;
        result.classList.remove('hidden');

        // Redirigir después de 15 segundos
        setTimeout(() => {
            clearConfetti();
            window.location.href = 'https://tu-web-de-eleccion.com';
        }, 15000);
    }, 5000); // La ruleta gira durante 5 segundos

    // Rehabilitar el botón después de 15 segundos
    setTimeout(() => {
        spinButton.disabled = false;
    }, 15000);
}

function createConfetti() {
    const confettiContainer = document.getElementById('confetti-container');
    for (let i = 0; i < 100; i++) {
        const confetti = document.createElement('div');
        confetti.className = 'confetti';
        confetti.style.left = `${Math.random() * 100}%`;
        confetti.style.animationDuration = `${Math.random() * 3 + 2}s`;
        confetti.style.backgroundColor = `hsl(${Math.random() * 360}, 100%, 50%)`;
        confettiContainer.appendChild(confetti);
    }
}

function clearConfetti() {
    const confettiContainer = document.getElementById('confetti-container');
    confettiContainer.innerHTML = '';
}
