using System.Collections.Generic;

namespace BusinessLayer.Models.DALModels
{
    public class FieldTypeModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public virtual IEnumerable<OptionalFieldModel> OptionalFields { get; set; }
    }
}