intSoftApp.factory("intSoftModalService",
[
    "$uibModal", "intSoftValues",
    function ($uibModal, intSoftValues) {
        var showModal = function (templateUrl, controller, modalScope) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: templateUrl,
                controller: controller,
                resolve: {
                    modalScope: modalScope
                }
            });
            modalInstance.result.then(function (modalResult) {
                if (modalScope.onClose) {
                    modalScope.onClose(modalResult);
                }
            },
                function (modalResult) {
                    if (modalScope.onDismiss) {
                        modalScope.onDismiss(modalResult);
                    }
                });
        }

        var showChangeEmailModal = function (modalScope) {
            showModal(intSoftValues.templatesUrl + "templates.changeEmail.html", "intSoftAccountChangeEmailController", modalScope);
        }

        var showChangePasswordModal = function (modalScope) {
            showModal(intSoftValues.templatesUrl + "templates.changePassword.html", "intSoftAccountChangePasswordController", modalScope);
        }

        var showChangePhoneNumberModal = function (modalScope) {
            showModal(intSoftValues.templatesUrl + "templates.changePhoneNumber.html", "intSoftAccountChangePhoneNumberController", modalScope);
        }

        return {
            showModal: showModal,
            showChangeEmailModal: showChangeEmailModal,
            showChangePasswordModal: showChangePasswordModal,
            showChangePhoneNumberModal: showChangePhoneNumberModal
        };
    }
]);