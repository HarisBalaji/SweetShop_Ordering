let currentSlide = 0;

function showSlide(index) {
    const slider = document.querySelector('.slider');
    const dots = document.querySelectorAll('.dot');

    if (index < 0) {
        currentSlide = 2;
    } else if (index > 2) {
        currentSlide = 0;
    } else {
        currentSlide = index;
    }

    const newTransformValue = -currentSlide * 100 + '%';
    if (currentSlide === 0 && index === 2) {
        slider.style.transition = 'transform 0.5s ease-in-out';
    } else {
        slider.style.transition = 'none'; 
    }
    slider.style.transform = `translateX(${newTransformValue})`;

    dots.forEach((dot, i) => {
        dot.classList.toggle('active', i === currentSlide);
    });

    const textContainers = document.querySelectorAll('.text-container');
    textContainers.forEach((textContainer, i) => {
        const isCurrentSlide = i === currentSlide;
        textContainer.classList.toggle('animated-text', isCurrentSlide);
        textContainer.style.animation = isCurrentSlide ? 'slideInLeft 1s ease-out' : 'none';
    });
}

function nextSlide() {
    showSlide(currentSlide + 1);
}

function previousSlide() {
    showSlide(currentSlide - 1);
}

setInterval(nextSlide, 5000);

const dots = document.querySelectorAll('.dot');
dots.forEach((dot, i) => {
    dot.addEventListener('click', () => showSlide(i));
});

document.addEventListener('DOMContentLoaded', () => {
    showSlide(0);
});

