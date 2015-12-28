//InstrumentEditorController.js
(function () {
    'use strict';
    angular.module("app-Instruments")
        .controller("InstrumentEditorController", InstrumentEditorController);
        
    function InstrumentEditorController($routeParams, $http) {
        var vm = this;
        vm.InstrumentName = $routeParams.InstrumentName;
        vm.SoundClips = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newSoundClip = {};
        
        var url = "/api/Instruments/" + vm.InstrumentName + "/SoundClips";

        $http.get(url)
          .then(function (response) {
              angular.copy(response.data, vm.SoundClips);
              _showMap(vm.SoundClips);
              console.log("_showMap(vm.SoundClips)")
;          }, function (err) {
              vm.errorMessage = "Failed to load SoundClips";
          })
          .finally(function () {
              vm.isBusy = false;
          });

        vm.addSoundClip = function () {
            vm.isBusy = true;
            $http.post(url, vm.newSoundClip)
                .then(function (response) {
                    //success
                    vm.SoundClips.push(response.data);
                    _showMap(vm.SoundClips);
                    vm.newSoundClip = {};
                }, function () {
                    //error
                    vm.errorMessage = "Failed to save new SoundClip";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }

    function _showMap(SoundClips) {
        if (SoundClips && SoundClips.length > 0) {
            var mapSoundClips = _.map(SoundClips, function(item){
                return {
                    lat : item.latitude,
                    long: item.longitude,
                    info : item.name
                };
            });
            //show map
            console.log("_showMap()");
            travelMap.createMap({
                SoundClips: mapSoundClips,
                selector: "#map",
                currentSoundClip: 1,
                initialZoom: 3
            });
        }
    }
})();