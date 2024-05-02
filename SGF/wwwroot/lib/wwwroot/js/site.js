// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var  formatear_fecha = (fecha)=>{
    let fecha_inicial = new Date(fecha);
    return fecha_inicial.toLocaleDateString("es-EC").replaceAll('/', '-');
}
var  formatear_fecha_hora = (fecha)=>{
    let fecha_inicial = new Date(fecha);
    return fecha_inicial.toLocaleString("es-EC").replaceAll(',', '').replaceAll('/', '-');
}