import { User } from '../user/user';

export interface Message {
    id: number;
    chatId: number;
    userId: number;
    user: User;
    text: string;
    date: Date;
}