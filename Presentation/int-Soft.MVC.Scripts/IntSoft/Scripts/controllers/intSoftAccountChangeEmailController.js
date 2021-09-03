intSoftApp.controller("intSoftAccountChangeEmailController", [
    "$scope", "intSoftNotificationService", "intSoftLoginService", "modalScope", "$uibModalInstance",
    function ($scope, intSoftNotificationService, intSoftLoginService, modalScope, $uibModalInstance) {

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

        // Scope Methods
        $scope.submit = function () {
            $scope.changeEmailForm = angular.element(document.querySelector("#changeEmailForm"))
                .controller("form");
            if ($scope.changeEmailForm.$invalid) {
                angular.forEach($scope.changeEmailForm,
                    function (value) {
                        if (typeof value === "object" && value.hasOwnProperty("$modelValue")) {
                            value.$setTouched();
                        }
                    });
                return;
            }
            $scope.isLoading = true;
            intSoftLoginService.changeEmail($scope.model, modalScope.antiForgeryToken)
                .then(onSuccess, onError);
        };

        $scope.cancel = function () {
            $uibModalInstance.close();
        };

        $scope.checkEmailAvailability = function () {
            if (!$scope.model.email || !$scope.model.email.includes("@") || !$scope.model.email.includes(".")) return;
            $scope.isLoading = true;
            $scope.form = angular.element(document.querySelector("#changeEmailForm")).controller("form");
            intSoftLoginService.checkEmailAvailability($scope.model.email, modalScope.antiForgeryToken)
                .then(function (response) {
                    if (response && response.data === true) {
                        $scope.form.email.$setValidity("invalid", true);
                        $scope.form.email.$setTouched();
                    }
                    $scope.isLoading = false;
                },
                    function () {
                        $scope.form.email.$setValidity("invalid", false);
                        $scope.form.email.$setTouched();
                        $scope.isLoading = false;
                    });
        };

        // initializations
        $scope.model = {
            password: "",
            email: ""
        };

    }
]);