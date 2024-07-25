$(document).ready(function () {
    $('#reportTable').DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "lengthChange": true,
        "buttons": ['csv', 'excel', 'pdf'],
        "order": [[7, 'desc']],
        //dom: 'Bfrtip'
    });
});