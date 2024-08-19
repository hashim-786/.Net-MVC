namespace StudentPortal.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public String Email { get; set; }
        public string Phone { get; set; }

        public bool isSubscribed { get; set; }
    }
}
