
using CatalystAPI.Models;
using CatalystAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using CatalsytAPI.Services;


namespace CatalsytAPI.Controllers
{


    [ApiController]
    [Route("api/Contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly LocalMailService _mailService;

        public ContactsController(ILogger<ContactsController> logger, LocalMailService mailService){
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentException(nameof(mailService));
        }

        [HttpGet(Name = "GetAllContacts")]
        public ActionResult<IEnumerable<IContact>> GetAllContacts(){
            try
            {
                var contacts = ContactDataStore.Current.Contacts;
                this._logger.Log(LogLevel.Information, "{count} contacts retrieved", contacts.Count);
                return Ok(ContactDataStore.Current.Contacts);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("An exception occurred while retrieving all contacts", ex);
                return StatusCode(500, "An exception occurred while retrieving all contacts");
            }
        }

        [HttpGet("{id:int}", Name = "GetContactById")]
        public ActionResult<IContact> GetContact(int id){
            try{
                IContact? contact = ContactDataStore.Current.Contacts.FirstOrDefault(c => c.Id == id);
                if (contact == null)
                {
                    _logger.Log(LogLevel.Information, "No contact with id of {id} found", id);
                    return NotFound(id);
                }
                return Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occurred while retrieving contact by {id}", ex);
                return StatusCode(500, "An exception occurred while retrieving all contacts");
            }
        }

        [HttpGet]
        [Route("{lastname}", Name = "GetContactByLastName")]
        public ActionResult<IContact> GetContact(string lastname){
            try
            {
                IContact? contact = ContactDataStore.Current.Contacts.FirstOrDefault(c => c.LastName == lastname);
                return contact == null ? NotFound() : Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occurred while retrieving contact by {lastname}", ex);
                return StatusCode(500, "An exception occurred while retrieving contact");
            }
        }

        [HttpPut(Name = "FullContactUpdate")]
        public ActionResult<Contact> FullUpdateContact(Contact updatedContact){
            try
            {

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
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occurred while retrieving contact by updating the contact", ex);
                return StatusCode(500, "An exception occurred while updating contact");
            }
        }

        [HttpPatch(Name = "PartialContactUpdate")]
        public ActionResult<Contact> PartialUpdateContact(int contactId, JsonPatchDocument<IContact> patchDocument){
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occurred while retrieving contact by updating the contact", ex);
                return StatusCode(500, "An exception occurred while updating the contact");
            }
        }

        [HttpDelete("{contactid:int}", Name = "DeleteContact")]
        public ActionResult DeleteContact(int contactid){
            try
            {
                ContactDataStore.Current.Contacts.RemoveAt(--contactid);
                _mailService.Send("Contact Deleted"
                    , $"A contact with id:{contactid} was deleted");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occurred while retrieving contact by deleting the contact {contactid}", ex);
                return StatusCode(500, "An exception occurred while deleting the contact");
            }
        }

    }
}
