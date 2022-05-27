using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace bkit.Areas.Identity.Data;

// Add profile data for application users by adding properties to the bkitUser class
public class bkitUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "varchar(100)")]
    public string FirstName { get; set; }
    [PersonalData]
    [Column(TypeName = "varchar(100)")]
    public string LastName { get; set; }
}

