using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Application.Dtos
{
    public record UserResultDto(string? ErrorMessage = null)
    {
        public bool Success => ErrorMessage == null;
    }
    
}
