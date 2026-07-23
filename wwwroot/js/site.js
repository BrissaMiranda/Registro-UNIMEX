document.addEventListener("DOMContentLoaded", () => {

    setTimeout(() => {

        const alerta = document.querySelector(".alert");

        if (alerta) {

            alerta.style.transition = "0.5s";
            alerta.style.opacity = "0";

            setTimeout(() => {

                alerta.remove();

            },500);

        }

    },4000);

});
