using System.ComponentModel.DataAnnotations;

public class Servico
{
    [Key]
    public int id{get;set;} 
    public String Nome{ get; set; } 
public decimal Preco{get; set; }
public Boolean Status{get; set; }

}