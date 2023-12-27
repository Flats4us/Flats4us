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
	name: string;
	content: string;
	trigger: boolean;
	optional: boolean;
	typeName: typeName;
	answers: string[];
}
