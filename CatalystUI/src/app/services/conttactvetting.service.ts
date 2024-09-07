import { Injectable } from '@angular/core';
import { IContact } from '../contacts/contact.model';

@Injectable({
  providedIn: 'root'
})

export class ContactVettingService {
  vettingQueue?: IContact[];

  constructor() { }

  addToContactVetting(contact: IContact){
    if (this.vettingQueue == undefined)
    {
      this.vettingQueue = [contact];      
    }
    else
    {
      this.vettingQueue.push(contact);
    }

    console.log(`Vetting Queue currently has ${this.vettingQueue.length} contacts submitted`);
  }

}
