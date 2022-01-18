"use strict";
var tl, el, il, blockUI;
var KTSigninGeneral = function () {
	return {
		init: function () {
			tl = document.querySelector("#crudUser"), el = document.querySelector("#submitUser"), il = FormValidation.formValidation(tl, {
				fields: {
					UserName: {
						validators: {
							notEmpty: {
								message: "Nama user tidak boleh kosong"
							},
							regexp: {
								regexp: /^\S*$/,
								message: 'Nama user tidak boleh ada spasi',
							}
						}
					},
					Email: {
						validators: {
							notEmpty: {
								message: "Email tidak boleh kosong"
							},
							emailAddress: {
								message: 'Email address tidk valid'
							}
						}
					},
					CurrentPassword: {
						validators: {
							notEmpty: {
								message: "Password lama tidak boleh kosong"
							}
						}
					},
					Password: {
						validators: {
							notEmpty: {
								message: "Password baru tidak boleh kosong"
							}
						}
					},
					Role: {
						validators: {
							notEmpty: {
								message: "Role tidak boleh kosong"
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

	$("#crudUser").submit(function (e) {
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