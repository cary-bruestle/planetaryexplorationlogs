import { Component, Input } from '@angular/core';
import { Mission } from '../mission.model';
import { DatePipe } from '@angular/common';
import { MissionService } from '../mission.service';

@Component({
    selector: 'app-mission',
    templateUrl: './mission.component.html',
    styleUrl: './mission.component.css',
    standalone: true,
    imports: [DatePipe]
})
export class MissionComponent {
  @Input({required: true}) mission!: Mission;
  @Input({required: true}) selected!: boolean;

  constructor(private missionService: MissionService) { }

  onSelectMission() {
    console.log(`mission.onSelectMission ` + this.mission.id);
    this.missionService.selectedMissionId.set(this.mission.id);    
  }

  onDeleteMission() {
    this.missionService.deleteMission(this.mission.id);
  }
}
