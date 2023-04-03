function loadDataTable() {

    $.ajax({
        type: "GET",
        url: "/MCRTStock/MCRTHeadersData",
        dataType: "json",
        data: {},
        contentType: "application/json",
        success: function (data) {
            if (data != null)
                displayDataTable(data);
        },
        error: function (result) {
            toastr.error('Something went wrong.');
        }
    });

    
}

function displayDataTable(ddata) {
    console.log(ddata);
    $('#tblMCTs').DataTable({
        "data": ddata,
        "columns": [
            { 'data': 'Id', "autoWidth": true },
            { 'data': 'ReturnedDate', "autoWidth": true },
            {
                'data': 'IsConsumer',
                "autoWidth": true,
                "render": function (data, type, row) {
                    if (data == true) {
                        return '<input type="checkbox" checked value=true disabled>';
                    } else {
                        return '<input type="checkbox" disabled>';
                    }
                    return data;
                },
                "className": "text-center"
            },
            { 'data': 'ReturnedBy', "autoWidth": true },
            { 'data': 'PostedBy', "autoWidth": true }
    
    
        ],
        "aoColumnDefs": [
            {
                "aTargets": [5],
                "mData": "Id",
                "mRender": function (data, type, full) {
                    return '<button class="btn btn-primary" style="font-size:smaller;" href="#"' + 'id="' + data + '" onclick="view(' + data + ')"><i class="glyphicon glyphicon-print"></i></button>';
                },
                "className": "text-center"
            }
        ],
        "aaSorting": [[0, "desc"]]
    });
}