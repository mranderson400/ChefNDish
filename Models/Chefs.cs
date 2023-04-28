#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefNDish.Models;
public class Chef
{    
    [Key]    

    public int ChefId { get; set; }
    
    [Required(ErrorMessage = "is required")] 
    [Display(Name ="First Name")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "is required")]
    [Display(Name ="Last Name")]
    public string LastName { get; set; }     
    
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Birth {get; set;} 
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;   
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    // public object Author { get; internal set; }
    public List<Dish> AllDishesMade {get; set;} = new List<Dish>(); // 1 User : Null posts relationship
}




