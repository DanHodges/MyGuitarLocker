(function() {

    "use strict";

    angular.module("app-Instruments", ["simpleControls", "ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "InstrumentsController",
                controllerAs: "vm",
                templateUrl: "/views/InstrumentView.html"
            });
            $routeProvider.when("/editor/:InstrumentName", {
                controller: "InstrumentEditorController",
                controllerAs: "vm",
                templateUrl: "/views/InstrumentEditorView.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });

        });

})();