intSoftApp.directive("intSoftNestedComboboxDate", ["intSoftValues",
    function (intSoftValues) {
        return {
            restrict: "E",
            scope: true,
            replace: true,
            templateUrl: function (tElement, tAttrs) {
                return tAttrs.templateUrl && tAttrs.templateUrl !== null && tAttrs.templateUrl !== ""
                    ? tAttrs.templateUrl
                    : intSoftValues.templatesUrl + "templates.nestedComboboxDate.html";
            },
            controller: function ($scope, $attrs) {

                $scope.generateYearList = function () {
                    $scope.yearList = [];
                    for (var i = $scope.maxYear; i > $scope.minYear - 1; i--) {
                        $scope.yearList.push(i);
                    }
                }

                $scope.generateMonthList = function () {
                    $scope.monthList = [];
                    for (var i = 1; i < 13; i++) {
                        $scope.monthList.push(i);
                    }
                }

                $scope.generateDayList = function (newVal) {
                    $scope.dayList = [];
                    var months1 = [1, 3, 5, 7, 8, 10, 12];
                    var months2 = [4, 6, 9, 11];
                    var maxDayInMonth = 28;
                    if (-1 !== months1.indexOf(newVal)) {
                        maxDayInMonth = 31;
                    } else if (-1 !== months2.indexOf(newVal)) {
                        maxDayInMonth = 30;
                    }
                    for (var i = 1; i < maxDayInMonth + 1; i++) {
                        $scope.dayList.push(i);
                    }
                }

                $scope.generateNgModel = function () {
                    if ($scope.model.year && $scope.model.month && $scope.model.day) {
                        $scope[$scope.modelObjectName][$scope.name] = new Date($scope.model.year, $scope.model.month - 1, $scope.model.day, 12); // -1 because fuck javascript thats why !!!!    
                    }
                }

                $scope.$watch('model.year', function (newVal) {
                    if (newVal)
                        $scope.generateNgModel();
                });
                $scope.$watch('model.month', function (newVal) {
                    if (newVal) {
                        $scope.generateDayList(newVal);
                        $scope.generateNgModel();
                    }
                });
                $scope.$watch('model.day', function (newVal) {
                    if (newVal)
                        $scope.generateNgModel();
                });

                // Initializations

                $scope.ngModel = $attrs.ngModel;
                $scope.minYear = $attrs.minYear;
                $scope.minMonth = $attrs.minMonth;
                $scope.minDay = $attrs.minDay;
                $scope.maxYear = $attrs.maxYear;
                $scope.maxMonth = $attrs.maxMonth;
                $scope.maxDay = $attrs.maxDay;
                $scope.inputClass = $attrs.inputClass;
                $scope.name = $attrs.name;
                $scope.modelObjectName = $attrs.modelObjectName ? $attrs.modelObjectName : "currentModel";
                $scope.yearId = $scope.name + "_yeaer";
                $scope.monthId = $scope.name + "_month";
                $scope.dayId = $scope.name + "_day";

                $scope.$watch($scope.modelObjectName, function (newVal) {
                    if (newVal)
                        if (newVal[$scope.name]) {
                            var date = new Date(newVal[$scope.name]);
                            $scope.model = {
                                year: date.getFullYear(),
                                month: date.getMonth() +1 ,
                                day: date.getDate()
                            };
                        }
                });
                $scope.model = {};

            }
        };
    }
]);