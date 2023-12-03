document.addEventListener('DOMContentLoaded', function () {
    var ctx = document.getElementById('tipoUsuarioChart').getContext('2d');
    var labelsJson = '@Html.Raw(ViewBag.TiposDeUsuariosJson)';
    var dataJson = '@Html.Raw(ViewBag.CantidadesJson)';

    if (labelsJson && dataJson) {
        var labels = JSON.parse(labelsJson);
        var data = JSON.parse(dataJson);

        var tipoUsuarioData = {
            labels: labels,
            datasets: [{
                label: '# de Usuarios',
                data: data,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    // Puedes agregar más colores aquí
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    // Puedes agregar más bordes de color aquí
                ],
                borderWidth: 1
            }]
        };

        var myChart = new Chart(ctx, {
            type: 'bar', // o 'line', 'pie', etc., según tus necesidades
            data: tipoUsuarioData,
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    } else {
        console.error('Los datos de la gráfica no están disponibles.');
    }
});