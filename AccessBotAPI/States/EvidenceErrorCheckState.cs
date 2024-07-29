using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja la verificación de errores en la evidencia proporcionada por el usuario.
    /// </summary>
    public class EvidenceErrorCheckState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario y determina el siguiente paso en la conversación.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario.</param>
        /// <returns>Un mensaje de respuesta basado en la entrada del usuario.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (response.Message.ToLower() == "si")
            {
                conversationState.CurrentStep = "endConversation";
                return "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día.";
            }
            else if (response.Message.ToLower() == "no")
            {
                conversationState.CurrentStep = "requestEvidence";
                return "Ingresa una imagen o video que muestre el problema ocurrido.";
            }
            else
            {
                return "Respuesta inválida. Por favor responde con SI o NO.";
            }
        }
    }
}