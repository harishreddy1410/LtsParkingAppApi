using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppDomain.Interfaces;
using AppDomain.Models.Interfaces;

namespace AppDomain.Models.AbstractClasses
{
    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }

        object IEntity.Id
        {
            get { return this.Id; }
            set { this.Id = (T)value; }
        }

        public bool IsActive { get; set; }

        bool IPassivable.IsActive
        {
            get { return this.IsActive; }
            set { this.IsActive = value; }
        }

        public bool IsDeleted { get; set; }

        bool ISoftDelete.IsDeleted
        {
            get { return this.IsDeleted; }
            set { this.IsDeleted = value; }
        }

        public int CreatedBy { get; set; }

        int ICreatedInfo.CreatedBy
        {
            get { return this.CreatedBy; }
            set { this.CreatedBy = value; }
        }

        public DateTime CreatedDate { get; set; }

        DateTime ICreatedInfo.CreatedDate
        {
            get { return this.CreatedDate; }
            set { this.CreatedDate = value; }
        }

        public int? ModifiedBy { get; set; }

        int? IModifiedInfo.ModifiedBy
        {
            get { return this.ModifiedBy; }
            set { this.ModifiedBy = value; }
        }

        public DateTime? ModifiedDate { get; set; }

        DateTime? IModifiedInfo.ModifiedDate
        {
            get { return this.ModifiedDate; }
            set { this.ModifiedDate = value; }
        }
    }


}
