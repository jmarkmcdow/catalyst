
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
        private readonly IMailService _mailService;
        private readonly ContactDataStore _dataStore;

        public ContactsController(ILogger<ContactsController> logger, IMailService mailService, ContactDataStore dataStore){
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentException(nameof(mailService));
            _dataStore = dataStore ?? throw new ArgumentException(nameof(ContactDataStore));
        }

        [HttpGet(Name = "GetAllContacts")]
        public ActionResult<IEnumerable<IContact>> GetAllContacts(){
            try
            {
                var contacts = _dataStore.Contacts;
                this._logger.Log(LogLevel.Information, "{count} contacts retrieved", contacts.Count);
                return Ok(_dataStore.Contacts);
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
                IContact? contact = _dataStore.Contacts.FirstOrDefault(c => c.Id == id);
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
                IContact? contact = _dataStore.Contacts.FirstOrDefault(c => c.LastName == lastname);
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
                IContact currentContact = _dataStore.Contacts.Where(c => c.Id == updatedContact.Id).Single();
                int contactIndex = _dataStore.Contacts.IndexOf(currentContact);

                // insure contact found and updated
                if (contactIndex == -1)
                    return NotFound("Customer ID not found");
                else
                    _dataStore.Contacts[contactIndex] = updatedContact;

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
                IContact currentContact = _dataStore.Contacts.Where(c => c.Id == contactId).SingleOrDefault();
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
                    int index = _dataStore.Contacts.IndexOf(currentContact);
                    _dataStore.Contacts[index] = BackupCopy;
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
                _dataStore.Contacts.RemoveAt(contactid - 1);
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
