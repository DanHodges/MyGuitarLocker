(function() {

    "use strict";

    // Getting the existing module
    angular.module("app-trips")
      .controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;

        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function (response) {
                angular.copy(response.data.results, vm.trips);
                console.log("response :", response);
            }, function (error) {
                vm.errorMessage = "Failed to load data " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addTrip  = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    //success
                    vm.trips.push(response.data);
                    console.log("success");
                    console.log("response.data :", response.data);
                    vm.newTrip = {};
                }, function () {
                    //error
                    vm.errorMessage = "Failed to save new trips";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };
    }
})();