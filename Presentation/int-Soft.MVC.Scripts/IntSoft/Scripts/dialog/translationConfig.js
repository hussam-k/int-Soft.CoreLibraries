intSoftApp.config([
    "$translateProvider", function ($translateProvider) {
        $translateProvider.translations("lang", window.resources);
        $translateProvider.preferredLanguage("lang");
        $translateProvider.useSanitizeValueStrategy(null);
    }
]);