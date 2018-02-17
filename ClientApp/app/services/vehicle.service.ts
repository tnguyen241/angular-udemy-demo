import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { unescapeIdentifier } from '@angular/compiler';

@Injectable()
export class VehicleService {

  constructor(private http:Http) { }
  public readonly Vehicle_API: string="/api/vehicles";
  getMakes(){
    return this.http.get("/api/makes").map(res=>res.json());
  }
  getModels(){
    return this.http.get("/api/models").map(res=>res.json());
  }
  getFeatures(){
    return this.http.get("/api/features").map(rs=>rs.json());
  }
  getVehicle(id:number) {
      return this.http.get("/api/vehicles/"+id).map(rs => rs.json());
  }
  getAllVehicles() {
    return this.http.get(this.Vehicle_API).map(rs => rs.json());
  }
  getAllVehiclesWithFilter(filter:any) {
    //console.log("getAllVehiclesWithFilter");
    return this.http.get(this.Vehicle_API+"?"+this.BuildQueryString(filter)).map(rs => rs.json());
  }
  BuildQueryString(filterObject:any){
    //console.log("BuildQueryString");
    var parts=[];
    for(var property in filterObject){
      var value=filterObject[property];
      if(value!=null && value!=undefined){
        parts.push(encodeURIComponent(property)+"=" +encodeURIComponent(value));
      }
    }
    var querystring=parts.join("&");
    //console.log(querystring);
    return querystring;
  }
  createVehicle(vehicle:any) {
     return this.http.post("/api/vehicles",vehicle).map(rs => rs.json());
  }  

}
