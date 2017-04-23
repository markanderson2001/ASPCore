//app-trips.js
//**Trip View of the app controller**\\
//create function expression - to take code out of the global scope

//ROUTES
// app-trips.js
(function () {

    "use strict";

    // Creating the Module
    angular.module("app-trips", ["simpleControls", "ngRoute"])
      .config(function ($routeProvider) {//an object to define client side route;

          $routeProvider.when("/", {//root of clientside ..wwwroot
              controller: "tripsController",
              controllerAs: "vm",
              templateUrl: "/views/tripsView.html"      
          });
          //$routeProvider.when("/editor", {
          $routeProvider.when("/editor/:tripName", {              
              controller: "tripEditorController",
              controllerAs: "vm",
              templateUrl: "/views/tripEditorView.html"
          });

          $routeProvider.otherwise({ redirectTo: "/" });//back to main route

      });

})();

////(function () {

////    "use strict";

////    // Creating the Module -(with empty array) . add simplecontrol as depnendency so browser won't ignore elements it does not knoe
////    //include dependency to app-trips modula as well (angularjs route
    
////    angular.module("app-trips", ["simpleControls", "ngRoute"])
////    //at this point now we can configure client side routes, we do this with a config method
////    .config(function ($routeProvider) { 

////        $routeProvider.when("/", {
////            controller: "tripsController",//what to do in that route
////            controllerAs: "vm", //the alias for databinding
////            templateUrl: "/views/tripsView.html"//path to html of actual view
////        });
////        // create 2nd route view - editor for individual trip -crtea te view ()shift- f2
////        //add parameter; variable that would be the name of the trip /:tripName - route on a single trp
////        $routeProvider.when("/editor/:tripName" ,{ 
////            controller: "tripEditorController",
////            controllerAs: "vm",
////            templateUrl: "/views/tripEditorView.html"
////        });
        
////                //if none of the routes match
////        $routeProvider.otherwise({ redirectTo: "/" });

////    });
    
////})();