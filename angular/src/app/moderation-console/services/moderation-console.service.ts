import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IStudentCard } from '../components/student-cards-verification/IStudentCard';
import { IDispute } from '../components/dispute/IDispute';

@Injectable({
	providedIn: 'root',
})
export class ModerationConsoleService {
	constructor(private http: HttpClient) {}

	public getStudentCards(): Observable<IStudentCard[]> {
		return this.http.get<IStudentCard[]>('../../assets/students.json');
	}

	public getDisputes() {
		return this.http.get<IDispute[]>('../../assets/disputes.json');
	}
}
