(function() {

    "use strict";

    angular.module("app-Instruments", ["simpleControls", "ngRoute", "ngAudio"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "instrumentsController",
                controllerAs: "vm",
                templateUrl: "/views/instrumentsView.html"
            });
            $routeProvider.when("/editor/:InstrumentName", {
                controller: "instrumentEditorController",
                controllerAs: "vm",
                templateUrl: "/views/instrumentEditorView.html"
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
            $routeProvider.when("/addInstrument", {
                controller: "addInstrumentController",
                controllerAs: "vm",
                templateUrl: "/views/addInstrumentView.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });

})();