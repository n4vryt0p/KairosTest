"use strict";
var tl, el, il;
var KTSigninGeneral = function () {
	return {
		init: function () {
			tl = document.querySelector("#account"), el = document.querySelector("#kt_sign_in_submit"), il = FormValidation.formValidation(tl, {
				fields: {
					UserName: {
						validators: {
							notEmpty: {
								message: "Username is required"
							}
						}
					},
					Password: {
						validators: {
							notEmpty: {
								message: "The password is required"
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
KTUtil.onDOMContentLoaded((function () {
	KTSigninGeneral.init();

	$("#account").submit(function (e) {
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
						window.location.href = "./";
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

	//e.addEventListener("click", (function (n) {
	//	n.preventDefault(), i.validate().then((function (i) {
	//		"Valid" == i ? (e.setAttribute("data-kt-indicator", "on"), e.disabled = !0, setTimeout((function () {
	//			e.removeAttribute("data-kt-indicator"), e.disabled = !1, Swal.fire({
	//				text: "You have successfully logged in!",
	//				icon: "success",
	//				buttonsStyling: !1,
	//				confirmButtonText: "Ok, got it!",
	//				customClass: {
	//					confirmButton: "btn btn-primary"
	//				}
	//			}).then((function (e) {
	//				e.isConfirmed
	//			}))
	//		}), 2e3)) : Swal.fire({
	//			text: "Sorry, looks like there are some errors detected, please try again.",
	//			icon: "error",
	//			buttonsStyling: !1,
	//			confirmButtonText: "Ok, got it!",
	//			customClass: {
	//				confirmButton: "btn btn-primary"
	//			}
	//		}).then((function (e) {
	//			e.isConfirmed
	//		}))
	//	}))
	//}));
}));