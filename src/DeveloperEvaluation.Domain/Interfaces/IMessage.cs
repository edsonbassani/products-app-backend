using Developerevaluation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developerevaluation.Domain.Interfaces
{
    /// <summary>
    /// Sets the contract to represent a message within the application
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Get the unique identifier for a message
        /// </summary>
        /// <returns>message's id as string</returns>
        public string Id { get; }

        /// <summary>
        /// Get the message type
        /// </summary>
        /// <returns>The message type</returns>
        public string MessageType { get; }

        /// <summary>
        /// Get the message payload
        /// </summary>
        /// <returns>The message's payload as string</returns>
        public string Payload { get; }

        /// <summary>
        /// Get the status of the message
        /// </summary>
        /// <returns>The message's status as string</returns>
        public MessageStatus Status { get; }
    }
}
