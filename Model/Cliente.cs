using System.ComponentModel.DataAnnotations;

public class CLiente
{

    [Key]
    public int id{ get;  set; }
    public String nome{ get; set; }
    public String email { get; set; }
    public String senha { get; set; }
    public String CPF { get; set; }

}