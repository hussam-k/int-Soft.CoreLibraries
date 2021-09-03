intSoftApp.directive("intSoftAccountLogin",
    ["$state", "$stateParams", "intSoftLoginService", "intSoftNotificationService","intSoftValues",
        function ($state, $stateParams, intSoftLoginService, intSoftNotificationService, intSoftValues) {
        return {
            restrict: "A",
            replace: true,
            scope: {
                usernamePlaceHolder: "=",
                passwordPlaceHolder: "=",
                showForgotYourPasswordButton: "=",
                forgotYourPasswordText: "=",
                showReactivateButton: "=",
                reactivateText: "=",
                logoIconSource: "=",
                antiForgeryToken: "="
            },
            templateUrl: function(tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.login.html";
            },
            controller: function($scope) {
                // Methods
                var onSuccess = function() {
                    $scope.isLoading = false;
                    if ($stateParams.redirectState && $stateParams.redirectState !== null) {
                        window.location = $state.href($stateParams.redirectState, $stateParams.redirectStateParams, { absolute: true });
                    } else {
                        window.location = "/";
                    }
                };
                                
                var onError = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifyError(response.data.errorMessage);
                };

                // Scope Methods
                $scope.submit = function () {
                    $scope.isLoading = true;
                    intSoftLoginService.login($scope.model, $scope.antiForgeryToken).then(onSuccess, onError);
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