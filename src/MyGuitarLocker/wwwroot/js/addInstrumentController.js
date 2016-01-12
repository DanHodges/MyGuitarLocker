(function () {

    "use strict";

    angular.module("app-Instruments")
      .controller("addInstrumentController", addInstrumentController);


    function addInstrumentController($http, $scope) {
        console.log("addPicsController");

        var vm = this;

        var s3Url = 'https://guitarlocker-pics.s3.amazonaws.com';

        vm.Instruments = [];

        vm.newInstrument = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/Instruments")
            .then(function (response) {
                angular.copy(response.data.results, vm.Instruments);
                console.log("response :", response.data.results);
            }, function (error) {
                vm.errorMessage = "Failed to load data " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        var _e_ = new Evaporate({
            signerUrl: 'https://calm-crag-9597.herokuapp.com/auth_upload/',
            aws_key: "AKIAIGHISJZUQSPAWXBA",
            bucket: "guitarlocker-pics",
            logging: false
        });

        $scope.files = null;

        $scope.upload = function () {
            $scope.files = [];
            var files = $('#thumbnailUrl')[0].files;
            for (var i = 0; i < files.length; i++) { $scope.files.push(files[i]); }

            $scope.files.forEach(function (file) {
                var fileKey = 'tmp/' + file.name;
                file.url = s3Url + '/' + fileKey;

                var fileUrl = (s3Url + '/' + fileKey).replace(/ /g, '_');
                vm.newInstrument.url += fileUrl + " ";

                if (file.type === '') { file.type = 'binary/octel-stream'; }

                file.started = Date.now();

                _e_.add({
                    name: fileKey,
                    file: file,
                    contentType: file.type,
                    xAmzHeadersAtInitiate: {
                        'x-amz-acl': 'public-read'
                    },
                    complete: function () {
                        file.completed = true;
                        $scope.$apply();
                    },
                    progress: function (progress) {
                        file.progress = (Math.round((progress * 100) * 100) / 100);

                        var currentTime = Date.now(), progressRemaining = (100 - file.progress),
                            progressionRate = (progressRemaining / file.progress),
                            timeToCurrentPosition = (currentTime - file.started);
                        // return seconds left during download
                        file.timeLeft = Math.round((progressionRate * timeToCurrentPosition) / 1000);
                        $scope.$apply();
                    }
                }); // _e_.add
            }); // forEach


            vm.addInstrument = function () {
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

        }; // upload
    }
})();