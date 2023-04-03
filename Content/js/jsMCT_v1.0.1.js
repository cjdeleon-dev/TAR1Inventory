
function loadDataTable() {
    $.ajax({
        type: "GET",
        url: "/MCTStock/MCTHeadersData",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result.data);
            var body;
            if (result.data != "[]") {
                $.each(result.data, function (i, data) {
                    var strId = data.Id;
                    body = '<tr>';
                    body += '<td class="text-center">' + strId + '</td>';
                    body += '<td>' + data.PostedDate + '</td>';
                    body += '<td>' + data.PostedBy + '</td>';
                    body += '<td>' + data.Project + '</td>';
                    body += '<td>' + data.ReceivedBy + '</td>';
                    body += '<td class="text-center">' + data.IsConsumer + '</td>';
                    body += '<td></td>';
                    body += '</tr>';
                    //append content
                    $("#tblMCTs tbody").append(body);
                });

                /*DataTables instantiation.*/
                $("#tblMCTs").DataTable({
                    "Paginate": true,
                    "columns": [
                        { 'data': result.data.Id, "autoWidth": true },
                        { 'data': result.data.PostedDate, "autoWidth": true },
                        { 'data': result.data.PostedBy, "autoWidth": true },
                        { 'data': result.data.Project, "autoWidth": true },
                        { 'data': result.data.ReceivedBy, "autoWidth": true },
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
                        }

                    ],
                    "aoColumnDefs": [
                        {
                            "aTargets": [6],
                            "mData": result.data.Id,
                            "mRender": function (data, type, full) {
                                return '<button class="btn btn-primary" style="font-size:smaller;" href="#"' + 'id="' + data + '" onclick="view(' + data + ')"><i class="glyphicon glyphicon-print"></i></button>';
                            },
                            "className": "text-center"
                        }
                    ],
                    "aaSorting": [[0, "desc"]]
                });
            }
           
        },
        error: function () {
            alert();
        }
    });


}

//function displayDataTable(ddata) {
//    console.log(ddata);
//    var mydata = ddata;
//    $('#tblMCTs').DataTable({
//        "columns": [
//            { 'data': 'Id', "autoWidth": true },
//            { 'data': 'PostedDate', "autoWidth": true },
//            { 'data': 'PostedBy', "autoWidth": true },
//            { 'data': 'Project', "autoWidth": true },
//            { 'data': 'ReceivedBy', "autoWidth": true },
//            {
//                'data': 'IsConsumer',
//                "autoWidth": true,
//                "render": function (data, type, row) {
//                    if (data == true) {
//                        return '<input type="checkbox" checked value=true disabled>';
//                    } else {
//                        return '<input type="checkbox" disabled>';
//                    }
//                    return data;
//                },
//                "className": "text-center"
//            }
    
//        ],
//        "aoColumnDefs": [
//            {
//                "aTargets": [6],
//                "mData": "Id",
//                "mRender": function (data, type, full) {
//                    return '<button class="btn btn-primary" style="font-size:smaller;" href="#"' + 'id="' + data + '" onclick="view(' + data + ')"><i class="glyphicon glyphicon-print"></i></button>';
//                },
//                "className": "text-center"
//            }
//        ],
//        "aaSorting": [[0, "desc"]]
//    });

//}

function newEntry() {
    $('#listMCT').hide();
    $('#mainButtons').hide();

    var html = '';
    html += '<div class="row">';
    html += '   <div class="col-lg-offset-1 col-lg-10">';
    html += '       <div class="form-group">';
    html += '           <button id="btnSaveMCT" type="button" class="btn btn-light bg-success rounded-pill shadow-sm px-4 mb-4" onclick="saveEntry()">';
    html += '               <i class="glyphicon glyphicon-floppy-disk mr-2"></i><small class="text-uppercase font-weight-bold"> Save</small>';
    html += '           </button>';
    html += '           <button id="btnCancel" type="button" class="btn btn-light bg-success rounded-pill shadow-sm px-4 mb-4" onclick="cancelEntry()">';
    html += '               <i class="glyphicon glyphicon-circle-arrow-left mr-2"></i><small class="text-uppercase font-weight-bold"> Cancel</small>';
    html += '           </button>';
    html += '       </div>';
    html += '       <h5>New Entry of Material Charge Ticket</h5>';
    html += '       <form style="background-color:white;padding:10px;border-radius:15px;">';
    html += '           <div class="form-group scroll">';
    html += '               <div class="row">';
    html += '                   <div class="col-md-3">';
    html += '                       <h6 class="text-primary">HEADER</h6>';
    html += '                   </div>';
    html += '               </div>';
    html += '               <div class="form-group">';
    html += '                   <div class="row">';
    html += '                       <div class="col-md-2">';
    html += '                           <input type="hidden" class="form-control" id="Id" placeholder="Id" disabled="disabled" />';
    html += '                           <label for="PostedDate" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Date: </label>';
    html += '                           <input type="date" class="form-control" id="PostedDate" style="min-width: 100%" />';
    html += '                       </div>';
    html += '                       <div class="col-md-5">';
    html += '                           <label for="Project" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Project: </label>';
    html += '                           <input type="text" class="form-control" id="Project" placeholder="Project" style="min-width: 100%" />';
    html += '                       </div>';
    html += '                       <div class="col-md-5">';
    html += '                           <label for="ProjectAddress" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Project Address: </label>';
    html += '                           <input type="text" class="form-control" id="ProjectAddress" placeholder="Project Address" style="min-width: 100%" />';
    html += '                       </div>';
    html += '                   </div>';
    html += '                   <div class="row">';
    html += '                       <div class="col-xs-6">';
    html += '                           <input type="hidden" class="form-control" id="IssuedById" />';
    html += '                           <label for="IssuedBy" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Issued By: </label>';
    html += '                           <select id="IssuedBy" class="form-control select2 select2-hidden-accessible" onchange="return cboSig1OnChange();" style="min-width: 100%"></select>';
    html += '                       </div>';
    html += '                       <div class="col-xs-6">';
    html += '                           <input type="hidden" class="form-control" id="ReceivedById" />';
    html += '                           <input type="checkbox" id="isConsumer" onclick="onChangeCheckBox();" />';
    html += '                           <label for="ReceivedBy" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0"> Is Consumer Received By: </label>';
    html += '                           <select id="ReceivedBy" class="form-control select2 select2-hidden-accessible" onchange="return cboRecOnChange();" style="min-width: 100%"></select>';
    html += '                           <input type="text" id="ConsumerReceivedBy" class="form-control" style="min-width:100%;" placeholder="Name of Consumer" />';
    html += '                       </div>';
    html += '                   </div>';
    html += '               </div>';
    html += '               <hr style="border-color:darkred;" />';
    html += '               <div class="row">';
    html += '                   <div class="col-md-3">';
    html += '                       <h6 class="text-primary">DETAIL ENTRY</h6>';
    html += '                   </div>';
    html += '               </div>';
    html += '               <div class="form-group">';
    html += '                   <div class="row">';
    html += '                       <div class="col-md-8">';
    html += '                           <input type="hidden" class="form-control" id="JOWOMOId" disabled="disabled" />';
    html += '                           <label for="JOWOMO" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">JO/WO/MO: </label>';
    html += '                           <select id="JOWOMO" class="form-control select2 select2-hidden-accessible" onchange="return cboJOWOMOOnChange();" style="min-width: 100%"></select>';
    html += '                       </div>';
    html += '                       <div class="col-md-3">';
    html += '                           <label for="JOWOMO" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Number: </label>';
    html += '                           <input type="text" class="form-control" id="JOWOMONumber" placeholder="JO/WO/MO Number" style="min-width: 100%" />';
    html += '                       </div>';
    html += '                   </div>';
    html += '                   <div class="row">';
    html += '                       <div class="col-md-10">';
    html += '                           <input type="hidden" class="form-control" id="MaterialId" />';
    html += '                           <label for="Material" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Select Material: </label>';
    html += '                           <select id="Material" name="cbomaterial" class="form-control select2 select2-hidden-accessible" onchange="return cboMaterialOnChange();" style="min-width: 100%"></select>';
    html += '                       </div>';
    html += '                       <div class="col-md-2" style="text-align:center;">';
    html += '                           <input type="hidden" class="form-control" id="UnitId" />';
    html += '                           <label for="Unit" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Unit </label>';
    html += '                           <input type="text" class="form-control" id="Unit" style="min-width: 100%; text-align:center;" disabled="disabled" />';
    html += '                       </div>';
    html += '                   </div>';
    html += '                   <div class="row">';
    html += '                       <div class="col-md-5" style="text-align:center;">';
    html += '                           <label for="SerialNo" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Serial Number: </label>';
    html += '                           <input type="text" class="form-control" id="SerialNo" name="txtserialno" onkeypress="return txtSerialOnKeyPress(event)" style="min-width: 100%; text-align:right;" />';
    html += '                       </div>';
    html += '                       <div class="col-md-2" style="text-align:center;">';
    html += '                           <label for="OnHand" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">On Hand </label>';
    html += '                           <input type="text" class="form-control" id="OnHand" style="min-width: 100%; text-align:center;" disabled="disabled" />';
    html += '                       </div>';
    html += '                       <div class="col-md-3" style="text-align:center;">';
    html += '                           <label for="Quantity" class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">QTY </label>';
    html += '                           <input type="text" class="form-control" id="Quantity" name="txtquantity" style="min-width: 100%; text-align:center;" onkeypress="return isNumber(event)" placeholder="0" />';
    html += '                       </div>';
    html += '                       <div class="col-md-2 text-center">';
    html += '                           <br /><button type="button" class="btn btn-light bg-success rounded-pill shadow-sm px-4 mt-4" id="btnAddMaterialItem" onclick="addMaterialItem();"><i class="glyphicon glyphicon-plus-sign mr-2"></i><small class="text-uppercase font-weight-bold"> ADD</small></button>';
    html += '                       </div>';
    html += '                   </div>';
    html += '                   <hr style="border-color:darkred;" />';
    html += '                   <div class="row">';
    html += '                       <div class="col-md-3">';
    html += '                           <h6 class="text-primary">DETAILS</h6>';
    html += '                       </div>';
    html += '                   </div>';
    html += '                   <table class="table table-striped table-bordered table-hover" id="tblMDetails" style="padding-left:10px;padding-right:10px;">';
    html += '                       <thead bgcolor="lightblue">';
    html += '                           <tr>';
    html += '                               <th style="text-align:center;display:none;">';
    html += '                                   MaterialId';
    html += '                               </th>';
    html += '                               <th style="text-align:center;">';
    html += '                                   Material';
    html += '                               </th>';
    html += '                               <th style="text-align:center;display:none;">';
    html += '                                   UnitId';
    html += '                               </th>';
    html += '                               <th style="text-align:center;">';
    html += '                                   Unit';
    html += '                               </th>';
    html += '                               <th style="text-align:center;">';
    html += '                                   Quantity';
    html += '                               </th>';
    html += '                               <th style="text-align:center;">';
    html += '                                   Serial No';
    html += '                               </th>';
    html += '                               <th style="text-align:center;display:none;">';
    html += '                                   JOWOMO Id';
    html += '                               </th>';
    html += '                               <th style="text-align:center;">';
    html += '                                   Code';
    html += '                               </th>';
    html += '                               <th style="text-align:center;">';
    html += '                                   Number';
    html += '                               </th>';
    html += '                               <th style="text-align:center;">';
    html += '                                   Action';
    html += '                               </th>';
    html += '                           </tr>';
    html += '                       </thead>';
    html += '                       <tbody class="tbodydetail"></tbody>';
    html += '                   </table>';
    html += '                   <div class="row">';
    html += '                       <div class="col-xs-4">';
    html += '                           <input type="hidden" class="form-control" id="CheckedById" />';
    html += '                           <label for="CheckedBy"  style="visibility:hidden;">Checked By: </label>';
    html += '                           <select id="CheckedBy" style="visibility:hidden;" class="form-control select2 select2-hidden-accessible" onchange="return cboSig4OnChange();" style="min-width: 100%"></select>';
    html += '                       </div>';
    html += '                       <div class="col-xs-4">';
    html += '                           <input type="hidden" class="form-control" id="NotedById" />';
    html += '                           <label for="NotedBy" style="visibility:hidden;">Noted By: </label>';
    html += '                           <select id="NotedBy" style="visibility:hidden;" class="form-control select2 select2-hidden-accessible" onchange="return cboSig2OnChange();" style="min-width: 100%"></select>';
    html += '                       </div>';
    html += '                       <div class="col-xs-4">';
    html += '                           <input type="hidden" class="form-control" id="AuditedById" />';
    html += '                           <label for="AuditedBy" style="visibility:hidden;">Audited By: </label>';
    html += '                           <select id="AuditedBy" style="visibility:hidden;" class="form-control select2 select2-hidden-accessible" onchange="return cboSig3OnChange();" style="min-width: 100%"></select>';
    html += '                       </div>';
    html += '                   </div>';
    html += '               </div>';
    html += '           </div>';
    html += '       </form>';
    html += '   </div >';
    html += '</div > ';

    $('#frmNewEntry').append(html);

    initDefaultFields();
}
function saveEntry() {
    var tsek;
    if ($('#isConsumer').is(":checked")) {
        tsek = true;
    } else {
        tsek = false;
    }

    if ($('#tblMDetails tbody tr').length == 0) {
        toastr.info("Invalid Entry. Please fill all required fields.");
        return false;
    }

    if (tsek) {
        if ($('#ConsumerReceivedBy').val().trim() == "") {
            toastr.info("Invalid Received By.");
            return false;
        }
    } else {
        if ($('#ReceivedById').val() == 0) {
            toastr.info("Invalid Selected Received By.");
            return false;
        }
    }

    if ($('#Project').val().trim() == "") {
        toastr.info("Project Field is required.");
        return false;
    }

    if ($('#ProjectAddress').val().trim() == "") {
        toastr.info("Project Address Field is required.");
        return false;
    }

    if ($('#JOWOMOId').val() == 0) {
        toastr.info("Invalid Selected JO/WO/MO.");
        return false;
    }

    if ($('#JOWOMONumber').val().trim() == "") {
        toastr.info("JO/WO/MO Number Field is required.");
        return false;
    }

    var objdata = {
        Id: parseInt(0),
        PostedDate: $('#PostedDate').val(),
        PostedById: parseInt($('#uid').val()),
        IssuedById: $('#IssuedById').val(),
        IsConsumerReceived: tsek,
        ReceivedById: $('#ReceivedById').val(),
        ConsumerReceivedBy: $('#ConsumerReceivedBy').val(),
        CheckedById: $('#CheckedById').val(),
        AuditedById: $('#AuditedById').val(),
        NotedById: $('#NotedById').val(),
        Project: $('#Project').val(),
        ProjectAddress: $('#ProjectAddress').val(),
        JOWOMOId: $('#JOWOMOId').val(),
        JOWOMONumber: $('#JOWOMONumber').val()
    };

    if (objdata != null) {
        $.ajax({
            type: "POST",
            url: "/MCTStock/AddMCTHeader/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify(objdata),
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess) {
                    $.ajax({
                        url: "/MCTStock/GetLoggedUserMaxMMId/" + parseInt($('#uid').val()),
                        type: "GET",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        success: function (result) {
                            if (parseInt(result.Id) > 0) {

                                addChargedMaterialDetails(result.Id);
                            }
                        },
                        error: function (errormessage) {
                            toastr.error(errormessage.responseText);
                        }
                    });
                } else {
                    toastr.error(response.ProcessMessage)
                }
            },
            error: function (errormessage) {
                toastr.error(errormessage.responseText);
            }
        });
    }
}

function addChargedMaterialDetails(id) {

    //kunin number of rows in table
    var cnt = $('#tblMDetails tbody tr').length;
    //kunin lahat ng rows
    var tblrows = $('#tblMDetails tbody tr');

    var arrayData = new Array();

    //dito iyon magloloop per item (details)
    for (var i = 0; i < cnt; i++) {

        var $tds = tblrows[i].cells;

        var detailobj = {
            MCTHeaderId: id,
            MaterialId: parseInt($tds[0].innerText),
            Quantity: parseInt($tds[4].innerText),
            UnitId: parseInt($tds[2].innerText),
            SerialNo: $tds[5].innerText,
            JOWOMOId: parseInt($tds[6].innerText),
            JOWOMONumber: $tds[8].innerText
        };

        arrayData.push(detailobj);
    }

    $.ajax({
        url: "/MCTStock/AddMCTDetail/",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(arrayData),
        dataType: "json",
        success: function (result) {
            swal({
                title: "Record Saved!",
                text: result.ProcessMessage,
                type: "success",
                showCancelButton: false,
                confirmButtonClass: "btn-success",
                confirmButtonText: "OK",
                closeOnConfirm: false
            },
                function () {
                    window.location = "/MCTStock/MCTStock";
                }
            );
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
}

function cancelEntry() {
    window.location = "/MCTStock/MCTStock";
}

function initDefaultFields() {
        var now = new Date();

        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);

        var today = now.getFullYear() + "-" + (month) + "-" + (day);

        $('#PostedDate').val(today);

        //initialize dropdown lists..
        loadcboJOWOMOs();
        loadcboMaterials();
        loadcboReceivedBy();
        loadcboSig1();
        loadcboSig2();
        loadcboSig3();
        loadcboSig4();

        $('#ReceivedBy').show();
        $('#ConsumerReceivedBy').hide();

        $('#ReceivedById').val(0);
        $('#IssuedById').val(332); //default Jerricko J. Tulabot
        $('#NotedById').val(317); //default Rodolfo R. Tal Placido
        $('#AuditedById').val(5); //default Melanie A. Abogado
        $('#CheckedById').val(78) //default GUBAC, IVY MARIE D.  

        $('#Project').val("");
        $('#ProjectAddress').val("");
        $('#JOWOMO').val(0);
        $('#JOWOMONumber').val("");

        $('#Material').val(0);
        $('#Unit').val("");
        $('#Quantity').val("");
        $('#SerialNo').val("");

        $('.tbodydetail').empty();
}

function onChangeCheckBox() {
    if ($('#isConsumer').is(":checked")) {
        $('#ReceivedBy').hide();
        $('#ConsumerReceivedBy').show();
    } else {
        $('#ReceivedBy').show();
        $('#ConsumerReceivedBy').hide();
    }
}

function loadcboMaterials() {
    $.ajax({
        url: "/MCTStock/GetMaterials/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Material').empty();
            $('#Material').val(0);
            $('#Material').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var Desc = result[i].Code + " - " + result[i].Description
                var opt = new Option(Desc, result[i].Id);
                $('#Material').append(opt);
            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function loadcboJOWOMOs() {
    $.ajax({
        url: "/MCTStock/GetJOWOMOs/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#JOWOMO').empty();
            $('#JOWOMO').val(0);
            $('#JOWOMO').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var opt = new Option((result[i].Code + " - " + result[i].Description), result[i].Id);
                $('#JOWOMO').append(opt);
            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function loadcboReceivedBy() {
    $.ajax({
        url: "/MCTStock/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ReceivedBy').empty();
            $('#ReceivedBy').val(0);
            $('#ReceivedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#ReceivedBy').append(opt);
            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig1() {
    $.ajax({
        url: "/MCTStock/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#IssuedBy').empty();
            $('#IssuedBy').val(0);
            $('#IssuedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#IssuedBy').append(opt);
            }
            $('#IssuedBy option[value=332]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig2() {
    $.ajax({
        url: "/MCTStock/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#NotedBy').empty();
            $('#NotedBy').val(0);
            $('#NotedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#NotedBy').append(opt);
            }
            $('#NotedBy option[value=60]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig3() {
    $.ajax({
        url: "/MCTStock/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#AuditedBy').empty();
            $('#AuditedBy').val(0);
            $('#AuditedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#AuditedBy').append(opt);
            }
            $('#AuditedBy option[value=5]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig4() {
    $.ajax({
        url: "/MCTStock/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#CheckedBy').empty();
            $('#CheckedBy').val(0);
            $('#CheckedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#CheckedBy').append(opt);
            }
            $('#CheckedBy option[value=178]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function view(id) {

    var parent = $('embed#mctpdf').parent();
    var newElement = '<embed src="/MCTStock/MCTReportView?id=' + parseInt(id) + '"  width="100%" height="700px" type="application/pdf" id="mctpdf">';

    $('embed#mctpdf').remove();
    parent.append(newElement);

    $('#MCTModal').modal('show');
}

function addMaterialItem() {
    //for validation-----------------------------------------------------------
    if ($('#Project').val().trim() == "") {
        toastr.info('Project field is required.');
        return false;
    }
    if ($('#ProjectAddress').val().trim() == "") {
        toastr.info('Project Address field is required.');
        return false;
    }
    if (parseInt($('#JOWOMO').val()) == 0) {
        toastr.info('Please select job order/work order/maintenance order.');
        return false;
    }
    if ($('#JOWOMONumber').val().trim() == "") {
        toastr.info('Job Order/Work Order/Maintenance Order Number is required.');
        return false;
    }
    if (parseInt($('#Material').val()) == 0) {
        toastr.info('Please select Material.');
        return false;
    }
    if (parseInt($('#Quantity').val()) == 0 || $('#Quantity').val() == "") {
        toastr.info('Quantity should be greater than zero (0).');
        return false;
    }
    if (parseInt($('#Quantity').val()) > 0) {
        //check if quantity is less than or equal to onhand.
        if (parseInt($('#Quantity').val()) > parseInt($('#OnHand').val())) {
            toastr.info('Quantity should be less than or equal to OnHand value.');
            return false;
        }
    }
    //end of validation---------------------------------------------------------------

    var selMaterial = $("#Material option:selected").html();
    var selUnit = $("#Unit").val();
    var selectedJOWOMO = $("#JOWOMO option:selected").html();
    var selJOWOMO = selectedJOWOMO.split('-');
    var JOWOMOCode = selJOWOMO[0];

    //<input type="text" class="text-center" style="padding:0;border:none;" value=' + $(' #SerialNo').val() + ' />

    var html = '';
    html += '<tr style="background-color: white;">';
    html += '<td style="display:none;" contenteditable="false">' + $('#MaterialId').val() + '</td>';
    html += '<td contenteditable="false">' + selMaterial + '</td>';
    html += '<td style="display:none;" contenteditable="false">' + $('#UnitId').val() + '</td>';
    html += '<td contenteditable="false">' + selUnit + '</td>';
    html += '<td style="text-align:center;" contenteditable="false">' + $('#Quantity').val() + '</td>';
    html += '<td style="text-align:center;" contenteditable="true">' + $(' #SerialNo').val() + '</td>';
    html += '<td style="text-align:center;display:none;" contenteditable="false">' + $('#JOWOMO').val() + '</td>';
    html += '<td style="text-align:center;" contenteditable="false">' + JOWOMOCode + '</td>';
    html += '<td style="text-align:center;" contenteditable="true">' + $('#JOWOMONumber').val() + '</td>';
    html += '<td style="text-align:center;" contenteditable="false">' +
        '<button class="btn btn-danger" style="font-size:smaller;" onclick=$(this).closest("tr").remove();>' +
        '<i class="glyphicon glyphicon-trash"></i></button></td > ';
    html += '</tr>';
    $('.tbodydetail').append(html);

    //reset required fields after adding the selected material
    var mat = selMaterial.split('-');

    $('#SerialNo').val("");
    $('#Quantity').val("");

    if (mat[0].trim().toUpperCase().substring(0, 5) != "KWH M" && mat[0].trim().toUpperCase().substring(0, 5) != "TRANS") {
        //document.myform.cbomaterial.focus();
        $('#Material').focus();
    } else {
        //document.myform.txtserialno.focus();
        $('#SerialNo').focus();
    }

}

function cboMaterialOnChange() {
    var matid = $('#Material').val();
    $('#MaterialId').val(matid);
    //get unitid, unit, and onhand
    $.ajax({
        url: "/MCTStock/GetUnitAndOnHandByMaterialId/",
        data: "matid=" + parseInt(matid),
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var unitonhand = result;
            var splittedvalues = unitonhand.split("#");
            var unitid = splittedvalues[0];
            var unitcode = splittedvalues[1];
            var onhand = splittedvalues[2];
            $('#UnitId').val(unitid);
            $('#Unit').val(unitcode);
            $('#OnHand').val(onhand);

            var selMaterial = $("#Material option:selected").html();
            var mat = selMaterial.split('-');

            if (mat[0].trim().toUpperCase().substring(0, 5) == "KWH M" || mat[0].trim().toUpperCase().substring(0, 5) == "TRANS"
                || mat[0].trim().toUpperCase().substring(0, 4) == "C.T." || mat[0].trim().toUpperCase().substring(0, 4) == "P.T."
                || mat[0].trim().toUpperCase().substring(0, 5) == "CAPAC") {
                //document.myform.txtserialno.focus();
                $('#SerialNo').focus();
                
            } else {
                //document.myform.txtquantity.focus();
                $('#Quantity').focus();
            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
}

function cboJOWOMOOnChange() {
    var jwmid = $('#JOWOMO').val();
    $('#JOWOMOId').val(jwmid);
}

function cboRecOnChange() {
    var recbyid = $('#ReceivedBy').val();
    $('#ReceivedById').val(recbyid);
}

function subExport() {
    if (isValidDateRange())
        $('#frmExport').submit();
    else
        toastr.error('Invalid Date Range.');

    $('#ExportModal').modal('hide');
}

//validation

function isValidDateRange() {

    if ($('#dtpFrom').val() == '' || $('#dtpTo').val() == '') {
        return false;
    } else {
        if ($('#dtpFrom').val() > $('#dtpTo').val()) {
            return false;
        } else
            return true;
    }

}

//onkeypress events------------------------------------------------------------------
function txtSerialOnKeyPress(evt) {
    if (evt.keyCode === 13) {
        evt.preventDefault();
        document.myform.txtquantity.focus();
    }
}

function isNumber(evt) {
    var ch = String.fromCharCode(evt.which);
    if (!(/[0-9]/.test(ch))) {
        evt.preventDefault();
    }
    if (evt.keyCode === 13) {
        evt.preventDefault();
        addMaterialItem();
    }
}
//-----------------------------------------------------------------------------------
