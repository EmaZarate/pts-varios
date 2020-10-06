import { EditTaskEvidence } from './EditTaskEvidence';

export class EditTaskUpdate
{
        TaskID: number;
        EntityID: number;
        EntityType: number;
        ImplementationEffectiveDate: string;
        Observation: string;
        TaskStateID:string;
        TaskStateCode:string;
        TaskEvidences:EditTaskEvidence[];
        DeleteEvidencesUrls: string[];
}