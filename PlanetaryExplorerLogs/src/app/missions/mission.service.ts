import { Injectable, OnInit, signal } from '@angular/core';
import { Mission } from './mission.model';
import { DiscoveryService } from '../discoveries/discovery.service';


@Injectable({ providedIn: 'root' })
export class MissionService{
  private missions = signal<Mission[]>([]);
  selectedMissionId = signal<number>(0);
  
  constructor(private discoveryService: DiscoveryService) {
    //localStorage.removeItem('missions');
    const storedmissions = localStorage.getItem('missions');

    if (storedmissions === null || storedmissions === undefined || storedmissions === 'undefined') {
      //this.addMission({name:"First Contact", date:new Date("1/1/2000"), planetId:1, description:"The very first mission ever"});
      //this.saveMissions();
      }
    else {
      this.missions.set(JSON.parse(storedmissions));
    }
  }

  addMission( missionData: {name : string, date : string, planetId : number, description : string})
  {
    const newMission: Mission = {
      ...missionData,
      id: this.maxid() + 1    
    };
    this.missions.update((oldMissions) => [...oldMissions, newMission]);
    this.saveMissions();
    console.log(`Added mission ` + missionData.name);
  }

  private maxid() 
  { 
    return this.missions().length == 0 ? 0 : Math.max(...this.missions().map(p => p.id));
  }
  
  getMissionsForPlanet(planetid: number) : Mission[] {
    return this.missions().filter((mission:Mission) => mission.planetId===planetid);
  }

  getMission(id: number): Mission | undefined {
    const foundMission = this.missions().find((mission: Mission) => mission.id === id);
    // if (foundMission)
    //   console.log(`getMission(` + id + `') found`);
    // else
    //   console.log(`*** getMission:` + foundMission);
    return foundMission;
  }

  unselectMission() { 
    this.selectedMissionId.set(0);
    console.log('unselect mission');
  }

  deleteMission(id: number) {
    this.discoveryService.deleteDiscoveriesForMission(id);
    this.missions.set(this.missions().filter(m => m.id !== id));
    this.saveMissions();
  }

  deleteMissionsForPlanet(planetid: number) {
    this.missions().forEach(m => {
      this.discoveryService.deleteDiscoveriesForMission(m.id);
    });
    this.missions.set(this.missions().filter((m) => m.planetId !== planetid));
  }
  saveMissions() {
    localStorage.setItem('missions', JSON.stringify(this.missions()));
  }
}
