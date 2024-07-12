
using CatalystAPI.Interfaces;
using CatalystAPI.Models;

namespace CatalsytAPI;

public class ContactDataStore{
    public List<IContact> Contacts {get; set;}
    public static ContactDataStore Current {get;} = new ContactDataStore();

    public ContactDataStore(){

        Contacts = new List<IContact>()
        {
                new Contact ("Jack", "Sprat", "comment", 44, 
                                new Address("1230 Main", "Box 0", "Spokane", "WA", "93333"), 
                                "Programmer", "Eid", 1),
                new Contact ("Molly", "Brown", "another comment", 45, 
                                new Address("1231 Main", "Box 1", "Spokane Valley", "WA", "93344"), 
                                "Entrepreneur", "Molly's Bar and Hotel", 2),
                new Contact ("Jackson", "Brown", "Molly's Beau", 45, 
                                new Address("1232 Main", "Box 2", "Liberty Lake", "WA", "97777"), 
                                "Musician","Molly's Bar and Hotel", 3),
                new Contact ("Alex", "Baldwin", "Doesn't know how to handle weapons", 54, 
                                null, "Actor", "AB Productions", 4)
        };
    }
}