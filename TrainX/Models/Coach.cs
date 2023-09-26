namespace TrainX.Models
{
	public partial class Coach
	{
		public Coach()
		{
			Videos = new HashSet<Video>();
		}

		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;

		public virtual ICollection<Video> Videos { get; set; }
	}
}
