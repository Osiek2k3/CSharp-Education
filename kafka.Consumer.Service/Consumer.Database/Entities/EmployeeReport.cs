using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Database.Entities
{
    public record EmployeeReport
    {
        public Guid Id { get; init; }
        public Guid EmployeeId { get; init; }
        public string Name { get;init; }
        public string Surname { get; init; }
        public EmployeeReport(Guid id, Guid EmployeeId, string name, string surname)
        {
            Id = id;
            this.EmployeeId = EmployeeId;
            Name = name;
            Surname = surname;
        }

    }
}
