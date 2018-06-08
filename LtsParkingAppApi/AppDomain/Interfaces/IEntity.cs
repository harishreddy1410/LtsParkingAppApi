//---------------------------------------------------------------------------------------
// Description: entity interface
//---------------------------------------------------------------------------------------
using AppDomain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppDomain.Interfaces
{
    public interface IEntity
    {
        object Id { get; set; }
    }

    public interface IEntity<T> : IEntity, IPassivable, ISoftDelete, ICreatedInfo, IModifiedInfo
    {
        new T Id { get; set; }
    }
}
