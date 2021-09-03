intSoftApp.directive("intSoftAccountReactivate",
    ["intSoftLoginService", "intSoftNotificationService","intSoftValues",
        function (intSoftLoginService, intSoftNotificationService, intSoftValues) {
        return {
            restrict: "A",
            replace: true,
            scope: {
                logoIconSource: "=",
                showLoginButton: "=",
                showForgotYourPasswordButton: "=",
                emailPlaceHolder: "=",
                loginText: "=",
                forgotYourPasswordText: "=",
                antiForgeryToken: "="
            },
            templateUrl: function (tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.reactivate.html";
            },
            controller: function($scope) {

                var onSuccessful = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifySuccess(response.data);
                };

                var onError = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifyError(response.data.errorMessage);
                };

                $scope.submit = function () {
                    $scope.isLoading = true;
                    intSoftLoginService.reactivate($scope.model, $scope.antiForgeryToken)
                        .then(onSuccessful, onError);
                };

                // initializations
                $scope.model = {
                    email: ""
                };
                $scope.currentSkin = intSoftValues.theme;
            }
        };
    }
]);