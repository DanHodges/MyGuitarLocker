(function () {

    "use strict";

    angular.module("app-browse")
      .controller("userController", userController);

    function userController($routeParams,$http) {
        var vm = this;
        console.log("$routeParams :", $routeParams);
        vm.user = $routeParams.user;
        vm.instruments=[];

        $http.get("/api/Instruments/" + vm.user)
            .then(function (response) {
                angular.copy(response.data.results, vm.instruments);
                console.log("response :", response.data.results);
            }, function (error) {
                vm.errorMessage = "Failed to load data " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });
    }
})();