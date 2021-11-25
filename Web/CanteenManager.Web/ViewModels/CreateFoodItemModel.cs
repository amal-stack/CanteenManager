using System.ComponentModel.DataAnnotations;

namespace CanteenManager.Web.ViewModels;

public class CreateFoodItemModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int CategoryId { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public IFormFile Image { get; set; }

}
