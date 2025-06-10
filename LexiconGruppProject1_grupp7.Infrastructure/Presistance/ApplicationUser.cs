using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Infrastructure.Presistance;

public class ApplicationUser : IdentityUser
{
    public string? Bio { get; set; }
}
