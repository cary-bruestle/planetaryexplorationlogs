import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormsModule } from '@angular/forms';
// import { MatDatepickerModule } from '@angular/material/datepicker';
// import { MatInputModule } from '@angular/material/input';
// import { MatFormFieldModule } from '@angular/material/form-field';

import { MissionService } from '../mission.service';
// import { MatNativeDateModule } from '@angular/material/core';

@Component({
    selector: 'app-new-mission',
    templateUrl: './new-mission.component.html',
    styleUrl: './new-mission.component.css',
    imports: [
        // MatDatepickerModule,
        // MatNativeDateModule,
        // MatInputModule,
        // MatFormFieldModule,
        FormsModule
    ]
})
export class NewMissionComponent {
  @Input({required: true}) planetId!: number;
  @Output() close = new EventEmitter<void>();

  constructor(private missionService: MissionService) { }

  newName = '';
  newDate = '';
  newDescription = '';
  
  onCancel() {
    this.close.emit();
  }

  onSubmit() {
    this.missionService.addMission(
      {
        name: this.newName,
        date: this.newDate,
        planetId: this.planetId,
        description: this.newDescription
      }
    );
    this.close.emit();
  }

}
