using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Guids
{
    public interface ISequentialIdGenerator
    {
        Guid NextId();
    }
}
