export interface IQuestionsData {
	questions: {
		id: number;
		title: string;
		content: string;
		type_name: string;
		answers: string[];
	}[];
}
