//'use strict';
///// <reference path="google.maps.d.ts" />
///// <reference path="jquery.d.ts" />
//import places = google.maps.places;

//ÇA MARCHE PAS sur un site asp.net je sais pas pourquoi... j'ai essayé exactement ce code la dans une page html ordinaire et y'a pas eu de problème. bref, a corrigé quand j'aurai le temps
///**
// * Initialise un autocomplete de google maps. La librairie Google places doit être inclus avant celle-ci.
// * @param selector Un selecteur CSS pour un input sur lequel on appliquera l'autocomplete
// */
//function InitMapsAutocomplete(selector: string) {
//    var input = <HTMLInputElement>$(selector)[0];

//    var ac = new places.Autocomplete(input);

    
//    //ac.addListener('place_changed', function () {

//    //    var place = ac.getPlace();
//    //    if (!place.geometry) {
//    //        window.alert("Autocomplete's returned place contains no geometry");
//    //        return;
//    //    });
//}

//InitMapsAutocomplete("#adr");