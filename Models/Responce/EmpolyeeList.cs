using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Responce
{
    public class EmpolyeeList
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public int LoginSuccess { get; set; }
        [JsonIgnore]
        public string Message { get; set; }

    }
   
}
