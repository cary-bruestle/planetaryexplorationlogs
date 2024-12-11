import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PlanetService } from '../planet.service';

@Component({
    selector: 'app-new-planet',
    templateUrl: './new-planet.component.html',
    styleUrl: './new-planet.component.css',
    imports: [FormsModule]
})
export class NewPlanetComponent {
  @Output() close = new EventEmitter<void>();

  constructor(private planetService: PlanetService) { }

  newName = '';
  newType = '';
  newClimate = '';
  newTerrain = '';
  newPopulation = '';

  onCancel() {
    this.close.emit();
  }

  onSubmit() {
    this.planetService.addPlanet(
      {
        name: this.newName,
        type: this.newType,
        climate: this.newClimate,
        terrain: this.newTerrain,
        population: this.newPopulation
      }
    );
    this.close.emit();
  }

}
