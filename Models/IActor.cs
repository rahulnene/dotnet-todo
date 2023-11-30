using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_todo.Models
{
	public interface IActor
	{
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		int Id { get; set; }
	}
}
