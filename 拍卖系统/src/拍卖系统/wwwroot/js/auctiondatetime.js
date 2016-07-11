$(function () {
    $('#reservationtime').daterangepicker({
        timePicker: true,
        timePicker24Hour: true,
        startDate: moment($('#StartTime').val()),
        endDate: moment($('#EndTime').val())
    }, function (start, end) {
        $('#StartTime').val(start.format('YYYY-MM-DD HH:mm:ss'));
        $('#EndTime').val(end.format('YYYY-MM-DD HH:mm:ss'));
    });
});
