using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja el estado de la solución de registro.
    /// </summary>
    public class RegisterSolutionState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario sobre si se resolvió el problema de registro.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario.</param>
        /// <returns>Un mensaje de respuesta basado en si el problema se resolvió o no.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (response.Message.ToLower() == "si")
            {
                conversationState.CurrentStep = "endConversation";
                return "Gracias por comunicarte al area de soporte a usuarios. ¡Ten un excelente día!";
            }
            else if (response.Message.ToLower() == "no")
            {
                conversationState.CurrentStep = "requestEvidenceConection";
                return "Ingresa una imagen o video que muestre el problema ocurrido.";
            }
            return null;
        }
    }
}