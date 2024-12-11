import { Injectable, OnInit, signal } from '@angular/core';
import { Discovery } from './discovery.model';


@Injectable({ providedIn: 'root' })
export class DiscoveryService{
  private discoveries = signal<Discovery[]>([]);
  
  constructor() {
    const storeddiscoveries = localStorage.getItem('discoveries');

    if (storeddiscoveries === null || storeddiscoveries === undefined || storeddiscoveries === 'undefined') {
   
      }
    else {
      this.discoveries.set(JSON.parse(storeddiscoveries));
    }
  }

  addDiscovery( discoveryData: { missionId : number, discoveryTypeId : number, name : string, description : string, location: string})
  {
    const newDiscovery: Discovery = {
      ...discoveryData,
      id: this.maxid() + 1    
    };
    this.discoveries.update((oldDiscoveries) => [...oldDiscoveries, newDiscovery]);
    this.saveDiscoveries();
    console.log(`Added discovery ` + discoveryData.name);
  }

  private maxid() 
  { 
    return this.discoveries().length == 0 ? 0 : Math.max(...this.discoveries().map(d => d.id));
  }
  
  getDiscoveriesForMission(missionid: number) : Discovery[] {
    return this.discoveries().filter((discovery:Discovery) => discovery.missionId===missionid);
  }

  getDiscovery(id: number): Discovery | undefined {
    const foundDiscovery = this.discoveries().find((discovery: Discovery) => discovery.id === id);
     return foundDiscovery;
  }

  deleteDiscovery(id: number) {
    this.discoveries.set(this.discoveries().filter(d => d.id !== id));
    this.saveDiscoveries();
  }

  deleteDiscoveriesForMission(missionid: number) {
    this.discoveries.set(this.discoveries().filter(d => d.missionId !== missionid));
    //console.log ('deleteDiscoveriesForMission(' + missionid + ') --> ' + this.discoveries().length + ' discoveries remaining');
  }

  saveDiscoveries() {
    localStorage.setItem('discoveries', JSON.stringify(this.discoveries()));
  }
}
