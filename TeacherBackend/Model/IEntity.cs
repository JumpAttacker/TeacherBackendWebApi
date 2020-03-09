using System.ComponentModel.DataAnnotations;

namespace TeacherBackend.Model
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}