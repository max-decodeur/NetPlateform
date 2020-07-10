namespace WcfDecryptorService.Models.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        public int id { get; set; }

        [StringLength(200)]
        public string login { get; set; }

        [StringLength(200)]
        public string password { get; set; }

        [StringLength(200)]
        public string usertoken { get; set; }
    }
}
