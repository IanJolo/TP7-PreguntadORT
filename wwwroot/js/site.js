function spinWheel() {
    const wheel = document.querySelector('.wheel');
    const result = document.getElementById('result');

    // Genera un número aleatorio de grados y hace que gire varias vueltas (agregando 720 o más para vueltas adicionales)
    const degrees = Math.floor(Math.random() * 360) + 720;
    wheel.style.transform = `rotate(${degrees}deg)`;

    setTimeout(() => {
        const normalizedDegrees = degrees % 360;
        const sectionDegrees = 360 / 5;
        const selectedIndex = Math.floor(normalizedDegrees / sectionDegrees);
        const categories = ['Geografía', 'Deportes', 'Entretenimiento', 'Ciencia', 'Historia'];
        const selectedCategory = categories[selectedIndex];

        result.textContent = `Categoría seleccionada: ${selectedCategory}`;
        console.log("Categoría seleccionada:", selectedCategory);
    }, 1600); // Ajusta el tiempo de espera si es necesario para sincronizar con la animación
}
