intSoftApp.directive("intSoftMasterList", [
    "intSoftCrudService", "$state", "$window", "$stateParams", "intSoftNotificationService", "$rootScope",
    function (intSoftCrudService, $state, $window, $stateParams, intSoftNotificationService, $rootScope) {
        return {
            restrict: "A",
            controller: function ($scope, $attrs, $rootScope) {

                // Scope Methods
                $scope.getPage = function () {
                    $scope.busy = true;
                    intSoftCrudService.list($scope.controller, $scope.getListAction,
                        { pageNumber: $scope.pageNumber, pageSize: $scope.pageSize }, $scope.antiForgeryToken)
                        .then(function (response) {
                            var listData = response.data.list;

                            for (var key in listData) {
                                $scope.modelList.push(listData[key]);
                            };

                            if (listData.length !== 0) {
                                $scope.pageNumber = $scope.pageNumber + 1;
                            }

                            $scope.busy = false;

                        }, function (response) {
                            console.log(response);
                            intSoftNotificationService.notifyError(response.data.errorMessage);
                        });
                };

                $scope.create = function () {
                    $state.go($scope.createState);
                };

                $scope.drafts = function () {
                    $state.go($scope.draftsState);
                };

                $scope.display = function (item) {
                    $scope.currentModel = item;
                    $scope.modelId = item.id;
                    $rootScope.$broadcast('selected-master-changed', item);
                };

                
                // Initializations 
                $scope.modelList = [];
                $scope.controller = $attrs.controller;
                $scope.createState = $attrs.createState;
                $scope.draftsState = $attrs.draftsState;
                $scope.getListAction = $attrs.listServerAction;
                $scope.pageNumber = 1;
                $scope.pageSize = $attrs.pageSize;
                $scope.scrollbarConfig = {
                    theme: 'dark',
                    callbacks: {
                        onTotalScroll: function () {
                            $scope.getPage();
                        }
                    }
                };
                $scope.getPage();
            }
        };
    }
]);