﻿using MediaBrowser.Model.Net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaBrowser.Common.Net
{
    public interface IWebSocketConnection : IDisposable
    {
        /// <summary>
        /// Gets the last activity date.
        /// </summary>
        /// <value>The last activity date.</value>
        DateTime LastActivityDate { get; }

        /// <summary>
        /// Gets or sets the receive action.
        /// </summary>
        /// <value>The receive action.</value>
        Action<WebSocketMessageInfo> OnReceive { get; set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        WebSocketState State { get; }

        /// <summary>
        /// Gets the remote end point.
        /// </summary>
        /// <value>The remote end point.</value>
        string RemoteEndPoint { get; }

        /// <summary>
        /// Sends a message asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">message</exception>
        Task SendAsync<T>(WebSocketMessage<T> message, CancellationToken cancellationToken);

        /// <summary>
        /// Sends a message asynchronously.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        Task SendAsync(byte[] buffer, CancellationToken cancellationToken);

        /// <summary>
        /// Sends a message asynchronously.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="type">The type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">buffer</exception>
        Task SendAsync(byte[] buffer, WebSocketMessageType type, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Class WebSocketMessageInfo
    /// </summary>
    public class WebSocketMessageInfo : WebSocketMessage<string>
    {
        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public IWebSocketConnection Connection { get; set; }
    }
}