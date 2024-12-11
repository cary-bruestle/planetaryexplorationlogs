
import { Injectable, OnInit, signal } from '@angular/core';
import { DiscoveryType } from './discovery.model';


@Injectable({ providedIn: 'root' })


export class DiscoveryTypeService{
    DiscoveryTypeOptions: DiscoveryType[] = [
        {
            id: 1,
            name: "Geological",
            description: "Discoveries related to the planet's geology, such as mineral deposits and tectonic activity."
        },
        {
            id: 2,
            name: "Biological",
            description: "Discoveries pertaining to life forms, ecosystems, and biological phenomena."
        },
        {
            id: 3,
            name: "Technological",
            description: "Findings related to advanced technologies, alien artifacts, or unexplained mechanisms."
        },
        {
            id: 4,
            name: "Atmospheric",
            description: "Discoveries involving atmospheric compositions, weather patterns, and climate anomalies."
        }
    ];    
    

    getDiscoveryTypes() {
        return this.DiscoveryTypeOptions;
    }
    
}

