intSoftApp.directive("intSoftDetailList", [
    "intSoftCrudService", "$state", "$window", "$stateParams", "intSoftNotificationService", "$rootScope","$translate","intSoftAlertService",
    function (intSoftCrudService, $state, $window, $stateParams, intSoftNotificationService, $rootScope, $translate, intSoftAlertService) {
        return {
            restrict: "A",
            scope: true,
            controller: function ($scope, $attrs) {

                // Scope Methods
                $rootScope.$on('selected-master-changed', function (event, data) {
                    $scope.currentMasterModel = data;
                    $scope.list();
                });

                $rootScope.$on('detail-saved', function (event) {
                    $scope.list();
                });

                $scope.list = function () {
                    intSoftCrudService.list($scope.controller, $scope.getListAction, { masterId: $scope.currentMasterModel[$scope.masterPrimaryKey],
                                pageNumber: $scope.currentPage,
                                pageSize: $scope.pageSize}, $scope.antiForgeryToken)

                        .then(function (response) {
                            $scope.modelList = response.data.list;
                            $scope.totalNumberOfItems = response.data.totalNumberOfItems;
                            $rootScope.$broadcast('onListDataLoaded', null);

                        }, function (response) {
                            console.log(response);
                            intSoftNotificationService.notifyError(response.data.errorMessage);
                        });
                };

                $scope.update = function (id) {
                    $state.go('.' + $scope.updateState, {
                        id: id,
                        masterId: $scope.currentMasterModel[$scope.masterPrimaryKey],
                        detailForignKeyPropertyName: $scope.detailForignKey
                    });
                };

                $scope.create = function () {
                    $state.go('.' + $scope.createState, {
                        masterId: $scope.currentMasterModel[$scope.masterPrimaryKey],
                        detailForignKeyPropertyName: $scope.detailForignKey
                    });
                };

                $scope.display = function (id) {
                    $state.go('.' + $scope.displayState, { id: id });
                };

                $scope.delete = function (id) {
                    intSoftAlertService.showConfirm($translate.instant('WARNING'), $translate.instant('FILE_WILL_NOT_BE_RECOVERABLE'), function () {
                        intSoftCrudService.delete($scope.controller, id, 'Delete', $scope.antiForgeryToken).then(function () {
                            intSoftAlertService.showSuccess($translate.instant('DELETED'), $translate.instant('DELETED_SUCCESSFULLY'));
                            $scope.list();
                        }, function (response) {
                            intSoftAlertService.showError($translate.instant('ERROR'), response.data.errorMessage);
                        });
                    });
                };

                //paging
                $scope.currentPageChanged = function () {
                    $scope.list();
                };

                $scope.currentPage = 1;
                $scope.pageSize = 10;
                $scope.totalNumberOfItems = 0;
                $scope.numberOfPages = Math.ceil($scope.totalNumberOfItems / $scope.pageSize);

                // Initializations 
                $scope.modelList = [];
                $scope.currentModel = {};
                $scope.controller = $attrs.controller;
                $scope.createState = $attrs.createState;
                $scope.updateState = $attrs.updateState;
                $scope.displayState = $attrs.displayState;
                $scope.getListAction = $attrs.getListAction;
                $scope.masterPrimaryKey = $attrs.masterPrimaryKey;
                $scope.detailForignKey = $attrs.detailForignKey;

            }
        };
    }
]);