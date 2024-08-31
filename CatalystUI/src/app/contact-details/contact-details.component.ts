import { Component, Input } from '@angular/core';
import { IContact } from '../contacts/contact.model';

@Component({
  selector: 'cnw-contact-details',
  standalone: true,
  imports: [],
  templateUrl: './contact-details.component.html',
  styleUrl: './contact-details.component.css'
})
export class ContactDetailsComponent {
  @Input() contact!: IContact;

  getImageUrl (contact: IContact){
    return `/images/${contact.firstname}${contact.lastname}.jpeg`;
  }

  editContact (contact: IContact){
    console.log('editContact: Not implemented');
  }
}
