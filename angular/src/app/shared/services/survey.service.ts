import { Injectable } from '@angular/core';
import { IQuestionsData } from '@shared/models/survey.models';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable()
export class SurveyService {
	protected apiRoute = `${environment.apiUrl}/surveys/template`;

	constructor(private http: HttpClient) {}

	public getStudentQuestions() {
		return this.http.get<IQuestionsData[]>(`${this.apiRoute}/student`);
	}

	public getOwnerQuestions() {
		return this.http.get<IQuestionsData[]>(`${this.apiRoute}/owner`);
	}
}
