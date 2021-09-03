intSoftApp.directive("intSoftUiGrid", [
    "intSoftAjaxService",
    function (intSoftAjaxService) {
        return {
            restrict: "A",
            scope: true,
            controller: function ($scope, $attrs) {

                // Scope Methods
                $scope.getUiGridDefinition = function () {
                    var url = "/" + $scope.uiGridDefinitionController + "/" + $scope.uiGridDefinitionAction + "/";

                    intSoftAjaxService.get(url, null, $scope.antiForgeryToken).then(function(response) {
                        $scope.uiGridDefinition = response.data;
                        console.log($scope);

                    }, function(response) {
                            console.log(response);
                        });
                };
                
                $scope.$on('onListDataLoaded', function () {
                    $scope.uiGridDefinition.data = $scope.modelList;
                });
                
                // Initializations 
                $scope.uiGridDefinition = {};
                $scope.uiGridDefinitionController = $attrs.uiGridDefinitionController;
                $scope.uiGridDefinitionAction = $attrs.uiGridDefinitionAction;

                $scope.getUiGridDefinition();
            }
        };
    }
]);