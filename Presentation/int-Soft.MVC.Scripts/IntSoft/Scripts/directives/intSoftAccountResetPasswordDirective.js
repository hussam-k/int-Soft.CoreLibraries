intSoftApp.directive("intSoftAccountResetPassword",
[
    "$state", "$stateParams", "intSoftLoginService", "intSoftNotificationService", "intSoftValues",
    function($state, $stateParams, intSoftLoginService, intSoftNotificationService, intSoftValues) {
        return {
            restrict: "A",
            replace: true,
            scope: {
                emailPlaceHolder: "=",
                passwordPlaceHolder: "=",
                confirmPasswordPlaceHolder: "=",
                passwordDontMatchText: "=",
                showLoginButton: "=",
                loginText: "=",
                logoIconSource: "=",
                antiForgeryToken: "="
            },
            templateUrl: function(tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.resetPassword.html";
            },
            controller: function($scope) {
                // Methods
                var onSuccess = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifySuccess(response.data);
                };

                var onError = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifyError(response.data.errorMessage);
                };

                $scope.submit = function() {
                    $scope.resetPasswordForm = angular.element(document.querySelector("#resetPasswordForm"))
                        .controller("form");
                    if ($scope.resetPasswordForm.$invalid) {
                        angular.forEach($scope.resetPasswordForm,
                            function(value) {
                                if (typeof value === "object" && value.hasOwnProperty("$modelValue")) {
                                    value.$setTouched();
                                }
                            });
                        return;
                    }
                    $scope.isLoading = true;
                    intSoftLoginService.resetPassword($scope.model, $scope.antiForgeryToken)
                        .then(onSuccess, onError);
                };
                
                $scope.checkPasswordAvailability = function() {
                    $scope.isLoading = true;
                    $scope.form = angular.element(document.querySelector("#resetPasswordForm")).controller("form");
                    intSoftLoginService.checkPasswordAvailability($scope.model.password, $scope.antiForgeryToken)
                        .then(function(response) {
                            if (response && response.data === true) {
                                $scope.form.password.$setValidity("invalidPassword", true);
                                $scope.form.password.$setTouched();
                            }
                            $scope.isLoading = false;
                        },
                            function(response) {
                                $scope.form.password.$setValidity("invalidPassword", false);
                                $scope.form.password.$setTouched();
                                $scope.invalidPasswordMessage = response.data.errorMessage;
                                console.log($scope.invalidPasswordMessage);
                                $scope.isLoading = false;
                            });
                };

                // initializations
                $scope.model = {
                    username: "",
                    password: "",
                    email: "",
                    id: $stateParams.userId,
                    code: $stateParams.code
                };
                $scope.currentSkin = intSoftValues.theme;
            }
        };
    }
]);