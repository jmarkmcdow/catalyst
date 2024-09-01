import { Component } from '@angular/core';
import { IContact } from '../contacts/contact.model';

@Component({
  selector: 'cnw-project',
  standalone: true,
  imports: [],
  templateUrl: './project.component.html',
  styleUrl: './project.component.css'
})
export class ProjectComponent {
  Participants?: IContact[];

  addProjectParticipant(contact:IContact){
    if (this.Participants === null)
      this.Participants = [contact];
    else
    {
      this.Participants?.push(contact);
      console.log(`Participant ${contact.lastname} added to project`);
    }
  }
}
