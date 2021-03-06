﻿(function () {
    'use strict';
    angular.module("app-Instruments")
      .controller("addClipsController", addClipsController);

    addClipsController.$inject = ["$scope", "$routeParams", "$http"];

    var s3Url = 'https://guitarlocker-pics.s3.amazonaws.com';

    function addClipsController($scope, $routeParams, $http) {
        var vm = this;
        vm.newSoundClip = {};
        vm.newSoundClip.url = "";
        var instrument = $routeParams.InstrumentName;

        var _e_ = new Evaporate({
            signerUrl: 'https://calm-crag-9597.herokuapp.com/auth_upload/',
            aws_key: "AKIAIGHISJZUQSPAWXBA",
            bucket: "guitarlocker-pics",
            logging: false
        });

        $scope.files = null;

        $scope.upload = function () {
            $scope.files = [];
            var files = $('#files')[0].files;
            for (var i = 0; i < files.length; i++) { $scope.files.push(files[i]); }

            $scope.files.forEach(function (file) {
                console.log(file);
                var fileKey = 'tmp/' + file.name.replace(/ /g, '_');
                //file.url = file.url.replace(/ /g, '_');
                file.url = s3Url + '/' + fileKey;
                console.log('file :', file);

                var fileUrl = (s3Url + '/' + fileKey).replace(/ /g, '_');
                vm.newSoundClip.url += fileUrl + " ";

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

            $http.post("/api/Instruments/" + instrument + "/SoundClips/", vm.newSoundClip)
              .then(function (response) {
                  console.log(vm.newSoundClip);
                  //success
                  console.log("success");
                  console.log("response.data :", response.data);
                  vm.newSoundClip = {};
              }, function () {
                  //error
                  console.log(vm.newSoundClip);
                  vm.errorMessage = "Failed to save new Instruments";
              })
              .finally(function () {
                  vm.isBusy = false;
              });

        }; // upload
    } //addClipsController
})();