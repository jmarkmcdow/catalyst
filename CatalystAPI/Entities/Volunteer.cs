using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace CatalystAPI.Entities
{
    public class VolunteerEntity(string firstname, string lastname, string comments, short age, string occupation, string business, string project) 
        : ContactEntity (firstname, lastname, comments, age, occupation, business)
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            int VolunteerId;

            [Required(AllowEmptyStrings = true)]
            string _project {get;} = project;
        }
}