import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';
import { ToastyService, ToastyConfig, ToastOptions, ToastData } from "ng2-toasty";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/Observable/forkJoin';
import { Vehicle } from '../../../models/vehicle';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
    makes: any[];
    features: any[];    
    models:any[];
    vehicle: Vehicle = {
      id: 0,
      model: {
        id:0,
        name:''
      },
      make: {
        id:0,
        name:'',
        models:[]
      },
      isRegistered: true,
      contact: {
        email:'',
        name:'',
        phone:''
      },
      features: []    
    };
  
  constructor(private route:ActivatedRoute, private router:Router,
      private vehicleService: VehicleService, private toastyService: ToastyService, private toastyConfig: ToastyConfig) {
      this.toastyConfig.theme = 'bootstrap';
      this.route.params.subscribe(p => this.vehicle.id = +p['id']);
  }

  ngOnInit() {
    //console.log('On init');
    var sources=[
      this.vehicleService.getMakes(),      
      this.vehicleService.getFeatures(),
      this.vehicleService.getModels(),
    ];
    if (this.vehicle.id)
    {
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));      
    }
    Observable.forkJoin(sources).subscribe(rs=> {
      this.makes=rs[0];      
      this.features=rs[1];
      this.models=rs[2];
      if (this.vehicle.id)
      {
        this.vehicle=rs[3];
        this.populateModels();
      }
    });        
  }
  hasFeature(feature: any){    
    var selectedFeature = this.vehicle.features.find(f => f.featureId == feature.id);
    if(selectedFeature){
      //console.log("selectedFeature"+selectedFeature+"feature.id="+feature.id);
      return true;
    }
    //console.log("feature.id="+feature.id);
    return false;
  }
  OnMakeChange(){
    this.populateModels();
    delete this.vehicle.model.id;
  }
  private populateModels()
  {
    if(this.vehicle.make){
      var selectedMake = this.makes.find(m => m.id == this.vehicle.make.id);          
    }
    this.models = selectedMake ? selectedMake.models : [];
  }
  OnFeatureToggle(feature: any, $event: any) {
      if ($event.target.checked) {
          this.vehicle.features.push(feature);
      }
      else {
          var index = this.vehicle.features.indexOf(feature.id);
          this.vehicle.features.splice(index, 1);
      }
  }
  onSubmit() {
      this.vehicleService.createVehicle(this.vehicle)
          .subscribe(
          rs => console.log(rs));
  } 
}

