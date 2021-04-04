using DIAssignment.Core.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DIAssignment.EventStore.Services
{
    /// <summary>
    /// Connects to a mongo db endpoint
    /// </summary>
    public interface IMongoService
    {
        /// <summary>
        /// Insert a new message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        void Insert<T>(T item) where T : UpsertMessage;

        /// <summary>
        /// List messages
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<T> Get<T>(Expression<Func<T, bool>> exp) where T : UpsertMessage, new();
    }
}
