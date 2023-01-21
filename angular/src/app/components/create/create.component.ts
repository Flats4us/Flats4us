import { Component, OnInit } from '@angular/core';
import { TenantsService } from 'src/app/services/tenants.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit{

  tenant = {
    name: '',
    surname: '',
    addressLine1: '',
    addressLine2: '',
    addressLine3: '',
    email: '',
    phoneNumber: ''
  };
  isTenantAdded = false;

  constructor(private tenantsService: TenantsService) {}

  ngOnInit(): void {  }

  addTenant(): void {
    const data = {
      name: this.tenant.name,
      surname: this.tenant.surname,
      addressLine1: this.tenant.addressLine1,
      addressLine2: this.tenant.addressLine2,
      addressLine3: this.tenant.addressLine3,
      email: this.tenant.email,
      phoneNumber: this.tenant.phoneNumber
    }

    this.tenantsService.create(data)
      .subscribe(
        response => {
          console.log(response);
          this.isTenantAdded = true;
        },
        error => {
          console.log(error);
        }
      );
  }

  newTenant(): void {
    this.isTenantAdded = false;
    this.tenant = {
      name: '',
      surname: '',
      addressLine1: '',
      addressLine2: '',
      addressLine3: '',
      email: '',
      phoneNumber: ''
    }
  }
}
