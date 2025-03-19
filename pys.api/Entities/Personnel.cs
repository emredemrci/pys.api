namespace pys.api.Entities
{
    public class Personnel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime FirstStartDate { get; set; }

        public DateTime? EndDate { get; set; } 

    }
}
