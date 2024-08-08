using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    [Required (ErrorMessage = "A first name must be provided")]
    [MaxLength(50)]
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

    [Required (ErrorMessage = "A last name must be provided")]
    [MaxLength(75)]
    public string LastName { 
        get{
            return this._lastname;
        }
        private set{
            this._lastname = value;
        }
    }

    [MaxLength(200)]
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
    [MaxLength(75)]
    
    public string Occupation{
        get{
            return this._occupation;
        }
        private set{
            this._occupation = value;
        }
    }
    [MaxLength(75)]
    
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
