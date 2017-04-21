//tripscontroller.js

(function () {
    "use strict";

    //Getting the existing module (created in app-trips.js)
    //will return an object to which we can add our controller
    angular.module("app-trips")
      .controller("tripsController", tripsController);

//create a call to the server (angular has facility for that) called HTTP) -smilar to constructor injection on server side inASP.net

    function tripsController($http) {

        //expose some data from the controller
                //this.//this - represents the object that is returned from the controller -  lets call it vm
        var vm = this;

        // vm.name = "Mark" //works

        //construct a couple of objects inside array
        vm.trips =
      [
          //{
          //    name: "US Trip ",
          //    created: new Date()
          //},
          //{
          //    name: "World Trip ",
          //    created: new Date()
          //}
      ];

        
        //add new member - Object that will accept data about a new trip
        //give it an object literal, for an object with no properties --need to add that directive (ng-modle at input on trips
        vm.newTrip = {};

        //member on error to get data if fail 
        vm.errorMessage = "";   //can create members here of the view model, that represents non-functional data

        //add a busy flag - because we're about to do a get, we'll mark it as true. 
        //primarilly we want to show user we're waiting on data ..if slow connection or eg tablet
        //then turn off wheter we succeeded or failed, could simply add vm.isBusy = false under succeed or failed but rather;
        //      add to finally block
        //Add to our trips.cs module too: on top: div ng-show="vm.isBusy

        vm.isBusy = true;


        //get data from server
        $http.get("/api/trips")//call "get' is matched to sever verb, so put or post or delete
        //this returns a promise object, one that we can immediately call .tyehn to specify the call back for succes or failure
            //the succeed we can include as a parameter the response we got from the server - contains number of pieces -we're only interested in is response.data
        .then(function (response) {
            //Success
            //now get data (response .data and  push this collection into the trip object on our vm; - this is short cut we could do a for each
            angular.copy(response.data, vm.trips);
        }, function (error){
            //Failure
            vm.errorMessage ="Failed to Load Data: " + error; //add vm.errormessage in trips.c
        })
         .finally(function () {
             vm.isBusy = false;
        });;
                  

        //called when user clicks on Add trip              
            //alert(vm.newTrip.name);//find the name that they typed in
            //Instead of alert lets add it to the collection

            vm.addTrip = function () {
                vm.trips.push({ name: vm.newTrip.name, created: new Date() }); //adds trip to form
                vm.newTrip = {};//tells form data done...clear input
             };
        }
    })();
                
              
                //        $http.post("/api/trips", vm.newTrip)
                //          .then(function (response) {
                //              // success
                      //       vm.trips.push(response.data);
                        //     vm.newTrip = {};
                //          }, function () {
                //              // failure
                //              vm.errorMessage = "Failed to save new trip";
                //          })
                //          .finally(function () {
                //              vm.isBusy = false;
                //          });






 

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

        // vm.name = "Mark"; //fails
        // vm.name = "Mark" //works



//    