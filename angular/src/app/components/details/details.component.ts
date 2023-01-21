import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TenantsService } from 'src/app/services/tenants.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit{

  currentTenant: any;
  message = '';

  constructor(
    private tenantsService: TenantsService,
    private route: ActivatedRoute,
    private router: Router
  ) { }
    
  ngOnInit(): void {
    this.message = '';
    this.getTenant(this.route.snapshot.paramMap.get('id'));
  }

  getTenant(id: string | null): void {
    this.tenantsService.getItem(id)
      .subscribe(
        (tenant: null) => {
          this.currentTenant = tenant;
          console.log(tenant);
        },
        (error: any) => {
          console.log(error);
        });
  }

  setAvailableStatus(status: any): void {
    const data = {
      name: this.currentTenant.name,
      surname: this.currentTenant.surname,
      addressLine1: this.currentTenant.addressLine1,
      addressLine2: this.currentTenant.addressLine2,
      addressLine3: this.currentTenant.addressLine3,
      email: this.currentTenant.email,
      phoneNumber: this.currentTenant.phoneNumber,
      available: status
    };

    this.tenantsService.update(this.currentTenant.id, data)
      .subscribe(
        response => {
          this.currentTenant.available = status;
          console.log(response);
        },
        error => {
          console.log(error);
        });
  }

  updateTenant(): void {
    this.tenantsService.update(this.currentTenant.tenantId, this.currentTenant)
      .subscribe(
        response => {
          console.log(response);
          this.message = 'The tenant was updated!';
        },
        error => {
          console.log(error);
        });
  }

  deleteTenant(): void {
    this.tenantsService.delete(this.currentTenant.tenantId)
      .subscribe(
        response => {
          console.log(response);
          this.router.navigate(['/tenants']);
        },
        error => {
          console.log(error);
        });
  }
}