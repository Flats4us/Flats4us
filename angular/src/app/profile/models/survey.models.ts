export enum typeName {
	RADIOBUTTON = 'RADIOBUTTON',
	FORM = 'FORM',
	SWITCH = 'SWITCH',
	SLIDER = 'SLIDER',
	CHECKBOX = 'CHECKBOX',
	SUBQUESTION = 'SUBQUESTION',
}

export interface IQuestionsData {
	id: string;
	title: string;
	content: string;
	typeName: typeName;
	answers: string[];
}
