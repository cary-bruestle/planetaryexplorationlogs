import { Component, inject, DestroyRef, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Planet } from '../planet.model';
import { map } from 'rxjs';
import { PlanetService } from '../planet.service';
import { PlanetComponent } from '../planet/planet.component';
import { NewPlanetComponent } from '../new-planet/new-planet.component';

@Component({
    selector: 'app-planet-list',
    templateUrl: './planet-list.component.html',
    styleUrl: './planet-list.component.css',
    imports: [PlanetComponent, NewPlanetComponent]
})
export class PlanetListComponent implements OnInit {
  isAddingPlanet = false;
  
  constructor(private planetService: PlanetService) { }

  get planets() {
    return this.planetService.getPlanets();
  }

  get selectedPlanetId() {
    return this.planetService.selectedPlanetId();
  }

  onStartAddPlanet() {
    this.isAddingPlanet = true;
  }

  onCloseAddPlanet() {
    this.isAddingPlanet = false;
  }

  ngOnInit() {
 
  }
}
