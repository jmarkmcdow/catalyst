
using CatalystAPI;
using CatalystAPI.Models;
using CatalystAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CatalsytAPI;

namespace CatalsytAPI.Controllers
{

    [ApiController]
    [Route("api/Contacts")]
    public class ContactsController : ControllerBase
    {
        [HttpGet(Name = "GetAllContacts")]
        public ActionResult<IEnumerable<IContact>> GetAllContacts(){
            var contacts = ContactDataStore.Current.Contacts;
                return Ok(ContactDataStore.Current.Contacts);
        }

        [HttpGet("{id:int}", Name = "GetContactById")]
        public ActionResult<IContact> GetContact(int id){
            IContact? contact = ContactDataStore.Current.Contacts.FirstOrDefault(c => c.Id == id);
            return contact == null ? NotFound() : Ok(contact);
        }

        [HttpGet]
        [Route("{lastname}", Name = "GetContactByLastName")]
        public ActionResult<IContact> GetContact(string lastname){
            IContact? contact = ContactDataStore.Current.Contacts.FirstOrDefault(c => c.LastName == lastname);
            return contact == null ? NotFound() : Ok(contact);
        }

        [HttpPost(Name = "Test")]
        public ActionResult<Contact> CreateNewContact(Contact contact){
            // calculate a new Id for the contact
            var id = (ContactDataStore.Current.Contacts.Max(c => c.Id)) + 1;

            IContact newContact = new Contact(contact.FirstName, contact.LastName, contact.Comments, contact.Age, contact.Address, contact.Occupation, contact.Business, id);

            // Save to current contacts list
            ContactDataStore.Current.Contacts.Add(newContact);

            return CreatedAtRoute("GetContactById",
                new{ id = newContact.Id}, newContact);
        }
    }
}
