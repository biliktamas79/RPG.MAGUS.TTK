using MAGUS.TTK.Domain;
using MAGUS.TTK.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Data.Repositories.InMemory
{
    public class InMemorySkillDefinitionRepository : InMemoryRepository<MagusTtkSkillDefinition>, ISkillDefinitionRepository
    {
    }
}
