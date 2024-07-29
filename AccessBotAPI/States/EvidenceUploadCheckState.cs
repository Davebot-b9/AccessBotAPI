using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja la verificación de la carga de evidencia.
    /// </summary>
    public class EvidenceUploadCheckState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario respecto a si se resolvió el problema de carga de evidencia.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario.</param>
        /// <returns>Un mensaje de respuesta basado en si el problema se resolvió o no.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (response.Message.ToLower() == "si")
            {
                conversationState.CurrentStep = "endConversation";
                return "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día.";
            }
            else if (response.Message.ToLower() == "no")
            {
                conversationState.CurrentStep = "endConversation";
                return "En breve nos comunicamos contigo para resolver esta incidencia, agradecemos mucho tu paciencia.";
            }
            else
            {
                return "Respuesta inválida. Por favor responde con SI o NO.";
            }
        }
    }
}