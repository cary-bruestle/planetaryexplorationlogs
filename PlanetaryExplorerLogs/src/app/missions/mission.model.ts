export interface Mission {
    id : number;
    name : string;
    date : string;      // tbd - process as a date
    planetId : number;
    description : string;
}
export interface MissionData {
    name : string;
    date : string;
    planetId : number;
    description : string;
}