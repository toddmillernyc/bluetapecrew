angular.module("btcApp", ["ngResource"])

    .directive("emailSubscribe", function ($resource) {
        return {
            scope: {},
            templateUrl: "/js/templates/email-subscribe.html",
            link: function (scope) {

                scope.vm = {};
                scope.vm.emailAddress = "";
                scope.vm.modelErrors = [];
                scope.vm.success = "";

                scope.vm.clearMessages = function() {
                    scope.vm.success = "";
                    scope.vm.modelErrors = "";
                }

                scope.vm.subscribe = function (vm) {
                    var api = $resource("/api/subscribe?emailAddress=" + vm.emailAddress);
                    api.save(function (data) {
                        console.log(data);
                        scope.vm.success = data.subscriptionMessage;
                        console.log(data.subscriptionMessage);
                        scope.vm.emailAddress = "";
                    }, function (error) { scope.vm.modelErrors = error.data.ModelState.emailAddress; });
                }
            }
        };
    });