$(document).ready(function () {

    //Item Details Add button click event
    $('#add').click(function () {
        //validation and add order items
        var isAllValid = true;
        
        //if (!($('#auditCode').val().trim() != '')) {
        //    isAllValid = false;
        //    $('#auditCode').siblings('span.error').css('visibility', 'visible');
        //} else {
        //    $('#auditCode').siblings('span.error').css('visibility', 'hidden');
        //}

        if (!($('#productSerialNo').val().trim() != '')) {
            isAllValid = false;
            $('#productSerialNo').siblings('span.error').css('visibility', 'visible');
        } else {
            $('#productSerialNo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#warrantPeriod').val().trim() == '') {
            $('#warrantPeriod').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#warrantPeriod').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#itemCName').val() == "0") {
            isAllValid = false;
            $('#itemCName').siblings('span.error').css('visibility', 'visible');
        } else {
            $('#itemCName').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#itemModel').val() == "0") {
            isAllValid = false;
            $('#itemModel').siblings('span.error').css('visibility', 'visible');
        } else {
            $('#itemModel').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#unitPrice').val().trim() != '' && (parseFloat($('#unitPrice').val()) || 0))) {
            isAllValid = false;
            $('#unitPrice').siblings('span.error').css('visibility', 'visible');
        } else {
            $('#unitPrice').siblings('span.error').css('visibility', 'hidden');
        }


        if (!($('#remarks').val().trim() != '')) {
            isAllValid = false;
            $('#remarks').siblings('span.error').css('visibility', 'visible');
        } else {
            $('#remarks').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var $newRow = $('#mainCheckInDetailsrow').clone().removeAttr('id');
            
            $('.itemCName', $newRow).val($('#itemCName').val());
            $('.itemModel', $newRow).val($('#itemModel').val());
            

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');
            $('#productSerialNo,#warrantPeriod,#itemCName,#itemModel,#unitPrice,#remarks', $newRow).attr('disabled', 'disabled');

            //remove id attribute from new clone row
            //$('#auditCode,#deviceCode,#warrantExpiredDate,#productSerialNo,#itemCName,#itemModel,#itemSize,#itemBrand,#unitPrice,#itemStatus,#remarks,#add', $newRow).removeAttr('id');
            $('#productSerialNo,#warrantPeriod,#itemCName,#itemModel,#unitPrice,#remarks,#add', $newRow).removeAttr('id');
            //$('#auditCode,#productSerialNo,#unitPrice,#remarks', $newRow).attr('disabled','');
            $('span.error', $newRow).remove();
            $('#Status', $newRow).remove();
            //append clone row
            $('#checkIndetailsStore').append($newRow).addClass('disabled');

            //clear select data
            $('#itemCName,#itemModel').val('0');
            //$('#auditCode,#deviceCode,#warrantExpiredDate,#productSerialNo,#unitPrice,#remarks').val('');
            $('#warrantPeriod,#productSerialNo,#unitPrice,#remarks').val('');
            //$("#auditCode").css("border-color", "silver");
            //$("#add").attr('disabled','true');
            $('#orderItemError').empty();
        }

    });

    //remove button click event
    $('#checkIndetailsStore').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });



    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var checkInDetailslist = [];
        var errorItemCount = 0;
        var auditCode = 1;
        var countCheckId = 1;
        var countCheckDetailsId = 1;
        var countCpuId = 1;
        var countDevicId = 1;
        var radioButtons = document.getElementsByName("CheckType");
        var cpuSelectedValue = "no";
        for (var x = 0; x < radioButtons.length; x ++) {
            if (radioButtons[x].checked) {
                cpuSelectedValue = radioButtons[x].value;
            }
        }
        //var bytesToSend = [253, 0, 128, 1],
        //    bytesArray = new Uint8Array(bytesToSend);


        $('#checkIndetailsStore tbody tr').each(function (index, ele) {
            if (
                //($('.auditCode', this).val()) == '' ||
                //(parseInt($('.deviceCode', this).val()) || 0) == 0 ||
                ($('.warrantPeriod', this).val()) == '' ||
                ($('.productSerialNo', this).val()) == '' ||
                $('select.itemCName', this).val() == "0" ||
                $('select.itemModel', this).val() == "0" ||
                //$('select.itemSize', this).val() == "0" ||
                //$('select.itemBrand', this).val() == "0" ||
                (parseFloat($('.unitPrice', this).val()) || 0) == 0 ||
                ($('.productSerialNo', this).val()) == '' 
                //$('select.itemStatus', this).val() == "0" ||
                
            ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var checkinDetailsItem = {
                    CheckInDetailId: countCheckDetailsId++,
                    QueryId: 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g,
                        function(c) {
                            var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
                            return v.toString(16);
                        }),
                    AuditCode: auditCode++,
                    CheckInId: "C00000001",
                    ItemId: $('select.itemCName', this).val(),
                    Unitprice: parseFloat($(".unitPrice", this).val()),
                    WarrantyExpireDate: "1/1/2022",
                    ProductSerialNo: $(".productSerialNo", this).val(),
                    ItemStatus: "New",
                    CurrentStatus: "Available",
                    Remarks: $(".remarks", this).val(),
                    PostedBy: "P",
                    PostedIp: "1",
                    PostedDate: new Date(),
                    UpdatedBy: "U",
                    UpdatedIp: "1",
                    UpdatedDate: new Date(),
                    CpuId: countCpuId++,
                    DeviceId: countDevicId++,
                    QRCodeImgPath: "N/A",
                    QRImage: [],
                    WarrentyDuration: $(".warrantPeriod", this).val(),
                    ItemDetailsId: $('select.itemModel', this).val(),
                  
                }
                checkInDetailslist.push(checkinDetailsItem);
            }
        });

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in order item list.");
            isAllValid = false;
        }

        if (checkInDetailslist.length == 0) {
            $('#orderItemError').text('At least 1 order item required.');
            isAllValid = false;
        }

        
        if ($('#itemCategory').val() == "0") {
            isAllValid = false;
            $('#itemCategory').siblings('span.error').css('visibility', 'visible');
        } else {
            $('#itemCategory').siblings('span.error').css('visibility', 'hidden');
        }
        if ($('#itemSupplier').val() == "0") {
            isAllValid = false;
            $('#itemSupplier').siblings('span.error').css('visibility', 'visible');
        } else {
            $('#itemSupplier').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#invoiceNo').val().trim() == '') {
            $('#invoiceNo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#invoiceNo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#invoicedatepicker').val().trim() == '') {
            $('#invoicedatepicker').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#invoicedatepicker').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#productPrice').val().trim() == '') {
            $('#productPrice').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#productPrice').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#receiptNo').val().trim() == '') {
            $('#receiptNo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#receiptNo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#workOrderNo').val().trim() == '') {
            $('#workOrderNo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#workOrderNo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#purchaseDate').val().trim() == '') {
            $('#purchaseDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#purchaseDate').siblings('span.error').css('visibility', 'hidden');
        }
        
        
        
        if (isAllValid) {
            var data = {
                CheckInId: countCheckId,
                QueryId: 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
                    return v.toString(16);
                }),
                CategoryId: document.getElementById("itemCategory").value,
                InvoiceNo: $('#invoiceNo').val().trim(),
                PurchaseDate: $('#purchaseDate').val().trim(),
                TotalBillAmount: $('#productPrice').val().trim(),
                Comment: cpuSelectedValue,
                PostedBy: "P",
                PostedIp: "1",
                PostedDate: new Date(),
                UpdatedBy: "U",
                UpdatedIp: "2",
                UpdatedDate: new Date(),
                SupplierId: document.getElementById("itemSupplier").value,
                InvoiceDate: $('#invoicedatepicker').val().trim(),
                PurchaseRequestNo: $('#receiptNo').val().trim(),
                WorkOrderNo: $('#workOrderNo').val().trim(),
                CheckInDetail: checkInDetailslist
            }

            $(this).val('Save.');

            $.ajax({
                type: 'POST',
                url: '../checkInDetailsMultiple/SaveCheckInDetails',
                //url: '@Url.Action("CheckInDetailsMultiple", "SaveCheckInDetails")',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        toastr.success('Successfully saved Item');
                        //$(this).val('Save Item');
                        //here we will clear the form
                        checkInDetailslist = [];
                        $('#invoiceNo,#purchaseDate,#productPrice,#receiptNo,#purchaseBy,#comment').val('');
                        $('#itemCategory,#itemSupplier').val('0');
                        $('#checkIndetailsStore').empty();

                    }
                    else {
                        toastr.error('Something unexpected happened');
                    }
                    $('#submit').text('Save');
                },
                error: function (error) {
                    console.log(error.statusText);
                    toastr.error(error.statusText);
                    $('#submit').text('Save');
                }
            });
        }

    });

});