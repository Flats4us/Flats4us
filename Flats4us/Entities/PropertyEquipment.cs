namespace Flats4us.Entities
{
    public class PropertyEquipment
    {
        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }

        public int EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set;}
    }
}
