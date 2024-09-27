using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArch.Domain.Core.Commands;

namespace CleanArch.Domain.Commands
{
    public abstract class CourseCommand : Command
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Imageurl { get; protected set; }
    }
}
