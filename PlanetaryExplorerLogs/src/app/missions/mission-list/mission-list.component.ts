import { Component, Input, OnInit } from '@angular/core';
import { MissionService } from '../mission.service';
import { Mission } from '../mission.model';
import { Planet } from '../../planets/planet.model';
import { MissionComponent } from '../mission/mission.component';
import { NewMissionComponent } from '../new-mission/new-mission.component';

@Component({
    selector: 'app-mission-list',
    templateUrl: './mission-list.component.html',
    styleUrl: './mission-list.component.css',
    imports: [MissionComponent, NewMissionComponent]
})
export class MissionListComponent implements OnInit {
  @Input({required: true}) planetId!: number;
  isAddingMission = false;
  
  constructor(private missionService: MissionService) { }

  missions() {
    return this.missionService.getMissionsForPlanet(this.planetId);
  }

  get selectedMissionId() {
    return this.missionService.selectedMissionId();
  }

  onStartAddMission() {
    this.isAddingMission = true;
  }

  onCloseAddMission() {
    this.isAddingMission = false;
  }

  ngOnInit() {
    
  }
}
