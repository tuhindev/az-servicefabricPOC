using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using MyEcommerceApp.UserActors.Interfaces;

namespace MyEcommerceApp.UserActors
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class UserActors : Actor, IUserActors
    {
        /// <summary>
        /// Initializes a new instance of UserActors
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public UserActors(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public async Task AddToCart(Guid productId, int quantity)
        {
            await StateManager.AddOrUpdateStateAsync(productId.ToString(), quantity, (id, old) =>
            {
                return old + quantity;
            });
        }

        public async Task<Dictionary<Guid, int>> GetCart()
        {
            var result = new Dictionary<Guid, int>();

            IEnumerable<string> productIDs = await StateManager.GetStateNamesAsync();

            foreach (string productId in productIDs)
            {
                //Accidentally added one count value to the actor, thus getting rid of it.
                if (!string.Equals("count", productId))
                {
                    int quantity = await StateManager.GetStateAsync<int>(productId);
                    result[new Guid(productId)] = quantity;
                }
            }
            return result;
        }

        

        ///// <summary>
        ///// TODO: Replace with your own actor method.
        ///// </summary>
        ///// <returns></returns>
        //Task<int> IUserActors.GetCountAsync(CancellationToken cancellationToken)
        //{
        //    return this.StateManager.GetStateAsync<int>("count", cancellationToken);
        //}

        ///// <summary>
        ///// TODO: Replace with your own actor method.
        ///// </summary>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //Task IUserActors.SetCountAsync(int count, CancellationToken cancellationToken)
        //{
        //    // Requests are not guaranteed to be processed in order nor at most once.
        //    // The update function here verifies that the incoming count is greater than the current count to preserve order.
        //    return this.StateManager.AddOrUpdateStateAsync("count", count, (key, value) => count > value ? count : value, cancellationToken);
        //}
    }
}
