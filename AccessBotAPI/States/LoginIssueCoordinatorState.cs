using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja los problemas de inicio de sesión relacionados con el coordinador.
    /// </summary>
    public class LoginIssueCoordinatorState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario respecto a si el problema de inicio de sesión se resolvió después de contactar al coordinador.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario.</param>
        /// <returns>Un mensaje de respuesta basado en si el problema se resolvió o no.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                throw new ArgumentException("Respuesta inválida. Por favor responde con SI o NO.");
            }
            if (response.Message.ToLower() == "si")
            {
                conversationState.CurrentStep = "endConversation";
                return "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día.";
            }
            else if (response.Message.ToLower() == "no")
            {
                conversationState.CurrentStep = "requestEmail";
                return "Por favor proporciona tu correo electrónico y alguien de soporte se comunicará en breve contigo.";
            }
            else
            {
                return "Respuesta inválida. Por favor responde con SI o NO.";
            }
        }
    }
}