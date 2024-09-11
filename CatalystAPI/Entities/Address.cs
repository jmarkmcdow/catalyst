using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CatalystAPI.Models;

namespace Catalyst.Entities
{
    public class Address (string street, string pobox, string city, string state, string zip)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId;
        
        [Required()]
        [MinLength(3)]
        [MaxLength(25)]
        public string _street {get;} = street;
        
        public string _pobox {get;} = pobox;
        
        [Required()]
        [MinLength(3)]
        [MaxLength(50)]
        public string _city {get;} = city;
        
        [Required()]
        [MinLength(3)]
        [MaxLength(25)]
        public string state {get;} = state;
        
        [Required]
        [MaxLength(10)]
        public string zip {get; } = zip;
        
        [ForeignKey("_contactId")]
        public Contact _contact {get; set;}
        public int _contactId {get; set;}

    }
}