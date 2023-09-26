using System.ComponentModel.DataAnnotations;

namespace TrainX.Models
{
	public partial class User
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Email { get; set; } = null!;
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
		[Display(Name = "Sport")]
		public int SportId { get; set; }

		public virtual Sport? Sport { get; set; }
	}
}
