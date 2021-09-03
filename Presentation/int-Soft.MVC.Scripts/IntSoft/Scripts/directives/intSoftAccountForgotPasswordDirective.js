intSoftApp.directive("intSoftAccountForgotPassword",
[
    "intSoftLoginService", "intSoftNotificationService", "intSoftValues",
    function(intSoftLoginService, intSoftNotificationService, intSoftValues) {
        return {
            restrict: "A",
            replace: true,
            scope: {
                emailPlaceHolder: "=",
                showLoginButton: "=",
                loginText: "=",
                showReactivateButton: "=",
                reactivateText: "=",
                logoIconSource: "=",
                antiForgeryToken: "="
            },
            templateUrl: function(tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.forgotPassword.html";
            },
            controller: function($scope) {
                // Methods
                var onSuccessful = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifySuccess(response.data);
                };

                var onError = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifyError(response.data.errorMessage);
                };

                $scope.submit = function() {
                    $scope.isLoading = true;
                    intSoftLoginService.forgotPassword($scope.model, $scope.antiForgeryToken)
                        .then(onSuccessful, onError);
                };

                // initializations
                $scope.model = {
                    username: "",
                    password: "",
                    email: ""
                };
                $scope.currentSkin = intSoftValues.theme;
            }
        };
    }
]);