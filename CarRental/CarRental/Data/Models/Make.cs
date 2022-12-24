namespace CarRental.Data.Models
{
    public class Make
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Model> Models { get; set; } =  new HashSet<Model>();

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}
