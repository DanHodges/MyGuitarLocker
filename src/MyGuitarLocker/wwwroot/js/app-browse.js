(function () {

    "use strict";

    angular.module("app-browse", ["ngRoute", "ngAudio"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "browseUsersController",
                controllerAs: "vm",
                templateUrl: "/views/browseUsersView.html"
            });
            $routeProvider.when("/locker/:user", {
                controller: "userController",
                controllerAs: "vm",
                templateUrl: "/views/userView.html"
            });
            $routeProvider.when("/locker/:user/:InstrumentName", {
                controller: "browseInstrumentController",
                controllerAs: "vm",
                templateUrl: "/views/browseInstrumentView.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });

})();