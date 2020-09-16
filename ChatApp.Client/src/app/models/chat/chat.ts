import { User } from '../user/user';

export interface Chat {
    id: number;
    firstMemberId: number;
    firstMember: User;
    secondMemberId: number;
    secondMember: User;
}