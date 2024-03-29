﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    [Table("DrivingLicensePhotos", Schema = "19118076")]
    public class DrivingLicensePhoto
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Driver))]
        public string DriverId { get; set; }

        public virtual ApplicationUser Driver { get; set; }

        public byte[] Bytes { get; set; }

        public string Description { get; set; }

        public string FileExtension { get; set; }

        public double Size { get; set; }

        public DateTime LastModified_19118076 { get; set; }
    }
}
