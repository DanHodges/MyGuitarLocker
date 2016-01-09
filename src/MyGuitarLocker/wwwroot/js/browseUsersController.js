(function () {

    "use strict";

    // Getting the existing module
    angular.module("app-browse")
      .controller("browseUsersController", browseUsersController);

    function browseUsersController($http) {
        console.log("userController");
        var vm = this;

        vm.users = [];

        $http.get("/api/users")
            .then(function (response) {
                angular.copy(response.data, vm.users);
                console.log("response :", response.data);
            }, function (error) {
                vm.errorMessage = "Failed to load data " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });
    }
})();