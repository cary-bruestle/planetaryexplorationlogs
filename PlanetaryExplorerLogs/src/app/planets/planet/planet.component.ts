import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Planet } from '../planet.model';
import { PlanetService } from '../planet.service';
import { MissionService } from '../../missions/mission.service';

@Component({
  selector: 'app-planet',
  templateUrl: './planet.component.html',
  styleUrl: './planet.component.css',
  standalone: true
})
export class PlanetComponent {
  @Input({required: true}) planet!: Planet;
  @Input({required: true}) selected!: boolean;

  constructor(private planetService: PlanetService, private missionService: MissionService) { }

  onSelectPlanet() {
    console.log(`planet.onSelectPlanet ` + this.planet.id);
    this.planetService.selectedPlanetId.set(this.planet.id);
    this.missionService.unselectMission();
  }
}
