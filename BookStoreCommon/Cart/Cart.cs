using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Cart
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        [Required(ErrorMessage = "UserId is null")]
        public int UserId { get; set; }
        //public virtual UserRegister UserRegister { get; set; }
        [Required(ErrorMessage = "BookId is null")]
        public int BookId { get; set; }
        //public virtual Book Book { get; set; }
    }
}
