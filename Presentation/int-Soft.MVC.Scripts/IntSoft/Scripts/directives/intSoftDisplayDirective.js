intSoftApp.directive('intSoftDisplay', [
    "intSoftCrudService", "$state", "$window", "$stateParams", "intSoftNotificationService", "$translate","intSoftAlertService",
function (intSoftCrudService, $state, $window, $stateParams, intSoftNotificationService, $translate, intSoftAlertService) {
    return {
        restrict: 'A',
        controller: function ($scope, $element, $attrs, $rootScope) {

            // Scope Methods
            $scope.get = function () {
                intSoftCrudService.get($scope.controller, $scope.modelId, $scope.getUrl, $scope.antiForgeryToken).then(function (response) {
                    $scope.currentModel = response.data;
                }, function (response) {
                    intSoftNotificationService.notifyError(response.data.errorMessage);
                });
            };

            $scope.edit = function () {
                $state.go($scope.updateState, { id: $scope.modelId });
            };

            $scope.delete = function () {
                intSoftAlertService.showConfirm($translate.instant('WARNING'), $translate.instant('FILE_WILL_NOT_BE_RECOVERABLE'), function() {
                    intSoftCrudService.delete($scope.controller, $scope.modelId, 'Delete', $scope.antiForgeryToken).then(function() {
                        intSoftAlertService.showSuccess($translate.instant('DELETED'), $translate.instant('DELETED_SUCCESSFULLY'));
                        $rootScope.$broadcast('masterRecordDeleted', { master: $scope.currentModel });
                        $state.go($scope.listState);
                    }, function(response) {
                        intSoftAlertService.showError($translate.instant('ERROR'), response.data.errorMessage);
                    });
                });
            };

            $scope.approve = function () {
                intSoftAlertService.showConfirm($translate.instant('WARNING'), $translate.instant('YOU_WILL_APPROVE_THE_RECORD'), function() {
                    intSoftCrudService.approve($scope.controller, $scope.modelId, 'Approve', $scope.antiForgeryToken).then(function() {
                        intSoftAlertService.showSuccess($translate.instant('APPROVED'), $translate.instant('APPROVED_SUCCESSFULLY'));
                        $scope.get();
                    }, function(response) {
                        intSoftAlertService.showError($translate.instant('ERROR'), response.data.errorMessage);
                    });
                });
            };

            // Initializations
            $scope.currentModel = {};
            $scope.controller = $attrs.controller;
            $scope.getUrl = $attrs.getUrl;
            $scope.updateState = $attrs.updatestate;
            $scope.listState = $attrs.liststate;

            if ($stateParams.id) {
                console.log($stateParams.id);
                $scope.modelId = $stateParams.id;
                $scope.get();
            } else {
                console.log('no params');
            }

            $scope.form = $element.inheritedData('$formController');

        }
    };
}
]);