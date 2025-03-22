namespace pys.api.Entities
{
    public class Personnel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime FirstStartDate { get; set; }
        public DateTime? EndDate { get; set; } 

    }
}
