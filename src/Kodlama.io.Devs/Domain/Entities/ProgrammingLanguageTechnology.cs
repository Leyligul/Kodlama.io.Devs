using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProgrammingLanguageTechnology: Entity
    {
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public virtual ProgrammingLanguage? Language { get; set; }
        public ProgrammingLanguageTechnology()
        {

        }

        public ProgrammingLanguageTechnology(int id, string name, int programmingLanguageId):this()
        {
            Id = id;
            Name = name;
            ProgrammingLanguageId = programmingLanguageId;
           
        }
    }
}
