using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.ConsoleApp.Models
{
    public class Pet : ModelBase
    {
        public ModelBase Category { get; set; }

        public ModelBase[] Tags { get; set; }
        public string[] PhotoUrls { get; set; }
        public string Status { get; set; }
    }
}