using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Testing1.Models
{
    public class Account{
        public Account() { }
    
        public int AccountID { get; set; }

        public string AccountName { get; set; }

        public string AccountMail { get; set; }

        public string Password { get; set; }
        

    }
}