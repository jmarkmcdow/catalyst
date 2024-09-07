import { Component, inject } from '@angular/core';
import { IContact } from './contact.model';
import { NgFor } from '@angular/common';
import { ContactDetailsComponent } from '../contact-details/contact-details.component';
import { ContactVettingService } from '../services/conttactvetting.service';

@Component({
  selector: 'cnw-contacts',
  standalone: true,
  imports: [NgFor, ContactDetailsComponent],
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.css'
})
export class ContactsComponent {
  contacts: IContact[];
  filter: string='';
  projectParticipants?: IContact[];

  // this is the alternate way of injecting the svc into component
//  private contactVettingSvc: ContactVettingService = inject(ContactVettingService);
// this is more readable but may negatively affect testing

  constructor(private contactVettingSvc: ContactVettingService){
    this.contacts = [
      {
        id: 1,
        firstname: 'Tina',
        lastname: 'Turner',
        comments: ['Legend'],
        age: 70,
        address: '334 Main St. Philadelphia, PA 88888',
        occupation: 'Vocalist',
        business: 'Music'
      },
      {
        id: 77,
        firstname: 'James',
        lastname: 'Mason',
        comments: ['Legend'],
        age: 90,
        address: '333 Angel Ave, Paradise, MO',
        occupation: 'Actor',
        business: 'Fox Studios',
      }
    ]
    this.contacts.forEach(e => {
      e.name = `${e.firstname} ${e.lastname}`
    });
  }

  addToVetting(contact: IContact){
    this.contactVettingSvc.addToContactVetting(contact);
  }

  getFilteredContacts(){
    return this.filter === '' 
    ? this.contacts 
    : this.contacts.filter((c) => c.lastname === this.filter);
  }
}
