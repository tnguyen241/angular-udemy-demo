import { Component, OnInit } from '@angular/core';
import { Vehicle, Make, Filter } from '../../../models/vehicle';
import { VehicleService } from '../../services/vehicle.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles : Vehicle[];
  allVehicles : Vehicle[];
  makes: Make[];
  filter: Filter={    
    sortBy:'',
    isSortAscending:true
  };
  allColumns: any[]=[
    {title:"Id",sortBy:'',isSortable:false},
    {title:"Make",sortBy:'make',isSortable:true},
    {title:"Model",sortBy:'model',isSortable:true},
    {title:"Contact Name",sortBy:'contactName',isSortable:true}
  ];

  constructor(private vehicleService:VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(m => this.makes=m);
    //this.vehicleService.getAllVehicles().subscribe(rs => this.vehicles=this.allVehicles=rs);
    this.populateVehicles();
  }
  populateVehicles()
  {
    this.vehicleService.getAllVehiclesWithFilter(this.filter).subscribe(rs => this.vehicles=rs);
  }

  OnMakeChange(){
    this.populateVehicles();
    //console.log("Filter Change:"+this.filter.makeId);
  }
  ResetFilter()
  {
    this.filter={      
      sortBy:'',
      isSortAscending:true
    };
    this.OnMakeChange();
  }
  OnSortingChange(columnName:string){
    this.filter.sortBy=columnName;
    this.filter.isSortAscending=!this.filter.isSortAscending;
    this.populateVehicles();
  }
}
