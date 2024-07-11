
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
        [HttpGet]
        public ActionResult<IEnumerable<IContact>> GetAllContacts(){
            var contacts = ContactDataStore.Current.Contacts;
                return Ok(ContactDataStore.Current.Contacts);
        }

        [HttpGet("{id:int}")]
        public ActionResult<IContact> GetContact(int id){
            IContact? contact = ContactDataStore.Current.Contacts.FirstOrDefault(c => c.Id == id);
            return contact == null ? NotFound() : Ok(contact);
        }

        [HttpGet]
        [Route("{lastname}")]
        public ActionResult<IContact> GetContact(string lastname){
            IContact? contact = ContactDataStore.Current.Contacts.FirstOrDefault(c => c.LastName == lastname);
            return contact == null ? NotFound() : Ok(contact);
        }
    }
}
