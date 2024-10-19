using CarAuctionApi.Core.Enums;
using CarAuctionApi.Core.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Core.Models
{
    public class Car : BaseEntity
    {
        public string VinCode { get; set; }
        public int FabricationYear { get; set; }
        public double Engine { get; set; }
        public string EngineName { get; set; }
        public double Millages { get; set; }
        public string Description { get; set; }
        public bool RunDrive { get; set; }
        public int HP { get; set; }
        public Guid BanId { get; set; }
        public Guid ModelId { get; set; }
        public Guid ColorId { get; set; }
        public FuelType FuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public virtual Ban Ban { get; set; }
        public virtual Model Model { get; set; }
        public virtual Color Color { get; set; }
        public virtual CarAuctionDetail AuctionDetail { get; set; }
        public virtual ICollection<CarImage> CarImages { get; set; }
    }
}
