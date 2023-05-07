export interface IQuestionsData {
	questions: {
		id: number;
		title: string;
		content: string;
		typeName: string;
		answers: string[];
	}[];
}
