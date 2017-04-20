//tripscontroller.js
(function () {
    "use strict";
     

    //Getting the existing module (created in app-trips.js)
    //will return an object to which we can add our controller
    angular.module("app-trips")
  .controller("tripsController", tripsController);
   

    function tripsController() {
         //expose some data from the controller

         //this.//this - represents the object that is returned from the controller -  lets call it vm
         var vm = this;
         vm.name = "Mark";

         //    vm.trips = [];

         //    vm.newTrip = {};

         //    vm.errorMessage = "";
         //    vm.isBusy = true;

         //    $http.get("/api/trips")
         //      .then(function (response) {
         //          // Success
         //          angular.copy(response.data, vm.trips);
         //      }, function (error) {
         //          // Failure
         //          vm.errorMessage = "Failed to load data: " + error;
         //      })
         //      .finally(function () {
         //          vm.isBusy = false;
         //      });

         //    vm.addTrip = function () {

         //        vm.isBusy = true;
         //        vm.errorMessage = "";

         //        $http.post("/api/trips", vm.newTrip)
         //          .then(function (response) {
         //              // success
         //              vm.trips.push(response.data);
         //              vm.newTrip = {};
         //          }, function () {
         //              // failure
         //              vm.errorMessage = "Failed to save new trip";
         //          })
         //          .finally(function () {
         //              vm.isBusy = false;
         //          });
         //}

     }
});
