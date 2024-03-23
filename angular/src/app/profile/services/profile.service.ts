import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import {
	IInterest,
	IOwner,
	IStudent,
	IUserProfile,
} from '../models/profile.models';
import { environment } from 'src/environments/environment.prod';
import { INumeric } from 'src/app/real-estate/models/real-estate.models';

@Injectable()
export class ProfileService {
	protected apiRoute = `${environment.apiUrl}`;

	public interests: IInterest[] = [];

	public documentTypes: INumeric[] = [
		{ value: 0, viewValue: 'Dowód osobisty' },
		{ value: 1, viewValue: 'Legitymacja studencka' },
		{ value: 2, viewValue: 'Paszport' },
	];

	constructor(private httpClient: HttpClient) {}

	public readAllInterests(): Observable<IInterest[]> {
		return this.getInterests('').pipe(
			map(interests => {
				interests.forEach(interest => this.interests.push(interest));
				return this.interests;
			}),
		);
	}

	public addProfileFiles(formData: FormData): Observable<void> {
		return this.httpClient.post<void>(`${this.apiRoute}/users/files`, formData);
	}

	public getStudents(): Observable<IStudent[]> {
		return this.httpClient.get<IStudent[]>('./assets/student-profiles.json');
	}
	public getActualProfile(): Observable<IUserProfile> {
		return this.httpClient.get<IUserProfile>(`${this.apiRoute}/users/my-profile`);
	}
	public getOwners(): Observable<IOwner[]> {
		return this.httpClient.get<IOwner[]>('./assets/owner-profiles.json');
	}
	public getStudent(id: string): Observable<IStudent> {
		return this.httpClient
			.get<IStudent[]>('./assets/student-profiles.json')
			.pipe(map(results => results.find(result => result.id === id) as IStudent));
	}
	public getOwner(id: string): Observable<IOwner> {
		return this.httpClient
			.get<IOwner[]>('./assets/owner-profiles.json')
			.pipe(map(results => results.find(result => result.id === id) as IOwner));
	}
	public getInterests(name = ''): Observable<IInterest[]> {
		let params = new HttpParams();
		params = params.append('name', name);
		return this.httpClient.get<IInterest[]>(`${this.apiRoute}/interests`, {
			params: params,
		});
	}
}
