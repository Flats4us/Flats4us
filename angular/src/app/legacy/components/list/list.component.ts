import { Component, OnInit } from '@angular/core';
import { TenantsService } from 'src/app/services/tenants.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit{

  tenants: any;
  currentTenant: any;

  constructor(private tenantsService: TenantsService) {  }

  ngOnInit(): void {
    this.getAllTenants();
  }

  getAllTenants(): void {
    this.tenantsService.list()
      .subscribe(
        (tenants: any) => {
          this.tenants = tenants;
        },
        (error: any) => {
          console.log(error);
        }
      );
  }

  deleteTenant(id: number) {
    this.tenantsService.delete(id)
      .subscribe(
        response => {
          this.getAllTenants();
        },
        error => {
          console.log(error);
        }
      );
  }

}
