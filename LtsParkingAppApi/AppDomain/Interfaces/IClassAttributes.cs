using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.Models.Interfaces
{
    /// <summary>
    /// This interface is used to make an entity active/passive.
    /// </summary>
    public interface IPassivable
    {
        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        bool IsActive { get; set; }
    }

    /// <summary>
    /// Used to standardize soft deleting entities.
    /// Soft-delete entities are not actually deleted,
    /// marked as IsDeleted = true in the database,
    /// but can not be retrieved to the application.
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Used to mark an Entity as 'Deleted'. 
        /// </summary>
        bool IsDeleted { get; set; }
    }

    /// <summary>
    /// This interface is used to find the user it was created and Created Date
    /// </summary>
    public interface ICreatedInfo
    {
        /// <summary>
        ///  This Entity will give us the Entity Created by the user
        /// </summary>
        //[MaxLength(32)]
        Guid CreatedBy { get; set; }

        /// <summary>
        ///  This Entity will give us the Entity Created on a date
        /// </summary>
        DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// This interface is used to find the user it was modified by and the date modified on 
    /// </summary>
    public interface IModifiedInfo
    {
        /// <summary>
        ///  This Entity will give us the Entity modified by the user
        /// </summary>
        //[MaxLength(32)]
        Guid? ModifiedBy { get; set; }

        /// <summary>
        ///  This Entity will give us the Entity modified on a date
        /// </summary>
        DateTime? ModifiedDate { get; set; }
    }

}
