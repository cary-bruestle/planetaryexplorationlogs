import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PlanetListComponent } from './planets/planet-list/planet-list.component';
import { HeaderComponent } from './header/header.component';
import { PlanetService } from './planets/planet.service';
import { PlanetDetailsComponent } from './planets/planet-details/planet-details.component';
import { MissionListComponent } from './missions/mission-list/mission-list.component';
import { MissionService } from './missions/mission.service';
import { DiscoveryListComponent } from './discoveries/discovery-list/discovery-list.component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [HeaderComponent, PlanetListComponent, PlanetDetailsComponent, MissionListComponent, DiscoveryListComponent]
})
export class AppComponent {
  constructor(private planetService: PlanetService, private missionService: MissionService) { }
 
  get selectedPlanetId() {
    return this.planetService.selectedPlanetId();
  }
  get selectedMissionId() {
    return this.missionService.selectedMissionId();
  }

}

// 1. List Planets
// 2. Make Planet component
// 3. Select planet
// 4. Show planet details of selected planet
// 5. Add planet
// 6. Save in local storage
// 7. Show list of missions for planet
// 8. Add mission
// 9. Show list of discoveries for mission
// 10. Add discovery
// 11. Delete discovery
// 12. Unselect mission when selecting different planet
// 13. Delete mission
// 14. Delete planet


// TBD
//  Get CORS problem resolved
//  Rewrite services to use API
//  Use datepicker for Mission Date
//  Discovery type dropdown





