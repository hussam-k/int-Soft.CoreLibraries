intSoftApp.factory("intSoftMarkdownService", ["$window", "$sce", function ($window, $sce) {

    var toHtml = function (markdownText) {
        return $sce.trustAsHtml($window.marked(markdownText));
    };

    return {
        toHtml: toHtml,
    };
}
]);