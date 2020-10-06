import { Time } from "@angular/common";

export class FindingReassignmentsHistory{
    workflowId: string;
    findingReassignmentHistoryID:number;
    findingID: number;
    reassignedUserID: number;
    plantTreatmentID: number;
    sectorTreatmentID: number;
    lastResponsibleUserID : number;
    createdByUserID:number;
    date: Time;
    state: string;
    rejectComment: string;
    EventData: string;
}