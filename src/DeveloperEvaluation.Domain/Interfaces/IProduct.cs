using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperEvaluation.Domain.Interfaces
{
    /// <summary>
    /// Sets the contract to represent a product within the application
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Get the unique identifier for a product
        /// </summary>
        /// <returns>Product's id as string</returns>
        public string Id { get; }

        /// <summary>
        /// Get the product name
        /// </summary>
        /// <returns>The product name</returns>
        public string Name { get; }

        /// <summary>
        /// Get the product price
        /// </summary>
        /// <returns>The product's price as double</returns>
        public double Price { get; }

        /// <summary>
        /// Get the state of the product
        /// </summary>
        /// <returns>The product's state as boolean</returns>
        public bool Active { get; }
    }
}
