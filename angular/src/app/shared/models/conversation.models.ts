export interface IConversation {
	sender: number;
	message: string;
}

export interface IConversations {
	chatId: number;
	otherUserId: number;
	otherUsername: string;
}

export interface IMessage {
	chatMessageId: number;
	content: string;
	dateTime: string;
	senderId: number;
	groupChatId: number | null;
	chatId: number;
}
