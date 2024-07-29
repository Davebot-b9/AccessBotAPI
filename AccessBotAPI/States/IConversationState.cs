using AccessBotAPI.Models;

namespace AccessBotAPI.States
{
    /// <summary>
    /// Define el contrato para manejar el estado de la conversación.
    /// </summary>
    public interface IConversationState
    {
        /// <summary>
        /// Maneja el mensaje del usuario y actualiza el estado de la conversación en consecuencia.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación.</param>
        /// <param name="response">La respuesta del usuario.</param>
        /// <returns>El siguiente mensaje para el usuario basado en su respuesta.</returns>
        string HandleMessage(SupportConversationState conversationState, UserResponse response);
    }
}
