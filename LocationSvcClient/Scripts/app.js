///<reference path="lob/angular.js" />

var locationApp = angular.module('locationApp', ['AdalAngular']);

locationApp.config(['$httpProvider', 'adalAuthenticationServiceProvider', function ($httpProvider, adalProvider) {
    var endpoints = {
        "https://localhost:44387/":"80156425-3161-40aa-8f54-b9731ba95e54"
        //"https://JYTechnology.onmicrosoft.com/637952ee-4722-4d4d-8908-77b2893dd1df"
    };

    adalProvider.init({
        instance: 'https://login.microsoftonline.com/',
        tenant: 'JYTechnology.onMicrosoft.com',
        clientId: "a3d084fd-d36a-49ac-9db1-bf92157650d0",
        endpoints: endpoints
    }, $httpProvider);
}]);

var locationController = locationApp.controller("locationController", [
    '$scope', '$http', 'adalAuthenticationService',
    function ($scope, $http,adalService) {
    $scope.getLocation = function () {
        //$http.get("https://localhost:44387/api/Location?cityName=dc").success(function (location) {
         //  $scope.city = location;
       // });
        $http({
            method: 'GET',
            url: 'https://localhost:44387/api/Location?cityName=dc'
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            $scope.city = response.data;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    $scope.login = function () {
        adalService.login();
        
    }

    $scope.logout = function () {
        adalService.logout();
    }
}]);