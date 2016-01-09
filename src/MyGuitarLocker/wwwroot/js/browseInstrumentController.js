//InstrumentEditorController.js
(function () {
    'use strict';
    angular.module("app-browse")
        .controller("browseInstrumentController", browseInstrumentController);

    function browseInstrumentController ($routeParams, $http, ngAudio) {
        var vm = this;
        vm.InstrumentName = $routeParams.InstrumentName;
        vm.user = $routeParams.user;
        vm.SoundClips = [];
        vm.Images = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        var soundUrl = "/api/Instruments/" + vm.InstrumentName + "/SoundClips/" + vm.user;
        var picUrl = "/api/Instruments/" + vm.InstrumentName + "/Images/" + vm.user;

        $http.get(soundUrl)
          .then(function (response) {
              console.log("response.data", response.data);
              for (var i = 0; i < response.data.length; i++) {
                  vm.SoundClips[i] = {
                      audio: ngAudio.load(response.data[i].url),
                      description: response.data[i].description,
                      id: response.data[i].id,
                      gear: response.data[i].recording_Gear,
                      title: response.data[i].title
                  };
              }
              console.log('vm.SoundClips', vm.SoundClips);
          }, function (err) {
              vm.errorMessage = "Failed to load SoundClips";
          })
          .finally(function () {
              vm.isBusy = false;
          });

        $http.get(picUrl)
          .then(function (response) {
              console.log("response.data", response.data);
              for (var i = 0; i < response.data.length; i++) {
                  vm.Images[i] = {
                      title: response.data[i].title,
                      id: response.data[i].id,
                      description: response.data[i].description,
                      urls: response.data[i].url.split(' ')
                  };
              }
              console.log('vm.Images', vm.Images);
          }, function (err) {
              vm.errorMessage = "Failed to load SoundClips";
          })
          .finally(function () {
              vm.isBusy = false;
          });
    }
})();