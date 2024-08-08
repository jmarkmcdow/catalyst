using CatalystAPI.Models;

namespace CatalystAPI.Interfaces;

public interface IContact
{
    
    public int Id {get;}
    public string FirstName {get; }
    public string LastName {get; }
    public Address? Address {get;}
    public short Age {get;}
    public string Comments {get;}
    public string Occupation {get;}
    public string Business {get;}
}
