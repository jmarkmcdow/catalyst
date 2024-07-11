namespace CatalystAPI.Models;

public class Address (string street, string pobox, string city, string state, string zip)
{
    public int Id;
    public string Street {get; private set;} = street;
    public string POBox {get; private set;} = pobox;
    public string City {get; private set;} = city;
    public string State {get; private set;} = state;
    public string Zip {get; private set;} = zip;

}
