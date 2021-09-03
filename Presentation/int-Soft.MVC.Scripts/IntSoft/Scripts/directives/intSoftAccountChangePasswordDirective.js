intSoftApp.directive("intSoftAccountChangePassword",
[
    "$state", "$stateParams", "intSoftLoginService", "intSoftNotificationService", "intSoftValues",
    function ($state, $stateParams, intSoftLoginService, intSoftNotificationService, intSoftValues) {
        return {
            restrict: "A",
            replace: true,
            scope: {
                currentPasswordPlaceHolder: "=",
                passwordPlaceHolder: "=",
                confirmPasswordPlaceHolder: "=",
                passwordDontMatchText: "=",
                logoIconSource: "=",
                antiForgeryToken: "="
            },
            templateUrl: function(tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.changePassword.html";
            },
            controller: function ($scope, $uibModalStack) {

                // Methods
                var onSuccess = function(response) {
                    $scope.isLoading = false;
                    $uibModalStack.dismissAll();
                    intSoftNotificationService.notifySuccess(response.data);
                };

                var onError = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifyError(response.data.errorMessage);
                };


                $scope.submit = function() {
                    $scope.changePasswordForm = angular.element(document.querySelector("#changePasswordForm"))
                        .controller("form");
                    if ($scope.changePasswordForm.$invalid) {
                        angular.forEach($scope.changePasswordForm,
                            function(value) {
                                if (typeof value === "object" && value.hasOwnProperty("$modelValue")) {
                                    value.$setTouched();
                                }
                            });
                        return;
                    }
                    $scope.isLoading = true;
                    intSoftLoginService.changePassword($scope.model, $scope.antiForgeryToken)
                        .then(onSuccess, onError);
                };

                $scope.checkPasswordAvailability = function() {
                    $scope.isLoading = true;
                    $scope.form = angular.element(document.querySelector("#changePasswordForm"))
                        .controller("form");
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
                                $scope.isLoading = false;
                            });
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