
using CatalystAPI.Models;
using CatalystAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpPut(Name = "FullContactUpdate")]
        public ActionResult<Contact> FullUpdateContact(Contact updatedContact){
            // find the contact by id
            IContact currentContact = ContactDataStore.Current.Contacts.Where(c => c.Id == updatedContact.Id).Single();
            int contactIndex = ContactDataStore.Current.Contacts.IndexOf(currentContact);

            // insure contact found and updated
            if (contactIndex == -1)
                return NotFound("Customer ID not found");
            else
                ContactDataStore.Current.Contacts[contactIndex] = updatedContact;

            return CreatedAtRoute("FullContactUpdate", new{ id = updatedContact.Id}, updatedContact);
        }

        [HttpPatch(Name = "PartialContactUpdate")]
        public ActionResult<Contact> PartialUpdateContact(int contactId, JsonPatchDocument<IContact> patchDocument){
            IContact currentContact = ContactDataStore.Current.Contacts.Where(c => c.Id == contactId).SingleOrDefault();
            IContact BackupCopy = currentContact;

            if (currentContact == null)
                return NotFound("Invalid Id");

            patchDocument.ApplyTo(currentContact, ModelState);

            // Validate the PatchDocument
            if (ModelState.IsValid)
                return CreatedAtRoute("PartialContactUpdate", currentContact);
            // validate the Contact models after PatchDocument changes have been applied
            else if (!TryValidateModel(currentContact))
            {
                int index = ContactDataStore.Current.Contacts.IndexOf(currentContact);
                ContactDataStore.Current.Contacts[index] = BackupCopy;
                return BadRequest(ModelState);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete("{contactid:int}", Name = "DeleteContact")]
        public ActionResult DeleteContact(int contactid){
            ContactDataStore.Current.Contacts.RemoveAt(--contactid);
            return NoContent();
        }

    }
}
