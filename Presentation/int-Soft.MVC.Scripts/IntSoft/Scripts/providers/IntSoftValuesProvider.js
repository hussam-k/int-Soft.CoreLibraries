intSoftApp.provider("intSoftValues",
    function () {
        // current logged in user
        var currentUser = null;
        this.currentUser = function (value) {
            currentUser = value;
            return this;
        }

        // URL for Directive Templates
        var templatesUrl = "";
        this.templatesUrl = function (value) {
            templatesUrl = value;
            return this;
        }

        // Main Application Theme
        var theme = "blue";
        this.theme = function (value) {
            theme = value;
            return this;
        }

        // Phone Verification Code Length
        var phoneVerificationCodeLength = 6;
        this.phoneVerificationCodeLength = function (value) {
            phoneVerificationCodeLength = value;
            return this;
        }

        // Phone Verification Code Resend Interval
        var phoneVerificationCodeResendInterval = 5;
        this.phoneVerificationCodeResendInterval = function (value) {
            phoneVerificationCodeResendInterval = value;
            return this;
        }

        // Phone Number Mask
        var phoneNumberMask = "+201000000000";
        this.phoneNumberMask = function (value) {
            phoneNumberMask = value;
            return this;
        }

        this.$get = function () {
            return {
                currentUser: currentUser,
                templatesUrl: templatesUrl,
                theme: theme,
                phoneVerificationCodeLength: phoneVerificationCodeLength,
                phoneVerificationCodeResendInterval: phoneVerificationCodeResendInterval,
                phoneNumberMask: phoneNumberMask
            };
        }
    }
);