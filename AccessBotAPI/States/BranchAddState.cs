using AccessBotAPI.Models;

namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja el estado de adición de sucursales faltantes.
    /// </summary>
    public class BranchAddState : IConversationState
    {
        /// <summary>
        /// Maneja el mensaje del usuario y actualiza el estado de la conversación en consecuencia.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación.</param>
        /// <param name="response">La respuesta del usuario.</param>
        /// <returns>El siguiente mensaje para el usuario basado en su respuesta.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                return "La sucursal faltante no puede estar vacía.";
            }
            conversationState.CurrentStep = "endConversation";
            return "Ingresa la sucursal que no te aparece por favor. Y en breve nos pondremos en contacto contigo, para poder resolver esta incidencia.";
        }
    }
}
