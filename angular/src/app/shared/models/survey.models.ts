export enum TypeName {
	RADIOBUTTON = 'RADIOBUTTON',
	FORM = 'FORM',
	SWITCH = 'SWITCH',
	SLIDER = 'SLIDER',
	CHECKBOX = 'CHECKBOX',
	SUBQUESTION = 'SUBQUESTION',
	TEXT = 'TEXT',
	NUMBER = 'NUMBER',
}

export interface IQuestionsData {
	id: string;
	name: string;
	trigger: boolean;
	optional: boolean;
	typeName: TypeName;
	answers: string[];
}
