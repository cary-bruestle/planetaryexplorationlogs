import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DiscoveryService } from '../discovery.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-discovery',
  standalone: true,
  
  templateUrl: './new-discovery.component.html',
  styleUrl: './new-discovery.component.css',
  imports: [FormsModule]
})
export class NewDiscoveryComponent {
  @Input({required: true}) missionId!: number;
  @Output() close = new EventEmitter<void>();

  constructor(private discoveryService: DiscoveryService) { }

  newName = '';
  newDiscoveryTypeId = 0;
  newLocation = '';
  newDescription = '';
  
  onCancel() {
    this.close.emit();
  }

  onSubmit() {
    this.discoveryService.addDiscovery(
      {
        missionId: this.missionId,
        discoveryTypeId : this.newDiscoveryTypeId, 
        name : this.newName, 
        description : this.newDescription, 
        location: this.newLocation
      }
    );
    this.close.emit();
  }
}