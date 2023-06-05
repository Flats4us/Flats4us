import { typeName } from './typeName';
export interface IQuestionsData {
	id: string;
	title: string;
	content: string;
	typeName: typeName;
	answers: string[];
}
