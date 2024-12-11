export interface Discovery {
    id : number;
    missionId : number;
    discoveryTypeId : number;
    name : string;
    description : string;
    location: string;
}

export interface DiscoveryType {
    id : number;
    name : string;
    description : string;
}