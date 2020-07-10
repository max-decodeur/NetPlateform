namespace WcfDecryptorService.Models.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class log
    {
        public int id { get; set; }

        public DateTime? date { get; set; }

        public int? iduser { get; set; }
    }
}
