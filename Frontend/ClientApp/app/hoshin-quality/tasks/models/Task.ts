import {EditTaskEvidence} from './EditTaskEvidence';
import { User } from "./User";

export class Task{
       taskID : number;
       entityID: number;
       entityType: number;
       description: string;
       responsibleUserID: string;
       implementationPlannedDate: Date;
       implementationEffectiveDate:Date;
       observation: string;
       result: string;
       taskStateID: number;
       requireEvidence: boolean;
       taskEvidences: EditTaskEvidence[];
       filetodelete:string[];
       responsibleUser: User;
       typeButtonSubmit: string;
       codeState: string;
       taskState;
}