intSoftApp.service("intSoftAjaxService", [
    "$http", "Upload" , function ($http, Upload) {

        var executeAction = function (method, url, params, antiForgeryToken) {
            var req = {
                method: method,
                url: url,
                data: params
            };

            if (req.method === "POST") {
                req.headers = {
                    'RequestVerificationToken': antiForgeryToken
                };

                return Upload.upload(req);
            }

            return $http(req);
        };

        var post = function (url, postData, antiForgeryToken) {
            return executeAction("POST", url, postData, antiForgeryToken);
        };

        var get = function(url, params) {
            return executeAction("GET", url, params);
        };

        return {
            post: post,
            get: get
        };
    }
]);