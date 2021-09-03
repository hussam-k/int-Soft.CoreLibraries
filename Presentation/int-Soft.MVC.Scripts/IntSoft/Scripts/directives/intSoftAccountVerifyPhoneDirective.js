intSoftApp.directive("intSoftAccountVerifyPhone",
[
    "intSoftLoginService", "intSoftNotificationService", "intSoftAjaxService", "intSoftValues", "$stateParams", "$interval", "$uibModalStack",
    function (intSoftLoginService, intSoftNotificationService, intSoftAjaxService, intSoftValues, $stateParams, $interval, $uibModalStack) {
        return {
            restrict: "A",
            replace: true,
            scope: {
                phoneVerificationTitle: "=",
                phoneVerificationNumberLabel: "=",
                phoneVerificationNumberPlaceHolder: "=",
                validPhoneVerificationCode: "=",
                invalidPhoneVerificationCode: "=",
                resendMessage: "=",
                okButtonText: "=",
                resendButtonText: "=",
                antiForgeryToken: "="
            },
            templateUrl: function (tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.verifyPhone.html";
            },
            controller: function ($scope) {

                // Methods
                $scope.checkPhoneVerificationCode = function () {
                    if ($scope.verificationCode && $scope.verificationCode.length === 6) {
                        intSoftLoginService.checkPhoneVerificationCodeValidity($scope.verificationCode,
                                $scope.phoneNumber,
                                $scope.antiForgeryToken)
                            .then(function (response) {
                                if (response && response.data === false) {
                                    $scope.invalidCode = true;
                                    $scope.validCode = false;
                                } else {
                                    $scope.invalidCode = false;
                                    $scope.validCode = true;
                                }
                            });
                    }
                };

                $scope.submit = function () {
                    intSoftLoginService
                        .checkPhoneVerificationCodeValidity($scope.verificationCode,
                            $scope.phoneNumber,
                            $scope.antiForgeryToken)
                        .then(function (response) {
                            if (response && response.data === false) {
                                $scope.invalidCode = true;
                                $scope.validCode = false;
                            } else {
                                $scope.invalidCode = false;
                                $scope.validCode = true;
                                $scope.successfulVerificationCallback($scope.verificationCode);
                            }
                        });
                };

                $scope.cancel = function() {
                    if ($scope.onCancelVerification) {
                        $scope.onCancelVerification();
                    }
                }

                $scope.sendVerificationCode = function () {
                    $scope.canResend = false;
                    $scope.timer = new Date();
                    $scope.timer.setMinutes(intSoftValues.phoneVerificationCodeResendInterval);
                    $scope.timer.setSeconds(0);
                    intSoftLoginService.sendPhoneVerification($scope.phoneNumber, $scope.antiForgeryToken)
                        .then(function () {
                            $interval(function () {
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
                        },
                            function (response) {
                                $uibModalStack.dismissAll();
                                intSoftNotificationService.notifyError(response.data.errorMessage);
                            });
                }

                // Initializations
                $scope.invalidCode = false;
                $scope.validCode = false;
                $scope.verificationCode = "";
                $scope.codeLength = intSoftValues.phoneVerificationCodeLength;
                $scope.phoneNumber = $stateParams.phoneNumber;
                $scope.successfulVerificationCallback = $stateParams.callback;
                $scope.currentSkin = intSoftValues.theme;
                $scope.onCancelVerification = $stateParams.onCancelVerification;
                $scope.sendVerificationCode();
            }
        };
    }
]);