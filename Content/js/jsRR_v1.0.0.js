function loadDataTable() {
    try {
        $('#tblRRs').DataTable({
            "ajax": {
                "url": "/RRStock/RRHeadersData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { 'data': 'Id', "autoWidth": true },
                { 'data': 'ReceivedDate', "autoWidth": true },
                { 'data': 'PreparedBy', "autoWidth": true },
                { 'data': 'ReceivedTotalCost', "autoWidth": true },
                {
                    'data': 'IsOld',
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
                { 'data': 'Supplier', "autoWidth": true },
                {
                    'data': 'DeliveryDate',
                    "autoWidth": true
                },
                { 'data': 'Remark', "autoWidth": true }

            ],
            "aoColumnDefs": [
                {
                    "aTargets": [8],
                    "mData": "Id",
                    "mRender": function (data, type, full) {
                        return '<button class="btn btn-primary" style="font-size:smaller;" href="#"' + 'id="' + data + '" onclick="view(' + data + ')"><i class="glyphicon glyphicon-print"></i></button>';
                    },
                    "className": "text-center"
                }
            ],
            "aaSorting": [[0, "desc"]]
        });


    } catch (e) {
        toastr.error('Something went wrong.');
    }
}

function subExport() {
    if (isValidDateRange()) 
        $('#frmExport').submit();
    else 
        toastr.error('Invalid Date Range.');

    $('#ExportModal').modal('hide');
}

function newEntry() {
    $('#listRR').hide();
    $('#mainButtons').hide();
    
    var html = '';
    html += '<div class="row">';
    html += '   <div class="col-lg-offset-1 col-lg-10">';
    html += '       <div class="form-group">';
    html += '           <button id="btnSaveRR" type="button" class="btn btn-light bg-success rounded-pill shadow-sm px-4 mb-4" onclick="saveEntry()">';
    html += '               <i class="glyphicon glyphicon-floppy-disk mr-2"></i><small class="text-uppercase font-weight-bold"> Save</small>';
    html += '           </button>';
    html += '           <button id="btnCancel" type="button" class="btn btn-light bg-success rounded-pill shadow-sm px-4 mb-4" onclick="cancelEntry()">';
    html += '               <i class="glyphicon glyphicon-circle-arrow-left mr-2"></i><small class="text-uppercase font-weight-bold"> Cancel</small>';
    html += '           </button>';
    html += '       </div>';
    html += '       <h5>New Entry of Receiving Report</h5>';
    html += '<form style="background-color:white;padding:10px;border-radius:15px;">';
    html += '    <div class="form-group scroll">';
    html += '        <div class="row">';
    html += '            <div class="col-md-3">';
    html += '                <h6 class="text-primary">HEADER</h6>';
    html += '            </div>';
    html += '        </div>';
    html += '        <div class="form-group">';
    html += '            <div class="row">';
    html += '                <div class="col-md-3">';
    html += '                    <input type="hidden" class="form-control" id="PreparedById" placeholder="Id" disabled="disabled" />';
    html += '                    <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Date:</label>';
    html += '                    <input type="date" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="ReceivedDate" style="min-width: 100%" />';
    html += '                </div>';
    html += '                <div class="col-md-4">';
    html += '                    <input type="hidden" class="form-control" id="SupplierId" />';
    html += '                    <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Supplier:</label><br />';
    html += '                    <select id="Supplier" class="form-control select2 select2-hidden-accessible  text-black-50 font-weight-bold px-3 small pb-4 mb-0" onchange="return cboSupplierOnChange();" style="min-width: 100%"></select>';
    html += '                </div>';
    html += '                <div class="col-md-5">';
    html += '                    <input type="hidden" class="form-control" id="Id" placeholder="Id" disabled="disabled" />';
    html += '                    <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Remark: </label>';
    html += '                    <input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="Remark" placeholder="Remark" style="min-width: 100%" />';
    html += '                </div>';
    html += '            </div>';
    html += '            <hr style="border-color:darkred;" />';
    html += '            <div class="row">';
    html += '                <div class="col-md-3">';
    html += '                   <div class="form-group">';
    html += '                       <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0" style="text-align:right;" >Delivery Date:</label >';
    html += '                       <input type="date" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="drDate" style="min-width: 100%" />';
    html += '                   </div>';
    html += '                </div>';
    html += '                <div class="col-md-9">';
    html += '                   <div class="form-group">';
    html += '                       <div class="row">';
    html += '                           <div class="col-md-1" style="text-align:right;padding-top:5px;"><label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">PO: </label></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="PO1" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="PO2" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="PO3" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="PO4" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="PO5" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                       </div>';
    html += '                       <div class="row">';
    html += '                           <div class="col-md-1" style="text-align:right;padding-top:5px;"><label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">SI: </label></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="SI1" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="SI2" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="SI3" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="SI4" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="SI5" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                       </div>';
    html += '                       <div class="row">';
    html += '                           <div class="col-md-1" style="text-align:right;padding-top:5px;"><label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">DR: </label></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="DR1" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="DR2" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="DR3" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="DR4" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                           <div class="col-md-2"><input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="DR5" style="min-width: 100%" onkeypress="return isNumber(event)" /></div>';
    html += '                       </div>';
    html += '                   </div>';
    html += '                </div>';
    html += '            </div>';
    html += '        <hr style="border-color:darkred;" />';
    html += '        <div class="form-group">';
    html += '            <h6 class="text-primary">DETAIL ENTRY</h6>';
    html += '            <div class="row">';
    html += '                <div class="col-md-6">';
    html += '                    <input type="hidden" id="MaterialId" />';
    html += '                    <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Select Material: </label>';
    html += '                    <select id="Material" class="form-control select2 select2-hidden-accessible text-black-50 font-weight-bold px-3 small pb-4 mb-0" onchange="return cboMaterialOnChange();" style="min-width: 100%"></select>';
    html += '                </div>';
    html += '                <div class="col-md-2" style="text-align:center;">';
    html += '                    <input type="hidden" class="form-control" id="UnitId" />';
    html += '                    <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Unit </label>';
    html += '                    <select id="Unit" class="form-control select2 select2-hidden-accessible text-black-50 font-weight-bold px-3 small pb-4 mb-0" onchange="return cboUnitOnChange();" style="min-width: 100%"></select>';
    html += '                </div>';
    html += '                <div class="col-md-2" style="text-align:center;">';
    html += '                    <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">QTY </label>';
    html += '                    <input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="Quantity" style="min-width: 100%; text-align:center;" onkeypress="return isNumber(event)" placeholder="0" />';
    html += '                </div>';
    html += '                <div class="col-md-2" style="text-align:center;">';
    html += '                    <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Total Cost </label>';
    html += '                    <input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="TotalCost" style="min-width: 100%; text-align:right;" onkeydown="numberWithCommas(this)" placeholder="0.00" />';
    html += '                </div>';
    html += '            </div>';
    html += '            <div class="row">';
    html += '                <div class="col-md-2" style="text-align:center;">';
    html += '                    <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">BAL. QTY. </label>';
    html += '                    <input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="BalQty" style="min-width: 100%; text-align:center;" onkeypress="return isNumber(event)" placeholder="0" />';
    html += '                </div>';
    html += '                <div class="col-md-8">';
    html += '                    <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Balance Remark </label>';
    html += '                    <input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="BalRemark" style="min-width: 100%;" placeholder="Balance Remark" />';
    html += '                </div>';
    html += '                <div class="col-md-2 text-center">';
    html += '                    <br /><button type="button" class="btn btn-light bg-success rounded-pill shadow-sm px-4 mt-4" id="btnAddMaterialItem" onclick="addMaterialItem();"><i class="glyphicon glyphicon-plus-sign mr-2"></i><small class="text-uppercase font-weight-bold"> ADD</small></button>';
    html += '                </div>';
    html += '            </div>';
    html += '        </div>';
    html += '        <hr style="border-color:darkred;" />';
    html += '        <div class="form-group">';
    html += '            <table class="table table-striped table-bordered table-hover  text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="tblMDetails" style="padding-left:10px;padding-right:10px;">';
    html += '                <thead bgcolor="lightblue">';
    html += '                    <tr>';
    html += '                        <th style="text-align:center;display:none;">';
    html += '                            MaterialId';
    html += '                        </th>';
    html += '                        <th style="text-align:center;">';
    html += '                            Material';
    html += '                        </th>';
    html += '                        <th style="text-align:center;display:none;">';
    html += '                            UnitId';
    html += '                        </th>';
    html += '                        <th style="text-align:center;">';
    html += '                            Unit';
    html += '                        </th>';
    html += '                        <th style="text-align:center;">';
    html += '                            Qty';
    html += '                        </th>';
    html += '                        <th style="text-align:center;">';
    html += '                            UCost';
    html += '                        </th>';
    html += '                        <th style="text-align:center;">';
    html += '                            Total Cost';
    html += '                        </th>';
    html += '                        <th style="text-align:center;display:none;">';
    html += '                            Inv. Cost';
    html += '                        </th>';
    html += '                        <th style="text-align:center;display:none;">';
    html += '                            VAT';
    html += '                        </th>';
    html += '                        <th style="text-align:center;">';
    html += '                            Bal.';
    html += '                        </th>';
    html += '                        <th style="text-align:center;">';
    html += '                            Rem.';
    html += '                        </th>';
    html += '                    </tr>';
    html += '                </thead>';
    html += '                <tbody class="tbodydetail"></tbody>';
    html += '            </table>';
    html += '            <div class="form-group">';
    html += '                <div class="row">';
    html += '                   <div class="col-md-offset-6 col-md-3" style="vertical-align:central;">';
    html += '                         <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0" style="color:red;float:right;vertical-align:central;">Received Total Cost </label>';
    html += '                   </div>';
    html += '                   <div class="col-md-3">';
    html += '                         <input type="text" class="form-control text-black-50 font-weight-bold px-3 small pb-4 mb-0" id="ReceivedTotalCost" style="float:right;text-align:right;" disabled="disabled" />';
    html += '                    </div>';
    html += '                </div>';
    html += '            </div>';
    html += '            <hr style="border-color:darkred;" />';
    html += '            <h6 class="text-primary">FOOTER</h6>';
    html += '            <div class="form-group">';
    html += '               <div class="row">';
    html += '                   <div class="col-md-3">';
    html += '                       <input type="hidden" class="form-control" id="ReceivedById" />';
    html += '                       <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Received By: </label>';
    html += '                       <select id="ReceivedBy" class="form-control select2 select2-hidden-accessible text-black-50 font-weight-bold px-3 small pb-4 mb-0" onchange="return cboSig1OnChange();" style="min-width: 100%"></select>';
    html += '                   </div>';
    html += '                   <div class="col-md-3">';
    html += '                       <input type="hidden" class="form-control" id="CheckedById" />';
    html += '                       <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Checked By: </label>';
    html += '                       <select id="CheckedBy" class="form-control select2 select2-hidden-accessible text-black-50 font-weight-bold px-3 small pb-4 mb-0" onchange="return cboSig4OnChange();" style="min-width: 100%"></select>';
    html += '                   </div>';
    html += '                   <div class="col-md-3">';
    html += '                       <input type="hidden" class="form-control" id="NotedById" />';
    html += '                       <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Noted By: </label>';
    html += '                       <select id="NotedBy" class="form-control select2 select2-hidden-accessible text-black-50 font-weight-bold px-3 small pb-4 mb-0" onchange="return cboSig2OnChange();" style="min-width: 100%"></select>';
    html += '                   </div>';
    html += '                   <div class="col-md-3">';
    html += '                       <input type="hidden" class="form-control" id="AuditedById" />';
    html += '                       <label class="text-black-50 font-weight-bold text-uppercase px-3 small pb-4 mb-0">Audited By: </label>';
    html += '                       <select id="AuditedBy" class="form-control select2 select2-hidden-accessible text-black-50 font-weight-bold px-3 small pb-4 mb-0" onchange="return cboSig3OnChange();" style="min-width: 100%"></select>';
    html += '                   </div>';
    html += '               </div>';
    html += '          </div>';
    html += '        </div>';
    html += '    </div>';
    html += '</form>';
    html += '   </div >';
    html += '</div > ';

    $('#frmNewEntry').append(html);

    initDefaultFields();
}

function saveEntry() {
    if (parseFloat($('#ReceivedTotalCost').val().replace(',', '')) == 0) {
        toastr.info('Please fill all required fields.');
        return false;
    }

    //get the checked value of checkbox
    var tsek;
    if ($('#isOld').is(":checked")) {
        tsek = true;
    } else {
        tsek = false;
    }

    var objdata = {
        Id: parseInt(0),
        ReceivedDate: $('#ReceivedDate').val(),
        PreparedById: $('#uid').val(),
        PreparedBy: $('#uname').text,
        ReceivedTotalCost: parseFloat($('#ReceivedTotalCost').val().replace(/,/g, "")),
        ReceivedById: $('#ReceivedById').val(),
        CheckedById: $('#CheckedById').val(),
        NotedById: $('#NotedById').val(),
        AuditedById: $('#AuditedById').val(),
        IsOld: tsek,
        SupplierId: $('#SupplierId').val(),
        PO1: $('#PO1').val() === null ? '' : $('#PO1').val(),
        PO2: $('#PO2').val(),
        PO3: $('#PO3').val(),
        PO4: $('#PO4').val(),
        PO5: $('#PO5').val(),
        SI1: $('#SI1').val(),
        SI2: $('#SI2').val(),
        SI3: $('#SI3').val(),
        SI4: $('#SI4').val(),
        SI5: $('#SI5').val(),
        DR1: $('#DR1').val(),
        DR2: $('#DR2').val(),
        DR3: $('#DR3').val(),
        DR4: $('#DR4').val(),
        DR5: $('#DR5').val(),
        DeliveryDate: $('#drDate').val(),
        Remark: $('#Remark').val()

    };

    if (objdata != null) {
        $.ajax({
            type: "POST",
            url: "/RRStock/AddReceivedHeader/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify(objdata),
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess) {
                    $.ajax({
                        url: "/RRStock/GetLoggedUserMaxRMId/" + parseInt($('#uid').val()),
                        type: "GET",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        success: function (result) {
                            if (parseInt(result.Id) > 0) {

                                addReceivingMaterialDetails(result.Id);
                            }

                        },
                        error: function (errormessage) {
                            //alert(errormessage.responseText);
                            toastr.error(errormessage.responseText);
                        }
                    });
                }
            },
            error: function (errormessage) {
                toastr.error(errormessage.responseText);
            }
        });
    }
}

function cancelEntry() {
    window.location = "/RRStock/RRStock";
}

function view(id) {
    var parent = $('embed#rrpdf').parent();
    var newElement = '<embed src="/RRStock/RRReportView?id=' + parseInt(id) + '"  width="100%" height="700px" type="application/pdf" id="rrpdf">';

    $('embed#rrpdf').remove();
    parent.append(newElement);

    $('#RRModal').modal('show');
}

function initDefaultFields() {
    var now = new Date();

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);

    $('#ReceivedDate').val(today);

    //initialize dropdown lists..
    loadcboMaterials();
    loadcboUnits();
    loadcboSig1();
    loadcboSig2();
    loadcboSig3();
    loadcboSig4();
    loadSuppliers();

    $('#Material').val(0);

    //var recby = "TULABOT, JERRICKO J. ";
    //$("#ReceivedBy option:contains(" + recby + ")").attr('selected', 'selected');


    //$('#NotedBy').val("TAL PLACIDO, RODOLFO R."); //default Rodolfo R. Tal Placido
    //$('#AuditedBy').val("ABOGADO, MELANIE A."); //default Melanie A. Abogado

    $('#ReceivedById').val(332); //default Jerricko J. Tulabot
    $('#NotedById').val(60); //default Allan G. Bermudez
    $('#AuditedById').val(5); //default Melanie A. Abogado
    $('#CheckedById').val(178) //default GUBAC, IVY MARIE D.  


    $('#Unit').val(0);
    $('#Quantity').val("");
    $('#ReceivedTotalCost').val("0.00");
    $('#TotalCost').val("");

    $('.tbodydetail').empty();

    $('#BalQty').val("");
    $('#BalRemark').val("");
}

function loadcboMaterials() {
    $.ajax({
        url: "/RRStock/GetMaterials/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Material').empty();
            $('#Material').val(0);
            $('#Material').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var Desc = result[i].Code + " - " + result[i].Description;
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

function loadcboUnits() {
    $.ajax({
        url: "/RRStock/GetUnits/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Unit').empty();
            $('#Unit').val(0);
            $('#Unit').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var opt = new Option(result[i].Description, result[i].Id);
                $('#Unit').append(opt);
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
        url: "/RRStock/GetEmployees/",
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
            $('#ReceivedBy option[value=332]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig2() {
    $.ajax({
        url: "/RRStock/GetEmployees/",
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
        url: "/RRStock/GetEmployees/",
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
        url: "/RRStock/GetEmployees/",
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
            $('#CheckedBy option[value=78]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function loadSuppliers() {
    $.ajax({
        url: "/RRStock/GetSuppliers/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Supplier').empty();
            $('#Supplier').val(0);
            $('#Supplier').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var supplier = result[i].Name;
                var opt = new Option(supplier, result[i].Id);
                $('#Supplier').append(opt);
            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });
    return false;
}

function cboUnitOnChange() {
    var unitid = $('#Unit').val();
    $('#UnitId').val(unitid);
}

function cboMaterialOnChange() {
    var matid = $('#Material').val();
    $('#MaterialId').val(matid);
}

function cboSig1OnChange() {
    var sigid1 = $('#ReceivedBy').val();
    $('#ReceivedById').val(sigid1);
}

function cboSig2OnChange() {
    var sigid2 = $('#NotedBy').val();
    $('#NotedById').val(sigid2);
}

function cboSig3OnChange() {
    var sigid3 = $('#AuditedBy').val();
    $('#AuditedById').val(sigid3);
}

function cboSig4OnChange() {
    var sigid4 = $('#CheckedBy').val();
    $('#CheckedById').val(sigid4);
}

function cboSupplierOnChange() {
    var suppid = $('#Supplier').val();
    $('#SupplierId').val(suppid);
}

function addMaterialItem() {

    //for validation-----------------------------------------------------------
    if (parseInt($('#Material').val()) == 0) {
        //alert('Please select Material.');
        //swal({
        //    title: "Invalid!",
        //    text: "Please select Material.",
        //    type: "warning",
        //    showCancelButton: false,
        //    confirmButtonClass: "btn-warning",
        //    confirmButtonText: "OK",
        //    closeOnConfirm: true
        //});
        toastr.info('Please select Material.');
        return false;
    }
    if (parseInt($('#Unit').val()) == 0) {

        toastr.info('Please select Unit.');
        return false;
    }
    if (parseInt($('#Quantity').val()) == 0 || $('#Quantity').val() == '') {

        toastr.info('Quantity should be greater than zero (0).');

        return false;
    }
    if (parseFloat($('#TotalCost').val()) == 0 || $('#TotalCost').val() == '') {
      
        toastr.info('Total Cost should be greater than zero (0).');

        return false;
    }
    //end of validation---------------------------------------------------------------

    //var ttlcost = parseInt($('#Quantity').val()) * parseFloat($('#UnitCost').val());
    var selMaterial = $("#Material option:selected").html();
    var selUnit = $("#Unit option:selected").html();
    var totalcost = $('#TotalCost').val().replace(/,/g, "");
    var invcost = parseFloat(totalcost) / (1.12);
    var vat = parseFloat(invcost) * 0.12;
    var unitcost = parseFloat(invcost) / parseFloat($('#Quantity').val());
    var balqty = 0;

    if ($('#BalQty').val() == '')
        balqty = 0;
    else
        balqty = parseInt($('#BalQty').val());

    var balrem = $('#BalRemark').val();


    var html = '';
    html += '<tr style="background-color: white;">';
    html += '<td style="display:none;">' + $('#MaterialId').val() + '</td>';
    html += '<td>' + selMaterial + '</td>';
    html += '<td style="display:none;">' + $('#UnitId').val() + '</td>';
    html += '<td>' + selUnit + '</td>';
    html += '<td style="text-align:center;">' + $('#Quantity').val() + '</td>';
    html += '<td style="text-align:right;">' + Intl.NumberFormat().format(unitcost) + '</td>';
    html += '<td style="text-align:right;">' + Intl.NumberFormat().format(totalcost) + '</td>';
    html += '<td style="text-align:right;display:none;">' + Intl.NumberFormat().format(invcost) + '</td>';
    html += '<td style="text-align:right;display:none;">' + Intl.NumberFormat().format(vat) + '</td>';
    html += '<td style="text-align:center;">' + balqty + '</td>';
    html += '<td>' + balrem + '</td>';
    html += '</tr>';
    $('.tbodydetail').append(html);

    //reset required fields after adding the selected material
    $('#Material').val(0);
    //$('#Unit').val(0);
    $('#Quantity').val("");
    $('#TotalCost').val("");

    $('#BalQty').val("");
    $('#BalRemark').val("");

    var rttlcost = getReceivedTotalCost();

    $('#ReceivedTotalCost').val(Intl.NumberFormat().format(rttlcost));
}

function getReceivedTotalCost() {
    var ttlcost = 0;

    var table = $(".tbodydetail");
    table.find('tr').each(function (i) {
        var $tds = $(this).find('td');
        var tcost = $tds.eq(6).text().replace(/,/g, "");

        ttlcost = parseFloat(ttlcost) + parseFloat(tcost.replace(/,/g, ""));

    });

    return ttlcost;
}

function addReceivingMaterialDetails(id) {

    //kunin number of rows in table
    var cnt = $('#tblMDetails tbody tr').length;
    //kunin lahat ng rows
    var tblrows = $('#tblMDetails tbody tr');

    var arrayData = new Array();

    //dito iyon magloloop per item (details)
    for (var i = 0; i < cnt; i++) {

        var $tds = tblrows[i].cells;

        var detailobj = {
            RRHeaderId: id,
            MaterialId: parseInt($tds[0].innerText),
            Quantity: parseInt($tds[4].innerText),
            UnitId: parseInt($tds[2].innerText),
            UnitCost: parseFloat($tds[5].innerText.replace(/,/g, "")),
            TotalCost: parseFloat($tds[6].innerText.replace(/,/g, "")),
            InventorialCost: parseFloat($tds[7].innerText.replace(/,/g, "")),
            VAT: parseFloat($tds[8].innerText.replace(/,/g, "")),
            OnHand: parseInt($tds[4].innerText),
            BalanceQty: parseInt($tds[9].innerText),
            Remark: $tds[10].innerText
        };

        arrayData.push(detailobj);
    }

    $.ajax({
        url: "/RRStock/AddReceiveMaterialDetail/",
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
                    window.location = "/RRStock/RRStock";
                }
            );
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);

        }
    });
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
function isNumber(evt) {
    var ch = String.fromCharCode(evt.which);
    if (!(/[0-9]/.test(ch))) {
        evt.preventDefault();
    }
}

function validateFloatKeyPress(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}

function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}

function numberWithCommas(x) {
    setTimeout(function () {
        if (x.value.lastIndexOf(".") != x.value.length - 1) {
            var dec = x.value.split(".", 2);

            var a;

            a = dec[0].replace(/,/g, "");

            //if (dec.length == 2)

            //else
            //    a = dec[0].replace(/,/g, "");

            var nf = new Intl.NumberFormat();

            if (dec.length == 2)
                x.value = nf.format(a) + "." + dec[1];
            else
                x.value = nf.format(a);
        } else {
            return false;
        }
    }, 0);
}
//-----------------------------------------------------------------------------------



var waitingDialog = waitingDialog || (function ($) {
    'use strict';

    // Creating modal dialog's DOM
    var $dialog = $(
        '<div class="modal fade bd-example-modal-sm" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
        '<div class="modal-dialog modal-sm">' +
        '<div class="modal-content">' +
        '<div class="modal-header"><h5 style="margin:0;"></h5></div>' +
        '<div class="modal-body">' +
        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
        '</div>' +
        '</div></div></div>');

    return {
        /**
         * Opens our dialog
         * @param message Custom message
         * @param options Custom options:
         * 				  options.dialogSize - bootstrap postfix for dialog size, e.g. "sm", "m";
         * 				  options.progressType - bootstrap postfix for progress bar type, e.g. "success", "warning".
         */
        show: function (message, options) {
            // Assigning defaults
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Loading';
            }
            var settings = $.extend({
                dialogSize: 'sm',
                progressType: '',
                onHide: null // This callback runs after the dialog was hidden
            }, options);

            // Configuring dialog
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h5').text(message);
            // Adding callbacks
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }
            // Opening dialog
            $dialog.modal();
        },
        /**
         * Closes dialog
         */
        hide: function () {
            $dialog.modal('hide');
        }
    };

})(jQuery);