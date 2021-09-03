intSoftApp.directive("intSoftAccountConfirmEmail",
[
    "$state", "$stateParams", "intSoftLoginService", "intSoftNotificationService", "intSoftValues",
    function($state, $stateParams, intSoftLoginService, intSoftNotificationService, intSoftValues) {
        return {
            restrict: "A",
            replace: true,
            scope: {
                successfulConfirmEmailText: "=",
                failedConfirmEmailText: "=",
                showLoginButton: "=",
                loginText: "=",
                logoIconSource: "=",
                antiForgeryToken: "="
            },
            templateUrl: function(tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.confirmEmail.html";
            },
            controller: function($scope) {
                // Methods
                var onSuccess = function(response) {
                    $scope.isLoading = false;
                    $scope.successfulConfirmEmail = true;
                    intSoftNotificationService.notifySuccess(response.data);
                };
                var onError = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifyError(response.data.errorMessage);
                    $scope.successfulConfirmEmail = false;
                };
                var confirmEmail = function() {
                    $scope.isLoading = true;
                    intSoftLoginService.confirmEmail({ userId: $stateParams.userId, code: $stateParams.code },
                            $scope.antiForgeryToken)
                        .then(onSuccess, onError);
                };

                // initializations
                confirmEmail();
                $scope.currentSkin = intSoftValues.theme;
            }
        };
    }
]);