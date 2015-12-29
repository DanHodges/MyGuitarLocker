(function() {

    "use strict";

    angular.module("app-Instruments", ["simpleControls", "ngRoute", "ngAudio"])
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
            $routeProvider.when("/editor/:InstrumentName/addClips", {
                controller: "addClipsController",
                controllerAs: "vm",
                templateUrl: "/views/addClipsView.html"
            });
            $routeProvider.when("/editor/:InstrumentName/addPics", {
                controller: "addPicsController",
                controllerAs: "vm",
                templateUrl: "/views/addPicsView.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });

})();