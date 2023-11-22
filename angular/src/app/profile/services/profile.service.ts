import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { IOwner, IStudent, IUser } from '../models/profile.models';

@Injectable()
export class ProfileService {
	constructor(private httpClient: HttpClient) {}

	public getStudents(): Observable<IStudent[]> {
		return this.httpClient.get<IStudent[]>('./assets/student-profiles.json');
	}
	public getOwners(): Observable<IOwner[]> {
		return this.httpClient.get<IOwner[]>('./assets/owner-profiles.json');
	}
	public getUsers(): Observable<IUser[]> {
		return this.httpClient.get<IUser[]>('./assets/user-profiles.json');
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
	public getUser(id: string): Observable<IUser> {
		return this.httpClient
			.get<IUser[]>('./assets/user-profiles.json')
			.pipe(map(results => results.find(result => result.id === id) as IUser));
	}
}
