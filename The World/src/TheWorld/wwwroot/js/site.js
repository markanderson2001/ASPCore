//site.js

//JavaScript - Clientside language for client side app in web application
//For this projects use JS within HTML
//	- Object oriented, prototypical inheritance instead of classes
//	- Dynamic - type are not fixed - NOT typeless, but du=ynamically typed
//	- Compiled down to machine code as its being used-just-in-time without intermediate format (eg MSIL, byteCode)
// just in time compile and JS in most cases are justintime compiled like firefox, ie safari
//Reference code in wwwroot - index.html - with new element called script








/*
Could be self-executing anonymous function  or 
An immediately invoked function expression 
 idea is a nameless function that is executed immediately
 looks like this:  and becomes one big expression

*/
(function () {

    //show and hide sidebar --then codde on css #sidebar
    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa");//go find sidebartoggle and as one of the children of it, find i - italic, that is class with fa
    


    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        //    $(this).text("Show SideBar")//sidebar toggle btn txt
        } else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
            
          //  $(this).text("Hide SideBar")
        }
    });
})();


    //jQuery we going to use jquery to find the selectors on the page ***


    //var ele = document.getElementById("username");//returns that object directly to the code--add username" to span id
    /****/
   // var ele = $("#username"); //overcomes browser issue - call right API - no testing for browser issue
   //// ele.innerHTML = "Developer Mark Anderson";
   // ele.text("Mark Anderson");

   


    //events
    //var main = $("#main");  //as referneced in index.html that contans the DIv
    //var main = document.getElementById("main");  //as referneced in index.html that contans the DIv - style of api is using function calls
    //main.on("mouseenter", function () {
    //    main.style = "background-color: #888;";
    //});

    //main.onmouseenter = function () {
    //    main.style.backgroundColor = "#888";
    //};

    //main.on("mouseleave", function () {
    //    main.style = "";
    //    main.onmouseleave = function () {
    //        main.style.backgroundColor = "";
    //    };
    //});


    //var main = $("#main");
    //main.on("mouseenter", function () {
    //  main.style = "background-color: #888;";
    //});
    //main.on("mouseleave", function () {
    //  main.style = "";
    //});
    
    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //  var me = $(this);
    //  alert(me.text()); //look at object inside of the meny handler
    //});
   
    
//})(); //two parenthises to execute




//function startup() {

//    var ele = document.getElementById("username");
//    ////we going to change text inside
//    ele.innerHTML = "MadDad Java";

//    //differt browsers have implimented these API's in differnet ways

//    //events
//    var main = document.getElementById("main");  //as referneced in index.html that contans the DIv
//    //wire up an event  - common call back event in js -one to be used to
//    main.onmouseenter = function () {
//        main.style.backgroundColor = "#888";
//    };


//    main.onmouseleave = function () {
//        main.style.backgroundColor = "";
//    };

//}

//startup()}