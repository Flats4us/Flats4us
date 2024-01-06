import { Injectable } from '@angular/core';
import { IStudent } from '../models/roommate-candidate.models';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class RoommateCandidateService {
	constructor(private http: HttpClient) {}

	public getStudent() {
		return this.http.get<IStudent>('../../assets/student.json');
	}
}
