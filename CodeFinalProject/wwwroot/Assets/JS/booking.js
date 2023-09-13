
$(document).on("input", "#searchValue", function (e) {

    var searchValue = $("#searchValue").val();
    let url = `Room/GetSearch?searchValue=${searchValue}`;

    fetch(url)
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Xeta bas verdi");
            }
        })
        .then(data => {
            console.log(data);
            let x = '';
            data.forEach(item => {
                x += ` 
                           <li>
                              <a style="color:gray;" href="room/detail/${item.id}" >${item.name}</a>
                           </li>`
                    ;
            });

            $("#searchResults ul").html(x);
        })
        .catch(error => {
            alert(error.message);
        });
});












let menu = document.querySelector('#booking-active');
let menu_bar = document.querySelector('#dropdown-menu')

menu.addEventListener('click', function () {
    console.log("salam")
    if (menu_bar.classList.contains('d-none')) {
        menu_bar.className = ''
    }
    else {
        menu_bar.className = 'd-none'
    }
});


let services = document.querySelector('#room-more-details');
let service_bar = document.querySelector('#room-services')

services.addEventListener('click', function () {
    console.log("salam")
    if (service_bar.classList.contains('d-none')) {
        service_bar.className = ''
    }
    else {
        service_bar.className = 'd-none'
    }
});

function AmountCur() {
    var a = document.getElementById('range1').value
    document.getElementById('amount-value').value = a
}

function Amount() {
    document.getElementById('range1').value = d
    var d = document.getElementById('amount-value').value
}


