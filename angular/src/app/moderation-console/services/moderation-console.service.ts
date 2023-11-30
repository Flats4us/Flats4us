import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IDispute } from '../components/dispute/IDispute';
import { IUser } from '../components/id-cards-verification/document.interface';

@Injectable({
	providedIn: 'root',
})
export class ModerationConsoleService {
	constructor(private http: HttpClient) {}

	public getStudentCards(): Observable<IUser[]> {
		return this.http.get<IUser[]>('../../assets/students.json');
	}

	public getDisputes() {
		return this.http.get<IDispute[]>('../../assets/disputes.json');
	}
}
