angular.module("btcApp", ["ngResource"])

    .directive("headerMenu", function ($resource) {

        var api = $resource("api/menu");
        return {
            scope: {},
            templateUrl: "js/templates/header-menu.html",
            replace: true,
            link: function(scope) {
                scope.vm = {};
                api.query(function(data) {
                    scope.vm.categories = data;
                });
            }
        };
    })

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
                    api.save(function(data) {
                            scope.vm.success = data.subscriptionMessage;
                            scope.vm.emailAddress = "";
                        },
                        function(error) {
                            scope.vm.modelErrors = error.data.modelState.emailAddress;
                        });
                };
            }
        };
    });