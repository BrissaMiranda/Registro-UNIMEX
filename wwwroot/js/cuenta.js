
document.addEventListener("DOMContentLoaded", () => {

    const nueva = document.getElementById("NuevaPassword");
    const confirmar = document.getElementById("ConfirmarPassword");

    if (!nueva || !confirmar)
        return;

    confirmar.addEventListener("input", () => {

        if (confirmar.value === "")
        {
            confirmar.setCustomValidity("");
            return;
        }

        if (nueva.value !== confirmar.value)
        {
            confirmar.setCustomValidity("Las contraseñas no coinciden.");
        }
        else
        {
            confirmar.setCustomValidity("");
        }

    });

});