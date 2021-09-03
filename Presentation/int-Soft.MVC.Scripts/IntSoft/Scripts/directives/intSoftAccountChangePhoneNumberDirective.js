intSoftApp.directive("intSoftAccountChangePhoneNumber",
[
    "intSoftLoginService", "intSoftNotificationService", "intSoftValues", "$state", "$uibModalStack",
    function (intSoftLoginService, intSoftNotificationService, intSoftValues, $state, $uibModalStack) {
        return {
            restrict: "A",
            replace: true,
            scope: {
                verifyPhoneNumberStateName: "=",
                newPhoneNumberPlaceHolder: "=",
                newPhoneNumberMask: "=",
                newPhoneNumberMaxLength: "=",
                passwordPlaceHolder: "=",
                logoIconSource: "=",
                antiForgeryToken: "="
            },
            templateUrl: function (tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.changePhoneNumber.html";
            },
            controller: function ($scope) {
                // Methods
                var onError = function (response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifyError(response.data.errorMessage);
                };

                var confirmPhoneNumber = function (code) {
                    $scope.model.code = code;
                    intSoftLoginService.confirmPhoneNumber($scope.model, $scope.antiForgeryToken)
                        .then(function (response) {
                            $scope.isLoading = false;
                            $uibModalStack.dismissAll();
                            intSoftNotificationService.notifySuccess(response.data);
                        },
                            onError);
                }
                var phoneNumberVerificationCanceled = function () {
                    $uibModalStack.dismissAll();
                    console.log("Verification Canceled");
                }

                var onSuccess = function (response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifySuccess(response.data);
                    $state.go($scope.verifyPhoneNumberStateName, {
                        phoneNumber: $scope.model.phoneNumber,
                        callback: confirmPhoneNumber,
                        onCancelVerification: phoneNumberVerificationCanceled
                    });
                };

                // Scope Methods

                $scope.submit = function () {
                    $scope.changePhoneNumberForm = angular.element(document.querySelector("#changePhoneNumberForm"))
                        .controller("form");
                    if ($scope.changePhoneNumberForm.$invalid) {
                        angular.forEach($scope.changePhoneNumberForm,
                            function (value) {
                                if (typeof value === "object" && value.hasOwnProperty("$modelValue")) {
                                    value.$setTouched();
                                }
                            });
                        return;
                    }
                    $scope.isLoading = true;
                    intSoftLoginService.changePhoneNumber($scope.model, $scope.antiForgeryToken)
                        .then(onSuccess, onError);
                };

                // initializations
                $scope.model = {
                    username: "",
                    password: "",
                    phoneNumber: ""
                };
                $scope.currentSkin = intSoftValues.theme;
            }
        };
    }
]);