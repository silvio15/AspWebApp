var app = angular.module("app", []);


app.controller('glavniController', function ($scope, $http, $location, $timeout, $rootScope, $window) {

    //URL ZA DOHVAĆANJE, KREIRANJE, AŽURIRANJE I BRISANJE OSOBA
    var url = "https://localhost:44362/api/Osoba/";


    //URL ZA DOHVAĆANJE SPOLOVA OSOBA
    var urlSpolovi = "https://localhost:44362/api/Spol/";


    //DOHVAĆANJE SVIH SPOLOVA OSOBA
    $http({
        method: "GET",
        url: urlSpolovi,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        contentType: "application/json; charset=utf-8"
    }).then(
        function (response) {

            $scope.aSpolovi = response.data;
        },
        function (error) {
            console.log(error);
        });   

    //FUNKCIJA ZA DOHVAĆANJE OSOBA 
    $scope.dohvatiSveOsobe = function () {

        $http({
            method: "GET",
            url: url,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            contentType: "application/json; charset=utf-8"
        }).then(
            function (response) {

                $scope.aOsobe = response.data;
            },
            function (error) {
                console.log(error);
            });   
    }

    //DOHVAĆANJE SVIH OSOBA
    $http({
        method: "GET",
        url: url,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        contentType: "application/json; charset=utf-8"
    }).then(
        function (response) {

            $scope.aOsobe = response.data;
        },
        function (error) {
            console.log(error);
        });   


    //*************  KREIRANJE GUID ID-a    */
    function CreateGuid() {
        function _p8(s) {
            var p = (Math.random().toString(16) + "000000000").substr(2, 8);
            return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
        }
        return _p8() + _p8(true) + _p8(true) + _p8();
    }


    //**************     MODAL  DODAJ OSOBU   **************/
    //OTVARANJE MODALA DODAJ OSOBU
    $scope.otvoriModalDodajOsobu = function () {

        var modal_popup = angular.element("#ModalDodajOsobu");
        modal_popup.modal("show");
    }

    //ZATVARANJE MODALA ZA DODAVANJE
    $scope.zatvoriModalDodajOsobu = function () {

        var modal_popup = angular.element("#ModalDodajOsobu");
        modal_popup.modal("hide");
    }

    // DODAVANJE OSOBE
    $scope.DodajOsobu = function () {

        if ($scope.input_datumRodenja == undefined || $scope.input_datumRodenja == "") {
            $scope.input_datumRodenja = new Date();
        }
        var guid = CreateGuid();
            var osoba = {
                Id:guid,
                Ime: $scope.input_ime,
                Prezime: $scope.input_prezime,
                DatumRodenja: $scope.input_datumRodenja,
                Spol: $scope.option_spol,
                Telefon: $scope.input_telefon,
                Adresa: $scope.input_adresa
        };

            $http({
                method: "POST",
                url: url+"Post",
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                data: osoba,
            }).then(
                function (response) {

                    $scope.aAddOsoba = response;

                    $scope.dohvatiSveOsobe();
                    $scope.zatvoriModalDodajOsobu();
                    $scope.resetAddFormValues();
                },
                function (error) {
                    console.log(error);
                }
            );   
    }

    // SPREMANJE ZAPISA
    $scope.SpremiZapis= function () {

        if ($scope.input_zapis_datumRodenja == undefined || $scope.input_zapis_datumRodenja == "") {
            $scope.input_zapis_datumRodenja = new Date();
        }
        var guid = CreateGuid();
        var osoba = {
            Id: guid,
            Ime: $scope.input_zapis_ime,
            Prezime: $scope.input_zapis_prezime,
            DatumRodenja: $scope.input_zapis_datumRodenja,
            Spol: $scope.option_zapis_spol,
            Telefon: $scope.input_zapis_telefon,
            Adresa: $scope.input_zapis_adresa
        };

        $http({
            method: "POST",
            url: url + "Post",
            headers: { 'Content-Type': 'application/json; charset=utf-8' },
            data: osoba,
        }).then(
            function (response) {

                $scope.aAddZapisOsoba = response;
                $scope.dohvatiSveOsobe();
            },
            function (error) {
                console.log(error);
            }
        );
    }

    //ČIŠĆENJE POLJA FORME DODAJ ZAPIS NAKON DODAVANJA 
    $scope.resetAddFormValues = function () {
        $scope.input_ime = "";
        $scope.input_prezime = "";
        $scope.input_datumRodenja = "";
        $scope.option_spol = "";
        $scope.input_telefon = "";
        $scope.input_adresa = "";
    }

    //ČIŠĆENJE POLJA FORME ZAPISA 
    $scope.ResetFormZapis = function () {
        $scope.input_zapis_ime = "";
        $scope.input_zapis_prezime = "";
        $scope.input_zapis_datumRodenja = "";
        $scope.option_zapis_spol = "";
        $scope.input_zapis_telefon = "";
        $scope.input_zapis_adresa = "";
    }


    //*****************   MODAL ZA AŽURIRANJE PODATAKA ODABRANE OSOBE   */
    //OTVARANJE MODALA ZA AŽURIRANJE
    $scope.otvoriModalAzurirajOsobu = function (id) {

        var modal_popup = angular.element("#ModalAzurirajOsobu");
        modal_popup.modal("show");

        $http({
            method: "GET",
            url: url+id,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            contentType: "application/json; charset=utf-8"
        }).then(
            function (response) {

                $scope.aOsoba = response.data;

                angular.forEach($scope.aOsoba, function (value, key) {
                    if (value.Id == id) {

                        $scope.edit_id = value.Id;
                        $scope.input_edit_ime = value.Ime,
                        $scope.input_edit_prezime = value.Prezime,
                        $scope.input_edit_datumRodenja = new Date(value.DatumRodenja),
                        $scope.option_edit_spol = value.Spol,
                        $scope.input_edit_telefon = value.Telefon,
                        $scope.input_edit_adresa = value.Adresa
                    }
                })
            },
            function (error) {
                console.log(error);
            }
        );
    }


    //ZATVARANJE MODALA ZA AZURIRANJE
    $scope.zatvoriModalAzurirajOsobu = function () {

        var modal_popup = angular.element("#ModalAzurirajOsobu");
        modal_popup.modal("hide");
    }


    // AŽURIRANJE OSOBE
    $scope.AzurirajOsobu = function () {
        id = $scope.edit_id;

            var osoba = {
                Id: id,
                Ime: $scope.input_edit_ime,
                Prezime: $scope.input_edit_prezime,
                DatumRodenja: $scope.input_edit_datumRodenja,
                Spol: $scope.option_edit_spol,
                Telefon: $scope.input_edit_telefon,
                Adresa: $scope.input_edit_adresa
            };

            $http({
                method: "PUT",
                url: url + id,
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                data : osoba,
            }).then(
                function (response) {

                    $scope.aEditOsobe = response;
                    $scope.dohvatiSveOsobe();
                    $scope.zatvoriModalAzurirajOsobu();
                },
                function (error) {
                    console.log(error);
                }
            );   
    }


    //*****************     MODAL ZA BRISANJE KORISNIKA     */
    //OTVARANJE MODALA ZA BRISANJE
    $scope.otvoriModalObrisiOsobu = function (id) {

        var modal_popup = angular.element("#ModalObrisiOsobu");
        modal_popup.modal("show");

        $http({
            method: "GET",
            url: url+ id,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            contentType: "application/json; charset=utf-8"
        }).then(
            function (response) {

                $scope.aOdbOsoba = response.data;
                angular.forEach($scope.aOdbOsoba, function (value, key) {

                    if (value.Id == id) {

                        $scope.delete_id = value.Id;
                        $scope.input_obrisi_ime = value.Ime,
                            $scope.input_obrisi_prezime = value.Prezime,
                            $scope.input_obrisi_datumRodenja = new Date(value.DatumRodenja),
                            $scope.option_obrisi_spol = value.Spol,
                            $scope.input_obrisi_telefon = value.Telefon,
                            $scope.input_obrisi_adresa = value.Adresa
                    }
                })
            },
            function (error) {
                console.log(error);
            }
        );
    }

    //ZATVARANJE MODALA ZA BRISANJE
    $scope.zatvoriModalObrisiOsobu = function () {

        var modal_popup = angular.element("#ModalObrisiOsobu");
        modal_popup.modal("hide");
    }

    // Brisanje vozila      
    $scope.obrisiOsobu = function () {

        var id = $scope.delete_id;
        $http({
            method: "DELETE",
            url: url +id,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(
            function (response) {
                $scope.aDeleteOsoba = response.data;
                $scope.dohvatiSveOsobe();
                $scope.zatvoriModalObrisiOsobu();
            },
            function (error) {
                console.log(error);
            }
        );   

    };
});
