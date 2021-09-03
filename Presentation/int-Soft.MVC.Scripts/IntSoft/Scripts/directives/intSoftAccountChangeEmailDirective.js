intSoftApp.directive("intSoftAccountChangeEmail",
[
    "intSoftLoginService", "intSoftNotificationService", "intSoftValues", "$stateParams",
    function(intSoftLoginService, intSoftNotificationService, intSoftValues, $stateParams) {
        return {
            restrict: "A",
            replace: true,
            scope: {
                newEmailPlaceHolder: "=",
                passwordPlaceHolder: "=",
                invalidEmailText: "=",
                logoIconSource: "=",
                antiForgeryToken: "="
            },
            templateUrl: function(tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.changeEmail.html";
            },
            controller: function ($scope, $uibModalStack) {
                // Methods
                var onSuccess = function(response) {
                    $scope.isLoading = false;
                    if ($scope.onSuccessCallback) {
                        $scope.onSuccessCallback();
                    }
                    $uibModalStack.dismissAll();
                    intSoftNotificationService.notifySuccess(response.data);
                };

                var onError = function(response) {
                    $scope.isLoading = false;
                    intSoftNotificationService.notifyError(response.data.errorMessage);
                };

                // Scope Methods

                $scope.submit = function() {
                    $scope.changeEmailForm = angular.element(document.querySelector("#changeEmailForm"))
                        .controller("form");
                    if ($scope.changeEmailForm.$invalid) {
                        angular.forEach($scope.changeEmailForm,
                            function(value) {
                                if (typeof value === "object" && value.hasOwnProperty("$modelValue")) {
                                    value.$setTouched();
                                }
                            });
                        return;
                    }
                    $scope.isLoading = true;
                    intSoftLoginService.changeEmail($scope.model, $scope.antiForgeryToken)
                        .then(onSuccess, onError);
                };

                $scope.checkEmailAvailability = function() {
                    $scope.isLoading = true;
                    $scope.form = angular.element(document.querySelector("#changeEmailForm")).controller("form");
                    intSoftLoginService.checkEmailAvailability($scope.model.email, $scope.antiForgeryToken)
                        .then(function(response) {
                                if (response && response.data === true) {
                                    $scope.form.email.$setValidity("invalid", true);
                                    $scope.form.email.$setTouched();
                                }
                                $scope.isLoading = false;
                            },
                            function() {
                                $scope.form.email.$setValidity("invalid", false);
                                $scope.form.email.$setTouched();
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
                $scope.onSuccessCallback = $stateParams.onSuccessCallback;
            }
        };
    }
]);