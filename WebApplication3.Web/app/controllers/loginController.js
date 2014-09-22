'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'toaster', function ($scope, $location, authService, toaster) {

    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        console.log("try login");

        authService.login($scope.loginData).then(function (response) {

            $location.path('/orders');

        },
         function (err) {
             $scope.message = err.data.message;
             toaster.pop('error', 'Login error', 'Invalid username or password');

         });
    };

}]);