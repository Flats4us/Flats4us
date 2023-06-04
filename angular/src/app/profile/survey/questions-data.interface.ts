import { TypeName } from './survey.component';
export interface IQuestionsData {
	id: number;
	title: string;
	content: string;
	typeName: TypeName;
	answers: string[];
}
