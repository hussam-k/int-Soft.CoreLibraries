intSoftApp.controller("intSoftAccountChangePasswordController", [
    "$scope", "intSoftNotificationService", "intSoftLoginService", "modalScope", "$uibModalInstance",
    function ($scope, intSoftNotificationService, intSoftLoginService, modalScope, $uibModalInstance) {

        // Methods
        var onSuccess = function (response) {
            $scope.isLoading = false;
            if (modalScope.onOk) {
                modalScope.onOk();
            }
            $uibModalInstance.close();
            intSoftNotificationService.notifySuccess(response.data);
        };

        var onError = function (response) {
            $scope.isLoading = false;
            intSoftNotificationService.notifyError(response.data.errorMessage);
        };

        $scope.cancel = function () {
            $uibModalInstance.close();
        };

        $scope.submit = function () {
            $scope.changePasswordForm = angular.element(document.querySelector("#changePasswordForm"))
                .controller("form");
            if ($scope.changePasswordForm.$invalid) {
                angular.forEach($scope.changePasswordForm,
                    function (value) {
                        if (typeof value === "object" && value.hasOwnProperty("$modelValue")) {
                            value.$setTouched();
                        }
                    });
                return;
            }
            $scope.isLoading = true;
            intSoftLoginService.changePassword($scope.model, modalScope.antiForgeryToken)
                .then(onSuccess, onError);
        };

        $scope.checkPasswordAvailability = function () {
            if (!$scope.model.password) return;
            $scope.isLoading = true;
            $scope.form = angular.element(document.querySelector("#changePasswordForm"))
                .controller("form");
            intSoftLoginService.checkPasswordAvailability($scope.model.password, modalScope.antiForgeryToken)
                .then(function (response) {
                    if (response && response.data === true) {
                        $scope.form.password.$setValidity("invalidPassword", true);
                        $scope.form.password.$setTouched();
                    }
                    $scope.isLoading = false;
                },
                    function (response) {
                        $scope.form.password.$setValidity("invalidPassword", false);
                        $scope.form.password.$setTouched();
                        $scope.invalidPasswordMessage = response.data.errorMessage;
                        $scope.isLoading = false;
                    });
        };

        // initializations
        $scope.model = {
            currentPassword: "",
            password: "",
            confirmPassword: ""
        };
    }
]);