using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string FullName { get; set; }
    public string UserName { get; set; }
    public string PassWord { get; set; }

}