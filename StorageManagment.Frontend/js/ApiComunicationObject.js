class ApiComunicator{
    
    static warhousesApiAdress = 'http://localhost:56819/api/Warehouses';
    static racksApiAdress = 'http://localhost:56819/api/StorageRacks';
    static shelvesApiAdress = 'http://localhost:56819/api/Shelves';
    static contractorsModuleApiAdress = 'http://my-json-server.typicode.com/Grzechu98/demo/contractors';
    static managmentApi = 'http://localhost:56819/api/Managment/';
    constructor(){

    }
    static fetchWarehouses(){
      return fetch(ApiComunicator.warhousesApiAdress,{
        method: 'GET'
      }).then(res => {
        if(!res.ok){
       
     }else{
      return res.json().then(data =>{
         return data;
       });
     }
  });  
    }
    static postWarehouses(warehouse){
        console.log(warehouse);
        fetch(ApiComunicator.warhousesApiAdress,{
             method: 'POST',
             headers: {
              "Content-type" : "application/json"
          },
            body: JSON.stringify(warehouse)
            }).then(res => {
              if(!res.ok){
                document.querySelector("#AddErr").innerHTML = "<p style='color:red;'>Sprawdź wprowadzone dane!</p>"; 
              }else{
                preparePage();
               $('#addModal').modal('hide');
              }
            });
  }
  static putWarehouses(warehouse){
    console.log(warehouse);
    fetch(ApiComunicator.warhousesApiAdress+'/'+warehouse.id,{
         method: 'PUT',
         headers: {
          "Content-type" : "application/json"
      },
        body: JSON.stringify(warehouse)
        }).then(res => {
          if(!res.ok){
            document.querySelector("#EditErr").innerHTML = "<p style='color:red;'>Sprawdź wprowadzone dane!</p>"; 
          }else{
            preparePage();
            $('#editModal').modal('hide');
          }
        });
}

  static getWarehouse(id){
    return fetch(ApiComunicator.warhousesApiAdress+'/'+id,{
             method: 'GET'
           }).then(res => {
             if(!res.ok){
            document.querySelector("#err").innerHTML = "<p style='color:red;'>Nie można znaleźć obiektu o podanym Id</p>"; 
          }else{
           return res.json().then(data =>{
              return data;
            });
          }
        });
  }

  static deleteWarehouse(id){
    fetch(ApiComunicator.warhousesApiAdress+'/'+id,{
      method: 'DELETE'
    }).then(res => {
      if(!res.ok){
        console.log(res.status); 
   }else{
    res.json().then(data =>{
      preparePage();
     });
   }
  });
  }
  static getWarehouseStockStatus(id){
    return fetch(ApiComunicator.warhousesApiAdress+'/'+id+'/stockstatus',{
             method: 'GET'
           }).then(res => {
           return res.json().then(data =>{
              return data;
            });
        });
  }
  static getStorageRack(id){
    return fetch(ApiComunicator.racksApiAdress+'/'+id,{
      method: 'GET'
    }).then(res => {
      if(!res.ok){
     document.querySelector("#err").innerHTML = "<p style='color:red;'>Nie można znaleźć obiektu o podanym Id</p>"; 
   }else{
    return res.json().then(data =>{
       return data;
     });
   }
 });
  }
  static postStorageRack(rack){
    return fetch(ApiComunicator.racksApiAdress,{
        method: 'POST',
        headers: {
         "Content-type" : "application/json"
     },
       body: JSON.stringify(rack)
       }).then(res => {
         if(!res.ok){
           document.querySelector("#AddRackErr").innerHTML = "<p style='color:red;'>Sprawdź wprowadzone dane!</p>"; 
         }else{
            return res.json().then(data =>{
                return data;
              });
         }
       });
  }
  static putStorageRack(rack){
    fetch(ApiComunicator.racksApiAdress+'/'+rack.id,{
         method: 'PUT',
         headers: {
          "Content-type" : "application/json"
      },
        body: JSON.stringify(rack)
        }).then(res => {
          if(!res.ok){
            document.querySelector("#EditRackErr").innerHTML = "<p style='color:red;'>Regał o podanym numerze znajduje sie w magazynie!</p>"; 
          }else{
            //window.location.reload(true);
            preparePage();
          }
        });
}
static deleteRack(id){
  fetch(ApiComunicator.racksApiAdress+'/'+id,{
    method: 'DELETE'
  }).then(res => {
    if(!res.ok){
      console.log(res.status); 
 }else{
  res.json().then(data =>{
   // window.location.reload(true);
   preparePage();
   });
 }
});
}

  static postShelf(shelf){
    return fetch(ApiComunicator.shelvesApiAdress,{
        method: 'POST',
        headers: {
         "Content-type" : "application/json"
     },
       body: JSON.stringify(shelf)
       }).then(res => {
         if(!res.ok){
           document.querySelector("#AddShelfErr").innerHTML = "<p style='color:red;'>Półka o podanym numerze znajduje sie w tym regale!</p>"; 
           console.log("error");
         }else{
            return res.json().then(data =>{  
              return data;
              });
         }
       });
  }
  static getContractorsContractorsModule(){
    fetch(ApiComunicator.contractorsModuleApiAdress,{
             method: 'GET'
           }).then(res => {
             if(!res.ok){
            document.querySelector("#err").innerHTML = "<p style='color:red;'>Nie można znaleźć obiektu o podanym Id</p>"; 
          }else{
            res.json().then(data =>{
              console.log(data);
            });
          }
        });
  }
  static deleteShelf(id){
    fetch(ApiComunicator.shelvesApiAdress+'/'+id,{
      method: 'DELETE'
    }).then(res => {
      if(!res.ok){
        console.log(res.status); 
   }else{
    res.json().then(data =>{
      window.location.reload(true);
     });
   }
  });
  }
  static putShelf(shelf){
    fetch(ApiComunicator.shelvesApiAdress+'/'+shelf.id,{
         method: 'PUT',
         headers: {
          "Content-type" : "application/json"
      },
        body: JSON.stringify(shelf)
        }).then(res => {
          if(!res.ok){
            document.querySelector("#EditShelfErr").innerHTML = "<p style='color:red;'>Półka o podanym numerze znajduje sie w tym regale!</p>"; 
          }else{
            window.location.reload(true);
          }
        });
      }
      static assignRack(reqBody){
         fetch(ApiComunicator.managmentApi+"ContractorRack",{
          method: 'POST',
          headers: {
           "Content-type" : "application/json"
       },
         body: JSON.stringify(reqBody)
        }).then(res => {
          if(res.status != 200){
            console.log(res.status);
            if(res.status === 404){
              document.querySelector("#assignRackErr").innerHTML = "<p style='color:red;'>Kontrahent o podanym NIP'ie nie istnieje!</p>";
            }else{
              document.querySelector("#assignRackErr").innerHTML = "<p style='color:red;'>Brak wolnych regałów w wybranym magazynie</p>";
            }
          }else{
             res.json().then(data =>{
              document.querySelector("#assignRackErr").innerHTML = "<p style='color:green;'>Regał o numerze "+data.rackNumber+"</p>";
            });
          }
        });
      }

      static placeProductWithContractor(obj){
        fetch(ApiComunicator.managmentApi+"PlaceContractorProduct",{
          method: 'POST',
          headers: {
           "Content-type" : "application/json"
       },
         body: JSON.stringify(obj)
        }).then(res => {
          if(res.status != 200){
            document.querySelector("#placeProductErr").innerHTML = "<p style='color:red;'>Błąd dodawania!</p>";
          }else{
             res.json().then(data =>{
               console.log(data);
              document.querySelector("#placedProduct").innerHTML = "<h7>Produkt umieszczono: regał: "+data.shelf.rack.rackNumber+" półka: "+data.shelf.shelfNumber+"</h7>";
              $('#placeProduct').modal('hide');
              $('#productPlaceModal').modal('show');
            });
          }
        });
      }
      static placeProduct(obj){
        console.log(ApiComunicator.managmentApi+"PlaceProduct/"+obj.id);
        fetch(ApiComunicator.managmentApi+"PlaceProduct/"+obj.id,{
          method: 'POST',
          headers: {
           "Content-type" : "application/json"
       },
         body: JSON.stringify(obj)
        }).then(res => {
          if(res.status != 200){
            document.querySelector("#placeProductErr").innerHTML = "<p style='color:red;'>Brak miejsca</p>";
          }else{
             res.json().then(data =>{
              document.querySelector("#placedProduct").innerHTML = "<h7>Produkt umieszczono: regał: "+data.shelf.rack.rackNumber+" półka: "+data.shelf.shelfNumber+"</h7>";
              $('#placeProduct').modal('hide');
              $('#productPlaceModal').modal('show');
            });
          }
        });
      }
}