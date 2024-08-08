using System.ComponentModel.DataAnnotations;

namespace CatalystAPI.Models;

public class Address (string street, string pobox, string city, string state, string zip)
{
    
    public int Id;
    [Required]
    public string Street {get; private set;} = street;
    public string POBox {get; private set;} = pobox;
    [Required]
    public string City {get; private set;} = city;
    [Required]
    public string State {get; private set;} = state;
    [Required]
    public string Zip {get; private set;} = zip;
}
