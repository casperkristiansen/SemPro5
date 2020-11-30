// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.




//Sticky navbar start

window.onscroll = function () { myFunction() };

var navbar = document.getElementById("navbar");

var sticky = navbar.offsetTop;

function myFunction() {
   if (window.pageYOffset >= sticky) {
       navbar.classList.add("sticky")
   } else {
        navbar.classList.remove("sticky");
    }
}
//Sticky navbar end


