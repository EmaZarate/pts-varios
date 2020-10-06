import {EditTaskEvidence} from './EditTaskEvidence';


export class EditTask{
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
       responsibleUser: string;
       typeButtonSubmit: string;
       codeState: string;
       
}
