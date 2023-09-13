cartclick = (id) => {
    var modal = document.querySelector(`.cart-modal-${id}`);
    modal.classList.toggle("d-none");
}

menuclick = (id) => {
    var modal = document.querySelector(`.menu-modal-${id}`);
    modal.classList.toggle("d-none");
}

blogclick = (id) => {
    var modal = document.querySelector(`.blog-modal-${id}`);
    modal.classList.toggle("d-none");
}

let bars = document.querySelector('#icon');
let ham_bar = document.querySelector('#ham-menu')

bars.addEventListener('click', function () {
    if (ham_bar.classList.contains('d-none')) {
        ham_bar.className = ''
    }
    else {
        ham_bar.className = 'd-none'
    }
});
