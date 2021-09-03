intSoftApp.controller("intSoftAccountChangePhoneNumberController", [
    "$scope", "intSoftNotificationService", "intSoftLoginService", "modalScope", "$uibModalInstance", "intSoftValues", "$interval",
    function ($scope, intSoftNotificationService, intSoftLoginService, modalScope, $uibModalInstance, intSoftValues, $interval) {
        // Methods
        var onError = function (response) {
            $scope.isLoading = false;
            intSoftNotificationService.notifyError(response.data.errorMessage);
        };

        $scope.cancel = function () {
            $uibModalInstance.close();
        }

        var onSuccessfulPhoneNumberChanged = function () {
            $scope.isLoading = false;
            if (modalScope.onPhoneChanged) {
                modalScope.onPhoneChanged();
            }
            $uibModalInstance.close();
        };

        var onVerificationCodeSent = function (response) {
            $scope.codeSent = true;
            console.log(response);
            $interval(function () {
                if ($scope.breakTimer && $scope.breakTimer === true) return;
                if ($scope.timer.getMinutes() > 0 || $scope.timer.getSeconds() > 0) {
                    if ($scope.timer.getSeconds() === 0) {
                        $scope.timer.setMinutes($scope.timer.getMinutes() - 1);
                        $scope.timer.setSeconds(59);
                    } else {
                        $scope.timer.setSeconds($scope.timer.getSeconds() - 1);
                    }
                } else {
                    $scope.canResend = true;
                }
            },
                1000);
        };

        // Scope Methods
        $scope.sendVerificationCode = function () {
            $scope.canResend = false;
            $scope.timer = new Date();
            $scope.timer.setMinutes(intSoftValues.phoneVerificationCodeResendInterval);
            $scope.timer.setSeconds(0);
            intSoftLoginService.sendPhoneVerification($scope.model.phoneNumber, modalScope.antiForgeryToken)
                .then(onVerificationCodeSent, onError);
        }

        $scope.checkPhoneVerificationCode = function () {
            if ($scope.model.verificationCode && $scope.model.verificationCode.length === $scope.codeLength) {
                intSoftLoginService.checkPhoneVerificationCodeValidity($scope.model.verificationCode,
                        $scope.model.phoneNumber,
                        modalScope.antiForgeryToken)
                    .then(function (response) {
                        if (response && response.data === false) {
                            $scope.invalidCode = true;
                            $scope.validCode = false;
                        } else {
                            $scope.invalidCode = false;
                            $scope.validCode = true;
                            $scope.breakTimer = true;
                        }
                    });
            }
            $scope.invalidCode = true;
            $scope.validCode = false;
        };

        $scope.submitChangePhoneNumber = function () {
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
            intSoftLoginService.changePhoneNumber($scope.model, modalScope.antiForgeryToken)
                .then(onSuccessfulPhoneNumberChanged, onError);
        };

        // initializations
        $scope.model = {
            password: "",
            phoneNumber: "",
            verificationCode: ""
        };
        $scope.invalidCode = false;
        $scope.validCode = false;
        $scope.verificationCode = "";
        $scope.newPhoneNumberMask = intSoftValues.phoneNumberMask;
        $scope.codeLength = intSoftValues.phoneVerificationCodeLength;

    }
]);