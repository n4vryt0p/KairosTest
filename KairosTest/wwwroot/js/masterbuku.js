"use strict";
var KTDatatablesServerSide = function () {
    // Shared variables
    var dt, table;
    // Private functions
    var initDatatable = function () {
        dt = $("#kt_datatable_buku").DataTable({
            searchDelay: 500,
            processing: true,
            deferRender: true
        });
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = function () {
        const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            dt.search(e.target.value).draw();
        });
    }
    return {
        init: function () {
            initDatatable();
            handleSearchDatatable();
        }
    }
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesServerSide.init();
});