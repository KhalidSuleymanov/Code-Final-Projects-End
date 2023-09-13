
let slidersImages = [
    'Assets/Images/second-slider-image-4.jpg',
    'Assets/Images/second-slider-image-1.jpg',
    'Assets/Images/second-slider-image-3.jpg',
    'Assets/Images/second-slider-image-2.jpg',
]
var dots = document.querySelector('.dots')
let imageCounter = 0;
let image = document.querySelector('.sliders img');
image.src = slidersImages[imageCounter];
for (let i = 0; i < slidersImages.length; i++) {
    var dot = document.createElement('div')
    dot.className = 'pn';
    dots.appendChild(dot)
}
var createdDots = document.getElementsByClassName("pn");
for (let i = 0; i < slidersImages.length; i++) {
    createdDots[i].onclick = function () {
        image.src = slidersImages[i]
    }
}
function AutoPlay() {
    imageCounter++;
    if (imageCounter >= slidersImages.length) {
        imageCounter = 0;
    }
    image.src = slidersImages[imageCounter];
}
after.onclick = function () {
    AutoPlay();
}
before.onclick = function () {
    imageCounter--;
    if (imageCounter === -1) {
        imageCounter = slidersImages.length - 1;
    }
    image.src = slidersImages[imageCounter];
}
setInterval(() => {
    AutoPlay();
}, 3000);