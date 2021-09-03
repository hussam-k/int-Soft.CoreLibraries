intSoftApp.directive('intSoftForm', [
    "intSoftCrudService", "$state", "$window", "$stateParams", "intSoftNotificationService", "intSoftAjaxService", "$uibModalStack", "$rootScope", "$translate", "intSoftAlertService",
    function (intSoftCrudService, $state, $window, $stateParams, intSoftNotificationService, intSoftAjaxService, $uibModalStack, $rootScope, $translate, intSoftAlertService) {
        return {
            restrict: 'A',
            controller: function ($scope, $element, $attrs) {

                // Scope Methods
                $scope.get = function (id) {
                    intSoftCrudService.get($scope.controller, id, $scope.getUrl).then(function (response) {
                        $scope.currentModel = response.data;
                    }, function (response) {
                        intSoftNotificationService.notifyError(response.data.errorMessage);
                    });
                };

                $scope.clearForm = function() {
                    $scope.currentModel = {};
                    angular.forEach($scope.form,
                        function(value) {
                            if (typeof value === 'object' && value.hasOwnProperty('$modelValue')) {
                                value.$setUntouched();
                            }
                        });
                };

                $scope.save = function () {
                    if ($scope.form.$invalid) {
                        if ($scope.form.$error.recaptcha) {
                            $scope.captchaErrorMessage = $scope.translations.INVALID_CAPTCHA;
                        } else {
                            $scope.captchaErrorMessage = null;
                        }
                        angular.forEach($scope.form, function (value) {
                            if (typeof value === 'object' && value.hasOwnProperty('$modelValue')) {
                                value.$setTouched();
                            }
                        });
                        return;
                    }

                    intSoftCrudService.save($scope.controller, $scope.currentModel, $scope.saveUrl, $scope.antiForgeryToken)
                        .then(function (response) {

                            intSoftNotificationService.notifySuccess(response.data.message);

                            if ($scope.isDetailModal === true) {
                                $uibModalStack.dismissAll();
                                $rootScope.$broadcast('detail-saved');
                            } else {
                                if ($scope.redirectState && $scope.redirectState !== '') {
                                    $state.go($scope.redirectState);
                                } else {
                                    if ($scope.redirectUrl) {
                                        window.location = $scope.redirectUrl;
                                    } else {
                                        if ($scope.clearAfterSubmit && $scope.clearAfterSubmit === 'true') {
                                            $scope.clearForm();
                                        }
                                    }
                                }
                            }
                        }, function (response) {
                            intSoftNotificationService.notifyError(response.data.errorMessage);
                        });
                };

                $scope.cancelForm = function () {
                    if ($scope.form.$dirty) {
                        intSoftAlertService.showConfirm($scope.translations.WARNING, $scope.translations.DO_YOU_WANT_TO_IGNORE_CHANGES, function () {
                            if ($scope.isDetailModal === true) {
                                $uibModalStack.dismissAll();
                            } else {
                                $window.history.back();
                            }
                        });
                    } else {
                        if ($scope.isDetailModal === true) {
                            $uibModalStack.dismissAll();
                        } else {
                            $window.history.back();
                        }
                    }
                };

                $scope.cascadeComboboxCall = function (propertyName, resultListName, callUrl) {
                    intSoftAjaxService.post(callUrl, { parentId: $scope.currentModel[propertyName] }, $scope.antiForgeryToken)
                        .success(function (data) {
                            if (data.list === undefined)
                                $scope[resultListName] = data;
                            else
                                $scope[resultListName] = data.list;
                        })
                        .error(function (response) {
                            console.log(response);
                        });
                };

                $scope.setWatch = function (parentPropertyName, resultListName, callUrl) {
                    $scope.$watch('currentModel.' + parentPropertyName, function (newVal) {
                        if (newVal)
                            $scope.cascadeComboboxCall(parentPropertyName, resultListName, callUrl);
                    });
                };


                $scope.comboBoxCall = function (url, listName) {
                    intSoftAjaxService.post(url, {}, $scope.antiForgeryToken)
                        .then(function (response) {
                            $scope[listName] = response.data.list;
                        }, function (response) {
                            intSoftNotificationService.notifyError(response.errorMessage);
                        });
                };

                // Initializations
                if (!$scope.currentModel) {
                    $scope.currentModel = {};
                }
                $scope.controller = $attrs.controller;
                $scope.saveUrl = $attrs.saveUrl;
                $scope.getUrl = $attrs.getUrl;
                $scope.redirectState = $attrs.redirectState;
                $scope.clearAfterSubmit = $attrs.clearAfterSubmit;
                $scope.redirectUrl = $attrs.redirectUrl;
                if ($stateParams.id)
                    $scope.get($stateParams.id);
                if ($stateParams.masterId) {
                    $scope.currentModel[$stateParams.detailForignKeyPropertyName] = $stateParams.masterId;
                    $scope.isDetailModal = true;
                }
                $scope.form = $element.inheritedData('$formController');
                $translate(['SAVED_SUCCESSFULLY', 'WARNING', 'YES', 'DO_YOU_WANT_TO_IGNORE_CHANGES', 'INVALID_CAPTCHA']).then(function (translations) {
                    $scope.translations = translations;
                }, function (translationIds) {
                    $scope.translations = translationIds;
                });
            }
        };
    }
]);