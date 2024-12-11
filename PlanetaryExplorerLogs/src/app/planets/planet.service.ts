import { Injectable, OnInit, signal } from '@angular/core';
import { Planet, PlanetData } from './planet.model';
import { MissionService } from '../missions/mission.service';


@Injectable({ providedIn: 'root' })
export class PlanetService{
  private planets = signal<Planet[]>([]);
  selectedPlanetId = signal<number>(0);
 
  allPlanets = this.planets.asReadonly();
  
  constructor(private missionService: MissionService) {
    const storedplanets = localStorage.getItem('planets');

    if (storedplanets === null || storedplanets === undefined || storedplanets === 'undefined') {
      this.addPlanet({name:"Terra Nova", type:"Terrestrial", climate:"Temperate", terrain:"Mountains and Oceans", population:"Sparse Colonies"});
      this.addPlanet({name: "Xenon Prime", type: "Gas Giant", climate: "Extreme Storms", terrain: "Gaseous Layers", population: "Uninhabited"});
      this.addPlanet({name : "Glaciera", type : "Ice Giant", climate : "Frozen", terrain : "Ice Plains and Subsurface Oceans", population : "Uninhabited"});
      this.addPlanet({name : "Dwarfia", type : "Dwarf", climate : "Arid", terrain : "Deserts", population : "Uninhabited"});
      this.savePlanets();
    }
    else {
      this.planets.set(JSON.parse(storedplanets));
    }
  }

  addPlanet( planetData: {name: string, type: string, climate: string, terrain:string, population: string})
  {
    const newPlanet: Planet = {
      ...planetData,
      id: this.maxid() + 1    
    };
    this.planets.update((oldPlanets) => [...oldPlanets, newPlanet]);
    console.log(`Added planet ` + planetData.name);
    this.savePlanets();
  }


  updatePlanet(planet: Planet)
  {
    const allButThis = this.planets().filter((planet) => planet.id !== planet.id);
    this.planets.update((oldPlanets) => [...allButThis, planet]);
    this.savePlanets();
  }

  private maxid() 
  { 
    return this.planets().length == 0 ? 0 : Math.max(...this.planets().map(p => p.id));
  }
  
  getPlanets() {
    return this.planets;
  }

  getPlanet(id: number): Planet | undefined {
    const foundPlanet = this.planets().find((planet: Planet) => planet.id === id);
    // if (foundPlanet)
    //   console.log(`getPlanet(` + id + `') found`);
    // else
    //   console.log(`*** getPlanet:` + foundPlanet);
    return foundPlanet;
  }

  getSelectedPlanet(): Planet | undefined {
    if (this.selectedPlanetId() === 0)
      return undefined;
    return this.getPlanet(this.selectedPlanetId());
  }

  deletePlanet(id: number) {
    this.missionService.deleteMissionsForPlanet(id);
    this.planets.set(this.planets().filter((planet) => planet.id !== id));
    this.savePlanets();
  }

  private savePlanets() {
    localStorage.setItem('planets', JSON.stringify(this.planets()));
  }

}
