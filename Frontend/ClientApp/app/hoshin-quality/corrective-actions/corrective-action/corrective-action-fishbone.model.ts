export interface CorrectiveActionFishbone {
    fishboneID: string;
    correctiveActionID: string;
    causes: CorrectiveActionFishboneCauses[]
}

export interface CorrectiveActionFishboneCauses {
    causeID: number;
    correctiveActionID: number;
    fishboneID: number;
    name: string;
    x1: number;
    x2: number;
    y1: number;
    y2: number;
    whys: CorrectiveActionFishboneWhy[]
}

export interface CorrectiveActionFishboneWhy {
    causeID: number;
    description: string;
    index: number;
    subChildren: string;
    whyID: number;
}