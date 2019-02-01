'use strict';
var customFilter = angular.module('customFilter', [])
.filter("cpfFormat", function () {
    return function(value) {
        if (value.length === 11) {
            var firstGroup = value.substring(0, 3);
            var secondGroup = value.substring(3, 6);
            var thirdGroup = value.substring(6, 9);
            var safeDigit = value.substring(9, 11);

            return firstGroup + "." + secondGroup + "." + thirdGroup + "-" + safeDigit;
        }
        return value;
    };
})
.filter("cnpjFilter", function() {
    return function(value) {
        if (value.lenth === 14) {
            return value.substring(0, 2) + "." +
                value.substring(2, 5) + "." +
                value.substring(5, 8) + "/" +
                value.substring(8, 12) + "-" +
                value.substring(12, 14);
        }
        return value;
    };
})
.filter("cepFilter", function() {
    return function(value) {
        if (value.length === 8) {
            return value.substring(0, 2) + "." + value.substring(2, 5) + "-" + value.substring(5, 8);
        }
        return value;
    };
})
 .filter("foneFilter", function() {
     return function(value) {
         if (value.length === 10) {
             return "(" + value.substring(0, 2) + ") " + value.substring(2, 6) + "-" + value.substring(6, 10);
         } else if (value.length === 11) {
             return "(" + value.substring(0, 2) + ") " + value.substring(2, 7) + "-" + value.substring(7, 12);
         }
         return value;
     };
 })
 .filter("setMaxLength", function() {
     return function(input, maxLength) {    	 
    	 if(input ===undefined) return;
         if (input.length > maxLength) {
             return input.substring(0, (maxLength - 4)) + " ...";
         } else {
             return input;
         }
     };
 })
.filter("dataFilter", function() {
    return function(value) {    	 	
        if (value) {         	        	        	
        	var data = new Date(value);//browser antigos não funcionam dessa forma aqui.  
        	var dia =data.getDate() ;
        	var mes =(data.getMonth() + 1);
        	var ano =data.getFullYear();        	
        	return dia+'/'+ mes + '/' +ano;
        	        
        } else {
           return value;
        }
    };
}).filter("money", function() {
	
    return function(value) { 
    	
    	if (value) { 
    		
    		value = value.toString();
    		
    		if(value.indexOf(".")===-1){
    			value+=".00";
    		}
    		
    		value = value.replace(".",",");      		   
    		  
    		var decimal= value.substr(value.indexOf(","),3);
    		var valor= value.substr(0,value.indexOf(","));
    		    		    		
    		var valorNew = '';
    		var posicao='';
    		
    		for(var i=valor.length-1;i>-1;i--){ 
    			posicao+=valor.substr(i,1);     			    			
    			valorNew = valor.substr(i,1) + valorNew;     		 			    			    			
    			if(posicao.length ===3){
    				valorNew +='.';
    				posicao='';
    			}
    		}
    		return "R$ " + valorNew + decimal;
    	};
    	return "R$ 00,00"
    }
});



