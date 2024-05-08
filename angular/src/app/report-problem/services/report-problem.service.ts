import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';
import { UntypedFormGroup } from '@angular/forms';

@Injectable({
	providedIn: 'root',
})
export class ReportProblemService {
	constructor(private http: HttpClient) {}

	public reportProblem(form: UntypedFormGroup) {
		return this.http.post(`${environment.apiUrl}/technical-problems`, {
			kind: Number(form.value.kind),
			description: form.value.description,
		});
	}
}
