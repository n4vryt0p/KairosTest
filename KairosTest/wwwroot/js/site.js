"use strict";
var oneDay = 24 * 60 * 60 * 1000, diffDays = 0, tl, el, il;
// Class definition
var KTDatatablesServerSide = function () {
    // Shared variables
    var dt, table;
    // Private functions
    var initDatatable = function () {
        dt = $("#kt_datatable_example_1").DataTable({
            searchDelay: 500,
            processing: true,
            serverSide: true,
            order: [[1, 'desc']],
            stateSave: true,
            //responsive: true,
            ajax: {
                url: "/sewa",
                contentType: "application/json",
                type: "POST",
                data: function (d) {
                    return JSON.stringify(d);
                }
            },
            columns: [
                { data: 'id' },
                { data: 'judulBuku' },
                { data: 'pengarang' },
                { data: 'jenisBuku' },
                { data: 'hargaSewa' },
                { data: 'jumlahHari' },
                { data: 'totalSewa' },
            ],
            columnDefs: [
                {
                    targets: [0],
                    visible: false
                }
            ],
            footerCallback: function (row, data, start, end, display) {
                var api = this.api(),
                    data;
                // Remove the formatting to get integer data for summation
                var intVal = function (i) {
                    return typeof i === "string" ?
                        i.replace(/[\$,]/g, "") * 1 :
                        typeof i === "number" ?
                            i : 0;
                };
                // Total over all pages
                var total = api
                    .column(6)
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);
                //// Total over this page
                //var pageTotal = api
                //    .column(4, {
                //        page: "current"
                //    })
                //    .data()
                //    .reduce(function (a, b) {
                //        return intVal(a) + intVal(b);
                //    }, 0);
                // Update footer
                $(api.column(4).footer()).html(
                    "Rp. " + total
                );
            }
        });
        table = dt.$;
        // Re-init functions on every table re-draw -- more info: https://datatables.net/reference/event/draw
        dt.on('draw', function () {
            KTMenu.createInstances();
        });
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = function () {
        const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            dt.search(e.target.value).draw();
        });
    }

    var alidate = function () {
        tl = document.querySelector("#kt_modal_new_target_form");
        el = document.querySelector("#submitBuku");
        il = FormValidation.formValidation(tl, {
            fields: {
                judulBuku: {
                    validators: {
                        notEmpty: {
                            message: "Judul Buku tidak boleh kosong"
                        }
                    }
                },
                mulaiPinjaman: {
                    validators: {
                        notEmpty: {
                            message: "Tanggal Peminjaman tidak boleh kosong"
                        }
                    }
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger,
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: ".fv-row"
                })
            }
        });
    }
    return {
        init: function () {
            initDatatable();
            handleSearchDatatable();
            alidate();
        }
    }
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesServerSide.init();
    $('#judulBuku').on('select2:select', function (e) {
        $('#pengarang').val(e.params.data.pengarang);
        $('#jenisBuku').val(e.params.data.jenisBuku);
        $('#harga').val(e.params.data.hargaSewa);
        $("[name='BukuId']").val(e.params.data.id);
        if (diffDays > 0) {
            const totharg = e.params.data.hargaSewa * diffDays;
            $('#totalSewa').val(totharg);
        }
    });
    $('#judulBuku').on('select2:unselect', function (e) {
        var elements = document.getElementsByTagName("input");
        for (var ii = 0; ii < elements.length; ii++) {
            if (elements[ii].type == "text") {
                elements[ii].value = "";
            }
        }
        diffDays = 0;
        $("[name='BukuId']").val(null);
        $("[name='MulaiSewa']").val(null);
        $("[name='SelesaiSewa']").val(null);
    });
    $("#mulaiPinjaman").flatpickr({
        minDate: "today",
        mode: "range",
        altInput: true,
        altFormat: "F j, Y",
        dateFormat: "Y-m-d",
        onValueUpdate: function (selectedDates, dateStr, instance) {
            let harg = $('#harga').val();
            if (selectedDates.length > 1 && harg > 0) {
                diffDays = Math.round(Math.abs((selectedDates[1] - selectedDates[0]) / oneDay));
                const totharg = harg * diffDays;
                $('#totalSewa').val(totharg);
                const jsonDate = selectedDates[0].toJSON();
                $("[name='MulaiSewa']").val(jsonDate);
                const jsonDate2 = selectedDates[1].toJSON();
                $("[name='SelesaiSewa']").val(jsonDate2);
            }
        }
    });

    $("#crudBukus").submit(function (e) {
        e = e || window.event;
        e.preventDefault(); // avoid to execute the actual submit of the form.
        var form = $(this);
        var actionUrl = form.attr('action');
        el.setAttribute("data-kt-indicator", "on");
        el.disabled = !0;
        il.validate().then(function (result) {
            if (result === "Valid") {
                $.ajax({
                    type: "POST",
                    url: actionUrl,
                    data: form.serialize(), // serializes the form's elements.
                    success: function (data) {
                        $("#kt_modal_2").modal("hide");
                        Swal.fire({
                            text: "Sukses, data penyewaan tersimpan",
                            icon: "success",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then((function (e) {
                            el.removeAttribute("data-kt-indicator");
                            el.disabled = !1;
                        }));
                    },
                    error: function (data) {
                        Swal.fire({
                            text: "Server eror, Silahkan hubungi IT",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then((function (e) {
                            el.removeAttribute("data-kt-indicator");
                            el.disabled = !1;
                        }));
                    }
                });
            } else {
                Swal.fire({
                    text: "Eror, silahkan di cek kembali",
                    icon: "error",
                    buttonsStyling: !1,
                    confirmButtonText: "Ok!",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                }).then((function (e) {
                    el.removeAttribute("data-kt-indicator");
                    el.disabled = !1;
                }));
            }
        });
    });
});

function editCat(e) {
    e = e || window.event;
    e.preventDefault();
    $.ajax({
        type: 'GET',
        url: '/BukuDdl',
        dataType: 'json',
        contentType: 'application/json',
        headers: {
            'Access-Control-Allow-Origin': '*',
        },
        success: function (datas) {
            let datax = $.map(datas, function (obj) {
                obj.text = obj.judulBuku || obj.JudulBuku;
                obj.id = obj.id || obj.Id;
                return obj;
            });
            $('#judulBuku').empty();
            $('#judulBuku').select2({
                placeholder: 'Pilih Buku...',
                allowClear: true,
                data: datax
            });
            $('#judulBuku').val(null).trigger("change");
            $("#kt_modal_2").modal("show");
        },
        error: function (request) {
            $('#judulBuku').empty();
            $('#judulBuku').val(null).trigger("change");
        }
    });
}