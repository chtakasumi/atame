'use strict';
//*****Lista
Array.prototype.BuscarPorId = function (id) {

    for (var i = 0; i < this.length; i++) {
        if (parseInt(this[i].ID) === parseInt(id)) {
            return angular.copy(this[i]);
        }
    }
    return null;
};

Array.prototype.isValue = function (chave, valor) {
    for (var i = 0; i < this.length; i++) {
        if (this[i][chave] === valor) return true;  
    }
    return false;
};

Array.prototype.sum = function (chave) {
	var total = 0;	
	for (var i = 0; i < this.length; i++) {
		total= total + this[i][chave]; 
	}
    return total;
};
Array.prototype.find = function (chave,value,comparador) {
	var array = new Array();	
	
	for (var i = 0; i < this.length; i++) {		
		if(this[i][chave]===value){
			array.push(this[i]);
		}		
	}
	
    return array;
};

Array.prototype.where = function (chave,comparador,value) {
			
	var array = new Array();	
	
	for (var i = 0; i < this.length; i++) {		
		
		if(comparador==="="){
			if(this[i][chave]===value){
				array.push(this[i]);
			}
		}else if(comparador===">"){
			if(this[i][chave]>value){
				array.push(this[i]);
			}
		}else if(comparador==="<"){
			if(this[i][chave]<value){
				array.push(this[i]);
			}	
		}else if(comparador==="!=="){
			if(this[i][chave]!==value){
				array.push(this[i]);
			}
		}else if(comparador===">=="){
			if(this[i][chave]>=value){
				array.push(this[i]);
			}		
		}else if(comparador==="<=="){
			if(this[i][chave]<=value){
				array.push(this[i]);
			}
		}		
	}
	
    return array;
};


//*****String
String.prototype.In = function () {
    var valor = this.toString();
    for (var i = 0; i < arguments.length; i++) {
        if (parseInt(valor) === parseInt(arguments[i])) {
            return true;
        }
    }
    return false;
};

String.prototype.isNullOrEmpty = function () {
    var valor = this.toString();
    if (valor === null || valor === '' || valor === 'null' || valor === undefined) {
        return true;
    }
    return false;
};

String.prototype.toDate = function () {
	 var obj = this.toString();
	 
	if (obj) {
        obj = obj.toString().indexOf("/Date(") > -1 ? new Date(eval('new '+obj.replace("/", "").replace("/", ""))) : obj;
    }	
    return obj;
};

String.prototype.isNullOrEmpty= function () {
	 var valor = this.toString();	 
	 return (valor===null || valor ==='' || valor===undefined);	
}

//*****Number
Number.prototype.In = function () {
    var valor = this.toString();
    for (var i = 0; i < arguments.length; i++) {
        if (parseInt(valor) === parseInt(arguments[i])) {
            return true;
        }
    }
    return false;
};

Number.prototype.isNullOrEmpty = function () {
	 var valor = this.toString();	 
	 return (valor===null || valor ==='' || valor===undefined);	 
};

Number.prototype.trunc = function (qtdCasasDecimais) {      
    return this.toFixed(qtdCasasDecimais);
};

Number.prototype.subtrair=function(value){	
	if (value.isNullOrEmpty()) return this;	
	var valor1 =  parseFloat(this).toFixed(2);		
	var valor2 =  parseFloat(value).toFixed(2);	
	var resultado = (valor1 - valor2);
	return resultado;
}

