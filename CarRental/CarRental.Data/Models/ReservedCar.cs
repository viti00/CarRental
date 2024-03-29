﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models
{
    [Table("ReservedCars", Schema = "19118076")]
    public class ReservedCar
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(Car))]
        public string? CarId { get; set; }

        public virtual Car? Car { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public ApplicationUser? Tenant { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsPaid { get; set; } = false;

        public DateTime LastModified_19118076 { get; set; }
    }
}
