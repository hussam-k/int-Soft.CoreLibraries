intSoftApp.directive("intSoftList", [
    "intSoftCrudService", "$state", "$window", "$stateParams", "intSoftNotificationService", "$translate","intSoftAlertService",
    function (intSoftCrudService, $state, $window, $stateParams, intSoftNotificationService, $translate, intSoftAlertService) {
        return {
            restrict: "A",
            controller: function ($scope, $attrs, $rootScope) {

                // Scope Methods
                $scope.list = function () {
                    intSoftCrudService.list($scope.controller, $scope.getListAction, { pageNumber: $scope.currentPage, pageSize: $scope.pageSize }, $scope.antiForgeryToken)
                        .then(function(response) {
                            $scope.modelList = response.data.list;
                            $scope.totalNumberOfItems = response.data.totalNumberOfItems;
                            $rootScope.$broadcast('onListDataLoaded', null);
                            console.log(response);

                        }, function(response) {
                            console.log(response);
                            intSoftNotificationService.notifyError(response.data.errorMessage);
                        });
                };

                $scope.update = function(id) {
                    $state.go($scope.updateState, { id: id });
                };

                $scope.create = function() {
                    $state.go($scope.createState);
                };

                $scope.drafts = function () {
                    $state.go($scope.draftsState);
                };

                $scope.display = function (id) {
                    $state.go($scope.displayState, { id: id });
                };

                $scope.goToList = function () {
                    $state.go($scope.listState);
                };
                
                $scope.delete = function (id) {
                    intSoftAlertService.showConfirm($translate.instant('WARNING'), $translate.instant('FILE_WILL_NOT_BE_RECOVERABLE'), function() {
                        intSoftCrudService.delete($scope.controller, id, 'Delete', $scope.antiForgeryToken).then(function() {
                            intSoftAlertService.showSuccess($translate.instant('DELETED'), $translate.instant('DELETED_SUCCESSFULLY'));
                            $scope.list();
                        }, function(response) {
                            intSoftAlertService.showError($translate.instant('ERROR'), response.data.errorMessage);
                        });
                    });
                };

                $scope.approve = function(id) {
                    intSoftAlertService.showConfirm($translate.instant('WARNING'), $translate.instant('YOU_WILL_APPROVE_THE_RECORD'), function() {
                        intSoftCrudService.approve($scope.controller, id, 'Approve', $scope.antiForgeryToken).then(function() {
                            intSoftAlertService.showSuccess($translate.instant('APPROVED'), $translate.instant('APPROVED_SUCCESSFULLY'));
                            $scope.list();
                        }, function(response) {
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
                $scope.createState = $attrs.createstate;
                $scope.updateState = $attrs.updatestate;
                $scope.draftsState = $attrs.draftsstate;
                $scope.displayState = $attrs.displaystate;
                $scope.listState = $attrs.liststate;
                $scope.getListAction = $attrs.getlistaction;
                $scope.list();
            }
        };
    }
]);