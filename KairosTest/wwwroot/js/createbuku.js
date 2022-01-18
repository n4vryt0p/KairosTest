"use strict";
var tl, el, il, blockUI;
var KTSigninGeneral = function () {
	return {
		init: function () {
			tl = document.querySelector("#crudBuku"), el = document.querySelector("#submitBuku"), il = FormValidation.formValidation(tl, {
				fields: {
					JudulBuku: {
						validators: {
							notEmpty: {
								message: "Judul Buku tidak boleh kosong"
							}
						}
					},
					Pengarang: {
						validators: {
							notEmpty: {
								message: "Pengarang tidak boleh kosong"
							}
						}
					},
					JenisBuku: {
						validators: {
							notEmpty: {
								message: "Jenis Buku tidak boleh kosong"
							}
						}
					},
					HargaSewa: {
						validators: {
							notEmpty: {
								message: "Harga Sewa tidak boleh kosong"
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
			})
		}
	}
}();
// On document ready
KTUtil.onDOMContentLoaded(function () {
	KTSigninGeneral.init()

	$("#crudBuku").submit(function (e) {
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
						Swal.fire({
							text: "Sukses, data buku tersimpan",
							icon: "success",
							buttonsStyling: !1,
							confirmButtonText: "Ok!",
							customClass: {
								confirmButton: "btn btn-primary"
							}
						}).then((function (e) {
							window.location.href = "./Bukus";
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