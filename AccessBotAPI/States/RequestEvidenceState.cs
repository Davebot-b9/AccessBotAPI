using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja la solicitud y recepción de evidencia general del usuario.
    /// </summary>
    public class RequestEvidenceState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario que contiene la evidencia solicitada.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario, que debe contener la evidencia.</param>
        /// <returns>Un mensaje de confirmación indicando que se ha recibido la evidencia y se dará seguimiento.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                return "La evidencia no puede estar vacía.";
            }
            conversationState.CurrentStep = "endConversation";
            return "En breve nos comunicamos contigo para resolver esta incidencia, agradecemos mucho tu paciencia.";
        }
    }
}