
document.addEventListener("DOMContentLoaded", () => {

    const rol = document.getElementById("rol");
    const estudiantes = document.getElementById("divEstudiantes");
    const docentes = document.getElementById("divDocentes");

    function cambiarRol() {

        if (!rol) return;

        if (rol.value == "2") {

            estudiantes.style.display = "block";
            docentes.style.display = "none";

        }
        else if (rol.value == "3") {

            estudiantes.style.display = "none";
            docentes.style.display = "block";

        }
        else {

            estudiantes.style.display = "none";
            docentes.style.display = "none";

        }

    }

    rol.addEventListener("change", cambiarRol);

    cambiarRol();

});