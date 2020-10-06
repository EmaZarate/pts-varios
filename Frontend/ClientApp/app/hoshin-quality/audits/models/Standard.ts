import { Aspect } from './Aspect';

export class Standard {
    standardID: number;
    code: string;
    name: string;
    active:boolean;
    aspects: Aspect[]
}