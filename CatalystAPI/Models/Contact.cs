using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using CatalystAPI.Interfaces;

namespace CatalystAPI.Models;
public abstract class Contact (string firstname, string lastname, string comments, short age, Address address, string occupation, string business,int id = 0 ) : IContact
{
    public int _id = id;
    string _firstname = firstname;
    string _lastname = lastname;
    string _comments = comments;
    short _age = age;
    Address? _address = address;
    string _occupation = occupation;
    string _business = business;
    

    public int Id{
        get{
            return this._id;
        }
    }
    public string FirstName {
        get
        {
            return this._firstname;
        } 
        private set
        {
            this._firstname = value;
        }
    }
    public string LastName { 
        get{
            return this._lastname;
        }
        private set{
            this._lastname = value;
        }
    }

    public string Comments{
        get{
            return this._comments;
        }
        private set{
            this._comments = value;
        }
    }

    public short Age{
        get{
            return this._age;
        }
        private set{
            this._age = value;
        }
    }
    public Address? Address {
        get{
            return this._address;
        }
        set{
            this._address = value;
        }
    }
    public string Occupation{
        get{
            return this._occupation;
        }
        private set{
            this._occupation = value;
        }
    }
    public string Business{
        get{
            return this._business;
        }
        private set{
            this._business = value;
        }
    }
}

public class Volunteer (string firstname, string lastname, string comments, short age, Address address, string occupation, string business, int id = 0) : Contact (firstname, lastname, comments, age, address, occupation, business, id)
{
}
