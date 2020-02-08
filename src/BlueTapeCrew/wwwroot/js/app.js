angular.module("btcApp", ["ngResource"])
    .directive("emailSubscribe", function ($resource) {
        return {
            scope: {},
            templateUrl: "js/templates/email-subscribe.html",
            link: function (scope) {

                scope.vm = {};
                scope.vm.emailAddress = "";
                scope.vm.modelErrors = [];
                scope.vm.success = "";

                scope.vm.clearMessages = function() {
                    scope.vm.success = "";
                    scope.vm.modelErrors = "";
                };

                scope.vm.subscribe = function(vm) {
                    var api = $resource("api/subscribe?emailAddress=" + vm.emailAddress);
                    api.save(function() {
                            alert("Thank you for subscribing");
                        },
                        function (error) {
                            console.error(error);
                            alert("There was an error subscribing");
                        });
                };
            }
        };
    });