namespace TrainX.Models
{
	public partial class Sport
	{
		public Sport()
		{
			Users = new HashSet<User>();
			Videos = new HashSet<Video>();
		}

		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? ImageUrl { get; set; }
		public string? Description { get; set; }

		public virtual ICollection<User> Users { get; set; }
		public virtual ICollection<Video> Videos { get; set; }
	}
}
