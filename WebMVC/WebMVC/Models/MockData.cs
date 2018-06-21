using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace WebMVC.Models
{
    [Table("MOCKDATAS")]    
    public class MockData
    {
        //first_name	last_name	email
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MockDataId { get; set; }
        [Column("first_name")]
        [DisplayName("First Name") ]
        public string FirstName { get; set; }
        [Column("last_name")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Column("email")]
        [DisplayName("e-mail")]
        public string Email { get; set; }
    }
}