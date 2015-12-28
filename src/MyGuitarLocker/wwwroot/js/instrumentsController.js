(function() {

    "use strict";

    // Getting the existing module
    angular.module("app-Instruments")
      .controller("InstrumentsController", InstrumentsController);

    function InstrumentsController($http) {

        var vm = this;

        vm.Instruments = [];

        vm.newInstrument = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/Instruments")
            .then(function (response) {
                angular.copy(response.data.results, vm.Instruments);
                console.log("response :", response);
            }, function (error) {
                vm.errorMessage = "Failed to load data " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addInstrument  = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/Instruments", vm.newInstrument)
                .then(function (response) {
                    //success
                    vm.Instruments.push(response.data);
                    console.log("success");
                    console.log("response.data :", response.data);
                    vm.newInstrument = {};
                }, function () {
                    //error
                    vm.errorMessage = "Failed to save new Instruments";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };
    }
})();