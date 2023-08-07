using System;
using System.Collections.Generic;

namespace Notebook.Models
{
    public partial class Client
    {
        public int Id { get; set; }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}


