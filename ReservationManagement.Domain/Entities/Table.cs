using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagement.Domain.Entities
{
    public class Table
    {
        public Guid Id { get; private set; }
        public int Number { get; private set; }
        public int Capacity { get; private set; }
        public bool IsVip { get; private set; }

        protected Table() { }

        public Table(int number, int capacity, bool isVip)
        {
            Id = Guid.NewGuid();
            Number = number;
            Capacity = capacity;
            IsVip = isVip;
        }
    }
}
