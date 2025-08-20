using System;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain;

namespace LiteMoney.Application.Interfaces;

public interface IRepositoryWithEntity<T> where T : class, IEntity
{
    Task<IEquatable<T>>  GetForUserAsync(string id, CancellationToken cancellationToken = default);
}