import { Component, Input } from '@angular/core';
import { DiscoveryService } from '../discovery.service';
import { DiscoveryComponent } from '../discovery/discovery.component';
import { NewDiscoveryComponent } from '../new-discovery/new-discovery.component';

@Component({
  selector: 'app-discovery-list',
  standalone: true,
  
  templateUrl: './discovery-list.component.html',
  styleUrl: './discovery-list.component.css',
  imports: [DiscoveryComponent, NewDiscoveryComponent]
})
export class DiscoveryListComponent {
  @Input({required: true}) missionId!: number;
  isAddingDiscovery = false;
  
  constructor(private discoveryService: DiscoveryService) { }

  discoveries() {
    return this.discoveryService.getDiscoveriesForMission(this.missionId);
  }
  
  onStartAddDiscovery() {
    this.isAddingDiscovery = true;
  }

  onCloseAddDiscovery() {
    this.isAddingDiscovery = false;
  }

  ngOnInit() {
 
  }
}