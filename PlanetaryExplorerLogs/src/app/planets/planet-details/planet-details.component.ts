import { Component, Input } from '@angular/core';
import { Planet } from '../planet.model';
import { PlanetService } from '../planet.service';

@Component({
  selector: 'app-planet-details',
  templateUrl: './planet-details.component.html',
  styleUrl: './planet-details.component.css',
  standalone: true
})
export class PlanetDetailsComponent {
  constructor(private planetService: PlanetService) { }
 
  get planet(): Planet | undefined {
    return this.planetService.getSelectedPlanet();
  }

  onDeletePlanet() {
    
  }
}
