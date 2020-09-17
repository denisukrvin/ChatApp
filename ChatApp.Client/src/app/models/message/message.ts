import { User } from '../user/user';

export class Message {
    id: number;
    chatId: number;
    userId: number;
    user: User;
    text: string;
    date: Date;
}