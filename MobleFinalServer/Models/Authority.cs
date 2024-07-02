namespace MobleFinalServer.Models
{
	public class Authority
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AuthorityView
    {
        public IEnumerable<User> Authorities { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
