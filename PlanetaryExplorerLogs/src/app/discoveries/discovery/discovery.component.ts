import { Component, Input } from '@angular/core';
import { Discovery } from '../discovery.model';
import { DiscoveryService } from '../discovery.service';

@Component({
  selector: 'app-discovery',
  standalone: true,
  
  templateUrl: './discovery.component.html',
  styleUrl: './discovery.component.css'
})
export class DiscoveryComponent {
  @Input({required: true}) discovery!: Discovery;

  constructor(private discoveryService: DiscoveryService) { }
  
  onDeleteDiscovery() {
    console.log('delete discovery ' + this.discovery.id);
    this.discoveryService.deleteDiscovery(this.discovery.id);
  }
}
