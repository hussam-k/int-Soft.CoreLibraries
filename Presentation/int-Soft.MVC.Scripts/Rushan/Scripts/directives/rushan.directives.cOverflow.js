﻿intSoftApp.directive("cOverflow",
[
    "scrollService", function(scrollService) {
        return {
            restrict: "C",
            link: function(scope, element) {

                if (!$("html").hasClass("ismobile")) {
                    scrollService.malihuScroll(element, "minimal-dark", "y");
                }
            }
        }
    }
]);