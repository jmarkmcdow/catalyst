
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CatalystAPI.Interfaces;

namespace Entities
{
    public abstract class ContactEntity (string firstname, string lastname, string comments, short age, string occupation, string business)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;

        [Required(AllowEmptyStrings = false)]
        string _firstname {get;} = firstname;
        
        [Required(AllowEmptyStrings = false), Length(3, 25)]
        string _lastname {get;} = lastname;
        
        [Required(AllowEmptyStrings = true), Length(3, 25)]
        string _comments {get;} = occupation;
        
        [Range(18, 105)]
        short _age {get;} = age;
        
        [Required(AllowEmptyStrings = true), Length(3, 25)]
        string _occupation {get;} = occupation;
        
        [Required(AllowEmptyStrings = true), Length(3, 25)]
        string _business {get;} = business;
    }
}