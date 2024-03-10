export enum TypeName {
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
	typeName: TypeName;
	answers: string[];
}
