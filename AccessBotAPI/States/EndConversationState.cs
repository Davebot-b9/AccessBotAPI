using AccessBotAPI.Models;
using System.Collections.Generic;

namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja el estado de finalización de la conversación.
    /// </summary>
    public class EndConversationState : IConversationState
    {
        private readonly Dictionary<string, SupportConversationState> _conversationStates;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EndConversationState"/>.
        /// </summary>
        /// <param name="conversationStates">Diccionario de estados de conversación.</param>
        public EndConversationState(Dictionary<string, SupportConversationState> conversationStates)
        {
            _conversationStates = conversationStates;
        }

        /// <summary>
        /// Maneja el mensaje del usuario y finaliza la conversación eliminando su estado.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación.</param>
        /// <param name="response">La respuesta del usuario.</param>
        /// <returns>Devuelve null ya que la conversación ha finalizado.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            _conversationStates.Remove(response.UserId);
            return null;
        }
    }
}
