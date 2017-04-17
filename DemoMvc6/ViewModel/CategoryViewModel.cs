namespace DemoMvc6.ViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
